# **TSMIRIGControlWr**

```
ERRVAL BTICard_TSMIRIGControlWr
(
```

```
ULONG ctrlval, //Value for outgoing IRIG control field
HCORE handleval //Device handle
```
## **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the value to the Device to send in the "Control Function" field of outgoing IRIG messages. Control Function bits 10 to 27 are taken from bits 0 to 17 of the *ctrlval*, with Control Function bit 10 located at *ctrlval* bit 0.

![](_page_0_Figure_9.jpeg)

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

**WARNINGS**

None.

### **SEE ALSO**

```
BTICard_TSMIRIGControlRd, BTICard_TSMIRIGYearsWr, BTICard_TSMIRIGYearsRd, 
BTICard_TSMSourceConfig
```