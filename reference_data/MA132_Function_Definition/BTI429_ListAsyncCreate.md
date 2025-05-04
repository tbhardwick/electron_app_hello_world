# **ListAsyncCreate**

```
LISTADDR BTI429_ListAsyncCreate
(
     ULONG ctrlflags, //Selects list options
     INT count, //One more than the number of entries in list
     INT channel, //Channel number to associate with List Buffer
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The Device address of the list if successful, otherwise zero.

## **DESCRIPTION**

Creates an asynchronous transmit List Buffer the size of *count* entries. The List Buffer is connected with the channel specified by *channel*. Every time a gap is encountered in a transmit Schedule, if data exists in the list, it will be transmitted during the gap time. If data does not exist in the list, then the gap time is

executed instead (gap times are processed in 36 bit-time increments, which is the size of a word plus a minimum gap). The maximum number of entries that may be stored in the list is *count* - 1 for FIFO mode and *count* for Circular mode.

*ctrlflags* specifies what type of List Buffer is created and the options associated with the List Buffer. Only FIFO mode is valid for an asynchronous List Buffer. ARINC 429 words are passed to the asynchronous List Buffer with BTI429\_ListDataWr and are transmitted only once.

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

3G and 4G Devices support up to 16,376 list entries while 5G, 5G+, and 6G Devices support up to 32,764 list entries.

## **WARNINGS**

Normally, this function should be used to associate an asynchronous List Buffer with a transmit channel before the Schedule is created for that channel. Do not use BTI429\_MsgDataRd and BTI429\_MsgDataWr as they will return incorrect results.

## **SEE ALSO**

```
BTI429_ListRcvCreate, BTI429_ListXmtCreate, BTI429_ListDataWr, 
BTI429_ListDataRd
```