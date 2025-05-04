# **ChSyncDefine**

```
ERRVAL BTI429_ChSyncDefine
(
      BOOL enable, //Enable/disable external sync output
      USHORT syncmask, //Line(s) used for sync output
      USHORT pinpolarity,//Active pin polarity (high/low)
      INT channel, //Channel number (transmit or receive)
      HCORE hCore //Core handle
)
```
## **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Defines the sync output settings for *channel* on *hCore* and configures all messages to output a sync signal. This sync signal appears on the line(s) specified by *syncmask* with the polarity specified by *pinpolarity* (see tables below). When enabled, the sync signal is active during the transmission or reception of the message. BTI429\_ChSyncDefine may be called during run time to redefine the sync output settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Sync outputs are both generation dependent and device dependent. Refer to Chapter [6](#page--1-0) and the following tables for more information.

| syncmask          |                               |    |    |     |    |
|-------------------|-------------------------------|----|----|-----|----|
| Constant          | Description                   | 4G | 5G | 5G+ | 6G |
| SYNCMASK_SYNCA    | Selects discrete sync line A  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGA | Selects PXIe TRIG sync line A |    |    |     | *  |
| SYNCMASK_SYNCB    | Selects discrete sync line B  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGB | Selects PXIe TRIG sync line B |    |    |     | *  |
| SYNCMASK_SYNCC    | Selects discrete sync line C  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGC | Selects PXIe TRIG sync line C |    |    |     | *  |
| SYNCMASK_PXISTARC | Selects PXIe STAR sync line C |    |    |     | *  |

*\* PXIe Syncs are only available on PXIe Devices. Refer to BTICard\_CardSyncValid for device dependent support*

| pinpolarity    |                                           |  |  |
|----------------|-------------------------------------------|--|--|
| Constant       | Description                               |  |  |
| SYNCPOL_SYNCAL | Sets active low polarity for sync line A  |  |  |
| SYNCPOL_SYNCAH | Sets active high polarity for sync line A |  |  |
| SYNCPOL_SYNCBL | Sets active low polarity for sync line B  |  |  |
| SYNCPOL_SYNCBH | Sets active high polarity for sync line B |  |  |
| SYNCPOL_SYNCCL | Sets active low polarity for sync line C  |  |  |
| SYNCPOL_SYNCCH | Sets active high polarity for sync line C |  |  |

Alternatively, to configure selected transmit message(s) to output a sync pulse, use BTI429\_Msg-SyncDefine.

## **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices. 3G Devices, which have a single sync line, can call BTI429\_ChConfig with the CHCFG429\_SYNC flag. The mapping of sync lines is hardware dependent. Please consult the hardware manual for the Device.

## **WARNINGS**

None.

## **SEE ALSO**

BTI429\_MsgSyncDefine, BTICard\_CardSyncValid