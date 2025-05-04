# **SeqFindNext**

```
ERRVAL BTICard_SeqFindNext
(
     LPUSHORT *pRecord, //Address of pointer
     LPUSHORT seqtype, //Pointer to variable to receive type value
     LPSEQFINDINFO sfinfo //Pointer to structure
)
```
## **RETURNS**

A negative value if an error occurs or zero if successful.

# **DESCRIPTION**

Finds the next record (*\*pRecord*) in the Sequential Record buffer (regardless of protocol). The protocol for that record is indicated by *seqtype* as shown below. The *sfinfo* structure is also updated.

| seqtype      |                                        |
|--------------|----------------------------------------|
| Constant     | Description                            |
| SEQTYPE_429  | Sequential Record type is ARINC 429    |
| SEQTYPE_717  | Sequential Record type is ARINC 717    |
| SEQTYPE_1553 | Sequential Record type is MIL-STD-1553 |
| SEQTYPE_708  | Sequential Record type is ARINC 708    |
| SEQTYPE_CSDB | Sequential Record type is CSDB         |
| SEQTYPE_DIO  | Sequential Record type is DIO          |
| SEQTYPE_USER | Sequential Record type is user-defined |
| SEQTYPE_EBR  | Sequential Record type is EBR-1553     |

Calling this function repeatedly steps through the records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. To handle the record data, cast the *\*pRecord* value to a structure pointer defined in the protocol-specific BTICard\_SeqFindNext?? functions.

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

To make this function start its search at the beginning of the Sequential Record buffer, the *sfinfo* structure must first be initialized with BTICard\_SeqFindInit. Otherwise, it finds the next record from where it left off.

## **SEE ALSO**

```
BTICard_SeqConfig, BTICard_SeqRd, BTICard_SeqFindInit, 
BTICard_SeqFindNext??
```