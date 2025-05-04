# **BCDPutMant**

```
ULONG BTI429_BCDPutMant
(
     ULONG msg, //32-bit BCD word
     ULONG value, //New data value
     USHORT sigdig, //Number of significant digits
     USHORT sign //Sign for SSM field
)
```
## **RETURNS**

The new 32-bit BCD word with the data field inserted.

## **DESCRIPTION**

Inserts the mantissa value into the data field of the BCD word specified in *msg*. *sigdig* specifies the number of significant digits (characters) in the BCD field. *value* is converted to BCD and inserted into the data field. The BCD data field is assumed to be divided into the following fields:

![](_page_0_Figure_8.jpeg)

If sign is non-zero, the value 11 (binary) is inserted into the SSM field to denote a signed value. Otherwise, the value 00 (binary) is inserted into the SSM field. No other conversion is made.

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_BCDGetMant