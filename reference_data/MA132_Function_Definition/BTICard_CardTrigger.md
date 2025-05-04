## **CardTrigger**

```
VOID BTICard_CardTrigger
(
      HCORE hCore //Core handle
)
```
### **RETURNS**

None.

## **DESCRIPTION**

Generates a software-simulated external trigger signal on all available trigger lines. For Devices with multiple trigger lines, BTICard\_CardTriggerEx can be used to specify lines individually.

## **DEVICE DEPENDENCY**

Applies to all Devices.

# **WARNINGS**

None.

## **SEE ALSO**

```
BTICard_CardTriggerEx, BTI1553_BCTriggerDefine,BTI1553_MsgTriggerDefine, 
BTI1553_ErrorTriggerDefine
```