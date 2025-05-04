# **ErrDescStr**

```
LPCSTR BTICard_ErrDescStr
(
     ERRVAL errval, //An error value
     HCORE hCore //Core handle
```
### )

### **RETURNS**

A pointer to a character string describing the error specified by *errval*.

#### **DESCRIPTION**

Describes the error value specified by *errval*.

All functions of type ERRVAL return a negative value if an error occurs, or zero if successful. BTICard\_ErrDescStr returns a description of the specified error value.

*Note: This is a utility function and does not access the Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

**WARNINGS** None.

#### **SEE ALSO**

ERRVAL type functions