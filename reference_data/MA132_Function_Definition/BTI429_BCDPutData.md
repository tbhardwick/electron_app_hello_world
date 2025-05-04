# **BCDPutData**

```
ULONG BTI429_BCDPutData
(
      ULONG msg, //32-bit BCD word
      ULONG value, //New data value
      USHORT msb, //Most significant bit of BCD field
      USHORT lsb //Least significant bit of BCD field
)
```
#### **RETURNS**

The new 32-bit BCD word with the data field inserted.

### **DESCRIPTION**

Inserts *value* into the data field of the BCD word specified by *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the field respectively. *value* is converted to BCD and inserted into the specified bits (within bits 9 through 29). No other conversion is made.

The function assumes the BCD word has the following format:

![](_page_0_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

*lsb* and *msb* values are limited to the BCD data/SDI field (bits 9 through 29). A maximum of 21 bits (when *msb*=29 and *lsb*=9) of *value* is used by this function.

# **SEE ALSO**

BTI429\_BCDGetData