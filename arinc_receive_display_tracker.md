# ARINC 429 Receive Data Display - Progress Tracker

## UI Layer

-   [X] **HTML (`index.html`):**
    -   [X] Implement tab structure.
    -   [X] Add "ARINC 429 Receive" tab.
    -   [X] Create 8 channel sections (`#channel-N-data`).
    -   [X] Add channel headings.
    -   [X] Add channel status indicators (`#channel-N-status-indicator`).
    -   [X] Add data tables (`#channel-N-table`).
    -   [X] Define table columns (Label, Word, Status).
-   [X] **CSS (`styles.css`):**
    -   [X] Style tab navigation.
    -   [X] Style data tables.
    -   [X] Define status indicator classes (`.green`, `.red`, `.grey`).
    -   [X] Style channel status indicators.
-   [X] **JavaScript (`renderer.js`):**
    -   [X] Implement tab switching logic.
    -   [X] Initialize `arincData` structure.
    -   [X] Implement IPC listener `onArincDataUpdate`.
    -   [X] Implement IPC listener `onArincErrorUpdate`.
    -   [X] Implement data handling logic in `onArincDataUpdate`.
    -   [X] Implement `renderLabelUpdate` function (DOM creation/update).
    -   [X] Implement `setInterval` for staleness check.
    -   [X] Implement `checkStaleness` function (update indicator colors).
    -   [X] Implement error display logic in `onArincErrorUpdate`.

## Main Process (`main.js`)

-   [X] Load C++ Addon.
-   [X] Call Addon `InitializeHardware` on renderer ready.
-   [X] Call Addon `InitializeReceiver` on renderer ready.
-   [X] Call Addon `StartMonitoring`, passing callbacks.
-   [X] Implement data forwarding callback (`onArincDataUpdate` -> WebContents).
-   [X] Implement error forwarding callback (`onArincErrorUpdate` -> WebContents).
-   [X] Call Addon `StopMonitoring` on app quit.
-   [X] Call Addon `CleanupHardware` on app quit.

## C++ Addon (`cpp-addon/src/addon.cpp`)

-   [X] Define required data structures (maps, list vector, atomics, handles, ThreadSafeFunctions).
-   [X] Implement `InitializeHardware` function (CardOpen, CoreOpen).
-   [X] Implement `InitializeReceiver` function (ChConfig, ListRcvCreate, EventLogConfig, Store ThreadSafeFunctions).
-   [X] Implement `StartMonitoring` function (Set flag, CardStart, Start thread).
-   [X] Implement `StopMonitoring` function (Set flag, Join thread, CardStop, Release TSFNs).
-   [X] Implement `CleanupHardware` function (CardClose).
-   [X] Implement `MonitorLoop` function (background thread):
    -   [X] Main `while (monitoringActive)` loop.
    -   [X] Event Log polling (`BTICard_EventLogRd`).
    -   [X] Periodic list data reading (`BTI429_ListStatus`, `BTI429_ListDataBlkRd`).
    -   [X] Label extraction (`BTI429_FldGetLabel`).
    -   [X] Timestamp generation (steady_clock -> epoch ms).
    -   [X] Update `latestWords` and `lastUpdateTimes` maps.
    -   [X] Build `currentBatchUpdates` vector.
    -   [X] Call `tsfnDataUpdate` with batched updates.
    -   [X] Implement `std::this_thread::sleep_for`.
-   [X] Implement timestamp conversion utility.
-   [X] Implement error reporting via `tsfnErrorUpdate`. 