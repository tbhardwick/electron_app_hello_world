# **SysMonMaxRd**

```
INT BTICard_SysMonMaxRd
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The historic maximum value of a sensor or SYSMONRD\_NOTVALID if the sensor is not present or the historic maximum value is not valid.

# **DESCRIPTION**

Reads the historic maximum value from the sensor specified by *index*. The value is in units of mV, mA, or m°C depending on the sensor type. Call BTICard\_SysMonUserStr to convert the value to a formatted string.

To reset the historic maximum value, call BTICard\_SysMonClear.

# **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

# **WARNINGS**

This function will return SYSMONRD\_NOTVALID for up to 800 ms after the first call to BTICard\_CardOpen and up to 150 ms after any call to BTICard\_SysMonClear to allow SysMon to get valid data.

*Note: Due to occasionally spurious values that may be reported by the voltage/current sensor, a second read of the system monitor to confirm an error is recommended prior to software acting on the assumed error data. See BTICard\_SysMonValRd for additional information.*

# **SEE ALSO**

```
BTICard_SysMonClear, BTICard_SysMonMinRd, BTICard_SysMonUserStr, 
BTICard_SysMonValRd
```