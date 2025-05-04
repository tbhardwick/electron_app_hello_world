# **CardTriggerEx**

```
VOID BTICard_CardTriggerEx
(
      USHORT trigmask, //Line(s) to trigger via software
      HCORE hCore //Core handle
)
```
## **RETURNS**

None.

### **DESCRIPTION**

Simulates an external trigger signal on the trigger input line(s) specified by *trigmask*. The constants in the table below may be bitwise OR-ed together to trigger multiple lines.

| trigmask       |                        |
|----------------|------------------------|
| Constant       | Description            |
| TRIGMASK_TRIGA | Selects trigger line A |
| TRIGMASK_TRIGB | Selects trigger line B |
| TRIGMASK_TRIGC | Selects trigger line C |

#### **DEVICE DEPENDENCY**

Though this function is intended for 4G, 5G, 5G+, and 6G Devices, which have multiple trigger lines, using a trigmask value of TRIGMASK\_TRIGA on 3G Devices produces the same result as BTICard\_CardTrigger.

#### **WARNINGS**

None.

#### **SEE ALSO**

```
BTICard_CardTrigger,BTI1553_BCTriggerDefine,BTI1553_MsgTriggerDefine,
BTI1553_ErrorTriggerDefine,BTICard_CardTriggerValid
```