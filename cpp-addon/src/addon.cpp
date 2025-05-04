#include "BTICARD.H" // Include the vendor header (Path relative to include_dirs)
#include "BTI429.H" // Added BTI429 Header - Verify Name!
#include "bti_constants.h" // Added constants header

// Then include standard and N-API headers
#include <napi.h>
#include <windows.h>
#include <string>
#include <iostream>
#include <vector>       // Added for vector usage
#include <thread>       // Added for std::this_thread
#include <chrono>       // Added for std::chrono

// Define placeholder types if they are not standard
// These should be defined in BTICARD.H or another header it includes.
// Verify these definitions against your actual header files.
#ifndef BTI_TYPES_DEFINED // Add include guard if BTICARD.H doesn't define these reliably
#define BTI_TYPES_DEFINED
typedef int ERRVAL;
typedef void* HCARD; 
typedef void** LPHCARD;
typedef void* HCORE;
typedef void** LPHCORE;
typedef unsigned short USHORT;
typedef unsigned long ULONG;
#endif

// Declare the BTI functions we'll use (ensure these match BTICARD.H)
extern "C" {
    ERRVAL BTICard_CardOpen(LPHCARD lphCard, int CardNum);
    ERRVAL BTICard_CoreOpen(LPHCORE lphCore, int CoreNum, HCARD hCard);
    ERRVAL BTICard_CardTest(USHORT wTestLevel, HCORE hCore);
    ERRVAL BTICard_CardClose(HCARD hCard);
    ERRVAL BTICard_BITInitiate(HCARD hCard);
    const char* BTICard_ErrDescStr(ERRVAL errval, HCORE hCore);
    // --- Add declarations for newly implemented functions --- 
    VOID BTICard_CardReset(HCORE hCore); // Added based on manual search
    ULONG BTICard_CardGetInfo(USHORT infotype, int channum, HCORE hCore); // Added - Verify Signature!

    // --- BTI429 Functions --- 
    ERRVAL BTI429_ChConfig(ULONG configval, int channum, HCORE hCore); // Added - Verify Signature!
    ERRVAL BTI429_ChStart(int channum, HCORE hCore); // Added - Verify Signature!
    ERRVAL BTI429_ChStop(int channum, HCORE hCore); // Added - Verify Signature!
    ULONG BTI429_ListXmtCreate(ULONG listconfigval, int count, int channelNum, HCORE hCore); // Added - Verify Signature & Params!
    ERRVAL BTI429_ListDataWr(ULONG value, ULONG listaddr, HCORE hCore); // Added - Verify Signature!
    ERRVAL BTI429_ListDataBlkWr(ULONG* listbuf, int count, ULONG listaddr, HCORE hCore); // Added - Verify Signature!
    ULONG BTI429_ListRcvCreate(ULONG listconfigval, int count, int channelNum, HCORE hCore); // Added - Verify Signature & Params!
    ULONG BTI429_ListDataRd(ULONG listaddr, HCORE hCore); // Added - Verify Signature & Return Value on Empty!
    ERRVAL BTI429_ListDataBlkRd(ULONG* listbuf, int count, ULONG listaddr, HCORE hCore); // Added - Verify Signature & Params!
    int BTI429_ListStatus(ULONG listaddr, HCORE hCore); // Added - Verify Signature & Return Type!
    ULONG BTI429_FilterSet(ULONG configval, int labelval, int sdimask, int channum, HCORE hCore); // Added - Verify Signature & Return!
    ULONG BTI429_FilterDefault(ULONG configval, int channum, HCORE hCore); // Added - Verify Signature & Return!
    // Add other BTI429 declarations here as needed...
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
            return;
        }

        while (true) {
            int currentListStatus = BTI429_ListStatus(listAddr_, hCore_); 

            if (currentListStatus < 0) { 
                SetError("Error checking list status.");
                status_ = currentListStatus;
                return;
            }

            // Use defined constants
            if (currentListStatus == STAT_PARTIAL || currentListStatus == STAT_FULL) { 
                // ** CRITICAL: Verify how ListDataBlkRd reports count read! **
                status_ = BTI429_ListDataBlkRd(readBuffer_.data(), maxCount_, listAddr_, hCore_); 

                if (status_ == ERR_NONE) { 
                    // ** PLACEHOLDER: Need actual count read ** 
                    actualCountRead_ = maxCount_; // *** REPLACE WITH ACTUAL LOGIC ***
                } else if (status_ == ERR_UNDERFLOW) { 
                    actualCountRead_ = 0;
                    status_ = ERR_NONE; // Treat underflow as success (read 0)
                } else { // Other BTI error
                    SetError("Error reading data block."); 
                    actualCountRead_ = 0;
                    // Keep the specific BTI error in status_
                }
                return; 
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
  HCARD cardHandle = (HCARD)(info[1].As<Napi::Number>().Int64Value());
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
  HCORE coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());
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
  HCARD cardHandle = (HCARD)(info[0].As<Napi::Number>().Int64Value());
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
      coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());
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
  HCARD hCard = (HCARD)(info[0].As<Napi::Number>().Int64Value());
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
    HCORE coreHandle = (HCORE)(info[0].As<Napi::Number>().Int64Value());

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
    HCORE coreHandle = (HCORE)(info[2].As<Napi::Number>().Int64Value());

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
    HCORE coreHandle = (HCORE)(info[2].As<Napi::Number>().Int64Value());

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
    HCORE coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function
    ERRVAL result = BTI429_ChStart(channelNum, coreHandle);

    // Return the status code
    return Napi::Number::New(env, result);
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
    HCORE coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function
    ERRVAL result = BTI429_ChStop(channelNum, coreHandle);

    // Return the status code
    return Napi::Number::New(env, result);
}

// N-API Wrapper for BTI429_ListXmtCreate
Napi::Value ListXmtCreateWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect four arguments: configVal (Number), count (Number), channelNum (Number), coreHandle (Number)
    if (info.Length() != 4 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (Number), count (Number), channelNum (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    int count = info[1].As<Napi::Number>().Int32Value();
    int channelNum = info[2].As<Napi::Number>().Int32Value();
    HCORE coreHandle = (HCORE)(info[3].As<Napi::Number>().Int64Value());

    // Call the actual library function - Assuming it returns list address (0 on failure)
    ULONG listAddr = BTI429_ListXmtCreate(configVal, count, channelNum, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    if (listAddr != 0) {
        // Success: Return list address and status 0 (ERR_NONE assumed)
        resultObj.Set("status", Napi::Number::New(env, 0)); // Assuming ERR_NONE = 0
        // Return address as Number - check if ULONG can exceed safe integer range!
        resultObj.Set("listAddr", Napi::Number::New(env, listAddr)); 
    } else {
        // Failure: Return an error status and null address
        // Need a way to get the actual error code if listAddr is 0.
        // BTICard_ErrDescStr might work, or there might be a BTI429 error func.
        // Using a generic error code for now.
        resultObj.Set("status", Napi::Number::New(env, -1)); // Generic error
        resultObj.Set("listAddr", env.Null());
    }

    return resultObj;
}

// N-API Wrapper for BTI429_ListDataWr
Napi::Value ListDataWrWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: value (Number), listAddr (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: value (Number), listAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG value = info[0].As<Napi::Number>().Uint32Value(); // ARINC word is 32 bits
    ULONG listAddr = info[1].As<Napi::Number>().Uint32Value(); // Assuming list address fits in 32 bits
    HCORE coreHandle = (HCORE)(info[2].As<Napi::Number>().Int64Value());

    // Call the actual library function
    ERRVAL result = BTI429_ListDataWr(value, listAddr, coreHandle);

    // Return the status code
    return Napi::Number::New(env, result);
}

// N-API Wrapper for BTI429_ListDataBlkWr
Napi::Value ListDataBlkWrWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: dataArray (Array), listAddr (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsArray() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: dataArray (Array), listAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments
    Napi::Array dataArray = info[0].As<Napi::Array>();
    ULONG listAddr = info[1].As<Napi::Number>().Uint32Value(); 
    HCORE coreHandle = (HCORE)(info[2].As<Napi::Number>().Int64Value());

    uint32_t count = dataArray.Length();
    if (count == 0) {
        return Napi::Number::New(env, 0); // Nothing to write, return success (ERR_NONE assumed)
    }

    // Create a C++ buffer from the JavaScript array
    std::vector<ULONG> cppBuffer; 
    cppBuffer.reserve(count);
    for (uint32_t i = 0; i < count; ++i) {
        Napi::Value val = dataArray.Get(i);
        if (!val.IsNumber()) {
            // Handle error: array contains non-number
             Napi::TypeError::New(env, "Array must contain only numbers").ThrowAsJavaScriptException();
             return env.Null();
        }
        // Assuming ARINC words fit in 32 bits
        cppBuffer.push_back(val.As<Napi::Number>().Uint32Value());
    }

    // Call the actual library function
    ERRVAL result = BTI429_ListDataBlkWr(cppBuffer.data(), cppBuffer.size(), listAddr, coreHandle);

    // Return the status code
    return Napi::Number::New(env, result);
}

// N-API Wrapper for BTI429_ListRcvCreate
Napi::Value ListRcvCreateWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect four arguments: configVal (Number), count (Number), channelNum (Number), coreHandle (Number)
    if (info.Length() != 4 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (Number), count (Number), channelNum (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    int count = info[1].As<Napi::Number>().Int32Value();
    int channelNum = info[2].As<Napi::Number>().Int32Value();
    HCORE coreHandle = (HCORE)(info[3].As<Napi::Number>().Int64Value());

    // Call the actual library function - Assuming it returns list address (0 on failure)
    ULONG listAddr = BTI429_ListRcvCreate(configVal, count, channelNum, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    if (listAddr != 0) {
        // Success: Return list address and status 0 (ERR_NONE assumed)
        resultObj.Set("status", Napi::Number::New(env, 0)); // Assuming ERR_NONE = 0
        // Return address as Number - check if ULONG can exceed safe integer range!
        resultObj.Set("listAddr", Napi::Number::New(env, listAddr)); 
    } else {
        // Failure: Return an error status and null address
        // Using a generic error code for now. Check manual for specific error retrieval.
        resultObj.Set("status", Napi::Number::New(env, -1)); // Generic error
        resultObj.Set("listAddr", env.Null());
    }

    return resultObj;
}

// N-API Wrapper for BTI429_ListDataRd
Napi::Value ListDataRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect two arguments: listAddr (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG listAddr = info[0].As<Napi::Number>().Uint32Value(); 
    HCORE coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function - Assuming it returns data word, or 0/error on empty
    ULONG dataWord = BTI429_ListDataRd(listAddr, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    
    // How to check for errors/empty? Needs verification.
    // Option 1: Assume 0 means empty/error (Less likely for data read)
    // Option 2: Assume it returns ERR_UNDERFLOW (-108?) on empty.
    // Option 3: Assume ListStatus must be checked first.
    
    // Assuming for now that a non-zero return is valid data, 0 is empty/error.
    // ** THIS IS LIKELY INCORRECT - VERIFY WITH MANUAL **
    if (dataWord != 0) { // Placeholder check - Verify actual empty/error condition! 
        resultObj.Set("status", Napi::Number::New(env, 0)); // Assuming success
        resultObj.Set("value", Napi::Number::New(env, dataWord));
    } else {
        // Assuming empty or error if 0 is returned.
        // Should ideally check ListStatus first or check for specific error return.
        resultObj.Set("status", Napi::Number::New(env, -108)); // Guessing ERR_UNDERFLOW
        resultObj.Set("value", env.Null());
    }

    return resultObj;
}

// N-API Wrapper for BTI429_ListDataBlkRd
Napi::Value ListDataBlkRdWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: listAddr (Number), maxCount (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (Number), maxCount (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments
    ULONG listAddr = info[0].As<Napi::Number>().Uint32Value(); 
    int maxCount = info[1].As<Napi::Number>().Int32Value();
    HCORE coreHandle = (HCORE)(info[2].As<Napi::Number>().Int64Value());

    if (maxCount <= 0) {
        // Return empty array with success status if maxCount is zero or negative
        Napi::Object resultObj = Napi::Object::New(env);
        resultObj.Set("status", Napi::Number::New(env, 0)); // Assuming ERR_NONE
        resultObj.Set("dataArray", Napi::Array::New(env, 0));
        return resultObj;
    }

    // Allocate a buffer to receive the data
    std::vector<ULONG> cppBuffer(maxCount);

    // Call the actual library function - Assuming it returns ERRVAL and fills the buffer.
    // Does 'count' need to be passed by reference? Does it return the number read?
    // Needs verification!
    ERRVAL result = BTI429_ListDataBlkRd(cppBuffer.data(), maxCount, listAddr, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    resultObj.Set("status", Napi::Number::New(env, result));

    if (result == 0) { // Assuming ERR_NONE means success, potentially with 0 words read
        // How many words were *actually* read?
        // If the function modified 'maxCount' or returned the count, we need that value.
        // Assuming here (incorrectly?) that if status is 0, all requested words (up to maxCount)
        // available were read and placed in the buffer. 
        // We might need ListStatus first to know how many are available.
        // ** VERIFY HOW TO GET ACTUAL READ COUNT **

        // For now, assume we need to check ListStatus separately or the function 
        // somehow signals the count read. Let's just copy potentially maxCount items.
        // This will likely contain garbage if fewer words were read.
        int actualCountRead = maxCount; // Placeholder - ** REPLACE WITH ACTUAL COUNT **

        Napi::Array dataArray = Napi::Array::New(env, actualCountRead);
        for (int i = 0; i < actualCountRead; ++i) {
            // ULONG might need BigInt if it's 64-bit and values are large
            dataArray.Set(i, Napi::Number::New(env, cppBuffer[i])); 
        }
        resultObj.Set("dataArray", dataArray);
    } else if (result == -108) { // Guessing ERR_UNDERFLOW (-108 means buffer was empty)
        resultObj.Set("dataArray", Napi::Array::New(env, 0)); // Return empty array
    } else {
        // Other error
        resultObj.Set("dataArray", env.Null());
    }

    return resultObj;
}

// N-API Wrapper for BTI429_ListStatus
Napi::Value ListStatusWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect two arguments: listAddr (Number), coreHandle (Number)
    if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG listAddr = info[0].As<Napi::Number>().Uint32Value(); 
    HCORE coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());

    // Call the actual library function - Assuming it returns the list status directly
    int listStatus = BTI429_ListStatus(listAddr, coreHandle);

    // It might return an ERRVAL on error, or just the status codes.
    // Assuming it returns status codes directly (e.g., 0, 1, 2 for STAT_EMPTY, etc.)
    // Need to verify if negative values indicate errors.
    Napi::Object resultObj = Napi::Object::New(env);
    if (listStatus >= 0) { // Placeholder check for non-error status
         resultObj.Set("status", Napi::Number::New(env, 0)); // Overall success
         resultObj.Set("listStatus", Napi::Number::New(env, listStatus));
    } else {
         resultObj.Set("status", Napi::Number::New(env, listStatus)); // Return the error code
         resultObj.Set("listStatus", env.Null());
    }

    return resultObj;
}

// N-API Wrapper for BTI429_FilterSet
Napi::Value FilterSetWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect five arguments: configVal, label, sdiMask, channelNum, coreHandle (all Numbers)
    if (info.Length() != 5 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber() || !info[4].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal, label, sdiMask, channelNum, coreHandle (all Numbers)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    int labelVal = info[1].As<Napi::Number>().Int32Value();
    int sdiMask = info[2].As<Napi::Number>().Int32Value();
    int channelNum = info[3].As<Napi::Number>().Int32Value();
    HCORE coreHandle = (HCORE)(info[4].As<Napi::Number>().Int64Value());

    // Call the actual library function - Assuming it returns filter address (0 on failure?)
    ULONG filterAddr = BTI429_FilterSet(configVal, labelVal, sdiMask, channelNum, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    // Check manual: Does 0 indicate failure, or is it a valid address?
    // Does it return ERRVAL on failure?
    if (filterAddr != 0) { // Placeholder check for success - VERIFY!
        resultObj.Set("status", Napi::Number::New(env, 0)); // Assuming ERR_NONE
        // Check if ULONG address needs BigInt
        resultObj.Set("filterAddr", Napi::Number::New(env, filterAddr)); 
    } else {
        // Failure: Return an error status and null address
        // Using a generic error code for now. Check manual for specific error retrieval.
        resultObj.Set("status", Napi::Number::New(env, -1)); // Generic error
        resultObj.Set("filterAddr", env.Null());
    }

    return resultObj;
}

// N-API Wrapper for BTI429_FilterDefault
Napi::Value FilterDefaultWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: configVal (Number), channelNum (Number), coreHandle (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: configVal (Number), channelNum (Number), coreHandle (Number)").ThrowAsJavaScriptException();
        return env.Null();
    }

    // Extract arguments and cast
    ULONG configVal = info[0].As<Napi::Number>().Uint32Value();
    int channelNum = info[1].As<Napi::Number>().Int32Value();
    HCORE coreHandle = (HCORE)(info[2].As<Napi::Number>().Int64Value());

    // Call the actual library function - Assuming it returns filter address (0 on failure?)
    ULONG filterAddr = BTI429_FilterDefault(configVal, channelNum, coreHandle);

    Napi::Object resultObj = Napi::Object::New(env);
    // Check manual: Does 0 indicate failure, or is it a valid address?
    // Does it return ERRVAL on failure?
    if (filterAddr != 0) { // Placeholder check for success - VERIFY!
        resultObj.Set("status", Napi::Number::New(env, 0)); // Assuming ERR_NONE
        // Check if ULONG address needs BigInt
        resultObj.Set("filterAddr", Napi::Number::New(env, filterAddr)); 
    } else {
        // Failure: Return an error status and null address
        // Using a generic error code for now. Check manual for specific error retrieval.
        resultObj.Set("status", Napi::Number::New(env, -1)); // Generic error
        resultObj.Set("filterAddr", env.Null());
    }

    return resultObj;
}

// --- Add New Async Wrappers Here ---

// Async N-API wrapper for ListDataRd
Napi::Value ListDataRdAsyncWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect three arguments: listAddr (Number), coreHandle (Number), timeoutMs (Number)
    if (info.Length() != 3 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (Number), coreHandle (Number), timeoutMs (Number)").ThrowAsJavaScriptException();
        // Returning promise that will be rejected, though TypeError is already thrown
        Napi::Promise::Deferred deferred = Napi::Promise::Deferred::New(env);
        deferred.Reject(Napi::String::New(env, "Invalid arguments"));
        return deferred.Promise();
    }

    ULONG listAddr = info[0].As<Napi::Number>().Uint32Value();
    HCORE coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());
    int timeoutMs = info[2].As<Napi::Number>().Int32Value();

    ListDataRdWorker* worker = new ListDataRdWorker(env, listAddr, coreHandle, timeoutMs);
    worker->Queue();
    return worker->GetPromise();
}

// Async N-API wrapper for ListDataBlkRd
Napi::Value ListDataBlkRdAsyncWrapped(const Napi::CallbackInfo& info) {
    Napi::Env env = info.Env();

    // Expect four arguments: listAddr (Number), maxCount (Number), coreHandle (Number), timeoutMs (Number)
    if (info.Length() != 4 || !info[0].IsNumber() || !info[1].IsNumber() || !info[2].IsNumber() || !info[3].IsNumber()) {
        Napi::TypeError::New(env, "Expected: listAddr (Number), maxCount(Number), coreHandle (Number), timeoutMs (Number)").ThrowAsJavaScriptException();
        Napi::Promise::Deferred deferred = Napi::Promise::Deferred::New(env);
        deferred.Reject(Napi::String::New(env, "Invalid arguments"));
        return deferred.Promise();
    }

    ULONG listAddr = info[0].As<Napi::Number>().Uint32Value();
    int maxCount = info[1].As<Napi::Number>().Int32Value();
    HCORE coreHandle = (HCORE)(info[2].As<Napi::Number>().Int64Value());
    int timeoutMs = info[3].As<Napi::Number>().Int32Value();

    ListDataBlkRdWorker* worker = new ListDataBlkRdWorker(env, listAddr, maxCount, coreHandle, timeoutMs);
    worker->Queue();
    return worker->GetPromise();
}

// Initializer function for the addon module
Napi::Object Init(Napi::Env env, Napi::Object exports) {
  // Export the wrapped functions using static linking
  exports.Set(Napi::String::New(env, "cardOpen"), Napi::Function::New(env, CardOpenWrapped));
  exports.Set(Napi::String::New(env, "coreOpen"), Napi::Function::New(env, CoreOpenWrapped));
  exports.Set(Napi::String::New(env, "cardTest"), Napi::Function::New(env, CardTestWrapped));
  // Corrected: Use CardCloseWrapped for 'cardClose' export
  exports.Set(Napi::String::New(env, "cardClose"), Napi::Function::New(env, CardCloseWrapped)); 
  exports.Set(Napi::String::New(env, "bitInitiate"), Napi::Function::New(env, BitInitiateWrapped));
  exports.Set(Napi::String::New(env, "getErrorDescription"), Napi::Function::New(env, GetErrorDescriptionWrapped));
  // --- Export new functions ---
  exports.Set(Napi::String::New(env, "cardReset"), Napi::Function::New(env, CardResetWrapped));
  exports.Set(Napi::String::New(env, "cardGetInfo"), Napi::Function::New(env, CardGetInfoWrapped));
  exports.Set(Napi::String::New(env, "chConfig"), Napi::Function::New(env, ChConfigWrapped)); // Exported BTI429_ChConfig
  exports.Set(Napi::String::New(env, "chStart"), Napi::Function::New(env, ChStartWrapped)); // Exported BTI429_ChStart
  exports.Set(Napi::String::New(env, "chStop"), Napi::Function::New(env, ChStopWrapped)); // Exported BTI429_ChStop
  exports.Set(Napi::String::New(env, "listXmtCreate"), Napi::Function::New(env, ListXmtCreateWrapped)); // Exported BTI429_ListXmtCreate
  exports.Set(Napi::String::New(env, "listDataWr"), Napi::Function::New(env, ListDataWrWrapped)); // Exported BTI429_ListDataWr
  exports.Set(Napi::String::New(env, "listDataBlkWr"), Napi::Function::New(env, ListDataBlkWrWrapped)); // Exported BTI429_ListDataBlkWr
  exports.Set(Napi::String::New(env, "listRcvCreate"), Napi::Function::New(env, ListRcvCreateWrapped)); // Exported BTI429_ListRcvCreate
  exports.Set(Napi::String::New(env, "listDataRd"), Napi::Function::New(env, ListDataRdWrapped)); // Exported BTI429_ListDataRd
  exports.Set(Napi::String::New(env, "listDataBlkRd"), Napi::Function::New(env, ListDataBlkRdWrapped)); // Exported BTI429_ListDataBlkRd
  exports.Set(Napi::String::New(env, "listStatus"), Napi::Function::New(env, ListStatusWrapped)); // Exported BTI429_ListStatus
  exports.Set(Napi::String::New(env, "filterSet"), Napi::Function::New(env, FilterSetWrapped)); // Exported BTI429_FilterSet
  exports.Set(Napi::String::New(env, "filterDefault"), Napi::Function::New(env, FilterDefaultWrapped)); // Exported BTI429_FilterDefault
  exports.Set(Napi::String::New(env, "listDataRdAsync"), Napi::Function::New(env, ListDataRdAsyncWrapped)); // Exported Async ListDataRd
  exports.Set(Napi::String::New(env, "listDataBlkRdAsync"), Napi::Function::New(env, ListDataBlkRdAsyncWrapped)); // Exported Async ListDataBlkRd

  return exports;
}

NODE_API_MODULE(btiaddon, Init) 