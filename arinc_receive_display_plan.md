# ARINC 429 Receive Data Display Implementation Plan

This plan outlines the steps to add a feature for displaying ARINC 429 receive data from the UA2430 device within the Electron application. The goal is to show the latest received 32-bit word for each unique label on each of the 8 receive channels, with status indicators showing data freshness.

## 1. UI Layer (`index.html`, `renderer.js`, `styles.css`)

### HTML (`index.html`)

-   **Tab Interface:** Implement a tabbed navigation structure. Add a new tab labeled "ARINC 429 Receive".
-   **Receive Tab Content:**
    -   Create 8 distinct sections (e.g., `div`s) for each receive channel (IDs: `channel-0-data` to `channel-7-data`).
    -   Each section needs:
        -   A heading (e.g., "Channel 0").
        -   A channel status indicator (e.g., `<span id="channel-0-status-indicator">●</span>`).
        -   A table (e.g., `<table id="channel-0-table">`) for displaying label data.
    -   Each table requires columns: "Label (Octal)", "Data Word (Hex)", "Status".
    -   Table rows will be dynamically added via JavaScript, each with a unique ID (e.g., `id="ch0-label-310"`). The "Status" cell will contain the indicator span (`<span class="status-indicator">●</span>`).

### CSS (`styles.css`)

-   Add styles for tab navigation.
-   Style the data tables for readability (borders, padding, alignment).
-   Define status indicator classes:
    -   `.status-indicator.green { color: green; }` (Fresh data)
    -   `.status-indicator.red { color: red; }` (Stale data)
    -   `.status-indicator.grey { color: grey; }` (Label never received/Initial state)
-   Style channel-level status indicators similarly.

### JavaScript (`renderer.js`)

-   **Initialization:**
    -   Implement tab switching logic.
    -   Initialize `arincData = {};` to store received data (channel -> label -> { word, timestamp, element }).
-   **IPC Listeners:**
    -   `window.electronAPI.onArincDataUpdate((updates) => { ... });` to handle batches of data from the main process. Payload: `updates = [{ channel: number, label: number, word: number, timestamp: number }, ...]`.
    -   `window.electronAPI.onArincErrorUpdate((errorInfo) => { ... });` to handle status/error messages. Payload: `errorInfo = { channel: number | null, status: 'OK'|'ERROR'|'TIMEOUT', message?: string }`.
-   **Data Handling (`onArincDataUpdate`):**
    -   Iterate through `updates`.
    -   Ensure channel exists in `arincData`.
    -   Convert label to octal string (`labelOctal`).
    -   Store/update `arincData[channel][labelOctal] = { word, timestamp }`.
    -   Call `renderLabelUpdate(channel, labelOctal)` to update the DOM.
-   **Rendering (`renderLabelUpdate` function):**
    -   Find the correct channel table.
    -   Check if the row for `labelOctal` exists using ID `chN-label-LLL`.
    -   If not, create `<tr>` with ID, `<td>`s for Label, Word, Status (initially grey indicator). Append to table body. Store element reference in `arincData`.
    -   Update the "Data Word" cell with the hex-formatted word.
-   **Staleness Check:**
    -   `setInterval(checkStaleness, 250);`
    -   `checkStaleness` function:
        -   Get `now = Date.now();`.
        -   Iterate through `arincData`.
        -   For each label entry, check if `(now - entry.timestamp) > 500`.
        -   Update the class (`green` or `red`) of the corresponding status indicator span.
-   **Error Display (`onArincErrorUpdate`):**
    -   Update channel-level (or global) status indicators based on `errorInfo.status`.
    -   Potentially display `errorInfo.message` via tooltip or text element.

## 2. Main Process (`main.js`)

-   **Addon Loading:** Load the C++ addon (`bti_addon.node`).
-   **Initialization:**
    -   After renderer loads, call addon functions: `InitializeHardware()` -> `InitializeReceiver()`. Handle results/errors.
-   **Monitoring Control:**
    -   Call addon's `StartMonitoring()`. Pass JS callback functions (wrapped for IPC if needed) for the addon to send data and error updates back.
