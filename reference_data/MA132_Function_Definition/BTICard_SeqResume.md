# **SeqResume**

```
BOOL BTICard_SeqResume
(
      HCORE hCore //Core handle
)
```
### **RETURNS**

TRUE if the Sequential Record was previously running, otherwise FALSE.

## **DESCRIPTION**

Resumes operation of the Sequential Record at the point at which it was stopped using BTICard\_SeqStop. Use this function to continue recording data to the Sequential Record without overwriting previous records.

### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

A call to BTICard\_SeqStop must precede this function.

### **SEE ALSO**

BTICard\_SeqStart, BTICard\_SeqStop