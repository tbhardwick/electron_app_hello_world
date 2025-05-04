# **TimerStatus**

```
INT BTICard_TimerStatus
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The status value of the timer configuration.

#### **DESCRIPTION**

This function determines the status of how the timer for a core is configured. Some devices have configurable modes that affect elements of data structures. For example, 4G Devices allow for BCD or binary time-tag formatting.

The status value can be tested using the predefined constants below:

| Constant           | Description                    |
|--------------------|--------------------------------|
| TIMETAG_FORMAT_BCD | Time-tags are in BCD format    |
| TIMETAG_FORMAT_BIN | Time-tags are in binary format |

### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

3G Devices and 4G Devices (when configured to use binary formatted time-tags) latch the time-tag value when processed by the Device firmware. This will create some minor variability in time-tags from message to message. 6G, 5G+, 5G, and 4G Devices (when configured to use IRIG BCD formatted time-tags) latch the time-tag value when processed by the Device hardware. This results in a very consistent and accurate time-tag value.

Some ARINC 429 messages of 4G Devices (when configured for binary formatted time-tags) may have identical time-tags due to batch processing of messages in the same Device firmware time slot. This is most noticeable in the Sequential Record when comparing time-tags of messages.

# **SEE ALSO**

```
BTICard_IRIGConfig, BTICard_IRIGTimeBCDToBin, BTICard_IRIGTimeBinToBCD, 
BTICard_SeqFind??
```