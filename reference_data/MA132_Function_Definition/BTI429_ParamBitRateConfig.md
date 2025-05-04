# **ParamBitRateConfig**

```
ERRVAL BTI429_ParamBitRateConfig
(
     ULONG configval, //Configuration options to set
     USHORT divval, //Divide value
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Configures *channel* for non-standard bus frequency as defined by *configval* (see table below) and sets the clock divider value to *divval*. If parametric bit rate control is not set or turned off, then the frequency reverts to the default value.

| configval              |                                          |  |
|------------------------|------------------------------------------|--|
| Constant               | Description                              |  |
| PARAMCFG429_DEFAULT    | Select all default settings (bold below) |  |
| PARAMCFG429_BITRATEON  | Enables parametric bit rate control      |  |
| PARAMCFG429_BITRATEOFF | Disables parametric bit rate control     |  |

### **DEVICE DEPENDENCY**

Applies to all 5G, 5G+, and 6G Devices. For 3G or 4G, this function may only be used on a 429 channel with parametric capability. A 3G or 4G channel can be tested for bit rate parametric capability using BTI429\_ChGetInfo.

Below are the formulas for rate calculations using *divval*. 5G Devices support a maximum *divval* of 255. 5G+ and 6G Devices support a minimum *divval* of 100 (200kHz/25kHz) and a maximum *divval* of 1023 (19.5kHz/2.4kHz).

| Formulas for Rate Calculations in kHz |                      |                    |
|---------------------------------------|----------------------|--------------------|
| Generation/Type                       | High Speed           | Low Speed          |
| 4G Encoder and Decoder                | 4,000 / (divval + 1) | 500 / (divval + 1) |
| 5G Decoder                            | 200 / (divval + 1)   | 25 / (divval + 1)  |
| 5G Encoder                            | 1,000 / (divval + 1) | 125 / (divval + 1) |
| 5G+ and 6G Encoder and Decoder        | 20,000 / divval      | 2,500 / divval     |

## **WARNINGS**

When converting an application from a 5G Device to a 5G+ Device (e.g UA14XX to UA24XX), calls to ParamBitRateConfig must update *divval* with the value calculated from the formula in the above table. Failure to do so will result in an incorrect bus frequency.

### **SEE ALSO**

BTI429\_ParamAmplitudeConfig, BTI429\_ChGetInfo