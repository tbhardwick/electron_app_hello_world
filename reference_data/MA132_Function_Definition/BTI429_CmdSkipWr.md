# **CmdSkipWr**

```
ERRVAL BTI429_CmdSkipWr
(
     BOOL value, //Value of skip bit
     SCHNDX index, //Schedule index of item to skip
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Sets the skip bit to *value* for the schedule entry specified by *index*. When set to TRUE, the skip bit instructs the schedule to skip over processing the specified opcode. The skip bit is FALSE (disabled) by default.

### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

## **SEE ALSO**

BTI429\_CmdSkipRd