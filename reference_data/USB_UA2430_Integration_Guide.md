# Programming Guide: Ballard UA2430 USB ARINC 429 Interface

This guide provides a concise overview and best practices for developing applications to interact with the Ballard UA2430 USB ARINC 429 interface using the MA132 library (part of the BTIDriver API). For comprehensive details, always refer to the full **MA132 ARINC 429 Programming Manual**.

## Core Concepts

*   **BTIDriver API:** The unified library used to control Ballard hardware. Functions are prefixed with `BTICard_` (protocol-independent) or `BTI429_` (ARINC 429 specific).
*   **Handles (Card & Core):** Software uses handles to identify and interact with the hardware.
    *   `hCard` (Card Handle): Identifies the specific UA2430 device. Obtained via `BTICard_CardOpen`.
    *   `hCore` (Core Handle): Identifies a specific processing core on the device. The UA2430 typically has one core (core number 0). Obtained via `BTICard_CoreOpen`. **Best Practice:** Always use the `hCore` for functions operating on the core's resources (channels, messages, etc.), even for single-core devices.
*   **Channels:** Represent physical ARINC 429 transmit (Tx) or receive (Rx) interfaces. Identified by constants like `CH0`, `CH1`, etc.
*   **Message Records:** On-device memory structures holding a single 32-bit ARINC 429 word and associated metadata (activity, time-tag, error flags, etc.). Used for both Tx and Rx buffering.
*   **Filter Tables (Rx):** Arrays (one per Rx channel) that map incoming messages (by Label/SDI) to specific Message Records. Configured using `BTI429_FilterSet` and `BTI429_FilterDefault`.
*   **Transmit Schedules (Tx):** Sequences of commands (transmit message, insert gap, etc.) stored on the device that control the timing and order of transmissions. Created automatically with `BTI429_SchedBuild` or explicitly with `BTI429_SchedMsg`, `BTI429_SchedGap`, etc.
*   **Sequential Monitor:** Records a time-tagged history of selected Tx/Rx bus activity to an on-device buffer (Sequential Record). Configured via `BTICard_SeqConfig`.

## Basic Programming Workflow

A typical application follows these steps:

1.  **Open Device & Core:**
    *   Call `BTICard_CardOpen` with the device's `cardnum` (often 0 if it's the only Ballard device) to get `hCard`.
    *   Call `BTICard_CoreOpen` with `hCard` and `corenum` (usually 0 for UA2430) to get `hCore`.
    *   **Best Practice:** Always check return values for errors.

2.  **Reset Core (Recommended):**
    *   Call `BTICard_CardReset(hCore)` to ensure a clean starting state.

3.  **Configure Channels:**
    *   **Rx:** Call `BTI429_ChConfig` for each receive channel, setting speed (e.g., `CHCFG429_AUTOSPEED`) and other options.
    *   **Tx:** Call `BTI429_ChConfig` for each transmit channel (e.g., `CHCFG429_HIGHSPEED`).
    *   **Filters (Rx):** Use `BTI429_FilterSet` to map specific Label/SDI combinations to Message Records. Optionally use `BTI429_FilterDefault` *before* `BTI429_FilterSet` to catch all other messages. Store the returned `MSGADDR`.
    *   **Messages (Tx):** Use `BTI429_MsgCreate` to allocate Message Records for transmission. Store the returned `MSGADDR`.
    *   **Schedule (Tx):** Use `BTI429_SchedBuild` (recommended for standard periodic transmission) or explicit `BTI429_SchedMsg`/`BTI429_SchedGap` calls to define the transmit sequence.
    *   **Sequential Monitor (Optional):** Call `BTICard_SeqConfig` to enable and configure monitoring. Use flags in `BTI429_ChConfig`, `BTI429_FilterSet`, or `BTI429_MsgCreate` (e.g., `CHCFG429_SEQALL`, `MSGCRT429_SEQ`) to select what gets recorded.

4.  **Initialize Data (Tx):**
    *   Before starting, write initial data values to transmit Message Records using `BTI429_MsgDataWr`. This prevents transmitting garbage data.

5.  **Activate Core:**
    *   Call `BTICard_CardStart(hCore)` to begin hardware operation (Tx and Rx).

6.  **Handle Data (Runtime Loop):**
    *   **Rx:** Read received data from Message Records using `BTI429_MsgDataRd(msg_addr, hCore)`. Check activity/error flags using `BTI429_MsgBlockRd` or `BTI429_MsgIsAccessed`.
    *   **Tx:** Update data in transmit Message Records using `BTI429_MsgDataWr(new_data, msg_addr, hCore)`. The hardware automatically uses the latest data based on the schedule.
    *   **Monitor:** Read buffered monitor data using `BTICard_SeqRd` (single) or `BTICard_SeqBlkRd`/`BTICard_SeqCommRd` (multiple). Parse records using `BTICard_SeqFindInit` and `BTICard_SeqFindNext429` (or other protocol-specific find functions).
    *   **Best Practice:** Poll efficiently or use event logging/interrupts (refer to manual sections 3.10, 5.1) for more advanced data handling.

