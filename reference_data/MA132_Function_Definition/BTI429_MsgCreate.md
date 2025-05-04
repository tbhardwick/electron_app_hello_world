# **MsgCreate**

```
MSGADDR BTI429_MsgCreate
(
      ULONG ctrlflags, //Selects message options
      HCORE hCore //Core handle
)
```
## **RETURNS**

The Device address of the Message Record if successful, otherwise zero.

#### **DESCRIPTION**

Allocates memory for a Message Record and initializes the record with the options specified in *ctrlflags*. The options that can be used in *ctrlflags* are listed below. Please note that only the transmitter options can be used with this function.

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
| MSGCRT429_PARERR    | This message will have a parity error                                            |     | x   |
| MSGCRT429_WIPE      | This data fields of this message will initially be wiped to a value              | x   | x   |
| MSGCRT429_NOWIPE    | The data fields of this message will initially be left unchanged                 | x   | x   |
| MSGCRT429_WIPE0     | The data fields of this message will be wiped with a value of zeros (this option | x   | x   |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |
| MSGCRT429_WIPE1     | The data fields of this message will be wiped with a value of ones (this option  | x   | x   |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_MsgDataRd, BTI429\_MsgDataWr, BTI429\_MsgBlockRd, BTI429\_MsgBlockWr BTI429\_MsgCommRd, BTI429\_MsgCommWr