# **PlayPutGap**

```
ERRVAL BTI429_PlayPutGap
(
      USHORT gapval, //Gap to Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

# **DESCRIPTION**

Writes a GAP Command Block into *playbuf* at *offset*. Note that *gapval* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

![](_page_0_Picture_8.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

# **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

### **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum offset.

# **SEE ALSO**

```
BTI429_ChConfig, BTI429_PlayBlockWr, BTI429_PlayStatus
```