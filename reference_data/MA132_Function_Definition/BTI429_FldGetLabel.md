# **FldGetLabel**

USHORT BTI429\_FldGetLabel ( ULONG *msg //32-bit ARINC 429 word* )

## **RETURNS**

The 8-bit label field of an ARINC 429 word.

## **DESCRIPTION**

Extracts the 8-bit label field by masking the ARINC 429 word in *msg* as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldPutLabel