# **MsgDataRd**

```
ULONG BTI429_MsgDataRd
(
      MSGADDR message, //Message from which to read
      HCORE hCore //Core handle
)
```
## **RETURNS**

32-bit value of the ARINC data word.

## **DESCRIPTION**

Reads the 32-bit value of the ARINC data word from the Message Record specified by *message*.

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

```
BTI429_MsgDataWr, BTI429_MsgCreate, BTI429_MsgBlockRd, BTI429_MsgCommRd
BTI429_FilterSet, BTI429_FilterDefault
```