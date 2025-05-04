# **PlayBlockWr**

```
ERRVAL BTI429_PlayBlockWr
(
     LPUSHORT playbuf, //Pointer to start of host playback buffer
     INT count, //Command Blocks to write to Playback FIFO
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Transfers *count* number of Command Blocks from the start of *playbuf* host playback buffer to the Device Playback FIFO. Refer to the *Playback Mode* section earlier in this manual for more information.

![](_page_0_Figure_8.jpeg)

*Example of Transferring 3 (of 4) Command Blocks to Device*

## **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

### **WARNINGS**

A call to BTI429\_ChConfig with CHCFG429\_PLAYBACK must precede this function. Additionally, any Command Block to be transferred must have been written to *playbuf* with BTI429\_PlayPut\* (e.g. BTI429\_PlayPutData) prior to calling this function; BTI429\_PlayBlockWr does not modify *playbuf*.

The Playback FIFO may be polled for status and availability with BTI429\_PlayStatus. Polling the Playback FIFO prevents overflow conditions.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that both *count* and Playback FIFO status are in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum *count*.

#### **SEE ALSO**

```
BTI429_ChConfig, BTI429_PlayPut*, BTI429_PlayStatus
```