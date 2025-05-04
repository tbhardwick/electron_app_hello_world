# **PlayPutPulse0**

```
ERRVAL BTI429_PlayPutPulse0
(
      INT dionum, //DIO number to pulse "Off" during Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Writes a PULSE0 Command Block into *playbuf* at offset. A PULSE0 Command Block sets the state of the specified *dionum* to the "Off" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

Note that by using this function in combination with BTI429\_PlayPutData, BTI429\_PlayPutGap, and/or other playback entries, the I/O signals can be used to frame data messages, groups of messages, or to create arbitrary pulse width signals.

![](_page_0_Figure_9.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

# **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum offset.

Playback internally uses prefetching to minimize intermessage gap time. Due to prefetching and asynchronous discrete operation, the absolute timing of the pulse from a *SchedPulse* entry can vary by a gap/message transmission. When comparing the occurrence of playback discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine.

# **SEE ALSO**

```
BTI429_ChConfig, BTI429_PlayBlockWr, BTI429_PlayStatus
```