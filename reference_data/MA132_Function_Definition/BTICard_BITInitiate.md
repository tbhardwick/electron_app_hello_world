# **BITInitiate**

```
ERRVAL BTICard_BITInitiate
```

```
(
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

Executes a read and write memory test on the card specified by hCard. When the test completes, the Card is left in the same state as after a call to BTICard\_CardReset.

## **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

## **WARNINGS**

Do not call when the Card is connected to an active databus. The function disrupts normal databus operation of the Card and the results will be unpredictable.

## **SEE ALSO**

BTICard\_CardReset