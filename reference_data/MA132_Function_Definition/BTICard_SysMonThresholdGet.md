# **SysMonThresholdGet**

```
ERRVAL BTICard_SysMonThresholdGet
(
      BOOL *enable,//Pointer to variable to receive enable value
      LPINT minval,//Pointer to variable to receive minimum threshold value
      LPINT maxval,//Pointer to variable to receive maximum threshold value
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

Reads the user definable thresholds, in units of m°C, for the temperature sensor specified by *index*. These user thresholds are disabled and set to System Limits at power on and can be modified by calling BTICard\_SysMonThresholdSet. The enable value and thresholds are passed through *enable*, *minval*, and *maxval* respectively.

Call BTICard\_SysMonUserStr to convert *minval* or *maxval* to a formatted string.

## **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

### **WARNINGS**

Due to rounding, *minval* and *maxval* may not match what was set in BTICard\_SysMonThresholdSet. The values may be rounded to the nearest resolution.

Does not apply to voltage and current sensors.

## **SEE ALSO**

BTICard\_BITStatusRd, BTICard\_SysMonThresholdSet