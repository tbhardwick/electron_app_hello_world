# **IRIGConfig**

ERRVAL **BTICard\_IRIGConfig** (

```
ULONG ctrlflags, //Selects IRIG configuration options
HCORE hCore //Core handle
```
)

#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Configures the onboard IRIG circuit as defined by *ctrlflags* (see table below) for the core specified by *hCore*. IRIG timers are configured and enabled for each core independently.

| Ctrlflags                              |                                          |             |     |
|----------------------------------------|------------------------------------------|-------------|-----|
| Constant                               | Description                              | Rcv         | Xmt |
| IRIGCFG_DEFAULT                        | Select all default settings (bold below) | x           | x   |
| IRIGCFG_ENABLE                         | Enables the IRIG timer                   | x           | x   |
| IRIGCFG_DISABLE                        | Disables the IRIG timer                  | x           | x   |
| IRIGCFG_SPEEDB                         | Enables IRIGB timing                     | x           | x   |
| IRIGCFG_SPEEDA                         | Enables IRIGA timing                     | x           | x   |
| IRIGCFG_INTERNAL Use internal IRIG bus |                                          | x           | x   |
|                                        | IRIGCFG_EXTERNAL Use external IRIG bus   | x           | x   |
| IRIGCFG_SLAVE                          | IRIG timer for this core is a slave      | x           |     |
| IRIGCFG_MASTER                         | IRIG timer for this core is the master   |             | x   |
| IRIGCFG_PPS                            | Enables pulse per second signaling       | x           | x   |
| IRIGCFG_PWM                            | Enables pulse width modulated signaling  | x           | x   |
| IRIGCFG_AM                             | Enables amplitude modulated signaling    | 5G, 5G+, 6G | 6G  |

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices. Amplitude modulated decoding is only supported by 5G, 5G+, and 6G Devices. Amplitude modulated transmission is only supported by 6G Devices. When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format.

### **WARNINGS**

Rounding is used when the IRIGCFG\_PPS option is enabled. Values get rounded up when above 500 ms and are rounded down when below 500 ms if signaling is configured for pulse per second (PPS).

## **SEE ALSO**

BTICard\_IRIGRd,BTICard\_IRIGWr,BTICard\_IRIGInputThresholdSet, BTICard\_IRIGInputThresholdGet, BTICard\_TimerStatus