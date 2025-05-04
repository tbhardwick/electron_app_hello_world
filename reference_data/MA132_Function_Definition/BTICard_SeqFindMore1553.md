# **SeqFindMore1553**

```
ERRVAL BTICard_SeqFindMore1553
(
      LPSEQRECORDMORE1553 *pRecMore, //Address of pointer to a structure
      LPSEQRECORD1553 pRecBase //Pointer to a structure
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Finds the extra fields at the end of a MIL-STD-1553 record in the Sequential Record buffer pointed to by *pRecBase* and updates *\*pRecMore* to point to those fields. BTICard\_SeqFindNext1553 must be called before each call to BTICard\_SeqFindMore1553 to seed the *pRecBase* structure with the first portion of a 1553 message. Repeatedly calling

BTICard\_SeqFindNext1553 and BTICard\_SeqFindMore1553 returns the 1553 records in the Sequential Record one at a time until the end of the buffer is reached (at which time BTICard\_SeqFindNext1553 returns an error value).

Part of the time-tag and the measured RT response times are recorded in the extra fields, as shown in the table below.

| SEQRECORDMORE1553 structure |        |                                              |                          |
|-----------------------------|--------|----------------------------------------------|--------------------------|
| Field                       | Size   | Description                                  | Version (of base record) |
| timestamph                  | ULONG  | Upper 32 bits of the time-tag value          | 1 or greater             |
| resptime1                   | USHORT | First RT response time (in units of 0.1 µs)  | 1 or greater             |
| resptime2                   | USHORT | Second RT response time (in units of 0.1 µs) | 1 or greater             |

# **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices, which add extra fields to 1553 records of version 1 or greater. The version number of the base record pointed to by *pRecBase* can be tested using BTICard\_SeqFindCheckVersion.

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

### **WARNINGS**

BTICard\_SeqFindNext1553 must be called before each call to BTICard\_SeqFindMore1553.

# **SEE ALSO**

BTICard\_SeqFindInit, BTICard\_SeqFindNext1553