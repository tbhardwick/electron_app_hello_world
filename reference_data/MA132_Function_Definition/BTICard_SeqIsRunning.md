# **SeqIsRunning**

```
BOOL BTICard_SeqIsRunning
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the Sequential Record is still active, otherwise FALSE.

#### **DESCRIPTION**

Determines whether the Sequential Record is active and is typically used when the Sequential Record has been configured with the SEQCFG\_FILLHALT flag. In which case, recording halts when the on-board Sequential Record is full. This prevents unread data from being overwritten when the host gets behind in reading data from the Sequential Record. Thus, in fill and halt mode BTICard\_SeqIsRunning effectively indicates whether the buffer is full or not.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd