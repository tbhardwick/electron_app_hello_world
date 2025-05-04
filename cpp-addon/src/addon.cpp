#include "BTICARD.H" // Include the vendor header (Path relative to include_dirs)
#include "BTI429.H" // Added BTI429 Header - Verify Name!
#include "bti_constants.h" // Added constants header

// Then include standard and N-API headers
#include <napi.h>
#include <windows.h>
#include <string>
#include <iostream>
#include <vector>
#include <thread>
#include <chrono>
#include <map>              // For storing latest words/timestamps
#include <atomic>           // For monitoringActive flag
#include <mutex>            // Potentially needed for data structures if accessed outside monitor thread
#include <condition_variable> // Potentially needed for signaling thread

// --- Define Constants ---
const int ARINC_CHANNEL_COUNT = 8;

// --- Global State for ARINC Monitoring ---
std::atomic<bool> monitoringActive(false);
std::thread monitorThread;
std::mutex dataMutex; // Mutex for protecting shared data structures if needed
HCARD hCardGlobal = nullptr;
HCORE hCoreGlobal = nullptr;
std::vector<LISTADDR> receiveListAddrs(ARINC_CHANNEL_COUNT, 0); // Store List Addrs per channel
std::map<int, std::map<int, ULONG>> latestWords; // channel -> label -> word
std::map<int, std::map<int, std::chrono::steady_clock::time_point>> lastUpdateTimes; // channel -> label -> timestamp

// ThreadSafeFunctions for callbacks to JavaScript
Napi::ThreadSafeFunction tsfnDataUpdate = nullptr;
Napi::ThreadSafeFunction tsfnErrorUpdate = nullptr;

// --- Forward Declarations ---
void MonitorLoop();
long long steady_clock_to_epoch_ms(const std::chrono::steady_clock::time_point& tp);

// Helper structure for passing data to JS callbacks
struct ArincUpdateData {
    int channel;
    int label;
    ULONG word;
    long long timestamp_ms; // Use epoch ms
};

struct ArincErrorData {
    int channel = -1; // Use -1 or similar for global errors
    int status_code;
    std::string message;
};

// Callback wrappers for ThreadSafeFunction
void CallJsDataUpdate(Napi::Env env, Napi::Function jsCallback, std::vector<ArincUpdateData>* updates) {
    if (!updates) return;

    Napi::Array jsArray = Napi::Array::New(env, updates->size());
    for (size_t i = 0; i < updates->size(); ++i) {
        const auto& update = (*updates)[i];
        Napi::Object obj = Napi::Object::New(env);
        obj.Set("channel", Napi::Number::New(env, update.channel));
        obj.Set("label", Napi::Number::New(env, update.label));
        obj.Set("word", Napi::Number::New(env, update.word));
        obj.Set("timestamp", Napi::Number::New(env, (double)update.timestamp_ms)); // Pass timestamp as number
        jsArray.Set(i, obj);
    }

    jsCallback.Call({jsArray});
    delete updates; // Clean up the heap-allocated vector
}

void CallJsErrorUpdate(Napi::Env env, Napi::Function jsCallback, ArincErrorData* errorData) {
    if (!errorData) return;

    Napi::Object obj = Napi::Object::New(env);
    if (errorData->channel >= 0) { // Check if it's a channel-specific error
        obj.Set("channel", Napi::Number::New(env, errorData->channel));
    } else {
        obj.Set("channel", env.Null()); // Indicate global error
    }
    // Map status code to string if possible, otherwise send code
    std::string statusStr;
    if(errorData->status_code == ERR_NONE) statusStr = "OK";
    else if (errorData->status_code == ERR_TIMEOUT) statusStr = "TIMEOUT";
    // Add more BTI specific error mappings here based on bti_constants.h
    else statusStr = "ERROR";

    obj.Set("status", Napi::String::New(env, statusStr));
    obj.Set("message", Napi::String::New(env, errorData->message));
    obj.Set("code", Napi::Number::New(env, errorData->status_code)); // Include the raw code

    jsCallback.Call({obj});
    delete errorData; // Clean up the heap-allocated data
}

// --- Timestamp Helper ---
long long steady_clock_to_epoch_ms(const std::chrono::steady_clock::time_point& tp) {
    // This conversion assumes steady_clock's epoch is reasonably close to system_clock's epoch
    // or that the difference is consistent. A more robust solution might calibrate at startup.
    auto duration = tp.time_since_epoch();
    return std::chrono::duration_cast<std::chrono::milliseconds>(duration).count();
    // A potentially more accurate way (if needed and C++20 available):
    // return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::system_clock::now().time_since_epoch()).count() +
    //        std::chrono::duration_cast<std::chrono::milliseconds>(tp - std::chrono::steady_clock::now()).count();
}

// --- Async Worker for ListDataRd ---
class ListDataRdWorker : public Napi::AsyncWorker {
public:
    ListDataRdWorker(Napi::Env env, ULONG listAddr, HCORE hCore, int timeoutMs)
        : Napi::AsyncWorker(env), listAddr_(listAddr), hCore_(hCore), timeoutMs_(timeoutMs), deferred_(Napi::Promise::Deferred::New(env)), dataWord_(0), status_(ERR_TIMEOUT) {} // Default status to timeout

    ~ListDataRdWorker() {}

    // Executed in worker thread
    void Execute() override {
        using namespace std::chrono;
        auto startTime = steady_clock::now();
        bool timedOut = false;

        while (true) {
            int currentListStatus = BTI429_ListStatus(listAddr_, hCore_); 

            if (currentListStatus < 0) { // Error checking status
                // Use generic error message but store specific BTI code
                SetError("Error checking list status."); 
                status_ = currentListStatus;
                return;
            }

            // Use defined constants for status check
            if (currentListStatus == STAT_PARTIAL || currentListStatus == STAT_FULL) { 
                dataWord_ = BTI429_ListDataRd(listAddr_, hCore_); 
                
                // ** VERIFICATION STILL NEEDED on how ListDataRd signals empty/error vs valid 0 **
                // Assuming ListDataRd returns the word directly and errors must be checked via status
                status_ = ERR_NONE; // Indicate success
                return; 
            }

            auto now = steady_clock::now();
            if (duration_cast<milliseconds>(now - startTime).count() > timeoutMs_) {
                // SetError will be called by AsyncWorker if status_ is still ERR_TIMEOUT
                timedOut = true; 
                break; 
            }
            std::this_thread::sleep_for(milliseconds(10));
        }
        
        // If loop exits due to timeout
        if (timedOut) {
             SetError("Timeout waiting for data.");
             status_ = ERR_TIMEOUT; // Ensure status reflects timeout
        }
    }

    // Executed in event loop thread
    void OnOK() override {
        Napi::HandleScope scope(Env());
        Napi::Object result = Napi::Object::New(Env());
        result.Set("status", Napi::Number::New(Env(), status_)); // Use status_ member
        if (status_ == ERR_NONE) {
            result.Set("value", Napi::Number::New(Env(), dataWord_)); 
        } else {
            result.Set("value", Env().Null());
        }
        deferred_.Resolve(result);
    }

    // Executed in event loop thread
    void OnError(const Napi::Error& e) override {
        Napi::HandleScope scope(Env());
        Napi::Object errorObj = Napi::Object::New(Env());
        errorObj.Set("status", Napi::Number::New(Env(), status_)); // Provide BTI/Timeout status
        errorObj.Set("message", Napi::String::New(Env(), e.Message()));
        errorObj.Set("value", Env().Null());
        deferred_.Reject(errorObj);
    }

    Napi::Promise GetPromise() { return deferred_.Promise(); }

private:
    ULONG listAddr_;
    HCORE hCore_;
    int timeoutMs_;
    Napi::Promise::Deferred deferred_;
    ULONG dataWord_;
    int status_; // Store BTI status or error code
};

// --- Async Worker for ListDataBlkRd ---
class ListDataBlkRdWorker : public Napi::AsyncWorker {
public:
    ListDataBlkRdWorker(Napi::Env env, ULONG listAddr, int maxCount, HCORE hCore, int timeoutMs)
        : Napi::AsyncWorker(env), listAddr_(listAddr), maxCount_(maxCount), hCore_(hCore), timeoutMs_(timeoutMs), deferred_(Napi::Promise::Deferred::New(env)), status_(ERR_TIMEOUT), actualCountRead_(0) { // Default status
            // Pre-allocate buffer based on maxCount
            if (maxCount_ > 0) {
                readBuffer_.resize(maxCount_);
            }
        }

    ~ListDataBlkRdWorker() {}

    // Executed in worker thread
    void Execute() override {
        using namespace std::chrono;
        auto startTime = steady_clock::now();
        bool timedOut = false;

        if (maxCount_ <= 0) { 
            status_ = ERR_NONE; // Success, read 0 items
            actualCountRead_ = 0;
            return;
        }

        while (true) {
            int currentListStatus = BTI429_ListStatus(listAddr_, hCore_); 

            if (currentListStatus < 0) { 
                SetError("Error checking list status.");
                status_ = currentListStatus;
                actualCountRead_ = 0;
                return;
            }

            // Use defined constants
            if (currentListStatus == STAT_PARTIAL || currentListStatus == STAT_FULL) { 
                USHORT countActuallyRead = 0; // Variable to receive the actual count
                // Pass address of countActuallyRead for the second parameter
                BOOL success = BTI429_ListDataBlkRd(readBuffer_.data(), &countActuallyRead, listAddr_, hCore_);

                if (success) { 
                    status_ = ERR_NONE; 
                    actualCountRead_ = countActuallyRead; // Store the count returned by the function
                } else { 
                    // Attempt to determine if it was Underflow or another error
                    int postReadStatus = BTI429_ListStatus(listAddr_, hCore_);
                    if (postReadStatus == STAT_EMPTY && countActuallyRead == 0) { // Check if list is empty AND 0 items were read
                        status_ = ERR_NONE; // Treat read 0 as success
                        actualCountRead_ = 0;
                    } else if (postReadStatus == STAT_EMPTY) { // List empty but read > 0 attempted?
                        status_ = ERR_UNDERFLOW; // Indicate underflow
                        actualCountRead_ = 0;
                        // Don't SetError, let OnError handle based on status_
                    } else {
                        status_ = postReadStatus < 0 ? postReadStatus : ERR_FAIL; // Use status error or generic fail
                        SetError("Error reading data block."); 
                        actualCountRead_ = 0;
                    }
                }
                return; // Exit loop after read attempt (success or fail)
            }

            auto now = steady_clock::now();
            if (duration_cast<milliseconds>(now - startTime).count() > timeoutMs_) {
                timedOut = true;
                break;
            }
            std::this_thread::sleep_for(milliseconds(10)); 
        }

        // If loop exits due to timeout
        if (timedOut) {
             SetError("Timeout waiting for data block.");
             status_ = ERR_TIMEOUT;
             actualCountRead_ = 0; // Ensure count is 0 on timeout
        }
    }

    // Executed in event loop thread
    void OnOK() override {
        Napi::HandleScope scope(Env());
        Napi::Object result = Napi::Object::New(Env());
        result.Set("status", Napi::Number::New(Env(), status_)); // Use status_ member

        if (status_ == ERR_NONE) { // If BTI call status was OK (includes underflow)
            Napi::Array dataArray = Napi::Array::New(Env(), actualCountRead_);
            for (int i = 0; i < actualCountRead_; ++i) {
                dataArray.Set(i, Napi::Number::New(Env(), readBuffer_[i]));
            }
            result.Set("dataArray", dataArray);
        } else {
             result.Set("dataArray", Env().Null());
        }
        deferred_.Resolve(result);
    }

    // Executed in event loop thread
    void OnError(const Napi::Error& e) override {
        Napi::HandleScope scope(Env());
        Napi::Object errorObj = Napi::Object::New(Env());
        errorObj.Set("status", Napi::Number::New(Env(), status_)); // Provide BTI/Timeout status
        errorObj.Set("message", Napi::String::New(Env(), e.Message()));
        errorObj.Set("dataArray", Env().Null());
        deferred_.Reject(errorObj);
    }

    Napi::Promise GetPromise() { return deferred_.Promise(); }

private:
    ULONG listAddr_;
    int maxCount_;
    HCORE hCore_;
    int timeoutMs_;
    Napi::Promise::Deferred deferred_;
    std::vector<ULONG> readBuffer_;
    int status_;
    int actualCountRead_;
};

// --- Existing N-API Wrappers (Static Linking) ---

// N-API Wrapper for BTICard_CardOpen
Napi::Value CardOpenWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();
  if (info.Length() != 1 || !info[0].IsNumber()) {
    Napi::TypeError::New(env, "Number expected for card number").ThrowAsJavaScriptException();
    return env.Null();
  }
  int cardNum = info[0].As<Napi::Number>().Int32Value();
  HCARD cardHandle = nullptr; 
  ERRVAL result = BTICard_CardOpen(&cardHandle, cardNum);
  Napi::Object resultObj = Napi::Object::New(env);
  resultObj.Set("resultCode", Napi::Number::New(env, result));
  if (result == 0 && cardHandle != nullptr) { // Assuming 0 (ERR_NONE) is success
    resultObj.Set("success", Napi::Boolean::New(env, true));
    resultObj.Set("handle", Napi::Number::New(env, reinterpret_cast<uintptr_t>(cardHandle)));
    resultObj.Set("message", Napi::String::New(env, "Card opened successfully."));
  } else {
    resultObj.Set("success", Napi::Boolean::New(env, false));
    resultObj.Set("handle", env.Null());
    // Consider adding error description here: 
    // const char* errStr = BTICard_ErrDescStr(result, nullptr); 
    // resultObj.Set("message", Napi::String::New(env, errStr ? errStr : "Failed to open card."));
    resultObj.Set("message", Napi::String::New(env, "Failed to open card.")); 
  }
  return resultObj;
}

// N-API Wrapper for BTICard_CoreOpen
Napi::Value CoreOpenWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();
  if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
    Napi::TypeError::New(env, "Expected: core number (Number), card handle (Number)").ThrowAsJavaScriptException();
    return env.Null();
  }
  int coreNum = info[0].As<Napi::Number>().Int32Value();
  HCARD cardHandle = reinterpret_cast<HCARD>(info[1].As<Napi::Number>().Int64Value());
  HCORE coreHandle = nullptr; 
  ERRVAL result = BTICard_CoreOpen(&coreHandle, coreNum, cardHandle);
  Napi::Object resultObj = Napi::Object::New(env);
  resultObj.Set("resultCode", Napi::Number::New(env, result));
  if (result == 0 && coreHandle != nullptr) {
    resultObj.Set("success", Napi::Boolean::New(env, true));
    resultObj.Set("handle", Napi::Number::New(env, reinterpret_cast<uintptr_t>(coreHandle)));
    resultObj.Set("message", Napi::String::New(env, "Core opened successfully."));
  } else {
    resultObj.Set("success", Napi::Boolean::New(env, false));
    resultObj.Set("handle", env.Null());
    // Consider adding error description here: 
    // const char* errStr = BTICard_ErrDescStr(result, coreHandle); // Pass coreHandle if available
    // resultObj.Set("message", Napi::String::New(env, errStr ? errStr : "Failed to open core."));
    resultObj.Set("message", Napi::String::New(env, "Failed to open core."));
  }
  return resultObj;
}

// N-API Wrapper for BTICard_CardTest
Napi::Value CardTestWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();
  if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
    Napi::TypeError::New(env, "Expected: test level (Number), core handle (Number)").ThrowAsJavaScriptException();
    return env.Null();
  }
  USHORT testLevel = (USHORT)info[0].As<Napi::Number>().Uint32Value();
  HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());
  ERRVAL result = BTICard_CardTest(testLevel, coreHandle);
  return Napi::Number::New(env, result);
}

// N-API Wrapper for BTICard_CardClose
Napi::Value CardCloseWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();
  if (info.Length() != 1 || !info[0].IsNumber()) {
    Napi::TypeError::New(env, "Expected: card handle (Number)").ThrowAsJavaScriptException();
    return env.Null();
  }
  HCARD cardHandle = reinterpret_cast<HCARD>(info[0].As<Napi::Number>().Int64Value());
  ERRVAL result = BTICard_CardClose(cardHandle);
  return Napi::Number::New(env, result);
}

// N-API Wrapper for BTICard_ErrDescStr
Napi::Value GetErrorDescriptionWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();
  if (info.Length() != 2 || !info[0].IsNumber() || (!info[1].IsNumber() && !info[1].IsNull() && !info[1].IsUndefined())) {
    Napi::TypeError::New(env, "Expected: errorCode (Number), coreHandle (Number or Null)").ThrowAsJavaScriptException();
    return env.Null();
  }
  ERRVAL errorCode = info[0].As<Napi::Number>().Int32Value();
  HCORE coreHandle = nullptr; 
  if (info[1].IsNumber()) {
      coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());
  }
  const char* errorDesc = BTICard_ErrDescStr(errorCode, coreHandle);
  if (errorDesc == nullptr) {
    return Napi::String::New(env, "Unknown error code or failed to get description.");
  }
  return Napi::String::New(env, errorDesc);
}

// N-API Wrapper for BTICard_BITInitiate
Napi::Value BitInitiateWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();
  if (info.Length() != 1 || !info[0].IsNumber()) {
    Napi::TypeError::New(env, "Number expected for hCard").ThrowAsJavaScriptException();
    return env.Null();
  }
  HCARD hCard = reinterpret_cast<HCARD>(info[0].As<Napi::Number>().Int64Value());
  ERRVAL result = BTICard_BITInitiate(hCard);
  return Napi::Number::New(env, result);
}

// --- Add New Wrappers Here ---

// N-API Wrapper for BTICard_CardReset
Napi::Value CardResetWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect one argument: core handle (Number)
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: core handle (Number)").ThrowAsJavaScriptException();
        return env.Null(); // Return null on error
    }

    // Extract core handle and cast
    HCORE coreHandle = reinterpret_cast<HCORE>(info[0].As<Napi::Number>().Int64Value());

    // Call the actual library function (returns VOID)
    BTICard_CardReset(coreHandle);

    // Since the function returns void, we don't have a status code to return directly.
    // We can return undefined or true to indicate the call was made.
    // Returning undefined is often clearer for void functions.
    return env.Undefined(); 
}

// N-API Wrapper for BTICard_CardGetInfo
Napi::Value CardGetInfoWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: infoType (Number), channelNum (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: infoType (Number), channelNum (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    USHORT infoType = (USHORT)info[0].As<Napi::Number>().Uint32Value();
    int channelNum = info[1].As<Napi::Number>().Int32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());

    // Call the actual library function
    ULONG resultValue = BTICard_CardGetInfo(infoType, channelNum, coreHandle);

    // This function likely doesn't return an ERRVAL directly, but the info value.
    // Error handling might rely on specific return values (e.g., 0 or -1 if info not found) 
    // or potentially setting a last error status retrievable elsewhere. Check manual.
    // For now, just returning the ULONG value.
    // Note: ULONG might exceed safe integer range for Napi::Number if it's 64-bit.
    // Consider Napi::BigInt if resultValue can be very large.
    return Napi::Number::New(env, resultValue); 
}

// N-API Wrapper for BTICard_CardStart
Napi::Value CardStartWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: core handle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    HCORE coreHandle = reinterpret_cast<HCORE>(info[0].As<Napi::Number>().Int64Value());
    ERRVAL result = BTICard_CardStart(coreHandle);
    return Napi::Number::New(env, result);
}

// N-API Wrapper for BTICard_CardStop
Napi::Value CardStopWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: core handle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    HCORE coreHandle = reinterpret_cast<HCORE>(info[0].As<Napi::Number>().Int64Value());
    BOOL result = BTICard_CardStop(coreHandle);
    return Napi::Boolean::New(env, result);
}

// N-API Wrapper for BTICard_EventLogConfig
Napi::Value EventLogConfigWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: ctrlflags (Number), count (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    USHORT ctrlflags = (USHORT)info[0].As<Napi::Number>().Uint32Value();
    USHORT count = (USHORT)info[1].As<Napi::Number>().Uint32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());
    ERRVAL result = BTICard_EventLogConfig(ctrlflags, count, coreHandle);
    return Napi::Number::New(env, result);
}

// N-API Wrapper for BTICard_EventLogRd
Napi::Value EventLogRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    HCORE coreHandle = reinterpret_cast<HCORE>(info[0].As<Napi::Number>().Int64Value());

    USHORT typeval = 0;
    ULONG infoval = 0;
    INT channel = -1; // Initialize channel

    ULONG logEntryAddr = BTICard_EventLogRd(&typeval, &infoval, &channel, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    if (logEntryAddr != 0) { // Assuming non-zero indicates an entry was read
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        resultObj.Set("type", Napi::Number::New(env, typeval));
        resultObj.Set("info", Napi::Number::New(env, infoval)); // ULONG may exceed safe int range
        resultObj.Set("channel", Napi::Number::New(env, channel));
        resultObj.Set("entryAddr", Napi::Number::New(env, logEntryAddr)); // ULONG
    } else {
        resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Or a specific 'empty' code if available
        resultObj.Set("type", env.Null());
        resultObj.Set("info", env.Null());
        resultObj.Set("channel", env.Null());
        resultObj.Set("entryAddr", env.Null());
    }
    return resultObj;
}

// N-API Wrapper for BTICard_EventLogStatus
Napi::Value EventLogStatusWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    HCORE coreHandle = reinterpret_cast<HCORE>(info[0].As<Napi::Number>().Int64Value());
    INT result = BTICard_EventLogStatus(coreHandle); // Returns INT status
    // Consider returning status and result separately like listStatus
     Napi::Object resultObj = Napi::Object::New(env);
     if (result >= 0) { // STAT_EMPTY, STAT_PARTIAL, etc.
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        resultObj.Set("logStatus", Napi::Number::New(env, result));
     } else {
         resultObj.Set("status", Napi::Number::New(env, result)); // Actual error code
         resultObj.Set("logStatus", env.Null());
     }
    return resultObj;
}

// N-API Wrapper for BTICard_Timer64Rd
Napi::Value Timer64RdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    HCORE coreHandle = reinterpret_cast<HCORE>(info[0].As<Napi::Number>().Int64Value());

    // Declare separate ULONG variables for high and low parts, as per BTI documentation
    ULONG timerValH = 0;
    ULONG timerValL = 0;

    // Call the BTI function with pointers to the ULONG variables
    ERRVAL result = BTICard_Timer64Rd(&timerValH, &timerValL, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    resultObj.Set("status", Napi::Number::New(env, result));
    if (result == ERR_NONE) {
        // Combine the high and low ULONG parts into a ULONGLONG
        ULONGLONG combinedValue = ((ULONGLONG)timerValH << 32) | timerValL;
        resultObj.Set("value", Napi::BigInt::New(env, combinedValue)); // Use BigInt for ULONGLONG
    } else {
        resultObj.Set("value", env.Null());
    }
    return resultObj;
}

// N-API Wrapper for BTICard_Timer64Wr
Napi::Value Timer64WrWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect two arguments: timerValue (BigInt), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsBigInt() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: timerValue (BigInt), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    bool lossless;
    ULONGLONG timerValue = info[0].As<Napi::BigInt>().Uint64Value(&lossless);
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    if (!lossless) {
        Napi::RangeError::New(env, "BigInt value could not be represented as uint64_t losslessly").ThrowAsJavaScriptException();
        return env.Null();
    }

    // *** Verify actual parameters from BTI header. Does it take one ULONGLONG or two ULONGs? ***
    // Assuming it takes one ULONGLONG for now:
    // BTICard_Timer64Wr(timerValue, coreHandle); // Example if it took one ULONGLONG
    // If it takes two ULONGs:
    ULONG timerValH = (ULONG)(timerValue >> 32);
    ULONG timerValL = (ULONG)(timerValue & 0xFFFFFFFF);
    BTICard_Timer64Wr(timerValH, timerValL, coreHandle); // HACK: Passing parts - NEEDS VERIFICATION

    // Function returns VOID
    return env.Undefined();
}

// N-API Wrapper for BTI429_ChConfig
Napi::Value ChConfigWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: configVal (Number), channelNum (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (Number), channelNum (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    // ULONG can be 32 or 64 bits depending on platform/compiler.
    // Use Uint32Value as config flags usually fit in 32 bits. Verify if ULONG is 64-bit in BTI headers.
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value(); 
    int channelNum = info[1].As<Napi::Number>().Int32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());

    // Call the actual library function
    ERRVAL result = BTI429_ChConfig(configVal, channelNum, coreHandle);

    // Return the status code
    return Napi::Number::New(env, result);
}

// N-API Wrapper for BTI429_ChStart
Napi::Value ChStartWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect two arguments: channelNum (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: channelNum (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    int channelNum = info[0].As<Napi::Number>().Int32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function (Returns BOOL)
    BOOL result = BTI429_ChStart(channelNum, coreHandle);

    // Return the BOOL result as a JavaScript boolean
    return Napi::Boolean::New(env, result);
}

// N-API Wrapper for BTI429_ChStop
Napi::Value ChStopWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect two arguments: channelNum (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: channelNum (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    int channelNum = info[0].As<Napi::Number>().Int32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function (Returns BOOL)
    BOOL result = BTI429_ChStop(channelNum, coreHandle);

    // Return the BOOL result as a JavaScript boolean
    return Napi::Boolean::New(env, result);
}

// N-API Wrapper for BTI429_ListXmtCreate
Napi::Value ListXmtCreateWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect four arguments: configVal, count, msgAddr, coreHandle
    if (info.Length() != 4 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (number), count (number), msgAddr (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    INT count = info[1].As<Napi::Number>().Int32Value();
    // Expecting MSGADDR (handle/address) passed as a number from JS
    MSGADDR msgAddr = info[2].As<Napi::Number>().Int64Value(); // Use Int64Value for safety if ULONG might exceed 32-bit range
    HCORE coreHandle = reinterpret_cast<HCORE>(info[3].As<Napi::Number>().Int64Value());

    // Call the actual library function
    LISTADDR listAddrResult = BTI429_ListXmtCreate(configVal, count, msgAddr, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    // LISTADDR is ULONG, check if non-zero indicates success (Check BTI docs)
    // Assuming non-zero means success for now.
    if (listAddrResult != 0) { 
        // Success: Return list address and status 0 (ERR_NONE assumed)
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE)); 
        // Return address as Number (LISTADDR is ULONG)
        resultObj.Set("listAddr", Napi::Number::New(env, listAddrResult)); 
    } else {
        // Failure: Return an error status and null address
        resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Generic failure for now
        resultObj.Set("listAddr", env.Null());
    }
    return resultObj;
}

// N-API Wrapper for BTI429_ListDataWr
Napi::Value ListDataWrWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: value (Number), listAddr (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: value (number), listAddr (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG value = info[0].As<Napi::Number>().Uint32Value(); // ARINC word is 32 bits
    // LISTADDR is ULONG, get as number
    LISTADDR listAddr = info[1].As<Napi::Number>().Int64Value(); // Use Int64Value for safety
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());

    // Call the actual library function (Returns BOOL)
    BOOL success = BTI429_ListDataWr(value, listAddr, coreHandle);

    // Return the status code (BOOL treated as ERR_NONE or ERR_FAIL for consistency)
    return Napi::Number::New(env, success ? ERR_NONE : ERR_FAIL); 
}

// N-API Wrapper for BTI429_ListDataBlkWr
Napi::Value ListDataBlkWrWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    if (info.Length() != 3 || !info[0].IsArray() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: dataArray (Array<number>), listAddr (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    Napi::Array jsArray = info[0].As<Napi::Array>();
    // LISTADDR is ULONG, get as number
    LISTADDR listAddr = info[1].As<Napi::Number>().Int64Value(); // Use Int64Value for safety
    HCORE hCore = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value()); // Use Int64 for handles

    uint32_t arrayLength = jsArray.Length();
    // Check if count exceeds USHORT max
     if (arrayLength > 65535) {
        Napi::RangeError::New(env, "Data array length exceeds maximum allowed (65535)").ThrowAsJavaScriptException();
        return env.Null();
    }
    // Function expects USHORT for count
    USHORT count = static_cast<USHORT>(arrayLength); 

    if (count == 0) {
        // Nothing to write, return success immediately
        return Napi::Number::New(env, ERR_NONE);
    }

    std::vector<ULONG> cppBuffer(count);
    for (uint32_t i = 0; i < count; ++i) {
        Napi::Value val = jsArray.Get(i);
        if (!val.IsNumber()) {
             Napi::TypeError::New(env, "Array element is not a number").ThrowAsJavaScriptException();
             return env.Null();
        }
        cppBuffer[i] = val.As<Napi::Number>().Uint32Value(); // Use Uint32 for ARINC words
    }

    // Call the actual library function (Returns BOOL, takes LPUSHORT count)
    // Pass the address of count (&count) as required by LPUSHORT
    BOOL success = BTI429_ListDataBlkWr(cppBuffer.data(), count, listAddr, hCore);

    // Return the result code (BOOL treated as ERR_NONE or ERR_FAIL for consistency)
    return Napi::Number::New(env, success ? ERR_NONE : ERR_FAIL); 
}

// N-API Wrapper for BTI429_ListRcvCreate
Napi::Value ListRcvCreateWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expecting configVal, count, msgAddr, coreHandle
    if (info.Length() != 4 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (number), count (number), msgAddr (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    INT count = info[1].As<Napi::Number>().Int32Value();
    // Expecting MSGADDR (handle/address) passed as a number from JS
    MSGADDR msgAddr = info[2].As<Napi::Number>().Int64Value(); // Use Int64Value for safety if ULONG might exceed 32-bit range
    HCORE hCore = reinterpret_cast<HCORE>(info[3].As<Napi::Number>().Int64Value());

    // Call the actual library function
    LISTADDR listAddrResult = BTI429_ListRcvCreate(configVal, count, msgAddr, hCore);

    Napi::Object resultObj = Napi::Object::New(env);
    // LISTADDR is ULONG, check if non-zero indicates success (Check BTI docs)
    // Assuming non-zero means success for now.
    if (listAddrResult != 0) { 
         resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
         // Return address as Number (LISTADDR is ULONG)
         resultObj.Set("listAddr", Napi::Number::New(env, listAddrResult)); 
    } else {
        // Need a way to get the actual error code if creation fails
         resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Generic failure for now
         resultObj.Set("listAddr", env.Null());
    }
    return resultObj;
}

// N-API Wrapper for BTI429_ListDataRd
Napi::Value ListDataRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect two arguments: listAddr (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    LISTADDR listAddr = info[0].As<Napi::Number>().Int64Value(); // Use Int64Value for safety
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    // Before calling read, check status - maybe handle empty synchronously?
    int listStatus = BTI429_ListStatus(listAddr, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);

    if (listStatus == STAT_EMPTY) {
        resultObj.Set("status", Napi::Number::New(env, ERR_UNDERFLOW)); // Specific code for empty
        resultObj.Set("value", env.Null());
    } else if (listStatus < 0) { // Error checking status
         resultObj.Set("status", Napi::Number::New(env, listStatus)); // Return BTI error
         resultObj.Set("value", env.Null());
    } else { // Status OK (Partial or Full)
        // Call the actual library function - Returns ULONG data
        ULONG dataWord = BTI429_ListDataRd(listAddr, coreHandle);
        
        // How to differentiate valid 0 data from an error? 
        // Assume success if status was ok before read. Could re-check status after.
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        resultObj.Set("value", Napi::Number::New(env, dataWord)); // Return ULONG as number
    }
    
    return resultObj;
}

// N-API Wrapper for BTI429_ListDataBlkRd
Napi::Value ListDataBlkRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (number), maxCount (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    LISTADDR listAddr = info[0].As<Napi::Number>().Int64Value(); // Use Int64Value for safety
    int maxCountJs = info[1].As<Napi::Number>().Int32Value();
    HCORE hCore = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());

    Napi::Object resultObj = Napi::Object::New(env);

    if (maxCountJs <= 0) {
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        resultObj.Set("dataArray", Napi::Array::New(env, 0));
        return resultObj;
    }
    
    // Check if maxCountJs exceeds USHORT max
    if (maxCountJs > 65535) {
        Napi::RangeError::New(env, "maxCount exceeds maximum allowed (65535)").ThrowAsJavaScriptException();
        return env.Null();
    }
    USHORT maxCount = static_cast<USHORT>(maxCountJs); // Cast to USHORT

    std::vector<ULONG> cppBuffer(maxCount);
    USHORT countActuallyRead = 0; // Variable to receive the count read

    // Call the actual library function (Returns BOOL, takes LPUSHORT count)
    BOOL success = BTI429_ListDataBlkRd(cppBuffer.data(), &countActuallyRead, listAddr, hCore);

    int resultCode = ERR_FAIL; // Default to fail

    if (success) {
        resultCode = ERR_NONE;
        Napi::Array dataArray = Napi::Array::New(env, countActuallyRead);
        for (USHORT i = 0; i < countActuallyRead; ++i) {
            dataArray.Set(i, Napi::Number::New(env, cppBuffer[i]));
        }
         resultObj.Set("dataArray", dataArray);
    } else {
         // Try ListStatus to get a more specific error
         int listStatus = BTI429_ListStatus(listAddr, hCore);
         if (listStatus < 0) { // BTI error code
             resultCode = listStatus;
             resultObj.Set("dataArray", env.Null());
         } else if (listStatus == STAT_EMPTY && countActuallyRead == 0) { // Check if list is empty AND 0 items were read
             resultCode = ERR_NONE; // Treat read 0 as success
             resultObj.Set("dataArray", Napi::Array::New(env, 0)); // Ensure empty array
         } else if (listStatus == STAT_EMPTY) { // List empty but read > 0 was attempted
              resultCode = ERR_UNDERFLOW; 
              resultObj.Set("dataArray", env.Null());
         } else { // Other non-success, non-empty, non-BTI-error situation?
             resultCode = ERR_FAIL; // Keep generic fail 
             resultObj.Set("dataArray", env.Null());
         }
    }

    resultObj.Set("status", Napi::Number::New(env, resultCode));
    return resultObj;
}

// N-API Wrapper for BTI429_ListStatus
Napi::Value ListStatusWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect two arguments: listAddr (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    LISTADDR listAddr = info[0].As<Napi::Number>().Int64Value(); // Use Int64Value for safety
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function - returns INT status code
    int listStatusResult = BTI429_ListStatus(listAddr, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    if (listStatusResult >= 0) { // Non-negative is a status (STAT_EMPTY, STAT_PARTIAL, STAT_FULL)
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE)); // Overall operation success
        resultObj.Set("listStatus", Napi::Number::New(env, listStatusResult));
    } else { // Negative is an error code
        resultObj.Set("status", Napi::Number::New(env, listStatusResult)); // Return BTI error code
        resultObj.Set("listStatus", env.Null()); // No valid status
    }
    
    return resultObj;
}

// N-API Wrapper for BTI429_FilterSet
Napi::Value FilterSetWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect five arguments: configVal, label, sdiMask, channelNum, coreHandle
    if (info.Length() != 5 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber() || !info[4].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (number), label (number), sdiMask (number), channelNum (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    INT labelVal = info[1].As<Napi::Number>().Int32Value(); // Label likely fits in INT
    INT sdiMask = info[2].As<Napi::Number>().Int32Value(); 
    INT channelNum = info[3].As<Napi::Number>().Int32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[4].As<Napi::Number>().Int64Value());

    // Call the actual library function - Returns ULONG (MSGADDR)
    // MSGADDR is likely a pointer or handle type
    MSGADDR filterAddrResult = BTI429_FilterSet(configVal, labelVal, sdiMask, channelNum, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
     // MSGADDR is ULONG, check if non-zero indicates success
    if (filterAddrResult != 0) { 
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        // Return address as Number (MSGADDR is ULONG)
        resultObj.Set("filterAddr", Napi::Number::New(env, filterAddrResult)); 
    } else {
         // Need a way to get the actual error code if creation fails
        resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Generic failure
        resultObj.Set("filterAddr", env.Null());
    }
    
    return resultObj;
}

// N-API Wrapper for BTI429_FilterDefault
Napi::Value FilterDefaultWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: configVal, channelNum, coreHandle
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (number), channelNum (number), coreHandle (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    INT channelNum = info[1].As<Napi::Number>().Int32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());

    // Call the actual library function - Returns ULONG (MSGADDR)
    MSGADDR filterAddrResult = BTI429_FilterDefault(configVal, channelNum, coreHandle);

     Napi::Object resultObj = Napi::Object::New(env);
     // MSGADDR is ULONG, check if non-zero indicates success
    if (filterAddrResult != 0) { 
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        // Return address as Number (MSGADDR is ULONG)
        resultObj.Set("filterAddr", Napi::Number::New(env, filterAddrResult)); 
    } else {
         // Need a way to get the actual error code if creation fails
        resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Generic failure
        resultObj.Set("filterAddr", env.Null());
    }
    
    return resultObj;
}

// N-API Wrapper for BTI429_MsgCreate
Napi::Value MsgCreateWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect two arguments: ctrlflags (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: ctrlflags (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    ULONG ctrlflags = info[0].As<Napi::Number>().Uint32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    MSGADDR msgAddrResult = BTI429_MsgCreate(ctrlflags, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    if (msgAddrResult != 0) { // Assuming non-zero MSGADDR indicates success
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        resultObj.Set("msgAddr", Napi::Number::New(env, msgAddrResult));
    } else {
        resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Generic failure
        resultObj.Set("msgAddr", env.Null());
    }
    return resultObj;
}

// N-API Wrapper for BTI429_MsgDataWr
Napi::Value MsgDataWrWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect three arguments: value (Number), msgAddr (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: value (Number), msgAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    ULONG value = info[0].As<Napi::Number>().Uint32Value();
    MSGADDR msgAddr = info[1].As<Napi::Number>().Int64Value(); // Assuming MSGADDR fits in Int64
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());

    BTI429_MsgDataWr(value, msgAddr, coreHandle);

    // Function returns VOID, return undefined or status code
    return env.Undefined(); // Or return Napi::Number::New(env, ERR_NONE);
}

// N-API Wrapper for BTI429_MsgDataRd
Napi::Value MsgDataRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect two arguments: msgAddr (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msgAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    MSGADDR msgAddr = info[0].As<Napi::Number>().Int64Value(); // Assuming MSGADDR fits in Int64
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function - Returns ULONG data
    // How to check for errors? Does it return 0 on error? Or is error implicit if msgAddr is invalid? Check manual.
    ULONG dataWord = BTI429_MsgDataRd(msgAddr, coreHandle);

    // Assume success if called, return the data word. Error handling might need refinement.
    Napi::Object resultObj = Napi::Object::New(env);
    resultObj.Set("status", Napi::Number::New(env, ERR_NONE)); // Assuming success
    resultObj.Set("value", Napi::Number::New(env, dataWord));
    return resultObj;
}

// N-API Wrapper for BTI429_FldGetLabel
Napi::Value FldGetLabelWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect one argument: msg (Number - ULONG)
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msg (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    ULONG msg = info[0].As<Napi::Number>().Uint32Value();
    USHORT label = BTI429_FldGetLabel(msg);
    return Napi::Number::New(env, label);
}

// N-API Wrapper for BTI429_FldGetSDI
Napi::Value FldGetSDIWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msg (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    ULONG msg = info[0].As<Napi::Number>().Uint32Value();
    USHORT sdi = BTI429_FldGetSDI(msg);
    return Napi::Number::New(env, sdi);
}

// N-API Wrapper for BTI429_FldGetData
Napi::Value FldGetDataWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msg (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    ULONG msg = info[0].As<Napi::Number>().Uint32Value();
    ULONG data = BTI429_FldGetData(msg);
    return Napi::Number::New(env, data); // Return ULONG data
}

// N-API Wrapper for BTI429_BCDGetData
Napi::Value BCDGetDataWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect three arguments: msg (Number), msb (Number), lsb (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msg (Number), msb (Number), lsb (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    ULONG msg = info[0].As<Napi::Number>().Uint32Value();
    USHORT msb = (USHORT)info[1].As<Napi::Number>().Uint32Value();
    USHORT lsb = (USHORT)info[2].As<Napi::Number>().Uint32Value();
    ULONG data = BTI429_BCDGetData(msg, msb, lsb);
    return Napi::Number::New(env, data); // Return ULONG data
}

// N-API Wrapper for BTI429_BNRGetData
Napi::Value BNRGetDataWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
     // Expect three arguments: msg (Number), msb (Number), lsb (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msg (Number), msb (Number), lsb (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    ULONG msg = info[0].As<Napi::Number>().Uint32Value();
    USHORT msb = (USHORT)info[1].As<Napi::Number>().Uint32Value();
    USHORT lsb = (USHORT)info[2].As<Napi::Number>().Uint32Value();
    ULONG data = BTI429_BNRGetData(msg, msb, lsb);
    return Napi::Number::New(env, data); // Return ULONG data
}

// Helper function to convert MSGFIELDS429 to Napi::Object
Napi::Object ConvertMsgFieldsToNapiObject(Napi::Env env, const MSGFIELDS429& fields) {
    Napi::Object obj = Napi::Object::New(env);
    obj.Set("msgopt", Napi::Number::New(env, fields.msgopt));
    obj.Set("msgact", Napi::Number::New(env, fields.msgact));
    obj.Set("msgdata", Napi::Number::New(env, fields.msgdata));
    obj.Set("listptr", Napi::Number::New(env, fields.listptr));
    obj.Set("timetag", Napi::Number::New(env, fields.timetag));
    obj.Set("hitcount", Napi::Number::New(env, fields.hitcount));
    obj.Set("maxtime", Napi::Number::New(env, fields.maxtime));
    obj.Set("elapsetime", Napi::Number::New(env, fields.elapsetime));
    obj.Set("mintime", Napi::Number::New(env, fields.mintime));
    // obj.Set("userptr", Napi::Number::New(env, fields.userptr)); // Usually not needed in JS
    obj.Set("timetagh", Napi::Number::New(env, fields.timetagh));
    obj.Set("decgap", Napi::Number::New(env, fields.decgap));
    obj.Set("paramflag", Napi::Number::New(env, fields.paramflag));
    return obj;
}

// N-API Wrapper for BTI429_MsgBlockRd
Napi::Value MsgBlockRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msgAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    MSGADDR msgAddr = info[0].As<Napi::Number>().Int64Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    MSGFIELDS429 msgFields = {0}; // Initialize structure

    // Call the actual library function - Returns MSGADDR
    MSGADDR resultAddr = BTI429_MsgBlockRd(&msgFields, msgAddr, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    // Check if resultAddr matches input msgAddr or is non-zero for success? Check manual.
    if (resultAddr != 0) { // Assuming non-zero return means success
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        resultObj.Set("fields", ConvertMsgFieldsToNapiObject(env, msgFields));
    } else {
        resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Generic failure
        resultObj.Set("fields", env.Null());
    }
    return resultObj;
}

// N-API Wrapper for BTI429_MsgCommRd
Napi::Value MsgCommRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
     if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msgAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    MSGADDR msgAddr = info[0].As<Napi::Number>().Int64Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    MSGFIELDS429 msgFields = {0}; // Initialize structure

    // Call the actual library function - Returns MSGADDR
    MSGADDR resultAddr = BTI429_MsgCommRd(&msgFields, msgAddr, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    // Check if resultAddr matches input msgAddr or is non-zero for success? Check manual.
    if (resultAddr != 0) { // Assuming non-zero return means success
        resultObj.Set("status", Napi::Number::New(env, ERR_NONE));
        resultObj.Set("fields", ConvertMsgFieldsToNapiObject(env, msgFields));
    } else {
        resultObj.Set("status", Napi::Number::New(env, ERR_FAIL)); // Generic failure
        resultObj.Set("fields", env.Null());
    }
    return resultObj;
}

// N-API Wrapper for BTI429_MsgIsAccessed
Napi::Value MsgIsAccessedWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: msgAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    MSGADDR msgAddr = info[0].As<Napi::Number>().Int64Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());

    BOOL result = BTI429_MsgIsAccessed(msgAddr, coreHandle);
    // How to detect errors? Assume BOOL return indicates access status only?
    Napi::Object resultObj = Napi::Object::New(env);
    resultObj.Set("status", Napi::Number::New(env, ERR_NONE)); // Assuming success
    resultObj.Set("accessed", Napi::Boolean::New(env, result));
    return resultObj;
}

// --- Add New Async Wrappers Here ---

// N-API Wrapper for ListDataRdAsync (uses ListDataRdWorker)
Napi::Value ListDataRdAsyncWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (number), coreHandle (number), timeoutMs (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    LISTADDR listAddr = info[0].As<Napi::Number>().Int64Value(); // Use Int64Value for safety
    HCORE coreHandle = reinterpret_cast<HCORE>(info[1].As<Napi::Number>().Int64Value());
    int timeoutMs = info[2].As<Napi::Number>().Int32Value();

    ListDataRdWorker* worker = new ListDataRdWorker(env, listAddr, coreHandle, timeoutMs);
    worker->Queue();
    return worker->GetPromise();
}

// N-API Wrapper for ListDataBlkRdAsync (uses ListDataBlkRdWorker)
Napi::Value ListDataBlkRdAsyncWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

     if (info.Length() != 4 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (number), maxCount (number), coreHandle (number), timeoutMs (number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    LISTADDR listAddr = info[0].As<Napi::Number>().Int64Value(); // Use Int64Value for safety
    int maxCountJs = info[1].As<Napi::Number>().Int32Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());
    int timeoutMs = info[3].As<Napi::Number>().Int32Value();

    // Basic validation for maxCount
    if (maxCountJs < 0) {
         Napi::RangeError::New(env, "maxCount cannot be negative").ThrowAsJavaScriptException();
        return env.Null();
    }
     if (maxCountJs > 65535) { // Also check against USHORT limit if applicable
        Napi::RangeError::New(env, "maxCount exceeds practical limit (65535)").ThrowAsJavaScriptException();
        return env.Null();
    }

    ListDataBlkRdWorker* worker = new ListDataBlkRdWorker(env, listAddr, maxCountJs, coreHandle, timeoutMs);
    worker->Queue();
    return worker->GetPromise();
}

// N-API Wrapper for BTICard_ExtDIOWr
Napi::Value ExtDIOWrWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect 3 arguments: dionum (Number), dioval (Boolean), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsBoolean() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: dionum (Number), dioval (Boolean), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }
    INT dionum = info[0].As<Napi::Number>().Int32Value();
    BOOL dioval = info[1].As<Napi::Boolean>().Value();
    HCORE coreHandle = reinterpret_cast<HCORE>(info[2].As<Napi::Number>().Int64Value());

    BTICard_ExtDIOWr(dionum, dioval, coreHandle);

    // Function returns VOID, indicate success
    return Napi::Number::New(env, ERR_NONE); // Or return env.Undefined();
}

// --- NEW Function to read all DIOs at once ---
Napi::Value GetAllDioStatesWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    // Expect 1 argument: coreHandle (BigInt)
    if (info.Length() != 1 || !info[0].IsBigInt()) { // Check for BigInt
        Napi::TypeError::New(env, "Expected: coreHandle (BigInt)").ThrowAsJavaScriptException();
        return env.Null();
    }

    bool lossless;
    uint64_t coreHandleValue = info[0].As<Napi::BigInt>().Uint64Value(&lossless);
    if (!lossless) {
        Napi::Error::New(env, "Invalid core handle value.").ThrowAsJavaScriptException();
        return env.Null();
    }
    HCORE coreHandle = reinterpret_cast<HCORE>(coreHandleValue);

    const INT dionumMapping[] = {1, 2, 3, 4, 9, 10, 11, 12};
    const int numDios = sizeof(dionumMapping) / sizeof(dionumMapping[0]);
    Napi::Array resultsArray = Napi::Array::New(env, numDios);

    for (int i = 0; i < numDios; ++i) {
        INT dionum = dionumMapping[i];
        Napi::Object dioResult = Napi::Object::New(env);
        dioResult.Set("index", Napi::Number::New(env, i));
        dioResult.Set("apiDionum", Napi::Number::New(env, dionum));

        // Call BTI function
        INT result = static_cast<INT>(BTICard_ExtDIORd(dionum, coreHandle));

        if (result < 0) { // Assuming negative means error based on previous logic
            dioResult.Set("status", Napi::Number::New(env, ERR_FAIL)); // Use a generic error code or map BTI errors
            // Potentially use BTICard_ErrDescStr here if ExtDIORd sets last error
            dioResult.Set("error", Napi::String::New(env, "Error reading DIO."));
            dioResult.Set("value", env.Null());
        } else {
            dioResult.Set("status", Napi::Number::New(env, ERR_NONE));
            dioResult.Set("value", Napi::Boolean::New(env, (result == 1))); // Assuming 1=ON, 0=OFF
            dioResult.Set("error", env.Null());
        }
        resultsArray.Set(i, dioResult);
    }

    return resultsArray;
}
// --- END NEW Function ---

// --- NEW ARINC Receive Functions ---

