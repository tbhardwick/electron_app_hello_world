const { contextBridge, ipcRenderer } = require('electron')

contextBridge.exposeInMainWorld('electronAPI', {
  send: (channel, data) => {
    // whitelist channels
    let validChannels = ['run-test'];
    if (validChannels.includes(channel)) {
      ipcRenderer.send(channel, data);
    }
  },
  on: (channel, func) => {
    let validChannels = ['test-results'];
    if (validChannels.includes(channel)) {
      // Deliberately strip event as it includes `sender` 
      ipcRenderer.on(channel, (event, ...args) => func(...args));
    }
  },
  // --- Expose the NEW getAllDioStates function ---
  getAllDioStates: (hCore) => ipcRenderer.invoke('get-all-dio-states', hCore),
  // Expose hardware initialization
  initializeHardware: () => ipcRenderer.invoke('initialize-hardware'),
  // Expose ARINC receiver initialization
  initializeReceiver: (hCore) => ipcRenderer.invoke('initialize-receiver', hCore),
  // Expose ARINC monitoring start
  startArincMonitoring: (hCore) => ipcRenderer.invoke('start-arinc-monitoring', hCore),
  // Expose ARINC monitoring stop
  stopArincMonitoring: (hCore) => ipcRenderer.invoke('stop-arinc-monitoring', hCore),

  // Listener for ARINC Data Updates from Main
  onArincDataUpdate: (callback) => {
    const channel = 'arincDataUpdate';
    // Remove existing listener for this channel to prevent duplicates
    ipcRenderer.removeAllListeners(channel);
    // Add the new listener
    ipcRenderer.on(channel, (event, ...args) => callback(...args));
    // Return a function to remove this specific listener
    return () => {
      ipcRenderer.removeListener(channel, callback);
    };
  },

  // Listener for ARINC Error/Status Updates from Main
  onArincErrorUpdate: (callback) => {
    const channel = 'arincErrorUpdate';
    ipcRenderer.removeAllListeners(channel);
    ipcRenderer.on(channel, (event, ...args) => callback(...args));
    return () => {
      ipcRenderer.removeListener(channel, callback);
    };
  }
}) 

console.log("preload.js loaded and electronAPI exposed."); 