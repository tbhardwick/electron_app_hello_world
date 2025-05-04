# **SeqFindNext1553**

```
ERRVAL BTICard_SeqFindNext1553
(
     LPSEQRECORD1553 *pRecord, //Address of pointer to a structure
     LPSEQFINDINFO sfinfo //Pointer to structure
)
```

```
RETURNS
```
A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

Finds the next MIL-STD-1553 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data in the *sfinfo* structure. Calling this function repeatedly returns the 1553 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD1553 allows for easy handling of the data.

| SEQRECORD1553 structure |        |                                                            |              |
|-------------------------|--------|------------------------------------------------------------|--------------|
| Field                   | Size   | Description                                                | Version      |
| type                    | USHORT | The protocol and version number of this record             | All versions |
| count                   | USHORT | Size of the record, used to parse the data-stream          | All versions |
| timestamp               | ULONG  | Lower 32 bits of the time-tag value                        | All versions |
| activity                | USHORT | Activity value (see table below for details)               | All versions |
| error                   | USHORT | Error value (see table below for details)                  | All versions |
| cwd1                    | USHORT | Command word 1 value                                       | All versions |
| cwd2                    | USHORT | Command word 2 value                                       | All versions |
| swd1                    | USHORT | Status word 1value                                         | All versions |
| swd2                    | USHORT | Status word 2 value                                        | All versions |
| datacount               | USHORT | Number of MIL-STD-1553 data words                          | All versions |
| data[]                  | USHORT | Array of 1553 data words (don't exceed data[datacount -1]) | All versions |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity and error fields may be tested by AND-ing the values returned with the constants from the tables below:

| MIL-STD-1553 activity field |                                                                   |  |  |  |
|-----------------------------|-------------------------------------------------------------------|--|--|--|
| Constant                    | Description                                                       |  |  |  |
| MSGACT1553_CHMASK           | The channel number. Shift the result right by MSGACT1553_CHSHIFT. |  |  |  |
| MSGACT1553_CHSHIFT          | Channel number shift value. See CHMASK above.                     |  |  |  |
| MSGACT1553_XMTCWD1          | Command word 1 was transmitted.                                   |  |  |  |
| MSGACT1553_XMTCWD2          | Command word 2 was transmitted.                                   |  |  |  |
| MSGACT1553_XMTSWD1          | Status word 1 was transmitted.                                    |  |  |  |
| MSGACT1553_XMTSWD2          | Status word 2 was transmitted.                                    |  |  |  |
| MSGACT1553_RCVCWD1          | Command word 1 was received.                                      |  |  |  |
| MSGACT1553_RCVCWD2          | Command word 2 was received.                                      |  |  |  |
| MSGACT1553_RCVSWD1          | Status word 1 was received.                                       |  |  |  |
| MSGACT1553_RCVSWD2          | Status word 2 was received.                                       |  |  |  |
| MSGACT1553_XMTDWD           | Data word was transmitted.                                        |  |  |  |
| MSGACT1553_RCVDWD           | Data word was received.                                           |  |  |  |
| MSGACT1553_BUS              | Message was transmitted/received on bus A (0) or B (1).           |  |  |  |

| MIL-STD-1553 error field |                                                                                         |  |  |  |
|--------------------------|-----------------------------------------------------------------------------------------|--|--|--|
| Constant                 | Description                                                                             |  |  |  |
| MSGERR1553_NORESP        | No response was received from the RT                                                    |  |  |  |
| MSGERR1553_ANYERR        | Set if MSGERR1553_NORESP is false and any other error bits are set                      |  |  |  |
| MSGERR1553_PROTOCOL      | A protocol error occurred                                                               |  |  |  |
| MSGERR1553_SYNC          | Wrong polarity of the sync pulse                                                        |  |  |  |
| MSGERR1553_DATACOUNT     | Too many/too few data words                                                             |  |  |  |
| MSGERR1553_MANCH         | Manchester error                                                                        |  |  |  |
| MSGERR1553_PARITY        | Parity error                                                                            |  |  |  |
| MSGERR1553_WORD          | Word error                                                                              |  |  |  |
| MSGERR1553_RETRY         | All attempts to retry transmission of this message failed (BC only)                     |  |  |  |
| MSGERR1553_SYSTEM        | Internal system error occurred                                                          |  |  |  |
| MSGERR1553_HIT           | Indicates that this message was transmitted or received since this bit was last cleared |  |  |  |
|                          | (always set)                                                                            |  |  |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT1553\_CHMASK and right-shifting the result by MSGACT1553\_CHSHIFT. The resulting value is the channel number associated with the MIL-STD-1553 record.

channel = (activity & MSGACT1553\_CHMASK) >> MSGACT1553\_CHSHIFT;

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit, BTICard\_SeqFindMore1553