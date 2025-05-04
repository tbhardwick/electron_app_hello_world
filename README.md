# Electron UA2430 Test Application

An Electron application demonstrating communication with a UA2430 device via its vendor-provided C++ API, using a Node.js native addon.

## Description

This application serves as a testbed and example for interacting with BTI UA2430 hardware from a modern desktop application built with Electron. It utilizes a C++ native addon to bridge the gap between JavaScript and the vendor's C++ library, providing a robust and maintainable solution.

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

To run the application for development purposes:

```bash
# Run the application using Electron Forge's development server
npm start
```

The application should load, and any UI elements connected to the addon functions (like the BIT Initiate test) should be available. Check the Developer Tools console (Ctrl+Shift+I) for logs and errors related to addon loading or function calls.

Refer to the "Development Workflow" section below for packaging and building distributables.

## Development Workflow

Standard npm scripts are provided for common development tasks:

- **Run in Development Mode:** `npm start` or `npm run dev`
  - Launches the application directly from source code using Electron Forge.
  - Enables features like hot-reloading for the renderer process, speeding up development.
  - **Does not create distributable packages.**

- **Package the Application:** `npm run package`
  - Bundles the application code and production dependencies into an unpackaged format (located in the `out/` directory).
  - Useful for testing the packaging process or running the app in a semi-production state without creating a full installer.

- **Build Distributables:** `npm run make`
  - Creates the final, distributable packages (e.g., installers, zip files) for your current platform.
  - The output is placed in the `out/make/` directory.
  - Use this command when you want to prepare a version for users.

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

## Available Addon Functions

This section details the functions currently wrapped in the C++ addon and exposed to JavaScript via the `btiAddon` object (loaded in `main.js`). The functions are grouped by category.

Constants mentioned (like `ERR_NONE`, `STAT_EMPTY`, etc.) are assumed to be defined in `bti_constants.h` or directly in the addon/JS code.

### Card and Core Management

*   **`cardOpen(cardNumber: number): Object`**
    *   **Description:** Attempts to open a connection to the specified BTI card.
    *   **Arguments:**
        *   `cardNumber`: The integer index of the card to open (e.g., 0).
    *   **Returns:** `Object`
        *   `success: boolean`: True if the card was opened successfully.
        *   `handle: number | null`: The opaque card handle (as a number) if successful, otherwise null.
        *   `message: string`: Status message ("Card opened successfully." or "Failed to open card.").
        *   `resultCode: number`: The raw result code from the underlying `BTICard_CardOpen` function.

*   **`coreOpen(coreNumber: number, cardHandle: number): Object`**
    *   **Description:** Attempts to open a specific core on an already opened card.
    *   **Arguments:**
        *   `coreNumber`: The integer index of the core to open (e.g., 0).
        *   `cardHandle`: The handle obtained from a successful `cardOpen` call.
    *   **Returns:** `Object`
        *   `success: boolean`: True if the core was opened successfully.
        *   `handle: number | null`: The opaque core handle (as a number) if successful, otherwise null.
        *   `message: string`: Status message ("Core opened successfully." or "Failed to open core.").
        *   `resultCode: number`: The raw result code from the underlying `BTICard_CoreOpen` function.

*   **`cardClose(cardHandle: number): number`**
    *   **Description:** Closes the connection to the specified card. This implicitly closes any open cores on that card.
    *   **Arguments:**
        *   `cardHandle`: The handle obtained from a successful `cardOpen` call.
    *   **Returns:** `number` - The raw result code (ERRVAL) from the underlying `BTICard_CardClose` function (0 typically indicates success).

*   **`cardReset(coreHandle: number): undefined`**
    *   **Description:** Stops and performs a hardware reset on the specified core.
    *   **Arguments:**
        *   `coreHandle`: The core handle obtained from a successful `coreOpen` call.
    *   **Returns:** `undefined` (The underlying C function is void).

*   **`cardStart(coreHandle: number): number`**
    *   **Description:** Starts the operation of the specified core.
    *   **Arguments:**
        *   `coreHandle`: The core handle obtained from a successful `coreOpen` call.
    *   **Returns:** `number` - The raw result code (ERRVAL) from the underlying `BTICard_CardStart` function (0 typically indicates success).

