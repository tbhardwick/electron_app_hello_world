# **MsgCommWr**

```
MSGADDR BTI429_MsgCommWr
(
     LPMSGFIELDS429 msgfields, //Pointer to source structure
     MSGADDR message, //Message to write to
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The address of the message structure that was written.

#### **DESCRIPTION**

Writes an entire message structure to the core. Similar to BTI429\_MsgBlkWr, except it uses noncontended accesses of Device memory. This function is used to modify certain fields in a Message Record after the Message Record was read using BTI429\_MsgBlockRd. Only the *msgdata*, *hitcount*, *mintime*, *maxtime*, and *paramflag* fields should be modified. All other fields should be restored to the value read. See BTI429\_MsgBlockRd for a list of the Message Record fields.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

Message field *paramflag* only applies to 6G Devices; *rsvd* for all other Devices.

## **WARNINGS**

Do not modify any fields other than the five listed above.

5G+ and 6G Devices must use BTI429\_ParamBitCountBuild when writing *bitcount* to *paramflag*. Failure to use BTI429\_ParamBitCountBuild will result in a default of 32 data word bits.

### **SEE ALSO**

```
BTI429_MsgCommRd, BTI429_MsgBlockWr, BTI429_MsgDataWr, BTI429_MsgCreate
BTI429_FilterSet, BTI429_FilterDefault, BTI429_ParamBitCountBuild
```