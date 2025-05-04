# **IntInstall**

```
ERRVAL BTICard_IntInstall
```

```
(
LPVOID hEvent, //Handle of a WIN32 event object
HCORE hCore //Core handle
```

```
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

### **DESCRIPTION**

BTICard\_IntInstall associates a WIN32 event object with interrupts from the core specified by *hCore*. If the function is successful, any interrupt issued from *hCore* causes the event object specified by *hEvent* to be set to the signaled state.

The user's application must ensure that the event object is set to the unsignaled state before the core issues the first interrupt. This can be done when creating the event object with the WIN32 API function CreateEvent.

Create a worker thread, which immediately goes to sleep by calling a WIN32 API wait function like WaitForSingleObject. When the Device issues an interrupt, the event object is signaled, and the worker thread wakes up to respond to the interrupt. The interrupt is generated whenever an entry is written to the Event Log List.

It is the user's responsibility to clear the interrupt from the core by calling BTICard\_IntClear in the worker thread. Note that event objects are never polled.

*Note: BTICard\_IntInstall should be called separately for each core on the Device, and there should be separate interrupt service threads for each core.*

# **DEVICE DEPENDENCY**

Applies to all Devices except those controlled via RPC.

### **WARNINGS**

If this function is used, BTICard\_IntUninstall MUST be called before the user's program terminates. It removes the association between the Device and the event object.

# **SEE ALSO**

BTICard\_EventLogRd, BTICard\_IntUninstall