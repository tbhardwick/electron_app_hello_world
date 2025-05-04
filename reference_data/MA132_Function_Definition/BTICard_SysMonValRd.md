# **SysMonValRd**

```
INT BTICard_SysMonValRd
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The current value for a sensor or SYSMONRD\_NOTVALID if the sensor is not present.

## **DESCRIPTION**

Reads the current value of the sensor specified by *index*. The units for the current value are in mV, mA, or m°C depending on the sensor type. Call BTICard\_SysMonUserStr to convert the value to a formatted string.

# **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

### **WARNINGS**

This function will return SYSMONRD\_NOTVALID for up to 800 ms after the first call to BTICard\_CardOpen and up to 150 ms after any call to BTICard\_SysMonClear to allow SysMon to get valid data.

*Note: Due to occasionally spurious values that may be reported by the voltage/current sensor, a second read of the system monitor to confirm an error is recommended prior to software acting on the assumed error data. A minimum wait time of 150 ms is required before issuing a second read of the system monitor to ensure that a new value has been sampled. Call BTICard\_SysMonClear to clear min/max values if a spurious voltage or current values is read.*

# **SEE ALSO**

BTICard\_SysMonUserStr, BTICard\_SysMonMaxRd, BTICard\_SysMonMinRd