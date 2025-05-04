# **SchedMsgEx**

```
SCHNDX BTI429_SchedMsgEx
(
      MSGADDR message, //Address of message
      USHORT gap, //Gap to follow message (in bit times or microseconds)
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

# **DESCRIPTION**

Appends a MESSAGE command Block to the current end of the Schedule with a user-specified gap value that can be less than the automatic minimum of 4 bit times. A MESSAGE command Block loads the message value specified by *message* into the transmit channel's encoder. The message will be transmitted when the Schedule encounters either a GAP Command Block or another MESSAGE command Block.

*gap* specifies the gap value (0 to 65,535) to follow the message. This gap value allows the user to explicitly specify a gap value of less (or more) than four bit times. This gap value is overwritten if followed by a GAP Command Block. Note that g*ap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

*Note: Execution of this function does NOT transmit the message. The message is transmitted only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

# **WARNINGS**

A call to BTI429\_ChConfig must precede this function. In addition, the message must have been created with BTI429\_MsgCreate.

# **SEE ALSO**

BTI429\_SchedMsg, BTI429\_MsgCreate