// Exported Function: InitializeHardware
Napi::Value InitializeHardwareWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    Napi::Object resultObj = Napi::Object::New(env);

    // Prevent re-initialization if already initialized
    if (hCardGlobal != nullptr || hCoreGlobal != nullptr) {
        resultObj.Set("success", Napi::Boolean::New(env, false));
        resultObj.Set("hCard", Napi::BigInt::New(env, reinterpret_cast<uint64_t>(hCardGlobal)));
        resultObj.Set("hCore", Napi::BigInt::New(env, reinterpret_cast<uint64_t>(hCoreGlobal)));
        resultObj.Set("message", Napi::String::New(env, "Hardware already initialized."));
        resultObj.Set("resultCode", Napi::Number::New(env, ERR_NONE)); // Or a custom code?
        return resultObj;
    }

    int cardNum = 0; // Use Card 0 as confirmed by user
    HCARD hCard = nullptr;
    ERRVAL result = BTICard_CardOpen(&hCard, cardNum);

    if (result != ERR_NONE || hCard == nullptr) {
        const char* errStr = BTICard_ErrDescStr(result, nullptr);
        resultObj.Set("success", Napi::Boolean::New(env, false));
        resultObj.Set("hCard", env.Null());
        resultObj.Set("hCore", env.Null());
        resultObj.Set("message", Napi::String::New(env, errStr ? errStr : "Failed to open card."));
        resultObj.Set("resultCode", Napi::Number::New(env, result));
        hCardGlobal = nullptr;
        hCoreGlobal = nullptr;
        return resultObj;
    }

    hCardGlobal = hCard;

    int coreNum = 0; // ARINC core
    HCORE hCore = nullptr;
    result = BTICard_CoreOpen(&hCore, coreNum, hCard);

    if (result != ERR_NONE || hCore == nullptr) {
        const char* errStr = BTICard_ErrDescStr(result, hCard);
        resultObj.Set("success", Napi::Boolean::New(env, false));
        resultObj.Set("hCard", Napi::BigInt::New(env, reinterpret_cast<uint64_t>(hCard)));
        resultObj.Set("hCore", env.Null());
        resultObj.Set("message", Napi::String::New(env, errStr ? errStr : "Failed to open core."));
        resultObj.Set("resultCode", Napi::Number::New(env, result));
        // Don't null hCardGlobal here, it might be needed for cleanup attempt
        hCoreGlobal = nullptr;
        return resultObj;
    }

    hCoreGlobal = hCore;

    // Optional Reset
    BTICard_CardReset(hCore);

    resultObj.Set("success", Napi::Boolean::New(env, true));
    resultObj.Set("hCard", Napi::BigInt::New(env, reinterpret_cast<uint64_t>(hCard)));
    resultObj.Set("hCore", Napi::BigInt::New(env, reinterpret_cast<uint64_t>(hCore)));
    resultObj.Set("message", Napi::String::New(env, "Hardware initialized successfully."));
    resultObj.Set("resultCode", Napi::Number::New(env, ERR_NONE));

    return resultObj;
}

// Exported Function: InitializeReceiver
Napi::Value InitializeReceiverWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 3 || !info[0].IsBigInt() || !info[1].IsFunction() || !info[2].IsFunction()) {
        Napi::TypeError::New(env, "Expected: hCore (BigInt), dataCallback (Function), errorCallback (Function)").ThrowAsJavaScriptException();
        return env.Null();
    }

    bool lossless;
    HCORE hCore = reinterpret_cast<HCORE>(info[0].As<Napi::BigInt>().Uint64Value(&lossless));
    if (!lossless || !hCore) {
        Napi::TypeError::New(env, "Invalid core handle provided.").ThrowAsJavaScriptException();
        return env.Null();
    }
     // Ensure global core handle matches if already set
    if (hCoreGlobal && hCoreGlobal != hCore) {
         Napi::Error::New(env, "Core handle mismatch with global state.").ThrowAsJavaScriptException();
        return env.Null();
    } else if (!hCoreGlobal) {
        hCoreGlobal = hCore; // Set if not already set
    }

     // Cleanup previous TSFNs if they exist
    if (tsfnDataUpdate) {
        // Check if acquire returns napi_ok before releasing, otherwise it might already be released/dead
        if (tsfnDataUpdate.Acquire() == napi_ok) {
            tsfnDataUpdate.Release();
        }
        tsfnDataUpdate = nullptr;
    }
    if (tsfnErrorUpdate) {
        if (tsfnErrorUpdate.Acquire() == napi_ok) {
             tsfnErrorUpdate.Release();
        }
        tsfnErrorUpdate = nullptr;
    }

    // Create new ThreadSafeFunctions with corrected argument count
    Napi::Function jsDataCallback = info[1].As<Napi::Function>();
    Napi::Function jsErrorCallback = info[2].As<Napi::Function>();

    // Define the finalizer lambda explicitly
    auto dataFinalizer = [](Napi::Env env, void* finalize_data) { // Simplified signature
        std::cout << "tsfnDataUpdate finalized." << std::endl;
    };

    tsfnDataUpdate = Napi::ThreadSafeFunction::New(
        env,
        jsDataCallback,
        "ARINC Data Update", // Resource Name
        0, // Max Queue Size (0 = unlimited)
        1, // Initial Thread Count
        dataFinalizer,
        (void*)nullptr // FinalizerDataType*
    );

    auto errorFinalizer = [](Napi::Env env, void* finalize_data) { // Simplified signature
        std::cout << "tsfnErrorUpdate finalized." << std::endl;
    };

    tsfnErrorUpdate = Napi::ThreadSafeFunction::New(
        env,
        jsErrorCallback,
        "ARINC Error Update", // Resource Name
        0, // Max Queue Size
        1, // Initial Thread Count
        errorFinalizer,
        (void*)nullptr // FinalizerDataType*
    );

    Napi::Object resultObj = Napi::Object::New(env);
    bool success = true;
    std::string errorMessage = "Receiver initialized successfully.";
    int lastErrorCode = ERR_NONE;

    // Configure Event Log first (Keep this)
    ERRVAL logConfigResult = BTICard_EventLogConfig(LOGCFG_ENABLE, 1024, hCore);
    if(logConfigResult != ERR_NONE) {
        // ... (Handle event log config error)
        goto end_init_receiver;
    }

    // Configure Channels & Lists
    for (int i = 0; i < ARINC_CHANNEL_COUNT; ++i) {
        // Config Channel
        ULONG chFlags = CHCFG429_AUTOSPEED | CHCFG429_LOGERR;
        ERRVAL chConfigResult = BTI429_ChConfig(chFlags, i, hCore);
        if (chConfigResult != ERR_NONE) {
            const char* errStr = BTICard_ErrDescStr(chConfigResult, hCore);
            errorMessage = "Failed to configure channel " + std::to_string(i) + ": " + (errStr ? errStr : "Unknown error");
            success = false;
            lastErrorCode = chConfigResult;
            break;
        }

        // Create Default Filter FIRST
        MSGADDR defaultMsgAddr = BTI429_FilterDefault(MSGCRT429_DEFAULT, i, hCore);
        if (defaultMsgAddr == 0) {
             errorMessage = "Failed to create default filter for channel " + std::to_string(i);
             std::cerr << "BTI429_FilterDefault failed for channel " << i << std::endl;
             success = false;
             lastErrorCode = ERR_FAIL; // Or get last error if possible
             break;
        }
        std::cout << "Created default filter for channel " << i << " with msg addr: " << defaultMsgAddr << std::endl;

        // Create Receive List, passing the message address from the default filter
        ULONG listFlags = LISTCRT429_FIFO; // Use FIFO mode
        LISTADDR listAddr = BTI429_ListRcvCreate(listFlags, 1024, defaultMsgAddr, hCore);
        if (listAddr == 0) {
            errorMessage = "Failed to create receive list for channel " + std::to_string(i) + " (linked to default filter)";
            std::cerr << "BTI429_ListRcvCreate failed for channel " << i << " with flags: " << listFlags << " msgAddr: " << defaultMsgAddr << std::endl;
            success = false;
            lastErrorCode = ERR_FAIL;
            break;
        }
        receiveListAddrs[i] = listAddr; // Still store list address for reading
        std::cout << "Created receive list for channel " << i << " linked to msg " << defaultMsgAddr << " with list address: " << listAddr << std::endl;
    }

end_init_receiver:
    resultObj.Set("success", Napi::Boolean::New(env, success));
    resultObj.Set("message", Napi::String::New(env, errorMessage));
    resultObj.Set("lastErrorCode", Napi::Number::New(env, lastErrorCode));

    // If initialization failed, release TSFNs immediately
    if (!success) {
         if (tsfnDataUpdate) { tsfnDataUpdate.Release(); tsfnDataUpdate = nullptr; }
         if (tsfnErrorUpdate) { tsfnErrorUpdate.Release(); tsfnErrorUpdate = nullptr; }
    }

    return resultObj;
}

