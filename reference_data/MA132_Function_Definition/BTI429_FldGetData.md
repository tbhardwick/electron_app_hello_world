# **FldGetData**

```
ULONG BTI429_FldGetData
(
      ULONG msg //32-bit ARINC 429 word
)
```
## **RETURNS**

The 24-bit field including both the data and parity of an ARINC 429 word.

#### **DESCRIPTION**

Extracts the 23-bit data and 1-bit parity fields of the ARINC 429 word in *msg*. The extracted 24-bit field is right-shifted and zero-filled as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_FldPutData