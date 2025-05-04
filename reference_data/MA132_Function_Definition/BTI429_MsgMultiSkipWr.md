# **MsgMultiSkipWr**

```
VOID BTI429_MsgMultiSkipWr
(
     BOOL skip[], //Array of skip values to write
     MSGADDR message[], //Array of message addresses
     INT count, //Number of messages to update
     HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if all messages were successfully updated, otherwise FALSE.

## **DESCRIPTION**

Writes the state of the skip bit for each of the Message Records specified by *message*. If the skip bit is FALSE (zero), the message will be processed as normal. If the bit is TRUE (non-zero), the message will be skipped and will not be processed.

The skip state of the nth element of the *message* array is set by the nth element of the *skip* array.

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

### **SEE ALSO**

BTI429\_MsgSkipRd, BTI429\_MsgSkipWr