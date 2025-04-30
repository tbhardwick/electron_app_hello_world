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
      window.electronAPI.on('test-results', (data) => {
        console.log('Received test results:', data);
        resultsOutput.textContent = data;
      });
  } else {
      console.error('electronAPI or electronAPI.on is not available');
      // Optionally display an error in resultsOutput here too
  }
} else {
    console.error('Could not find button or results elements in the DOM.');
    // If resultsOutput exists, display error there
    if(resultsOutput) {
        resultsOutput.textContent = 'Error: UI elements not found.';
    }
} 