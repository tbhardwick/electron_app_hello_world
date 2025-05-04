# **MsgGroupRd**

```
VOID BTI429_MsgGroupRd
(
     ULONG msgdata[], //Pointer to destination array 
     MSGADDR msgs[], //Array of Messages to read from
     INT nummsgs, //Number of messages to read
     HCORE hCore //Core handle
)
```
**RETURNS** None.

**DESCRIPTION** Reads the 32-bit ARINC data word from multiple Message Records in a single operation. It is similar to BTI429\_MsgDataRd except it reads *nummsgs* Message Records instead of just one Message Record. The array *msgs* points to the message addresses to read from. The array *msgdata* points to the memory to receive the 32-bit ARINC data word of each message.

### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

### **SEE ALSO**

BTI429\_MsgGroupWr, BTI429\_MsgDataRd