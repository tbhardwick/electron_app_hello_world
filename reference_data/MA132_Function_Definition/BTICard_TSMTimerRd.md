# **TSMTimerRd**

#### ERRVAL **BTICard\_TSMDTimerRd**

```
LPULONGLONG valueptr, //Pointer to variable to hold current time 
          //(ns)
    HCORE handleval //Device handle 
)
```
### **RETURNS**

(

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the current value of the Device timer and places the value into the location pointed to by value*ptr*. Value is in nanoseconds.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMTimerWr