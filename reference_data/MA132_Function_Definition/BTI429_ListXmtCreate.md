# **ListXmtCreate**

```
LISTADDR BTI429_ListXmtCreate
(
     ULONG ctrlflags, //Selects list options
     INT count, //One more than the number of entries in list
     MSGADDR message, //Message address to associate with List Buffer
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The Device address of the list if successful, otherwise zero.

### **DESCRIPTION**

Creates a transmit List Buffer the size of *count* entries. The List Buffer is connected with a Message Record so that the data is processed in the list instead of in the Message Record. The *ctrlflags* specify what type of List Buffer is created and the options associated with the List Buffer. The maximum number of entries that may be stored in the list is *count* - 1 for FIFO mode and *count* for Circular mode.

| ctrlflags           |                                                                                       |
|---------------------|---------------------------------------------------------------------------------------|
| Constant            | Description                                                                           |
| LISTCRT429_DEFAULT  | Select all default settings (bold below)                                              |
| LISTCRT429_FIFO     | Selects FIFO mode                                                                     |
| LISTCRT429_PINGPONG | Selects ping-pong mode                                                                |
| LISTCRT429_CIRCULAR | Selects circular mode                                                                 |
| LISTCRT429_NOLOG    | An entry will not be created in the Event Log List when the List Buffer is empty/full |
| LISTCRT429_LOG      | An entry will be created in the Event Log List when the List Buffer is empty/full     |

## **DEVICE DEPENDENCY**

LISTCRT429\_PINGPONG is not supported by 5G, 5G+, and 6G Devices since they inherently have protection for data coherency (use BTI429\_MsgCommRd and BTI429\_MsgCommWr instead). If LISTCRT429\_PINGPONG is used against 5G and 6G Devices the API call will fail and returns LISTADDR of NULL.

3G and 4G Devices support up to 16,376 list entries while 5G, 5G+, and 6G Devices support up to 32,764 list entries.

# **WARNINGS**

After connecting *message* with a List Buffer, the functions BTI429\_ListDataRd and BTI429\_ListDataWr must be used to read or write the data. Do not use BTI429\_MsgDataRd and BTI429\_MsgDataWr as they will return incorrect results.

# **SEE ALSO**

```
BTI429_ListRcvCreate, BTI429_ListAsyncCreate BTI429_ListDataWr, 
BTI429_ListDataRd
```