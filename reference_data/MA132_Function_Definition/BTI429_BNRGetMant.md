# **BNRGetMant**

```
ULONG BTI429_BNRGetMant
(
     ULONG msg, //BNR word to extract data from
     USHORT sigdig //Number of significant digits
)
```
# **RETURNS**

The data field mantissa (right-adjusted).

#### **DESCRIPTION**

Extracts the mantissa value from the data field of the BNR word specified in *msg*. *sigdig* specifies the number of significant digits in the data field. If the SSM field of *msg* specifies a signed value, then the two's complement of the data field is returned. No other conversion, such as scaling by the resolution value, is made.

The BNR data field is signed if bit 29 in the SSM field is non-zero. The BNR data field is assumed to be left-adjusted at bit 28 as shown below:

| PARITY |  | SSM | BNR DATA |  |  |  |  |  |  |  |  |  |  |  | SDI |  |  |                                                                                                                    |  |  | LABEL |  |  |                         |  |  |  |
|--------|--|-----|----------|--|--|--|--|--|--|--|--|--|--|--|-----|--|--|--------------------------------------------------------------------------------------------------------------------|--|--|-------|--|--|-------------------------|--|--|--|
|        |  |     |          |  |  |  |  |  |  |  |  |  |  |  |     |  |  | 32   31   30   28   27   26   23   22   20   19   18   17   16   13   12   11   10   9   1   2   3   4   5   6   7 |  |  |       |  |  |                         |  |  |  |
|        |  |     |          |  |  |  |  |  |  |  |  |  |  |  |     |  |  |                                                                                                                    |  |  |       |  |  | ARINC 429<br>BIT NUMBER |  |  |  |

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

The *sigdig* values may range from 1 to 20 bits.

# **SEE ALSO**

BTI429\_BNRPutMant