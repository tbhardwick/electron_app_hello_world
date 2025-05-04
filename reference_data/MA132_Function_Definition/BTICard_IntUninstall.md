# **IntUninstall**

ERRVAL **BTICard\_IntUninstall**

```
(
      HCORE hCore //Core handle
)
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Removes the association between interrupts from the core specified by *hCore* and WIN32 event objects created by the BTICard\_IntInstall function. The Event Log List of the core remains unchanged.

# **DEVICE DEPENDENCY**

Applies to all Devices except those controlled via RPC.

## **WARNINGS**

This function must be called before the user's application terminates if BTICard\_IntInstall has been called.

# **SEE ALSO**

BTICard\_IntInstall