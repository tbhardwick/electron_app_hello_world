## <span id="page-0-0"></span>Contents

| CONTENTS                                         |  |
|--------------------------------------------------|--|
| 1. INTRODUCTION                                  |  |
| 1.1 ARINC 429 Overview                           |  |
| 1.2 BTIDriver for ARINC 429                      |  |
| 1.3 CoPilot®                                     |  |
| 1.4   How to Use This Manual                     |  |
|                                                  |  |
| 1.5 Support and Service                          |  |
| 1.6 Updates                                      |  |
| 2. PROGRAMMING BASICS                            |  |
| 2.1 Terminology                                  |  |
| 2.2 Getting Started                              |  |
| 2.3 Steps a Program Must Perform                 |  |
| 2.4 Handles, Cards, and Cores                    |  |
| 2.5 Receiver Example                             |  |
| 2.6 Transmitter Example                          |  |
|                                                  |  |
| 2.7 Monitor Example                              |  |
| 3. ADVANCED OPERATION                            |  |
| 3.1 Overview                                     |  |
| 3.2 Message Records                              |  |
| 3.2.1 Reserved                                   |  |
| 3.2.2 Activity                                   |  |
| 3.2.3 ARINC Word                                 |  |
| 3.2.4 List Buffer Pointer                        |  |
| 3.2.5 Time-Tag                                   |  |
| 3.2.6 Hit Counter                                |  |
| 3.2.7 Elapsed Time<br>3.2.8 Min/Max Elapsed Time |  |
| 3.2.9 Decoder Gap                                |  |
| 3.3 Filter Tables                                |  |
| 3.3.1 How Filter Tables Work                     |  |
| 3.3.2 Configuring the Filter Tables              |  |
| 3.4 Transmit Schedules                           |  |
| 3.4.1 How Schedules Work                         |  |
| 3.4.2 Creating a Schedule                        |  |
| 3.4.3 Example of a Schedule with Explicit Timing |  |
| 3.5 TSM: Theory of Operation                     |  |
| 3.6 Sequential Monitor                           |  |
| 3.7 List Buffers                                 |  |
| 3.7.1 Receive List Buffers                       |  |
| 3.7.2 Interrupts                                 |  |
| 3.7.3 Scheduled Transmit List Buffers            |  |

| 3.7.4 Asynchronous Transmit List Buffers                                                                                                                                       |  |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--|
| 3.8 Playback Mode                                                                                                                                                              |  |
| 3.8.1 Playback Configuration                                                                                                                                                   |  |
| 3.8.2 Playback Operation                                                                                                                                                       |  |
| 3.8.3 Playback Example                                                                                                                                                         |  |
| 3.9 Error Injection                                                                                                                                                            |  |
| 3.9.1 Parity Errors                                                                                                                                                            |  |
| 3.9.2 Gap Errors                                                                                                                                                               |  |
| 3.9.3 Parametric Bit Count                                                                                                                                                     |  |
| 3.9.4 Waveform Configuration                                                                                                                                                   |  |
| 3.9.5 Output State                                                                                                                                                             |  |
| 3.10 Special Events                                                                                                                                                            |  |
| 3.10.1 Event Log List                                                                                                                                                          |  |
| 3.10.2 Polling                                                                                                                                                                 |  |
| 4. BTI429 FUNCTION REFERENCE                                                                                                                                                   |  |
| 4.1 Overview of the BTIDriver API                                                                                                                                              |  |
| 4.1.1 "handle" Parameters                                                                                                                                                      |  |
| 4.1.2 "ctriflags" Parameters                                                                                                                                                   |  |
| 4.1.3 Schedule Indices (SCHNDX)                                                                                                                                                |  |
| 4.1.4 "channel" Parameters                                                                                                                                                     |  |
| 4.1.5 "message" Parameters ……………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………………… |  |
| 4.1.6 Error Values                                                                                                                                                             |  |
| 4.1.7 UINT16/UINT32                                                                                                                                                            |  |
| 4.2 Function Summaries                                                                                                                                                         |  |
| 4.3 Function Detail                                                                                                                                                            |  |
| 5. BTICARD FUNCTION REFERENCES                                                                                                                                                 |  |
| 5.1 Function Summaries                                                                                                                                                         |  |
| 5.2 Function Detail                                                                                                                                                            |  |
| 6. DEVICE DEPENDENCE                                                                                                                                                           |  |
|                                                                                                                                                                                |  |
| 7. MULTI-PROTOCOL / DEVICE PROGRAMS                                                                                                                                            |  |
| 7.1 Programming Rules                                                                                                                                                          |  |
| 7.2 BTICard Functions                                                                                                                                                          |  |
| 7.3 Sequential Record                                                                                                                                                          |  |
| 7.4 Event Log List                                                                                                                                                             |  |
| 7.5 Using Multiple Devices                                                                                                                                                     |  |
| 8. REVISION HISTORY                                                                                                                                                            |  |

## **LIST OF TABLES**

| Table 2.1 —Example Messages9                                     |  |
|------------------------------------------------------------------|--|
| Table 2.2 —Transmit Intervals<br>13                              |  |
| Table 2.3 —Sequential Monitor Filtering Options15                |  |
| Table 3.1 —Command Blocks in the Transmit Schedule23             |  |
| Table 3.2 —Example Transmit Intervals<br>24                      |  |
| Table 3.3 —6G Parametric Waveform Functions35                    |  |
| Table 3.4 —6G Transmitter Output State Examples36                |  |
| Table 4.1—ARINC 429 BTI429_ functions40                          |  |
| Table 5.1 —Protocol-Independent (BTICard_ ) Functions165         |  |
| Table 6.1—Devices Grouped by Generation and Functionality<br>292 |  |

## **LIST OF FIGURES**

| Figure 1.1—A Sample CoPilot Screen<br>7                                             |  |
|-------------------------------------------------------------------------------------|--|
| Figure 2.1—Example Receiver Program12                                               |  |
| Figure 2.2—Example Transmitter Program14                                            |  |
| Figure 2.3 —Example Monitor Program16                                               |  |
| Figure 3.1—Message Flow Between Primary Data Structures in Typical 429 Device<br>18 |  |
| Figure 3.2—Message Record Structure19                                               |  |
| Figure 3.3—Filter Tables<br>21                                                      |  |
| Figure 3.4—Transmit Schedule22                                                      |  |
| Figure 3.5—Transmit Schedule for Low-Speed Timing Requirements of Table 3.225       |  |
| Figure 3.6—Example with Explicit Transmit Schedule25                                |  |
| Figure 3.7—Operation of Sequential Record in Interval Mode<br>28                    |  |
| Figure 3.8—Receive and Transmit List Buffer Structure29                             |  |
| Figure 3.9—Operation of an Asynchronous Transmit List Buffer31                      |  |
| Figure 3.10—Example of Simple Playback Data Transmit<br>33                          |  |
| Figure 3.11—Example of Long Parametric Message35                                    |  |
| Figure 4.1—Standard and Reversed Label Formats of the ARINC 429 Word<br>39          |  |