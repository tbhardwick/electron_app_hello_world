## **TSMDriftRd**

ERRVAL **BTICard\_TSMDriftRd**

```
(
```
LPINT *driftptr, //Pointer to variable to hold current drift (ppt)* HCORE *handleval //Device handle* 

#### **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Places the current two's complement representation of the commanded drift into the location pointed to by *driftptr*. Value is in parts-per-trillion.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

None.

### **SEE ALSO**

BTICard\_TSMDriftWr