7.  **Deactivate Core:**
    *   Call `BTICard_CardStop(hCore)` when finished interacting with the bus.

8.  **Close Device:**
    *   **Crucial:** Call `BTICard_CardClose(hCard)` before the application terminates to release hardware resources correctly. Failure to do so can lead to system instability.

## Example Snippets (Conceptual)

**Receiver Setup:**

```c
// ... (Open Card/Core) ...
BTI429_ChConfig(CHCFG429_AUTOSPEED, CH0, hCore); // Configure Rx Ch 0
MSGADDR defaultAddr = BTI429_FilterDefault(MSGCRT429_DEFAULT, CH0, hCore);
MSGADDR latAddr = BTI429_FilterSet(MSGCRT429_DEFAULT | MSGCRT429_SEQ, 0310, SDIALL, CH0, hCore);
MSGADDR lonAddr = BTI429_FilterSet(MSGCRT429_DEFAULT | MSGCRT429_SEQ, 0311, SDIALL, CH0, hCore);
// ... (Start Card) ...
// Runtime:
ULONG latData = BTI429_MsgDataRd(latAddr, hCore);
ULONG lonData = BTI429_MsgDataRd(lonAddr, hCore);
```

**Transmitter Setup:**

```c
// ... (Open Card/Core) ...
BTI429_ChConfig(CHCFG429_HIGHSPEED, CH4, hCore); // Configure Tx Ch 4
MSGADDR airspeedAddr = BTI429_MsgCreate(MSGCRT429_DEFAULT, hCore);
MSGADDR tempAddr = BTI429_MsgCreate(MSGCRT429_DEFAULT, hCore);
// Setup arrays for SchedBuild (addresses, min/max intervals in ms)
MSGADDR addrs[] = {airspeedAddr, tempAddr};
INT mins[] = {60, 240};
INT maxs[] = {120, 500};
BTI429_SchedBuild(2, addrs, mins, maxs, CH4, hCore);
// Initialize data BEFORE starting
BTI429_MsgDataWr(BTI429_FldPutLabel(0, 0206), airspeedAddr, hCore); // Label 206, data 0
BTI429_MsgDataWr(BTI429_FldPutLabel(0, 0211), tempAddr, hCore);     // Label 211, data 0
// ... (Start Card) ...
// Runtime:
ULONG newAirspeedData = ...; // Calculate new data
BTI429_MsgDataWr(newAirspeedData, airspeedAddr, hCore); // Update data
```

## Using the C++ Addon Wrapper

This `electron_app_hello_world` project includes a C++ addon located at `cpp-addon/src/addon.cpp`. This addon serves as a wrapper around the MA132 C library functions.

**Purpose:**

*   The primary goal of the addon is to expose the necessary MA132 `BTI429_` and `BTICard_` functions to the Node.js runtime environment used by Electron.
*   This allows the JavaScript parts of the application (e.g., `main.js`, `renderer.js`) to interact with the UA2430 hardware without directly calling C functions.
*   The addon handles the complexities of calling the C library, managing data types (marshalling), and potentially handling asynchronous operations or callbacks needed for Node.js integration.

**How to Use:**

*   Instead of calling the C functions like `BTICard_CardOpen` directly, your JavaScript code will call corresponding functions exported by the C++ addon.
*   The fundamental workflow and concepts (opening the device, configuring channels, reading/writing data, closing the device) remain the same as described in the "Basic Programming Workflow" section above.
*   However, the exact function names, parameters, and return value handling (e.g., using Promises or callbacks for asynchronous operations) will depend on how the addon is implemented.

**Best Practice:**

*   Refer to the `cpp-addon/src/addon.cpp` file to see which MA132 functions are exposed and how they are wrapped.
*   Examine the JavaScript files (`main.js`, `renderer.js`, or other relevant modules) that interact with the addon to understand the specific API and usage patterns provided by the wrapper.
*   While the addon simplifies interaction from JavaScript, understanding the underlying MA132 concepts from this guide and the full manual is still essential for effective development and debugging.

## Further Information

This guide covers the basics. For advanced features like List Buffers, Playback Mode, Error Injection, IRIG timing, interrupt handling, multi-protocol operation, and detailed function parameters/flags, consult the **MA132 ARINC 429 Programming Manual (MA132_ARINC_429_Programming_Manual_Rev_7.md)**. 