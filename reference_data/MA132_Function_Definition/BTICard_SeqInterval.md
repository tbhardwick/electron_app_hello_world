# **SeqInterval**

```
INT BTICard_SeqInterval
(
     INT interval, //Interval time (in seconds)
     INT mode, //Mode to determine interval value
     HCORE hCore //Core handle
)
```
## **RETURNS**

The actual interval value that the core will use.

# **DESCRIPTION**

Sets the interval time for the Sequential Monitor, and is used when the Sequential Record has been configured with the SEQCFG\_INTERVAL flag. In Interval mode, the Sequential Monitor records only the first occurrence of selected messages within the specified interval. The availability of the Interval mode is both Device- and protocol-dependent.

The core cannot accommodate all interval values that could be passed through *interval*. The specified mode helps determine the actual interval that will be used. The constants below should be used to set the mode:

| mode                 |                                                    |
|----------------------|----------------------------------------------------|
| Constant             | Description                                        |
| INTERVALMODE_CLOSEST | Uses the value closest to the specified interval   |
| INTERVALMODE_LESS    | Uses the value just less than specified interval   |
| INTERVALMODE_GREATER | Uses the value just greater the specified interval |

### **DEVICE DEPENDENCY**

Applies only to 3G and 4G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd