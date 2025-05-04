# **FilterDefault**

```
MSGADDR BTI429_FilterDefault
(
     ULONG ctrlflags, //Selects message options
     INT channel, //Number of receive channel
     HCORE hCore //Core handle
)
```
## **RETURNS**

Address of the Message Record the function created and placed in the Filter Table.

## **DESCRIPTION**

Creates a Message Record with the options specified in *ctrlflags*, and then sets it as the default Message Record for the channel specified by *channel*. Received messages that do not meet the criteria of specific filters are saved in the default Message Record. If no default filter was created and a message does not match a specific filter, then the message is skipped and not saved in memory.

The options that can be used in *ctrlflags* are listed below. Please note that only the receiver options can be used with this function.

| ctrlflags           |                                                                                  |     |     |
|---------------------|----------------------------------------------------------------------------------|-----|-----|
| Constant            | Description                                                                      | Rcv | Xmt |
| MSGCRT429_DEFAULT   | Select all default settings (bold below)                                         | x   | x   |
| MSGCRT429_NOSEQ     | This message will not get recorded in the Sequential Record                      | x   | x   |
| MSGCRT429_SEQ       | This message will get recorded in the Sequential Record                          | x   | x   |
| MSGCRT429_NOLOG     | This message will not create an entry in the Event Log List                      | x   | x   |
| MSGCRT429_LOG       | This message will create an entry in the Event Log List                          | x   | x   |
| MSGCRT429_NOTIMETAG | This message will not record a time-tag                                          | x   | x   |
| MSGCRT429_TIMETAG   | This message will record a time-tag                                              | x   | x   |
| MSGCRT429_NOELAPSE  | This message will not record an Elapsed Time                                     | x   | x   |
| MSGCRT429_ELAPSE    | This message will record an Elapsed Time                                         | x   | x   |
| MSGCRT429_NOMAXMIN  | This message will not record maximum and minimum repetition rates                | x   | x   |
| MSGCRT429_MAX       | This message will record maximum repetition rates                                | x   | x   |
| MSGCRT429_MIN       | This message will record minimum repetition rates                                | x   | x   |
| MSGCRT429_MAXMIN    | This message will record maximum and minimum repetition rates                    | x   | x   |
| MSGCRT429_NOHIT     | This message will not record a Hit Counter                                       | x   | x   |
| MSGCRT429_HIT       | This message will record a Hit Counter (can't have Hit Counter with time-tag,    | x   | x   |
|                     | elapse, or max/min timing)                                                       |     |     |
| MSGCRT429_NOSKIP    | This message will not be skipped                                                 | x   | x   |
| MSGCRT429_SKIP      | This message will be skipped, and none of the options will be processed          | x   | x   |
| MSGCRT429_NOSYNC    | This message will not generate a SYNCOUT signal                                  |     | x   |
| MSGCRT429_SYNC      | This message will generate a SYNCOUT signal                                      |     | x   |
| MSGCRT429_NOEXTRIG  | This message will be triggered immediately                                       |     | x   |
| MSGCRT429_EXTRIG    | This message will wait for an EXTRIG* pulse to be triggered                      |     | x   |
| MSGCRT429_NOERR     | This message will not have a parity error                                        |     | x   |
| MSGCRT429_PARERR    | This message will have a parity error                                            |     | X   |
| MSGCRT429_WIPE      | The data fields of this message will initially be wiped to a value               | x   | x   |
| MSGCRT429_NOWIPE    | The data fields of this message will initially be left unchanged                 | x   | x   |
| MSGCRT429_WIPE0     | The data fields of this message will be wiped with a value of zeros (this option | x   | x   |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |
| MSGCRT429_WIPE1     | The data fields of this message will be wiped with a value of ones (this option  | x   | x   |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

This function initializes all filters (all label/SDI combinations) for the specified channel. Any filters previously created for the channel are overwritten. Therefore, BTI429\_FilterDefault must precede any calls to BTI429\_FilterSet.

#### **SEE ALSO**

BTI429\_FilterSet, BTI429\_FilterRd, BTI429\_FilterWr