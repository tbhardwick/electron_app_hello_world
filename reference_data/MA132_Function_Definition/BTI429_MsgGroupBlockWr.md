# **MsgGroupBlockWr**

```
VOID BTI429_MsgGroupBlockWr
(
     LPMSGFIELDS429 msgfields, //Pointer to source structures
     MSGADDR msgs[], //Messages to write to
     INT nummsgs, //Number of messages to write
     HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Writes a series of Message Records to the Device. *messages* points to an array of message addresses. Each entry in *msgfields* is written to its respective message in *messages*. There must be *numsgs* entries in both *msgs* and *msgfields*.

This function is used to modify certain fields in Message Records after the Message Records were read using BTI429\_MsgGroupBlockRd or BTI429\_MsgBlockRd. Only the *msgdata*, *hitcount*, *mintime*, *maxtime*, and *paramflag* fields should be modified. All other fields should be restored to the value read. See BTI429\_MsgBlockRd for a list of the Message Record fields.

## **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

Message field *paramflag* only applies to 5G+ and 6G Devices; *rsvd* for all other Devices.

## **WARNINGS**

Do not modify any fields other than the five listed above.

5G+ and 6G Devices must use BTI429\_ParamBitCountBuild when writing *bitcount* to *paramflag*. Failure to use BTI429\_ParamBitCountBuild will result in a default of 32 data word bits.

## **SEE ALSO**

```
BTI429_MsgGroupBlockRd, BTI429_MsgBlockRd, BTI429_MsgDataRd, 
BTI429_MsgDataWr, BTI429_MsgCreate, BTI429_FilterSet, 
BTI429_FilterDefault, BTI429_ParamBitCountBuild
```