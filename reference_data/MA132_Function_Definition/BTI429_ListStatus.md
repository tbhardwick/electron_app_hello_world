# **ListStatus**

```
INT BTI429_ListStatus
(
     LISTADDR list, //Address of the List Buffer
     HCORE hCore //Core handle
)
```
# **RETURNS**

The status value of the specified List Buffer.

### **DESCRIPTION**

Checks the status of the List Buffer specified by *list*. The status value can be tested using the predefined constants below:

| Constant     | Description                     |
|--------------|---------------------------------|
| STAT_EMPTY   | List Buffer is empty            |
| STAT_PARTIAL | List Buffer is partially filled |
| STAT_FULL    | List Buffer is full             |
| STAT_OFF     | List Buffer is off              |

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

# **SEE ALSO**

```
BTI429_ListAsyncCreate, BTI429_ListDataRd, BTI429_ListDataWr, 
BTI429_ListRcvCreate, BTI429_ListXmtCreate
```