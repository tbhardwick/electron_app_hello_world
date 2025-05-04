# **MsgBlockRd**

```
MSGADDR BTI429_MsgBlockRd
(
     LPMSGFIELDS429 msgfields, //Pointer to destination structure
     MSGADDR message, //Message from which to read
     HCORE hCore //Core handle
)
```
## **RETURNS**

The address of the message structure that was read.

## **DESCRIPTION**

Reads an entire Message Record from the Device.

| MSGFIELDS429 structure |        |                                                                                                                           |
|------------------------|--------|---------------------------------------------------------------------------------------------------------------------------|
| Field                  | Size   | Description                                                                                                               |
| msgopt                 | USHORT | Message options fields. Do not modify these fields.                                                                       |
| msgact                 | USHORT | Message activity. See table below for detail.                                                                             |
| msgdata                | ULONG  | Message data value. A 32-bit ARINC 429 data word value.                                                                   |
| listptr                | ULONG  | List Buffer pointer. Used instead of msgdata when in List Buffer mode.                                                    |
| timetag                | ULONG  | Lower 32 bits of the time-tag value.                                                                                      |
| hitcount               | ULONG  | Hit Counter value. Used instead of time-tag when in Hit Counter mode.                                                     |
| maxtime                | ULONG  | Maximum repetition rate. 32 bits with resolution equal to time-tag resolution.                                            |
| elapsetime             | ULONG  | Elapsed time. 32 bits with resolution equal to time-tag resolution. Used instead of<br>maxtime when in Elapsed Time mode. |
| mintime                | ULONG  | Minimum repetition rate. 32 bits with resolution equal to time-tag resolution.                                            |
| userptr                | ULONG  | Reserved                                                                                                                  |
| timetagh               | ULONG  | Upper 32 bits of the time-tag value                                                                                       |
| decgap                 | USHORT | Measured decoder gap time (4 bits)                                                                                        |
| paramflag              | USHORT | Parametric bitrate configuration (includes bitcount)                                                                      |

The msgact field may be tested by AND-ing the values returned with the constants from the following table:

| msgact field      |                                                                                             |  |
|-------------------|---------------------------------------------------------------------------------------------|--|
| Constant          | Description                                                                                 |  |
| MSGACT429_CHMASK  | The channel number mask value. Shift the result right with MSGACT429_CHSHIFT.               |  |
| MSGACT429_CHSHIFT | Channel number shift value. See CHMASK above.                                               |  |
| MSGACT429_SPD     | This bit reflects the speed detected. A one signifies high speed (100 Kbps), and a zero     |  |
|                   | signifies low speed (12.5 Kbps).                                                            |  |
| MSGACT429_ERR     | If set, it signifies that an error occurred in receiving this word. The type of error is    |  |
|                   | defined by the following bits.                                                              |  |
| MSGACT429_GAP     | Gap Error. A gap of less than four bit times preceded the word.                             |  |
| MSGACT429_PAR     | Parity error. A parity error was detected in the word.                                      |  |
| MSGACT429_LONG    | Long word error. A word of more than 32 bits was detected.                                  |  |
| MSGACT429_BIT     | Bit timing error. An error occurred while decoding the bits of the word (short bits or long |  |
|                   | bits).                                                                                      |  |
| MSGACT429_TO      | Time out error. The decoder timed out while receiving a word (short word).                  |  |
| MSGACT429_HIT     | Signifies that the message has been processed by the firmware (the Hit bit).                |  |

### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

Message field *paramflag* only applies to 5G+ and 6G Devices; *rsvd* for all other Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgBlockWr, BTI429\_MsgCommRd BTI429\_MsgDataRd, BTI429\_MsgDataWr, BTI429\_FilterSet, BTI429\_FilterDefault