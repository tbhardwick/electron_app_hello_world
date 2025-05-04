# **TSMStatus**

```
ERRVAL BTICard_TSMStatus
(
```

```
LPULONG statusptr //Pointer to value to store current status
HCORE handleval //Device handle
```
## **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the Device's TSM status and loads the results into the location specified by statusptr*.* The status value can be tested using the predefined constants below:

| *statusptr          |                                                 |
|---------------------|-------------------------------------------------|
| Constant            | Description                                     |
| TSMSTAT_IRIGPRES    | Input IRIG signal is toggling and decodable     |
| TSMSTAT_IRIGBITSYNC | Input IRIG signal is toggling                   |
| TSMSTAT_IRIGSYNC    | TSM is locked to IRIG input                     |
| TSMSTAT_10MHZPRES   | Input 10 MHz signal is toggling                 |
| TSMSTAT_10MHZSYNC   | TSM is locked to 10 MHz input                   |
| TSMSTAT_PPSPRES     | Input PPS signal is toggling                    |
| TSMSTAT_PPSSYNC     | TSM is locked to PPS input                      |
| TSMSTAT_OUTOFBOUNDS | Drift control is outside of set drift threshold |

### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

If the TSM clock and input signal source are drifting more than the drift threshold returned by BTICard\_TSMDriftMaxGet, the TSM will not lock to the source and the associated synchronization status {TSMSTAT\_IRIGSYNC, TSMSTAT\_PPSSYNC, TSMSTAT\_10MHZSYNC} will not be asserted.

### **SEE ALSO**

BTICard\_TSMSyncConfig, BTICard\_TSMSourceConfig, BTICard\_TSMDriftMaxGet