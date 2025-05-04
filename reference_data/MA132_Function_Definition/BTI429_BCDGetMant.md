# **BCDGetMant**

```
ULONG BTI429_BCDGetMant
(
     ULONG msg, //BCD word to extract data from
     USHORT sigdig //Number of significant digits
)
```
## **RETURNS**

The data field mantissa.

#### **DESCRIPTION**

Extracts the mantissa value from the data field of the BCD word specified in *msg*. *sigdig* specifies the number of significant BCD digits (characters) in the data field. The result is converted to a 32 bit unsigned value and returned. No other conversion, such as scaling by the resolution value, is made.

The function assumes the BCD data field is divided into the following fields:

![](_page_0_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

Per the ARINC 429 specification, the *sigdig* value is defined for the following number of significant digits: 3, 4, 5, and 6. Use the BTI429\_BCDGetData function for other non-standard data formats.

## **SEE ALSO**

BTI429\_BCDPutMant