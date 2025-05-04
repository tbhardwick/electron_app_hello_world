# **MsgSyncDefine**

```
ERRVAL BTI429_MsgSyncDefine
(
      BOOL enable, //Enable/disable external sync pulse
      USHORT syncmask, //Line(s) used for sync output
      USHORT pinpolarity,//Active pin polarity (high/low)
      MSGADDR message, //Transmit message to drive sync pulse
      HCORE hCore //Core handle
)
```
## **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Defines the sync output conditions for a transmit message specified by *message* and configures it to output a sync signal. This sync signal appears on the line(s) specified by *syncmask* with the polarity specified by *pinpolarity* (see tables below). BTI429\_MsgSyncDefine may be called during run time to redefine the sync output settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Sync outputs are both generation dependent and device dependent. Refer to the Device Dependency table and the following tables for more information.

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

| pinpolarity    |                                           |  |  |  |
|----------------|-------------------------------------------|--|--|--|
| Constant       | Description                               |  |  |  |
| SYNCPOL_SYNCAL | Sets active low polarity for sync line A  |  |  |  |
| SYNCPOL_SYNCAH | Sets active high polarity for sync line A |  |  |  |
| SYNCPOL_SYNCBL | Sets active low polarity for sync line B  |  |  |  |
| SYNCPOL_SYNCBH | Sets active high polarity for sync line B |  |  |  |
| SYNCPOL_SYNCCL | Sets active low polarity for sync line C  |  |  |  |
| SYNCPOL_SYNCCH | Sets active high polarity for sync line C |  |  |  |

Alternatively, to configure all messages on a specified channel to output a sync pulse, use BTI-429\_ChSyncDefine.

### **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices. 3G Devices, which have only a single sync line, can call BTI429\_MsgCreate with the MSGCRT429\_SYNC flag. The mapping of sync lines to discretes is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function only applies to transmit messages.

#### **SEE ALSO**

```
BTI429_ChSyncDefine, BTICard_CardSyncValid
```