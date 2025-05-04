# **ListDataRd**

```
ULONG BTI429_ListDataRd
(
     LISTADDR list, //List Buffer from which to read data
     HCORE hCore //Core handle
)
```
## **RETURNS**

The 32-bit value of the ARINC word if there is a word in the list, or zero if it is empty.

## **DESCRIPTION**

This function reads one message from the list, and automatically updates the pointers for the next message. It is similar to BTI429\_MsgDataRd except it reads from a List Buffer. The *list* parameter is the value returned when the List Buffer was created using BTI429\_ListRcvCreate. The position of the message to be read is determined by the mode of the list as follows:

- Circular mode: Not valid for a receive List Buffer.
- FIFO mode: Reads the oldest complete message received.
- Ping-Pong mode: Reads the newest complete message received.

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

*list* must be configured as a receive List Buffer using BTI429\_ListRcvCreate.

## **SEE ALSO**

```
BTI429_MsgDataRd, BTI429_ListDataWr, BTI429_ListRcvCreate, 
BTI429_ListXmtCreate
```