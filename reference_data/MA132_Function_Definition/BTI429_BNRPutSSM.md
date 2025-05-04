# **BNRPutSSM**

```
ULONG BTI429_BNRPutSSM
(
      ULONG msg, //32-bit BNR word
      USHORT value //3-bit value of SSM field
)
```
### **RETURNS**

The new 32-bit BNR word with the SSM field inserted.

### **DESCRIPTION**

Inserts *value* into the 3-bit SSM field of the BNR word specified by *msg*. The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_0_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

The sign is generally specified by bit 29. Changing this value may change the sign.

## **SEE ALSO**

BTI429\_BNRGetSSM, BTI429\_BNRGetSign