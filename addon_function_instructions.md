# C++ Addon Function Implementation Guide (BTI/ARINC 429)

This document outlines the core functions required for the C++ addon to interact with the BTI UA2430 device via the BTI API (`BTICARD.dll`/`BTICARD64.dll` and `BTI429.dll`/`BTI42964.dll`). Refer to the **MA132 ARINC 429 Programming Manual** for detailed function parameters, return codes, constants, and usage examples.

## Core Concepts

*   **Handles:** Most BTI functions require a `handle` obtained by opening a card (`CardOpenStr`). This handle must be passed to subsequent calls for that card.
*   **Channels:** ARINC 429 operations are channel-specific. Functions often take a `channelNum` parameter.
*   **Error Handling:** Functions typically return an integer status code. `0` (ERR_NONE) indicates success. Consult the manual for specific error codes.
*   **Data Types:** Ensure correct mapping of C# types (e.g., `uint`, `ushort`, `int`, `nint`) to their C++ equivalents (e.g., `unsigned int`, `unsigned short`, `int`, `INT_PTR` or `HANDLE`). Pay attention to pointer types and references (`ref`).

## Required Functions

The following functions should be implemented in the C++ addon and exposed to Node.js.

### 1. Device Management (`BTICARD`)

These functions manage the connection to the BTI hardware.

