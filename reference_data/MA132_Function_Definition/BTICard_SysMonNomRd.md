# **SysMonNomRd**

```
INT BTICard_SysMonNomRd
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The nominal voltage for a voltage sensor or SYSMONRD\_NOTVALID if the sensor is not present.

### **DESCRIPTION**

Reads the nominal voltage from the voltage sensor specified by *index*. The value is in units of mV. Call BTICard\_SysMonUserStr to convert the value to a formatted string.

## **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

### **WARNINGS**

Does not apply to temperature and current sensors.

## **SEE ALSO**

BTICard\_SysMonUserStr, BTICard\_SysMonValRd