# **FldPutLabel**

```
ULONG BTI429_FldPutLabel
(
      ULONG msg, //32-bit ARINC 429 word
      USHORT label //New 8-bit label field value
)
```
### **RETURNS**

The new 32-bit ARINC 429 word with the label field inserted.

### **DESCRIPTION**

Inserts an 8-bit label field value into the ARINC 429 word in *msg*. *label* is packed into *msg* as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

### **SEE ALSO**

BTI429\_FldGetLabel