# **PlayPutLog**

```
ERRVAL BTI429_PlayPutLog
(
      ULONG condition, //Value to test during Playback
      USHORT tagval, //Event tag value during Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes a conditional LOG Command Block into *playbuf* at *offset*. A conditional LOG Command Block causes the Device to generate an Event Log List entry if *condition* evaluates as TRUE. The event type placed in the Event Log List is EVENTTYPE\_429OPCODE and the user-specified value *tagval* is used as the info value. Entries are read out of the Event Log List using BTICard\_EventLogRd. Refer to BTI429\_SchedLog for (the same) condition options.

![](_page_0_Picture_8.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

## **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

## **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum *offset*.

## **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayBlockWr, BTI429\_PlayStatus, BTI429\_SchedLog