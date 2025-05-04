# **TSMDriftWr**

ERRVAL **BTICard\_TSMDriftWr** (

```
INT drift, //Commanded drift value (ppt)
HCORE handleval //Device handle
```
## **RETURNS**

)

A negative value if an error occurs, otherwise zero.

### **DESCRIPTION**

Writes the input two's complement representation to the Device's drift adjustment value. When used in conjunction with BTICard\_TSMSyncConfig called with TSMCFG\_HOST, the device will adjust the clock frequency by the amount of the input PPT. A negative number represents a slower frequency. Value is in parts-per-trillion and is limited to the range from -425,000,000 *drift* < 425,000,000.

# **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

The Device will not adjust the frequency past the value set by BTICard\_TSMDriftMaxSet**.**

# **SEE ALSO**

BTICard\_TSMDriftRd, BTICard\_TSMSyncConfig, BTICard\_TSMDriftMaxSet