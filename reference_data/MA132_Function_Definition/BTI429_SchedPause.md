# **SchedPause**

```
SCHNDX BTI429_SchedPause
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a PAUSE Command Block to the current end of the Schedule. A PAUSE Command Block pauses the channel specified by channel until the Device is unpaused using BTI429\_ChResume. When a PAUSE Command Block is encountered, it has the same effect as executing the BTI429\_ChPause function.

*Note: Execution of this function does NOT pause the channel. The channel is paused only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

Because of internal prefetching on 3G/4G/5G Devices, when scheduling a SchedPause entry two very small preceding gaps (SchedGap with a gapval of 1) should be inserted. If these additional gaps are not inserted on 3G/4G/5G Devices, the schedule could pause before the preceding message is transmitted.

## **SEE ALSO**

BTI429\_ChPause, BTI429\_ChResume, BTI429\_SchedHalt, BTI429\_SchedRestart