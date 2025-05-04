# **SeqStop**

```
BOOL BTICard_SeqStop
(
      HCORE hCore //Core handle
)
```
## **RETURNS**

TRUE if the Sequential Record was previously running, otherwise FALSE.

## **DESCRIPTION**

Suspends the recording of data to the Sequential Record before the buffer is filled. If BTICard\_SeqResume is subsequently called, recording is resumed at the point at which it was stopped without overwriting previous records. If BTICard\_SeqStart is called after BTICard\_SeqStop, recording starts at the beginning of the buffer and previous data is overwritten.

### **DEVICE DEPENDENCY**

Applies to all Devices.

**WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqStart, BTICard\_SeqResume