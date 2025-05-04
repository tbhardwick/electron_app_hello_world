# **ChStart**

```
BOOL BTI429_ChStart
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
## **RETURNS**

TRUE if the channel was previously enabled, otherwise FALSE.

#### **DESCRIPTION**

Enables the operation of the channel specified by *channel*. If it is a transmit channel, the Schedule restarts at the beginning. The channel remains enabled until BTI429\_ChStop is called or a HALT Command Block is encountered in the transmit Schedule. If the Device is stopped, then channel activity begins when the Device is started with BTICard\_CardStart.

BTI429\_ChStart and BTI429\_ChStop enable and disable a channel. BTI429\_ChConfig initializes the channel as either enabled (default) or disabled. These functions allow the channel to be stopped and reconfigured with different settings while other channels are running.

### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

Do not confuse this channel enable with either channel pause or the card-level controls. Channel pause is controlled by BTI429\_ChConfig, BTI429\_ChPause, and BTI429\_ChResume. Cardlevel controls are activated through BTICard\_CardStart, BTICard\_CardStop, and BTICard\_CardResume.

## **SEE ALSO**

BTI429\_ChStop, BTICard\_CardStart, BTICard\_CardStop, BTI429\_SchedHalt