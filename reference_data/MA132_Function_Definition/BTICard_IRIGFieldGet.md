# **IRIGFieldGet??**

```
ULONG BTICard_IRIGFieldGetDays
ULONG BTICard_IRIGFieldGetHours
ULONG BTICard_IRIGFieldGetMin
ULONG BTICard_IRIGFieldGetSec
ULONG BTICard_IRIGFieldGetMillisec
ULONG BTICard_IRIGFieldGetMicrosec
(
      ULONG irigvalh, //Upper 32 bits of the 64-bit BCD IRIG time-tag
      ULONG irigvall, //Lower 32 bits of the 64-bit BCD IRIG time-tag
)
```
**RETURNS**

The integer value of the IRIG field for which the function is named.

#### **DESCRIPTION**

Extracts the specified BCD field from the 64-bit IRIG time-tag, converts it to an integer, and returns the integer.

An IRIG time-tag is divided into the following BCD fields:

| RSVD                     | DAYS | HOURS | MINUTES                                                                                                           |
|--------------------------|------|-------|-------------------------------------------------------------------------------------------------------------------|
|                          |      |       |                                                                                                                   |
|                          |      |       | 64   63   61   60   59   58   57   56   52   51   50   48   47   46   41   40   39 ' 38 ' 37   36 ' 5 ' 34 ' 35 ' |
|                          |      |       |                                                                                                                   |
| irigvalh (upper 32 bits) |      |       |                                                                                                                   |

| SECONDS                  | MILLISECONDS                                                                                                               | MICROSECONDS |  |  |
|--------------------------|----------------------------------------------------------------------------------------------------------------------------|--------------|--|--|
|                          |                                                                                                                            |              |  |  |
|                          | 32   31   30   28   27   26   23   20   10   18   17   16   15   4   13   12   11   10   9   8   7   6   5   4   3   2   1 |              |  |  |
| iriaval1 (lower 32 hits) |                                                                                                                            |              |  |  |

*Note: These are utility functions and do not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTICard\_IRIGFieldPut??