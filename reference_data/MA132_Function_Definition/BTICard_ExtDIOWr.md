# **ExtDIOWr**

```
VOID BTICard_ExtDIOWr
(
      INT dionum, //Specifies the DIO number
      BOOL dioval, //The value to set
      HCORE hCore //Core handle
```
)

# **RETURNS**

None.

# **DESCRIPTION**

Sets the digital I/O pin specified by *dionum* to the value specified by *dioval*. A *dioval* of zero sets the pin to inactive, and a *dioval* of one sets the pin to active.

# **DEVICE DEPENDENCY**

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

Some discretes are avionics discretes while others are digital I/O discretes. When using the digital I/O as an output (as this function does), do not drive the digital I/O pin from an external source as this may damage the Device. Please consult the hardware manual for the Device.

# **SEE ALSO**

BTICard\_ExtDIORd, BTICard\_ExtDIOENWr