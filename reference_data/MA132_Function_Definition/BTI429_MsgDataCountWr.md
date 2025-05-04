# **MsgDataCountWr**

```
ERRVAL BTI429_MsgDataCountWr
(
      ULONG dataval, //Value of data to write to message
      INT bitcount, //Number of bits of dataval to transmit (1-32)
      MSGADDR message, //Message to receive new data and bitcount
      HCORE hCore //Core handle
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Writes the 32-bit ARINC data value specified by *dataval* into the Message Record specified by *message*. Also writes the *bitcount* value into the *paramflag* field of the Message Record specified by *message*. *bitcount* indicates the number of ARINC 429 data word bits the channel will transmit for applications of less than 32 bits.

## **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_MsgCountRd, BTI429\_MsgDataRd, BTI429\_MsgBlockRd