# **TSMInputDelayCompGet**

```
ERRVAL BTICard_TSMInputDelayCompGet
(
```

```
LPINT delayptr, //Pointer to variable to hold current input 
                  // compensation (ns)
HCORE handleval //Device handle
```
)

## **RETURNS**

A negative value if an error occurs, otherwise zero.

## **DESCRIPTION**

Reads the current value of the input compensation and places the value into the location pointed to by *delayptr*. Value is in nanoseconds.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

## **WARNINGS**

None.

## **SEE ALSO**

BTICard\_TSMInputDelayCompSet