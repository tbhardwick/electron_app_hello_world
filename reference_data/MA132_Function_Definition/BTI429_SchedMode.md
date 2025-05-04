# **SchedMode**

```
ERRVAL BTI429_SchedMode
(
      ULONG modeval //Schedule Build mode options
)
```
## **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

Sets scheduling options for the BTI429\_SchedBuild function. The parameter *modeval* controls various options such as the scheduling method, time base and execution environment of the BTI429\_SchedBuild function. The following is a list of the different values that can be OR'ed together for the *modeval* options. Only one METHOD may be specified per call.

| Modeval                  |                                                                                                                                                                                                                       |  |
|--------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--|
| Constant                 | Description                                                                                                                                                                                                           |  |
| SCHEDMODE_DEFAULT        | Selects all default settings (bold below)                                                                                                                                                                             |  |
| SCHEDMODE_METHOD_NORMAL  | Selects the normal scheduling method                                                                                                                                                                                  |  |
| SCHEDMODE_METHOD_QUICK   | Selects the quick scheduling method                                                                                                                                                                                   |  |
| SCHEDMODE_METHOD_BOTH    | Uses the quick method first then uses the normal method if the quick method fails to                                                                                                                                  |  |
|                          | build a schedule.                                                                                                                                                                                                     |  |
| SCHEDMODE_MILLISEC       | Sets the min and max periods to be specified in milliseconds                                                                                                                                                          |  |
| SCHEDMODE_MICROSEC       | Sets the min and max periods to be specified in microseconds                                                                                                                                                          |  |
| SCHEDMODE_REMOTE         | For RPC Devices, specifies to perform calculations remotely                                                                                                                                                           |  |
| SCHEDMODE_LOCAL          | For RPC Devices, specifies to perform calculations locally                                                                                                                                                            |  |
| SCHEDMODE_SKIPRANGECHECK | Skips range checking of a message when the min period equals the max period.<br>Attempts to meet the rate, but schedule will succeed even if the messages with same<br>min/max cannot transmit at a specified period. |  |
| SCHEDMODE_RANGECHECK     | Performs range checking on all messages. SchedBuild fails if any messages are out<br>of range.                                                                                                                        |  |

The normal scheduling method uses our traditional algorithm for scheduling messages. It is the default method to preserve schedule timing in established applications. The quick scheduling method significantly reduces the schedule build times and improves the ability to schedule highduty cycle schedules. It is recommended to use the SCHEDMODE\_METHOD\_BOTH which first uses the quick scheduling method, and then uses the normal scheduling method if necessary.

For RPC Devices, SCHEDMODE\_REMOTE and SCHEDMODE\_LOCAL control where the scheduling calculations are executed. SCHEDMODE\_REMOTE can be faster for many schedules or when network latency is high. SCHEDMODE\_LOCAL can offer performance improvements for more complex schedules and if network latency is low.

The purpose of SCHEDMODE\_SKIPRANGECHECK is to simplify scheduling by using a "best effort" mode. For messages with identical min/max transmit interval values, SchedBuild successfully generates automatic schedules even if there are absolute timing violations for the messages with the identical min/max times. Refer to the following example:

| MESSAGE | MIN   | MAX    |
|---------|-------|--------|
| Label 1 | 90 ms | 110 ms |
| Label 2 | 37 ms | 37 ms  |
| ···     | ···   | ···    |
| Label n | ···   | ···    |

#### Example Results

The bus loading will change depending on the number of labels. Label 1 and Label 2 are not multiples of each other; although not required, this can cause SchedBuild to fail when heavily loaded. As an example using Label 2, three times the 37ms interval is 111ms. When SCHEDMODE\_SKIPFRAMECHECK is set, the interval for Label 2 is allowed to vary slightly, and the end result may be slightly modified for a successful build using 37ms, 37ms, and 36ms. When SCHEDMODE\_RANGECHECK is set, the function will fail because the interval is out of range.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

Calling this function sets the operational mode for BTI429\_SchedBuild for ALL Devices in the same process space (i.e. same application). To use different schedule modes on different channels and/or different hardware, the BTI429\_SchedMode function must be called prior to each subsequent call to BTI429\_SchedBuild.

## **SEE ALSO**

BTI429\_SchedBuild