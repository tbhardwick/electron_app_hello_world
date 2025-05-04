# **SeqFindNextEBR**

```
ERRVAL BTICard_SeqFindNextEBR
(
      LPSEQRECORDEBR *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

# **DESCRIPTION**

Finds the next EBR-1553 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data in the *sfinfo* structure. Calling this function repeatedly returns the EBR records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORDEBR allows for easy handling of the data.

| SEQRECORDEBR structure |        |                                                                          |              |  |  |
|------------------------|--------|--------------------------------------------------------------------------|--------------|--|--|
| Field                  | Size   | Description                                                              | Version      |  |  |
| type                   | UINT16 | The protocol and version number of this record                           | All versions |  |  |
| count                  | UINT16 | Word count in record                                                     | All versions |  |  |
| activity               | UINT16 | Activity value (see table below for details)                             | All versions |  |  |
| error                  | UINT16 | Error value (see table below for details)                                | All versions |  |  |
| timestamp              | UINT32 | Lower 32 bits of the time-tag value                                      | All versions |  |  |
| timestamph             | UINT32 | Upper 32 bits of the time-tag value                                      | All versions |  |  |
| cwd                    | UINT16 | Command word value<br>All versions                                       |              |  |  |
| cwdinfo                | UINT16 | RT Link the command word was sent on                                     |              |  |  |
| swd                    | UINT16 | All versions<br>Status wordvalue<br>All versions                         |              |  |  |
| swdinfo                | UINT16 | RT Link the status word was sent on                                      |              |  |  |
| resptime               | UINT16 | All versions<br>RT response time (in 100ths of µs)<br>All versions       |              |  |  |
| datacount              | UINT16 | Number EBR-1553 data words<br>All versions                               |              |  |  |
| data[]                 | UINT16 | Array of EBR data words (don't exceed data[datacount-1])<br>All versions |              |  |  |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity and error fields may be tested by AND-ing the values returned with the constants from the tables below:

| EBR-1553 activity field |                                                                               |  |  |  |  |
|-------------------------|-------------------------------------------------------------------------------|--|--|--|--|
| Constant                | Description                                                                   |  |  |  |  |
| MSGACTEBR_CHMASK        | The channel number mask value. Shift the result right with MSGACTEBR_CHSHIFT. |  |  |  |  |
| MSGACTEBR_CHSHIFT       | Channel number shift value. See CHMASK above.                                 |  |  |  |  |
| MSGACTEBR_LINKMASK      | The RT Link number. Shift the result right by MSGACTEBR_LINKSHIFT.            |  |  |  |  |
| MSGACTEBR_LINKSHIFT     | Link number shift value. See LINKMASK above.                                  |  |  |  |  |
| MSGACTEBR_XMTCWD        | The command word was transmitted.                                             |  |  |  |  |
| MSGACTEBR_XMTSWD        | The status word was transmitted.                                              |  |  |  |  |
| MSGACTEBR_RCVCWD        | The command word was received.                                                |  |  |  |  |
| MSGACTEBR_RCVSWD        | The status word was received.                                                 |  |  |  |  |
| MSGACTEBR_XMTDWD        | Data words were transmitted.                                                  |  |  |  |  |
| MSGACTEBR_RCVDWD        | Data words were received.                                                     |  |  |  |  |
| MSGACTEBR_MODEMASK      | LHub Mode mask value                                                          |  |  |  |  |
| MSGACTEBR_MODESHIFT     | LHub Mode shift value                                                         |  |  |  |  |

| EBR-1553 error field |                                                                                                         |  |  |  |
|----------------------|---------------------------------------------------------------------------------------------------------|--|--|--|
| Constant             | Description                                                                                             |  |  |  |
| MSGERREBR_NORESP     | No response was received from the RT                                                                    |  |  |  |
| MSGERREBR_ANYERR     | Set if any other error bits are set                                                                     |  |  |  |
| MSGERREBR_PROTOCOL   | A protocol error occurred                                                                               |  |  |  |
| MSGERREBR_SYNC       | Wrong polarity of the sync pulse                                                                        |  |  |  |
| MSGERREBR_DATACOUNT  | Too many/too few data words                                                                             |  |  |  |
| MSGERREBR_MANCH      | Manchester error                                                                                        |  |  |  |
| MSGERREBR_PARITY     | Parity error                                                                                            |  |  |  |
| MSGERREBR_WORD       | Word error                                                                                              |  |  |  |
| MSGERREBR_RETRY      | All attempts to retry transmission of this message failed (BC only)                                     |  |  |  |
| MSGERREBR_SYSTEM     | Internal system error occurred                                                                          |  |  |  |
| MSGERREBR_LHUB       | Indicates unexpected LHUB activity was detected                                                         |  |  |  |
| MSGERREBR_HIT        | Indicates that this message was transmitted or received since this bit was last cleared<br>(always set) |  |  |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACTEBR\_CHMASK and right-shifting the result by MSGACTEBR\_CHSHIFT. The resulting value is the channel number associated with the EBR-1553 record.

channel = (activity & MSGACTEBR\_CHMASK) >> MSGACTEBR\_CHSHIFT;

| Cwdinfo field        |                                                                               |  |  |  |
|----------------------|-------------------------------------------------------------------------------|--|--|--|
| Constant             | Description                                                                   |  |  |  |
| CWDINFOEBR_RESVMASK  | Reserved for future use. Do not use.                                          |  |  |  |
| CWDINFOEBR_LINKMASK  | The link number mask value. Shift the result right with CWDINFOEBR_LINKSHIFT. |  |  |  |
| CWDINFOEBR_LINKSHIFT | Link number shift value. See LINKMASK above.                                  |  |  |  |

| Swdinfo field        |                                                                               |  |  |  |  |
|----------------------|-------------------------------------------------------------------------------|--|--|--|--|
| Constant             | Description                                                                   |  |  |  |  |
| SWDINFOEBR_RESVMASK  | Reserved for future use. Do not use.                                          |  |  |  |  |
| SWDINFOEBR_LINKMASK  | The link number mask value. Shift the result right with SWDINFOEBR_LINKSHIFT. |  |  |  |  |
| SWDINFOEBR_LINKSHIFT | Link number shift value. See LINKMASK above.                                  |  |  |  |  |

The RT Link number that the command was sent on can be extracted from cwdinfo field through the same process.

cwdlink = (cwdinfo & CWDINFOEBR\_LINKMASK) >> CWDINFOEBR\_LINKSHIFT;

This is also done to determine the RT Link number the response was seen on using the swdinfo field.

swdlink = (swdinfo & SWDINFOEBR\_LINKMASK) >> SWDINFOEBR\_LINKSHIFT;

The configured Logical Hub mode can also be extracted from the activity word. The MODE bits must be masked and shifted by doing the following

mode = (activity & MSGACTEBR\_MODEMASK) >> MSGACTEBR\_MODESHIFT;

Then the mode can be directly compared against the following values

| LHub Mode       |                              |  |
|-----------------|------------------------------|--|
| Constant        | Description                  |  |
| LHUBMODE_SPEC   | The LHub is in "spec" mode   |  |
| LHUBMODE_SWITCH | The LHub is in "switch" mode |  |
| LHUBMODE_LINK   | The LHub is in "link" mode   |  |

#### Comparison is as follows

if (mode == LHUBMODE\_SPEC)

# **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit