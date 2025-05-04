# **ExtDIORd**

## BOOL **BTICard\_ExtDIORd**

```
(
      INT dionum, //Specifies the DIO number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

Status of the digital I/O pin. Returns a zero if the pin is inactive or a one if the pin is active.

## **DESCRIPTION**

Reads the status of the digital I/O pin specified by *dionum*.

### **DEVICE DEPENDENCY**

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

### **WARNINGS**

None.

### **SEE ALSO**

BTICard\_ExtDIOWr, BTICard\_ExtDIOENWr