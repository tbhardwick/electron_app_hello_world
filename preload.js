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
  getAllDioStates: async () => {
      console.log("[preload.js] getAllDioStates called");
      try {
          const results = await ipcRenderer.invoke('bti:getAllDioStates');
          console.log("[preload.js] invoke('bti:getAllDioStates') returned successfully.");
          return results; 
      } catch (error) {
          console.error("[preload.js] Error invoking 'bti:getAllDioStates':", error);
          throw error; // Re-throw
      }
  }
}) 

console.log("preload.js loaded and electronAPI exposed."); 