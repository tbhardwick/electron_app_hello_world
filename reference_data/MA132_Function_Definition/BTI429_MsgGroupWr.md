# **MsgGroupWr**

```
VOID BTI429_MsgGroupWr
(
     ULONG msgdata[], //Pointer to source array 
     MSGADDR msgs[], //Array of Messages to write to
     INT nummsgs, //Number of messages to write
     HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

### **DESCRIPTION**

Writes the 32-bit ARINC data word to multiple Message Records in a single operation. It is similar to BTI429\_MsgDataWr except it writes *nummsgs* Message Records instead of just one Message Record. The array *msgdata* points to the 32-bit ARINC data words to write. The array *msgs* points to message addresses to write to.

### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgGroupRd, BTI429\_MsgDataWr