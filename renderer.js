const runTestBtn = document.getElementById('runTestBtn');
const resultsOutput = document.getElementById('resultsOutput');

if (runTestBtn && resultsOutput) { // Add checks to ensure elements exist
  runTestBtn.addEventListener('click', () => {
    resultsOutput.textContent = 'Running test...';
    // Check if electronAPI is available before calling send
    if (window.electronAPI && typeof window.electronAPI.send === 'function') {
        window.electronAPI.send('run-test'); 
    } else {
        resultsOutput.textContent = 'Error: electronAPI not loaded.';
        console.error('electronAPI or electronAPI.send is not available');
    }
  });

  // Check if electronAPI is available before calling on
  if (window.electronAPI && typeof window.electronAPI.on === 'function') {
    window.electronAPI.on('test-results', (resultsData) => {
      console.log('Received test results data:', resultsData);

      let htmlOutput = '';

      // Check for execution errors first
      if (resultsData.error) {
        htmlOutput += `<h2>Execution Error</h2>
<p class="error-message">${resultsData.error}</p>`;
        if (resultsData.stderr) {
          htmlOutput += `<h3>Stderr:</h3><pre class="stderr-output">${resultsData.stderr}</pre>`;
        }
        if (resultsData.stdout) {
          htmlOutput += `<h3>Stdout:</h3><pre>${resultsData.stdout}</pre>`;
        }
      } else {
        // Display Device Info
        htmlOutput += '<h2>Device Information</h2>';
        if (resultsData.deviceInfo && resultsData.deviceInfo.type) {
          htmlOutput += `<p><strong>Type:</strong> ${resultsData.deviceInfo.type}</p>`;
        }
        if (resultsData.deviceInfo && resultsData.deviceInfo.product) {
          htmlOutput += `<p><strong>Product:</strong> ${resultsData.deviceInfo.product}</p>`;
        }
        if (!resultsData.deviceInfo || (!resultsData.deviceInfo.type && !resultsData.deviceInfo.product)) {
            htmlOutput += '<p>No device information found.</p>';
        }
        
        // Display Warnings
        if (resultsData.warnings && resultsData.warnings.length > 0) {
            htmlOutput += '<h2 class="warning-message">Warnings</h2><ul class="results-list">';
            resultsData.warnings.forEach(warning => {
                htmlOutput += `<li>${warning}</li>`;
            });
            htmlOutput += '</ul>';
        }

        // Display Test Results
        htmlOutput += '<h2>Test Results</h2>';
        if (resultsData.tests && resultsData.tests.length > 0) {
          htmlOutput += '<ul class="results-list">';
          resultsData.tests.forEach(test => {
            const statusClass = test.status.startsWith('PASSED') ? 'status-passed' : 'status-failed';
            htmlOutput += `<li>
                             <span class="test-name">${test.name}:</span> 
                             <span class="${statusClass}">${test.status}</span>
                           </li>`;
          });
          htmlOutput += '</ul>';
        } else {
          htmlOutput += '<p>No test results found.</p>';
        }

        // Display Stderr (if any)
        if (resultsData.stderr) {
          htmlOutput += `<h2>Stderr Output</h2><pre class="stderr-output">${resultsData.stderr}</pre>`;
        }
        
        // Optionally display raw output for debugging
        // htmlOutput += `<h2>Raw Output</h2><pre>${resultsData.raw}</pre>`;
      }

      // Update the DOM using innerHTML to render the tags
      resultsOutput.innerHTML = htmlOutput;
    });
  } else {
    console.error('electronAPI or electronAPI.on is not available');
    // Display error in resultsOutput
    if (resultsOutput) {
      resultsOutput.innerHTML = '<p class="error-message">Error: electronAPI not available for receiving results.</p>';
    }
  }
} else {
    console.error('Could not find button or results elements in the DOM.');
    // If resultsOutput exists, display error there
    if(resultsOutput) {
        resultsOutput.textContent = 'Error: UI elements not found.';
    }
} 