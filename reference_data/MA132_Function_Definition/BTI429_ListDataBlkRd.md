# **ListDataBlkRd**

```
BOOL BTI429_ListDataBlkRd
(
     ULONG data[], //Pointer to destination buffer
     LPUSHORT datacount, //Size of destination, number of words read
     LISTADDR list, //List from which to read data
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A non-zero value if the function succeeded, or zero if unable to read the list.

## **DESCRIPTION**

Reads multiple 32-bit ARINC messages from the *list* and automatically updates the pointers for the next message. It is similar to BTI429\_ListDataRd except it reads multiple messages from the list in a single operation. *data* points to the buffer to receive the data words.

When called, *datacount* must point to a variable that contains maximum number of 32-bit ARINC messages that *data* can hold. On return, the variable will contain the number of 32-bit ARINC messages written to *data*.

The *list* must have been returned when the list was created using BTI429\_ListRcvCreate.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

Function fails if the list is in Ping-Pong mode.

## **SEE ALSO**

```
BTI429_ListDataBlkWr, BTI429_ListDataRd, BTI429_MsgDataRd, 
BTI429_ListRcvCreate, BTI429_ListXmtCreate
```