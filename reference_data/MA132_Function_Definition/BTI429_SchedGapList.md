# **SchedGapList**

```
SCHNDX BTI429_SchedGapList
(
     USHORT gap, //Gap value (in bit times or microseconds)
     LISTADDR list, //Address of asynchronous List Buffer
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

# **DESCRIPTION**

Appends a conditional LISTGAP Command Block to the current end of the Schedule. A conditional LISTGAP Command Block specifies *gap* before the next Scheduled message can be transmitted. During this gap time, if a message exists in the asynchronous List Buffer, it is transmitted in the gap time. Note that g*ap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

*Note: This is an advanced function, and for most applications, the BTI429\_SchedGap function is preferred.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

# **WARNINGS**

A call to BTI429\_ChConfig must precede this function. In addition, an asynchronous List Buffer must be configured using BTI429\_ListAsyncCreate before calling this function.

# **SEE ALSO**

BTI429\_SchedGap, BTI429\_SchedGapFixed, BTI429\_ListAsyncCreate