// Exported Function: StartMonitoring
Napi::Value StartMonitoringWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsBigInt()) {
        Napi::TypeError::New(env, "Expected: hCore (BigInt)").ThrowAsJavaScriptException();
        return env.Null();
    }

    bool lossless;
    HCORE hCore = reinterpret_cast<HCORE>(info[0].As<Napi::BigInt>().Uint64Value(&lossless));
    if (!lossless || !hCore || hCore != hCoreGlobal) { // Ensure handle matches global
        Napi::TypeError::New(env, "Invalid or mismatched core handle provided.").ThrowAsJavaScriptException();
        return env.Null();
    }

    Napi::Object resultObj = Napi::Object::New(env);

    if (monitoringActive.load()) {
        resultObj.Set("success", Napi::Boolean::New(env, false));
        resultObj.Set("message", Napi::String::New(env, "Monitoring is already active."));
        return resultObj;
    }
    if (!tsfnDataUpdate || !tsfnErrorUpdate) {
         resultObj.Set("success", Napi::Boolean::New(env, false));
        resultObj.Set("message", Napi::String::New(env, "Callbacks not initialized. Call InitializeReceiver first."));
        return resultObj;
    }

    ERRVAL startResult = BTICard_CardStart(hCore);
    if (startResult != ERR_NONE) {
        const char* errStr = BTICard_ErrDescStr(startResult, hCore);
        resultObj.Set("success", Napi::Boolean::New(env, false));
        resultObj.Set("message", Napi::String::New(env, std::string("Failed to start card: ") + (errStr ? errStr : "Unknown error")));
        return resultObj;
    }

    monitoringActive.store(true);
    try {
        // Ensure previous thread is joined if somehow still exists
        if (monitorThread.joinable()) {
             std::cout << "Warning: Previous monitor thread was still joinable. Joining now." << std::endl;
            monitorThread.join();
        }
        monitorThread = std::thread(MonitorLoop);
    } catch (const std::exception& e) {
         monitoringActive.store(false);
         BTICard_CardStop(hCore); // Attempt to stop card if thread creation failed
         const char* errStr = BTICard_ErrDescStr(startResult, hCore);
         resultObj.Set("success", Napi::Boolean::New(env, false));
         resultObj.Set("message", Napi::String::New(env, std::string("Failed to start monitoring thread: ") + e.what()));
         return resultObj;
    }

    resultObj.Set("success", Napi::Boolean::New(env, true));
    resultObj.Set("message", Napi::String::New(env, "ARINC monitoring started."));
    return resultObj;
}

// Exported Function: StopMonitoring
Napi::Value StopMonitoringWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    if (info.Length() != 1 || !info[0].IsBigInt()) {
        Napi::TypeError::New(env, "Expected: hCore (BigInt)").ThrowAsJavaScriptException();
        return env.Null();
    }

    bool lossless;
    HCORE hCore = reinterpret_cast<HCORE>(info[0].As<Napi::BigInt>().Uint64Value(&lossless));
     if (!lossless || !hCore || hCore != hCoreGlobal) { // Check against global
        Napi::TypeError::New(env, "Invalid or mismatched core handle provided.").ThrowAsJavaScriptException();
        return env.Null();
    }

    Napi::Object resultObj = Napi::Object::New(env);

    if (!monitoringActive.load()) {
        resultObj.Set("success", Napi::Boolean::New(env, true)); // Already stopped
        resultObj.Set("message", Napi::String::New(env, "Monitoring was not active."));
        // Ensure TSFNs are released if somehow they weren't
         if (tsfnDataUpdate) { tsfnDataUpdate.Abort(); tsfnDataUpdate.Release(); tsfnDataUpdate = nullptr; }
         if (tsfnErrorUpdate) { tsfnErrorUpdate.Abort(); tsfnErrorUpdate.Release(); tsfnErrorUpdate = nullptr; }
        return resultObj;
    }

    std::cout << "StopMonitoring called. Setting flag false." << std::endl;
    monitoringActive.store(false);

    // Abort TSFNs to unblock any pending calls immediately
    if (tsfnDataUpdate) {
        tsfnDataUpdate.Abort(); // Abort any queued calls
    }
    if (tsfnErrorUpdate) {
        tsfnErrorUpdate.Abort();
    }

    if (monitorThread.joinable()) {
        std::cout << "Joining monitor thread..." << std::endl;
        try {
             monitorThread.join();
             std::cout << "Monitor thread joined." << std::endl;
        } catch(const std::system_error& e) {
            std::cerr << "Error joining monitor thread: " << e.what() << " (" << e.code() << ")" << std::endl;
            // Proceed with stopping the card anyway
        }
    } else {
         std::cout << "Monitor thread was not joinable." << std::endl;
    }

    // Stop the card
     std::cout << "Stopping BTI Card..." << std::endl;
    BTICard_CardStop(hCore);
     std::cout << "BTI Card stopped." << std::endl;

    // Release ThreadSafeFunctions - crucial to prevent leaks/crashes
    if (tsfnDataUpdate) {
        std::cout << "Releasing tsfnDataUpdate..." << std::endl;
        tsfnDataUpdate.Release();
        tsfnDataUpdate = nullptr;
    }
    if (tsfnErrorUpdate) {
         std::cout << "Releasing tsfnErrorUpdate..." << std::endl;
        tsfnErrorUpdate.Release();
        tsfnErrorUpdate = nullptr;
    }

    resultObj.Set("success", Napi::Boolean::New(env, true));
    resultObj.Set("message", Napi::String::New(env, "ARINC monitoring stopped."));
    return resultObj;
}

// Exported Function: CleanupHardware
Napi::Value CleanupHardwareWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();
    Napi::Object resultObj = Napi::Object::New(env);

    // Ensure monitoring is stopped first
    if (monitoringActive.load() && hCoreGlobal) {
         std::cout << "CleanupHardware: Stopping active monitoring first..." << std::endl;
         // Need to recreate CallbackInfo or pass null if no args needed
         // This is tricky. It's better if stop is called explicitly before cleanup.
         // Forcing a stop here without proper JS context can be problematic.
         // Let's just set the flag and rely on card close.
         monitoringActive.store(false);
         if (monitorThread.joinable()) monitorThread.join(); // Try to join if possible
         BTICard_CardStop(hCoreGlobal);
          if (tsfnDataUpdate) { tsfnDataUpdate.Abort(); tsfnDataUpdate.Release(); tsfnDataUpdate = nullptr; }
         if (tsfnErrorUpdate) { tsfnErrorUpdate.Abort(); tsfnErrorUpdate.Release(); tsfnErrorUpdate = nullptr; }
    }

    if (hCardGlobal) {
        std::cout << "CleanupHardware: Closing card..." << std::endl;
        ERRVAL closeResult = BTICard_CardClose(hCardGlobal);
        if (closeResult == ERR_NONE) {
             resultObj.Set("success", Napi::Boolean::New(env, true));
             resultObj.Set("message", Napi::String::New(env, "Hardware resources released."));
        } else {
            const char* errStr = BTICard_ErrDescStr(closeResult, hCardGlobal);
            resultObj.Set("success", Napi::Boolean::New(env, false));
            resultObj.Set("message", Napi::String::New(env, std::string("Error closing card: ") + (errStr ? errStr : "Unknown error")));
        }
        hCardGlobal = nullptr; // Clear global handles
        hCoreGlobal = nullptr;
    } else {
         resultObj.Set("success", Napi::Boolean::New(env, true)); // No card was open
         resultObj.Set("message", Napi::String::New(env, "No hardware card was open."));
    }

    return resultObj;
}

