#include <napi.h>
#include "BTICARD.H" // Include the vendor header (Path relative to include_dirs)

// Define placeholder types if they are not standard
// These might be defined in BTICARD.H or another header it includes.
// Adjust these if necessary based on the actual header definitions.
typedef int ERRVAL;
typedef void* HCARD; // Assuming HCARD is void*
typedef void** LPHCARD; // Pointer to HCARD pointer
typedef void* HCORE; // Assuming HCORE is void*
typedef void** LPHCORE; // Pointer to HCORE pointer
typedef unsigned short USHORT;

// --- N-API Wrapper for BTICard_CardOpen ---
Napi::Value CardOpenWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();

  // Expect one argument: card number
  if (info.Length() != 1 || !info[0].IsNumber()) {
    Napi::TypeError::New(env, "Number expected for card number").ThrowAsJavaScriptException();
    return env.Null();
  }

  int cardNum = info[0].As<Napi::Number>().Int32Value();
  HCARD cardHandle = nullptr; // Variable to hold the resulting handle

  // Call the actual library function, passing the address of cardHandle
  ERRVAL result = BTICard_CardOpen(&cardHandle, cardNum);

  // Create a result object to send back to JavaScript
  Napi::Object resultObj = Napi::Object::New(env);
  resultObj.Set("resultCode", Napi::Number::New(env, result));

  if (result == 0 && cardHandle != nullptr) {
    // Success: Return the handle as a number (assuming it can be represented safely)
    // Note: Casting pointers to numbers can be platform-dependent.
    // Consider using BigInt if handles might exceed 32/53 bits, but number is often fine for opaque handles.
    resultObj.Set("success", Napi::Boolean::New(env, true));
    resultObj.Set("handle", Napi::Number::New(env, reinterpret_cast<uintptr_t>(cardHandle)));
    resultObj.Set("message", Napi::String::New(env, "Card opened successfully."));
  } else {
    // Failure: Return error code and message
    resultObj.Set("success", Napi::Boolean::New(env, false));
    resultObj.Set("handle", env.Null()); // No valid handle on failure
    // Could add BTICard_ErrDescStr call here later if needed
    resultObj.Set("message", Napi::String::New(env, "Failed to open card.")); 
  }

  return resultObj;
}
// --- End CardOpen Wrapper ---

// --- N-API Wrapper for BTICard_CoreOpen ---
Napi::Value CoreOpenWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();

  // Expect two arguments: core number, card handle (as number)
  if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
    Napi::TypeError::New(env, "Expected: core number (Number), card handle (Number)").ThrowAsJavaScriptException();
    return env.Null();
  }

  int coreNum = info[0].As<Napi::Number>().Int32Value();
  // Use Int64Value to handle potential 64-bit pointer addresses passed as numbers
  HCARD cardHandle = (HCARD)(info[1].As<Napi::Number>().Int64Value());
  HCORE coreHandle = nullptr; // Variable to hold the resulting core handle

  // Call the actual library function, passing the address of coreHandle
  ERRVAL result = BTICard_CoreOpen(&coreHandle, coreNum, cardHandle);

  // Create a result object
  Napi::Object resultObj = Napi::Object::New(env);
  resultObj.Set("resultCode", Napi::Number::New(env, result));

  if (result == 0 && coreHandle != nullptr) {
    resultObj.Set("success", Napi::Boolean::New(env, true));
    resultObj.Set("handle", Napi::Number::New(env, reinterpret_cast<uintptr_t>(coreHandle)));
    resultObj.Set("message", Napi::String::New(env, "Core opened successfully."));
  } else {
    resultObj.Set("success", Napi::Boolean::New(env, false));
    resultObj.Set("handle", env.Null());
    resultObj.Set("message", Napi::String::New(env, "Failed to open core."));
  }

  return resultObj;
}
// --- End CoreOpen Wrapper ---

// --- N-API Wrapper for BTICard_CardTest ---
Napi::Value CardTestWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();

  // Expect two arguments: test level (Number), core handle (Number)
  if (info.Length() != 2 || !info[0].IsNumber() || !info[1].IsNumber()) {
    Napi::TypeError::New(env, "Expected: test level (Number), core handle (Number)").ThrowAsJavaScriptException();
    return env.Null();
  }

  USHORT testLevel = (USHORT)info[0].As<Napi::Number>().Uint32Value();
  HCORE coreHandle = (HCORE)(info[1].As<Napi::Number>().Int64Value());

  // Call the actual library function
  ERRVAL result = BTICard_CardTest(testLevel, coreHandle);

  // Return the result code directly
  return Napi::Number::New(env, result);
}
// --- End CardTest Wrapper ---

// --- N-API Wrapper for BTICard_CardClose ---
Napi::Value CardCloseWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();

  // Expect one argument: card handle (Number)
  if (info.Length() != 1 || !info[0].IsNumber()) {
    Napi::TypeError::New(env, "Expected: card handle (Number)").ThrowAsJavaScriptException();
    return env.Null();
  }

  HCARD cardHandle = (HCARD)(info[0].As<Napi::Number>().Int64Value());

  // Call the actual library function
  ERRVAL result = BTICard_CardClose(cardHandle);

  // Return the result code directly
  return Napi::Number::New(env, result);
}
// --- End CardClose Wrapper ---

// N-API Wrapper for BTICard_BITInitiate
Napi::Value BitInitiateWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();

  // Check if the correct number of arguments was passed
  if (info.Length() != 1 || !info[0].IsNumber()) {
    Napi::TypeError::New(env, "Number expected for hCard").ThrowAsJavaScriptException();
    return env.Null(); // Return null on error
  }

  // Extract the hCard argument from JavaScript and cast it to the HCARD type
  // Use Int64Value() to handle potential 64-bit pointer addresses passed as numbers
  HCARD hCard = (HCARD)(info[0].As<Napi::Number>().Int64Value());

  // Call the actual library function
  ERRVAL result = BTICard_BITInitiate(hCard);

  // Return the result to JavaScript
  return Napi::Number::New(env, result);
}

// Initializer function for the addon module
Napi::Object Init(Napi::Env env, Napi::Object exports) {
  // Export the wrapped functions
  exports.Set(Napi::String::New(env, "cardOpen"),
              Napi::Function::New(env, CardOpenWrapped));
  exports.Set(Napi::String::New(env, "coreOpen"),
              Napi::Function::New(env, CoreOpenWrapped));
  exports.Set(Napi::String::New(env, "cardTest"),
              Napi::Function::New(env, CardTestWrapped));
  exports.Set(Napi::String::New(env, "cardClose"),
              Napi::Function::New(env, CardCloseWrapped));
  exports.Set(Napi::String::New(env, "bitInitiate"),
              Napi::Function::New(env, BitInitiateWrapped));
  return exports;
}

NODE_API_MODULE(bti_addon, Init) // Register the addon 