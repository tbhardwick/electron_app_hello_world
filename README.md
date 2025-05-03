# Electron UA2430 Test Application

An Electron application demonstrating communication with a UA2430 device via its vendor-provided C++ API, using a Node.js native addon.

## Description

This application serves as a testbed and example for interacting with BTI UA2430 hardware from a modern desktop application built with Electron. It replaces manual DLL management or C# interop with a more robust C++ native addon approach.

## Architecture

- **Electron Shell:** Provides the main application window and user interface (HTML/CSS/JS).
- **JavaScript (Renderer/Main):** Application logic written in JavaScript (`renderer.js`, `main.js`).
- **Node.js Native Addon (`bti_addon`):** A C++ module that acts as a bridge between JavaScript and the vendor's C++ library.
    - Located in the `cpp-addon/` directory.
    *   Wraps functions from the vendor's `BTICARD.H` / `BTI429.H` headers using N-API.
    *   Links against the vendor's static libraries (`.LIB` files).
    *   The build process copies the required vendor runtime libraries (`.DLL` files) alongside the compiled addon.
- **Vendor Library:** The pre-compiled C++ libraries (`.DLL`, `.LIB`) and headers (`.H`) provided by the hardware vendor (BTI). These are located in `cpp-addon/vendor/`.

JavaScript code calls functions exported by the `bti_addon` (e.g., `btiAddon.bitInitiate(handle)`), which in turn call the underlying functions in the vendor's DLLs.

## Features

- Demonstrates loading and calling a C++ native addon from Electron.
- Provides a basic example of wrapping a vendor C++ API (`BITInitiate`).
- Establishes a build process using `node-gyp` for the native addon.

## Prerequisites

Before you begin, ensure you have the following installed:
- [Node.js](https://nodejs.org/) (v18 or higher recommended)
- npm (comes with Node.js)
- **Windows Build Tools:**
    - Install Visual Studio (Community Edition is sufficient) with the "Desktop development with C++" workload. Ensure the C++ toolset and Windows SDK are included.
    - Or, install the stand-alone Visual Studio Build Tools, making sure to select the C++ build tools workload.
- Python (v3.x, required by `node-gyp`, usually included with Node.js or VS Build Tools installs). `node-gyp` will typically find the correct Python installation.

## Installation and Building

Clone the repository and install dependencies. The `npm install` command will automatically trigger the build for the C++ native addon using `node-gyp`.

```bash
# Clone this repository
# git clone ... (replace with your repo URL)

# Go into the repository
cd electron_app_hello_world

# Install JavaScript dependencies and build the C++ addon
npm install
```

If the C++ addon build fails, check the prerequisites (especially the C++ build tools) and the error messages from `node-gyp` in the console output.

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
    - `build/`: Output directory for the compiled addon (`*.node`) and copied DLLs (created during `npm install`). This directory is ignored by Git.

## Development Notes

- **Modifying C++ Addon:** After changing code in `cpp-addon/src/addon.cpp`, you *must* rebuild the addon by running `npm install` again.
- **Accessing Addon:** The `bindings` package (`require('bindings')('bti_addon')`) is used in JavaScript to locate and load the compiled `.node` file from the `cpp-addon/build/Release` directory.

## License

This project is licensed under the MIT License. Vendor libraries in `cpp-addon/vendor/` are subject to their own licenses.

## Author

Brad Hardwick

---

This project was created using Electron, a framework for creating native applications with web technologies like JavaScript, HTML, and CSS. 