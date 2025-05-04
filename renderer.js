// const runTestBtn = document.getElementById('runTestBtn'); // REMOVED
// const stopPollBtn = document.getElementById('stopPollBtn'); // REMOVED - Was this used?
// const resultsOutput = document.getElementById('resultsOutput'); // REMOVED
const discreteIndicators = [];
for (let i = 0; i < 8; i++) {
    discreteIndicators.push(document.getElementById(`discrete-in-${i}`));
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

// Keep track of the polling interval ID
let dioPollingIntervalId = null;
let hCore = null;

// --- ARINC Receive Data Handling ---
let arincData = {}; // Structure: { channelId: { labelOctal: { word: number, timestamp: number, rowElement: Element } } }
const ARINC_CHANNEL_COUNT = 8;
const STALE_THRESHOLD_MS = 500;
const STALENESS_CHECK_INTERVAL_MS = 250;

function initializeArincUI() {
    console.log('Initializing ARINC Receive UI...');
    const container = document.querySelector('.arinc-channels-container');
    if (!container) {
        console.error('ARINC channels container not found!');
        return;
    }
    container.innerHTML = ''; // Clear previous content

    for (let i = 0; i < ARINC_CHANNEL_COUNT; i++) {
        arincData[i] = {}; // Initialize data store for channel

        const section = document.createElement('div');
        section.className = 'arinc-channel-section';
        section.id = `channel-${i}-data`;

        const heading = document.createElement('h3');
        const statusIndicator = document.createElement('span');
        statusIndicator.id = `channel-${i}-status-indicator`;
        statusIndicator.className = 'status-indicator grey'; // Initial state: grey
        statusIndicator.textContent = '●';
        heading.appendChild(statusIndicator);
        heading.appendChild(document.createTextNode(` Channel ${i}`));
        section.appendChild(heading);

        const table = document.createElement('table');
        table.id = `channel-${i}-table`;
        table.className = 'arinc-data-table table table-sm table-bordered table-striped'; // Added bootstrap classes
        table.innerHTML = `
            <thead>
                <tr>
                    <th>Label (Octal)</th>
                    <th>Data Word (Hex)</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                <!-- Data rows will be added here -->
            </tbody>
        `;
        section.appendChild(table);
        container.appendChild(section);
    }
    console.log('ARINC Receive UI Initialized.');
}

// Render/update a specific label row in the table
function renderLabelUpdate(channel, labelOctal) {
    const channelData = arincData[channel];
    if (!channelData || !channelData[labelOctal]) return;

    const entry = channelData[labelOctal];
    const tableBody = document.querySelector(`#channel-${channel}-table tbody`);
    if (!tableBody) {
        console.error(`Table body for channel ${channel} not found!`);
        return;
    }

    let row = entry.rowElement;
    if (!row) {
        row = document.createElement('tr');
        row.id = `ch${channel}-label-${labelOctal}`; // Use octal in ID

        const labelCell = document.createElement('td');
        labelCell.textContent = labelOctal;

        const wordCell = document.createElement('td');
        wordCell.textContent = entry.word.toString(16).toUpperCase().padStart(8, '0'); // Initial word

        const statusCell = document.createElement('td');
        const statusIndicator = document.createElement('span');
        statusIndicator.className = 'status-indicator grey'; // Initial state
        statusIndicator.textContent = '●';
        statusCell.appendChild(statusIndicator);

        row.appendChild(labelCell);
        row.appendChild(wordCell);
        row.appendChild(statusCell);

        tableBody.appendChild(row);
        entry.rowElement = row; // Cache the row element
    } else {
        // Row exists, just update the word cell
        const wordCell = row.cells[1];
        if (wordCell) {
            wordCell.textContent = entry.word.toString(16).toUpperCase().padStart(8, '0');
        }
    }
    // Staleness check will handle the indicator color update
}

// Check for stale data
function checkStaleness() {
    const now = Date.now();
    for (const channel in arincData) {
        for (const labelOctal in arincData[channel]) {
            const entry = arincData[channel][labelOctal];
            if (entry.rowElement) {
                const statusIndicator = entry.rowElement.querySelector('.status-indicator');
                if (statusIndicator) {
                    const isStale = (now - entry.timestamp) > STALE_THRESHOLD_MS;
                    statusIndicator.className = 'status-indicator ' + (isStale ? 'red' : 'green');
                }
            }
        }
    }
}

// Setup interval for staleness check
setInterval(checkStaleness, STALENESS_CHECK_INTERVAL_MS);

// --- Refactored Data Handler ---
function handleArincDataUpdate(updates) {
    if (!Array.isArray(updates)) {
        console.error('Received non-array data for ARINC update:', updates);
        return;
    }
    // console.log(`Handling ${updates.length} ARINC updates`);
    const now = Date.now(); // Use a consistent timestamp for the batch if not provided
    updates.forEach(update => {
        if (update.channel === undefined || update.label === undefined || update.word === undefined) {
            console.error('Invalid ARINC update received:', update);
            return;
        }

        const channel = update.channel;
        // Ensure label is treated as decimal if coming from mock generator
        const labelOctal = Number(update.label).toString(8).padStart(3, '0');

        if (!arincData[channel]) {
            console.warn(`Received data for unexpected channel ${channel}`);
            arincData[channel] = {}; // Initialize if needed
        }

        if (!arincData[channel][labelOctal]) {
            arincData[channel][labelOctal] = { rowElement: null }; // Initialize if first time
        }

        arincData[channel][labelOctal].word = update.word;
        // Use provided timestamp, otherwise use batch time
        arincData[channel][labelOctal].timestamp = update.timestamp_ms || update.timestamp || now;

        renderLabelUpdate(channel, labelOctal);
    });
}
// --- End Refactored Data Handler ---

// Handle ARINC data updates from main process
window.electronAPI?.onArincDataUpdate(handleArincDataUpdate);

// Handle ARINC error/status updates from main process
window.electronAPI?.onArincErrorUpdate((errorInfo) => {
    console.error('ARINC Error/Status Update:', errorInfo);
    const errorDiv = document.getElementById('arinc-rx-error');
    let statusIndicator = null;

    if (errorInfo.channel !== null && errorInfo.channel >= 0 && errorInfo.channel < ARINC_CHANNEL_COUNT) {
        // Channel-specific error
        statusIndicator = document.getElementById(`channel-${errorInfo.channel}-status-indicator`);
        if (errorDiv) {
            errorDiv.textContent = `Channel ${errorInfo.channel}: [${errorInfo.status}] ${errorInfo.message || ''}`;
            errorDiv.style.display = 'block';
        }
    } else {
        // Global error or status - update all channel indicators?
        if (errorDiv) {
            errorDiv.textContent = `Global: [${errorInfo.status}] ${errorInfo.message || ''}`;
            errorDiv.style.display = 'block';
        }
        // Potentially update all channel indicators, or add a global one
        for (let i = 0; i < ARINC_CHANNEL_COUNT; i++) {
             const indi = document.getElementById(`channel-${i}-status-indicator`);
             if(indi) {
                 indi.className = 'status-indicator ' + (errorInfo.status === 'OK' ? 'green' : 'red');
             }
        }
        return; // Exit after handling global update
    }

    if (statusIndicator) {
        statusIndicator.className = 'status-indicator ' + (errorInfo.status === 'OK' ? 'green' : 'red');
    }
});

// --- Mock Data Generation ---
function generateMockArincData(count = 20) {
    const mockUpdates = [];
    const now = Date.now();
    for (let i = 0; i < count; i++) {
        const channel = Math.floor(Math.random() * ARINC_CHANNEL_COUNT);
        // Generate random octal label between 001 and 377
        const label = Math.floor(Math.random() * (0o377 - 0o001 + 1)) + 0o001;
        const word = Math.floor(Math.random() * 0xFFFFFFFF);
        mockUpdates.push({
            channel: channel,
            label: label, // Keep as decimal number for consistency
            word: word,
            timestamp_ms: now // Use consistent timestamp for the batch
        });
    }
    console.log("Generated mock data:", mockUpdates);
    return mockUpdates;
}
// --- End Mock Data Generation ---

// --- End ARINC Receive Data Handling ---


function updateDIOStatus(statuses) {
    const container = document.getElementById('dio-status');
    const errorDiv = document.getElementById('dio-error');
    const lastUpdatedSpan = document.getElementById('dio-last-updated');
    container.innerHTML = ''; // Clear previous content
    errorDiv.style.display = 'none'; // Hide error initially

    // Only update timestamp if polling is active
    if (dioPollingIntervalId !== null) {
        lastUpdatedSpan.textContent = `Last Updated: ${new Date().toLocaleTimeString()}`;
    } else {
        lastUpdatedSpan.textContent = `Polling Stopped`;
    }

    if (!Array.isArray(statuses)) {
        console.error('Expected an array of DIO statuses, received:', statuses);
        container.innerHTML = '<p class="text-danger">Error: Invalid data received.</p>';
        return;
    }

    const row = document.createElement('div');
    row.className = 'dio-container'; // Use flex container

    statuses.forEach(dio => {
        const dioElement = document.createElement('div');
        dioElement.className = 'dio-indicator';
        dioElement.id = `dio-${dio.apiDionum}`;

        const labelSpan = document.createElement('span');
        labelSpan.className = 'dio-label';
        // Map apiDionum back to a user-friendly index or name if desired
        labelSpan.textContent = `DIO ${dio.apiDionum}:`;

        const statusSpan = document.createElement('span');
        statusSpan.className = 'dio-status';

        if (dio.status === 0) { // Assuming ERR_NONE is 0
            statusSpan.textContent = dio.value ? 'ON' : 'OFF';
            statusSpan.classList.add(dio.value ? 'on' : 'off');
        } else {
            statusSpan.textContent = 'ERR';
            statusSpan.classList.add('error');
            statusSpan.title = dio.error || `Error code: ${dio.status}`;
            // Display individual DIO error inline or collectively
            errorDiv.textContent = `Error on DIO ${dio.apiDionum}: ${dio.error || `Code ${dio.status}`}`;
            errorDiv.style.display = 'block';
        }

        dioElement.appendChild(labelSpan);
        dioElement.appendChild(statusSpan);
        row.appendChild(dioElement);
    });
    container.appendChild(row);
}

// Renamed from refreshDIO to reflect its use within polling
async function fetchAndUpdateDIO() {
    if (!hCore) {
        console.error('Core not open. Cannot fetch DIO.');
        // Optionally update UI to show core is not open
        stopDioPolling(); // Stop polling if core becomes invalid
        return;
    }
    // Ensure the toggle button reflects the running state
    const toggleButton = document.getElementById('toggle-dio-polling');
    if (toggleButton && dioPollingIntervalId !== null) {
        toggleButton.textContent = 'Stop Polling';
        toggleButton.classList.remove('btn-success');
        toggleButton.classList.add('btn-warning');
    }

    try {
        const dioStatuses = await window.electronAPI.getAllDioStates(hCore);
        updateDIOStatus(dioStatuses);
    } catch (error) {
        console.error('Error getting DIO states during poll:', error);
        const errorDiv = document.getElementById('dio-error');
        if(errorDiv) { // Check if element exists
             errorDiv.textContent = `Error polling DIO: ${error.message || error}`;
             errorDiv.style.display = 'block';
        }
        updateDIOStatus([]); // Clear display on error
        stopDioPolling(); // Stop polling on error
    }
}

function stopDioPolling() {
    if (dioPollingIntervalId !== null) {
        clearInterval(dioPollingIntervalId);
        dioPollingIntervalId = null;
        console.log("DIO Polling stopped.");
        const toggleButton = document.getElementById('toggle-dio-polling');
        const lastUpdatedSpan = document.getElementById('dio-last-updated');
        if (toggleButton) {
            toggleButton.textContent = 'Start Polling';
            toggleButton.classList.remove('btn-warning');
            toggleButton.classList.add('btn-success');
        }
        if(lastUpdatedSpan) {
             lastUpdatedSpan.textContent = `Polling Stopped`;
        }
    }
}

function startDioPolling() {
    if (dioPollingIntervalId === null && hCore) { // Only start if stopped and core is ready
        console.log("Starting DIO Polling...");
        fetchAndUpdateDIO(); // Fetch immediately
        dioPollingIntervalId = setInterval(fetchAndUpdateDIO, 100); // Poll every 100ms
        const toggleButton = document.getElementById('toggle-dio-polling');
         if (toggleButton) {
            toggleButton.textContent = 'Stop Polling';
            toggleButton.classList.remove('btn-success');
            toggleButton.classList.add('btn-warning');
        }
    } else if (!hCore) {
        console.error("Cannot start DIO polling: Core handle is not valid.");
    } else {
        console.log("DIO Polling is already active.");
    }
}

document.addEventListener('DOMContentLoaded', () => {
    // REMOVED: Listener for refreshButton
    const togglePollingButton = document.getElementById('toggle-dio-polling');
    const mockArincButton = document.getElementById('generate-mock-arinc');

    // Add listener for the toggle button
    if (togglePollingButton) {
        togglePollingButton.addEventListener('click', () => {
            if (dioPollingIntervalId === null) {
                startDioPolling();
            } else {
                stopDioPolling();
            }
        });
    } else {
        console.error('Toggle DIO Polling button not found');
    }

    // Add listener for mock data button
    if (mockArincButton) {
        mockArincButton.addEventListener('click', () => {
            const mockData = generateMockArincData(30); // Generate 30 updates
            handleArincDataUpdate(mockData); // Directly call the handler
        });
    } else {
        console.error('Generate Mock ARINC button not found');
    }

    // --- Initialization Sequence ---
    console.log('Initializing hardware...');
    window.electronAPI.initializeHardware()
        .then(result => {
            if (result.success) {
                hCore = result.hCore;
                console.log('Hardware initialized successfully, Core Handle:', hCore);
                return window.electronAPI.initializeReceiver(hCore);
            }
             else {
                throw new Error(`Failed to initialize hardware: ${result.message} (Code: ${result.resultCode})`);
            }
        })
        .then(initResult => {
             if (initResult.success) {
                console.log('ARINC Receiver initialized successfully.');
                 // Start DIO polling automatically after successful init
                 startDioPolling();
                 return window.electronAPI.startArincMonitoring(hCore);
             } else {
                  throw new Error(`Failed to initialize ARINC receiver: ${initResult.message || 'Unknown error'}`);
             }
        })
         .then(monitorResult => {
             if (monitorResult.success) {
                console.log('ARINC Monitoring started successfully.');
                 // Handle successful monitoring start (e.g., update UI indicator)
                 const globalIndicator = document.getElementById('arinc-rx-error');
                 if(globalIndicator) {
                    globalIndicator.style.display = 'none'; // Hide error on successful start
                 }
                 // Indicate monitoring is active on channel indicators maybe?
                 for (let i = 0; i < ARINC_CHANNEL_COUNT; i++) {
                     const indi = document.getElementById(`channel-${i}-status-indicator`);
                     if (indi) {
                         indi.className = 'status-indicator green'; // Show green initially
                         indi.title = 'Monitoring Active';
                     }
                 }
            } else {
                 throw new Error(`Failed to start ARINC monitoring: ${monitorResult.message || 'Unknown error'}`);
            }
        })
        .catch(error => {
            console.error('Initialization or Monitoring Start Failed:', error);
            const errorDiv = document.getElementById('dio-error'); // Or a more general error div
            errorDiv.textContent = `Initialization Failed: ${error.message || error}`;
            errorDiv.style.display = 'block';
             const arincErrorDiv = document.getElementById('arinc-rx-error');
            if(arincErrorDiv) {
                arincErrorDiv.textContent = `Initialization Failed: ${error.message || error}`;
                arincErrorDiv.style.display = 'block';
            }
            stopDioPolling(); // Ensure polling is stopped if init fails
        });

        // Initial UI setup for ARINC
        initializeArincUI();

    // Stop polling/monitoring if the window is closed
    window.addEventListener('beforeunload', () => {
        // Use the stop function
        stopDioPolling();
        if(hCore) {
            window.electronAPI.stopArincMonitoring(hCore).catch(console.error);
        }
    });
});

// Function to update discrete input status display
async function updateDiscreteInputs() {
    if (!isDeviceReady) {
        // Maybe reset indicators to a default state if device becomes not ready?
        for (let i = 0; i < 8; i++) {
            const indicatorDiv = discreteIndicators[i];
            if (indicatorDiv) {
                const statusSpan = indicatorDiv.querySelector('.dio-status');
                indicatorDiv.classList.remove('active', 'inactive', 'error');
                if (statusSpan) statusSpan.textContent = '---';
            }
        }
        return;
    }

    if (!window.electronAPI || typeof window.electronAPI.getAllDioStates !== 'function') {
        console.error('electronAPI or getAllDioStates not available');
        // Update UI to show API error state
        for (let i = 0; i < 8; i++) {
             const indicatorDiv = discreteIndicators[i];
            if (indicatorDiv) {
                const statusSpan = indicatorDiv.querySelector('.dio-status');
                indicatorDiv.classList.remove('active', 'inactive');
                indicatorDiv.classList.add('error');
                if (statusSpan) statusSpan.textContent = 'API ERR';
            }
        }
        return;
    }

    try {
        const allStates = await window.electronAPI.getAllDioStates();

        if (Array.isArray(allStates) && allStates.length === 8) {
            for (const dioState of allStates) {
                const displayIndex = dioState.index;
                const indicatorDiv = discreteIndicators[displayIndex];
                
                if (indicatorDiv) {
                    const statusSpan = indicatorDiv.querySelector('.dio-status');
                    const labelSpan = indicatorDiv.querySelector('.dio-label');
                    const displayName = discreteMapping[displayIndex]?.name || `Unknown DIO ${displayIndex}`;

                    // Ensure label is set correctly (in case it was changed)
                    if (labelSpan && labelSpan.textContent !== `${displayName}:`) {
                       labelSpan.textContent = `${displayName}:`;
                    }

                    // Remove previous state classes
                    indicatorDiv.classList.remove('active', 'inactive', 'error');

                    if (dioState.status === 0) {
                        if (dioState.value === true) {
                            indicatorDiv.classList.add('active');
                            if (statusSpan) statusSpan.textContent = 'Active';
                        } else {
                            indicatorDiv.classList.add('inactive');
                            if (statusSpan) statusSpan.textContent = 'Inactive';
                        }
                    } else {
                        const errorMsg = dioState.error || 'Error';
                        indicatorDiv.classList.add('error');
                        if (statusSpan) statusSpan.textContent = errorMsg.substring(0, 6); // Keep error text short
                        console.error(`Error reading ${displayName} (API ${dioState.apiDionum}): Status ${dioState.status}, Msg: ${errorMsg}`);
                    }
                }
            }
        } else {
            console.error('Received invalid data structure from getAllDioStates:', allStates);
            for (let i = 0; i < 8; i++) {
                const indicatorDiv = discreteIndicators[i];
                if (indicatorDiv) {
                    const statusSpan = indicatorDiv.querySelector('.dio-status');
                    indicatorDiv.classList.remove('active', 'inactive');
                    indicatorDiv.classList.add('error');
                    if (statusSpan) statusSpan.textContent = 'Data ERR';
                }
            }
        }
    } catch (error) {
        console.error('Error in updateDiscreteInputs calling getAllDioStates:', error);
        for (let i = 0; i < 8; i++) {
            const indicatorDiv = discreteIndicators[i];
            if (indicatorDiv) {
                const statusSpan = indicatorDiv.querySelector('.dio-status');
                indicatorDiv.classList.remove('active', 'inactive');
                indicatorDiv.classList.add('error');
                if (statusSpan) statusSpan.textContent = 'Comm ERR';
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
        const indicatorDiv = discreteIndicators[i];
        if (indicatorDiv) {
            const statusSpan = indicatorDiv.querySelector('.dio-status');
            const labelSpan = indicatorDiv.querySelector('.dio-label');
            const displayName = discreteMapping[i]?.name || `DIO ${i}`;
            
            indicatorDiv.classList.remove('active', 'inactive');
            indicatorDiv.classList.add('error');
            if(labelSpan) labelSpan.textContent = `${displayName}:`;
            if(statusSpan) statusSpan.textContent = 'Disabled';
        }
    }
} 