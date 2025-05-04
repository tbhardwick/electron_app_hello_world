const { app, BrowserWindow, ipcMain } = require('electron/main')
const path = require('path')
// const { execFile } = require('child_process') // No longer needed for this test
const bindings = require('bindings');

// --- BTI Constants (Verify values from BTI429.H) ---
const BTI429_CHCFG429_AUTOSPEED = 0x0004; // Replace with actual value if different
const BTI429_CHCFG429_HIT = 0x0400;       // Replace with actual value if different
const MSGCRT429_DEFAULT = 0;              // Default message creation flag

// --- Pre-calculate config flags --- 
const ARINC_CONFIG_FLAGS = BTI429_CHCFG429_AUTOSPEED | BTI429_CHCFG429_HIT;
const FILTER_CONFIG_FLAGS = MSGCRT429_DEFAULT;

// Global handles - Initialized to null
let gCardHandle = null;
let gCoreHandle = null;

// --- Load the Native Addon ---
let btiAddon;
let addonLoadError = null;

try {
  btiAddon = bindings({ 
    bindings: 'bti_addon', 
    module_root: path.join(__dirname, 'cpp-addon') 
  }); 
  console.log("BTI Addon loaded successfully in main process.");
} catch (error) {
  console.error("Failed to load bti_addon in main process:", error);
  addonLoadError = error; 
  btiAddon = null; 
}
// --- End Addon Loading ---


const createWindow = () => {
  const win = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      preload: path.join(__dirname, 'preload.js'),
      // Recommended for security: disable nodeIntegration and enable contextIsolation
      // nodeIntegration: false, // Default in Electron 12+
      // contextIsolation: true, // Default in Electron 12+
    }
  })

  win.loadFile('index.html')
}

app.whenReady().then(() => {
  createWindow()

  app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
      createWindow()
    }
  })
})

app.on('window-all-closed', () => {
  // --- Clean up BTI handles on exit ---
  if (gCoreHandle !== null) {
    // CoreClose doesn't exist, we only close the card
    // console.log(`Attempting to close core handle ${gCoreHandle}`);
    // try { 
    //     // Need BTICard_CoreClose if it exists, otherwise skip
    // } catch (closeErr) { /* Handle error */ }
  }
  if (gCardHandle !== null) {
      console.log(`Attempting to close card handle ${gCardHandle} on window close.`);
      try {
          if (btiAddon && typeof btiAddon.cardClose === 'function') {
              const closeResult = btiAddon.cardClose(gCardHandle);
              console.log(`btiAddon.cardClose on exit returned: ${closeResult}`);
              if (closeResult !== 0) {
                  // Log error but continue quitting
                  console.error(`CardClose failed on exit (Code: ${closeResult})`);
              }
          } else {
              console.warn("btiAddon or cardClose function not available on exit.");
          }
      } catch (closeErr) {
         console.error("Error calling btiAddon.cardClose on exit:", closeErr);
      }
      gCardHandle = null; // Ensure handle is marked as closed
      gCoreHandle = null; // Also nullify core handle if card is closed
  }
  // --- End BTI Cleanup ---

  if (process.platform !== 'darwin') {
    app.quit()
  }
})

