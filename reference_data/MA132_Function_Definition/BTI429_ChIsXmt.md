# **ChIsXmt**

```
BOOL BTI429_ChIsXmt 
(
      INT channel, //Channel number to test
      HCORE hCore //Core handle
)
```
# **RETURNS**

TRUE if the channel is a transmitter, or FALSE if it is not a transmitter.

#### **DESCRIPTION**

Checks to see if the channel number specified by *channel* is a transmit channel.

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

If this function returns a value of zero, do not assume that the channel must then be a receiver, because the channel may not exist at all. A call to BTI429\_ChIsRcv must be made to be sure that the channel is a receiver.

# **SEE ALSO**

BTI429\_ChIsRcv, BTI429\_ChGetCount, BTI429\_ChIs429