# **SeqDMARd**

```
USHORT BTICard_SeqDMARd
(
      LPUSHORT seqbuf, //Pointer to Sequential Record buffer
      ULONG bufcount, //Size of the buffer (in 16-bit words)
      HCORE hCore //Core handle
)
```
## **RETURNS**

The number of 16-bit words copied to the user-supplied buffer.

## **DESCRIPTION**

Copies as many available complete records as possible from the Sequential Record on the core to the buffer *seqbuf* and returns the number of 16-bit words copied. The larger the buffer size (*bufcount*) the greater the number of records that can be copied by a single call to this function. The Sequential Record data is read from Host memory since it was already transferred from the core memory to the Host in a DMA process. This allows an infinite amount of data to be gathered as long as this function (or one of the others in the table below) is called frequently enough to prevent the Sequential Record on the core from overflowing.

There are four functions that read from the Sequential Record. BTICard\_SeqRd reads a single record; BTICard\_SeqBlkRd, BTICard\_SeqCommRd, and BTICard\_SeqDMARd read multiple records. Any one of these functions may be used in most applications. The difference lies in their speed of execution under different conditions and availability by Device. The table below compares the four functions that read from the Sequential Record and gives some rationale for selecting one over another:

| Function          | Function<br>Overhead | Per Record<br>Overhead | Use When…                                                                   |
|-------------------|----------------------|------------------------|-----------------------------------------------------------------------------|
| BTICard_SeqRd     | low                  | n/a                    | Expect one or no records per function call                                  |
| BTICard_SeqBlkRd  | low                  | High                   | Expect a small number of records per function call                          |
| BTICard_SeqCommRd | high                 | Low                    | Expect a large number of records per function call                          |
| BTICard_SeqDMARd  | low                  | Low                    | Need to offload application from reading monitor data<br>(Device-dependent) |

# **DEVICE DEPENDENCY**

Sequential DMA is supported on all 4G, 5G, 5G+, and 6G Devices; however, sequential DMA is not supported on 4G Devices when operating over USB.

## **WARNINGS**

None.

## **SEE ALSO**

```
BTICard_SeqConfig, BTICard_SeqBlkRd, BTICard_SeqRd, BTICard_SeqDMARd, 
BTICard_SeqFindInit, BTICard_SeqFindNext??
```