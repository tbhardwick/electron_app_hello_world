# **IRIGTimeBCDToBin**

```
VOID BTICard_IRIGTimeBCDToBin
(
      LPULONG timevalh, //Pointer to upper 32 bits of binary time µs value
```

```
LPULONG timevall, //Pointer to lower 32 bits of binary time µs value
ULONG irigvalh, //Upper 32 bits of BCD IRIG time value
ULONG irigvall, //Lower 32 bits of BCD IRIG time value
```

```
)
```
## **RETURNS**

None.

## **DESCRIPTION**

Converts the 64 bit IRIG BCD time value to the equivalent binary time value (in microseconds).

*Note: This is a utility function and does not access any Device hardware.*

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

### **SEE ALSO**

```
BTICard_IRIGTimeBinToBCD, BTICard_IRIGTimeNanoBinToBCD,
BTICard_IRIGTimeBCDToNanoBin
```