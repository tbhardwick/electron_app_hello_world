# **MsgTriggerDefine**

```
ERRVAL BTI429_MsgTriggerDefine
(
      BOOL enable, //Enable/disable external trigger
      USHORT trigmask, //Line(s) used for trigger signal
      USHORT trigval, //Active/inactive condition for trigger line(s)
      USHORT pinpolarity,//Active pin polarity (high/low)
      MSGADDR message, //Message address
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Defines the trigger input settings for the message specified by *message* and associates it with a trigger signal. The input line(s) are specified by *trigmask* with an active trigger state being the combination of *trigval* and *pinpolarity* (see tables below). When tagged and encountered in a transmit schedule, the *message* waits for the defined trigger condition before transmitting. BTI429\_MsgTriggerDefine may be called during run time to redefine the trigger input settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Only one *trigmask* may be selected per trigger line (A, B, and C), and all combined states must be true for the trigger to occur. Triggers are both generation dependent and device dependent. Refer to the Device Dependency table and the following tables for more information.

| trigmask          |                                  |    |    |     |    |  |  |
|-------------------|----------------------------------|----|----|-----|----|--|--|
| Constant          | Description                      | 4G | 5G | 5G+ | 6G |  |  |
| TRIGMASK_TRIGA    | Selects discrete trigger line A  | x  | x  | x   | x  |  |  |
| TRIGMASK_PXITRIGA | Selects PXIe TRIG trigger line A |    |    |     | *  |  |  |
| TRIGMASK_PXISTARA | Selects PXIe STAR trigger line A |    |    |     | *  |  |  |
| TRIGMASK_TRIGB    | Selects discrete trigger line B  | x  | x  | x   | x  |  |  |
| TRIGMASK_PXITRIGB | Selects PXIe TRIG trigger line B |    |    |     | *  |  |  |
| TRIGMASK_PXISTARB | Selects PXIe STAR trigger line B |    |    |     | *  |  |  |
| TRIGMASK_TRIGC    | Selects discrete trigger line C  | x  | x  | x   | x  |  |  |
| TRIGMASK_PXITRIGC | Selects PXIe TRIG trigger line C |    |    |     | *  |  |  |
| TRIGMASK_PXISTARC | Selects PXIe STAR trigger line C |    |    |     | *  |  |  |

*\* PXIe Syncs are only available on PXIe Devices. Refer to BTICard\_CardTriggerValid for device dependent support*

| trigval          |                            |  |  |  |  |
|------------------|----------------------------|--|--|--|--|
| Constant         | Description                |  |  |  |  |
| TRIGVAL_TRIGAOFF | Trigger on line A inactive |  |  |  |  |
| TRIGVAL_TRIGAON  | Trigger on line A active   |  |  |  |  |
| TRIGVAL_TRIGBOFF | Trigger on line B inactive |  |  |  |  |
| TRIGVAL_TRIGBON  | Trigger on line B active   |  |  |  |  |
| TRIGVAL_TRIGCOFF | Trigger on line C inactive |  |  |  |  |
| TRIGVAL_TRIGCON  | Trigger on line C active   |  |  |  |  |

| pinpolarity    |                                               |    |    |     |    |  |
|----------------|-----------------------------------------------|----|----|-----|----|--|
| Constant       | Description                                   | 4G | 5G | 5G+ | 6G |  |
| TRIGPOL_TRIGAL | Sets active low polarity for trigger line A   | x  | x  | x   | x  |  |
| TRIGPOL_TRIGAH | Sets active high polarity for trigger line A  | x  | x  | x   | x  |  |
| TRIGPOL_TRIGAF | Sets active on falling edge of trigger line A |    |    |     | x  |  |
| TRIGPOL_TRIGAR | Sets active on rising edge of trigger line A  |    |    |     | x  |  |
| TRIGPOL_TRIGBL | Sets active low polarity for trigger line B   | x  | x  | x   | x  |  |
| TRIGPOL_TRIGBH | Sets active high polarity for trigger line B  | x  | x  | x   | x  |  |
| TRIGPOL_TRIGBF | Sets active on falling edge of trigger line B |    |    |     | x  |  |
| TRIGPOL_TRIGBR | Sets active on rising edge of trigger line B  |    |    |     | x  |  |
| TRIGPOL_TRIGCL | Sets active low polarity for trigger line C   | x  | x  | x   | x  |  |
| TRIGPOL_TRIGCH | Sets active high polarity for trigger line C  | x  | x  | x   | x  |  |
| TRIGPOL_TRIGCF | Sets active on falling edge of trigger line C |    |    |     | x  |  |
| TRIGPOL_TRIGCR | Sets active on rising edge of trigger line C  |    |    |     | x  |  |

Alternatively, to associate all messages on a transmit channel with a trigger signal, use BTI429\_ChTriggerDefine.

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices . 3G Devices, which have a single external trigger can call BTI429\_MsgCreate with the MSGCRT429\_EXTRIG flag. The mapping of trigger lines to discretes is hardware dependent. Please consult the hardware manual for the Device.

## **WARNINGS**

None.

## **SEE ALSO**

```
BTI429_ChTriggerDefine, BTICard_CardTrigger, BTICard_CardTriggerEx, 
BTICard_CardTriggerValid
```