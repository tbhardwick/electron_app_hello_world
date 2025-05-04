# **IRIGSyncStatus**

BOOL **BTICard\_IRIGSyncStatus**

```
(
HCORE hCore //Core handle
```
)

## **RETURNS**

TRUE if the IRIG timer is synchronized or FALSE if it is not synchronized.

#### **DESCRIPTION**

Reports the status of the IRIG timer on *hCore* in synchronizing to the signal on the IRIG bus.

## **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTICard\_IRIGConfig, BTICard\_IRIGWr, BTICard\_IRIGRd