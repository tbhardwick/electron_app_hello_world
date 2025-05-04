# **BCDPutSSM**

```
ULONG BTI429_BCDPutSSM
(
     ULONG msg, //32-bit BCD word
     USHORT value //2-bit value of SSM field
)
```
## **RETURNS**

The new 32-bit BCD word with the SSM field inserted.

## **DESCRIPTION**

Inserts value into the SSM field of the BCD word in msg. The function assumes the SSM field is located at bits 30 through 31 as shown below:

| PARITY<br>SSM | BCD DATA                                                                                       | SDI | LABEL                      |
|---------------|------------------------------------------------------------------------------------------------|-----|----------------------------|
|               | 32 13 130 29 28 27 126 25 22 120 19 18 17 16 15 14 13 12 11 1 10 1 9 1 1 2 1 3 1 4 1 5 1 6 1 7 |     |                            |
|               |                                                                                                |     | ARINC 429<br>NUMBER<br>BIT |

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_BCDGetSSM