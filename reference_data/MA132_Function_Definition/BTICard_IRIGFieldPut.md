# **IRIGFieldPut??**

```
VOID BTICard_IRIGFieldPutDays
VOID BTICard_IRIGFieldPutHours
VOID BTICard_IRIGFieldPutMin
VOID BTICard_IRIGFieldPutSec
VOID BTICard_IRIGFieldPutMillisec
VOID BTICard_IRIGFieldPutMicrosec
(
      ULONG value, //Field value to write to the BCD IRIG time
      LPULONG irigvalh, //Pointer to a variable for the upper 32 bits
      LPULONG irigvall, //Pointer to a variable for the lower 32 bits
)
```
### **RETURNS**

Nothing.

#### **DESCRIPTION**

Converts an integer (*value*) to BCD and inserts the BCD value into the specified field in the 64-bit IRIG time-tag.

| An IRIG time-tag is divided into the following BCD fields: |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|------------------------------------------------------------|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|
|                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |

| SECONDS                  | MILLISECONDS | MICROSECONDS                                                                                                               |  |  |  |  |  |  |  |  |
|--------------------------|--------------|----------------------------------------------------------------------------------------------------------------------------|--|--|--|--|--|--|--|--|
|                          |              |                                                                                                                            |  |  |  |  |  |  |  |  |
|                          |              | 32   30   28   27   26   25   24   23   22   20   19   18   17   16   13   2   11   10   9   8   7 ` 6 ` 5   4 ` 3 ` 2 ` 1 |  |  |  |  |  |  |  |  |
|                          |              |                                                                                                                            |  |  |  |  |  |  |  |  |
| iriarall (lower 32 hits) |              |                                                                                                                            |  |  |  |  |  |  |  |  |

*Note: These are utility functions and do not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTICard\_IRIGFieldGet??