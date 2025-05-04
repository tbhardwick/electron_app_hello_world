# **ExtStatusLEDRd**

#### VOID **BTICard\_ExtStatusLEDRd** (

```
LPINT ledval, //Pointer to variable to receive LED state
     LPINT ledcolor, //Pointer to variable to receive LED color
     HCORE hCore //Core handle
)
```
# **RETURNS**

None.

### **DESCRIPTION**

Reads the state of the onboard Status LED. A zero value will be passed to *ledval* if the LED is off, and a one value if the LED is on.

### **DEVICE DEPENDENCY**

4G Devices have a red status LED.

BUSBox BB1xxx Devices have a multi-color Status LED. The color state of that LED can be read through *ledcolor.* A zero value indicates a red color, and a one value indicates a green color.

For all other Devices, please refer to the Device specific hardware manual.

# **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ExtStatusLEDWr, BTICard\_ExtLEDRd