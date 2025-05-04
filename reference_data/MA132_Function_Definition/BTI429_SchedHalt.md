# **SchedHalt**

```
SCHNDX BTI429_SchedHalt
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
## **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

## **DESCRIPTION**

Appends a HALT Command Block to the current end of the Schedule. A HALT Command Block stops the Schedule until the channel is re-enabled using BTI429\_ChStart. When a HALT Command Block is encountered, it has the same effect as executing the BTI429\_ChStop function.

*Note: Execution of this function does NOT halt the Schedule. The Schedule is halted only when the resulting Schedule executes the HALT Command Block after BTICard\_CardStart starts the Device.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

## **SEE ALSO**

BTI429\_SchedPause, BTI429\_ChStart, BTI429\_ChStop