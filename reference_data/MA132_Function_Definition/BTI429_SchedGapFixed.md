# **SchedGapFixed**

```
SCHNDX BTI429_SchedGapFixed
(
      USHORT gap, //Gap value (in bit times or microseconds)
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
## **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

## **DESCRIPTION**

Appends a FIXEDGAP Command Block to the current end of the Schedule. When a FIXEDGAP Command Block is encountered in the Schedule, it triggers the transmission of any preceding MESSAGE command Block as well as the specified gap before the next message can be transmitted. The gap time is fixed so no asynchronous messages can be transmitted during this time. Note that *gap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

*Note: This is an advanced function, and for most applications, the BTI429\_SchedGap function is preferred.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

### **SEE ALSO**

BTI429\_SchedGap, BTI429\_SchedGapList