# **CardResume**

```
ERRVAL BTICard_CardResume
(
      HCORE hCore //Core handle
)
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Reactivates the specified core from the point at which it was stopped (using BTICard\_CardStop). The following table compares the difference between calling BTICard\_CardResume and BTICard\_CardStart:

| Feature            | When CardStart is called….                                                             | When CardResume is called….                                                          |
|--------------------|----------------------------------------------------------------------------------------|--------------------------------------------------------------------------------------|
| Transmit Schedule  | Execution starts at the start of the transmit<br>Schedule.                             | Execution resumes at the point the transmit<br>Schedule was stopped.                 |
| Event Log List     | Any unread entries in the Event Log List are<br>cleared before the core is started.    | Any unread records in the Event Log List are<br>preserved as the core is resumed.    |
| Sequential Monitor | Any unread records in the Sequential Record<br>are cleared before the core is started. | Any unread records in the Sequential Record<br>are preserved as the core is resumed. |

### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

A call to BTICard\_CardStop must precede this function.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop