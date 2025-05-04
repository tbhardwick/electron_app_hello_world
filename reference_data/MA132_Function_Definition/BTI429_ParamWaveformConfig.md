# **ParamWaveformConfig**

```
ERRVAL BTI429_ParamWaveformConfig
(
      ULONG configval, //Configuration options to set
      INT highvolt, //High state differential amplitude (in millivolts)
      INT lowvolt, //Low state differential amplitude (in millivolts)
      INT nullvolt, //Null state differential amplitude (in millivolts)
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```

```
RETURNS
```
A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

```
Configures channel as defined by configval for parametric transmitter waveforms. All 
parameters (highvolt, lowvolt, and nullvolt) are internally passed to 
BTI429_ParamWaveformConfigEx with all other waveform parameters (biasvolt, highrise, 
and lowrise) configured as default values. Refer to BTI429_ParamWaveformConfigEx for 
more information regarding configval values, parameter definitions, and waveform examples.
```
#### **DEVICE DEPENDENCY**

Applies to 6G Devices with parametric waveform capability. A channel can be tested for functionality using BTI429\_ChGetInfo. Please consult the Device hardware manual for more information.

#### **WARNINGS**

Exact times and voltages depend on several factors: such as where the measurement occurs on the databus, analog and digital delays in the onboard circuits, and other analog characteristics of the databus.

## **SEE ALSO**

```
BTI429_ParamAmplitudeConfig, BTI429_ParamWaveformConfigEx, 
BTI429_ChGetInfo
```