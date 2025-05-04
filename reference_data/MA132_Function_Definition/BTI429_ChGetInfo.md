# **ChGetInfo**

```
ULONG BTI429_ChGetInfo
(
     USHORT infotype, //Type of information to be returned
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
**RETURNS**

The requested information about the specified channel.

#### **DESCRIPTION**

Provides information about the functionality of the specified *channel*. The *infotype* constant listed below may be used to specify the requested information.

| infotype         |                   |                                                                                               |
|------------------|-------------------|-----------------------------------------------------------------------------------------------|
| Constant         | Returns           | Description                                                                                   |
| INFO429_PARAM    | 1=TRUE            | Channel is parametric                                                                         |
|                  | 0=FALSE           | Channel is not parametric                                                                     |
| INFO429_PWAVE    | 1=TRUE<br>0=FALSE | Channel supports parametric waveform (DAC)<br>Channel does not support parametric<br>waveform |
| INFO429_CMBIAS   | 1=TRUE<br>0=FALSE | Channel supports common-mode DC bias<br>Channel does not support common-mode DC<br>bias       |
| INFO429_OUTSTATE | 1=TRUE<br>0=FALSE | Channel supports Leg lifting/shorting<br>Channel does not support Leg lifting/shorting        |
| INFO429_PLAYBACK | 1=TRUE<br>0=FALSE | Channel supports playback mode<br>Channel does not support playback mode                      |

# **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Playback is determined per core, not per channel. Passing the INFO429\_PLAYBACK flag will return TRUE even on channels that cannot be playback (receive) channels if the core supports playback.

# **SEE ALSO**

```
BTI429_ParamAmplitudeConfig, 
BTI429_ParamBitRateConfig, BTI429_ParamWaveformConfig, 
BTI429_ParamWaveformConfigEx, 
BTI429_ChOutputStateSet
```
# ChIs429

```
BOOL BTI429_ChIs429
(
      INT channel, //Channel number to test
      HCORE hCore //Core handle
)
```
# **RETURNS**

TRUE if the channel is ARINC 429, otherwise FALSE.

#### **DESCRIPTION**

Checks to see if the channel number specified by *channel* is a 429 channel.

# **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

# **SEE ALSO**

BTI429\_ChGetInfo, BTI429\_ChIsRcv, BTI429\_ChIsXmt, BTI429\_ChGetCount