*   **`addon_CardOpenStr(cardString)`**
    *   **Purpose:** Opens a connection to the specified BTI card/device.
    *   **BTI Equivalent:** `BTICard_CardOpenStr(ref nint lphandle, string cardstr)`
    *   **Parameters:**
        *   `cardString` (string): Identifier for the card (e.g., "UA2430-0").
    *   **Returns:** An object containing:
        *   `status` (int): BTI status code.
        *   `handle` (number/pointer): The device handle if successful, otherwise null/invalid. Store this handle for subsequent calls.
    *   **Notes:** The C++ function needs to manage the handle (`nint` in C#, likely `INT_PTR` or `HANDLE` in C++) returned by reference.

*   **`addon_CardClose(handle)`**
    *   **Purpose:** Closes the connection to the specified BTI card.
    *   **BTI Equivalent:** `BTICard_CardClose(nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The handle obtained from `addon_CardOpenStr`.
    *   **Returns:**
        *   `status` (int): BTI status code.
    *   **Notes:** Releases the device handle.

*   **`addon_CardReset(handle)`**
    *   **Purpose:** Resets the BTI card.
    *   **BTI Equivalent:** `BTICard_CardReset(nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The handle obtained from `addon_CardOpenStr`.
    *   **Returns:** (void or status int - check manual)
    *   **Notes:** Often called after opening the card.

*   **`addon_CardGetInfo(handle, infoType, channelNum)`**
    *   **Purpose:** Retrieves specific information about the card or a channel.
    *   **BTI Equivalent:** `BTICard_CardGetInfo(ushort infotype, int channum, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `infoType` (int): The type of information requested (e.g., `INFOTYPE_429COUNT`, `INFOTYPE_SERIALNUM`). Use constants defined in the manual/header.
        *   `channelNum` (int): Channel number (often -1 for card-level info).
    *   **Returns:** An object containing:
        *   `status` (int): BTI status code (check if `CardGetInfo` itself returns status or if errors are handled via other means).
        *   `value` (int): The requested information value.

### 2. ARINC 429 Channel Configuration (`BTI429`)

These functions configure individual ARINC 429 channels for transmit or receive operations.

*   **`addon_429_ChConfig(handle, channelNum, configVal)`**
    *   **Purpose:** Configures an ARINC 429 channel.
    *   **BTI Equivalent:** `BTI429_ChConfig(uint configval, int channum, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `channelNum` (int): The channel number to configure.
        *   `configVal` (int): Configuration flags combined using bitwise OR (e.g., `CHCFG429_HIGHSPEED | CHCFG429_PAREVEN`). Use constants from the manual/header.
    *   **Returns:**
        *   `status` (int): BTI status code.

*   **`addon_429_ChStart(handle, channelNum)`**
    *   **Purpose:** Starts the ARINC 429 engine for the specified channel.
    *   **BTI Equivalent:** `BTI429_ChStart(int channum, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `channelNum` (int): The channel number to start.
    *   **Returns:**
        *   `status` (int): BTI status code.

*   **`addon_429_ChStop(handle, channelNum)`**
    *   **Purpose:** Stops the ARINC 429 engine for the specified channel.
    *   **BTI Equivalent:** `BTI429_ChStop(int channum, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `channelNum` (int): The channel number to stop.
    *   **Returns:**
        *   `status` (int): BTI status code.

### 3. ARINC 429 Data Transmission (`BTI429`)

Functions for sending ARINC 429 data. FIFO/List-based transmission is common.

*   **`addon_429_ListXmtCreate(handle, channelNum, count, configVal)`** (Required for ListDataWr)
    *   **Purpose:** Creates a transmit list buffer.
    *   **BTI Equivalent:** `BTI429_ListXmtCreate(uint listconfigval, int count, uint msgaddr, nint handleval)` (Note: `msgaddr` might be 0 for async lists, check manual).
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `channelNum` (int): The transmit channel number.
        *   `count` (int): The size of the list buffer in words.
        *   `configVal` (int): List configuration flags (e.g., `LISTCRT429_FIFO`, `LISTCRT429_LOG`).
    *   **Returns:** An object containing:
        *   `status` (int): BTI status code.
        *   `listAddr` (int): The address/identifier of the created list buffer.

*   **`addon_429_ListDataWr(handle, listAddr, value)`** (Assuming List/FIFO mode)
    *   **Purpose:** Writes a single ARINC 429 word to a transmit list buffer.
    *   **BTI Equivalent:** `BTI429_ListDataWr(uint value, uint listaddr, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `listAddr` (int): The address/identifier of the transmit list buffer (obtained via `ListXmtCreate` or similar - might need an addon function for list creation).
        *   `value` (int): The 32-bit ARINC 429 word to transmit.
    *   **Returns:**
        *   `status` (int): BTI status code.

*   **`addon_429_ListDataBlkWr(handle, listAddr, dataArray)`** (Alternative)
    *   **Purpose:** Writes a block of ARINC 429 words to a transmit list buffer.
    *   **BTI Equivalent:** `BTI429_ListDataBlkWr(uint[] listbuf, int count, uint listaddr, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `listAddr` (int): The address/identifier of the transmit list buffer.
        *   `dataArray` (array of numbers): An array containing the 32-bit ARINC 429 words to transmit.
    *   **Returns:**
        *   `status` (int): BTI status code.
    *   **Notes:** The C++ function needs to handle the array conversion.

### 4. ARINC 429 Data Reception (`BTI429`)

Functions for receiving ARINC 429 data. List/FIFO buffering is typical.

*   **`addon_429_ListRcvCreate(handle, channelNum, count, configVal)`** (Required for ListDataRd)
    *   **Purpose:** Creates a receive list buffer.
    *   **BTI Equivalent:** `BTI429_ListRcvCreate(uint listconfigval, int count, uint msgaddr, nint handleval)` (Note: `msgaddr` might be 0 for async lists, check manual).
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `channelNum` (int): The receive channel number.
        *   `count` (int): The size of the list buffer in words.
        *   `configVal` (int): List configuration flags (e.g., `LISTCRT429_FIFO`, `LISTCRT429_CIRCULAR`).
    *   **Returns:** An object containing:
        *   `status` (int): BTI status code.
        *   `listAddr` (int): The address/identifier of the created list buffer.

*   **`addon_429_ListDataRd(handle, listAddr)`** (Assuming List/FIFO mode)
    *   **Purpose:** Reads a single ARINC 429 word from a receive list buffer.
    *   **BTI Equivalent:** `BTI429_ListDataRd(uint listaddr, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `listAddr` (int): The address/identifier of the receive list buffer (obtained via `ListRcvCreate` or similar - might need an addon function for list creation).
    *   **Returns:** An object containing:
        *   `status` (int): BTI status code (e.g., `ERR_UNDERFLOW` if empty).
        *   `value` (int): The 32-bit ARINC 429 word received, or an indicator of no data.

*   **`addon_429_ListDataBlkRd(handle, listAddr, maxCount)`** (Alternative)
    *   **Purpose:** Reads a block of ARINC 429 words from a receive list buffer.
    *   **BTI Equivalent:** `BTI429_ListDataBlkRd(ref uint[] listbuf, int count, uint listaddr, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `listAddr` (int): The address/identifier of the receive list buffer.
        *   `maxCount` (int): The maximum number of words to read.
    *   **Returns:** An object containing:
        *   `status` (int): BTI status code.
        *   `dataArray` (array of numbers): An array containing the received 32-bit ARINC 429 words (up to `maxCount`).
    *   **Notes:** The C++ function needs to allocate a buffer, call the BTI function, and convert the result to a JS array.

*   **`addon_429_ListStatus(handle, listAddr)`**
    *   **Purpose:** Checks the status of a list buffer (e.g., empty, full, partial).
    *   **BTI Equivalent:** `BTI429_ListStatus(uint listaddr, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `listAddr` (int): The address/identifier of the list buffer.
    *   **Returns:** An object containing:
        *   `status` (int): BTI status code (check manual if this returns a general status or the list status directly).
        *   `listStatus` (int): The status of the list (e.g., `STAT_EMPTY`, `STAT_PARTIAL`, `STAT_FULL`). Use constants from the manual/header.

### 5. ARINC 429 Filtering (`BTI429`)

Functions to control which labels are received.

*   **`addon_429_FilterSet(handle, channelNum, label, sdiMask, configVal)`**
    *   **Purpose:** Configures the receive filter for a specific label/SDI combination.
    *   **BTI Equivalent:** `BTI429_FilterSet(uint configval, int labelval, int sdimask, int channum, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `channelNum` (int): The receive channel number.
        *   `label` (int): The ARINC 429 label (octal or decimal as per manual).
        *   `sdiMask` (int): Mask specifying which SDIs to match (e.g., `SDIALL`, `SDI00 | SDI11`).
        *   `configVal` (int): Filter configuration (e.g., enable/disable logging, sequencing - check `MSGCRT429_*` constants).
    *   **Returns:**
        *   `status` (int): BTI status code (check manual, might return filter address or status).

*   **`addon_429_FilterDefault(handle, channelNum, configVal)`**
    *   **Purpose:** Sets the default filter behavior for a channel (e.g., receive all labels).
    *   **BTI Equivalent:** `BTI429_FilterDefault(uint configval, int channum, nint handleval)`
    *   **Parameters:**
        *   `handle` (number/pointer): The device handle.
        *   `channelNum` (int): The receive channel number.
        *   `configVal` (int): Default filter configuration.
    *   **Returns:**
        *   `status` (int): BTI status code (check manual, might return filter address or status).

## Implementation Notes

*   **DLL Loading:** Ensure the C++ addon correctly loads the appropriate BTI DLLs (`BTICARD.dll`/`BTICARD64.dll`, `BTI429.dll`/`BTI42964.dll`) based on the system architecture (32-bit/64-bit). Use `LoadLibrary` and `GetProcAddress` on Windows.
*   **Asynchronous Operations:** For receive operations, consider using asynchronous patterns (e.g., `Napi::AsyncWorker`) in the C++ addon to avoid blocking the Node.js event loop while waiting for data. Polling `ListStatus` and `ListDataRd` within an `AsyncWorker` is a common approach.
*   **Constants:** Define the necessary BTI constants (like `CHCFG429_*`, `MSGCRT429_*`, `SDI*`, `STAT_*`, error codes) in your C++ code, mirroring those in the BTI headers/manual.
*   **Resource Management:** Ensure proper cleanup. Close card handles (`CardClose`) when the addon is unloaded or the application exits. Free any allocated list buffers if applicable (check manual for list buffer management functions like `CmdClear` or heap functions if used).
*   **Node-API (N-API):** Use N-API for creating the addon to ensure ABI stability across Node.js versions.

Refer closely to the **MA132 ARINC 429 Programming Manual** for the exact function signatures, parameter details, required constants, and return code meanings. 