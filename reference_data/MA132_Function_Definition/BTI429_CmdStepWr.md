# **CmdStepWr**

```
ERRVAL BTI429_CmdStepWr
(
     BOOL value, //Value of step bit
     SCHNDX index, //Schedule index of item to step
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Sets the step bit to *value* for the schedule entry specified by *index*. When set to TRUE, the step bit instructs the schedule to pause after processing the specified opcode. The step bit is FALSE (disabled) by default.

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

The user is especially cautioned to consider the effects of stepping through opcodes that depend upon or generate hardware timing.

# **SEE ALSO**

BTI429\_CmdStepRd