# **TSMInputThresholdSet**

```
ERRVAL BTICard_TSMInputThresholdSet
```

```
(
LPUSHORT dacval, //Value for selected input threshold
INT pinindex, //Index indicating which input DAC threshold to set
HCORE handleval //Device handle
```
)

### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Sets the selected input threshold DAC value to the input *dacval*. *dacval* is an unsigned representation.

Available *pinindex* are shown below:

| pinindex        |                              |
|-----------------|------------------------------|
| Constant        | Description                  |
| TSMPIN_PWMIRIG0 | Threshold for IRIG PCM/PPS   |
| TSMPIN_PWMIRIG1 | Threshold for IRIG PCM/PPS   |
| TSMPIN_AMIRIGH* | Threshold for IRIG AM, Mark  |
| TSMPIN_AMIRIGL  | Threshold for IRIG AM, Space |
| TSMPIN_10MHZ    | Threshold for 10 MHz         |

*Note: By default the Device will auto-calculate the optimal AM IRIG threshold. To disable auto-calculation for AM IRIG use the flag TSMCFG\_AMDACUSER when calling BTICard\_TSMSyncConfig.*

# **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

When using AM IRIG, unless BTICard\_TSMSyncConfig is called with the TSMCFG\_AMDACUSER flag, the Device will automatically calculate the optimal threshold. Overriding the automatic thresholds is not recommended for reliable performance.

# **SEE ALSO**

```
BTICard_TSMInputDelayCompSet, BTICard_TSMSyncConfig
```