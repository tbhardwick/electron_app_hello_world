# **SysMonUserStr**

```
LPCSTR BTICard_SysMonUserStr
(
      INT value, //Value to be formatted
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A pointer to a character string containing the value and units for a sensor or NULL if sensor is not present.

## **DESCRIPTION**

Returns a formatted character string containing the value and units for the sensor specified by *index*. The parameter *value* is typically read by calling BTICard\_SysMonValRd, BTICard\_SysMonNomRd, BTICard\_SysMonMinRd, or BTICard\_SysMonMaxRd.

### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

## **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SysMonValRd, BTICard\_SysMonNomRd, BTICard\_SysMonMinRd, BTICard\_SysMonMaxRd