// --- IPC Listener Modified to Use Native Addon ---
ipcMain.on('run-test', (event) => {
  console.log("Received 'run-test' IPC message.");

  // Initialize results
  let finalResults = {
    success: false,
    message: 'Test not fully executed.',
    resultCode: null 
  };

  // Check if the addon loaded correctly
  if (!btiAddon) {
    console.error("BTI Addon not loaded, cannot run test.");
    finalResults.message = `Failed to run test: Addon not loaded. ${addonLoadError?.message || ''}`;
    event.sender.send('test-results', finalResults);
    return;
  }

  // Use global handles defined outside this handler
  let tempCardHandle = null; 
  let tempCoreHandle = null;

  try {
    // --- Step 1: Open the Card (if not already open) ---
    if (gCardHandle === null) {
        const cardNumToOpen = 0; 
        console.log(`Calling btiAddon.cardOpen with card number: ${cardNumToOpen}`);
        const openResult = btiAddon.cardOpen(cardNumToOpen);
        console.log(`btiAddon.cardOpen returned:`, openResult);
        finalResults.resultCode = openResult.resultCode;

        if (!openResult.success || openResult.handle === null) {
          let errorDesc = "Unknown error";
          try {
            errorDesc = btiAddon.getErrorDescription(openResult.resultCode, null);
          } catch (descErr) { console.error("Failed to get error description for cardOpen", descErr); }
          finalResults.message = `Failed to open card: ${errorDesc} (Code: ${openResult.resultCode})`;
          throw new Error(finalResults.message);
        }
        tempCardHandle = openResult.handle; // Assign to temp handle first
        console.log(`Card opened successfully. Temporary Handle: ${tempCardHandle}`);
    } else {
        console.log(`Card already open with handle: ${gCardHandle}`);
        tempCardHandle = gCardHandle; // Use existing global handle
    }

    // --- Step 2: Open the Core (if not already open) ---
    if (gCoreHandle === null) {
        const coreNumToOpen = 0;
        console.log(`Calling btiAddon.coreOpen for core ${coreNumToOpen} with card handle: ${tempCardHandle}`);
        const coreOpenResult = btiAddon.coreOpen(coreNumToOpen, tempCardHandle);
        console.log(`btiAddon.coreOpen returned:`, coreOpenResult);
        finalResults.resultCode = coreOpenResult.resultCode;

        if (!coreOpenResult.success || coreOpenResult.handle === null) {
          let errorDesc = "Unknown error";
          try {
            errorDesc = btiAddon.getErrorDescription(coreOpenResult.resultCode, null);
          } catch (descErr) { console.error("Failed to get error description for coreOpen", descErr); }
          finalResults.message = `Failed to open core: ${errorDesc} (Code: ${coreOpenResult.resultCode})`;
          throw new Error(finalResults.message); // Use throw for cleanup logic
        }
        tempCoreHandle = coreOpenResult.handle; // Assign to temp handle first
        console.log(`Core opened successfully. Temporary Handle: ${tempCoreHandle}`);
    } else {
        console.log(`Core already open with handle: ${gCoreHandle}`);
        tempCoreHandle = gCoreHandle; // Use existing global handle
    }

    // Assign to global handles ONLY after both open successfully
    gCardHandle = tempCardHandle;
    gCoreHandle = tempCoreHandle;
    console.log(`Global handles assigned: Card=${gCardHandle}, Core=${gCoreHandle}`);

    // --- REMOVED Reset the Core --- 
    // console.log(`Calling btiAddon.cardReset with core handle: ${gCoreHandle}`);
    // const resetResult = btiAddon.cardReset(gCoreHandle); 
    // console.log("Core reset successful (assumed).");
    // --- End Core Reset ---

    // --- REMOVED extDIORd(0) Call --- 
    // console.log(`Calling btiAddon.extDIORd(0) with core handle: ${gCoreHandle}`);
    // const initDioResult = btiAddon.extDIORd(0, gCoreHandle);
    // console.log(`btiAddon.extDIORd(0) returned:`, initDioResult);
    // if (!initDioResult || initDioResult.status !== 0) {
    //     // ... error handling ...
    // }
    // console.log("Initial DIO read(0) successful.");
    // --- END REMOVED Call ---

    // --- REMOVED: Configure ARINC Channel 0 --- 
    // const channelNum = 0; 
    // const configFlags = ARINC_CONFIG_FLAGS;
    // console.log(`Calling btiAddon.chConfig for channel ${channelNum} with flags ${configFlags}`);
    // const chConfigResult = btiAddon.chConfig(configFlags, channelNum, gCoreHandle);
    // console.log(`btiAddon.chConfig returned: ${chConfigResult}`);
    // if (chConfigResult !== 0) { 
    //     // ... error handling ...
    // }
    // console.log(`ARINC Channel ${channelNum} configured successfully.`);

    // --- REMOVED: Set Default Filter for Channel 0 --- 
    // const filterConfigFlags = FILTER_CONFIG_FLAGS;
    // console.log(`Calling btiAddon.filterDefault for channel ${channelNum}`);
    // const filterResult = btiAddon.filterDefault(filterConfigFlags, channelNum, gCoreHandle);
    // console.log(`btiAddon.filterDefault returned:`, filterResult);
    // if (!filterResult || filterResult.status !== 0) {
    //     // ... error handling ...
    // }
    // console.log(`ARINC Channel ${channelNum} default filter set successfully (Addr: ${filterResult.filterAddr}).`);
    // --- End ARINC Config ---

    // --- REMOVED: Start the card --- 
    // console.log(`Calling btiAddon.cardStart with core handle: ${gCoreHandle}`);
    // const startResult = btiAddon.cardStart(gCoreHandle);
    // console.log(`btiAddon.cardStart returned: ${startResult}`);
    // if (startResult !== 0) { 
    //     // ... error handling ...
    // }
    // console.log("Card started successfully.");
    // --- End Card Start ---

    // --- Assume success if Card Open/Core Open passed ---
    finalResults.success = true;
    finalResults.message = `Card/Core Opened.`; // Minimal success message
    finalResults.resultCode = 0; // Indicate success

  } catch (error) {
    console.error("Error during BTI operation:", error.message);
    finalResults.success = false;
    finalResults.message = error.message.startsWith('Failed to open') || error.message.startsWith('Card/Core Opened. Card Test') ? error.message : `Error executing native function: ${error.message}`;
    // Don't reset global handles on error here, let subsequent calls fail if needed
    // or implement specific error handling logic.

  } finally {
    // --- REMOVED Step 4: Close the Card --- 
    // Handles are now global and closed on window-all-closed
    // console.log("Skipping card close in run-test handler; using global handles.");
  }

  console.log("Sending final results back to renderer:", finalResults);
  event.sender.send('test-results', finalResults);
});
// --- End IPC Listener Modification ---

