## **IRIGRd**

(

)

ERRVAL **BTICard\_IRIGRd**

```
LPBTIIRIGTIME irigtime, //Pointer to an IRIG time structure
HCORE hCore //Core handle
```
# **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Reads the current value of the IRIG timer from the core specified by *hCore* and puts the value into the *irigtime* structure.

| BTIIRIGTIME structure |        |                                            |
|-----------------------|--------|--------------------------------------------|
| Field                 | Size   | Description                                |
| days                  | USHORT | Day of the year (see Device Dependency)    |
| hours                 | USHORT | Hours after midnight (0–23)                |
| min                   | USHORT | Minutes after the hour (0–59)              |
| sec                   | USHORT | Seconds after the minute (0–59)            |
| msec                  | USHORT | Milliseconds after the second (0–999)      |
| usec                  | USHORT | Microseconds after the millisecond (0–999) |

*Note: To read the binary timer, see BTICard\_TimerRd.*

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices.

For 4G Devices, the days count 0-365 with 0=January 1st .

For 5G and 5G+ Devices, the days count 0-364 with 0=January 1st .

For 6G Devices, the days count to a user programmable rollover.

### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_IRIGWr, BTICard\_IRIGConfig, BTICard\_TimerRd