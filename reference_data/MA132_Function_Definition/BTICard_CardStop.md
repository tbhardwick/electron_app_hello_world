# **CardStop**

```
BOOL BTICard_CardStop
(
      HCORE hCore //Core handle
)
```
### **RETURNS**

TRUE if the core was active, otherwise FALSE.

#### **DESCRIPTION**

Stops operation of all the channel on the specified core. If a message is being processed, the processing is allowed to finish before the core is halted.

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

# **SEE ALSO**

BTICard\_CardStart, BTICard\_CardIsRunning