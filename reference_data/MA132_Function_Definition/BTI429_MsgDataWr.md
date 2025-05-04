# **MsgDataWr**

```
VOID BTI429_MsgDataWr
(
     ULONG value, //Value of data to write to message
     MSGADDR message, //Message to receive new data
     HCORE hCore //Core handle
```
)

#### **RETURNS**

None.

#### **DESCRIPTION**

Writes the 32-bit ARINC data value specified by value into the Message Record specified by message.

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

### **SEE ALSO**

```
BTI429_MsgDataRd, BTI429_MsgDataCountWr, BTI429_MsgCountWr, 
BTI429_MsgCreate, BTI429_MsgBlockRd, BTI429_MsgCommWr, BTI429_MsgBlockWr
```