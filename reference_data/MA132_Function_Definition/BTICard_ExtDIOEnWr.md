# **ExtDIOEnWr**

## BOOL **BTICard\_ExtDIOEnWr**

```
(
INT dionum, //Specifies the DIO number
BOOL dioval, //The value to set
BOOL dioen, //The output enable to set
HCORE hCore //Core handle
```
#### )

# **RETURNS**

None.

### **DESCRIPTION**

Sets the digital I/O pin specified by dionum to the value specified by dioval with an output enable specified by dioen*.* A dioen of zero tri-states the pin, and a dioen of one sets the pin to the value specified by dioval. A dioval of zero sets the pin to inactive, and a *dioval* of one sets the pin to active.

## **DEVICE DEPENDENCY**

Only applies to 6G Devices. If the DIO referenced by *dionum* cannot be tri-stated, then *dioen* will be ignored.

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

Some discretes are avionics discretes while others are digital I/O discretes. When using the digital I/O as an output (*dioen* set to one), do not drive the digital I/O pin from an external source as this may damage the Device. Please consult the hardware manual for the Device.

#### **SEE ALSO**

BTICard\_ExtDIORd, BTICard\_ExtDIOWr