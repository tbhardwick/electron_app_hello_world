#include <napi.h>
#include "BTICARD.H" // Include the vendor header (Path relative to include_dirs)

// Define placeholder types if they are not standard
// These might be defined in BTICARD.H or another header it includes.
// Adjust these if necessary based on the actual header definitions.
typedef int ERRVAL;
// typedef int HCARD; // Removed: HCARD is defined in BTICARD.H, likely as void*

// N-API Wrapper for BTICard_BITInitiate
Napi::Value BitInitiateWrapped(const Napi::CallbackInfo& info) {
  Napi::Env env = info.Env();

  // Check if the correct number of arguments was passed
  if (info.Length() != 1 || !info[0].IsNumber()) {
    Napi::TypeError::New(env, "Number expected for hCard").ThrowAsJavaScriptException();
    return env.Null(); // Return null on error
  }

  // Extract the hCard argument from JavaScript and cast it to the HCARD type
  // Assuming the number passed from JS represents the opaque handle.
  HCARD hCard = (HCARD)(info[0].As<Napi::Number>().Int32Value());

  // Call the actual library function
  ERRVAL result = BTICard_BITInitiate(hCard);

  // Return the result to JavaScript
  return Napi::Number::New(env, result);
}

// Initializer function for the addon module
Napi::Object Init(Napi::Env env, Napi::Object exports) {
  // Export the wrapped function under the name "bitInitiate"
  exports.Set(Napi::String::New(env, "bitInitiate"),
              Napi::Function::New(env, BitInitiateWrapped));
  return exports;
}

NODE_API_MODULE(bti_addon, Init) // Register the addon 