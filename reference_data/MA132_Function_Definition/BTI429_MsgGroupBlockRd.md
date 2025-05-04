# **MsgGroupBlockRd**

```
VOID BTI429_MsgGroupBlockRd
(
     MSGFIELDS429 msgfields[], //Array of destination structures
     MSGADDR msgs[], //Array of message addresses
     INT nummsgs, //Number of messages to read
     HCORE hCore //Core handle
)
```
### **RETURNS**

None.

### **DESCRIPTION**

Reads a series of Message Records from the Device. *messages* points to an array of message addresses. The contents of each message in *messages* is written to its respective entry in *msgfields*. There must be *numsgs* entries in both *messages* and *msgfields*. See BTI429\_MsgBlockRd for a list of the Message Record fields.

## **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

## **WARNINGS**

None.

### **SEE ALSO**

```
BTI429_MsgBlockWr, BTI429_MsgCommRd, BTI429_MsgDataRd, 
BTI429_MsgGroupBlockWr, BTI429_MsgDataWr, BTI429_FilterSet, 
BTI429_FilterDefault
```