# **ExtLEDRd**

#### INT **BTICard\_ExtLEDRd** (

```
HCORE hCore //Core handle
)
```
# **RETURNS**

Returns a zero if the LED is off or a one if the LED is on.

# **DESCRIPTION**

Reads the state of the on-board LED.

# **DEVICE DEPENDENCY**

4G, 5G, 5G+, and 6G Devices have a user-controlled LED for each core. For all other Devices, please refer to the hardware manual.

### **WARNINGS**

None.

# **SEE ALSO**

BTICard\_ExtLEDWr