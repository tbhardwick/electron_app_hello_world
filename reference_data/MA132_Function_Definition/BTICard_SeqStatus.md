# **SeqStatus**

```
BOOL BTICard_SeqStatus
(
      HCORE hCore //Core handle
)
```
# **RETURNS**

The status value of the Sequential Record.

#### **DESCRIPTION**

Checks the status of the Sequential Record. The status value can be tested using the predefined constants below:

| Constant     | Description                           |
|--------------|---------------------------------------|
| STAT_EMPTY   | Sequential Record is empty            |
| STAT_PARTIAL | Sequential Record is partially filled |
| STAT_FULL    | Sequential Record is full             |
| STAT_OFF     | Sequential Record is off              |

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

The operation of the SeqStatus is configuration and Device dependent. All 4G Devices and 5G, 5G+, 6G RPC Devices configured for DMA can return STAT\_EMPTY when the internal sequential buffer is empty but the DMA buffer contains data.

# **SEE ALSO**

```
BTICard_SeqStart, BTICard_SeqStop, BTICard_SeqResume
```