*   **`cardStop(coreHandle: number): boolean`**
    *   **Description:** Stops the operation of the specified core.
    *   **Arguments:**
        *   `coreHandle`: The core handle obtained from a successful `coreOpen` call.
    *   **Returns:** `boolean` - The result from `BTICard_CardStop` (TRUE if the core was running, FALSE otherwise).

*   **`cardTest(testLevel: number, coreHandle: number): number`**
    *   **Description:** Runs a built-in self-test on the specified core.
    *   **Arguments:**
        *   `testLevel`: The integer code for the test level to run (e.g., 1 for Memory Interface Test). See `bti_constants.h`.
        *   `coreHandle`: The handle obtained from a successful `coreOpen` call.
    *   **Returns:** `number` - The raw result code (ERRVAL) from the underlying `BTICard_CardTest` function (0 typically indicates success).

*   **`bitInitiate(cardHandle: number): number`**
    *   **Description:** Initiates the Built-In Test (BIT) for the specified card. Leaves the card in a reset state.
    *   **Arguments:**
        *   `cardHandle`: The handle obtained from a successful `cardOpen` call.
    *   **Returns:** `number` - The raw result code (ERRVAL) from the underlying `BTICard_BITInitiate` function (0 typically indicates success).

*   **`cardGetInfo(infoType: number, channelNum: number, coreHandle: number): number`**
    *   **Description:** Retrieves specific information about the card or a channel.
    *   **Arguments:**
        *   `infoType`: The integer code for the information requested (e.g., `INFOTYPE_429COUNT`). See `bti_constants.h` and `BTICard_CardGetInfo.md`.
        *   `channelNum`: Reserved for future use (pass 0 or -1 as appropriate per BTI docs).
        *   `coreHandle`: The core handle.
    *   **Returns:** `number` - The requested information value (ULONG). Note: Error handling depends on interpreting the returned value based on `infoType`, as no separate status code is returned by this wrapper.

*   **`getErrorDescription(errorCode: number, coreHandle: number | null): string`**
    *   **Description:** Retrieves a descriptive string for a given BTI error code.
    *   **Arguments:**
        *   `errorCode`: The numerical error code (ERRVAL) returned by another BTI function.
        *   `coreHandle`: The core handle associated with the error, if applicable (can be `null` if the error occurred before a core was opened or if the error is not core-specific).
    *   **Returns:** `string` - The error description, or a generic message if the code is unknown.

### ARINC 429 Channel Management

*   **`chConfig(configVal: number, channelNum: number, coreHandle: number): number`**
    *   **Description:** Configures an ARINC 429 channel (e.g., speed, parity). Stops, configures, and potentially restarts the channel.
    *   **Arguments:**
        *   `configVal`: Bitwise OR-ed configuration flags (e.g., `CHCFG429_HIGHSPEED`). See `bti_constants.h` and `BTI429_ChConfig.md`.
        *   `channelNum`: The channel number to configure.
        *   `coreHandle`: The core handle.
    *   **Returns:** `number` - The raw result code (ERRVAL) from `BTI429_ChConfig` (0 indicates success, negative for error).

*   **`chStart(channelNum: number, coreHandle: number): boolean`**
    *   **Description:** Enables the operation of the specified ARINC 429 channel.
    *   **Arguments:**
        *   `channelNum`: The channel number to start.
        *   `coreHandle`: The core handle.
    *   **Returns:** `boolean` - The result from `BTI429_ChStart` (TRUE if the channel was previously enabled, FALSE otherwise). Note: This indicates the previous state, not necessarily success/failure of the start command itself.

*   **`chStop(channelNum: number, coreHandle: number): boolean`**
    *   **Description:** Disables operation of the specified ARINC 429 channel.
    *   **Arguments:**
        *   `channelNum`: The channel number to stop.
        *   `coreHandle`: The core handle.
    *   **Returns:** `boolean` - The result from `BTI429_ChStop` (TRUE if the channel was previously enabled, FALSE otherwise). Note: This indicates the previous state.

