# **SeqFindNext429**

```
ERRVAL BTICard_SeqFindNext429
(
      LPSEQRECORD429 *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Finds the next ARINC 429 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the 429 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD429 allows for easy handling of the data.

| SEQRECORD429 structure |        |                                                                                 |              |
|------------------------|--------|---------------------------------------------------------------------------------|--------------|
| Field                  | Size   | Description                                                                     | Version      |
| type                   | USHORT | The protocol and version number of this record                                  | All versions |
| count                  | USHORT | Size of the record, used to parse the data-stream                               | All versions |
| timestamp              | ULONG  | Lower 32 bits of the time-tag value                                             | All versions |
| activity               | USHORT | Activity (see table below for details)                                          | All versions |
| decgap                 | USHORT | Gap preceding the 429 word in half bit-times (up to a maximum of 7.5 bit times) | 1 or greater |
| data                   | ULONG  | 32-bit ARINC 429 data word value                                                | All versions |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value                                             | 1 or greater |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The *decgap* field is a 4-bit value that measures the gap preceding the decoded word. If *decgap* indicates 7.5 bit times (F hexadecimal), then the gap is 7.5 bit times or greater.

The activity field may be tested by AND-ing the value returned with the constants from the table below:

| ARINC 429 activity field |                                                                                                                             |  |  |
|--------------------------|-----------------------------------------------------------------------------------------------------------------------------|--|--|
| Constant                 | Description                                                                                                                 |  |  |
| MSGACT429_CHMASK         | The channel number. Shift the result right by MSGACT429_CHSHIFT.                                                            |  |  |
| MSGACT429_CHSHIFT        | Channel number shift value. See CHMASK above.                                                                               |  |  |
| MSGACT429_SPD            | This bit reflects the speed detected. A one signifies high speed (100 Kbps), and a zero<br>signifies low speed (12.5 Kbps). |  |  |
| MSGACT429_ERR            | If set, it signifies that an error occurred in receiving this word. The type of error is defined by<br>the following bits.  |  |  |
| MSGACT429_GAP            | Gap Error: A gap of less than four bit times preceded the word.                                                             |  |  |
| MSGACT429_PAR            | Parity error: A parity error was detected in the word.                                                                      |  |  |
| MSGACT429_LONG           | Long word error: A word of more than 32-bits was detected.                                                                  |  |  |
| MSGACT429_BIT            | Bit timing error: An error occurred while decoding the bits of the word (short bits or long bits).                          |  |  |
| MSGACT429_TO             | Time out error: The decoder timed out while receiving a word (short word).                                                  |  |  |
| MSGACT429_HIT            | Signifies that the message has been processed by the firmware (the Hit bit).                                                |  |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT429\_CHMASK and right-shifting the result by MSGACT429\_CHSHIFT. The resulting value is the channel number associated with the ARINC 429 record.

channel = (activity & MSGACT429\_CHMASK) >> MSGACT429\_CHSHIFT;

## **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

## **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit