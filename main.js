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

ipcMain.on('run-test', (event) => {
  const executablePath = path.join(__dirname, 'BTICardTest', 'BTICardTest', 'bin', 'Debug', 'net6.0', 'BTICardTest.exe');

  console.log(`Attempting to run: ${executablePath}`);

  execFile(executablePath, (error, stdout, stderr) => {
    if (error) {
      console.error(`execFile error: ${error}`);
      event.sender.send('test-results', `Error executing file: ${error.message}
Stderr: ${stderr}`);
      return;
    }
    
    console.log(`stdout: ${stdout}`);
    console.error(`stderr: ${stderr}`);

    event.sender.send('test-results', `Stdout:
${stdout}
Stderr:
${stderr}`);
  });
});