### ARINC 429 List Buffer Operations

*   **`listXmtCreate(configVal: number, count: number, msgAddr: number, coreHandle: number): Object`**
    *   **Description:** Creates a transmit list buffer (e.g., FIFO) for a channel.
    *   **Arguments:**
        *   `configVal`: List configuration flags (e.g., `LISTCRT429_FIFO`). See `bti_constants.h` and `BTI429_ListXmtCreate.md`.
        *   `count`: The desired number of entries + 1.
        *   `msgAddr`: Message address/handle to associate (use 0 if not needed, check BTI docs).
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) on success, `ERR_FAIL` (-1) on failure).
        *   `listAddr: number | null`: The address/identifier (LISTADDR) of the created list buffer, or null on failure.

*   **`listRcvCreate(configVal: number, count: number, msgAddr: number, coreHandle: number): Object`**
    *   **Description:** Creates a receive list buffer (e.g., FIFO, circular) for a channel.
    *   **Arguments:**
        *   `configVal`: List configuration flags (e.g., `LISTCRT429_FIFO | LISTCRT429_CIRCULAR`). See `bti_constants.h` and `BTI429_ListRcvCreate.md`.
        *   `count`: The desired number of entries + 1.
        *   `msgAddr`: Message address/handle to associate (use 0 if not needed, check BTI docs).
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) on success, `ERR_FAIL` (-1) on failure).
        *   `listAddr: number | null`: The address/identifier (LISTADDR) of the created list buffer, or null on failure.

*   **`listDataWr(value: number, listAddr: number, coreHandle: number): number`**
    *   **Description:** Writes a single 32-bit ARINC 429 word to a transmit list buffer.
    *   **Arguments:**
        *   `value`: The 32-bit word (ULONG) to transmit.
        *   `listAddr`: The list buffer address obtained from `listXmtCreate`.
        *   `coreHandle`: The core handle.
    *   **Returns:** `number` - Result code (`ERR_NONE` (0) if `BTI429_ListDataWr` returned TRUE, `ERR_FAIL` (-1) if it returned FALSE).

*   **`listDataBlkWr(dataArray: number[], listAddr: number, coreHandle: number): number`**
    *   **Description:** Writes an array of 32-bit ARINC 429 words to a transmit list buffer.
    *   **Arguments:**
        *   `dataArray`: An array of numbers representing the 32-bit words (ULONG). Max length 65535.
        *   `listAddr`: The list buffer address.
        *   `coreHandle`: The core handle.
    *   **Returns:** `number` - Result code (`ERR_NONE` (0) if `BTI429_ListDataBlkWr` returned TRUE, `ERR_FAIL` (-1) if it returned FALSE).

*   **`listDataRd(listAddr: number, coreHandle: number): Object`** (Synchronous)
    *   **Description:** Reads a single 32-bit ARINC 429 word from a receive list buffer. Checks status first to avoid ambiguity. Potentially blocking if used incorrectly (prefer async version).
    *   **Arguments:**
        *   `listAddr`: The list buffer address obtained from `listRcvCreate`.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) for success, `ERR_UNDERFLOW` if empty, or other negative BTI error codes from status check).
        *   `value: number | null`: The 32-bit word (ULONG) read, or null if status is not `ERR_NONE`.

*   **`listDataBlkRd(listAddr: number, maxCount: number, coreHandle: number): Object`** (Synchronous)
    *   **Description:** Reads a block of up to `maxCount` words from a receive list buffer. Checks status after attempting read if needed. Potentially blocking (prefer async version).
    *   **Arguments:**
        *   `listAddr`: The list buffer address.
        *   `maxCount`: The maximum number of words to read (0 to 65535).
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) for success (including reading 0 words), `ERR_UNDERFLOW` if read attempt failed due to empty list, or other negative BTI error codes).
        *   `dataArray: number[] | null`: An array containing the words read (ULONGs). Can be empty if 0 words were read successfully. Null on error.

