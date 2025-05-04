# **ChResume**

```
VOID BTI429_ChResume
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
### **RETURNS**

None.

## **DESCRIPTION**

Resumes the operation of the channel specified by *channel* after it has been paused by BTI429\_ChPause or the Device has encountered a PAUSE Command Block in the transmit Schedule. BTI429\_ChConfig initializes the channel as either unpaused (default) or paused. If the Device is running, all activity on the channel will begin. If the Device is stopped, channel activity will begin when the Device is started.

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

Do not confuse this channel pause with either channel enable or the card-level controls. Channel enable is controlled by BTI429\_ChConfig, BTI429\_ChStart, and BTI429\_ChStop. Card-level controls are activated through BTICard\_CardStart, BTICard\_CardStop, and BTICard\_CardResume.

## **SEE ALSO**

BTI429\_ChPause, BTI429\_SchedPause