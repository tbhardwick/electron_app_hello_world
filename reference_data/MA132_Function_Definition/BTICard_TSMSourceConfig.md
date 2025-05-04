# **TSMSourceConfig**

```
ERRVAL BTICard_TSMSourceConfig
(
```

```
ULONG sourcecfg //Specifies configuration 
HCORE handleval //Device handle
```
### **RETURNS**

)

A negative value if an error occurs, otherwise zero.

### **DESCRIPTION**

Sets the Device's TSM as the output timing source with the options defined by *sourcecfg*. The constants below define the available operating modes of the TSM, which is able to source a single IRIG or PPS timing mode and simultaneously output the 10 MHz signal. Use TSMCFG\_IRIGA and TSMCFG\_IRIGB to select the IRIG speed.

The TSM is able to "relay" an IRIG timing input to a PPS timing output. To use this feature, call BTICard\_TSMSyncConfig first with the desired IRIG input, and call

BTICard\_TSMSourceConfig second with the desired PPS output, ensuring that the input and output are on different pins.

| sourcecfg       |                                                                            |
|-----------------|----------------------------------------------------------------------------|
| Constant        | Description                                                                |
| TSMCFG_NONE     | Select all default settings (bold below).                                  |
| TSMCFG_PPS0     | Outputs PPS signal                                                         |
| TSMCFG_PPS1     | Outputs PPS signal                                                         |
| TSMCFG_PWMIRIG0 | Outputs PWM IRIG signal at speed selected by TSMCFG_IRIGA and TSMCFG_IRIGB |
| TSMCFG_PWMIRIG1 | Outputs PWM IRIG signal at speed selected by TSMCFG_IRIGA and TSMCFG_IRIGB |
| TSMCFG_AMIRIG   | Outputs AM IRIG signal at speed selected by TSMCFG_IRIGA and TSMCFG_IRIGB  |
| TSMCFG_10MHZ    | Outputs 10 MHz signal                                                      |
| TSMCFG_IRIGA    | Selects 100 ms period IRIG                                                 |
| TSMCFG_IRIGB    | Selects 1 s period IRIG                                                    |

See specific hardware manuals for information regarding TSM pin specifications.

# **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

None.

# **SEE ALSO**

BTICard\_TSMSyncConfig