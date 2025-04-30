const { app, BrowserWindow, ipcMain } = require('electron/main')
const path = require('path')
const { execFile } = require('child_process')

const createWindow = () => {
  const win = new BrowserWindow({
    width: 800,
    height: 600,
    webPreferences: {
      preload: path.join(__dirname, 'preload.js')
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

// Added: Function to parse the C# output
const parseTestOutput = (stdout) => {
  const lines = stdout.trim().split(/\r?\n/); // Split into lines, handling Windows/Unix endings
  const results = {
    deviceInfo: { type: null, product: null },
    tests: [],
    warnings: [],
    errors: [], // Parsing errors
    raw: stdout // Keep raw output for debugging/fallback
  };

  // Regex for test results (captures name and status)
  const testResultRegex = /^(.+?): (PASSED|FAILED.*)$/;

  lines.forEach(line => {
    line = line.trim(); // Trim whitespace
    if (!line) return; // Skip empty lines

    if (line.startsWith('Card Type:')) {
      results.deviceInfo.type = line.substring('Card Type:'.length).trim();
    } else if (line.startsWith('Product:')) {
      results.deviceInfo.product = line.substring('Product:'.length).trim();
    } else if (line.startsWith('Warning:')) {
      results.warnings.push(line);
    } else {
      const match = line.match(testResultRegex);
      if (match) {
        results.tests.push({ name: match[1].trim(), status: match[2].trim() });
      }
      // Add other parsing logic here if needed (e.g., Driver Info)
    }
  });

  return results;
};

// Modified: IPC Listener to run the test executable and parse output
ipcMain.on('run-test', (event) => {
  const executablePath = path.join(__dirname, 'BTICardTest', 'BTICardTest', 'bin', 'Debug', 'net6.0', 'BTICardTest.exe');

  console.log(`Attempting to run: ${executablePath}`);

  execFile(executablePath, (error, stdout, stderr) => {
    if (error) {
      console.error(`execFile error: ${error}`);
      // Send error details back as an object
      event.sender.send('test-results', {
        error: `Error executing file: ${error.message}`,
        stderr: stderr,
        stdout: stdout // Include stdout even on error if available
      });
      return;
    }

    console.log(`stdout:\n${stdout}`);
    if (stderr) { // Only log stderr if it's not empty
      console.error(`stderr:\n${stderr}`);
    }

    // Parse the stdout
    const parsedResults = parseTestOutput(stdout);
    parsedResults.stderr = stderr; // Add stderr to the results object

    console.log('Parsed Results:', JSON.stringify(parsedResults, null, 2));

    // Send the parsed object back to the renderer process
    event.sender.send('test-results', parsedResults);
  });
});