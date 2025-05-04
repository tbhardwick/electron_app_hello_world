# **BCDPutSign**

```
ULONG BTI429_BCDPutSign
(
      ULONG msg, //32-bit BCD word
      USHORT sign //Sign for SSM field
)
```
### **RETURNS**

The new 32-bit BCD word with the SSM field inserted.

#### **DESCRIPTION**

Inserts *sign* into the SSM field of the BCD word specified in *msg*. If *sign* is non-zero, the value 11 (binary) is inserted into the SSM field to specify a signed value. Otherwise, the value 00 (binary) is inserted into the SSM field.

The function assumes the SSM field is located at bits 30 through 31 as shown below:

![](_page_0_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

# **SEE ALSO**

BTI429\_BCDGetSign