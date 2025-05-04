# **ExtStatusLEDWr**

#### VOID **BTICard\_ExtStatusLEDWr**

```
(
```
)

```
HCORE hCore //Core handle
```
INT *ledval*, *//New state of the LED* INT *ledcolor*, *//New color of the LED*

## **RETURNS**

None.

## **DESCRIPTION**

Sets the state of the onboard Status LED. An *ledval* of zero turns the LED off and an *ledval* of one turns the LED on.

## **DEVICE DEPENDENCY**

4G Devices have a red status LED.

BUSBox BB1xxx Devices have a multi-color Status LED. The color state of that LED can be controlled through *ledcolor.* A zero value indicates a red color, and a one value indicates a green color.

RPC Devices have a red Status LED that indicates a succesful booting of the Device. Afterwards, the Status LED can be controlled with this function.

UA1xxx and UA2xxx Devices have a red Status LED that is both user-controllable and can indicate that a MIL-STD-1553 or EBR-1553 protocol protocol error has occurred. The on/off state of the Status LED is controlled through *ledval* when the *ledcolor* value is set to zero. The enabling/disabling of the 1553 protocol error indication is controlled through *ledval* when the *ledcolor* value is set to one. When this feature is enabled, the Status LED will be automaticaly set by Device hardware when a MIL-STD 1553 or EBR-1553 protocol error occurs. The Status LED can then be cleared by calling this function.

For all other Devices, please refer to the Device specific hardware manual.

### **WARNINGS**

None.

## **SEE ALSO**

BTICard\_ExtStatusLEDRd, BTICard\_ExtLEDWr