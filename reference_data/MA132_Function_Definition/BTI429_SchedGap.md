# **SchedGap**

```
SCHNDX BTI429_SchedGap
(
      USHORT gap, //Gap value (in bit times or microseconds)
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

## **DESCRIPTION**

Appends a GAP Command Block to the current end of the Schedule. When a GAP Command Block is encountered in the Schedule, it triggers the transmission of any preceding MESSAGE command Block as well as the specified gap before the next message can be transmitted. Note that g*ap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

Depending on the configuration of an asynchronous List Buffer, this function will internally call either BTI429\_SchedGapFixed or BTI429\_SchedGapList. This allows the user to include or leave out an asynchronous List Buffer without rebuilding the Schedule.

## **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

## **SEE ALSO**

BTI429\_SchedMsg, BTI429\_SchedGapFixed, BTI429\_SchedGapList