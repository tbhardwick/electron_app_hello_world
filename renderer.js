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

// Handle ARINC data updates from main process
window.electronAPI?.onArincDataUpdate((updates) => {
    if (!Array.isArray(updates)) {
        console.error('Received non-array data for ARINC update:', updates);
        return;
    }
    // console.log(`Received ${updates.length} ARINC updates`);
    const now = Date.now(); // Use a consistent timestamp for the batch
    updates.forEach(update => {
        if (update.channel === undefined || update.label === undefined || update.word === undefined) {
            console.error('Invalid ARINC update received:', update);
            return;
        }

        const channel = update.channel;
        const labelOctal = update.label.toString(8).padStart(3, '0');

        if (!arincData[channel]) {
            console.warn(`Received data for unexpected channel ${channel}`);
            arincData[channel] = {}; // Initialize if needed, though UI init should do this
        }

        if (!arincData[channel][labelOctal]) {
            arincData[channel][labelOctal] = { rowElement: null }; // Initialize if first time seeing label
        }

        arincData[channel][labelOctal].word = update.word;
        arincData[channel][labelOctal].timestamp = update.timestamp || now; // Use provided timestamp or batch time

        renderLabelUpdate(channel, labelOctal);
    });
});

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

// --- End ARINC Receive Data Handling ---


function updateDIOStatus(statuses) {
    const container = document.getElementById('dio-status');
    const errorDiv = document.getElementById('dio-error');
    const lastUpdatedSpan = document.getElementById('dio-last-updated');
    container.innerHTML = ''; // Clear previous content
    errorDiv.style.display = 'none'; // Hide error initially
    lastUpdatedSpan.textContent = `Last Updated: ${new Date().toLocaleTimeString()}`;

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

async function refreshDIO() {
    if (!hCore) {
        console.error('Core not open. Cannot refresh DIO.');
        const errorDiv = document.getElementById('dio-error');
        errorDiv.textContent = 'Error: Hardware core is not open.';
        errorDiv.style.display = 'block';
        return;
    }
    try {
        // console.log('Requesting DIO states from main process...');
        const dioStatuses = await window.electronAPI.getAllDioStates(hCore);
        // console.log('Received DIO states:', dioStatuses);
        updateDIOStatus(dioStatuses);
    } catch (error) {
        console.error('Error getting DIO states:', error);
        const errorDiv = document.getElementById('dio-error');
        errorDiv.textContent = `Error refreshing DIO: ${error.message || error}`;
        errorDiv.style.display = 'block';
        updateDIOStatus([]); // Clear display on error
    }
}


document.addEventListener('DOMContentLoaded', () => {
    const refreshButton = document.getElementById('refresh-dio');

    if (refreshButton) {
        refreshButton.addEventListener('click', refreshDIO);
    } else {
        console.error('Refresh DIO button not found');
    }

    // --- Initialization Sequence ---
    console.log('Initializing hardware...');
    window.electronAPI.initializeHardware()
        .then(result => {
            if (result.success) {
                hCore = result.hCore; // Store the core handle
                console.log('Hardware initialized successfully, Core Handle:', hCore);
                // Initialize ARINC Receiver specific components
                return window.electronAPI.initializeReceiver(hCore);
            }
             else {
                throw new Error(`Failed to initialize hardware: ${result.message} (Code: ${result.resultCode})`);
            }
        })
        .then(initResult => {
             if (initResult.success) {
                console.log('ARINC Receiver initialized successfully.');
                // Now safe to refresh DIO or start ARINC monitoring
                refreshDIO(); // Perform initial DIO read
                // Start ARINC monitoring
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
        });

        // Initial UI setup for ARINC
        initializeArincUI();

    // Stop polling/monitoring if the window is closed
    window.addEventListener('beforeunload', () => {
        if (dioPollingIntervalId) {
            clearInterval(dioPollingIntervalId);
        }
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