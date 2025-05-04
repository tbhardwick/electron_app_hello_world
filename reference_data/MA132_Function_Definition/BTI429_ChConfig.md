# **ChConfig**

```
ERRVAL BTI429_ChConfig
(
     ULONG ctrlflags, //Selects channel options
     INT channel, //Number of channel
     HCORE hCore //Core handle
)
```
## **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Configures the specified transmit or receive channel of the specified Device by performing the following steps:

- 1. Stops the channel.
- 2. Clears transmit Schedule for the specified channel (Filter Tables are not affected).
- 3. Writes Device options defined by *ctrlflags*.
- 4. Restarts the channel if previously started and not disabled by CHCFG429\_INACTIVE.

The CHCFG429\_GAP1US flag configures the channel to interpret all future gap parameters (*gap*/*gapval*) as microsecond values rather than bit-times. All functions with gap parameters are affected when operating the channel in this mode such as but not limited to:

BTI429\_SchedGap, BTI429\_SchedMsgEx, BTI429\_SchedData, BTI429\_PlayPutGap, BTI429\_PlayPutData

| ctrlflags            |                                                                                 |     |     |  |
|----------------------|---------------------------------------------------------------------------------|-----|-----|--|
| Constant             | Description                                                                     | Rcv | Xmt |  |
| CHCFG429_DEFAULT     | Select all default settings (bold below)                                        | x   | x   |  |
| CHCFG429_LOWSPEED    | Select low speed messages (12.5 Kbps)                                           | x   | x   |  |
| CHCFG429_HIGHSPEED   | Select high speed messages (100 Kbps)                                           | x   | x   |  |
| CHCFG429_AUTOSPEED   | Select auto speed detection of messages                                         | x   |     |  |
| CHCFG429_PARODD      | Select odd parity                                                               | x   | x   |  |
| CHCFG429_PAREVEN     | Select even parity                                                              | x   | x   |  |
| CHCFG429_PARDATA     | Select parity bit as data (ignores parity)                                      | x   | x   |  |
| CHCFG429_ACTIVE      | Enable channel activity                                                         | x   | x   |  |
| CHCFG429_INACTIVE    | Disable channel activity                                                        | x   | x   |  |
| CHCFG429_SEQSEL      | Sequential monitoring selected at message level                                 | x   | x   |  |
| CHCFG429_SEQALL      | Every message will be recorded in the Sequential Record                         | x   | x   |  |
| CHCFG429_NOLOGHALT   | No entry will be made in the Event Log List on a HALT command                   |     | x   |  |
| CHCFG429_LOGHALT     | An entry will be made in the Event Log List on a HALT command                   |     | x   |  |
| CHCFG429_NOLOGPAUSE  | No entry will be made in the Event Log List on a PAUSE command                  |     | x   |  |
| CHCFG429_LOGPAUSE    | An entry will be made in the Event Log List on a PAUSE command                  |     | x   |  |
| CHCFG429_NOLOGERR    | No entry will be made in the Event Log List when a decoder detects an error     | x   |     |  |
| CHCFG429_LOGERR      | An entry will be made in the Event Log List when a decoder detects an error     | x   |     |  |
| CHCFG429_TIMETAGOFF  | The time-tag option is selected at the message level                            | x   | x   |  |
| CHCFG429_TIMETAG     | All messages will record a time-tag.                                            | x   | x   |  |
| CHCFG429_ELAPSEOFF   | The elapse timing option is selected at the message level                       | x   | x   |  |
| CHCFG429_ELAPSE      | All messages will record an elapsed time.                                       | x   | x   |  |
| CHCFG429_MAXMINOFF   | Max and min repetition rates are selected at the message level                  | x   | x   |  |
| CHCFG429_MAX         | All messages will record a max time                                             | x   | x   |  |
| CHCFG429_MIN         | All messages will record a min time                                             | x   | x   |  |
| CHCFG429_MAXMIN      | All messages will record a max and a min time                                   | x   | x   |  |
| CHCFG429_NOHIT       | The Hit Counter is selected at the message level                                | x   | x   |  |
| CHCFG429_HIT         | The Hit Counter is selected for all messages (can't have Hit Counter with       |     |     |  |
|                      | time-tag, elapse, or max/min timing)                                            | x   | x   |  |
| CHCFG429_SELFTESTOFF | This channel will transmit/receive on the operational bus and not the self-test |     |     |  |
|                      | bus.                                                                            | x   | x   |  |

| ctrlflags<br>Constant | Description                                                                                                                                                                                                                                                            | Rcv | Xmt |
|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----|-----|
| CHCFG429_SELFTEST     | This channel will transmit/receive on the internal self-test bus and not the<br>operational bus. Only one transmitter can be on the self-test bus at a time;<br>therefore, only the last transmit channel configured to use the self-test bus will<br>use this option. | x   | x   |
| CHCFG429_SYNCOFF      | The SYNCOUT signal option will be selected at the message level                                                                                                                                                                                                        | x   | x   |
| CHCFG429_SYNC         | The SYNCOUT signal will be active for all messages of this channel                                                                                                                                                                                                     | x   | x   |
| CHCFG429_EXTOFF       | Messages are selected for an external trigger at the message level                                                                                                                                                                                                     |     | x   |
| CHCFG429_EXTTRIG      | All messages for this channel will be externally triggered                                                                                                                                                                                                             |     | x   |
| CHCFG429_NOERR        | Error injection is selected at the message level                                                                                                                                                                                                                       |     | x   |
| CHCFG429_PARERR       | All messages will have a parity error (inverts the parity bit)                                                                                                                                                                                                         |     | x   |
| CHCFG429_UNPAUSE      | The channel will initially be unpaused                                                                                                                                                                                                                                 | x   | x   |
| CHCFG429_PAUSE        | The channel will initially be paused                                                                                                                                                                                                                                   | x   | x   |
| CHCFG429_NOLOOPMAX    | Disable Schedule maximum loop count                                                                                                                                                                                                                                    |     | x   |
| CHCFG429_LOOPMAX      | Enable Schedule maximum loop count                                                                                                                                                                                                                                     |     | x   |
| CHCFG429_GAPBIT       | Enable bit-time gaps                                                                                                                                                                                                                                                   |     | x   |
| CHCFG429_GAP1US       | Enable microsecond gaps (Device Dependent)                                                                                                                                                                                                                             |     | x   |
| CHCFG429_SCHEDULE     | Configure for Schedule Mode                                                                                                                                                                                                                                            |     | x   |
| CHCFG429_PLAYBACK     | Configure for Playback FIFO Mode (Device Dependent)                                                                                                                                                                                                                    |     | x   |

#### *(Continued from previous page)*

## **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

5G Devices do not support the CHCFG429\_LOOPMAX flag.

Only 5G+ and 6G Devices support the CHCFG429\_GAP1US and CHCFG429\_PLAYBACK flags.

#### **WARNINGS**

The function clears any previous configuration of the channel.

Channels configured with CHCFG429\_SCHEDULE must only use BTI429\_Sched\* functions, whereas channels configured with CHCFG429\_PLAYBACK must only use BTI429\_Play\* functions.

## **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop