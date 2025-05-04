# **FldGetSDI**

USHORT BTI429\_FldGetSDI ( ULONG *msg //32-bit ARINC 429 word* )

## **RETURNS**

The 2-bit SDI field of an ARINC 429 word.

### **DESCRIPTION**

Extracts the 2-bit SDI field of the ARINC 429 word in msg. The extracted SDI field is right-shifted and zero-filled as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldPutSDI