# **BNRPutSign**

```
ULONG BTI429_BNRPutSign
(
      ULONG msg, //32-bit BNR word
      USHORT twos //Two's complement field
)
```
## **RETURNS**

The new 32-bit BNR word with the SSM field inserted.

### **DESCRIPTION**

Inserts *twos* into the SSM field of the BNR word specified in *msg*. If *twos* is non-zero, then bit 29 of the SSM field is set to one. Otherwise, bit 29 of the SSM field is set to zero.

The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_0_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

## **SEE ALSO**

BTI429\_BNRGetSign, BTI429\_BNRPutSSM