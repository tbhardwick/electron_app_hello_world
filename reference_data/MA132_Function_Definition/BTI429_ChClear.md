# **ChClear**

```
ERRVAL BTI429_ChClear
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
# **RETURNS**

A negative value if an error occurs, or zero if successful.

# **DESCRIPTION**

Clears the contents of the specified channel. If the channel is a transmitter, all Command Blocks in the transmit Schedule are deleted. If the channel is a receiver, all filters are deleted. The contents of the Message Records are unaffected. The configuration options previously set by BTI429\_ChConfig are unchanged.

# **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

# **SEE ALSO**

BTICard\_CardReset, BTI429\_ChConfig