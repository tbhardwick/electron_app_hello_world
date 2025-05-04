# **TSMInputThresholdGet**

```
ERRVAL BTICard_TSMInputThresholdGet
```

```
(
LPUSHORT dacval, //Pointer to variable to hold current DAC value
INT pinindex, //Index indicating which input DAC threshold to read
HCORE handleval //Device handle
```
)

### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the current value of the input threshold DAC setting and places the value into the location pointed to by *dacval*. Value is an unsigned representation.

Available *pinindex* values are shown below:

| pinindex        |                              |
|-----------------|------------------------------|
| Constant        | Description                  |
| TSMPIN_PWMIRIG0 | Threshold for IRIG PCM/PPS   |
| TSMPIN_PWMIRIG1 | Threshold for IRIG PCM/PPS   |
| TSMPIN_AMIRIGH* | Threshold for IRIG AM, Mark  |
| TSMPIN_AMIRIGL  | Threshold for IRIG AM, Space |
| TSMPIN_10MHZ    | Threshold for 10MHz          |

Note: By default the Device will auto-calculate the optimal AM IRIG Mark threshold. To disable auto-calculation for AM IRIG Mark threshold, use the flag *TSMCFG\_AMDACUSER* when calling *BTICard\_TSMSyncConfig.*

### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

When using AM IRIG, unless BTICard\_TSMSyncConfig is called with the TSMCFG\_AMDACUSER flag, the Device will automatically calculate the optimal threshold. Overriding the automatic threshold is not recommended for reliable performance.

## **SEE ALSO**

BTICard\_TSMInputDelayCompGet, BTICard\_TSMSyncConfig