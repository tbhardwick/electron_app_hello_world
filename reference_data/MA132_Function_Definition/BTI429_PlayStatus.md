# **PlayStatus**

```
INT BTI429_PlayStatus
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
# **RETURNS**

A negative value if an error occurs, or the Playback FIFO's available Command Block space if successful.

## **DESCRIPTION**

Polls the Playback FIFO for *channel* and reports number of Command Blocks the FIFO is able to accept. A full Playback FIFO will return 0; an empty FIFO will return the FIFO capacity.

## **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

# **WARNINGS**

A call to BTI429\_ChConfig with CHCFG429\_PLAYBACK must precede this function.

All Playback Command Blocks are 8 USHORTs, thus the return value is in units of Command Blocks (not USHORTs).

## **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayPut\*, BTI429\_PlayBlockWr