# **BCDPutVal**

```
ULONG BTI429_BCDPutVal
(
     LPCSTR buf, //Pointer to buffer containing ASCII string
     ULONG msg, //32-bit BCD word
     USHORT sigdig, //Number of significant digits
     LPCSTR resolstr //Pointer to resolution string
)
```
#### **RETURNS**

The new 32-bit BCD word with the data field inserted.

#### **DESCRIPTION**

Inserts a new value into the data field of the BCD word specified by *msg*. *buf* holds an ASCII string that contains the value to insert. It may contain a decimal point and may be signed.

*sigdig* specifies the number of significant digits (characters) in the BCD data field. *resolstr* points to a null-terminated ASCII string specifying the resolution of the BCD data. This resolution string is a decimal real number that represents the value of the least significant digit (e.g., 0.1). It may contain a decimal point if needed, but should not have a sign or exponent.

The BCD data field is assumed to be divided into the following fields:

![](_page_0_Figure_10.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

Per the ARINC 429 specification, the *sigdig* value is defined for the following number of significant digits: 3, 4, 5, and 6. Use of functions

BTI429\_BCDPutData and BTI429\_BCDPutSign and additional scaling by the resolution value may be required for non-standard data formats.

### **SEE ALSO**

```
BTI429_BCDGetVal, BTI429_BCDPutData, BTI429_BCDPutSign
```