// --- REMOVED Handler for Individual Discrete Input Reading ---
// ipcMain.handle('bti:extDIORd', async (event, dionum) => {
//     // ... implementation ...
// });
// --- END Removed Handler ---

// --- ADD Handler for Getting All DIO States ---
ipcMain.handle('bti:getAllDioStates', async (event) => {
    console.log(">>> IPC Handler Entered for getAllDioStates");
    // Check if addon and function exist
    if (!btiAddon || typeof btiAddon.getAllDioStates !== 'function') {
        console.error('BTI Addon or getAllDioStates function not available.');
        throw new Error('BTI Addon not loaded or function missing.');
    }
    // Check if core handle is valid 
    if (gCoreHandle === null) {
        console.error('Core handle is null. Cannot read DIOs. Run test first?');
        throw new Error('Device core not opened. Please run the test first.');
    }

    try {
        // Call the C++ addon function (takes only core handle)
        const resultsArray = btiAddon.getAllDioStates(gCoreHandle);
        console.log(`btiAddon.getAllDioStates(${gCoreHandle}) returned:`, resultsArray);
        
        // The C++ wrapper should return the array directly if successful
        // Add checks if the wrapper returns status/error object instead
        if (!Array.isArray(resultsArray)) {
             console.error('getAllDioStates did not return an array:', resultsArray);
             throw new Error('Failed to get DIO states: Invalid response from addon.');
        }

        return resultsArray; // Return the array of result objects

    } catch (error) {
        console.error(`Exception calling btiAddon.getAllDioStates:`, error);
        // Re-throw the error to be caught by preload.js
        throw error; 
    }
});
// --- END Get All DIO States Handler ---

// --- IPC Handlers for ARINC Receiver Initialization, Start/Stop Monitoring, and Data/Error Forwarding ---

