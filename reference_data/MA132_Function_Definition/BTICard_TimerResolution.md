# **TimerResolution**

```
INT BTICard_TimerResolution
(
     INT timerresol, //Selects the timer resolution
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The value of the previous resolution. Refer to *timerresol* for return values.

#### **DESCRIPTION**

Selects the resolution for the time-tag timer on the specified Device. Timer resolution can be read (without modifying) by setting *timerresol* to *TIMERRESOL\_CURRENT*. *timerresol* must be one of the following predefined constants:

| timerresol         |            |                              |
|--------------------|------------|------------------------------|
| Constant           | Resolution | 3G/4G Range (hr:min:sec)     |
| TIMERRESOL_CURRENT | current    | -                            |
| TIMERRESOL_1US     | 1 µs       | 1:11:34                      |
| TIMERRESOL_16US    | 16 µs      | 19:05:19                     |
| TIMERRESOL_1024US  | 1024 µs    | 50 days                      |
| TIMERRESOL_1NS     | 1 ns       | 6G Only (Refer to Table 6.1) |

## **DEVICE DEPENDENCY**

Only 3G and 4G Devices have resolutions that can be modified. To use the IRIG timer for a specified 4G core instead of the default binary timer, see BTICard\_IRIGConfig. The 5G and 5G+ binary timer resolution is always 1 s (*TIMERRESOL\_1US*), and has a range of 365 days. The 6G binary timer resolution is always 1 ns and defaults to a range of 365 days. The 6G binary timer is capable of larger ranges, see BTICard\_TSMTimerRolloverSet.

## **WARNINGS**

After changing the resolution on a 3G/4G Device, a call to BTICard\_TimerClear should be made to clear the timer.

## **SEE ALSO**

```
BTICard_TimerClear, BTICard_IRIGConfig, BTICard_IRIGRd, BTICard_IRIGWr, 
BTICard_TSMTimerRolloverSet
```