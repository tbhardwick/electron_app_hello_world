# **EventLogStatus**

```
INT BTICard_EventLogStatus
(
      HCORE hCore //Core handle
)
```
## **RETURNS**

The status value of the Event Log List.

#### **DESCRIPTION**

Checks the status of the Event Log List without removing an entry. The status value can be tested using the predefined constants below:

| Constant     | Description                        |
|--------------|------------------------------------|
| STAT_EMPTY   | Event Log List is empty            |
| STAT_PARTIAL | Event Log List is partially filled |
| STAT_FULL    | Event Log List is full             |
| STAT_OFF     | Event Log List is off              |

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

When the buffer is full it wraps around and overwrites previous entries.

### **SEE ALSO**

BTICard\_EventLogConfig, BTICard\_EventLogRd