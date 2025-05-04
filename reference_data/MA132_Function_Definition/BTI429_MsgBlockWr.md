# **MsgBlockWr**

```
MSGADDR BTI429_MsgBlockWr
(
     LPMSGFIELDS429 msgfields, //Pointer to source structure
     MSGADDR message, //Message to write to
     HCORE hCore //Core handle
)
```
**RETURNS**

The address of the message structure that was written.

#### **DESCRIPTION**

Writes an entire Message Record to the Device. This function is used to modify certain fields in a Message Record after the Message Record was read using BTI429\_MsgBlockRd. Only the msgdata, hitcount, mintime, maxtime, and paramflag fields should be modified. All other fields should be restored to the value read. See BTI429\_MsgBlockRd for a list of the Message Record fields.

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

Message field paramflag only applies to 6G Devices; rsvd for all other Devices.

## **WARNINGS**

Do not modify any fields other than the five listed above.

5G+ and 6G Devices must use BTI429\_ParamBitCountBuild when writing *bitcount* to *paramflag*. Failure to use BTI429\_ParamBitCountBuild will result in a default of 32 data word bits.

## **SEE ALSO**

```
BTI429_MsgBlockRd, BTI429_MsgCommWr, BTI429_MsgDataRd, BTI429_MsgDataWr
BTI429_MsgCreate, BTI429_FilterSet, BTI429_FilterDefault, 
BTI429_ParamBitCountBuild
```