*   **`listStatus(listAddr: number, coreHandle: number): Object`**
    *   **Description:** Checks the current status of a list buffer.
    *   **Arguments:**
        *   `listAddr`: The list buffer address.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code of the check itself (`ERR_NONE` (0) indicates success, or negative BTI error code).
        *   `listStatus: number | null`: The actual list status (`STAT_EMPTY`, `STAT_PARTIAL`, `STAT_FULL`, `STAT_OFF`) if the check succeeded (`status === ERR_NONE`), otherwise null.

### ARINC 429 Filtering

*   **`filterSet(configVal: number, label: number, sdiMask: number, channelNum: number, coreHandle: number): Object`**
    *   **Description:** Sets up a filter based on label and SDI bits for a receive channel.
    *   **Arguments:**
        *   `configVal`: Filter/message configuration flags (e.g., `MSGCRT429_TIMETAG`). See `bti_constants.h` and `BTI429_FilterSet.md`.
        *   `label`: The ARINC 429 label (octal) to filter on.
        *   `sdiMask`: Bitmask for SDI bits to match (e.g., `SDIALL`, `SDI01`). See `bti_constants.h`.
        *   `channelNum`: The receive channel number.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) on success, `ERR_FAIL` (-1) on failure).
        *   `filterAddr: number | null`: The address/handle (MSGADDR) of the created filter message, or null on failure.

*   **`filterDefault(configVal: number, channelNum: number, coreHandle: number): Object`**
    *   **Description:** Sets up a default filter (receives all data) for a channel. Overwrites any existing filters for the channel.
    *   **Arguments:**
        *   `configVal`: Filter/message configuration flags. See `bti_constants.h` and `BTI429_FilterDefault.md`.
        *   `channelNum`: The receive channel number.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) on success, `ERR_FAIL` (-1) on failure).
        *   `filterAddr: number | null`: The address/handle (MSGADDR) of the created filter message, or null on failure.

### ARINC 429 Message Operations

*   **`msgCreate(ctrlflags: number, coreHandle: number): Object`**
    *   **Description:** Creates a message structure used for non-list based transmission or reception.
    *   **Arguments:**
        *   `ctrlflags`: Message creation flags (e.g., `MSGCRT429_TIMETAG`, `MSGCRT429_HITCOUNT`). See `bti_constants.h` and `BTI429_MsgCreate.md`.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) on success, `ERR_FAIL` (-1) on failure).
        *   `msgAddr: number | null`: The address/handle (MSGADDR) of the created message structure, or null on failure.

*   **`msgDataWr(value: number, msgAddr: number, coreHandle: number): undefined`**
    *   **Description:** Writes a 32-bit ARINC 429 word into a message structure.
    *   **Arguments:**
        *   `value`: The 32-bit word (ULONG) to write.
        *   `msgAddr`: The message address obtained from `msgCreate` or `filterSet`/`filterDefault`.
        *   `coreHandle`: The core handle.
    *   **Returns:** `undefined` (The underlying C function is void).

*   **`msgDataRd(msgAddr: number, coreHandle: number): Object`**
    *   **Description:** Reads the 32-bit ARINC 429 word from a message structure.
    *   **Arguments:**
        *   `msgAddr`: The message address.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) is assumed, as the BTI function doesn't directly return status).
        *   `value: number`: The 32-bit data word (ULONG) read.

*   **`msgBlockRd(msgAddr: number, coreHandle: number): Object`**
    *   **Description:** Reads the entire set of fields from a message structure (contended access).
    *   **Arguments:**
        *   `msgAddr`: The message address.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) on success, `ERR_FAIL` (-1) on failure - based on return address check).
        *   `fields: Object | null`: An object containing the message fields (`msgopt`, `msgact`, `msgdata`, `timetag`, etc.) or null on failure.

*   **`msgCommRd(msgAddr: number, coreHandle: number): Object`**
    *   **Description:** Reads the entire set of fields from a message structure (non-contended access).
    *   **Arguments:**
        *   `msgAddr`: The message address.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) on success, `ERR_FAIL` (-1) on failure - based on return address check).
        *   `fields: Object | null`: An object containing the message fields (`msgopt`, `msgact`, `msgdata`, `timetag`, etc.) or null on failure.