-   **Data Forwarding:**
    -   Implement the callbacks passed to the addon.
    -   Forward data received from addon to renderer: `mainWindow.webContents.send('arincDataUpdate', dataBatch);`.
    -   Forward errors: `mainWindow.webContents.send('arincErrorUpdate', errorInfo);`.
-   **Cleanup:** On `app.quit`, call addon's `StopMonitoring()` and `CleanupHardware()`.

## 3. C++ Addon (`cpp-addon/src/addon.cpp`)

-   **Data Structures:**
    -   `std::vector<LISTADDR> receiveListAddrs;` (size 8)
    -   `std::map<int, std::map<int, ULONG>> latestWords;` (channel -> label -> word)
    -   `std::map<int, std::map<int, std::chrono::steady_clock::time_point>> lastUpdateTimes;` (channel -> label -> timestamp)
    -   `Napi::ThreadSafeFunction tsfnDataUpdate;`
    -   `Napi::ThreadSafeFunction tsfnErrorUpdate;`
    -   `std::thread monitorThread;`
    -   `std::atomic<bool> monitoringActive;`
    -   `HCARD hCard; HCORE hCore;`
-   **`InitializeHardware` (Exported Function):**
    -   Calls `BTICard_CardOpen`, stores `hCard`.
    -   Calls `BTICard_CoreOpen`, stores `hCore`.
    -   Returns handles and status to JS.
-   **`InitializeReceiver` (Exported Function):**
    -   Requires `hCore`.
    -   Loops 0-7 for channels:
        -   `BTI429_ChConfig(CHCFG429_AUTOSPEED | CHCFG429_LOG, channel, hCore);` // Enable logging for EVENTTYPE_429MSG
        -   `BTI429_ListRcvCreate(LISTCRT429_FIFO | LISTCRT429_LOG, 1024, channel, hCore);` // List not tied to a filter message
        -   Store returned `LISTADDR`.
    -   `BTICard_EventLogConfig(LOGCFG_ENABLE, 1024, hCore);`
    -   Accepts JS callbacks, creates and stores `tsfnDataUpdate`, `tsfnErrorUpdate`.
-   **`StartMonitoring` (Exported Function):**
    -   Requires `hCore`.
    -   Sets `monitoringActive = true;`.
    -   `BTICard_CardStart(hCore);`.
    -   Starts `std::thread` running `MonitorLoop`. Stores thread object.
-   **`StopMonitoring` (Exported Function):**
    -   Sets `monitoringActive = false;`.
    -   Joins `monitorThread`.
    -   `BTICard_CardStop(hCore);`.
    -   Releases `ThreadSafeFunction`s.
-   **`CleanupHardware` (Exported Function):**
    -   `BTICard_CardClose(hCard);`.
-   **`MonitorLoop` (Background Thread):**
    -   `while (monitoringActive)`:
        -   **Event Log Polling:** `BTICard_EventLogRd(...)`. Check if an event occurred. Process `EVENTTYPE_429MSG`, `EVENTTYPE_429LIST`, errors.
        -   **Data Reading (Periodic/Triggered):**
            -   `currentBatchUpdates.clear();`
            -   Loop channels 0-7:
                -   Get `listAddr`.
                -   `BTI429_ListStatus(...)`. Handle errors.
                -   If `STAT_PARTIAL` or `STAT_FULL`:
                    -   `BTI429_ListDataBlkRd(...)` into a temporary buffer (e.g., `std::vector<ULONG>`).
                    -   If successful and words read > 0:
                        -   Process buffer: Get `label` (`BTI429_FldGetLabel`), get current steady_clock time (`now`).
                        -   Compare `word` and `now` with stored `latestWords` and `lastUpdateTimes`.
                        -   If changed/new: Update stores, convert `now` to epoch ms (`timestamp_ms`). Add `{ channel, label, word, timestamp_ms }` to `currentBatchUpdates`.
                    -   Handle read errors.
        -   **Push Updates:** If `!currentBatchUpdates.empty()`, call `tsfnDataUpdate.BlockingCall(...)` with properly marshalled data.
        -   `std::this_thread::sleep_for(std::chrono::milliseconds(10));` // Adjust polling interval.
-   **Timestamp Conversion:** Implement helper to convert `steady_clock::time_point` to epoch milliseconds.
-   **Error Reporting:** Use `tsfnErrorUpdate` to send error details to `main.js`. 