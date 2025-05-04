# **ListDataBlkWr**

```
BOOL BTI429_ListDataBlkWr
(
     ULONG data[], //Pointer to source buffer
     LPUSHORT datacount, //Size of source, number of words written
     LISTADDR list, //List to write data to
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A non-zero value if the function succeeded, or zero if unable to wrote to the list.

## **DESCRIPTION**

Writes multiple 32-bit ARINC messages to the *list* and automatically updates the pointers for the next message. It is similar to BTI429\_ListDataWr except it writes multiple messages to the *list* in a single operation. *data* points to the buffer containing the data words to write.

When called, *datacount* must contain the total number of 32-bit data words that *data* contains.

The *list* must have been returned when the *list* was created using BTI429\_ListXmtCreate or BTI429\_ListAsyncCreate.

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Function fails if the list is in Ping-Pong mode.

## **SEE ALSO**

```
BTI429_ListDataBlkRd, BTI429_ListDataWr, BTI429_MsgDataWr, 
BTI429_ListRcvCreate, BTI429_ListXmtCreate
```