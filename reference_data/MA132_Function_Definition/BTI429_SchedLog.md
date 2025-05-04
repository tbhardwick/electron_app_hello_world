# **SchedLog**

```
SCHNDX BTI429_SchedLog
(
     ULONG condition, //Value to test
     USHORT tagval, //Event tag value
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

### **DESCRIPTION**

Appends a conditional LOG Command Block to the current end of the schedule. A conditional LOG Command Block causes the Device to generate an Event Log List entry if condition evaluates as TRUE. The event type placed in the Event Log List is EVENTTYPE\_429OPCODE and the userspecified value *tagval* is used as the info value. Entries are read out of the Event Log List using BTICard\_EventLogRd.

The condition flags listed below may be used to specify the Event condition.

| Condition        |                                      |
|------------------|--------------------------------------|
| Constant         | Description                          |
| COND429_ALWAYS   | Always branch                        |
| COND429_DIO1ACT  | Create log entry if DIO1 is active   |
| COND429_DIO1NACT | Create log entry if DIO1 is inactive |
| COND429_DIO2ACT  | Create log entry if DIO2 is active   |
| COND429_DIO2NACT | Create log entry if DIO2 is inactive |
| COND429_DIO3ACT  | Create log entry if DIO3 is active   |
| COND429_DIO3NACT | Create log entry if DIO3 is inactive |
| COND429_DIO4ACT  | Create log entry if DIO4 is active   |
| COND429_DIO4NACT | Create log entry if DIO4 is inactive |
| COND429_DIO5ACT  | Create log entry if DIO5 is active   |
| COND429_DIO5NACT | Create log entry if DIO5 is inactive |
| COND429_DIO6ACT  | Create log entry if DIO6 is active   |
| COND429_DIO6NACT | Create log entry if DIO6 is inactive |
| COND429_DIO7ACT  | Create log entry if DIO7 is active   |
| COND429_DIO7NACT | Create log entry if DIO7 is inactive |
| COND429_DIO8ACT  | Create log entry if DIO8 is active   |
| COND429_DIO8NACT | Create log entry if DIO8 is inactive |

## **DEVICE DEPENDENCY**

5G Devices only support the COND429\_ALWAYS flag. The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

## **SEE ALSO**

BTICard\_EventLogRd