# **SeqStart**

```
BOOL BTICard_SeqStart
(
      HCORE hCore //Core handle
)
```
### **RETURNS**

TRUE if the Sequential Record was previously running, otherwise FALSE.

### **DESCRIPTION**

Starts recording of the Sequential Record. If necessary, it also stops and clears the Sequential Record before restarting it.

## **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

If this function is called after BTICard\_SeqStop, recording starts at the beginning of the buffer and previous data is overwritten. To add to previous data without erasing it, use BTICard\_SeqResume instead.

### **SEE ALSO**

BTICard\_SeqStop, BTICard\_SeqResume