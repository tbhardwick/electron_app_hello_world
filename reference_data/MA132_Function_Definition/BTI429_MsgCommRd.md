# **MsgCommRd**

```
MSGADDR BTI429_MsgCommRd
(
     LPMSGFIELDS429 msgfields, //Pointer to destination structure
     MSGADDR message, //Message from which to read
     HCORE hCore //Core handle
```
)

# **RETURNS**

The address of the message structure that was read.

### **DESCRIPTION**

Reads an entire message structure from the core. Similar to BTI429\_MsgBlockRd, except it uses non-contended accesses of Device memory. See BTI429\_MsgBlockRd for a list of the Message Record fields.

# **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

### **SEE ALSO**

```
BTI429_MsgBlockRd, BTI429_MsgCommWr, BTI429_MsgDataRd, BTI429_MsgDataWr, 
BTI429_FilterSet, BTI429_FilterDefault
```