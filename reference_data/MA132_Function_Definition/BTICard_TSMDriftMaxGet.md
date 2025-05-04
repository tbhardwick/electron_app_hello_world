## **TSMDriftMaxGet**

#### ERRVAL **BTICard\_TSMDriftMaxGet** (

```
LPULONG driftptr,//Pointer to variable to hold current drift max(ppt)
HCORE handleval //Device handle
```

```
)
```
### **RETURNS**

A negative value if an error, otherwise zero.

#### **DESCRIPTION**

Reads the current value of the maximum drift adjustment limit and places the value into the location pointed to by *driftptr*. Value is in parts-per-trillion.

### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

None.

### **SEE ALSO**

BTICard\_TSMDriftMaxSet