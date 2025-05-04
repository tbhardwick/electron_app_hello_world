#ifndef BTI_CONSTANTS_H
#define BTI_CONSTANTS_H

// ***************************************************************************
// ** IMPORTANT: VERIFY ALL VALUES AGAINST ACTUAL BTI HEADER FILES        **
// ** (`BTICARD.H`, `BTI429.H`, etc.)                                     **
// ***************************************************************************

// --- General Error Codes (from BTICARD.H / C# wrapper) --- 
// Values based on common patterns, VERIFY!
#define ERR_NONE 0         // No error
#define ERR_TIMEOUT -90    // Function timed out waiting for data (Value guessed from AsyncWorker)
#define ERR_UNDERFLOW -108 // Read failed because the buffer is empty (Value guessed from AsyncWorker)
#define ERR_FAIL -1        // Generic failure code
// Add other common ERR_ codes as needed...

// --- List Status Codes (from C# wrapper STAT_*) --- 
// Values based on C# wrapper, likely correct but VERIFY!
#define STAT_EMPTY 0       // Buffer is empty
#define STAT_PARTIAL 1     // Buffer is partially filled
#define STAT_FULL 2        // Buffer is full
#define STAT_OFF 3         // Buffer is off (not typically used for read/write checks)

// --- ARINC 429 Channel Config Flags (CHCFG429_* from BTI429.H / C# wrapper) ---
// Add common flags needed for chConfig. Values need VERIFICATION.
#define CHCFG429_DEFAULT 0x00000000
#define CHCFG429_HIGHSPEED 0x00000001
#define CHCFG429_LOWSPEED 0x00000000
#define CHCFG429_PARODD 0x00000000
#define CHCFG429_PAREVEN 0x00000010
#define CHCFG429_PARDATA 0x00000020
// ... add others as required (e.g., CHCFG429_AUTOSPEED, CHCFG429_SELFTEST) ...

// --- ARINC 429 List Creation Config Flags (LISTCRT429_* from BTI429.H / C# wrapper) ---
// Add common flags needed for list create functions. Values need VERIFICATION.
#define LISTCRT429_DEFAULT 0x00000000
#define LISTCRT429_FIFO 0x00000000
#define LISTCRT429_CIRCULAR 0x00000002
#define LISTCRT429_PINGPONG 0x00000001
#define LISTCRT429_RCV 0x00000010
#define LISTCRT429_XMT 0x00000020
#define LISTCRT429_LOG 0x00000100
// ... add others as required ...

// --- ARINC 429 Filter/Message Config Flags (MSGCRT429_* from BTI429.H / C# wrapper) ---
// Add common flags needed for filter functions. Values need VERIFICATION.
#define MSGCRT429_DEFAULT 0x00000000
#define MSGCRT429_SEQ 0x00000001     // Record in sequential record
#define MSGCRT429_LOG 0x00000002     // Generate event log
#define MSGCRT429_TIMETAG 0x00000010 // Record time-tag
// ... add others as required ...

// --- ARINC 429 SDI Mask Flags (from C# wrapper SDI*) ---
// Values based on C# wrapper, VERIFY!
#define SDI00 0x0001
#define SDI01 0x0002
#define SDI10 0x0004
#define SDI11 0x0008
#define SDIALL 0x000F

// --- Card Info Types (INFOTYPE_* from BTICARD.H / C# wrapper) ---
// Add common types needed for cardGetInfo. Values need VERIFICATION.
#define INFOTYPE_429COUNT 0x0006
#define INFOTYPE_SERIALNUM 0x0019
// ... add others as required ...

// --- Card Test Levels (TEST_LEVEL_* from BTICARD.H / manual) ---
// Values based on manual search, likely correct but VERIFY!
#define TEST_LEVEL_0 0 // Test I/O interface
#define TEST_LEVEL_1 1 // Test memory interface
#define TEST_LEVEL_2 2 // Test communication process
#define TEST_LEVEL_3 3 // Test bus transceiver


#endif // BTI_CONSTANTS_H 