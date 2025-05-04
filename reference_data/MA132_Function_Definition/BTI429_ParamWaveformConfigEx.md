# **ParamWaveformConfigEx**

```
ERRVAL BTI429_ParamWaveformConfigEx
(
    ULONG configval, //Configuration options to set
    INT channel, //Channel number
    HCORE hCore //Core handle
)
```
LPPARAMFIELDS429 *paramflds*, *//Structure of waveform parameters*

#### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Configures *channel* as defined by *configval* (see table below) for parametric transmitter waveforms. Changes to *biasvolt* requires the PARAMCFG429\_CMBIAS constant. Default parameter values are selected with PARAMCFG429\_WAVEOFF; refer to the following tables.

| configval             |                                               |  |  |  |  |
|-----------------------|-----------------------------------------------|--|--|--|--|
| Constant              | Description                                   |  |  |  |  |
| PARAMCFG429_DEFAULT   | Select all default settings (bold below)      |  |  |  |  |
| PARAMCFG429_WAVEON    | Enables parametric waveform control (default) |  |  |  |  |
| PARAMCFG429_WAVEOFF   | Disables parametric waveform control          |  |  |  |  |
| PARAMCFG429_CMBIASOFF | Disables common-mode DC bias (default)        |  |  |  |  |
| PARAMCFG429_CMBIAS    | Enables common-mode DC bias                   |  |  |  |  |

The following figure and tables describe the waveform parameters and demonstrate various configurations for the ARINC 429 parametric waveform. Note that *highvolt*, *lowvolt*, and *nullvolt* are differential voltages; whereas, *biasvolt* is a single-ended voltage. Limits, resolution, and accuracy for each parameter are determined by the Device hardware; refer to the hardware manual more information.

| paramflds                |                                      |                     |                                                   |  |  |  |  |
|--------------------------|--------------------------------------|---------------------|---------------------------------------------------|--|--|--|--|
| Parameter<br>Description |                                      | Units               | PARAMCFG429_WAVEOFF                               |  |  |  |  |
| highvolt                 | High state<br>differential amplitude | millivolts<br>(mV)  | 10000 (10V)                                       |  |  |  |  |
| lowvolt                  | Low state<br>differential amplitude  | millivolts<br>(mV)  |                                                   |  |  |  |  |
| nullvolt                 | Null state<br>differential amplitude | millivolts<br>(mV)  | 0 (0V)                                            |  |  |  |  |
| biasvolt                 | Single-ended<br>common-mode bias     | millivolts<br>(mV)  | 0 (0V)                                            |  |  |  |  |
| highrise                 | High state<br>rise and fall time     | nanoseconds<br>(ns) | Lowspeed: 10000 (10µs)<br>Highspeed: 1800 (1.8µs) |  |  |  |  |
| lowrise                  | Low state<br>rise and fall time      | nanoseconds<br>(ns) | Lowspeed: 10000 (10µs)<br>Highspeed: 1800 (1.8µs) |  |  |  |  |

![](_page_0_Figure_12.jpeg)

|                               | Example  | Default | Double | Half   | 5V Null | 10V Offset    | 5V Common |
|-------------------------------|----------|---------|--------|--------|---------|---------------|-----------|
|                               |          |         | Ampl   | Ampl   | Offset  | Entire Signal | Mode Bias |
|                               | highvolt | 10000   | 20000  | 5000   | 10000   | 20000         | 10000     |
|                               | nullvolt | 0       | 0      | 0      | 5000    | 10000         | 0         |
| Parameter                     | lowvolt  | -10000  | -20000 | -5000  | -10000  | 0             | -10000    |
|                               | biasvolt | 0       | 0      | 0      | 0       | 0             | 5000      |
| (Refer To Figure)<br>Waveform | VD1      | 10 V    | 20 V   | 5 V    | 10 V    | 20 V          | 10 V      |
|                               | VDN      | 0 V     | 0 V    | 0 V    | 5 V     | 10 V          | 0 V       |
|                               | VD0      | -10 V   | -20 V  | -5 V   | -10 V   | 0 V           | -10 V     |
|                               | VP1      | 5 V     | 10 V   | 2.5 V  | 5 V     | 10 V          | 10 V      |
|                               | VPN      | 0 V     | 0 V    | 0 V    | 2.5 V   | 5 V           | 5 V       |
|                               | VP0      | -5 V    | -10 V  | -2.5 V | -5 V    | 0 V           | 0 V       |
|                               | VN1      | -5 V    | -10 V  | -2.5 V | -5 V    | -10 V         | 0 V       |
|                               | VNN      | 0 V     | 0 V    | 0 V    | -2.5 V  | -5 V          | 5 V       |
|                               | VN0      | 5 V     | 10 V   | 2.5 V  | 5 V     | 0 V           | 10 V      |

|                               | Example  | Low Speed Examples |             |                | High Speed Examples |           |                |
|-------------------------------|----------|--------------------|-------------|----------------|---------------------|-----------|----------------|
|                               |          | Default            | Double Ampl | Double Ampl    | Default             | Half Ampl | Half Ampl      |
|                               |          |                    | Std. Slew   | Std. Rise/Fall |                     | Std. Slew | Std. Rise/Fall |
| Parameter                     | highrise | 10000              | 20000       | 10000          | 1800                | 900       | 1800           |
|                               | lowrise  | 10000              | 20000       | 10000          | 1800                | 900       | 1800           |
|                               | tH       | 10 µs              | 20 µs       | 10 µs          | 1.8 µs              | 0.9 µs    | 1.8 µs         |
|                               | tL       | 10 µs              | 20 µs       | 10 µs          | 1.8 µs              | 0.9 µs    | 1.8 µs         |
| (Refer To Figure)<br>Waveform | SlewH    | 1 V/µs             | 1 V/µs      | 2 V/µs         | 5.6 V/µs            | 5.6 V/µs  | 2.8 V/µs       |
|                               | SlewL    | 1 V/µs             | 1 V/µs      | 2 V/µs         | 5.6 V/µs            | 5.6 V/µs  | 2.8 V/µs       |

#### **DEVICE DEPENDENCY**

Applies to 6G Devices with parametric waveform capability. Common-mode bias is supported on a subset of all parametric-capable channels. A channel can be tested for parametric waveform and common-mode bias functionality using BTI429\_ChGetInfo. Please consult the Device hardware manual for more information.

#### **WARNINGS**

Exact times and voltages depend on several factors: such as where the measurement occurs on the databus, analog and digital delays in the onboard circuits, and other analog characteristics of the databus.

## **SEE ALSO**

```
BTI429_ParamAmplitudeConfig, BTI429_ParamWaveformConfig, BTI429_ChGetInfo
```