const runTestBtn = document.getElementById('runTestBtn');
const stopPollBtn = document.getElementById('stopPollBtn'); // Get reference to stop button
const resultsOutput = document.getElementById('resultsOutput');
const discreteInputSpans = [];
for (let i = 0; i < 8; i++) {
    discreteInputSpans.push(document.getElementById(`discrete-in-${i}`));
}

// Mapping from display index (0-7) to Hardware Reference Name and API dionum
const discreteMapping = [
    { name: "DIO 0", apiDionum: 1 },   // HW DIO0 -> API dionum 1
    { name: "DIO 1", apiDionum: 2 },   // HW DIO1 -> API dionum 2
    { name: "DIO 2", apiDionum: 3 },   // HW DIO2 -> API dionum 3
    { name: "DIO 3", apiDionum: 4 },   // HW DIO3 -> API dionum 4
    { name: "DIO 4", apiDionum: 9 },   // HW DIO4 -> API dionum 9
    { name: "DIO 5", apiDionum: 10 },  // HW DIO5 -> API dionum 10
    { name: "DIO 6", apiDionum: 11 },  // HW DIO6 -> API dionum 11
    { name: "DIO 7", apiDionum: 12 }   // HW DIO7 -> API dionum 12
];

// Variable to hold the interval ID
let pollingIntervalId = null; 

// Flag to track if the device (card/core) is successfully opened
let isDeviceReady = false;

if (runTestBtn && resultsOutput) { // Add checks to ensure elements exist
  runTestBtn.addEventListener('click', () => {
    resultsOutput.textContent = 'Running test...';
    resultsOutput.className = ''; // Reset any previous status class
    // Check if electronAPI is available before calling send
    if (window.electronAPI && typeof window.electronAPI.send === 'function') {
        window.electronAPI.send('run-test');
    } else {
        resultsOutput.textContent = 'Error: electronAPI not loaded.';
        resultsOutput.className = 'error-message'; // Add error class
        console.error('electronAPI or electronAPI.send is not available');
    }
  });

  // Check if electronAPI is available before calling on
  if (window.electronAPI && typeof window.electronAPI.on === 'function') {
    // --- Updated listener for the NEW simple resultsData structure from the native addon --- 
    window.electronAPI.on('test-results', (resultsData) => {
      console.log('Received test results data:', resultsData);

      resultsOutput.className = ''; // Reset classes

      if (resultsData && typeof resultsData.success === 'boolean') {
        // Update the device ready state based on test success
        isDeviceReady = resultsData.success;
        console.log(`Device ready state set to: ${isDeviceReady}`);

        if (resultsData.success) {
          resultsOutput.textContent = `Success: ${resultsData.message} (Code: ${resultsData.resultCode !== null ? resultsData.resultCode : 'N/A'})`;
          resultsOutput.className = 'status-passed'; // Use a success class
        } else {
          resultsOutput.textContent = `Error: ${resultsData.message} (Code: ${resultsData.resultCode !== null ? resultsData.resultCode : 'N/A'})`;
          resultsOutput.className = 'error-message status-failed'; // Use an error class
        }
      } else {
        // Test failed or unexpected data, ensure device is marked not ready
        isDeviceReady = false;
        console.log(`Device ready state set to false due to unexpected data.`);
        // Fallback if the data structure is not what we expect
        resultsOutput.textContent = 'Error: Received unexpected data structure from main process.';
        resultsOutput.className = 'error-message';
        console.error('Unexpected resultsData structure:', resultsData);
      }
    });
    // --- End Updated Listener ---

  } else {
    console.error('electronAPI or electronAPI.on is not available');
    // Display error in resultsOutput
    if (resultsOutput) {
      resultsOutput.innerHTML = '<p class="error-message">Error: electronAPI not available for receiving results.</p>';
      resultsOutput.className = 'error-message';
    }
  }
} else {
    console.error('Could not find button or results elements in the DOM.');
    // If resultsOutput exists, display error there
    if(resultsOutput) {
        resultsOutput.textContent = 'Error: UI elements not found.';
        resultsOutput.className = 'error-message';
    }
}

