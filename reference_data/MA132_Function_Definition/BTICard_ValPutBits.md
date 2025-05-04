# **ValPutBits**

### USHORT **BTICard\_ValPutBits**

```
(
     ULONG oldvalue, //The old value
     ULONG newfld, //The value of the new field
     INT startbit, //Position of starting bit of field
     INT endbit //Position of ending bit of field
)
```
### **RETURNS**

The integer value with the inserted bit field.

### **DESCRIPTION**

Inserts a bit field into a 32-bit integer value. The *oldval* is masked and OR-ed with the shifted value of *newfld*. The LSB is bit number zero.

*Note: This is a utility function and does not access the Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ValGetBits