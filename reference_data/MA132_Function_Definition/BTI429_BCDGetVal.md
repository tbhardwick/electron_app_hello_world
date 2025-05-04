# **BCDGetVal**

```
VOID BTI429_BCDGetVal
(
     LPSTR buf, //Pointer to buffer to receive ASCII string
     ULONG msg, //BCD word to extract data from
     USHORT sigdig, //Number of significant digits
     LPCSTR resolstr //Pointer to resolution string
)
```
#### **RETURNS**

None.

### **DESCRIPTION**

Extracts the data field of the BCD word specified in *msg* and calculates the value. The resulting ASCII string is written to *buf*. This string may contain a decimal point and may be signed.

*sigdig* specifies the number of significant digits (characters) in the BCD data field. *resolstr* points to a null-terminated ASCII string specifying the resolution of the BCD data. This resolution string is a decimal real number that represents the value of the least significant digit (e.g., 0.1). It may contain a decimal point if needed, but should not have a sign or exponent.

The function assumes the BCD data field is divided into the following fields:

![](_page_0_Figure_10.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

Per the ARINC 429 specification, the *sigdig* value is defined for the following number of significant digits: 3, 4, 5, and 6. Use of functions BTI429\_BCDGetData and BTI429\_BCDGetSign and additional scaling by the resolution value may be required for non-standard data formats.

## **SEE ALSO**

BTI429\_BCDPutVal, BTI429\_BCDGetData, BTI429\_BCDGetSign