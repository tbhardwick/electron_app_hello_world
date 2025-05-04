# **SchedBranch**

```
SCHNDX BTI429_SchedBranch
(
      ULONG condition, //Condition for branch
      SCHNDX destindex, //Destination index for branch
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a conditional BRANCH Command Block to the current end of the Schedule. A conditional BRANCH Command Block causes the Device to branch to the index in the Schedule specified by *destindex* if *condition* evaluates as TRUE.

The *condition* flags listed below may be used to specify the branch condition.

| condition        |  |                            |
|------------------|--|----------------------------|
| Constant         |  | Description                |
| COND429_ALWAYS   |  | Always branch              |
| COND429_DIO1ACT  |  | Branch if DIO1 is active   |
| COND429_DIO1NACT |  | Branch if DIO1 is inactive |
| COND429_DIO2ACT  |  | Branch if DIO2 is active   |
| COND429_DIO2NACT |  | Branch if DIO2 is inactive |
| COND429_DIO3ACT  |  | Branch if DIO3 is active   |
| COND429_DIO3NACT |  | Branch if DIO3 is inactive |
| COND429_DIO4ACT  |  | Branch if DIO4 is active   |
| COND429_DIO4NACT |  | Branch if DIO4 is inactive |
| COND429_DIO5ACT  |  | Branch if DIO5 is active   |
| COND429_DIO5NACT |  | Branch if DIO5 is inactive |
| COND429_DIO6ACT  |  | Branch if DIO6 is active   |
| COND429_DIO6NACT |  | Branch if DIO6 is inactive |
| COND429_DIO7ACT  |  | Branch if DIO7 is active   |
| COND429_DIO7NACT |  | Branch if DIO7 is inactive |
| COND429_DIO8ACT  |  | Branch if DIO8 is active   |
| COND429_DIO8NACT |  | Branch if DIO8 is inactive |

## **DEVICE DEPENDENCY**

Does not apply to 5G Devices. The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function. When creating subroutines, BTI429\_SchedEntry needs to be called to point to the main section. The Command Block pointed to by *destindex* must have been previously inserted in the Schedule.

## **SEE ALSO**

BTI429\_SchedEntry, BTI429\_SchedCall, BTI429\_SchedReturn