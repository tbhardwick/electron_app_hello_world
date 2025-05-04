# **SchedEntry**

```
SCHNDX BTI429_SchedEntry
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
# **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

# **DESCRIPTION**

Sets the next available location in the Schedule as the beginning of the Schedule. This operation is only necessary if subroutines are used in a Schedule.

To create a Schedule with subroutines, first define the subroutines by calling the desired Schedule functions while saving the returned Schedule indices. Then call BTI429\_SchedEntry to set the starting point of the Schedule. Then build the main part of the Schedule by calling the other Schedule functions that include the commands that call the subroutines (i.e., BTI429\_SchedCall and BTI429\_SchedBranch).

# **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

### **SEE ALSO**

BTI429\_SchedBranch, BTI429\_SchedCall, BTI429\_SchedReturn