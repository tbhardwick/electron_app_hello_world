# **IntClear**

```
VOID BTICard_IntClear
```

```
(
      HCORE hCore //Core handle
)
```
## **RETURNS**

None.

## **DESCRIPTION**

Clears the interrupt from the core so it is ready for the next interrupt. Typically, the user's worker thread calls this function. Because the core cannot process another interrupt until the current one is cleared, BTICard\_IntClear should be called after each interrupt has been processed.

## **DEVICE DEPENDENCY**

Applies to all Devices except those controlled via RPC.

## **WARNINGS**

If another interrupt occurs before BTICard\_IntClear is called, the new interrupt is lost.

## **SEE ALSO**

BTICard\_IntInstall, BTICard\_IntUninstall