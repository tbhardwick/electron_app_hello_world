# **ListDataWr**

```
BOOL BTI429_ListDataWr
(
     ULONG value, //32-bit ARINC word value
     LISTADDR list, //List to write new data
     HCORE hCore //Core handle
)
```
# **RETURNS**

TRUE if the operation was successful, or FALSE if an error occurred or the function was unable to write to the list.

# **DESCRIPTION**

This function writes one message to the list and automatically updates the pointers for the next message. It is similar to BTI429\_MsgDataWr except it writes the 32-bit ARINC data value specified by *value* to a List Buffer. The *list* parameter is the value returned when the List Buffer was created using BTI429\_ListXmtCreate or BTI429\_ListAsyncCreate. The position to which the message is written in the list is determined by the mode of the list as follows:

- Circular mode: This mode is intended as a preloaded value List Buffer. With a circular List Buffer, this function will write to the next available position and will overwrite data in the buffer when it wraps around.
- FIFO mode: Data is written to one end of the list and is transmitted and removed from the other end of list. This function returns a zero if the List Buffer is full.
- Ping-Pong mode: When writing is complete, the data will be used for the next message transmission.

# **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*list* must be configured as a write transmit List Buffer using either BTI429\_ListXmtCreate or BTI429\_ListAsyncCreate

# **SEE ALSO**

```
BTI429_MsgDataWr, BTI429_ListDataRd, BTI429_ListRcvCreate, 
BTI429_ListXmtCreate, BTI429_ListAsyncCreate
```