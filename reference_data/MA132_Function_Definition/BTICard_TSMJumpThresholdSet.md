# **TSMJumpThresholdSet**

```
ERRVAL BTICard_TSMJumpThresholdSet
(
      ULONG drift, //Value for offset drift threshold (ppt)
      ULONG offset, //Value for offset jump threshold (ns)
      HCORE handleval //Device handle 
)
```
#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the values *drift and offset* to the Device's IRIG/PPS drift and time thresholding limits.

When using the Device to synchronize to an single external IRIG/PPS source for both time and drift modes, it is possible to 'drift' the timer to match the external time source time by overcorrecting the frequency of the Device's clock to relatively speed or slow the timer's incrementing. This will cause the timer to move slowly towards the target time value rather than immediately 'jumping' to the received time. This may be useful to prevent discontinuities in protocol timestamps.

*The drift* threshold is used to set the maximum drift adjustment a time delta can force onto the clock frequency. Note that this is in addition to any drift difference due to frequency variation between timing samples and the sum is clipped by the value set by *BTICard\_TSMDriftMaxSet*. The value is in parts-per-trillion unsigned representation with a valid range of 0 to 425,000,000.

The *offset* threshold is used to set the minimum offset value that will force the timer to 'jump' to the received time. When the time received via IRIG/PPS is more than the *offset* threshold from the Device's internal time, the Device's time will snap to the received time. Value is in nanoseconds, unsigned representation with a valid range of 0 to 0xFFFFFFFF.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

## **WARNINGS**

None.

#### **SEE ALSO**

```
BTICard_TSMJumpThresholdGet, BTICard_TSMSyncConfig,
BTICard_TSMDriftMaxSet
```