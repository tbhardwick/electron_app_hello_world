# **CardSyncValid**

```
USHORT BTICard_CardSyncValid
(
      HCORE hCore //Core handle
)
```
# **RETURNS**

A bitwise OR-ed value of *syncmask* (refer to table) for Device's valid syncs.

| syncmask          |                               |
|-------------------|-------------------------------|
| Constant          | Description                   |
| SYNCMASK_SYNCA    | Selects discrete sync line A  |
| SYNCMASK_PXITRIGA | Selects PXIe TRIG sync line A |
| SYNCMASK_SYNCB    | Selects discrete sync line B  |
| SYNCMASK_PXITRIGB | Selects PXIe TRIG sync line B |
| SYNCMASK_SYNCC    | Selects discrete sync line C  |
| SYNCMASK_PXITRIGC | Selects PXIe TRIG sync line C |
| SYNCMASK_PXISTARC | Selects PXIe STAR sync line C |

# **DESCRIPTION**

This function may be called from an application to verify Device-specific sync support.

# **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices.

The mapping of sync lines is hardware dependent. Please consult the hardware manual for the Device.

# **WARNINGS**

None.

#### **SEE ALSO**

```
 BTI1553_BCSyncDefine, BTI1553_MsgSyncDefine, BTI1553_RTSyncDefine
```