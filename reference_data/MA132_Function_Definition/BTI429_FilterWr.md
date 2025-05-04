# **FilterWr**

```
ERRVAL BTI429_FilterWr
(
     MSGADDR message, //Message address to write to filter
     INT label, //Label value
     INT sdi, //SDI value
     INT channel, //Number of receive channel
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

## **DESCRIPTION**

Writes the message address (*message*) of the Message Record into the Filter Table position specified by *channel*, *label*, and *sdi*. The valid range of *sdi* is 0-3.

This function is most useful to assign multiple labels to point to one Message Record. After calling BTI429\_FilterSet for the first label of interest, this function could be called with the message address that was returned by BTI429\_FilterSet for each of the remaining labels. This would point all the labels of interest to one Message Record.

## **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

## **SEE ALSO**

```
BTI429_FilterRd, BTI429_FilterSet, BTI429_FilterDefault
```