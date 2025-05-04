# **SchedReturn**

```
SCHNDX BTI429_SchedReturn
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

### **DESCRIPTION**

Appends a RETURN Command Block to the current end of the Schedule. A RETURN Command Block returns the Schedule to the point at which the last CALL command was made. For every CALL command there must be a RETURN command to insure proper operation.

### **DEVICE DEPENDENCY**

Does not apply to 5G Devices.

### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

## **SEE ALSO**

BTI429\_SchedCall