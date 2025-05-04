# **SysMonTypeGet**

```
ULONG BTICard_SysMonTypeGet
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
## **RETURNS**

The type of sensor or SYSMONTYPE\_NONE if the sensor is not present.

## **DESCRIPTION**

Reports the type of the sensor specified by *index by returning one of the* predefined constants below:

| Constant           | Description           |
|--------------------|-----------------------|
| SYSMONTYPE_NONE    | Sensor is not present |
| SYSMONTYPE_TEMP    | Temperature Sensor    |
| SYSMONTYPE_VOLTAGE | Voltage Sensor        |
| SYSMONTYPE_CURRENT | Current Sensor        |

## **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

# **WARNINGS**

None.

## **SEE ALSO**

BTICard\_SysMonDescGet