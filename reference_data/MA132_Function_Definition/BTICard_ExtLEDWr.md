# **ExtLEDWr**

```
VOID BTICard_ExtLEDWr
(
      INT ledval, //New state of the LED
      HCORE hCore //Core handle
)
```
# **RETURNS**

None.

# **DESCRIPTION**

Sets the state of the onboard LED. An *ledval* of zero turns the LED off and an *ledval* of one turns the LED on.

## **DEVICE DEPENDENCY**

4G, 5G, 5G+, and 6G Devices have a user-controlled LED for each core. For all other Devices, please refer to the hardware manual.

### **WARNINGS**

None.

### **SEE ALSO**

BTICard\_ExtLEDRd