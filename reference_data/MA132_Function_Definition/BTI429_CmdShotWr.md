# **CmdShotWr**

```
ERRVAL BTI429_CmdShotWr
(
     BOOL value, //Value of single-shot bit
     SCHNDX index, //Schedule index of item to single-shot
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Sets the single-shot bit to *value* for the schedule entry specified by *index*. When set to TRUE, the single-shot bit instructs the schedule to process the specified opcode one time, and then to set the skip bit after processing is complete. The single-shot bit is FALSE (disabled) by default.

# **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

# **SEE ALSO**

BTI429\_CmdShotRd