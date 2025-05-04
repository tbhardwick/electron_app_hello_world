# **BCDGetSSM**

USHORT BTI429\_BCDGetSSM ( ULONG *msg //BCD word to extract data from* )

## **RETURNS**

Value of the 2-bit SSM field.

## **DESCRIPTION**

Extracts the SSM field of the BCD word in *msg*. The function assumes the SSM field is located at bits 30 through 31 as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_BCDPutSSM