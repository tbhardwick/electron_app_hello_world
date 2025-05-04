# **CardTriggerValid**

```
USHORT BTICard_CardTriggerValid
(
      HCORE hCore //Core handle
)
```
## **RETURNS**

A bitwise OR-ed value of *trigmask* (refer to table) for Device's valid triggers.

| trigmask          |                                  |
|-------------------|----------------------------------|
| Constant          | Description                      |
| TRIGMASK_TRIGA    | Selects discrete trigger line A  |
| TRIGMASK_PXITRIGA | Selects PXIe TRIG trigger line A |
| TRIGMASK_PXISTARA | Selects PXIe STAR trigger line A |
| TRIGMASK_TRIGB    | Selects discrete trigger line B  |
| TRIGMASK_PXITRIGB | Selects PXIe TRIG trigger line B |
| TRIGMASK_PXISTARB | Selects PXIe STAR trigger line B |
| TRIGMASK_TRIGC    | Selects discrete trigger line C  |
| TRIGMASK_PXITRIGC | Selects PXIe TRIG trigger line C |
| TRIGMASK_PXISTARC | Selects PXIe STAR trigger line C |

## **DESCRIPTION**

This function may be called from an application to verify Device-specific trigger support.

## **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices.

PXIe Triggers are only available on PXIe Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTI1553\_BCTriggerDefine,BTI1553\_ErrorTriggerDefine, BTI1553\_MsgTriggerDefine