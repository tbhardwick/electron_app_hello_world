# **Timer64Rd**

```
ERRVAL BTICard_Timer64Rd
```

```
(
     LPULONG valueh, //Pointer to upper 32 bits of the timer value
     LPULONG valuel, //Pointer to lower 32 bits of the timer value
     HCORE hCore //Core handle
```
)

## **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Reads the current value of the binary timer from the specified Device.

### **DEVICE DEPENDENCY**

Applies to all Devices.

6G Devices have a 64-bit binary timer with a 1 ns resolution.

5G and 5G+ Devices have a 48-bit binary timer with a 1 s resolution.

3G and 4G Devices have a 32-bit binary timer with an adjustable resolution. To use the IRIG timer for a specified 4G Device instead of the default binary timer, see BTICard\_IRIGConfig.

Please see BTICard\_TimerRd for a discussion of Device-dependent timer differences.

#### **WARNINGS**

None.

### **SEE ALSO**

```
BTICard_TimerClear, BTICard_TimerRd, BTICard_TimerWr, BTICard_Timer64Wr, 
BTICard_IRIGConfig, BTICard_IRIGRd, BTICard_IRIGWr
```