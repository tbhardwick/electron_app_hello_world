# **FldGetValue**

```
ULONG BTI429_FldGetValue
(
     ULONG msg, //32-bit ARINC 429 word
     USHORT startbit, //Starting bit number of BCD field
     USHORT endbit //Ending bit number of BCD field
)
```
## **RETURNS**

The specified field of an ARINC 429 word.

## **DESCRIPTION**

Extracts the value of a specified a bit field from the ARINC 429 word in *msg*. *startbit* (zero-based) and *endbit* (zero-based) determine the lowest and highest bit position of the field to extract. The extracted field is right-shifted and zero-filled.

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_FldPutValue