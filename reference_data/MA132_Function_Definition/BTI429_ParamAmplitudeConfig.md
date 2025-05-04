# **ParamAmplitudeConfig**

```
ERRVAL BTI429_ParamAmplitudeConfig
(
     ULONG configval, //Configuration options to set
     USHORT dacval, //12-bit digital-analog converter value
     INT xmtchan, //Transmit channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

# **DESCRIPTION**

Enables variable transmit amplitude control as defined by *configval* (see table below) on the transmit channel specified by *xmtchan* and sets the digital-to-analog converter to *dacval*. If this parametric control is not used or is disabled, then the amplitude reverts to default (full) amplitude.

| configval           |                                          |
|---------------------|------------------------------------------|
| Constant            | Description                              |
| PARAMCFG429_DEFAULT | Select all default settings (bold below) |
| PARAMCFG429_AMPLON  | Enables parametric amplitude control     |
| PARAMCFG429_AMPLOFF | Disables parametric amplitude control    |

# **DEVICE DEPENDENCY**

This function may only be used with a 429 transmit channel with parametric capability. A channel can be tested for parametric capability using BTI429\_ChGetInfo.

# **WARNINGS**

None.

# **SEE ALSO**

BTI429\_ParamBitRateConfig, BTI429\_ChGetInfo

# ParamBitCountBuild

```
USHORT BTI429_ParamBitCountBuild
(
      INT bitcount //Value of bitcount
)
```
# **RETURNS**

Message field formatted for paramflag (containing bitcount).

# **DESCRIPTION**

Adds *bitcount* to the *paramflag* field of a message record. That record can be updated on the card with BTI429\_MsgBlockWr.

# **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

### **WARNINGS**

None.

# **SEE ALSO**

BTI429\_MsgBlockWr