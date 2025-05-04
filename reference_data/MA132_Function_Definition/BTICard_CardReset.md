# **CardReset**

```
VOID BTICard_CardReset
```

```
(
      HCORE hCore //Core handle
)
```
# **RETURNS**

None.

# **DESCRIPTION**

Stops and performs a hardware reset on the core specified by *hCore*. If a message is being processed, the processing is allowed to finish before the core is halted.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

Does not reset historic maximum and minimum Sysmon sensor values; to reset these values use BTICard\_SysMonClear.

# **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop, BTICard\_SysMonClear