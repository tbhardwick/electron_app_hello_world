# **TSMTimerWr**

#### ERRVAL **BTICard\_TSMTimerWr**

```
ULONGLONG value, //Value to set timer (ns)
HCORE handleval //Device handle
```
## **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Commands the Device to set the timer to the input *value*. Valid range is 0 to (2^64)-1.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

Writing a timer value higher than the timer rollover (set by BTICard\_TSMTimerRolloverSet**)** will cause the timer to rollover to zero.

#### **SEE ALSO**

BTICard\_TSMTimerRd, BTICard\_TSMTimerRelWr, BTICard\_TSMTimerRolloverSet