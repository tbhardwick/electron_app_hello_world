# **IRIGTimeNanoBinToBCD**

```
VOID BTICard_IRIGTimeNanoBinToBCD
(
      LPULONG irigvalh, //Pointer to upper 32 bits of BCD IRIG time value
      LPULONG irigvall, //Pointer to lower 32 bits of BCD IRIG time value
      ULONG timevalh, //Upper 32 bits of binary time ns value
      ULONG timevall, //Lower 32 bits of binary time ns value
)
```
## **RETURNS**

None.

#### **DESCRIPTION**

Converts the binary time value (in nanoseconds) to the equivalent 64 bit IRIG BCD time value.

*Note: This is a utility function and does not access any Device hardware.*

### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

### **SEE ALSO**

```
BTICard_IRIGTimeBCDToNanoBin, BTICard_IRIGTimeBCDToBin,
BTICard_IRIGTimeBinToBCD
```