# **BCDGetData**

```
ULONG BTI429_BCDGetData
(
      ULONG msg, //BCD word to extract data from
      USHORT msb, //Most significant bit of BCD field
      USHORT lsb //Least significant bit of BCD field
)
```
**RETURNS**

32-bit value of data field containing up to a maximum of 21 bits (19 bits from the BCD data field plus the 2-bit SDI field).

#### **DESCRIPTION**

Extracts the data field of the BCD word in *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the BCD field respectively. The specified bits are converted to a 32-bit unsigned value. No other conversions, such as resolution or sign, are made.

The function assumes the BCD word has the following format:

| PARITY SSM                                                                                       |  |  | BCD DATA |  |  |  |  |  |  |  |  |  |  | SDI |  |  |  |  |  | LABEL |  |  |  |  |  |                         |  |
|--------------------------------------------------------------------------------------------------|--|--|----------|--|--|--|--|--|--|--|--|--|--|-----|--|--|--|--|--|-------|--|--|--|--|--|-------------------------|--|
| 32 13 130 29 28 27 28 25 24 123 122 12 19 18 17 16 15 14 13 12 11 10 19 1 1 2 13 1 4 1 5 1 6 1 7 |  |  |          |  |  |  |  |  |  |  |  |  |  |     |  |  |  |  |  |       |  |  |  |  |  |                         |  |
| s hite from the navity and CCM figlia are not ha included in the return value Jillhon annaith u  |  |  |          |  |  |  |  |  |  |  |  |  |  |     |  |  |  |  |  |       |  |  |  |  |  | ARINC 429<br>BIT NUMBER |  |

The bits from the parity and SSM fields are not be included in the return value. When specifying a *lsb* value in the SDI field (bits 9 or 10), the bits are treated as data for this function.

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*lsb* and *msb* values are limited to the BCD data/SDI field (bits 9 through 29).

## **SEE ALSO**

```
BTI429_BCDPutData, BTI429_BCDGetSign
```