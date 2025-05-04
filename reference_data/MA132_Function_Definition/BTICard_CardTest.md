# **CardTest**

```
ERRVAL BTICard_CardTest
(
```

```
USHORT level, //Level of tests to perform
     HCORE hCore //Core handle
)
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

### **DESCRIPTION**

Executes a hardware test specified by *level* on the core specified by *hCore*. When the test completes, the core is left in the same state as after a BTICard\_CardReset call.

| Level        |                                                                                                                                                                                        |
|--------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Constant     | Description                                                                                                                                                                            |
| TEST_LEVEL_0 | Tests the I/O interface of the core. The test reads and writes each I/O with a walking-bit pattern.                                                                                    |
| TEST_LEVEL_1 | In addition to Level 0, this level tests the memory interface of the core. The test performs a pattern<br>test of the RAM.                                                             |
| TEST_LEVEL_2 | In addition to previous levels, this level tests the communication process of the core. The test<br>performs a pattern test of the RAM using the communication process.                |
| TEST_LEVEL_3 | In addition to previous levels, this level tests the encoders and decoders of the core. Where possible,<br>the core monitors its own transmissions to validate protocol functionality. |

# **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

This function disrupts normal operation of the core. For TEST\_LEVEL\_3, do not use this function when the core is connected to an active databus, as the results will be unpredictable.

## **SEE ALSO**

BTICard\_CardReset