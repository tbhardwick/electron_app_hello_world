# **ChGetCount**

```
VOID BTI429_ChGetCount
(
      LPINT rcvcount, //Pointer to variable to hold receiver count
      LPINT xmtcount, //Pointer to variable to hold transmitter count
      HCORE hCore //Core handle
```
)

# **RETURNS**

None.

# **DESCRIPTION**

Determines the transmitter and receiver channel count, and puts them in the variables pointed to by *rcvcount* and *xmtcount*.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

# **SEE ALSO**

BTI429\_ChIsRcv, BTI429\_ChIsXmt, BTI429\_ChIs429, BTI429\_ChGetInfo