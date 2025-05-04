# **SeqFindCheckVersion**

```
BOOL BTICard_SeqFindCheckVersion
(
      LPUSHORT pRecord, //Pointer to a record
      USHORT version, //Version number to test
)
```
#### **RETURNS**

TRUE if record pointed to by *pRecord* is equal to or greater than the version number represented by *version*, otherwise FALSE.

#### **DESCRIPTION**

Checks to see if the version number of the record pointed to by *pRecord* is equal to or greater than the constant passed for *version*. Use this function to test the eligibility of a given record for a version-dependent application of a BTICard\_SeqFindMore?? function.

| version  |                             |
|----------|-----------------------------|
| Constant | Description                 |
| SEQVER_0 | Sequential Record Version 0 |
| SEQVER_1 | Sequential Record Version 1 |

## **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

## **SEE ALSO**

BTICard\_SeqFindInit, BTICard\_SeqFindNext??