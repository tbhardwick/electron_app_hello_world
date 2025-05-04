# **BITConfig**

```
ERRVAL BTICard_BITConfig
```

```
(
     ULONG ctrlflags, //Selects configuration options
     HCARD hCard //Card handle
)
```
# **RETURNS**

A negative value if an error occurs or zero if successful.

# **DESCRIPTION**

Configures the Continuous Built-In Test (CBIT) functionality as defined by ctrlflags (see table below) for the card specified by hCard. Various functional blocks in the CBIT system can be enabled or disabled using the flags in the table below. Each of these blocks report to the BIT Status register that can be read using BTICard\_BITStatusRd. These blocks can also be configured to generate an Event Log list entry when an error occurs.

| ctrlflags                 |                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
|---------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Constant                  | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |
| BITCFG_DEFAULT            | Select all default settings (bold below)                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| BITCFG_MEMECC_ENABLE      | Enables the memory interface to operate in ECC mode. In this mode, if a single<br>bit error occurs, the read value will be corrected and<br>BITSTAT_SINGLE_BIT_ERR will be set in the BIT Status register. If a double<br>bit error occurs, the read value can't be corrected so the card will be stopped<br>and BITSTAT_DOUBLE_BIT_ERR will be set in the BIT Status register.                                                                                              |
| BITCFG_MEMECC_DISABLE     | Disables ECC operation of the memory.                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| BITCFG_FPGA_ENABLE        | Enables monitoring for Single Event Upsets (SEU) in the FPGA configuration.<br>In this mode, if a Single Event Upset is detected the card will be stopped and<br>BITSTAT_CBIT_FPGA_ERR will be set in the BIT Status register.                                                                                                                                                                                                                                               |
| BITCFG_FPGA_DISABLE       | Disables monitoring for Single Event Upsets in the FPGA configuration.                                                                                                                                                                                                                                                                                                                                                                                                       |
| BITCFG_PROTOCOL_ENABLE    | Enables CBIT in the 1553 Protocol Engine. Every 1553 word transmitted by the<br>Card will be monitored and checked for accuracy. If the transceiver is damaged<br>or there is a collision on the bus, the protocol error bit will be set in the BIT<br>Status register. In addition, the MSGERR1553_SYSTEM bit will also be set in the<br>1553 Message Record and Sequential Record of the transmission that failed.                                                         |
| BITCFG_PROTOCOL_DISABLE   | Disables CBIT in the 1553 Protocol Engine.                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| BITCFG_MEMECC_NOLOG       | Does not generate an event log entry when the ECC decoder detects a single<br>or double bit error in the on-card memory.                                                                                                                                                                                                                                                                                                                                                     |
| BITCFG_MEMECC_LOG         | Generates an event log entry when the ECC decoder detects a single or double<br>bit error in the on-card memory.                                                                                                                                                                                                                                                                                                                                                             |
| BITCFG_FPGA_NOLOG         | Does not generate an event log entry when an SEU is detected in the<br>FPGA Configuration.                                                                                                                                                                                                                                                                                                                                                                                   |
| BITCFG_FPGA_LOG           | Generates an event log entry when an SEU is detected in the FPGA<br>Configuration.                                                                                                                                                                                                                                                                                                                                                                                           |
| BITCFG_PROTOCOL_NOLOG     | Does not generate an event log entry when 1553 CBIT detects an<br>error.                                                                                                                                                                                                                                                                                                                                                                                                     |
| BITCFG_PROTOCOL_LOG       | Generates an event log entry when 1553 CBIT detects an error.                                                                                                                                                                                                                                                                                                                                                                                                                |
| BITCFG_CARD_STOPPED_NOLOG | Does not generate an event log entry when the Card is stopped due to<br>CBIT Errors.                                                                                                                                                                                                                                                                                                                                                                                         |
| BITCFG_CARD_STOPPED_LOG   | Generates an event log entry when the Card is stopped due to CBIT<br>Errors. When a temperature or voltage sensor value exceeds the<br>System limits for safe operation, the Card will automatically stop<br>protocol activity to reduce power draw. The Card will also stop when<br>an uncorrectable (double bit) error is detected in the memory or an<br>SEU is detected in the FPGA configuration. The card is stopped to<br>prevent the transmission of corrupted data. |

*(Continued on next page)*

#### (Continued from previous page)

| BITCFG_SYSMON_NOLOG | Does not generate an event log entry when SysMon detects that a<br>temperature sensor has exceeded the user definable thresholds.                                                                                   |
|---------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| BITCFG_SYSMON_LOG   | Generates an event log entry when the SysMon detects that a temperature<br>sensor has exceeded the user definable thresholds. User definable thresholds<br>can be configured by calling BTICard_SysMonThresholdSet. |

# **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

# **WARNINGS**

This function will clear any errors in the status read by BTICard\_BITStatusRd. In order to generate Event Log list entries, the Event Log must be configured by calling BTICard\_EventLogConfig.

## **SEE ALSO**

BTICard\_BITStatusRd, BTICard\_EventLogConfig, BTICard\_SysMonThresholdSet