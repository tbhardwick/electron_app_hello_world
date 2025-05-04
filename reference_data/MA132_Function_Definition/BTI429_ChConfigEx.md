# **ChConfigEx**

```
ERRVAL BTI429_ChConfigEx
(
     ULONG ctrlflags, //Selects channel options
     USHORT count, //Number of entries in the schedule
     INT channel, //Number of channel
     HCORE hCore //Core handle
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

# **DESCRIPTION**

Configures the channel similar to BTI429\_ChConfig, but with the addition of the parameter *count* which is used to specify either the number of schedule entries to allocate for the schedule (CHCFG429\_SCHEDULE) or the number of Command Block entries for the Playback FIFO (CHCFG429\_PLAYBACK). BTI429\_ChConfig defaults to 512 entries, and *count* can be used to allocate a different number.

# **DEVICE DEPENDENCY**

3G and 4G Devices support up to 4089 entries, while 5G, 5G+, and 6G Devices support up to 8187 entries.

Only 5G+ and 6G Devices support Playback.

# **WARNINGS**

The function clears any previous configuration of the channel.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop