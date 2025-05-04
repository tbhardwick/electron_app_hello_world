# **PXIStatus**

#### ERRVAL **BTICard\_PXIStatus**

**(**

)

```
LPULONG infoptr, //Pointer to variable to receive info
USHORT infotype, //Info status type
HCORE hCore //Core handle
```
**RETURNS**

A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

The desired status is returned through *infoptr* by setting *infotype* to one of the following parameters:

| Status Selection |                                        |
|------------------|----------------------------------------|
| infotype         | infoptr                                |
| PXITYPE_GEOADDR  | Geographical Address                   |
| PXITYPE_CLKSEL   | PXIe Clock Selection                   |
|                  | 0 = PXIe_CLK100                        |
|                  | 1 = Onboard 100 MHz Oscillator         |
| PXITYPE_TRIGVERS | Version of the BTI PXIe Trigger Engine |
| PXITYPE_OUTEN    | Bitmask of PXI_TRIG drive status       |

## **DEVICE DEPENDENCY**

Only applies to 6G Devices with PXIe functionality.

Please consult the Device hardware manual for more information.

### **WARNINGS**

None.

**SEE ALSO**

None.