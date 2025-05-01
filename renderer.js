const runTestBtn = document.getElementById('runTestBtn');
const resultsOutput = document.getElementById('resultsOutput');

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
    // Updated listener for the new resultsData structure
    window.electronAPI.on('test-results', (resultsData) => {
      console.log('Received test results data:', resultsData);

      let htmlOutput = '';
      resultsOutput.className = ''; // Reset classes

      // --- Error Handling --- 
      // 1. Check for execution errors (process couldn't start, non-zero exit etc.)
      if (resultsData.executionError) {
        htmlOutput += `<h2>Execution Error</h2>
                       <p class="error-message">Failed to run test executable: ${resultsData.executionError}</p>`;
        resultsOutput.className = 'error-message'; 
      }
      // 2. Check for errors parsing the output from the executable
      else if (resultsData.parseError) {
          htmlOutput += `<h2>Parse Error</h2>
                         <p class="error-message">${resultsData.parseError}</p>`;
          if (resultsData.rawStdout) { // Show raw output if parsing failed
              htmlOutput += `<h3>Raw Output:</h3><pre>${resultsData.rawStdout}</pre>`;
          }
          resultsOutput.className = 'error-message';
      }
      // 3. Check if C# code itself reported an error (e.g., exception during Initialize or RunTests)
      else if (resultsData.csOutput && resultsData.csOutput.Success === false && resultsData.csOutput.ErrorMessage) {
          htmlOutput += `<h2>Test Execution Error</h2>
                         <p class="error-message">Error during BTI card operation: ${resultsData.csOutput.ErrorMessage}</p>`;
          resultsOutput.className = 'error-message';
          // Also display any partial results if available
          if (resultsData.csOutput.DeviceInfo) {
              htmlOutput += displayDeviceInfo(resultsData.csOutput.DeviceInfo);
          }
          if (resultsData.csOutput.TestResults && resultsData.csOutput.TestResults.length > 0) {
              htmlOutput += displayTestResults(resultsData.csOutput.TestResults);
          }
      }
      // --- Successful Execution --- 
      // 4. If csOutput exists and indicates success, display the results
      else if (resultsData.csOutput && resultsData.csOutput.Success === true) {
        htmlOutput += displayDeviceInfo(resultsData.csOutput.DeviceInfo);
        htmlOutput += displayTestResults(resultsData.csOutput.TestResults);
      }
       // 5. Fallback for unexpected cases (e.g., csOutput is null but no errors reported)
      else {
          htmlOutput += `<h2>Unknown State</h2>
                         <p class="error-message">Received unexpected data structure from main process.</p>
                         <pre>${JSON.stringify(resultsData, null, 2)}</pre>`; // Show the raw data received
          resultsOutput.className = 'error-message';
      }
      
      // Display Stderr if it exists (can occur with or without other errors)
      if (resultsData.stderr) {
        htmlOutput += `<h2>Stderr Output</h2><pre class="stderr-output">${resultsData.stderr}</pre>`;
        // Don't override error class if already set
        if (!resultsOutput.className.includes('error-message')) {
            resultsOutput.className = 'warning-message'; // Treat stderr as a warning if no other errors
        }
      }

      // Update the DOM using innerHTML to render the tags
      resultsOutput.innerHTML = htmlOutput;
    });
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

// --- Helper functions for generating HTML --- 

function displayDeviceInfo(deviceInfo) {
    let html = '<h2>Device Information</h2>';
    if (deviceInfo) {
        html += `<p><strong>Type:</strong> ${deviceInfo.CardType || 'N/A'}</p>`;
        html += `<p><strong>Product:</strong> ${deviceInfo.CardProduct || 'N/A'}</p>`;
        html += `<p><strong>Driver Info:</strong> ${deviceInfo.DriverInfo || 'N/A'}</p>`;
    } else {
        html += '<p>No device information found.</p>';
    }
    return html;
}

function displayTestResults(testResults) {
    let html = '<h2>Test Results</h2>';
    if (testResults && testResults.length > 0) {
        html += '<ul class="results-list">';
        testResults.forEach(test => {
            // Use test.ErrorCode for more robust pass/fail check (assuming 0 means pass)
            const statusClass = test.ErrorCode === 0 ? 'status-passed' : 'status-failed';
            // Use test.Status which contains the formatted message from C#
            html += `<li>
                         <span class="test-name">${test.Name}:</span> 
                         <span class="${statusClass}">${test.Status}</span>
                       </li>`;
        });
        html += '</ul>';
    } else {
        html += '<p>No test results reported.</p>';
    }
    return html;
} 