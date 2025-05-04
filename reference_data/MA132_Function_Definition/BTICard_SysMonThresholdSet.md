# **SysMonThresholdSet**

```
ERRVAL BTICard_SysMonThresholdSet
(
      BOOL enable, //Enable for the Sensor
      INT minval, //Minimum threshold value
      INT maxval, //Maximum threshold value
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Sets the user definable thresholds, in units of m°C, for the temperature sensor specified by *index*. The enable value and thresholds are passed through *enable*, *minval*, and *maxval* respectively. Once enabled, if the sensor value exceeds the the user definable thresholds, the BIT Status register will indicate a fault which can be read by calling BTICard\_BITStatusRd.

### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

## **WARNINGS**

Due to rounding, *minval* and *maxval* may not match what was set in BTICard\_SysMonThresholdSet. The values may be rounded to the nearest resolution.

Does not apply to voltage and current sensors.

minval and maxval should not exceed the system limits. Values exceeding the limits will be clamped to the system minimum and maximum accordingly. System limits vary by product (See product manual for the system limits).

## **SEE ALSO**

BTICard\_BITStatusRd, BTICard\_SysMonThresholdSet