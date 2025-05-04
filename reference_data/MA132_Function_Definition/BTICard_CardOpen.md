# **CardOpen**

ERRVAL **BTICard\_CardOpen**

```
(
      LPHCARD lpHandle, //Pointer to the card handle
      INT cardnum //Card number of Device
)
```
## **RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Enables access to a Device. If BTICard\_CardOpen finds the Device that has been assigned *cardnum*, it performs a quick hardware self-test of the Device. Since this function opens the Device and provides a card handle parameter required by BTICard\_CoreOpen (which returns the core handle used by all other functions), this function is always the first BTIDriver function called by a program.

Card numbers are assigned to Devices by the operating system or the user. If only one Device has been installed, the system defaults the card number to zero. How the system assigns card numbers for multiple Devices and how the number can be changed by the user is OS-dependent. See the README.TXT file for your operating system on the distribution disk for more information. A test program for determining the card number(s) is provided on the distribution disk. The card numbers assigned to BTIDriver Devices are specific to BTIDriver-compliant Devices, so there is no conflict when non-BTIDriver-compliant Devices use those same card numbers.

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

BTICard\_CardClose must be called to release the hardware resources before the program terminates.

### **SEE ALSO**

BTICard\_CardClose