// --- ARINC Monitoring Thread Loop ---
void MonitorLoop() {
    if (!hCoreGlobal) {
        std::cerr << "MonitorLoop started with null hCoreGlobal!" << std::endl;
        return;
    }

    HCORE hCore = hCoreGlobal; // Use the global core handle
    const int MAX_READ_COUNT = 512; // How many words to read per channel check
    std::vector<ULONG> readBuffer(MAX_READ_COUNT);

    std::cout << "ARINC Monitor Thread Started." << std::endl;

    while (monitoringActive.load()) {
        // Check if TSFNs are still valid before proceeding
        if (!tsfnDataUpdate || !tsfnErrorUpdate) {
             std::cerr << "MonitorLoop: TSFN became invalid. Exiting loop." << std::endl;
             monitoringActive.store(false);
             break;
        }

        bool dataProcessedInCycle = false;
        auto updatesBatch = new std::vector<ArincUpdateData>(); // Allocate on heap for TSFN

        // 1. Poll Event Log (Optional but good for responsiveness)
        USHORT eventType = 0;
        ULONG eventInfo = 0;
        INT eventChannel = -1;
        ULONG logEntryAddr = BTICard_EventLogRd(&eventType, &eventInfo, &eventChannel, hCore);

        if (logEntryAddr != 0) {
            dataProcessedInCycle = true; // Consider event log read as activity
           // std::cout << "Event Logged: Type=" << eventType << ", Info=" << eventInfo << ", Chan=" << eventChannel << std::endl;
            if (eventType == EVENTTYPE_429LIST) { // List full/empty
                std::cout << "ARINC List event on channel " << eventChannel << " (Info: " << eventInfo << " -> " << (eventInfo == 0 ? "Empty?" : "Full?") << ")" << std::endl;
                // Could potentially report this as a warning/info via tsfnErrorUpdate
                // auto* errorData = new ArincErrorData{eventChannel, ERR_INFO, std::string("List Buffer Event: ") + (eventInfo == 0 ? "Empty/Underrun" : "Full/Overflow") };
                // tsfnErrorUpdate.BlockingCall(errorData, CallJsErrorUpdate);
            }
            else if (eventType == EVENTTYPE_429ERR) { // Decoder error
                 auto* errorData = new ArincErrorData{eventChannel, ERR_FAIL, "ARINC Decoder Error (See message activity)" };
                 tsfnErrorUpdate.BlockingCall(errorData, CallJsErrorUpdate);
            }
            // Add more event handling here if needed
        }

        // 2. Periodically Check Receive Lists
        for (int channel = 0; channel < ARINC_CHANNEL_COUNT; ++channel) { // Uses the new constant
            if (!monitoringActive.load()) break; // Check flag again inside loop

            LISTADDR listAddr = receiveListAddrs[channel];
            if (listAddr == 0) continue; // Skip if list wasn't created

            int listStatus = BTI429_ListStatus(listAddr, hCore);

            if (listStatus < 0) {
                 // Error checking list status
                 const char* errStr = BTICard_ErrDescStr(listStatus, hCore);
                 auto* errorData = new ArincErrorData{channel, listStatus, std::string("Error checking list status: ") + (errStr ? errStr : "Unknown error")};
                 if (tsfnErrorUpdate) tsfnErrorUpdate.BlockingCall(errorData, CallJsErrorUpdate);
                 continue; // Skip this channel on error
            }

            if (listStatus == STAT_PARTIAL || listStatus == STAT_FULL) {
                dataProcessedInCycle = true;
                USHORT countActuallyRead = 0; // Initialize to 0 before passing address
                // std::cout << "Ch " << channel << " Status: " << listStatus << ". Reading up to " << MAX_READ_COUNT << std::endl;
                BOOL success = BTI429_ListDataBlkRd(readBuffer.data(), &countActuallyRead, listAddr, hCore);
                 // std::cout << "Ch " << channel << " Read attempt done. Success: " << success << ", Count: " << countActuallyRead << std::endl;

                if (success && countActuallyRead > 0) {
                    //std::cout << "Read " << countActuallyRead << " words from Ch " << channel << std::endl;
                    for (USHORT i = 0; i < countActuallyRead; ++i) {
                        ULONG word = readBuffer[i];
                        int label = BTI429_FldGetLabel(word); // Label is bits 0-7
                        auto now = std::chrono::steady_clock::now();

                        // Update map
                        auto& lastWord = latestWords[channel][label];
                        auto& lastTime = lastUpdateTimes[channel][label];

                        // Store/update data and add to batch for JS update
                        lastWord = word;
                        lastTime = now;
                        updatesBatch->push_back({channel, label, word, steady_clock_to_epoch_ms(now)});
                    }
                } else if (!success) {
                    // Handle read failure - check status again?
                    int postReadStatus = BTI429_ListStatus(listAddr, hCore);
                    const char* errStr = BTICard_ErrDescStr(postReadStatus, hCore);
                    std::string errMsg = "Error reading data block (ListDataBlkRd failed).";
                    if(postReadStatus < 0) {
                        errMsg += " BTI Error: " + (errStr ? std::string(errStr) : "Unknown");
                    } else {
                        errMsg += " Post-read status: " + std::to_string(postReadStatus);
                    }
                     auto* errorData = new ArincErrorData{channel, (postReadStatus < 0 ? postReadStatus : ERR_FAIL), errMsg};
                     if (tsfnErrorUpdate) tsfnErrorUpdate.BlockingCall(errorData, CallJsErrorUpdate);
                } else if (countActuallyRead == 0 && listStatus == STAT_FULL) {
                     // This case might indicate an issue, log or report it
                     // std::cerr << "Warning: Ch " << channel << " status FULL but read 0 words." << std::endl;
                     // auto* errorData = new ArincErrorData{channel, ERR_INFO, "List status FULL but read 0 words."};
                     // if (tsfnErrorUpdate) tsfnErrorUpdate.BlockingCall(errorData, CallJsErrorUpdate);
                }
            }
        }
        if (!monitoringActive.load()) break; // Check flag again after loop

        // 3. Send Batch Update if data was found/processed
        if (!updatesBatch->empty()) {
            if (tsfnDataUpdate) {
               napi_status status = tsfnDataUpdate.BlockingCall(updatesBatch, CallJsDataUpdate);
                if (status != napi_ok && status != napi_closing) { // Ignore error if stopping
                    std::cerr << "Failed to call tsfnDataUpdate! Status: " << status << std::endl;
                    delete updatesBatch; // Clean up if call failed
                } else if (status == napi_closing) {
                     std::cout << "tsfnDataUpdate closing, discarding batch." << std::endl;
                      delete updatesBatch;
                } // On napi_ok, updatesBatch is deleted inside CallJsDataUpdate
            } else {
                std::cerr << "tsfnDataUpdate is null, discarding batch." << std::endl;
                delete updatesBatch; // Clean up if TSFN is null
            }
        } else {
             delete updatesBatch; // Clean up if empty
        }

        // 4. Sleep
        // Only sleep if no data was processed to stay responsive
        if (!dataProcessedInCycle) {
            std::this_thread::sleep_for(std::chrono::milliseconds(10)); // Polling interval when idle
        } else {
             std::this_thread::sleep_for(std::chrono::milliseconds(1)); // Shorter sleep if busy
        }
    }

    std::cout << "ARINC Monitor Thread Exiting." << std::endl;
}

// --- Initializer function for the addon module ---
Napi::Object Init(Napi::Env env, Napi::Object exports) {
  // Export the original wrapped functions
  exports.Set(Napi::String::New(env, "cardOpen"), Napi::Function::New(env, CardOpenWrapped));
  exports.Set(Napi::String::New(env, "coreOpen"), Napi::Function::New(env, CoreOpenWrapped));
  exports.Set(Napi::String::New(env, "cardTest"), Napi::Function::New(env, CardTestWrapped));
  exports.Set(Napi::String::New(env, "cardClose"), Napi::Function::New(env, CardCloseWrapped));
  exports.Set(Napi::String::New(env, "bitInitiate"), Napi::Function::New(env, BitInitiateWrapped));
  exports.Set(Napi::String::New(env, "getErrorDescription"), Napi::Function::New(env, GetErrorDescriptionWrapped));
  exports.Set(Napi::String::New(env, "cardReset"), Napi::Function::New(env, CardResetWrapped));
  exports.Set(Napi::String::New(env, "cardGetInfo"), Napi::Function::New(env, CardGetInfoWrapped));
  exports.Set(Napi::String::New(env, "cardStart"), Napi::Function::New(env, CardStartWrapped));
  exports.Set(Napi::String::New(env, "cardStop"), Napi::Function::New(env, CardStopWrapped));
  exports.Set(Napi::String::New(env, "eventLogConfig"), Napi::Function::New(env, EventLogConfigWrapped));
  exports.Set(Napi::String::New(env, "eventLogRd"), Napi::Function::New(env, EventLogRdWrapped));
  exports.Set(Napi::String::New(env, "eventLogStatus"), Napi::Function::New(env, EventLogStatusWrapped));
  exports.Set(Napi::String::New(env, "timer64Rd"), Napi::Function::New(env, Timer64RdWrapped));
  exports.Set(Napi::String::New(env, "timer64Wr"), Napi::Function::New(env, Timer64WrWrapped));
  exports.Set(Napi::String::New(env, "chConfig"), Napi::Function::New(env, ChConfigWrapped));
  exports.Set(Napi::String::New(env, "chStart"), Napi::Function::New(env, ChStartWrapped));
  exports.Set(Napi::String::New(env, "chStop"), Napi::Function::New(env, ChStopWrapped));
  exports.Set(Napi::String::New(env, "listXmtCreate"), Napi::Function::New(env, ListXmtCreateWrapped));
  exports.Set(Napi::String::New(env, "listDataWr"), Napi::Function::New(env, ListDataWrWrapped));
  exports.Set(Napi::String::New(env, "listDataBlkWr"), Napi::Function::New(env, ListDataBlkWrWrapped));
  exports.Set(Napi::String::New(env, "listRcvCreate"), Napi::Function::New(env, ListRcvCreateWrapped));
  exports.Set(Napi::String::New(env, "listDataRd"), Napi::Function::New(env, ListDataRdWrapped));
  exports.Set(Napi::String::New(env, "listDataBlkRd"), Napi::Function::New(env, ListDataBlkRdWrapped));
  exports.Set(Napi::String::New(env, "listStatus"), Napi::Function::New(env, ListStatusWrapped));
  exports.Set(Napi::String::New(env, "filterSet"), Napi::Function::New(env, FilterSetWrapped));
  exports.Set(Napi::String::New(env, "filterDefault"), Napi::Function::New(env, FilterDefaultWrapped));
  exports.Set(Napi::String::New(env, "listDataRdAsync"), Napi::Function::New(env, ListDataRdAsyncWrapped));
  exports.Set(Napi::String::New(env, "listDataBlkRdAsync"), Napi::Function::New(env, ListDataBlkRdAsyncWrapped));
  exports.Set(Napi::String::New(env, "msgCreate"), Napi::Function::New(env, MsgCreateWrapped));
  exports.Set(Napi::String::New(env, "msgDataWr"), Napi::Function::New(env, MsgDataWrWrapped));
  exports.Set(Napi::String::New(env, "msgDataRd"), Napi::Function::New(env, MsgDataRdWrapped));
  exports.Set(Napi::String::New(env, "fldGetLabel"), Napi::Function::New(env, FldGetLabelWrapped));
  exports.Set(Napi::String::New(env, "fldGetSDI"), Napi::Function::New(env, FldGetSDIWrapped));
  exports.Set(Napi::String::New(env, "fldGetData"), Napi::Function::New(env, FldGetDataWrapped));
  exports.Set(Napi::String::New(env, "bcdGetData"), Napi::Function::New(env, BCDGetDataWrapped));
  exports.Set(Napi::String::New(env, "bnrGetData"), Napi::Function::New(env, BNRGetDataWrapped));
  exports.Set(Napi::String::New(env, "msgBlockRd"), Napi::Function::New(env, MsgBlockRdWrapped));
  exports.Set(Napi::String::New(env, "msgCommRd"), Napi::Function::New(env, MsgCommRdWrapped));
  exports.Set(Napi::String::New(env, "msgIsAccessed"), Napi::Function::New(env, MsgIsAccessedWrapped));
  exports.Set(Napi::String::New(env, "extDIOWr"), Napi::Function::New(env, ExtDIOWrWrapped));
  exports.Set(Napi::String::New(env, "getAllDioStates"), Napi::Function::New(env, GetAllDioStatesWrapped));

  // --- Export NEW ARINC receiver control functions ---
  exports.Set(Napi::String::New(env, "initializeHardware"), Napi::Function::New(env, InitializeHardwareWrapped));
  exports.Set(Napi::String::New(env, "initializeReceiver"), Napi::Function::New(env, InitializeReceiverWrapped));
  exports.Set(Napi::String::New(env, "startMonitoring"), Napi::Function::New(env, StartMonitoringWrapped));
  exports.Set(Napi::String::New(env, "stopMonitoring"), Napi::Function::New(env, StopMonitoringWrapped));
  exports.Set(Napi::String::New(env, "cleanupHardware"), Napi::Function::New(env, CleanupHardwareWrapped));
  // --- END Export ---

  return exports;
}

NODE_API_MODULE(bti_addon, Init) 