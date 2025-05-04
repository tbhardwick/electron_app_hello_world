# **SeqFindNextDIO**

```
ERRVAL BTICard_SeqFindNextDIO
(
      LPSEQRECORDDIO *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Finds the next DIO record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the DIO records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORDDIO allows for easy handling of the data.

| SEQRECORDDIO structure |        |                                                |              |
|------------------------|--------|------------------------------------------------|--------------|
| Field                  | Size   | Description                                    | Version      |
| type                   | USHORT | The protocol and version number of this record | All versions |
| count                  | USHORT | The length of this record                      | All versions |
| bank                   | USHORT | Number of the bank                             | All versions |
| state                  | USHORT | State of the bank                              | 0 only       |
| timestamp              | ULONG  | Lower 32 bits of the time-tag value            | All versions |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value            | All versions |
| change                 | USHORT | Bitmask of discrete inputs that changed value  | 1 or greater |
| value                  | USHORT | Current value of discrete inputs               | 1 or greater |

# **DEVICE DEPENDENCY**

Applies to all 5G, 5G+, and 6G Devices. The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device. Also applies to 4G Devices with one or more discrete I/O modules (832 module). Please consult the OmniBus Discrete IO User's Manual for usage with 4G Devices.

When IRIG is enabled on a 4G Device, time-tags in Sequential Records will be in BCD format (see BTICard\_TimerStatus).

# **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

### **SEE ALSO**

```
BTICard_ExtDIOMonConfig,BTICard_SeqConfig, BTICard_SeqRd, 
BTICard_SeqFindInit
```