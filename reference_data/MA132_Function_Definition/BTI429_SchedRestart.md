# **SchedRestart**

```
SCHNDX BTI429_SchedRestart
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a RESTART Command Block to the current end of the Schedule. A RESTART Command Block restarts the Schedule back at the beginning. A RESTART Command Block is automatically appended to the end of the Schedule, so this function does not need to be called for simple Schedules.

*Note: Execution of this function does NOT restart the Schedule. The Schedule is restarted only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

## **SEE ALSO**

BTI429\_SchedEntry