# **TSMTimerRelWr**

```
ERRVAL BTICard_TSMTimerRelWr
```

```
LONGLONG value, //Value to jump timer (ns)
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Commands the Device to 'jump' the timer by the number of nanoseconds in *value*. A negative *value* will 'jump' the timer backwards in time.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

## **WARNINGS**

'Jumping' the timer backwards can cause out of order protocol timestamps.

Using a *value* that is large enough to push the timer value over the rollover value will cause the timer to immediately rollover and start incrementing from zero. Writing a negative number with magnitude larger than the current timer value will cause the timer to wrap around and rollover (depending on current rollover value).

## **SEE ALSO**

BTICard\_TSMTimerWr