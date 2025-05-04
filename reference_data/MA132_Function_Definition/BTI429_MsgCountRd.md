# **MsgCountRd**

```
INT BTI429_MsgCountRd
(
      MSGADDR message, //Message from which to read
      HCORE hCore //Core handle
)
```
### **RETURNS**

*bitcount* value from the *paramflag* field of the Message Record.

### **DESCRIPTION**

Reads the number of valid ARINC 429 data word bits from the Message Record specified by *message*. This value indicates the number of bits the channel will transmit if parametric bit count is supported by the hardware.

### **DEVICE DEPENDENCY**

Applies to transmit channels of 5G+ and 6G Devices.

## **WARNINGS**

None.

### **SEE ALSO**

BTI429\_MsgCountWr, BTI429\_MsgDataCountWr, BTI429\_MsgBlockRd