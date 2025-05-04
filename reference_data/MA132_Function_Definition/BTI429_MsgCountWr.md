# **MsgCountWr**

```
ERRVAL BTI429_MsgCountWr
(
      INT bitcount, //Number of bits of data to transmit (1-32)
      MSGADDR message, //Message to receive new data and bitcount
      HCORE hCore //Core handle
)
```
## **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Writes the *bitcount* value into the *paramflag* field of the Message Record specified by *message*. *bitcount* indicates the number of ARINC 429 data word bits the channel will transmit for applications of less than 32 bits.

## **DEVICE DEPENDENCY**

Applies to transmit channels of 5G+ and 6G Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_MsgCountRd, BTI429\_MsgDataCountWr, BTI429\_MsgBlockRd