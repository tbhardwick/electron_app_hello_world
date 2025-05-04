# **TSMIRIGControlRd**

```
ERRVAL BTICard_TSMIRIGControlRd
(
```

```
LPULONG ctrlptr, //Pointer to variable to hold IRIG control field
HCORE handleval //Device handle
```
# **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the last received "Control Function" field from a received IRIG message and places the value into the location pointed to by *ctrlptr*. Control Function bits 10 to 27 are placed in bits 0 to 17 of the ULONG.

![](_page_0_Figure_9.jpeg)

# **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

None.

### **SEE ALSO**

```
BTICard_TSMIRIGControlWr, BTICard_TSMIRIGYearsWr, BTICard_TSMIRIGYearsRd, 
BTICard_TSMSyncConfig
```