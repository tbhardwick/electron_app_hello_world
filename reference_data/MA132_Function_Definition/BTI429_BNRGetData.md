# **BNRGetData**

```
ULONG BTI429_BNRGetData
(
      ULONG msg, //BNR word to extract data from
      USHORT msb, //Most significant bit of BNR field
      USHORT lsb //Least significant bit of BNR field
)
```
**RETURNS**

32-bit value of data field containing up to a maximum of 20 bits (18 bits from the BCD data field plus the 2-bit SDI field).

## **DESCRIPTION**

Extracts the data field of the BNR word in *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the BNR

data field respectively. The specified bits are converted to a 32-bit unsigned value. No other conversions, such as resolution or sign, are made.

The function assumes the BNR word has the following format:

![](_page_0_Figure_10.jpeg)

The bits from the parity and SSM fields are not be included in the return value. When specifying a *lsb* value in the SDI field (bits 9 or 10), the bits are treated as data for this function.

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

*lsb* and *msb* values are limited to the BNR data/SDI field (bits 9 through 28).

## **SEE ALSO**

```
BTI429_BNRPutData, BTI429_BNRGetSign
```