# **ChIsRcv**

```
BOOL BTI429_ChIsRcv 
(
      INT channel, //Channel number to test
      HCORE hCore //Core handle
)
```
## **RETURNS**

TRUE if the channel is an ARINC 429 receiver, or FALSE if it is not a receiver.

#### **DESCRIPTION**

Checks to see if the channel number specified by *channel* is a receive channel.

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

If this function returns a value of zero, do not assume that the channel must then be a transmitter, because the channel may not exist at all. A call to BTI429\_ChIsXmt must be made to be sure that the channel is a transmitter.

## **SEE ALSO**

BTI429\_ChIsXmt, BTI429\_ChGetCount, BTI429\_ChIs429