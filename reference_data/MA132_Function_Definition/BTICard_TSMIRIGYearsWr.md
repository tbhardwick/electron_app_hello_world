# **TSMIRIGYearsWr**

#### ERRVAL **BTICard\_TSMIRIGYearsWr** (

```
ULONG year, //Value to set Device year
HCORE handleval //Device handle
```
### **RETURNS**

)

A negative value if an error occurs, otherwise zero.

### **DESCRIPTION**

Sets the Device's Years to the input value *year,* which is an unsigned integer value between 2000 and 2099.

If Device has been configured via BTICard\_TSMSourceConfig to output IRIG, the Device will send *year* in an IRIG message.

The Device's Years value will increment at the time indicated via BTICard\_TSMTimerRolloverSet**.**

# **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

When using IRIG time synchronization via BTICard\_TSMSyncConfig the Device's year value will be automatically populated with the Year field of the received message, overwriting the User set value.

# **SEE ALSO**

BTICard\_TSMIRIGYearsRd, BTICard\_TSMSyncConfig, BTICard\_TSMSourceConfig, BTICard\_TSMTimerRolloverSet