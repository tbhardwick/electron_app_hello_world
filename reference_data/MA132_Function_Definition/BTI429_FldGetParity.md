# **FldGetParity**

USHORT BTI429\_FldGetParity ( ULONG *msg //32-bit ARINC 429 word* )

## **RETURNS**

The parity bit of an ARINC 429 word.

## **DESCRIPTION**

Extracts the parity bit of the ARINC 429 word in *msg*. The extracted parity bit is right-shifted and zero-filled as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_FldGetData