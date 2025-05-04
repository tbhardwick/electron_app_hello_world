# **SeqLogFrequency**

```
USHORT BTICard_SeqLogFrequency
(
```

```
USHORT logfreq, //Frequency of Event Log List entries
HCORE hCore //Core handle
```

```
)
```
## **RETURNS**

The previous value of the frequency of Event Log List entries.

## **DESCRIPTION**

Sets the Event Log List frequency for the Sequential Monitor. It is used when the Sequential Record has been configured with the SEQCFG\_LOGFREQ flag in BTICard\_SeqConfig. The Sequential Record generates an Event Log List entry after it records *logfreq* amount of records. The user specifies the value of *logfreq*. For example, a value of 1 enables an Event Log List entry after every record, a value of 2 after every second record, and so on. It continues in this manner until the Sequential Record is stopped.

# **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

## **SEE ALSO**

```
BTICard_SeqConfig, BTICard_SeqRd, BTICard_EventLogConfig, 
BTICard_EventLogRd
```