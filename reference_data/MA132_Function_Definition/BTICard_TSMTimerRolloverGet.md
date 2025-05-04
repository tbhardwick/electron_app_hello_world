# **TSMTimerRolloverGet**

ERRVAL **BTICard\_TSMTimerRolloverGet** (

```
LPULONGLONG valueptr, //Pointer to variable to hold timer rollover 
     //(ns)
HCORE handleval //Device handle
```
**RETURNS**

)

A negative value if an error occurs, otherwise zero.

### **DESCRIPTION**

Reads the Device's current timer rollover and places the value into the location pointed to by *valueptr*. Value is in nanoseconds. Default rollover value is exactly 365 days, but can be adjusted by BTICard\_TSMTimerRolloverSet to account for leap years and seconds. Value is in nanoseconds and valid range is 1000 to 2^64 - 1.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

None.

### **SEE ALSO**

BTICard\_TSMTimerRolloverSet