# **SchedData**

```
SCHNDX BTI429_SchedData
(
     ULONG dataval, //Data word to schedule
     INT bitcount, //Bitcount to schedule
     USHORT gapval, //Following gap to schedule
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a DATA Command Block to the current end of the Schedule. A DATA Command Block directly stores the data to transmit instead of the address of a message. DATA Command Blocks are useful for simple data transmissions by eliminating the overhead of a Message Record. When a DATA Command Block is encountered in the Schedule, the *channel* will transmit *bitcount*  (number of bits) of *dataval* followed by the a following *gapval* length gap. Note that *gapval*  units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

## **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

## **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

DATA Command Blocks do not access/update Message Records. As such, activity and time-tag information must be accessed from Sequential Monitor records rather than a Message Record.

## **SEE ALSO**

BTI429\_SchedEntry, BTICard\_SeqRd, BTI429\_ChConfig