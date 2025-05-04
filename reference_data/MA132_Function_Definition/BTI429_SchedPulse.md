# **SchedPulse**

```
SCHNDX BTI429_SchedPulse
(
      INT dionum, //Dio number to pulse
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
**RETURNS**

The index of the appended schedule entry if successful, or a negative value if an error occurs.

## **DESCRIPTION**

Schedules a pair of opcodes that pulse the specified *dionum* to the "On" state followed by the "Off" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

### **DEVICE DEPENDENCY**

The level of the *dionum* "On" and "Off" states, as well as the mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

The SCHNDX returned is for the second of the pair of opcodes that are scheduled. The opcodes are scheduled sequentially, so the SCHNDX of the first pulse opcode can be derived from the return value by subtracting one.

Schedules internally use prefetching to minimize intermessage gap time. Because of schedule prefetching and asynchronous discrete operation, the absolute timing of the pulse from a SchedPulse entry can vary by a gap/message transmission. When comparing the occurrence of scheduled discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine or BTI429\_MsgSyncDefine.

### **SEE ALSO**

```
BTI429_SchedPulse0, BTI429_SchedPulse1, BTICard_ExtDIORd, 
BTICard_ExtDIOWr
```