// Handle DIO refresh request
ipcMain.handle('get-all-dio-states', async (event, hCore) => {
  if (!btiAddon) {
    throw new Error('Addon not loaded');
  }
  if (!hCore) {
      throw new Error('Invalid core handle provided for DIO read.');
  }
  try {
    const hCoreNumber = BigInt(hCore); // Convert handle back to BigInt/Number as needed by addon
    // console.log('main requesting getAllDioStates with hCore:', hCoreNumber);
    const result = btiAddon.getAllDioStates(hCoreNumber); // Assuming handle passed as Number/BigInt
    // console.log('main received getAllDioStates result:', result);
    return result;
  } catch (error) {
    console.error('Error calling getAllDioStates in addon:', error);
    throw error; // Re-throw to be caught in renderer
  }
});

// Handle Hardware Initialization
ipcMain.handle('initialize-hardware', async () => {
  if (!btiAddon) {
    throw new Error('Addon not loaded');
  }
  try {
    console.log('Calling addon.initializeHardware...');
    const result = btiAddon.initializeHardware(); // Assuming this is synchronous for now
    console.log('Addon initializeHardware result:', result);
    // if (result.success) {
    //   // Store handles globally if needed, though passing hCore around might be better
    //   // globalHCard = result.hCard;
    //   // globalHCore = result.hCore;
    // }
    return result;
  } catch (error) {
    console.error('Error calling initializeHardware in addon:', error);
    throw error;
  }
});

// Handle ARINC Receiver Initialization
ipcMain.handle('initialize-receiver', async (event, hCore) => {
    if (!btiAddon) throw new Error('Addon not loaded');
    if (!hCore) throw new Error('Invalid core handle for receiver initialization.');
    try {
        console.log('Calling addon.initializeReceiver with hCore:', hCore);
        // Define callbacks for the addon to send data back
        const onDataUpdate = (dataBatch) => {
            if (gCoreHandle && typeof btiAddon.sendARINCData === 'function') {
                // console.log(`Forwarding ${dataBatch?.length} ARINC updates to renderer.`);
                btiAddon.sendARINCData(gCoreHandle, dataBatch);
            }
        };
        const onErrorUpdate = (errorInfo) => {
            if (gCoreHandle && typeof btiAddon.sendARINCError === 'function') {
                console.error('Forwarding ARINC error to renderer:', errorInfo);
                btiAddon.sendARINCError(gCoreHandle, errorInfo);
            }
        };

        // Pass handle and callbacks to the addon function
        const result = btiAddon.initializeReceiver(BigInt(hCore), onDataUpdate, onErrorUpdate);
        console.log('Addon initializeReceiver result:', result);
        return result;
    } catch (error) {
        console.error('Error calling initializeReceiver in addon:', error);
        throw error;
    }
});

// Handle Start ARINC Monitoring
ipcMain.handle('start-arinc-monitoring', async (event, hCore) => {
    if (!btiAddon) throw new Error('Addon not loaded');
    if (!hCore) throw new Error('Invalid core handle for starting monitoring.');
    try {
        console.log('Calling addon.startMonitoring with hCore:', hCore);
        const result = btiAddon.startMonitoring(BigInt(hCore));
        console.log('Addon startMonitoring result:', result);
        return result;
    } catch (error) {
        console.error('Error calling startMonitoring in addon:', error);
        throw error;
    }
});

// Handle Stop ARINC Monitoring
ipcMain.handle('stop-arinc-monitoring', async (event, hCore) => {
    if (!btiAddon) {
        console.warn('Attempted to stop monitoring, but addon not loaded.');
        return { success: true, message: 'Addon not loaded, assuming stopped.' }; // Graceful handling
    }
    if (!hCore) {
        console.warn('Attempted to stop monitoring with invalid core handle.');
         return { success: false, message: 'Invalid core handle.' };
    }
    try {
        console.log('Calling addon.stopMonitoring with hCore:', hCore);
        const result = btiAddon.stopMonitoring(BigInt(hCore));
        console.log('Addon stopMonitoring result:', result);
        return result;
    } catch (error) {
        console.error('Error calling stopMonitoring in addon:', error);
        return { success: false, message: error.message };
    }
});
// --- End IPC Handlers for ARINC Receiver Initialization, Start/Stop Monitoring, and Data/Error Forwarding ---