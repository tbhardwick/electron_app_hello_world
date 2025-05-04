# **IRIGInputThresholdSet**

ERRVAL **BTICard\_IRIGInputThresholdSet**

```
(
USHORT dacval, //Digital-analog converter value
HCORE hCore //Core handle
```

```
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Sets the threshold of the IRIG input circuitry. The parameter *dacval* represents a linear voltage scale from 0x0000 to 0xFFF0 see product manual for voltage range. Only the most significant 12 bits are used.

### **DEVICE DEPENDENCY**

Applies to all 5G, 5G+, and 6G Devices, except AB1xxx, NG1, NG2, and NG3 Devices.

## **WARNINGS**

Should only be used when IRIG circuit is configured for Amplitude Modulated (AM) input signaling. Changing the input threshold affects both AM and Pulse Width Modulated decoding.

## **SEE ALSO**

BTICard\_IRIGConfig, BTICard\_IRIGInputThresholdGet