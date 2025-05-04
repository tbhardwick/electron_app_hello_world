# **BNRPutData**

```
ULONG BTI429_BNRPutData
(
      ULONG msg, //32-bit BNR word
      ULONG value, //New data value
      USHORT msb, //Most significant bit of BNR field
      USHORT lsb //Least significant bit of BNR field
)
```
#### **RETURNS**

The new 32-bit BNR word with the data field inserted.

#### **DESCRIPTION**

Inserts *value* into the data field of the BNR word specified by *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the field respectively. *value* is converted to BNR and inserted into the specified bits (within bits 9 through 28). No other conversion, such as

resolution or sign, is made.

The function assumes the BNR word has the following format:

![](_page_0_Figure_10.jpeg)

The bits from the parity and SSM fields are not modified. When specifying a *lsb* value in the SDI field (bits 9 or 10), the bits are treated as data for this function.

*Note: This is a utility function and does not access any Device hardware.*

### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

*lsb* and *msb* values are limited to the BNR data/SDI field (bits 9 through 28). A maximum of 20 bits (when *msb*=28 and *lsb*=9) of *value* is used by this function.

## **SEE ALSO**

BTI429\_BNRGetData, BTI429\_BNRPutSSM, BTI429\_BNRPutSign