// Function to update discrete input status display
async function updateDiscreteInputs() {
    // *** ADDED CHECK: Only proceed if the device is ready ***
    if (!isDeviceReady) {
        return;
    }

    // Use the new getAllDioStates function
    if (!window.electronAPI || typeof window.electronAPI.getAllDioStates !== 'function') {
        console.error('electronAPI or getAllDioStates not available');
        return;
    }

    try {
        // Call the single function to get all states
        const allStates = await window.electronAPI.getAllDioStates();
        // console.log("Received allStates:", allStates); // Optional: log the whole array

        // Process the returned array
        if (Array.isArray(allStates) && allStates.length === 8) {
            for (const dioState of allStates) {
                // dioState should be { index: 0-7, apiDionum: 1-12, status: 0/-1, value: true/false/null, error: null/string }
                const displayIndex = dioState.index;
                const displayName = discreteMapping[displayIndex]?.name || `Unknown DIO ${displayIndex}`;
                
                if (discreteInputSpans[displayIndex]) {
                    if (dioState.status === 0) {
                        const statusText = dioState.value === true ? 'Active' : 'Inactive';
                        discreteInputSpans[displayIndex].textContent = `${displayName}: ${statusText}`;
                    } else {
                        // Use the error message from the addon if available
                        const errorMsg = dioState.error || 'Error'; 
                        discreteInputSpans[displayIndex].textContent = `${displayName}: ${errorMsg}`;
                         console.error(`Error reading ${displayName} (API ${dioState.apiDionum}): Status ${dioState.status}, Msg: ${errorMsg}`);
                    }
                }
            }
        } else {
             console.error('Received invalid data structure from getAllDioStates:', allStates);
             // Set all to error state
             for (let i = 0; i < 8; i++) {
                 if (discreteInputSpans[i]) {
                    discreteInputSpans[i].textContent = `${discreteMapping[i]?.name || 'DIO ' + i}: Invalid Data`;
                 }
            }
        }
    } catch (error) {
        console.error('Error in updateDiscreteInputs calling getAllDioStates:', error);
        // Set all to error state on general failure
        for (let i = 0; i < 8; i++) {
             if (discreteInputSpans[i]) {
                discreteInputSpans[i].textContent = `${discreteMapping[i]?.name || 'DIO ' + i}: Comm Error`;
             }
        }
    }
}

// Start polling discrete inputs every 100ms (changed from 1 second)
// We only start polling if the API function is available
if (window.electronAPI && typeof window.electronAPI.getAllDioStates === 'function') { // Check for new function
    // Store the interval ID
    pollingIntervalId = setInterval(updateDiscreteInputs, 100); // Changed 1000 to 100 
} else {
    console.warn('getAllDioStates function not found on window.electronAPI. Discrete input polling disabled.');
    // Update UI to indicate polling is disabled
    for (let i = 0; i < 8; i++) {
        if(discreteInputSpans[i]) {
            discreteInputSpans[i].textContent = `DI ${i}: Disabled`;
        }
    }
}

// Add event listener for the stop button
if (stopPollBtn) {
    stopPollBtn.addEventListener('click', () => {
        if (pollingIntervalId !== null) {
            clearInterval(pollingIntervalId);
            pollingIntervalId = null; // Clear the ID
            console.log("Polling stopped by user.");
            // Optionally update UI to indicate polling is stopped
             for (let i = 0; i < 8; i++) {
                 if (discreteInputSpans[i]) {
                    // Append ' (Stopped)' or similar
                    const currentText = discreteInputSpans[i].textContent;
                    if (!currentText.includes('(Stopped)')) { 
                        discreteInputSpans[i].textContent += ' (Stopped)';
                    }
                 }
             }
        } else {
            console.log("Polling is already stopped.");
        }
    });
} 