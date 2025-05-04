# **ChPauseCheck**

```
INT BTI429_ChPauseCheck
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
### **RETURNS**

A non-zero value if the channel is paused, or zero if the channel is not paused.

## **DESCRIPTION**

Determines whether the channel specified by *channel* is paused.

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

## **SEE ALSO**

BTI429\_ChPause, BTI429\_ChResume