# **EventLogRd**

```
ULONG BTICard_EventLogRd
(
      LPUSHORT typeval, //Pointer to variable to receive type value
      LPULONG infoval, //Pointer to variable to receive info value
      LPINT channel, //Pointer to variable to receive channel value
      HCORE hCore //Core handle
)
```
# **RETURNS**

The address of the entry in the Event Log List, or zero if it is empty and there are no entries to read.

# **DESCRIPTION**

Reads the next entry from the Event Log List and advances the pointer. The type of event and channel that generated the entry are passed through *typeval* and *channel.* An information word associated with the event is passed through *infoval*.

The value of *typeval* determines the meaning of the *infoval* value (see table below). Note that the Event Log List records events from all protocols implemented on the specified core. The first two event types, regarding the Sequential Record, are protocol-independent. The rest of the table is subdivided by protocol.

|              | typeval                  | Description                                                                 | infoval                                | Refer to…                                                                                  |
|--------------|--------------------------|-----------------------------------------------------------------------------|----------------------------------------|--------------------------------------------------------------------------------------------|
| General      | EVENTTYPE_SEQFULL        | Sequential Record full (halted)<br>or overwritten                           | Address of last<br>entry               | BTICard_SeqConfig                                                                          |
|              | EVENTTYPE_SEQFREQ        | th entry (user-specified)<br>n                                              | Address of last<br>entry               | BTICard_SeqConfig                                                                          |
|              | EVENTTYPE_BITERROR       | BIT system detected an error                                                | BIT Status at the<br>time of the Error | BTICard_BITConfig                                                                          |
| MIL-STD-1553 | EVENTTYPE_1553MSG        | Message processed                                                           | Address of the<br>Message<br>structure | BTI1553_BCCreateMsg<br>BTI1553_RTCreateMsg<br>BTI1553_BCCreateList<br>BTI1553_RTCreateList |
|              | EVENTTYPE_1553OPCODE     | BC Schedule encountered a<br>LOG command                                    | User-assigned<br>tag value             | BTI1553_BCSchedLog                                                                         |
|              | EVENTTYPE_1553HALT       | BC Schedule encountered a<br>HALT command                                   | Address of the<br>Schedule entry       | BTI1553_BCSchedHalt                                                                        |
|              | EVENTTYPE_1553PAUSE      | BC Schedule encountered a<br>PAUSE command                                  | Address of the<br>Schedule entry       | BTI1553_BCSchedPause                                                                       |
|              | EVENTTYPE_1553LIST       | List Buffer empty/ full<br>(underflow or overflow). Does<br>not work in 4G. | List Buffer<br>address                 | BTI1553_BCCreateList<br>BTI1553_RTCreateList                                               |
| Serial       | EVENTTYPE_422TXTHRESHOLD | TX FIFO under threshold                                                     | None                                   | BTI422_FIFOConfigTx                                                                        |
|              | EVENTTYPE_422TXFIFO      | TX FIFO underflow                                                           | None                                   | BTI422_FIFOConfigTx                                                                        |
|              | EVENTTYPE_422RXTHRESHOLD | RX FIFO over threshold                                                      | None                                   | BTI422_FIFOConfigRx                                                                        |
|              | EVENTTYPE_422RXFIFO      | RX FIFO overflow                                                            | None                                   | BTI422_FIFOConfigRx                                                                        |
|              | EVENTTYPE_422RXERROR     | RX error                                                                    | None                                   | BTI422_FIFOConfigRx                                                                        |

(Continued on next page)

|             | typeval              | Description                          | infoval                          | Refer to…                                  |
|-------------|----------------------|--------------------------------------|----------------------------------|--------------------------------------------|
|             | EVENTTYPE_429MSG     | Message received or                  | Address of the Message           | BTI429_MsgCreate                           |
|             |                      | transmitted                          | Record                           | BTI429_FilterDefault                       |
| ARINC 429   |                      |                                      |                                  | BTI429_FilterSet                           |
|             | EVENTTYPE_429OPCODE  | A transmit Schedule                  | User-assigned tag value          |                                            |
|             |                      | encountered a LOG                    |                                  | BTI429_SchedLog                            |
|             |                      | command                              |                                  |                                            |
|             | EVENTTYPE_429HALT    | A transmit Schedule                  | Address of the Schedule          | BTI429_ChConfig                            |
|             |                      | encountered a HALT                   | entry                            | BTI429_SchedHalt                           |
|             | EVENTTYPE_429PAUSE   | command                              |                                  |                                            |
|             |                      | A transmit Schedule<br>encountered a | Address of the Schedule<br>entry | BTI429_ChConfig                            |
|             |                      | PAUSE command                        |                                  | BTI429_SchedPause                          |
|             | EVENTTYPE_429LIST    | List Buffer empty or                 | List Buffer address              | BTI429_ListAsyncCreate                     |
|             |                      | full (underflow or                   |                                  | BTI429_ListRcvCreate                       |
|             |                      | overflow)                            |                                  | BTI429_ListXmtCreate                       |
|             | EVENTTYPE_429ERR     | A decoder error was                  | Address of the message           |                                            |
|             |                      | detected                             | that contained the error         | BTI429_ChConfig                            |
|             | EVENTTYPE_717WORD    | Processed 717 word                   | Word address                     | BTI717_SubFrmWordConfig                    |
|             |                      |                                      |                                  | BTI717_SuperFrmWordConfig                  |
|             | EVENTTYPE_717SUBFRM  | Processed 717                        | Subframe number                  | BTI717_SubFrmWordConfig                    |
| ARINC 717   |                      | subframe                             |                                  | BTI717_SuperFrmWordConfig                  |
|             | EVENTTYPE_717SYNCERR | 717 receive channel                  | Channel number                   | BTI717_ChConfig                            |
|             | EVENTTYPE_708MSG     | lost sync                            |                                  |                                            |
| ARINC 708   |                      | Message received/<br>transmitted     | Message index                    |                                            |
|             |                      |                                      |                                  | BTI708_RcvBuild                            |
|             |                      |                                      |                                  | BTI708_XmtBuild                            |
|             |                      |                                      |                                  |                                            |
|             | EVENTTYPE_EBRMSG     | Message processed                    | Address of the Message           | BTIEBR_BCCreateMsg                         |
|             |                      |                                      | structure                        | BTIEBR_RTCreateMsg                         |
|             |                      |                                      |                                  | BTIEBR_BCCreateList                        |
|             |                      |                                      |                                  | BTIEBR_RTCreateList                        |
|             | EVENTTYPE_EBROPCODE  | BC Schedule                          | User-assigned tag value          | BTIEBR_BCSchedLog                          |
|             |                      | encountered a LOG<br>command         |                                  |                                            |
|             | EVENTTYPE_EBRHALT    | BC Schedule                          | Address of the Schedule          |                                            |
| EBR-1553    |                      | encountered a HALT                   | entry                            | BTIEBR_BCSchedHalt                         |
|             |                      | command                              |                                  |                                            |
|             | EVENTTYPE_EBRPAUSE   | BC Schedule                          | Address of the Schedule          |                                            |
|             |                      | encountered a                        | entry                            | BTIEBR_BCSchedPause                        |
|             |                      | PAUSE command                        |                                  |                                            |
|             | EVENTTYPE_EBRLIST    | List Buffer empty/ full              | List Buffer address              |                                            |
|             |                      | (underflow or                        |                                  | BTIEBR_BCCreateList<br>BTIEBR_RTCreateList |
|             |                      | overflow)                            |                                  |                                            |
|             | EVENTTYPE_DIOEDGE    | An edge transition                   | 8-bit mask specifying            |                                            |
|             |                      | occurred                             | which I/O had an edge, 8-        | BTIDIO_BankConfig                          |
|             |                      |                                      | bit state of the bank I/O        | BTIDIO_DiscreteConfig                      |
| Discrete IO |                      |                                      | after edge.                      |                                            |
|             | EVENTTYPE_DIOFAULT   | A fault occurred                     | 8-bit mask specifying            | BTIDIO_BankConfig                          |
|             |                      |                                      | which I/O had a fault            | BTIDIO_DiscreteConfig                      |

| (Continued from previous page) |  |  |
|--------------------------------|--|--|
|                                |  |  |

# **DEVICE DEPENDENCY**

The value passed back in *channel* may not be valid for all types of events. If an event does not have an associated channel value, it is filled in with the value FFh.

A typeval of EVENTTYPE\_1553LIST does not work in 4G (see [Table 6.1\)](#page--1-0).

# **WARNINGS**

This function should be preceded by a call to BTICard\_EventLogConfig. To use this function, it is not necessary to install an interrupt handler.

# **SEE ALSO**

BTICard\_EventLogConfig