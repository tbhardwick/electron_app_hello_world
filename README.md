# Electron UA2430 Test Application

An Electron application demonstrating communication with a UA2430 device via its vendor-provided C++ API, using a Node.js native addon.

## Description

This application serves as a testbed and example for interacting with BTI UA2430 hardware from a modern desktop application built with Electron. It utilizes a C++ native addon to bridge the gap between JavaScript and the vendor's C++ library, providing a robust and maintainable solution.

## Architecture

- **Electron Shell:** Provides the main application window and user interface (HTML/CSS/JS).
- **JavaScript (Renderer/Main):** Application logic written in JavaScript (`renderer.js`, `main.js`).
- **Node.js Native Addon (`bti_addon`):** A C++ module that acts as a bridge between JavaScript and the vendor's C++ library.
    - Located in the `cpp-addon/` directory.
    - Wraps functions from the vendor's `BTICARD.H` / `BTI429.H` headers using N-API.
    - Links against the vendor's static libraries (`.LIB` files).
    - The build process copies the required vendor runtime libraries (`.DLL` files) alongside the compiled addon.
- **Vendor Library:** The pre-compiled C++ libraries (`.DLL`, `.LIB`) and headers (`.H`) provided by the hardware vendor (BTI). These are located in `cpp-addon/vendor/`.

JavaScript code calls functions exported by the `bti_addon` (e.g., `btiAddon.bitInitiate(handle)`), which in turn call the underlying functions in the vendor's DLLs.

## Features

- Demonstrates loading and calling a C++ native addon from Electron.
- Provides examples of wrapping various vendor C++ API functions:
    - `BTICard_CardOpen`
    - `BTICard_CoreOpen`
    - `BTICard_CardTest`
    - `BTICard_CardClose`
    - `BTICard_BITInitiate`
    - `BTICard_ErrDescStr` (for descriptive error messages)
- Establishes a build process using `node-gyp` for the native addon.

## Available Addon Functions

This section details the functions currently wrapped in the C++ addon and exposed to JavaScript via the `btiAddon` object (loaded in `main.js`).

*   **`cardOpen(cardNumber: number)`**
    *   **Description:** Attempts to open a connection to the specified BTI card.
    *   **Arguments:**
        *   `cardNumber`: The integer index of the card to open (e.g., 0).
    *   **Returns:** `Object`
        *   `success: boolean`: True if the card was opened successfully.
        *   `handle: number | null`: The opaque card handle (as a number) if successful, otherwise null.
        *   `message: string`: Status message.
        *   `resultCode: number`: The raw result code from the underlying `BTICard_CardOpen` function.

*   **`coreOpen(coreNumber: number, cardHandle: number)`**
    *   **Description:** Attempts to open a specific core on an already opened card.
    *   **Arguments:**
        *   `coreNumber`: The integer index of the core to open (e.g., 0).
        *   `cardHandle`: The handle obtained from a successful `cardOpen` call.
    *   **Returns:** `Object`
        *   `success: boolean`: True if the core was opened successfully.
        *   `handle: number | null`: The opaque core handle (as a number) if successful, otherwise null.
        *   `message: string`: Status message.
        *   `resultCode: number`: The raw result code from the underlying `BTICard_CoreOpen` function.

*   **`cardTest(testLevel: number, coreHandle: number)`**
    *   **Description:** Runs a built-in self-test on the specified core.
    *   **Arguments:**
        *   `testLevel`: The integer code for the test level to run (e.g., 1 for Memory Interface Test).
        *   `coreHandle`: The handle obtained from a successful `coreOpen` call.
    *   **Returns:** `number` - The raw result code from the underlying `BTICard_CardTest` function (0 typically indicates success).

*   **`cardClose(cardHandle: number)`**
    *   **Description:** Closes the connection to the specified card. This implicitly closes any open cores on that card.
    *   **Arguments:**
        *   `cardHandle`: The handle obtained from a successful `cardOpen` call.
    *   **Returns:** `number` - The raw result code from the underlying `BTICard_CardClose` function (0 typically indicates success).

*   **`bitInitiate(cardHandle: number)`**
    *   **Description:** Initiates the Built-In Test (BIT) for the specified card.
    *   **Arguments:**
        *   `cardHandle`: The handle obtained from a successful `cardOpen` call.
    *   **Returns:** `number` - The raw result code from the underlying `BTICard_BITInitiate` function.

*   **`getErrorDescription(errorCode: number, coreHandle: number | null)`**
    *   **Description:** Retrieves a descriptive string for a given BTI error code.
    *   **Arguments:**
        *   `errorCode`: The numerical error code returned by another BTI function.
        *   `coreHandle`: The core handle associated with the error, if applicable (can be `null` if the error occurred before a core was opened or if the error is not core-specific).
    *   **Returns:** `string` - The error description, or a generic message if the code is unknown.

## Prerequisites

Before you begin, ensure you have the following installed:
- [Node.js](https://nodejs.org/) (v18 or higher recommended)
- npm (comes with Node.js)
- **Windows Build Tools:**
    - Install Visual Studio (Community Edition is sufficient) with the "Desktop development with C++" workload. Ensure the C++ toolset and Windows SDK are included.
    - Or, install the stand-alone Visual Studio Build Tools, making sure to select the C++ build tools workload.
- Python (v3.x, required by `node-gyp`, usually included with Node.js or VS Build Tools installs). `node-gyp` will typically find the correct Python installation.

## Installation and Building

Clone the repository and install dependencies. The initial `npm install` command will automatically download JavaScript dependencies and trigger the first build for the C++ native addon using `node-gyp`.

```bash
# Clone this repository
# git clone ... (replace with your repo URL)

# Go into the repository
cd electron_app_hello_world

# Install JavaScript dependencies AND build the C++ addon
npm install
```

**Important:** If you modify the C++ source code (`cpp-addon/src/addon.cpp`) later, you **must** rebuild the addon. See the "Rebuilding the Addon" section below.

If the initial C++ addon build fails, double-check the prerequisites (especially the C++ build tools installation and path) and review the error messages from `node-gyp` in the console output.

## Rebuilding the Addon

After making changes to the C++ code in `cpp-addon/src/addon.cpp` or the `cpp-addon/binding.gyp` file, you need to recompile the native addon. The simplest way is often to re-run the install script, which includes the rebuild step:

```bash
npm install
```

Alternatively, you can directly invoke `node-gyp`:

```bash
# Navigate to the addon directory
cd cpp-addon

# Rebuild the addon
node-gyp rebuild

# Navigate back to the project root
cd ..
```

## Usage

Run the application:

```bash
# Run the application
npm start

# Optional: Run in development mode with hot reloading (if configured)
# npm run dev
```

The application should load, and any UI elements connected to the addon functions (like the BIT Initiate test) should be available. Check the Developer Tools console (Ctrl+Shift+I) for logs and errors related to addon loading or function calls.

## Project Structure

- `main.js`: Electron main process script. Manages the application lifecycle and window creation.
- `preload.js`: Script to securely expose Node.js/addon functionality to the renderer process.
- `renderer.js`: JavaScript for the renderer process (the UI window). Interacts with the addon via functions exposed in `preload.js`.
- `index.html`: Main HTML file for the application window.
- `styles.css`: CSS styles for the UI.
- `package.json`: Project configuration, dependencies, and build scripts.
- `cpp-addon/`: Directory containing the C++ native addon.
    - `src/addon.cpp`: C++ source code for the N-API wrapper functions.
    - `binding.gyp`: Build configuration file for `node-gyp`.
    - `vendor/`: Contains the vendor-provided header (`include/`), library (`lib/`), and runtime (`bin/`) files.
    - `build/`: Output directory for the compiled addon (`*.node`) and copied DLLs (created during `npm install` or rebuild). This directory is ignored by Git.

## Development Notes

### Adding New Function Wrappers

To add a wrapper for another function from the vendor's library (e.g., `BTICard_SomeFunction`):

1.  **Declare the Function:** Add the C function signature to the `extern "C" { ... }` block at the top of `cpp-addon/src/addon.cpp` based on its definition in `BTICARD.H` or `BTI429.H`.
    ```cpp
    extern "C" {
        // ... existing declarations ...
        RETURN_TYPE BTICard_SomeFunction(PARAM_TYPE param1, OTHER_TYPE* outParam2);
    }
    ```
2.  **Create N-API Wrapper:** Define a new C++ function that takes `const Napi::CallbackInfo& info` and returns `Napi::Value`.
    ```cpp
    Napi::Value SomeFunctionWrapped(const Napi::CallbackInfo& info) {
        Napi::Env env = info.Env();

        // 1. Check info.Length() and argument types (info[0].IsNumber(), etc.)
        if (info.Length() != 1 || !info[0].IsNumber()) {
            Napi::TypeError::New(env, "Expected: param1 (Number)").ThrowAsJavaScriptException();
            return env.Null();
        }

        // 2. Extract arguments and cast to C++ types
        // Use .Int32Value(), .DoubleValue(), .Int64Value() (for handles), etc.
        PARAM_TYPE cppParam1 = info[0].As<Napi::Number>().Int32Value();
        OTHER_TYPE cppOutParam2; // Variable to hold output parameter

        // 3. Call the original C function
        RETURN_TYPE result = BTICard_SomeFunction(cppParam1, &cppOutParam2);

        // 4. Package results for JavaScript
        // Can return single value (Napi::Number::New, Napi::String::New, etc.)
        // Or an object for multiple return values/status
        Napi::Object resultObj = Napi::Object::New(env);
        resultObj.Set("resultCode", Napi::Number::New(env, result));
        resultObj.Set("outputValue", Napi::Number::New(env, cppOutParam2)); // Example
        return resultObj;
    }
    ```
3.  **Export the Wrapper:** Add the new wrapper function to the `Init` function at the bottom of `addon.cpp`.
    ```cpp
    Napi::Object Init(Napi::Env env, Napi::Object exports) {
        // ... existing exports ...
        exports.Set(Napi::String::New(env, "someFunction"), // JavaScript name
                    Napi::Function::New(env, SomeFunctionWrapped)); // C++ wrapper function
        return exports;
    }
    ```
4.  **Rebuild:** Run `npm install` or `node-gyp rebuild` as described in the "Rebuilding the Addon" section.
5.  **Use in JavaScript:** Call the newly exported function from your JavaScript code (e.g., in `main.js`) using the `btiAddon` object:
    ```javascript
    const results = btiAddon.someFunction(jsParam1);
    console.log(results.resultCode, results.outputValue);
    ```

- **Accessing Addon:** The `bindings` package (`require('bindings')('bti_addon')`) is used in JavaScript (`main.js`) to locate and load the compiled `.node` file from the `cpp-addon/build/Release` directory.
- **Handles:** Device/card handles (like `HCARD`, `HCORE`) are pointers in C++. They are typically passed between C++ and JavaScript as Numbers (using `Int64Value()` or `BigInt64Value()` for safety on 64-bit systems) and cast back and forth using `reinterpret_cast<uintptr_t>` in C++.

## License

This project is licensed under the MIT License. Vendor libraries in `cpp-addon/vendor/` are subject to their own licenses.

## Author

Brad Hardwick

---

This project was created using Electron, a framework for creating native applications with web technologies like JavaScript, HTML, and CSS. 