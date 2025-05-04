# **CardClose**

ERRVAL **BTICard\_CardClose** (

```
HCARD hCard //Card handle
)
```
## **RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Disables access to the specified Device and releases the associated hardware resources (e.g., memory and I/O space, interrupt number, and DMA channel). BTICard\_CardClose closes the Device and all of its cores opened with BTICard\_CoreOpen. This function does not stop the core(s) from operating (use BTICard\_CardStop to stop each core).

### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

Before a program terminates, this function MUST be called to release the associated hardware resources. This is especially important in Microsoft Windows operating systems.

### **SEE ALSO**

BTICard\_CardOpen, BTICard\_CoreOpen, BTICard\_CardStop