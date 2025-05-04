# **FilterRd**

```
MSGADDR BTI429_FilterRd
(
      INT label, //Label value
      INT sdi, //SDI pattern
      INT channel, //Number of receive channel
      HCORE hCore //Core handle
)
```
**RETURNS**

Address of the Message Record pointed to by the Filter for the given parameters.

### **DESCRIPTION**

Reads the address of the Message Record pointed to by the Filter Table for the specified *channel*, *label*, and *sdi* values.

### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

This value reads the address of the Message Record that a filter is pointing to, not the ARINC 429 data word. Use BTI429\_MsgDataRd to read the ARINC 429 data from a Message Record.

## **SEE ALSO**

BTI429\_FilterWr, BTI429\_FilterSet, BTI429\_FilterDefault, BTI429\_MsgDataRd