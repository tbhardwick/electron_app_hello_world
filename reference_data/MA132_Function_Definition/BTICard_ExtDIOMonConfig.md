# **ExtDIOMonConfig**

```
ERRVAL BTICard_ExtDIOMonConfig
(
      USHORT risemask, //Discrete bitmask to monitor rising edges
      USHORT fallmask, //Discrete bitmask to monitor falling edges
      INT banknum, //Bank number of DIO
      HCORE hCore //Core handle
)
```
## **RETURNS**

A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

Enables Sequential Monitoring on specific transitions of discrete inputs. Discrete inputs are sampled at a minimum rate of 100 kHz and compared to the previously sampled values. If the digital values of any discretes specified in the risemask have transitioned from a zero to a one, or the digital values of any discretes specified in the fallmask have transitioned from a one to a zero, then a Sequential Record will be created. To disable previously-enabled monitor settings, call BTICard\_ExtDIOMonConfig again with *risemask* and *fallmask* both set to zero.

The *risemask* and *fallmask* are a bit mask specifying up to 16 discrete input signals. For banknum 0, the LSB of each value corresponds to dionum 1 and the MSB corresponds to *dionum* 16. See table below for dionum higher than 16.

| dionum to banknum and bitmask Translation |         |             |             |
|-------------------------------------------|---------|-------------|-------------|
| dionum Range                              | banknum | bitmask LSB | bitmask MSB |
| 1-16                                      | 0       | 1           | 16          |
| 17-32                                     | 1       | 17          | 32          |
| 33-48                                     | 2       | 33          | 48          |
| 49-64                                     | 3       | 49          | 64          |

## **DEVICE DEPENDENCY**

Applies to 5G, 5G+ and 6G Devices. The mapping of dionum to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

# **WARNINGS**

By default, avionics discretes are active low. A grounded avionics discrete will have a digital value of one. If the polarity for that discrete input has been changed, the digital value compared for rising and falling edges will be reversed.

## **SEE ALSO**

BTICard\_SeqFindNextDIO