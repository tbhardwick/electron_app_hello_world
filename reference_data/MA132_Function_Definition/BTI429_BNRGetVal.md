# **BNRGetVal**

```
VOID BTI429_BNRGetVal
(
     LPSTR buf, //Pointer to buffer to receive ASCII string
     ULONG msg, //BNR word to extract data from
     USHORT sigbit, //Number of significant bits
     LPCSTR resolstr //Pointer to resolution string
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Extracts the data field of the BNR word specified in *msg* and calculates the value. The resulting ASCII string is written to *buf*. This string may contain a decimal point and a negative sign.

*sigbit* specifies the number of significant bits in the BNR data field. *resolstr* points to a nullterminated ASCII string specifying the resolution of the BNR data. This resolution string is a decimal real number that represents the value of the least significant bit (e.g., 0.25). It may contain a decimal point if needed, but should not have a sign or exponent.

The function assumes the data field is left-adjusted at bit 28 as shown below:

| PARITY                                                                                                                    | SSM |  |  | BNR DATA |  |  |  |  |  |  |  |  | SDI |  |  |  |  |  |  | LABEL |  |  |  |           |  |  |            |
|---------------------------------------------------------------------------------------------------------------------------|-----|--|--|----------|--|--|--|--|--|--|--|--|-----|--|--|--|--|--|--|-------|--|--|--|-----------|--|--|------------|
| 32 1 31 30 29 1 27 1 26 1 25   24   23   20   19   18   17   16   15   12   11   10   9   1   2   3   4   5   6   7   ( 0 |     |  |  |          |  |  |  |  |  |  |  |  |     |  |  |  |  |  |  |       |  |  |  |           |  |  |            |
|                                                                                                                           |     |  |  |          |  |  |  |  |  |  |  |  |     |  |  |  |  |  |  |       |  |  |  | ARINC 429 |  |  | T NI IMBER |

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

The *sigdig* values may range from 1 to 20 bits. The sign of the result is determined by the SSM fields as described by the BTI429\_BNRGetSign function.

## **SEE ALSO**

BTI429\_BNRPutVal, BTI429\_BNRGetSign