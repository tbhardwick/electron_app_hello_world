# **TSMTimerRolloverSet**

```
ERRVAL BTICard_TSMTimerRolloverSet
```

```
ULONGLONG value, //Value for timer rollover (ns)
HCORE handleval //Device handle
```
### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

### **DESCRIPTION**

Writes *value* to the Device's timer rollover. This will signal the Device to increment the Years field and reset the timer to zero. Default rollover value is exactly 365 days, but can be adjusted to account for leap years and leap seconds. value is in nanoseconds and valid range is 1000 to 2^64 - 1.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

### **WARNINGS**

None.

### **SEE ALSO**

BTICard\_TSMTimerRolloverGet