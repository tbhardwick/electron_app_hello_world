# Ballard UA2430 Interface Guide

## Introduction

This document provides guidance on how to interface with the Ballard UA2430 card based on integration patterns found in the Main Cargo Door Diagnostic Tool application. The UA2430 is an ARINC 429/discrete I/O interface card manufactured by Astronics Ballard Technology.

## Hardware Overview

The Ballard UA2430 is a multi-protocol avionics interface card that supports:
- ARINC 429 communication 
- Discrete I/O signals
- Multiple cores for different protocol handling

## Software Integration

### Required DLLs

The application uses the following Ballard DLLs to interface with the UA2430:

- 32-bit systems:
  - `BTICARD.dll` - Core card management functionality
  - `BTI429.dll` - ARINC 429 protocol support
  - `BTIDIO.dll` - Discrete I/O support

- 64-bit systems:
  - `BTICARD64.dll` - Core card management functionality
  - `BTI42964.dll` - ARINC 429 protocol support
  - `BTIDIO64.dll` - Discrete I/O support

### Basic Initialization Flow

To initialize and use the UA2430 card:

1. Open the card
2. Open the required cores (ARINC 429, DIO)
3. Configure the cores and channels
4. Start the card operation
5. Read/write data with the card
6. Close the card when finished

### Configuration Parameters

The application uses the following configuration values:

```csharp
// Card device settings
public const int CARDNUM = 0;        // 1: PCIe, 0: USB
public const int CORENUM_ARINC = 0;  // Core for ARINC 429
public const int CORENUM_DIO = 1;    // 1: PCIe, 0: USB
public const int RCVCHAN_FWD_ARINC = 0;  // ARINC 429 channel for forward data
public const int RCVCHAN_AFT_ARINC = 1;  // ARINC 429 channel for aft data
```

## Code Examples

### Opening and Initializing the Card

```csharp
// Card handle
private nint hCard = nint.Zero;
private nint hCore_ARINC = nint.Zero;
private nint hCore_DIO = nint.Zero;

// Open the card
int errval = BTICARD.BTICard_CardOpen(ref hCard, cardnum);
if (errval != 0)
{
    // Handle error
}

// Open ARINC 429 core
errval = BTICARD.BTICard_CoreOpen(ref hCore_ARINC, corenum_ARINC, hCard);
if (errval != 0)
{
    // Handle error
}

// Open Discrete I/O core
errval = BTICARD.BTICard_CoreOpen(ref hCore_DIO, corenum_DIO, hCard);
if (errval != 0)
{
    // Handle error
}
```

### Configuring ARINC 429 Channel

```csharp
// Configure ARINC 429 channel
int errval = BTI429.BTI429_ChConfig(BTI429.CHCFG429_AUTOSPEED | BTI429.CHCFG429_HIT, rcvchan, hCore_ARINC);
if (errval != 0)
{
    // Handle error
}

// Define filter for receiving all messages
msgdefault.addr = BTI429.BTI429_FilterDefault(BTI429.MSGCRT429_DEFAULT, rcvchan, hCore_ARINC);
```

### Configuring Discrete I/O Banks

```csharp
// Configure a DIO bank
int errval = BTIDIO.BTIDIO_BankConfig(bank.ConfigVal, bank.Threshold, bank.Samplerate, banknum, hCore_DIO);
if (errval != 0)
{
    // Handle error
}
```

### Starting Card Operation

```csharp
// Start operation of the card
BTICARD.BTICard_CardStart(hCore_DIO);
BTICARD.BTICard_CardStart(hCore_ARINC);
```

### Reading ARINC 429 Data

```csharp
// Read ARINC word
msgdefault.data = BTI429.BTI429_MsgDataRd(msgdefault.addr, hCore_ARINC);

// Extract label from word
int label = BTI429.BTI429_FldGetLabel(msgdefault.data);

// Extract data bits
int someValue = BTI429.BTI429_BCDGetData(msgdefault.data, startBit, endBit);
```

### Reading Discrete I/O Data

```csharp
// Check for bank fault
byte fault = BTIDIO.BTIDIO_BankFaultRd(banknum, hCore_DIO);
if (fault != 0)
{
    // Handle fault
}

// Read the bank
byte inputBank = BTIDIO.BTIDIO_BankRd(banknum, hCore_DIO);

// Check if specific bit is set
bool isBitSet = (inputBank & (1 << bitPosition)) != 0;
```

### Writing Discrete I/O Data

```csharp
// Write to a bank
int errval = BTIDIO.BTIDIO_BankWr(byteValue, banknum, hCore_DIO);
if (errval != 0)
{
    // Handle error
}

// Write with a mask (only modify certain bits)
int errval = BTIDIO.BTIDIO_BankWrMask(byteValue, byteMask, banknum, hCore_DIO);
if (errval != 0)
{
    // Handle error
}
```

### Cleaning Up Resources

```csharp
// Stop the card
BTICARD.BTICard_CardStop(hCore_DIO);
BTICARD.BTICard_CardStop(hCore_ARINC);

// Close the card
BTICARD.BTICard_CardClose(hCard);
```

## Recommended Implementation Pattern

For robust integration, implement:

1. A dedicated device management class that encapsulates all interaction with the card
2. Background workers to continuously poll for data
3. Proper error handling for all device operations
4. Resource cleanup in Dispose method to prevent resource leaks

## Common Constants

### ARINC 429 Channel Configuration

```csharp
public const uint CHCFG429_DEFAULT = 0x00000000;    // Default settings
public const uint CHCFG429_AUTOSPEED = 0x00000001;  // Auto speed detection
public const uint CHCFG429_HIT = 0x00000002;        // Hit count mode
```

### DIO Bank Configuration

```csharp
public const uint BANKCFGDIO_DEFAULT = 0x00000000;  // Default settings
public const uint BANKCFGDIO_INPUT = 0x00000000;    // Input mode
public const uint BANKCFGDIO_OUTPUT = 0x00000001;   // Output mode
public const uint BANKCFGDIO_INOUT = 0x00000002;    // Input and output mode
```

### DIO Threshold Constants

```csharp
public const ushort THRESHOLDDIO_TTL = 0x00F0;      // 1.4V threshold (TTL)
public const ushort THRESHOLDDIO_CMOS = 0x01B0;     // 2.5V threshold (CMOS)
public const ushort THRESHOLDDIO_5V = 0x0370;       // 5V threshold
```

## Troubleshooting

Common issues:

1. Card not found (-13=ERR_NOCARD) - Verify card installation and address
2. Core not found (-4=ERR_NOCORE) - Verify core number is correct
3. Invalid channel (-23=ERR_NOTCHAN) - Verify channel number is correct
4. Bank fault errors - Check hardware connection and fault register

## References

- Astronics Ballard Technology: www.astronics.com
- Support contact: Ballard.Support@astronics.com 