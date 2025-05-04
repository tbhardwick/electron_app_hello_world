# **TSMSyncConfig**

```
ERRVAL BTICard_TSMSyncConfig
(
      ULONG timeconfig //Specifies time configuration 
      ULONG driftconfig //Specifies drift configuration 
      HCORE handleval //Device handle 
)
```
### **RETURNS**

A negative value if an error occurs, otherwise zero.

# **DESCRIPTION**

Sets the Device's TSM to synchronize time and/or drift to an external source with options defined by *timeconfig and driftconfig as shown in the tables below*.

The TSM can synchronize time to a single IRIG or PPS input as set by *timeconfig and drift to* a single IRIG, PPS or 10 MHz input as set by *driftconfig.* 

Using time synchronization with an IRIG source will set the Device's timer to the time received via IRIG. Using time synchronization with a PPS source will round the timer to the nearest second at the PPS timing mark. When using a PPS input source, it is recommended to call BTICard\_TSMTimerWr to set the Device's time to the system time.

Using the TSM drift controls (set by *driftconfig*) will speed or slow the Device's clock by up to 425 PPM to account for the clock drift between the Device clock and the input source clock.

If BTICard\_TSMSyncConfig is not called, the host can still adjust the time using BTICard\_TSMTimerWr.

| timeconfig       |                                                                            |  |  |
|------------------|----------------------------------------------------------------------------|--|--|
| Constant         | Description                                                                |  |  |
| TSMCFG_NONE      | Select all default settings (bold below). Timer is free-running            |  |  |
| TSMCFG_PPS0      | Synchronizes time to PPS signal                                            |  |  |
| TSMCFG_PPS1      | Synchronizes time to PPS signal                                            |  |  |
| TSMCFG_PWMIRIG0  | Synchronizes time to PCM IRIG signal at speed selected by TSMCFG_IRIGA and |  |  |
|                  | TSMCFG_IRIGB                                                               |  |  |
| TSMCFG_PWMIRIG1  | Synchronizes time to PCM IRIG signal at speed selected by TSMCFG_IRIGA and |  |  |
|                  | TSMCFG_IRIGB                                                               |  |  |
| TSMCFG_AMIRIG    | Synchronizes time to AM IRIG signal at speed selected by TSMCFG_IRIGA and  |  |  |
|                  | TSMCFG_IRIGB                                                               |  |  |
| TSMCFG_IRIGA     | Selects 100 ms period IRIG                                                 |  |  |
| TSMCFG_IRIGB     | Selects 1 s period IRIG                                                    |  |  |
| TSMCFG_AMDACAUTO | Automatically selects appropriate AM IRIG voltage threshold                |  |  |
| TSMCFG_AMDACUSER | Allows user to select AM IRIG voltage threshold                            |  |  |

| driftconfig      |                                                                        |  |
|------------------|------------------------------------------------------------------------|--|
| Constant         | Description                                                            |  |
| TSMCFG_NONE      | Select all default settings (bold below). Clock frequency is unbiased. |  |
| TSMCFG_PPS0      | Adjusts clock frequency to match that of source of PPS signal          |  |
| TSMCFG_PPS1      | Adjusts clock frequency to match that of source of PPS signal          |  |
| TSMCFG_PWMIRIG0  | Adjusts clock frequency to match that of source of PCM IRIG signal     |  |
| TSMCFG_PWMIRIG1  | Adjusts clock frequency to match that of source of PCM IRIG signal     |  |
| TSMCFG_AMIRIG    | Adjusts clock frequency to match that of source of AM IRIG signal      |  |
| TSMCFG_10MHZ     | Adjusts clock frequency to match that of source of 10 MHz signal       |  |
| TSMCFG_HOST      | Allows user to adjust clock frequency via BTICARD_TSMDriftWr           |  |
| TSMCFG_IRIGA     | Selects 100 ms period IRIG                                             |  |
| TSMCFG_IRIGB     | Selects 1 s period IRIG                                                |  |
| TSMCFG_AMDACAUTO | Automatically selects appropriate AM IRIG voltage threshold            |  |
| TSMCFG_AMDACUSER | Allows user to select AM IRIG voltage threshold                        |  |

| Drift Input | Time Input |
|-------------|------------|
| None        | None       |
| None        | IRIG       |
| None        | PPS        |
| 10 MHz      | None       |
| 10 MHz      | IRIG       |
| 10 MHz      | PPS        |
| IRIG        | IRIG       |
| PPS         | None       |
| PPS         | IRIG       |
| PPS         | PPS        |

The TSM can use drift and time controls simultaneously with valid configurations of sources defined in a table below.

*TSM valid drift and time combinations*

See specific hardware manuals for information regarding TSM pin specifications.

# **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

None.

### **SEE ALSO**

BTICard\_TSMSourceConfig