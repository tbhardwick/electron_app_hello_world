# **SeqBlkRd**

```
ULONG BTICard_SeqBlkRd
(
     LPUSHORT seqbuf, //Pointer to Sequential Record buffer
     ULONG bufcount, //Size of the buffer (in 16-bit words)
     HCORE hCore //Core handle
)
```

```
LPULONG blockcount, //Number of records copied to the buffer
```
#### **RETURNS**

The number of 16-bit words copied to the user-supplied buffer.

### **DESCRIPTION**

Copies as many available complete records as possible from the Sequential Record on the core to a buffer (*seqbuf*). The function returns the number of 16-bit words copied and the puts the number of records copied into *blockcount*. The larger the buffer size (*bufcount*) the greater the number of records that can be copied by a single call to this function. The data read is effectively removed from the Sequential Record on the core. This allows an infinite amount of data to be gathered as long as this function (or one of the others in the table below) is called frequently enough to prevent the Sequential Record on the core from overflowing.

There are four functions that read from the Sequential Record. BTICard\_SeqRd reads a single record; BTICard\_SeqBlkRd, BTICard\_SeqCommRd, and BTICard\_SeqDMARd read multiple records. Any one of these functions may be used in most applications. The difference lies in their speed of execution under different conditions and availability by Device. The table below compares the four functions that read from the Sequential Record and gives some rationale for selecting one over another:

| Function          | Function<br>Overhead | Per Record<br>Overhead | Use When…                                                                |
|-------------------|----------------------|------------------------|--------------------------------------------------------------------------|
| BTICard_SeqRd     | low                  | n/a                    | Expect one or no records per function call                               |
| BTICard_SeqBlkRd  | low                  | high                   | Expect a small number of records per function call                       |
| BTICard_SeqCommRd | high                 | low                    | Expect a large number of records per function call                       |
| BTICard_SeqDMARd  | low                  | low                    | Expect a large number of records per function call<br>(Device-dependent) |

#### **DEVICE DEPENDENCY**

On 3G Devices, BTICard\_SeqRd, \_SeqBlkRd, and \_SeqCommRd all read multiple records in the same manner.

## **WARNINGS**

None.

## **SEE ALSO**

BTICard\_SeqConfig,BTICard\_SeqCommRd, BTICard\_SeqDMARd,BTICard\_SeqBlkRd, BTICard\_SeqFindInit, BTICard\_SeqFindNext??