*   **`msgIsAccessed(msgAddr: number, coreHandle: number): Object`**
    *   **Description:** Checks if a message structure has been accessed (Hit bit set) and clears the bit.
    *   **Arguments:**
        *   `msgAddr`: The message address.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) is assumed).
        *   `accessed: boolean`: True if the message had been accessed (Hit bit was set), false otherwise.

### ARINC 429 Data Field Extraction (Utility Functions)

These functions operate directly on a 32-bit ARINC word (passed as a number) and do not require a `coreHandle` as they don't interact with the hardware.

*   **`fldGetLabel(msg: number): number`**
    *   **Description:** Extracts the 8-bit label (bits 1-8) from an ARINC 429 word.
    *   **Arguments:**
        *   `msg`: The 32-bit ARINC word (ULONG).
    *   **Returns:** `number` - The 8-bit label (USHORT).

*   **`fldGetSDI(msg: number): number`**
    *   **Description:** Extracts the 2-bit SDI (bits 9-10) from an ARINC 429 word.
    *   **Arguments:**
        *   `msg`: The 32-bit ARINC word (ULONG).
    *   **Returns:** `number` - The 2-bit SDI (USHORT).

*   **`fldGetData(msg: number): number`**
    *   **Description:** Extracts the 23-bit data field + parity bit (bits 11-32) from an ARINC 429 word.
    *   **Arguments:**
        *   `msg`: The 32-bit ARINC word (ULONG).
    *   **Returns:** `number` - The 24-bit data+parity value (ULONG).

*   **`bcdGetData(msg: number, msb: number, lsb: number): number`**
    *   **Description:** Extracts a BCD (Binary-Coded Decimal) data field between specified bits from an ARINC 429 word.
    *   **Arguments:**
        *   `msg`: The 32-bit ARINC word (ULONG).
        *   `msb`: The most significant bit number (11-29) of the BCD field.
        *   `lsb`: The least significant bit number (9-29) of the BCD field.
    *   **Returns:** `number` - The extracted BCD data as an unsigned integer (ULONG).

*   **`bnrGetData(msg: number, msb: number, lsb: number): number`**
    *   **Description:** Extracts a BNR (Binary) data field between specified bits from an ARINC 429 word.
    *   **Arguments:**
        *   `msg`: The 32-bit ARINC word (ULONG).
        *   `msb`: The most significant bit number (11-28) of the BNR field.
        *   `lsb`: The least significant bit number (9-28) of the BNR field.
    *   **Returns:** `number` - The extracted BNR data as an unsigned integer (ULONG).

### Event Log

*   **`eventLogConfig(ctrlflags: number, count: number, coreHandle: number): number`**
    *   **Description:** Configures and enables/disables the event log for the core.
    *   **Arguments:**
        *   `ctrlflags`: Configuration flags (`LOGCFG_ENABLE`, `LOGCFG_DISABLE`). See `bti_constants.h` and `BTICard_EventLogConfig.md`.
        *   `count`: Size of the log buffer (note: fixed size on some hardware).
        *   `coreHandle`: The core handle.
    *   **Returns:** `number` - Result code (ERRVAL, 0 for success, negative for error).

*   **`eventLogRd(coreHandle: number): Object`**
    *   **Description:** Reads the next entry from the event log list.
    *   **Arguments:**
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) if an entry was read, `ERR_FAIL` (-1) if empty).
        *   `type: number | null`: The event type code (USHORT), or null if empty.
        *   `info: number | null`: Additional info associated with the event (ULONG), or null if empty.
        *   `channel: number | null`: Channel associated with the event (INT), or null if empty.
        *   `entryAddr: number | null`: Address of the log entry read (ULONG), or null if empty.

*   **`eventLogStatus(coreHandle: number): Object`**
    *   **Description:** Checks the status of the event log list without reading an entry.
    *   **Arguments:**
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code of the check itself (`ERR_NONE` (0) indicates success, or negative BTI error code).
        *   `logStatus: number | null`: The actual log status (`STAT_EMPTY`, `STAT_PARTIAL`, `STAT_FULL`, `STAT_OFF`) if the check succeeded (`status === ERR_NONE`), otherwise null.

