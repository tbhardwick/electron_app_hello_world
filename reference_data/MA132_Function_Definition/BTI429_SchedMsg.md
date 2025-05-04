# **SchedMsg**

```
SCHNDX BTI429_SchedMsg
(
     MSGADDR message, //Address of message
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
```
)

## **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

## **DESCRIPTION**

Appends a MESSAGE command Block to the current end of the Schedule. When a MESSAGE command Block is encountered in the Schedule, the message value specified by *message* is loaded into the transmit channel's encoder. The message will be transmitted when the Schedule subsequently encounters either a GAP Command Block or another MESSAGE command Block. If a MESSAGE command Block is followed by another MESSAGE command Block, the gap time between the two messages is automatically set to four bit times. More or less gap time can be explicitly set with BTI429\_SchedGap or BTI429\_SchedMsgEx.

*Note: Execution of this function does NOT transmit the message. The message is transmitted only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function. In addition, the message must have been created with BTI429\_MsgCreate.

# **SEE ALSO**

BTI429\_SchedMsgEx, BTI429\_MsgCreate