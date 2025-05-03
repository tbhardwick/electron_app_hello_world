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
    // --- Updated listener for the NEW simple resultsData structure from the native addon --- 
    window.electronAPI.on('test-results', (resultsData) => {
      console.log('Received test results data:', resultsData);

      resultsOutput.className = ''; // Reset classes

      if (resultsData && typeof resultsData.success === 'boolean') {
        if (resultsData.success) {
          resultsOutput.textContent = `Success: ${resultsData.message} (Code: ${resultsData.resultCode !== null ? resultsData.resultCode : 'N/A'})`;
          resultsOutput.className = 'status-passed'; // Use a success class
        } else {
          resultsOutput.textContent = `Error: ${resultsData.message} (Code: ${resultsData.resultCode !== null ? resultsData.resultCode : 'N/A'})`;
          resultsOutput.className = 'error-message status-failed'; // Use an error class
        }
      } else {
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