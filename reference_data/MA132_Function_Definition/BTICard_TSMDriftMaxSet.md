# **TSMDriftMaxSet**

#### ERRVAL **BTICard\_TSMDriftMaxSet** (

```
ULONG drift, //Value for maximum drift (ppt)
HCORE handleval //Device handle
```
#### **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the value *drift* to the Device's maximum drift adjustment limit. This value is the limit the device will attempt to bias the clock frequency to match an incoming source frequency. Value is in parts-per-trillion and is limited to 425 ppm (425,000,000 ppt).

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

None.

### **SEE ALSO**

BTICard\_TSMDriftMaxGet