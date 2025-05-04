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
    BOOL BTI429_ChStart(int channum, HCORE hCore); // Corrected Return Type
    BOOL BTI429_ChStop(int channum, HCORE hCore); // Corrected Return Type
    LISTADDR BTI429_ListXmtCreate(ULONG listconfigval, INT count, MSGADDR msgaddr, HCORE hCore); 
    BOOL BTI429_ListDataWr(ULONG value, LISTADDR listaddr, HCORE hCore); 
    BOOL BTI429_ListDataBlkWr(LPULONG listbuf, USHORT count, LISTADDR listaddr, HCORE hCore); 
    LISTADDR BTI429_ListRcvCreate(ULONG listconfigval, INT count, MSGADDR msgaddr, HCORE hCore); 
    ULONG BTI429_ListDataRd(LISTADDR listaddr, HCORE hCore); // Added - Verify Signature & Return Value on Empty!
    BOOL BTI429_ListDataBlkRd(LPULONG listbuf, LPUSHORT count, LISTADDR listaddr, HCORE hCore); 
    int BTI429_ListStatus(LISTADDR listaddr, HCORE hCore); // Added - Verify Signature & Return Type!
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
    BOOL success = BTI429_ListDataBlkWr(cppBuffer.data(), &count, listAddr, hCore);

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

NODE_API_MODULE(bti_addon, Init) 