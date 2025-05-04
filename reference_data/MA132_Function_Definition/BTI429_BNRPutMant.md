# **BNRPutMant**

```
ULONG BTI429_BNRPutMant
(
     ULONG msg, //32-bit BNR word
     ULONG value, //New data value
     USHORT sigbit, //Number of significant bits
     USHORT twos //Two's complement field
)
```
#### **RETURNS**

The new 32-bit BNR word with the data field inserted.

### **DESCRIPTION**

Inserts the mantissa value into the data field of the BNR word specified in *msg*. *sigbit* specifies the number of significant bits in the BNR field. *value* is converted to BNR and inserted into the data field.

The function assumes the BNR data field is left-adjusted at bit 28 as shown below:

| PARITY<br>SSM |  |  |  |  |  |  |  | BNR DATA |                                                                                                          |  |  |  |  |  |  |  | SDI |  |  |  |  |  |  | LABEL |  |  |  |           |            |
|---------------|--|--|--|--|--|--|--|----------|----------------------------------------------------------------------------------------------------------|--|--|--|--|--|--|--|-----|--|--|--|--|--|--|-------|--|--|--|-----------|------------|
|               |  |  |  |  |  |  |  |          | 32 13 130 29 28 27 26 25 24 123 122 12 120 19 18 17 16 15 14 13 12 11 10 1 9 1 1 2 1 3 1 1 2 1 3 1 6 1 7 |  |  |  |  |  |  |  |     |  |  |  |  |  |  |       |  |  |  |           | । (8)      |
|               |  |  |  |  |  |  |  |          |                                                                                                          |  |  |  |  |  |  |  |     |  |  |  |  |  |  |       |  |  |  | ARINC 429 | BIT NUMBER |

If *twos* is non-zero, then the two's complement of *value* is inserted into the data field, and bit 29 of the SSM field is set to one. Otherwise, bit 29 of the SSM field is set to zero.

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

The *sigdig* values may range from 1 to 20 bits. No resolution scaling is done by this function. .

# **SEE ALSO**

BTI429\_BNRGetMant