# **ChPause**

```
VOID BTI429_ChPause
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

## **DESCRIPTION**

Pauses the operation of the channel specified by *channel*. All activity on the channel ceases. The channel remains paused until the channel is resumed by BTI429\_ChResume. BTI429\_ChConfig initializes the channel as either unpaused (default) or paused.

*Note: A transmit channel can also be paused when the Device encounters a PAUSE Command Block in the transmit Schedule.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

Do not confuse this channel pause with either channel enable or the card-level controls. Channel enable is controlled by BTI429\_ChConfig, BTI429\_ChStart, and BTI429\_ChStop. Card-level controls are activated through BTICard\_CardStart, BTICard\_CardStop, and BTICard\_CardResume.

### **SEE ALSO**

BTI429\_ChResume, BTI429\_SchedPause