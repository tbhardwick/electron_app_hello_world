# **TSMIRIGYearsRd**

```
ERRVAL BTICard_TSMIRIGYearsRd
(
```

```
LPULONG year, //Pointer to variable to hold year value
HCORE handleval //Device handle
```
## **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads Device's Year and places the value into the location pointed to by *year*. Value is an unsigned integer value between 2000 and 2099.

When using IRIG time synchronization via BTICard\_TSMSyncConfig the Device's year value will be automatically populated with the Year field of the received message. Otherwise, the Device's year value can be set by the user via BTICard\_TSMIRIGYearsWr and the value will increment at the time indicated via BTICard\_TSMTimerRolloverSet**.** For IRIG formats without a Years field, this function can be ignored.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

**WARNINGS**

None.

#### **SEE ALSO**

```
BTICard_TSMIRIGControlWr, BTICard_TSMIRIGControlRd, 
BTICard_TSMIRIGYearsWr, BTICard_TSMSyncConfig, 
BTICard_TSMTimerRolloverSet
```