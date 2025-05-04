# **CardTypeStr**

LPCSTR **BTICard\_CardTypeStr** ( HCORE *hCore //Core handle* )

## **RETURNS**

A pointer to a character string describing the Device specified by *hCore*.

### **DESCRIPTION**

Returns the type of Device specified by *hCore*.

BTICard\_CardTypeStr identifies the family to which a Device belongs. Inside of that family, BTICard\_CardProductStr specifies product information such as model number, level of functionality, or configuration. Combine these functions to identify your Device.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_CardGetInfo, BTICard\_CardProductStr