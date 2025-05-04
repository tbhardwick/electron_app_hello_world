# **ChTriggerDefine**

```
ERRVAL BTI429_ChTriggerDefine
(
      BOOL enable, //Enable/disable external trigger
      USHORT trigmask, //Line(s) used for trigger signal
      USHORT trigval, //Active/inactive condition for trigger line(s)
      USHORT pinpolarity,//Active pin polarity (high/low) 
      INT xmtchan, //Transmit channel number
      HCORE hCore //Core handle
)
```
## **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Defines the transmit trigger settings for *xmtchan* on *hCore* and configures every message in the transmit schedule to wait for the defined trigger signal before transmitting. The input line(s) are specified by *trigmask* with an active trigger state being the combination of *trigval* and *pinpolarity* (see tables below).

BTI429\_ChTriggerDefine may be called during run time to redefine the trigger input settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Only one *trigmask* may be selected per trigger line (A, B, and C), and all combined states must be true for the trigger to occur. Triggers are both generation dependent and device dependent. Refer to Chapter [6](#page--1-0) and the following tables for more information.

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

| trigval          |                            |  |  |  |
|------------------|----------------------------|--|--|--|
| Constant         | Description                |  |  |  |
| TRIGVAL_TRIGAOFF | Trigger on line A inactive |  |  |  |
| TRIGVAL_TRIGAON  | Trigger on line A active   |  |  |  |
| TRIGVAL_TRIGBOFF | Trigger on line B inactive |  |  |  |
| TRIGVAL_TRIGBON  | Trigger on line B active   |  |  |  |
| TRIGVAL_TRIGCOFF | Trigger on line C inactive |  |  |  |
| TRIGVAL_TRIGCON  | Trigger on line C active   |  |  |  |

| pinpolarity    |                                               |    |    |     |    |  |  |
|----------------|-----------------------------------------------|----|----|-----|----|--|--|
| Constant       | Description                                   | 4G | 5G | 5G+ | 6G |  |  |
| TRIGPOL_TRIGAL | Sets active low polarity for trigger line A   | x  | x  | x   | x  |  |  |
| TRIGPOL_TRIGAH | Sets active high polarity for trigger line A  | x  | x  | x   | x  |  |  |
| TRIGPOL_TRIGAF | Sets active on falling edge of trigger line A |    |    |     | x  |  |  |
| TRIGPOL_TRIGAR | Sets active on rising edge of trigger line A  |    |    |     | x  |  |  |
| TRIGPOL_TRIGBL | Sets active low polarity for trigger line B   | x  | x  | x   | x  |  |  |
| TRIGPOL_TRIGBH | Sets active high polarity for trigger line B  | x  | x  | x   | x  |  |  |
| TRIGPOL_TRIGBF | Sets active on falling edge of trigger line B |    |    |     | x  |  |  |
| TRIGPOL_TRIGBR | Sets active on rising edge of trigger line B  |    |    |     | x  |  |  |
| TRIGPOL_TRIGCL | Sets active low polarity for trigger line C   | x  | x  | x   | x  |  |  |
| TRIGPOL_TRIGCH | Sets active high polarity for trigger line C  | x  | x  | x   | x  |  |  |
| TRIGPOL_TRIGCF | Sets active on falling edge of trigger line C |    |    |     | x  |  |  |
| TRIGPOL_TRIGCR | Sets active on rising edge of trigger line C  |    |    |     | x  |  |  |

Alternatively, to associate selected message(s) to a trigger signal, use BTI429\_MsgTriggerDefine.

### **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices. 3G Devices, which have a single external trigger, can call BTI429\_ChConfig with the CHCFG429\_EXTTRIG flag. The mapping of trigger lines is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

## **SEE ALSO**

BTI429\_MsgTriggerDefine, BTICard\_CardTrigger, BTICard\_CardTriggerEx, BTICard\_CardTriggerValid