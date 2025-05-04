# **ChStop**

```
BOOL BTI429_ChStop
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
### **RETURNS**

TRUE if the channel was previously enabled, otherwise FALSE.

#### **DESCRIPTION**

Disables operation of the channel specified by *channel*. If a message is being sent or received, the processing is allowed to finish before the channel is halted. Use BTI429\_ChStart to reenable the channel.

BTI429\_ChStart and BTI429\_ChStop enable and disable a channel. BTI429\_ChConfig initializes the channel as either enabled (default) or disabled. These functions allow the channel to be stopped and reconfigured with different settings while other channels are running.

*Note: A transmit channel can also be stopped when the Device encounters a HALT Command Block in the transmit Schedule.* 

### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Do not confuse this channel enable with either channel pause or the card-level controls. Channel pause is controlled by BTI429\_ChConfig, BTI429\_ChPause, and BTI429\_ChResume. Cardlevel controls are activated through:

BTICard\_CardStart, BTICard\_CardStop BTICard\_CardResume

## **SEE ALSO**

```
BTI429_ChStart, BTI429_SchedHalt, BTICard_CardStart, BTICard_CardStop
```