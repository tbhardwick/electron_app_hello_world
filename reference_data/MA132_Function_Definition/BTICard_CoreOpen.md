# **CoreOpen**

```
ERRVAL BTICard_CoreOpen
(
```

```
INT corenum, //Core number
HCARD hCard //Card handle
```
LPHCORE *lphCore*, *//Pointer to a core handle*

#### **RETURNS**

)

A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

Enables access to a core (the presence of multiple cores is Device-dependent). BTICard\_CardOpen must first be called to obtain the card handle (*hCard*). BTICard\_CoreOpen finds the core on the Device specified by *hCard that* has been assigned *corenum*, and returns a handle to that core. BTICard\_CoreOpen must be called for each core that you wish to access in your program. BTICard\_CardClose will close all cores opened with BTICard\_CoreOpen.

If you pass the card handle to a function (such as a channel function) instead of a core handle, it will only access the first (or only) core.

## **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

BTICard\_CardOpen must be called before this function. BTICard\_CoreOpen must be called for each core that you wish to access in your program.

## **SEE ALSO**

BTICard\_CardOpen, BTICard\_CardClose