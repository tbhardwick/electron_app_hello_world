# **SysMonClear**

ERRVAL **BTICard\_SysMonClear**

```
(
      HCARD hCard //Card handle
)
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Resets the historic maximum and minimum values for all sensors on the card specified by *hCard*.

## **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

### **WARNINGS**

None.

## **SEE ALSO**

BTICard\_SysMonTypeGet