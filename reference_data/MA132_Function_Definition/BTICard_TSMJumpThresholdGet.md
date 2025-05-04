# **TSMJumpThresholdGet**

ERRVAL **BTICard\_TSMJumpThresholdGet**

```
(
  LPULONG drift, //Pointer to variable to hold current offset drift 
      // threshold (ppt)
  LPULONG offset, //Pointer to variable to hold current offset jump 
   // threshold (ns)
  HCORE handleval //Device handle 
)
```
### **RETURNS**

A negative value if an error occurs, otherwise zero.

# **DESCRIPTION**

Reads the current values of the offset drift threshold and offset jump threshold and places the values into the location pointed to by *drift* and *offset* respectively. *drift* is in Parts-per-Trillion and *offset* is in nanoseconds. See BTICard\_TSMJumpThresholdSet for details on how these thresholds work.

# **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

None.

### **SEE ALSO**

BTICard\_TSMJumpThresholdSet