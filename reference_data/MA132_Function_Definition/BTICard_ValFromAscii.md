# **ValFromAscii**

```
ULONG BTICard_ValFromAscii
(
      LPCSTR asciistr, //ASCII string to convert
      INT radixval //Radix of string
)
```
#### **RETURNS**

The converted integer numeric value.

#### **DESCRIPTION**

Converts a string representation of a 32-bit value with the specified radix to an integer. Processing stops at the first null terminator. *radixval* can be any positive integer, but is commonly 16 for hexadecimal, 8 for octal, or 10 for decimal.

*Note: This is a utility function and does not access the Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

No check is made for invalid characters.

## **SEE ALSO**

BTICard\_ValToAscii