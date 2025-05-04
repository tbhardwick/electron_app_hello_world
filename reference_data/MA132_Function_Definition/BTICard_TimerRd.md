# **TimerRd**

```
ULONG BTICard_TimerRd
(
      HCORE hCore //Core handle
)
```
### **RETURNS**

The current Device timer value.

## **DESCRIPTION**

Reads the lower 32-bits of the current value of the binary timer from the specified Device.

# **DEVICE DEPENDENCY**

Applies to all Devices.

6G Devices have a 64-bit binary timer with a 1 ns resolution.

5G and 5G+ Devices have a 48-bit binary timer with a 1 s resolution.

For 3G and 4G Devices, the binary timer exists in two parts: a hardware DSP timer, and a software extended value. Together these values make a 48-bit time value. BTICard\_TimerResolution is used to adjust which bits of this 48-bit value are used to make the 32-bit time-tag used in message structures and Sequential Records. BTICard\_TimerRd and BTICard\_TimerWr functions only read from and write to the software extended portion of this time value and not the hardware portion (due to the complexity of accounting for the rollover from the Host, an accurate reading of both the hardware and software part is not possible). The software extended portion used by BTICard\_TimerRd and BTICard\_TimerWr has a resolution of 4.096 ms.

To use the IRIG timer on a 4G Device to generate time-tag values for message structures and Sequential Records instead of the default binary timer, see BTICard\_IRIGConfig.

# **WARNINGS**

Use caution to account for rollover when using this function. For example, the lower 32-bits of a 1 ns binary timer will rollover approximately every 4 seconds.

Refer to BTICard\_Timer64Rd for access to the full binary timer.

# **SEE ALSO**

```
BTICard_Timer64Rd, BTICard_Timer64Wr, BTICard_TimerClear, 
BTICard_TimerResolution, BTICard_TimerWr, BTICard_IRIGConfig, 
BTICard_IRIGRd, BTICard_IRIGWr
```