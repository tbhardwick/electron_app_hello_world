# **FldPutData**

```
ULONG BTI429_FldPutData
(
      ULONG msg, //32-bit ARINC 429 word
      ULONG data //New 23-bit data field value
)
```
#### **RETURNS**

The new 32-bit ARINC 429 word with the data field inserted.

#### **DESCRIPTION**

Inserts a 24-bit field including both data and parity into the ARINC 429 word in *msg*. *data* is leftshifted and packed into *msg* as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_FldGetData