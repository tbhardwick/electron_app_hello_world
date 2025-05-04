# **BNRPutVal**

```
ULONG BTI429_BNRPutVal
(
     LPCSTR buf, //Pointer to buffer containing ASCII string
     ULONG msg, //32-bit BNR word
     USHORT sigbit, //Number of significant bits
     LPCSTR resolstr //Pointer to resolution string
)
```
### **RETURNS**

The new 32-bit BNR word with the data field inserted.

## **DESCRIPTION**

Inserts *buf* into the data field of the BNR word specified by *msg*. *buf* holds an ASCII string containing the value to insert. This string may contain a decimal point and may be signed.

*sigbit* specifies the number of significant bits in the BNR data field. *resolstr* points to a nullterminated ASCII string specifying the resolution of the BNR data. This resolution string is a decimal real number that represents the value of the least significant bit (e.g., 0.25). It may contain a decimal point if needed, but should not have a sign or exponent.

The function assumes the data field is left-adjusted at bit 28 as shown below:

| PARITY                                                                                                                   | SSM | BNR DATA |  |  |  |  |  |  |  |  |  |  | SDI |  |  |  |  |  |  |  |  | LABEL |  |  |  |  |  |  |  |                         |
|--------------------------------------------------------------------------------------------------------------------------|-----|----------|--|--|--|--|--|--|--|--|--|--|-----|--|--|--|--|--|--|--|--|-------|--|--|--|--|--|--|--|-------------------------|
| 32   31   30   29   27   26   25   24   20   19   18   17   16   15   12   11   10   9   1   2   3   4   5   6   7   (8) |     |          |  |  |  |  |  |  |  |  |  |  |     |  |  |  |  |  |  |  |  |       |  |  |  |  |  |  |  |                         |
|                                                                                                                          |     |          |  |  |  |  |  |  |  |  |  |  |     |  |  |  |  |  |  |  |  |       |  |  |  |  |  |  |  | ARINC 429<br>BIT NUMBER |

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

The *sigdig* values may range from 1 to 20 bits. The sign of the result is determined by the SSM fields as described by the BTI429\_BNRGetSign function.

## **SEE ALSO**

BTI429\_BNRGetVal, BTI429\_BNRPutData, BTI429\_BNRPutSign