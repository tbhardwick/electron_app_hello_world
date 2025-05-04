# **MsgIsAccessed**

```
BOOL BTI429_MsgIsAccessed
(
      MSGADDR message, //Message to test if it has been accessed
      HCORE hCore //Core handle
)
```
# **RETURNS**

TRUE if a message has been accessed (if the Hit bit is set), otherwise FALSE.

### **DESCRIPTION**

This function indicates that a Message Record has been processed by either transmission or reception of a message. BTI429\_MsgIsAccessed returns the value of the Hit bit, then clears it.

## **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

# **SEE ALSO**

```
BTI429_MsgDataRd, BTI429_MsgCreate, BTI429_MsgBlockRd, BTI429_MsgCommRd, 
BTI429_MsgBlockWr, BTI429_FilterSet, BTI429_FilterDefault
```