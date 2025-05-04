# **SeqConfig**

ERRVAL **BTICard\_SeqConfig**

```
(
```
ULONG *ctrlflags, //Selects configuration options* HCORE *hCore //Core handle*

#### ) **RETURNS**

A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

Configures the Sequential Monitor of the core by allocating an onboard buffer and initializing internal pointers associated with the buffer.

| ctrlflags         |                                                                                   |
|-------------------|-----------------------------------------------------------------------------------|
| Constant          | Description                                                                       |
| SEQCFG_DEFAULT    | Select all default settings (bold below)                                          |
| SEQCFG_DISABLE    | Disable Sequential Record                                                         |
| SEQCFG_DMA        | Enables DMA mode (Device-dependent)                                               |
| SEQCFG_TCPNODELAY | Disable Nagle's algorithm on sequential DMAfor RPC devices (Device-dependent)     |
| SEQCFG_FILLHALT   | Enable Sequential Record in fill and halt mode                                    |
| SEQCFG_CONTINUOUS | Enable Sequential Record in continuous mode                                       |
| SEQCFG_DELTA      | Enable Sequential Record in delta mode (ARINC 429 only, 4G only)                  |
| SEQCFG_INTERVAL   | Enable Sequential Record in interval mode (ARINC 429 only, 4G only)               |
| SEQCFG_16K        | Allocate a 16 K Sequential Record buffer                                          |
| SEQCFG_32K        | Allocate a 32 K Sequential Record buffer                                          |
| SEQCFG_64K        | Allocate a 64 K Sequential Record buffer                                          |
| SEQCFG_128K       | Allocate a 128 K Sequential Record buffer                                         |
| SEQCFG_ALLAVAIL   | Allocate all available memory to the Sequential Record                            |
| SEQCFG_NOLOGFULL  | Do not generate an entry in the Event Log List when the Sequential Record is full |
| SEQCFG_LOGFULL    | Generate an entry in the Event Log List when the Sequential Record is full        |
| SEQCFG_NOLOGFREQ  | Do not generate entries in the Event Log List at user-defined frequency (see      |
|                   | BTICard_SeqLogFrequency)                                                          |
| SEQCFG_LOGFREQ    | Generate entries in the Event Log List at user-defined frequency (see             |
|                   | BTICard_SeqLogFrequency)                                                          |

*Note: It is highly recommended that the SEQCFG\_FILLHALT mode be used for the Sequential Record. This mode will allow for continuous recording of databus activity as long as the host keeps up with reading out record data. To allow the host flexibility in reading the Sequential Record, it is also recommended to use a value of SEQCFG\_128K for the size of the buffer.*

When using the SEQCFG\_CONTINOUS mode, databus activity will be continuously written to the Sequential Record without regard for the host reading data from the buffer. If the host attempts to read from it while the Device is running, the data returned could be corrupted. Therefore, when in this mode the Sequential Record should only be read while stopping and resuming the monitor using BTICard\_SeqStop and BTICard\_SeqResume.

# **DEVICE DEPENDENCY**

5G, 5G+, and 6G Devices always have a 16MB Sequential Record buffer. 5G, 5G+, and 6G 429 Devices do not support SEQCFG\_INTERVAL and SEQCFG\_DELTA flags. If used, they will simply be ignored.

Sequential DMA is supported on all 4G, 5G, 5G+, and 6G Devices; however, sequential DMA is not supported on 4G Devices when operating over USB.

# **WARNINGS**

If the SEQCFG\_ALLAVAIL flag is used, BTICard\_SeqConfig should be the last function called that allocates memory before BTICard\_CardStart is called.

#### **SEE ALSO**

BTICard\_SeqRd, BTICard\_SeqInterval, BTICard\_SeqLogFrequency