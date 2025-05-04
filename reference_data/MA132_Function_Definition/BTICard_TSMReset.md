# **TSMReset**

#### ERRVAL **BTICard\_TSMReset** (

HCORE *handleval //Device handle* 

)

**RETURNS** A negative value if an error occurs, otherwise zero.

## **DESCRIPTION**

Commands the Timing Synchronization Manager to reset. Resets Device time and mode settings.

## **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

## **WARNINGS**

TSM functionality is not reset on BTICard\_CardReset.

This function will not reset input threshold DAC values.

#### **SEE ALSO**

BTICard\_CardReset