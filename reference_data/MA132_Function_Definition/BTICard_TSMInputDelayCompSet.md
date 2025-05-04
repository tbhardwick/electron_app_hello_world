# **TSMInputDelayCompSet**

#### ERRVAL **BTICard\_TSMInputDelayCompSet** (

```
INT delay, //Value for input compensation (ns)
     HCORE handleval //Device handle 
)
```
### **RETURNS**

A negative value if an error occurs, otherwise zero.

### **DESCRIPTION**

Writes the value *delay* to the Device's input compensation. The Device will adjust the internal time by the set amount to account for flight and logic delays in the circuitry and wiring when synchronizing time to an external IRIG or PPS source. Value is in nanoseconds and valid range is 0 to 1000000 (1 ms).

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMInputDelayCompGet, BTICard\_TSMSyncConfig