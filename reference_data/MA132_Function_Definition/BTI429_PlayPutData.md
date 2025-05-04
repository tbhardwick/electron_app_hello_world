# **PlayPutData**

```
ERRVAL BTI429_PlayPutData
(
      ULONG dataval, //Data word to Playback
      INT bitcount, //Bitcount to Playback
      USHORT gapval, //Following gap to Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Writes a DATA Command Block into *playbuf* at *offset*. A DATA Command Block directly stores the data to transmit instead of the address of a message. When a DATA Command Block is played back, the *channel* will transmit *bitcount* (number of bits) of *dataval* followed by the a following *gapval* length gap. Note that *gapval* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

![](_page_0_Figure_8.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

## **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum *offset*.

## **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayBlockWr, BTI429\_PlayStatus