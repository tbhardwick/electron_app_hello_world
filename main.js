const { app, BrowserWindow, ipcMain } = require('electron/main')
const path = require('path')
const { execFile } = require('child_process')

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

// IPC Listener updated to run the executable with arguments and parse JSON output
ipcMain.on('run-test', (event) => {
  // Define arguments for the C# executable
  const args = ['--action', 'testCard']; // Add more args here if needed in the future
  // Construct the path to the executable within the workspace
  const executablePath = path.join(__dirname, 'bin', 'Debug', 'net6.0', 'BTICardTest.exe');

  console.log(`Attempting to run: ${executablePath} ${args.join(' ')}`);

  let fullStdout = '';
  let fullStderr = '';

  const child = execFile(executablePath, args);

  child.stdout.on('data', (data) => {
    fullStdout += data;
  });

  child.stderr.on('data', (data) => {
    fullStderr += data;
    console.error(`stderr chunk: ${data}`);
  });

  child.on('close', (code) => {
    console.log(`C# executable exited with code ${code}`);
    console.log(`Full stdout:\n${fullStdout}`);
    if (fullStderr) {
      console.error(`Full stderr:\n${fullStderr}`);
    }

    const resultsData = {
      executionError: null,
      parseError: null,
      csOutput: null, // This will hold the parsed JSON object from C#
      stderr: fullStderr || null, // Store stderr, ensure it's null if empty
      rawStdout: fullStdout || null // Store raw stdout
    };

    if (code !== 0) {
      resultsData.executionError = `C# executable exited with code ${code}. Check stderr for details.`;
    }

    // Attempt to parse the JSON part of stdout
    if (fullStdout) {
      try {
        // Use regex to find the largest JSON block starting with { and ending with }
        const jsonMatch = fullStdout.match(/(\{.*\})/s); // Use 's' flag for dotAll if needed, find largest block
        
        if (jsonMatch && jsonMatch[1]) {
          const jsonString = jsonMatch[1]; // The captured JSON string
          
          // --- Add detailed logging here ---
          console.log(`Attempting to parse JSON substring via Regex (length ${jsonString.length}):`);
          console.log(`>>>${jsonString}<<<`); 
          // --- End detailed logging ---

          resultsData.csOutput = JSON.parse(jsonString); 
          
          if (resultsData.csOutput && resultsData.csOutput.Success === false && !resultsData.executionError) {
            console.warn('C# process reported failure:', resultsData.csOutput.ErrorMessage || 'No specific error message provided.');
            resultsData.executionError = `C# logic reported failure: ${resultsData.csOutput.ErrorMessage || 'Unknown C# error'}`;
          }
        } else {
           // If regex fails, fall back to the brace finding method for logging
           const jsonStartIndex = fullStdout.lastIndexOf('{');
           const jsonEndIndex = fullStdout.lastIndexOf('}');
           throw new Error(`Could not find JSON pattern using regex. Last braces found at start: ${jsonStartIndex}, end: ${jsonEndIndex}.`);
        }
      } catch (parseErr) {
        console.error(`JSON parse error: ${parseErr}`);
        resultsData.parseError = `Failed to parse JSON output from C# executable. ${parseErr.message}`;
      }
    } else {
        console.log('stdout was empty.');
        if (!resultsData.executionError) {
            resultsData.parseError = 'C# executable produced no output.';
            resultsData.executionError = resultsData.parseError; // Treat no output as an error if exit code was 0
        }
    }

    console.log('Sending results to renderer:', JSON.stringify(resultsData, null, 2));

    // Send the structured results object back to the renderer process
    event.sender.send('test-results', resultsData);
  });

  child.on('error', (err) => {
    console.error(`Failed to start subprocess: ${err}`);
    // Send error back immediately if the process couldn't even start
    event.sender.send('test-results', {
      executionError: `Failed to start C# executable: ${err.message}`,
      parseError: null,
      csOutput: null,
      stderr: null,
      rawStdout: null
    });
  });
});