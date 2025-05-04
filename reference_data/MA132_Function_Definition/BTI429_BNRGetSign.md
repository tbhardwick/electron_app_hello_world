# **BNRGetSign**

USHORT BTI429\_BNRGetSign ( ULONG *msg //BNR word to extract data from* )

# **RETURNS**

A non-zero value if sign of BNR word is negative, otherwise zero if the sign is positive.

# **DESCRIPTION**

Extracts the sign of the BNR word specified in *msg*. The result is non-zero if the sign of the BNR word is negative (bit 29 of the SSM field is non-zero). Otherwise, the function returns a zero value.

The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_0_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BNRPutSign, BTI429\_BNRGetSSM