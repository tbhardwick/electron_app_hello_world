# **ChOutputStateSet**

```
ERRVAL BTI429_ChOutputStateSet
(
      ULONG pos, //Output state of transmitter's positive leg
      ULONG neg, //Output state of transmitter's negative leg
      INT channum, //Channel number to test
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Sets the output state of each Transmit Channel leg (*pos*/*neg*) based on one of the following constants:

| pos/neg              |                                           |
|----------------------|-------------------------------------------|
| Constant             | Description                               |
| OUTSTATE429_NOCHANGE | Keep leg in its current state (default)   |
| OUTSTATE429_SIGNAL   | Connect the leg to normal transmit signal |
| OUTSTATE429_OPEN     | Leave the leg open                        |
| OUTSTATE429_GROUND   | Short the leg to ground                   |

Note that *pos* and *neg* may use different constants and need not match output states (e.g. only ground the positive leg). Only one constant can be used per leg.

#### **DEVICE DEPENDENCY**

Applies to 6G Devices with output state functionality. A channel can be tested for functionality using BTI429\_ChGetInfo. Please consult the Device hardware manual for more information.

## **WARNINGS**

This function is used for error injection; therefore, a transmitter leg may be shorted to ground and/or opened (floating) as instructed. Use caution that these output states do not damage any external databus hardware.

## **SEE ALSO**

BTI429\_ChGetInfo