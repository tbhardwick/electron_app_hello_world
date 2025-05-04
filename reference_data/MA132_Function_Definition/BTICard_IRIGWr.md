# **IRIGWr**

ERRVAL **BTICard\_IRIGWr**

```
LPBTIIRIGTIME irigtime, //Pointer to an IRIG time array
HCORE hCore //Core handle
```
# )

(

**RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Sets the IRIG timer to *irigtime* on the core specified by *hCore*.

| IRIGTIME structure |        |                                         |
|--------------------|--------|-----------------------------------------|
| Field              | Size   | Description                             |
| days               | USHORT | Day of the year (see Device Dependency) |
| hours              | USHORT | Hours after midnight (0–23)             |
| min                | USHORT | Minutes after hour (0–59)               |
| sec                | USHORT | Seconds after minute (0–59)             |
| msec               | USHORT | Milliseconds after minute (0–999)       |
| usec               | USHORT | Microseconds after millisecond (0–999)  |

*Note: To write to the binary timer, see BTICard\_TimerWr.*

# **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices.

For 4G Devices, the days count 0-365 with 0=January 1st .

For 5G and 5G+ Devices, the days count 0-364 with 0=January 1st .

For 6G Devices, the days count to a user programmable rollover.

## **WARNINGS**

None.

### **SEE ALSO**

BTICard\_IRIGRd, BTICard\_IRIGConfig, BTICard\_TimerWr