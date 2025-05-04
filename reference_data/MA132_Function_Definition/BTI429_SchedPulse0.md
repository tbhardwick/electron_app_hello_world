# **SchedPulse0**

```
SCHNDX BTI429_SchedPulse0
(
      INT dionum, //DIO number to pulse
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
## **RETURNS**

The index of the appended schedule entry if successful, or a negative value if an error occurred.

## **DESCRIPTION**

Sets the state of the specified *dionum* to the "Off" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

*Note that by using this function in combination with BTI429\_SchedGap, BTI429\_SchedMessage and/or other schedule entries, the I/O signals can be used to frame messages, groups of messages, or to create arbitrary pulse width signals.*

## **DEVICE DEPENDENCY**

The level of the *dionum* "On" and "Off" states, as well as the mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

## **WARNINGS**

Schedules internally use prefetching to minimize intermessage gap time. Because of schedule prefetching and asynchronous discrete operation, the absolute timing of the pulse from a SchedPulse entry can vary by a gap/message transmission. When comparing the occurrence of scheduled discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine or BTI429\_MsgSyncDefine.

## **SEE ALSO**

BTI429\_SchedPulse, BTI429\_SchedPulse1, BTICard\_ExtDIORd, BTICard\_ExtDIOWr