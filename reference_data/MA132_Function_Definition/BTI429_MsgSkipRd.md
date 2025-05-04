# **MsgSkipRd**

```
BOOL BTI429_MsgSkipRd
(
      MSGADDR message, //Message to read from
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The state of the skip bit for a message.

## **DESCRIPTION**

Reads the state of the skip bit for the Message Record specified by *message*. If the bit is zero, the message will be processed as normal. If the bit is non-zero, the message will be skipped and will not be processed.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

## **SEE ALSO**

BTI429\_MsgSkipRd, BTI429\_MsgMultiSkipWr