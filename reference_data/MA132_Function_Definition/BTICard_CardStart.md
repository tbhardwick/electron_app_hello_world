# **CardStart**

```
ERRVAL BTICard_CardStart
(
```

```
HCORE hCore //Core handle
)
```
## **RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Activates all configured channels of the specified core. The Sequential Monitor and Event Log List are cleared and begin operation at the start of their allocated buffers.

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

The core continues operating even after an application program ends unless BTICard\_CardStop halts it. Even after BTICard\_CardStart, individual channels may not transmit or receive if they are disabled or paused. See the channel configuration and control functions for each protocol.

## **SEE ALSO**

BTICard\_CardStop, BTICard\_CardIsRunning