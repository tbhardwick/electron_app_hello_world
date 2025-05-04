# **SeqFindNext708**

```
ERRVAL BTICard_SeqFindNext708
(
      LPSEQRECORD708 *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

# **DESCRIPTION**

Finds the next ARINC 708 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the 708 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD708 allows for easy handling of the data.

| SEQRECORD708 structure |        |                                                                  |              |
|------------------------|--------|------------------------------------------------------------------|--------------|
| Field                  | Size   | Description                                                      | Version      |
| type                   | USHORT | The protocol and version number of this record                   | All versions |
| count                  | USHORT | Size of the record, used to parse the data-stream                | All versions |
| timestamp              | ULONG  | Lower 32 bits of the time-tag value                              | All versions |
| activity               | USHORT | Activity (see table below for details)                           | All versions |
| datacount              | USHORT | Number of data words                                             | All versions |
| data[100]              | USHORT | ARINC 708 data word values (100 16-bit data words)               | All versions |
| extra[16]              | USHORT | Additional data (if variable bit mode is enabled)                | 1 or greater |
| bitcount               | USHORT | Number of bits in this message (if variable bit mode is enabled) | 1 or greater |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value                              | 1 or greater |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity field may be tested by AND-ing the value returned with the constants from the table below:

| ARINC 708 activity field |                                                                                          |  |
|--------------------------|------------------------------------------------------------------------------------------|--|
| Constant                 | Description                                                                              |  |
| MSGACT708_CHMASK         | The channel number. Shift the result right by MSGACT708_CHSHIFT.                         |  |
| MSGACT708_CHSHIFT        | Channel number shift value. See CHMASK above.                                            |  |
| MSGACT708_ERR            | This bit is set if any of the error bits are set.                                        |  |
| MSGACT708_DSYNC          | No data sync at end of word.                                                             |  |
| MSGACT708_MANCH          | Manchester error.                                                                        |  |
| MSGACT708_WORD           | Word error.                                                                              |  |
| MSGACT708_LONG           | Long word error: A word of more than 1600 bits was detected.                             |  |
| MSGACT708_SHORT          | Short word error: A word of less than 1600 bits was detected.                            |  |
| MSGACT708_TO             | Time out error: The decoder timed out while receiving a word.                            |  |
| MSGACT708_HIT            | Indicates that this message was transmitted or received since this bit was last cleared. |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT708\_CHMASK and right-shifting the result by MSGACT708\_CHSHIFT. The resulting value is the channel number associated with the ARINC 708 record.

channel = (activity & MSGACT708\_CHMASK) >> MSGACT708\_CHSHIFT;

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

# **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

# **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit