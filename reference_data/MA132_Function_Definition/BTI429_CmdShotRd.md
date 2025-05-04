# **CmdShotRd**

```
BOOL BTI429_CmdShotRd
(
     SCHNDX index, //Schedule index of item to read
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the single-shot bit is set, otherwise FALSE if not set.

#### **DESCRIPTION**

Reads the value of the single-shot bit for the schedule opcode specified by *index*.

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

## **SEE ALSO**

BTI429\_CmdShotWr