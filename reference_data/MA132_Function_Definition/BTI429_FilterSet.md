# **FilterSet**

```
MSGADDR BTI429_FilterSet
(
     ULONG ctrlflags, //Selects message options
     INT label, //Label value to receive
     INT sdi, //SDI patterns to receive
     INT channel, //Number of receive channel
     HCORE hCore //Core handle
)
```
## **RETURNS**

Address of the Message Record the function created and placed in the Filter Table.

# **DESCRIPTION**

Creates a filter for a receive channel by creating a Message Record with the options specified in *ctrlflags*, then setting it as the Message Record for the specified *channel*, *label*, and *sdi* values.

Filters are used to sort and save (by label and SDI) messages that are received over a given databus. During operation, when the label and SDI in a received message match the label and SDI information in a specific filter, the message is stored in a specific Message Record location.

*sdi* allows one or more SDI combinations to be specified. A filter is created for each specified SDI. The predefined constants listed below can be used to specify the SDI. When a combination of SDIs are selected, the constants should be OR-ed together.

| sdi       |                                   |  |  |
|-----------|-----------------------------------|--|--|
| Constants | Description                       |  |  |
| SDI00     | Selects only the SDI value of 00. |  |  |
| SDI01     | Selects only the SDI value of 01. |  |  |
| SDI10     | Selects only the SDI value of 10. |  |  |
| SDI11     | Selects only the SDI value of 11. |  |  |
| SDIALL    | Selects all the SDI values.       |  |  |

The options that can be used in *ctrlflags* are listed below. Please note that only the receiver options can be used with this function.

| ctrlflags           |                                                                               |     |     |  |
|---------------------|-------------------------------------------------------------------------------|-----|-----|--|
| Constant            | Description                                                                   | Rcv | Xmt |  |
| MSGCRT429_DEFAULT   | Select all default settings (bold below)                                      | x   | x   |  |
| MSGCRT429_NOSEQ     | This message will not get recorded in the Sequential Record                   | x   | x   |  |
| MSGCRT429_SEQ       | This message will get recorded in the Sequential Record                       | x   | x   |  |
| MSGCRT429_NOLOG     | This message will not create an entry in the Event Log List                   | x   | x   |  |
| MSGCRT429_LOG       | This message will create an entry in the Event Log List                       | x   | x   |  |
| MSGCRT429_NOTIMETAG | This message will not record a time-tag                                       | x   | x   |  |
| MSGCRT429_TIMETAG   | This message will record a time-tag                                           | x   | x   |  |
| MSGCRT429_NOELAPSE  | This message will not record an Elapsed Time                                  | x   | x   |  |
| MSGCRT429_ELAPSE    | This message will record an Elapsed Time                                      | x   | x   |  |
| MSGCRT429_NOMAXMIN  | This message will not record maximum and minimum repetition rates             | x   | x   |  |
| MSGCRT429_MAX       | This message will record maximum repetition rates                             | x   | x   |  |
| MSGCRT429_MIN       | This message will record minimum repetition rates                             | x   | x   |  |
| MSGCRT429_MAXMIN    | This message will record maximum and minimum repetition rates                 | x   | x   |  |
| MSGCRT429_NOHIT     | This message will not record a Hit Counter                                    | x   | x   |  |
| MSGCRT429_HIT       | This message will record a Hit Counter (can't have Hit Counter with time-tag, | x   | x   |  |
|                     | elapse, or max/min timing)                                                    |     |     |  |
| MSGCRT429_NOSKIP    | This message will not be skipped                                              | x   | x   |  |
| MSGCRT429_SKIP      | This message will be skipped, and none of the options will be processed       | x   | x   |  |
| MSGCRT429_NOSYNC    | This message will not generate a SYNCOUT signal                               |     | x   |  |
| MSGCRT429_SYNC      | This message will generate a SYNCOUT signal                                   |     | x   |  |
| MSGCRT429_NOEXTRIG  | This message will be triggered immediately                                    |     | x   |  |
| MSGCRT429_EXTRIG    | This message will wait for an EXTRIG* pulse to be triggered                   |     | x   |  |
| MSGCRT429_NOERR     | This message will not have a parity error                                     |     | x   |  |
| MSGCRT429_PARERR    | This message will have a parity error                                         |     | x   |  |
| MSGCRT429_WIPE      | The data fields of this message will initially be wiped to a value            | x   | x   |  |
| MSGCRT429_NOWIPE    | The data fields of this message will initially be left unchanged              | x   | x   |  |
| MSGCRT429_WIPE0     | The data fields of this message will be wiped with a value of zeros (this     | x   | x   |  |
|                     | option does not get used if MSGCRT429_NOWIPE is used)                         |     |     |  |
| MSGCRT429_WIPE1     | The data fields of this message will be wiped with a value of ones (this      | x   | x   |  |
|                     | option does not get used if MSGCRT429_NOWIPE is used)                         |     |     |  |

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

### **WARNINGS**

None.

### **SEE ALSO**

BTI429\_FilterDefault, BTI429\_FilterRd, BTI429\_FilterWr