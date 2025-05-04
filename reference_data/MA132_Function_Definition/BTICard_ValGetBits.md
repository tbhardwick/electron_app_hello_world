# **ValGetBits**

```
ULONG BTICard_ValGetBits
(
     ULONG oldvalue, //The old value
     INT startbit, //Position of starting bit of field
     INT endbit //Position of ending bit of field
)
```
## **RETURNS**

The value of the extracted bit field.

## **DESCRIPTION**

Extracts the specified bit field from the 32-bit integer *oldvalue*. The result is obtained by masking the field and shifting the *endbit* to the LSB of the return value. The LSB is bit number zero.

*Note: This is a utility function and does not access the Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

**WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ValPutBits