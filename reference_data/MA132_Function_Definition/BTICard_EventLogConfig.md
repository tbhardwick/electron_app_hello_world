# **EventLogConfig**

```
ERRVAL BTICard_EventLogConfig
(
     USHORT ctrlflags //Selects the configuration options
     USHORT count //Number of entries in the Event Log List
     HCORE hCore //Core handle
)
```
# **RETURNS**

A negative value if an error occurs or zero if successful.

# **DESCRIPTION**

Configures and enables the Event Log List of the core specified by *hCore*. The maximum number of entries that may be contained in the Event Log List is set by *count*. Ctrlflags can be one of the following constants:

| ctrlflags      |                                     |
|----------------|-------------------------------------|
| Constant       | Condition                           |
| LOGCFG_DEFAULT | Selects all default (bold) settings |
| LOGCFG_ENABLE  | Enables the Event Log List          |
| LOGCFG_DISABLE | Disables the Event Log List         |

# **DEVICE DEPENDENCY**

The size of the Event Log for 5G, 5G+, and 6G Devices is always 256 entries regardless of count.

#### **WARNINGS**

#### None.

#### **SEE ALSO**

BTICard\_EventLogRd, BTICard\_EventLogStatus