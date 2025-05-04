# **SysMonDescGet**

```
LPCSTR BTICard_SysMonDescGet
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A pointer to a character string describing the sensor or NULL if the sensor is not present.

## **DESCRIPTION**

Provides a formatted string that describes the sensor specified by *index*.

## **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

## **WARNINGS**

None.

## **SEE ALSO**

BTICard\_SysMonTypeGet