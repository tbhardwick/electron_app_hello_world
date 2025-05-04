# **FldPutSDI**

```
ULONG BTI429_FldPutSDI
(
      ULONG msg, //32-bit ARINC 429 word
      USHORT sdi //New 2-bit SDI field value
)
```
### **RETURNS**

The new 32-bit ARINC 429 word with the SDI field inserted.

#### **DESCRIPTION**

Inserts a 2-bit SDI field value into the ARINC 429 word in *msg*. *sdi* is left-shifted and packed into *msg* as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

## **SEE ALSO**

BTI429\_FldGetSDI