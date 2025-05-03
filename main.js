const { app, BrowserWindow, ipcMain } = require('electron/main')
const path = require('path')
// const { execFile } = require('child_process') // No longer needed for this test

// --- Load the Native Addon ---
const bindings = require('bindings');
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

  let cardHandle = null; // Variable to store the card handle
  let coreHandle = null; // Variable to store the core handle

  try {
    // --- Step 1: Open the Card ---
    const cardNumToOpen = 0; 
    console.log(`Calling btiAddon.cardOpen with card number: ${cardNumToOpen}`);
    const openResult = btiAddon.cardOpen(cardNumToOpen);
    console.log(`btiAddon.cardOpen returned:`, openResult);
    finalResults.resultCode = openResult.resultCode;

    if (!openResult.success || openResult.handle === null) {
      finalResults.message = `Failed to open card: ${openResult.message} (Code: ${openResult.resultCode})`;
      throw new Error(finalResults.message); // Use throw to trigger finally block for cleanup
    }
    cardHandle = openResult.handle;
    console.log(`Card opened successfully. Handle: ${cardHandle}`);

    // --- Step 2: Open the Core ---
    const coreNumToOpen = 0;
    console.log(`Calling btiAddon.coreOpen for core ${coreNumToOpen} with card handle: ${cardHandle}`);
    const coreOpenResult = btiAddon.coreOpen(coreNumToOpen, cardHandle);
    console.log(`btiAddon.coreOpen returned:`, coreOpenResult);
    finalResults.resultCode = coreOpenResult.resultCode;

    if (!coreOpenResult.success || coreOpenResult.handle === null) {
      finalResults.message = `Failed to open core: ${coreOpenResult.message} (Code: ${coreOpenResult.resultCode})`;
      throw new Error(finalResults.message); // Use throw for cleanup
    }
    coreHandle = coreOpenResult.handle;
    console.log(`Core opened successfully. Handle: ${coreHandle}`);

    // --- Step 3: Run Card Test (Level 1) ---
    const testLevel = 1; // Memory Interface Test
    console.log(`Calling btiAddon.cardTest (Level ${testLevel}) with core handle: ${coreHandle}`);
    const testResult = btiAddon.cardTest(testLevel, coreHandle);
    finalResults.resultCode = testResult;
    console.log(`btiAddon.cardTest returned: ${testResult}`);

    if (testResult === 0) { // Assuming 0 is ERR_NONE for success
      finalResults.success = true;
      finalResults.message = `Card/Core Opened. Card Test Level ${testLevel} successful!`;
    } else {
      finalResults.success = false;
      // TODO: Could wrap/call BTICard_ErrDescStr here for a better message
      finalResults.message = `Card/Core Opened. Card Test Level ${testLevel} failed with error code: ${testResult}`;
    }

  } catch (error) {
    // Catch errors during any native function call or explicit throws
    console.error("Error during BTI operation:", error.message);
    finalResults.success = false;
    // Use the message set before throwing, or the generic error message
    finalResults.message = error.message.startsWith('Failed to open') ? error.message : `Error executing native function: ${error.message}`;
    // Invalidate handles if error occurred after they were obtained
    if (error.message.startsWith('Failed to open core')) coreHandle = null;
    if (error.message.startsWith('Failed to open card')) cardHandle = null;

  } finally {
    // --- Step 4: Close the Card (Always attempt if handle was obtained) ---
    if (cardHandle !== null) {
      console.log(`Attempting to close card handle ${cardHandle}`);
      try {
        const closeResult = btiAddon.cardClose(cardHandle);
        console.log(`btiAddon.cardClose returned: ${closeResult}`);
        if (closeResult !== 0) {
           // Append warning if close failed, but don't overwrite main success/failure
           finalResults.message += ` (Warning: CardClose failed with code ${closeResult})`; 
        }
      } catch (closeErr) {
         console.error("Error calling btiAddon.cardClose:", closeErr);
         finalResults.message += ` (Error during CardClose: ${closeErr.message})`;
      }
    } else {
        console.log("Card handle was null, skipping close.");
    }
  }

  console.log("Sending final results back to renderer:", finalResults);
  event.sender.send('test-results', finalResults);
});
// --- End IPC Listener Modification ---