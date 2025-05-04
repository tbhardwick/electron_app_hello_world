# **SeqFindNext717**

```
ERRVAL BTICard_SeqFindNext717
(
      LPSEQRECORD717 *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Finds the next ARINC 717 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the 717 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD717 allows for easy handling of the data.

| SEQRECORD717 structure |        |                                                   |              |
|------------------------|--------|---------------------------------------------------|--------------|
| Field                  | Size   | Description                                       | Version      |
| type                   | USHORT | The protocol and version number of this record    | All versions |
| count                  | USHORT | Size of the record, used to parse the data-stream | All versions |
| timestamp              | ULONG  | Lower 32-bits of the time-tag value               | All versions |
| activity               | USHORT | Activity (see table below for details)            | All versions |
| wordnum                | USHORT | Number of words                                   | All versions |
| subframe               | USHORT | Number of subframes                               | All versions |
| superframe             | USHORT | Number of superframes                             | All versions |
| data                   | USHORT | 12-bit ARINC 717 data word value in LSBs          | All versions |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value               | 1 or greater |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity field may be tested by AND-ing the value returned with the constants from the table below:

| ARINC 717 activity field |                                                                                          |  |
|--------------------------|------------------------------------------------------------------------------------------|--|
| Constant                 | Description                                                                              |  |
| MSGACT717_CHMASK         | The channel number. Shift the result right by MSGACT717_CHSHIFT.                         |  |
| MSGACT717_CHSHIFT        | Channel number shift value. See CHMASK above.                                            |  |
| MSGACT717_SPDMASK        | The current speed mask value.                                                            |  |
| MSGACT717_64WPS          | The current speed is 64 wps (words per second).                                          |  |
| MSGACT717_128WPS         | The current speed is 128 wps.                                                            |  |
| MSGACT717_256WPS         | The current speed is 256 wps.                                                            |  |
| MSGACT717_512WPS         | The current speed is 512 wps.                                                            |  |
| MSGACT717_1024WPS        | The current speed is 1024 wps.                                                           |  |
| MSGACT717_2048WPS        | The current speed is 2048 wps.                                                           |  |
| MSGACT717_4096WPS        | The current speed is 4096 wps.                                                           |  |
| MSGACT717_8192WPS        | The current speed is 8192 wps.                                                           |  |
| MSGACT717_TO             | Time out error: The decoder timed out while receiving a (short) word.                    |  |
| MSGACT717_HIT            | Indicates that this message was transmitted or received since this bit was last cleared. |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT717\_CHMASK and right-shifting the result by MSGACT717\_CHSHIFT. The resulting value is the channel number associated with the ARINC 717 record.

channel = (activity & MSGACT717\_CHMASK) >> MSGACT717\_CHSHIFT;

### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

## **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit