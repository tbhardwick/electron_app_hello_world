# **FldPutValue**

```
ULONG BTI429_FldPutValue
(
     ULONG msg, //32-bit ARINC 429 word
     ULONG data, //New 23-bit data field value
     USHORT startbit, //Starting bit position of field
     USHORT endbit //Ending bit position of field
)
```
#### **RETURNS**

The new 32-bit ARINC 429 word with the specified field inserted.

# **DESCRIPTION**

Inserts a bit field value into the ARINC 429 word in *msg*. *startbit* (zero-based) and *endbit* (zerobased) specify the low and high bit positions of the field.

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

# **SEE ALSO**

BTI429\_FldGetValue