### Timers

*   **`timer64Rd(coreHandle: number): Object`**
    *   **Description:** Reads the 64-bit binary timer value.
    *   **Arguments:**
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (ERRVAL, 0 for success, negative for error).
        *   `value: BigInt | null`: The 64-bit timer value as a BigInt, or null on error.

*   **`timer64Wr(timerValue: BigInt, coreHandle: number): undefined`**
    *   **Description:** Writes a 64-bit value to the binary timer.
    *   **Arguments:**
        *   `timerValue`: The 64-bit value to write (ULONGLONG) passed as a JavaScript BigInt.
        *   `coreHandle`: The core handle.
    *   **Returns:** `undefined` (The underlying C function is void).

### Discrete I/O

*   **`extDIORd(dionum: number, coreHandle: number): Object`**
    *   **Description:** Reads the status of a single external digital I/O pin.
    *   **Arguments:**
        *   `dionum`: The DIO number/index to read.
        *   `coreHandle`: The core handle.
    *   **Returns:** `Object`
        *   `status: number`: Result code (`ERR_NONE` (0) is assumed).
        *   `value: boolean`: The state of the DIO pin (true for active/high, false for inactive/low).

*   **`extDIOWr(dionum: number, dioval: boolean, coreHandle: number): number`**
    *   **Description:** Sets the state of a single external digital I/O pin.
    *   **Arguments:**
        *   `dionum`: The DIO number/index to write.
        *   `dioval`: The state to set (true for active/high, false for inactive/low).
        *   `coreHandle`: The core handle.
    *   **Returns:** `number` - Result code (`ERR_NONE` (0) is assumed as underlying function is void).

### Asynchronous Operations

These functions perform polling in a background thread and return a Promise.

*   **`listDataRdAsync(listAddr: number, coreHandle: number, timeoutMs: number): Promise<Object>`**
    *   **Description:** Reads a single 32-bit ARINC 429 word from a receive list buffer asynchronously. Uses a background thread to poll the list status and retrieve data.
    *   **Arguments:**
        *   `listAddr`: The list buffer address obtained from `listRcvCreate`.
        *   `coreHandle`: The core handle.
        *   `timeoutMs`: Maximum time in milliseconds to wait for data.
    *   **Returns:** `Promise<Object>`
        *   **Resolves with:** `Object`
            *   `status: number`: Result code (`ERR_NONE` (0) for success, or other BTI error codes from status check).
            *   `value: number | null`: The 32-bit word (ULONG) read, or null if status is not `ERR_NONE`.
        *   **Rejects with:** `Object` (On timeout or error checking status)
            *   `status: number`: Error code (`ERR_TIMEOUT` or BTI error).
            *   `message: string`: Error description ("Timeout waiting for data." or "Error checking list status.").
            *   `value: null`.

*   **`listDataBlkRdAsync(listAddr: number, maxCount: number, coreHandle: number, timeoutMs: number): Promise<Object>`**
    *   **Description:** Reads a block of up to `maxCount` words from a receive list buffer asynchronously. Uses a background thread to poll the list status and retrieve data.
    *   **Arguments:**
        *   `listAddr`: The list buffer address.
        *   `maxCount`: The maximum number of words to attempt to read (0 to 65535).
        *   `coreHandle`: The core handle.
        *   `timeoutMs`: Maximum time in milliseconds to wait for data to become available.
    *   **Returns:** `Promise<Object>`
        *   **Resolves with:** `Object`
            *   `status: number`: Result code (`ERR_NONE` (0) for success (including reading 0 words), `ERR_UNDERFLOW` if list remains empty after timeout, or other BTI error codes from status check).
            *   `dataArray: number[] | null`: An array containing the words read (ULONGs). Can be empty if 0 words were read successfully. Null if status is not `ERR_NONE`.
        *   **Rejects with:** `Object` (On timeout with non-empty list or error checking status)
            *   `status: number`: Error code (`ERR_TIMEOUT` or BTI error).
            *   `message: string`: Error description ("Timeout waiting for data block." or "Error checking list status." or "Error reading data block.").
            *   `dataArray: null`.

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