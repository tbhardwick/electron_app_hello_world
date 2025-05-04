# **BNRGetSSM**

USHORT BTI429\_BNRGetSSM ( ULONG *msg //BNR word to extract SSM field from* )

## **RETURNS**

Value of SSM field.

## **DESCRIPTION**

Extracts the SSM field from the BNR word in msg. The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_0_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_BNRPutSSM, BTI429\_BNRGetSign