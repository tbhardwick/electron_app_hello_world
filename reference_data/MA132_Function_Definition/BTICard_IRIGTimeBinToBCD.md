# **IRIGTimeBinToBCD**

```
VOID BTICard_IRIGTimeBinToBCD
```

```
(
      LPULONG irigvalh, //Pointer to upper 32 bits of BCD IRIG time value
      LPULONG irigvall, //Pointer to lower 32 bits of BCD IRIG time value
      ULONG timevalh, //Upper 32 bits of binary time µs value
      ULONG timevall, //Lower 32 bits of binary time µs value
)
```
## **RETURNS**

None.

### **DESCRIPTION**

Converts the binary time value (in microseconds) to the equivalent 64 bit IRIG BCD time value.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

```
BTICard_IRIGTimeBCDToBin, BTICard_IRIGTimeBCDToNanoBin,
BTICard_IRIGTimeNanoBinToBCD
```