![](_page_0_Picture_0.jpeg)

![](_page_0_Picture_1.jpeg)

# PROGRAMMING MANUAL ARINC 429 BTIDriver-Compliant Devices

# **Safety Warning**

Ballard products are not intended, warranted, or certified for any particular use or application or for use in any application where failure of the products could lead directly to death, personal injury, or damage to property. Customers, licensees, and/or users are responsible for establishing and assuring suitability and proper use of Ballard products for their particular use or application.

# **Trademarks**

OmniBus, BUSBox, and CoPilot are registered trademarks of Astronics Advanced Electronic Systems Corp. BTIDriver, Avionics BusBox, and OmniBusBox are trademarks of Astronics Advanced Electronic Systems Corp. Windows is a trademark of Microsoft Corporation. All other product names or trademarks are the property of their respective owners.

# **Copyright Notice**

Copyright © 2024 Astronics Advanced Electronic Systems Corp.—hereafter referred to as Astronics AES. Permission to copy and distribute this manual is for the purchaser's private use only and is conditioned upon the purchaser's use and application with the hardware and/or software that was shipped with this manual. This notice allows no commercial resale or outside distribution rights. This material remains the property of Astronics Advanced Electronic Systems Corp. All other rights reserved by Astronics Advanced Electronic Systems Corp.

# **Contact Astronics**

Direct any questions regarding this User Manual to your Ballard Customer Support contact at Astronics AES. If you are unfamiliar with your Ballard Customer Support contact, you may alternatively direct your questions to:

### **Astronics AES**

12950 Willows Road NE Kirkland, WA 98034 USA +1.425.339.0281 [Ballard.Support@astronics.com](mailto:Ballard.Support@astronics.com)

On the web at: [www.astronics.com/BallardSupport](http://www.astronics.com/BallardSupport)

EXPORT CONTROLLED TECHNOLOGY. This information is controlled for export in accordance with the U.S. Export Administration Regulations ("EAR") under EAR99. Export, reexport, or transfer without a license or other authorization from the Bureau of Industry and Security is prohibited.

<span id="page-2-0"></span>

| CONTENTS3      |                                                 |  |
|----------------|-------------------------------------------------|--|
| 1.             | INTRODUCTION6                                   |  |
| 1.1            | ARINC 429 Overview<br>6                         |  |
| 1.2            | BTIDriver for ARINC 4296                        |  |
| 1.3            | CoPilot® 6                                      |  |
| 1.4            | How to Use This Manual7                         |  |
| 1.5            | Support and Service8                            |  |
|                |                                                 |  |
| 1.6            | Updates<br>8                                    |  |
| 2.             | PROGRAMMING BASICS9                             |  |
| 2.1            | Terminology<br>9                                |  |
| 2.2            | Getting Started<br>9                            |  |
| 2.3            | Steps a Program Must Perform<br>10              |  |
| 2.4            | Handles, Cards, and Cores10                     |  |
| 2.5            | Receiver Example<br>11                          |  |
| 2.6            | Transmitter Example<br>13                       |  |
| 2.7            | Monitor Example<br>15                           |  |
|                |                                                 |  |
| 3.             | ADVANCED OPERATION18                            |  |
| 3.1            | Overview<br>18                                  |  |
| 3.2            | Message Records<br>18                           |  |
| 3.2.1          | Reserved 19                                     |  |
| 3.2.2          | Activity 19                                     |  |
| 3.2.3          | ARINC Word20                                    |  |
| 3.2.4<br>3.2.5 | List Buffer Pointer 20<br>Time-Tag 20           |  |
| 3.2.6          | Hit Counter 20                                  |  |
| 3.2.7          | Elapsed Time21                                  |  |
| 3.2.8          | Min/Max Elapsed Time 21                         |  |
| 3.2.9          | Decoder Gap 21                                  |  |
| 3.3            | Filter Tables21                                 |  |
| 3.3.1          | How Filter Tables Work 21                       |  |
| 3.3.2          | Configuring the Filter Tables22                 |  |
| 3.4            | Transmit Schedules<br>22                        |  |
| 3.4.1<br>3.4.2 | How Schedules Work 22<br>Creating a Schedule 24 |  |
| 3.4.3          | Example of a Schedule with Explicit Timing 24   |  |
| 3.5            | TSM: Theory of Operation26                      |  |
| 3.6            | Sequential Monitor<br>27                        |  |
| 3.7            | List Buffers<br>28                              |  |
| 3.7.1          | Receive List Buffers29                          |  |
| 3.7.2          | Interrupts 29                                   |  |
| 3.7.3          | Scheduled Transmit List Buffers30               |  |

| 3.7.4 | Asynchronous Transmit List Buffers 30       |  |
|-------|---------------------------------------------|--|
| 3.8   | Playback Mode31                             |  |
| 3.8.1 | Playback Configuration31                    |  |
| 3.8.2 | Playback Operation 31                       |  |
| 3.8.3 | Playback Example 32                         |  |
| 3.9   | Error Injection33                           |  |
| 3.9.1 | Parity Errors33                             |  |
| 3.9.2 | Gap Errors 34                               |  |
| 3.9.3 | Parametric Bit Count34                      |  |
| 3.9.4 | Waveform Configuration35                    |  |
| 3.9.5 | Output State 36                             |  |
| 3.10  | Special Events<br>36                        |  |
|       | 3.10.1 Event Log List36<br>3.10.2 Polling37 |  |
|       |                                             |  |
| 4.    | BTI429 FUNCTION REFERENCE38                 |  |
| 4.1   | Overview of the BTIDriver API<br>38         |  |
| 4.1.1 | "handle" Parameters 38                      |  |
| 4.1.2 | "ctrlflags" Parameters 38                   |  |
| 4.1.3 | Schedule Indices (SCHNDX)38                 |  |
| 4.1.4 | "channel" Parameters 38                     |  |
| 4.1.5 | "message" Parameters 39                     |  |
| 4.1.6 | Error Values39                              |  |
| 4.1.7 | UINT16/UINT3239                             |  |
| 4.2   | Function Summaries<br>40                    |  |
| 4.3   | Function Detail<br>42                       |  |
| 5.    | BTICARD FUNCTION REFERENCES165              |  |
| 5.1   | Function Summaries<br>165                   |  |
| 5.2   | Function Detail<br>167                      |  |
|       |                                             |  |
| 6.    | DEVICE DEPENDENCE292                        |  |
| 7.    | MULTI-PROTOCOL / DEVICE PROGRAMS293         |  |
| 7.1   | Programming Rules<br>293                    |  |
| 7.2   | BTICard_ Functions<br>293                   |  |
|       |                                             |  |
| 7.3   | Sequential Record294                        |  |
| 7.4   | Event Log List<br>294                       |  |
| 7.5   | Using Multiple Devices294                   |  |
| 8.    | REVISION HISTORY<br>295                     |  |

# **LIST OF TABLES**

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
|                                                                  |  |
| Table 6.1—Devices Grouped by Generation and Functionality<br>292 |  |

# **LIST OF FIGURES**

| Figure 1.1—A Sample CoPilot Screen<br>7<br>Figure 2.1—Example Receiver Program12    |  |
|-------------------------------------------------------------------------------------|--|
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

# <span id="page-5-0"></span>**1. Introduction**

This manual documents the ARINC 429 functions of the Ballard unified API library called **BTIDriver™**. You can use this manual to learn how to create custom applications for any of ARINC 429 BTIDrivercompliant interface products.

The following conventions are observed throughout this manual:

- *Device* with a capital *D* is used generically to mean any BTIDriver-compliant interface device.
- Driver function names are in bold type and are all prefixed by "BTICard\_" or **"**BTI429\_" (e.g., BTI429\_ChConfig).
- A small *h* suffix indicates hexadecimal values (e.g., F01Ch)
- Constants defined in the driver software are written in all capital letters (e.g., CHCFG429\_DEFAULT).
- The symbol ?? is used in function names in this manual to indicate a category of functions with similar uses or attributes. These characters should be replaced by a category prefix or suffix in an actual function call (e.g., BTI429\_MsgData?? to represent BTI429\_MsgDataRd and BTI429\_MsgDataWr).

### <span id="page-5-1"></span>**1.1 ARINC 429 Overview**

ARINC 429 is the specification that defines a local area network used on commercial aircraft and is the industry's standard for transfer of digital data between avionics system elements. The specification describes how an avionics system transmits information over a single twisted and shielded pair of wires (the databus) to as many as 20 receivers connected to that databus. Bidirectional data flow on a given bus is not permitted.

### <span id="page-5-2"></span>**1.2 BTIDriver for ARINC 429**

BTIDriver is a unified library of API (Application Program Interface) functions designed to control BTIDriver-compliant hardware products. Astronics AES makes many hardware products that interface with various avionics databuses to facilitate avionics development, simulation, and testing. Software is used to operate these hardware Devices. Programmers can use BTIDriver to create custom software for Ballard hardware Devices.

BTIDriver supports different avionics databus protocols and different Ballard hardware Devices. As long as the Devices have similar hardware capability, BTIDriver applications written for one Device can run on another Device with little or no change. This manual only documents the principles and functions used for ARINC 429 software applications. Other protocols are documented separately. Chapter [7](#page-292-0) provides guidance for writing multi-protocol BTIDriver applications.

### <span id="page-5-3"></span>**1.3 CoPilot®**

As an alternative to writing your own custom programs, you can operate your ARINC 429 interface Device with Ballard CoPilot® , a Windows® -based GUI (Graphical User Interface) software (see [Figure](#page-6-1)  [1.1\)](#page-6-1). CoPilot greatly simplifies tasks such as defining and scheduling bus messages and capturing and analyzing data. Using CoPilot, you can transmit, monitor, and record databus messages with a few clicks of the mouse. CoPilot is user-friendly and has many timesaving features. For example, bus messages can be automatically detected, posted in the hardware tree, and associated with the appropriate attributes from the database of equipment, message, and engineering unit specifications.

CoPilot users can quickly configure, run, and display the activity of multiple databuses in a unified view. Data can be observed and changed in engineering units while the bus is running. The Strip View graphically illustrates the history of the selected data values. Data can also be entered and viewed as

virtual instruments (knobs, dials, gauges, etc.) created by the user or automatically generated by dragging and dropping an item into the Control View window.

CoPilot can host multiple channels and databus protocols in the same project, making it an ideal tool for operating multi-protocol Devices. CoPilot can be purchased separately or with a BTIDrivercompliant hardware Device. For more information or a free evaluation copy, please contact Customer Support (see Section [1.5\)](#page-7-0). In addition, you can learn more about the latest version of CoPilot at [www.astronics.com/CoPilot.](http://www.astronics.com/CoPilot)

![](_page_6_Figure_4.jpeg)

<span id="page-6-1"></span>*Figure 1.1—A Sample CoPilot Screen*

### <span id="page-6-0"></span>**1.4 How to Use This Manual**

This manual is designed to be a tutorial and reference guide. You can read the sections that you need and refer to it as required. After reading Chapter [2](#page-8-0) (Programming Basics) and referring to Chapter [4](#page-37-0) (Function Reference) you should be able to write simple computer programs to operate your Ballard ARINC 429 interface Device. Refer to Chapter [3](#page-17-0) (Advanced Operation) for more complex applications. This guide can be used in conjunction with the programming manuals for other avionics databus protocols (see Chapte[r 7\)](#page-292-0).

This manual assumes you are familiar with the essentials of compiling, linking, and running programs in C. It is also assuming you are familiar with the ARINC 429 protocol. With minor exceptions, the content of this manual also applies to other programming languages.

### <span id="page-7-0"></span>**1.5 Support and Service**

Astronics AES offers technical support before and after purchase of our Ballard products. Our hours of operation are 7:00 am to 5:00 pm Pacific Time (PT). Support and sales engineers are often available outside these hours as well. We invite your questions and comments on any of our products.

Telephone: +1.425.339.0281

Web: [www.astronics.com/BallardSupport](http://www.astronics.com/BallardSupport)

Email: [Ballard.Support@astronics.com](mailto:Ballard.Support@astronics.com)

### <span id="page-7-1"></span>**1.6 Updates**

At Astronics AES, we take pride in high quality, reliable products that meet the needs of our customers. Because we are continually improving our products, we may issue periodic updates to our documentation and software. Register your product a[t www.astronics.com/BallardSupport](http://www.astronics.com/BallardSupport) for information on updates, customer service, and new products.

## <span id="page-8-0"></span>**2. Programming Basics**

This chapter illustrates basic ARINC 429 operation using the BTIDriver library.

Examples demonstrate:

- Operation of ARINC 429 receive channels
- Operation of ARINC 429 transmit channels
- Monitoring and selectively recording ARINC 429 bus traffic

The examples provide code fragments in the C language. Libraries for other standard languages are available on request. Driver functions are prefixed by "BTICard\_" for protocol-independent Device functions and "BTI429\_" for ARINC 429-specific functions. The examples are given with the assumption that CH0 is a receive channel and CH4 is a transmit channel. The transmit and receive channels for your ARINC 429 Device may be different.

This chapter describes the operational elements relevant to most applications. After reading this chapter, you will be familiar with the essentials of an application program, and you will recognize the most important driver functions. Complete descriptions of all BTIDriver functions for ARINC 429 may be found in Chapter [4.](#page-37-0)

### <span id="page-8-1"></span>**2.1 Terminology**

The basic unit of information defined by the ARINC 429 specification is a 32-bit word made up of several bit fields. The most important bit fields are the label (8 bits), SDI (2 bits), and data (variable length). Given the source of the data, the label determines how the data field is interpreted, including:

- Data format (binary, BCD, ASCII character, etc.)
- Data type (temperature, pressure, etc.)
- Units (Newtons, degrees Celsius, etc.)

The SDI (Source/Destination Identifier) identifies the source of a word when more than one source exists on the aircraft (e.g., left engine temperature versus right engine temperature). However, the SDI bits are often ignored.

Although a 32-bit ARINC 429 word is sometimes referred to as a *label* in the avionics industry, to avoid ambiguity with the 8-bit label field this manual uses the term *message.*

### <span id="page-8-2"></span>**2.2 Getting Started**

The first step in developing an application is identifying the ARINC 429 messages to be handled. For the examples in this chapter, suppose we want to simultaneously:

- Receive selected messages from a Global Positioning System (GPS)
- Simulate transmission of messages from an Air Data System (ADS)
- Record all above activity to disk for later analysis

<span id="page-8-3"></span>Specifically, assume that among all messages that GPS and ADS units can transmit, we are interested only in those shown in the following table.

|     | Equipment                         | ARINC 429 Word Name       | Octal Label |  |
|-----|-----------------------------------|---------------------------|-------------|--|
| GPS |                                   | Present Position—Latitude | 310         |  |
|     | GPS<br>Present Position—Longitude |                           | 311         |  |
|     | ADS                               | Computed Airspeed         | 206         |  |
|     | ADS<br>Total Air Temperature      |                           | 211         |  |
|     | ADS<br>Altitude Rate              |                           | 212         |  |

*Table 2.1 —Example Messages*

Part of the configuration process is associating messages with Message Records. Each Message Record contains a single 32-bit ARINC 429 word, and possibly, some extra data related to that message (e.g., the time-tag). When the Device receives a given message, the full 32-bit word is stored in a designated Message Record. When the Device transmits a given message, the word to be transmitted is retrieved from a predetermined Message Record. More detailed information on Message Records may be found in Section [3.2.](#page-17-2)

## <span id="page-9-0"></span>**2.3 Steps a Program Must Perform**

Before examining the examples, you should understand the sequence of steps a program must perform to operate a Device. Operating your Device with the BTIDriver library involves eight general steps, most of which require only a single function call. Three of the steps have many options, so the number of function calls required depends on the options desired. The Steps are as follows:

- 1. **Open:** Software gains access to the Device hardware through the host computer's operating system. The BTICard\_CardOpen and BTICard\_CoreOpen functions request access and obtain *handles* by which subsequent API functions require (see Section [2.4](#page-9-1) for more information on card and core handles).
- 2. **Reset:** Although not required, it is a good idea to reset any previous hardware configuration of the Device by calling the BTICard\_CardReset function for each core.
- 3. **Configure:** The required Device channels and capabilities are enabled by configuration functions. Transmitter operation is enabled by creating a transmit Schedule. Filter Tables are configured for receiver operation. Special features of the Device may require extra configuration. Other low-level options such as Event Log List entries and bus speed are also set at this point.
- 4. **Initialize Data:** Data associated with transmit messages should be initialized before the Device is activated. Initialization prevents the transmission of invalid messages.
- 5. **Activate:** BTICard\_CardStart activates all configured channels of a core simultaneously. It should be called separately for each core. Once activated, the Device transmits and/or receives from the databuses independently of the host computer.
- 6. **Handle Data:** The Device transmits and receives messages according to its configuration without requiring any host supervision. A program running on the host can update data for transmission or read received data at any time. To reduce data latency and/or host overhead, applications may access data in response to bus events detected by polling or interrupts. API functions are provided to simplify data exchange between the host and the Device. Additionally, a software library of utility functions is provided to insert and extract the bit fields of an ARINC 429 word. These functions may be used independently of the Device.
- 7. **Deactivate:** BTICard\_CardStop deactivates a core on the Device. Unless a core is explicitly deactivated, it continues operating even if the application software halts.
- 8. **Close:** Whether or not the Device is deactivated, BTICard\_CardClose must be called before an application terminates. Failure to do so can cause unpredictable results. BTICard\_CardClose does not deactivate the Device.

### <span id="page-9-1"></span>**2.4 Handles, Cards, and Cores**

In order for software to address hardware, the software must be able to specify the hardware so that it is differentiated from other similar, and even dissimilar hardware. Also, if the hardware has more than one section the software must be able to specify which section. Ballard hardware Devices are implemented with one or more sections called cores. Typically, each core has its own protocol

processing circuitry. The BTIDriver functions specify the Device with a card handle and the core with a core handle.

Nearly all functions require a handle parameter. The handle is always the last parameter in any function that requires it. BTICard\_CardOpen**,** which is always the first function called, obtains a card handle (*hCard*). The card handle uniquely identifies a Device when more than one is used in a single computer. The card handle is different from the card number assigned by the host computer operating system.

A multi-core Device can have cores that work with the same or different protocols. In a way, the software sees each core as a separate card, so a handle (*hCore*) is needed to differentiate the cores. When passed a card handle (*hCard*) and the core number (*corenum*), BTICard\_CoreOpen returns a core handle (*hCore*), which uniquely identifies the core/card combination. Therefore, BTICard\_CardOpen must be called first to provide the card handle for BTICard\_CoreOpen**.**

The following are handle-related guidelines to use when developing programs for BTIDriver-compliant Devices:

- 1. Run the test program provided on the distribution disk with the Device (e.g., on Windows® run BTITST32.EXE) to find the Devices, along with the number and configuration of their cores. When there is only one Device in the system, its *cardnum* is zero (0). When a Device has only one core its *corenum* is zero (0). At any time, you can use this test program or the Windows Device Manager (for Windows operating systems) to reassign the card number.
- 2. At the beginning of the program, get the card handle (*hCard*) for each Device by calling BTICard\_CardOpen**.**
- 3. Get the core handle (*hCore*) for each core on each Device by calling BTICard\_CoreOpen.
- 4. Use *hCore* as the handle in all subsequent functions except BTICard\_CardClose**.**
- 5. At the end of your program, call BTICard\_CardClose once for each Device using its *hCard* handle to release the card and all of its core resources.
- 6. It is advisable to use the above procedure (using both *hCard* and *hCore*) even when the Device has only one core. This makes the code adaptable for use with multi-core Devices.
- 7. Use of *hCard* in place of *hCore* addresses the first core (i.e., Core A with *corenum =* 0). This allows software written for legacy BTIDriver compliant Devices to operate on the first core of multi-core Devices.
- 8. Legacy BTIDriver-compliant Devices that had not referred to cores in the documentation have only one core and should use core number (*corenum*) zero (0) in BTICard\_CoreOpen**.**

The following examples show how to perform these steps using the BTIDriver functions.

### <span id="page-10-0"></span>**2.5 Receiver Example**

In this section, we describe an example program that receives latitude and longitude messages (see [Figure 2.1—Example Receiver Program\)](#page-11-0). The primary task in configuring receive channels is telling the Device which messages it should store in specific Message Records. The Device ignores messages not assigned a Message Record unless a default record has been defined. If a default record is defined, all messages not assigned their own Message Record are written to the default record. The default record makes it possible to receive all bus traffic while isolating the messages of interest.

The code in the following example configures the Device to receive all traffic. The two GPS messages of interest are stored in separate Message Records. The Device writes all other messages to the default record.

```
HCARD hCard;
HCORE hCore; 
MSGSTRUCT429 msgdefault, latitude, longitude;
INT cardnum = 0; //Assumes only one Device has been installed
INT corenum = 0; //Assumes only one core on the Device
BTICard_CardOpen(&hCard,cardnum); //Open the Device
BTICard_CoreOpen(&hCore,corenum,hCard); //Open each core
BTICard_CardReset(hCore); //Reset each core
//Configure channel
BTI429_ChConfig(CHCFG429_AUTOSPEED,CH0,hCore);
// Define filters
msgdefault.addr = BTI429_FilterDefault(MSGCRT429_DEFAULT,CH0,hCore);
latitude.addr = BTI429_FilterSet(MSGCRT429_DEFAULT,0310,SDIALL,CH0,hCore); 
longitude.addr = BTI429_FilterSet(MSGCRT429_DEFAULT,0311,SDIALL,CH0,hCore);
//Start the core
BTICard_CardStart(hCore);
//Read data as required by application
while (!done)
{
       msgdefault.data = BTI429_MsgDataRd(msgdefault.addr, hCore);
       latitude.data = BTI429_MsgDataRd(latitude.addr, hCore);
       longitude.data = BTI429_MsgDataRd(longitude.addr, hCore);
}
BTICard_CardStop(hCore); //Stop each core
BTICard_CardClose(hCard); //Close the Device
```
*Figure 2.1—Example Receiver Program*

<span id="page-11-0"></span>The code starts by creating message structures named msgdefault, latitude, and longitude. These structures have members for the message address and the value of the data.

BTICard\_CardOpen and BTICard\_CoreOpen are always the first driver functions called since they obtain the handles used by subsequent functions. This example assumes there is only one Device with a single core installed in the system, so the card number (cardnum) and core number (corenum) are given values of zero. BTICard\_CardReset clears the core memory, removing any existing configuration data.

BTI429\_ChConfig sets up an empty Filter Table for a receive channel and enables or disables selected options for that channel. This function enables the automatic speed detection feature and selects all other default options. Constants can be included to turn on specific options. Note that this function does not activate the channel. CH0 is a predefined constant referring to physical channel zero (0) of the core.

A Filter Table is an array of pointers to Message Records. There is one pointer for every possible message type (i.e., every possible label/SDI combination). All pointers are initially zero. A received message is recorded only if its entry in the Filter Table points to a valid Message Record. The BTI-429\_FilterDefault function fills the Filter Table with pointers, so all messages received on that channel are written to the default record. If BTI429\_FilterDefault is used, it must precede any calls to BTI429\_FilterSet. The two BTI429\_FilterSet functions assign receive messages to Message Records by placing entries in the Filter Table. The BTI429\_FilterDefault and BTI-429\_FilterSet functions return the address of the Message Record.

The SDIALL constant tells the Device to accept all messages with the given label and any SDI. Different constants may be used to specify that only messages with specific SDIs are to be received. Messages with different SDIs could be assigned to separate Message Records.

The Device begins receiving messages only after BTICard\_CardStart is called. As discussed above, when a message is received, it is stored in its assigned Message Record. The previous value in that Message Record is overwritten. BTI429\_MsgDataRd may be called at any time to return the full 32-bit ARINC word from a specified Message Record. A software library of functions is provided to extract the relevant bit fields from the ARINC word.

The program ends with the required BTICard\_CardStop and BTICard\_CardClose functions. Receiving messages on other receive channels only requires extra BTI429\_ChConfig**,**  BTI429\_FilterDefault, and BTI429\_FilterSet function calls.

### <span id="page-12-0"></span>**2.6 Transmitter Example**

In this section, we describe an example program that transmits airspeed, temperature, and altitude rate messages (see table below). Normally, ARINC 429 sources transmit messages periodically at repetition rates prescribed by the ARINC 429 specification. The specification defines a minimum and maximum transmit interval for each message. Many different transmit intervals are called out in the specification, so transmitting many different messages may involve complex timing requirements. The BTIDriver functions handle timing requirements automatically.

To illustrate this example, recall that the example is simulating an ADS (Air Data System) transmitting three messages (transmit intervals are shown in the below table). The code in the figure below configures the Device to transmit these messages with proper timing.

| Word                  | Octal Label | Minimum Transmit Interval | Maximum Transmit Interval |
|-----------------------|-------------|---------------------------|---------------------------|
| Computed Airspeed     | 206         | 62.5 ms                   | 125 ms                    |
| Total Air Temperature | 211         | 250 ms                    | 500 ms                    |
| Altitude Rate         | 212         | 31.3 ms                   | 62.5 ms                   |
|                       |             |                           |                           |

| Computed Airspeed             | 206 | 62.5 ms | 125 ms  |  |
|-------------------------------|-----|---------|---------|--|
| Total Air Temperature         | 211 | 250 ms  | 500 ms  |  |
| Altitude Rate                 | 212 | 31.3 ms | 62.5 ms |  |
| Table 2.2 —Transmit Intervals |     |         |         |  |

<span id="page-12-1"></span>This example begins like the previous one. First, message structures are defined for the messages of interest. Three arrays are also created for building a Schedule. As always, the BTICard\_CardOpen and BTICard\_CoreOpen functions must be called first to obtain the necessary handles. BTI429\_Ch-Config automatically determines that CH4 is a transmit channel and configures it accordingly.

```
HCARD hCard;
HCORE hCore;
MSGSTRUCT429 comp_airspeed, tot_air_temp, altitude_rate;
MSGADDR msgaddr[3];
INT min_intrvls[3];
INT max_intrvls[3];
INT cardnum = 0; //Assumes only one Device has been installed
INT corenum = 0; //Assumes only one core on the Device
BTICard_CardOpen(&hCard,cardnum); //Open the 
DeviceBTICard_CoreOpen(&hCore,corenum,hCard); //Open each core
BTICard_CardReset(hCore); //Reset each core
//Configure channel
BTI429_ChConfig(CHCFG429_HIGHSPEED,CH4,hCore);
//Create 3 messages
comp_airspeed.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
tot_air_temp.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
altitude_rate.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
//Set up arrays and transmit intervals
msgaddr[0] = comp_airspeed.addr;
min_intrvl[0] = 63;
max_intrvl[0] = 125;
msgaddr[1] = tot_air_temp.addr;
min_intrvl[1] = 250;
max_intrvl[1] = 500;
msgaddr[2] = altitude_rate.addr;
min_intrvl[2] = 32;
max_intrvl[2] = 62;
//Build Schedule
BTI429_SchedBuild(3,msgaddr,min_intrvl,max_intrvl,CH4,hCore);
//Initialize data
BTI429_MsgDataWr(0206,comp_airspeed.addr,hCore);
BTI429_MsgDataWr(0211,tot_air_temp.addr,hCore);
BTI429_MsgDataWr(0212,altitude_rate.addr,hCore);
//Start the core
BTICard_CardStart(hCore);
//Update data as required by the application
while (!done)
{
   BTI429_MsgDataWr(comp_airspeed.data,comp_airspeed.addr,hCore);
   BTI429_MsgDataWr(tot_air_temp.data,tot_air_temp.addr,hCore);
   BTI429_MsgDataWr(altitude_rate.data,altitude_rate.addr,hCore);
}
BTICard_CardStop(hCore); //Stop each core
```
<span id="page-13-0"></span>**BTICard\_CardClose**(hCard); *//Close the Device*

*Figure 2.2—Example Transmitter Program*

The easiest way to create a Schedule that transmits the three messages at their proper transmit intervals is to use the BTI429\_SchedBuild function. To do this, three messages are created using BTI429\_MsgCreate, which returns their addresses. BTI429\_MsgCreate in transmit is equivalent to BTI429\_FilterSet in receive. Next, we set the values of three arrays: one for the message addresses, one for minimum transmit intervals, and one for maximum transmit intervals. Information on a given message is contained in the same position in each of these arrays. The minimum and maximum transmit intervals (rounded to integer values in milliseconds) are defined as specified in [Table 2.2.](#page-12-1)

The BTI429\_SchedBuild function constructs a Schedule in the Device memory. A Schedule is a sequence of messages separated by timed gaps. The gaps are calculated so the timing requirements of all messages are satisfied. The parameters in the example indicate that BTI429\_SchedBuild is to schedule transmission of three messages on CH4 from the information in the three arrays. An alternative to using BTI429\_SchedBuild is to explicitly define your own Schedule as described in Section [3.4.](#page-21-1)

The Message Records are initialized using BTI429\_MsgDataWr before BTICard\_CardStart commands the Device to begin transmitting. Here all three messages are initialized to zero except for their labels. Notice the label is the least significant part of the data and is entered in octal. Also using BTI429\_MsgDataWr, we can update the data value of a message at any time.

See [4](#page-37-0) for more information regarding the order of bits in an ARINC word. The three messages are repeatedly transmitted at the proper rates until the core is halted by BTICard\_CardStop. As always, the program ends with BTICard\_CardClose.

### <span id="page-14-0"></span>**2.7 Monitor Example**

In a sense, any receiver is a monitor that holds the most recent value of the received data. However, Ballard Devices have a Sequential Monitor that records a time-tagged history of messages which is useful for analyzing and reconstructing all or selected bus activity. This activity is stored in a Sequential Record. Additional information on the Sequential Monitor may be found in Section [3.6.](#page-26-0)

The code in the example below combines the previous receive and transmit examples while configuring the Sequential Monitor to capture only the five message types defined in the examples. Specifically, the Sequential Monitor will capture only the latitude and longitude messages from the receive channel and all words transmitted by the core.

| Use these functions and<br>parameters →<br>to record  | BTI429_ChConfig | Receive<br>BTI429_FilterSet | Transmit<br>BTI429_MsgCreate |
|--------------------------------------------------------|-----------------|-----------------------------|------------------------------|
| from all ARINC 429<br>messages on selected<br>channels | CHCFG429_SEQALL | don't care                  | don't care                   |
| from selected 429<br>messages on selected<br>channels  | CHCFG429_SEQSEL | MSGCRT429_SEQ               | MSGCRT429_SEQ                |

*Table 2.3 —Sequential Monitor Filtering Options*

<span id="page-14-1"></span>Filtering irrelevant messages out of the bus traffic conserves Sequential Monitor memory and makes the Sequential Record easier to analyze. Filtering for the Sequential Monitor can be established at either the channel level or the message level. If the CHCFG429\_SEQALL constant is used in BTI429\_ChConfig, then all ARINC 429 messages on that channel are saved in the Sequential Record. If only specific messages are to be saved, then the MSGCRT429\_SEQ constant is used in either BTI429\_FilterSet for receive or BTI429\_MsgCreate for transmit. These are summarized in the table above.

Much of the code in the figure below is a combination of modified fragments from the previous examples. Since we want to monitor all transmitted messages, the CHCFG429\_SEQALL constant is used in place of the CHCFG429\_DEFAULT constant in BTI429\_ChConfig for CH4. To save the latitude and longitude messages in the Sequential Record, the MSGCRT429\_SEQ constant is used in their respective BTI429\_FilterSet functions. BTICard\_SeqConfig allocates memory for and configures the Sequential Monitor. The SEQCFG\_DEFAULT constant selects all default settings. BTICard\_CardStart activates all configured channels on the core. After activation, the core begins simultaneously receiving, transmitting, and recording the specified traffic in its Sequential Record.

The while() loop in the code polls and processes the Sequential Record. For illustration purposes, this example simply prints each ARINC 429 message value and its time-tag. BTICard\_SeqRd copies the available records from the on-board Sequential Record to the user-supplied buffer (seqbuf) and returns the number of words copied (seqcount). To process records in seqbuf, it is necessary to find the start and type of each record, which may be done with different BTICard\_SeqFind?? functions. Here, BTICard\_SeqFindInit initializes a structure (sfinfo). Then, repeated calls to BTICard\_SeqFindNext429 find and point (pRec429) to the next occurrence of a 429-type record. The desired values are then printed from the record. If the application requires it, the received and transmitted messages may be read/written as in the previous examples. As usual, the program ends with BTICard\_CardStop and BTICard\_CardClose.

```
HCARD hCard;
HCORE hCore;
MSGSTRUCT429 msgdefault, latitude, longitude;
MSGSTRUCT429 comp_airspeed, tot_air_temp, altitude_rate;
MSGADDR msgaddr[3];
INT min_intrvls[3];
INT max_intrvls[3];
USHORT seqbuf[2048];
ULONG seqcount;
SEQFINDINFO sfinfo;
LPSEQRECORD429 pRec429;
INT cardnum = 0; //Assumes only one Device has been installed
INT corenum = 0; //Assumes only one core on the Device
BTICard_CardOpen(&hCard,cardnum); //Open the Device
BTICard_CoreOpen(&hCore,corenum,hCard); //Open each core
BTICard_CardReset(hCore); //Reset each core
//Configure receive channel
BTI429_ChConfig(CHCFG429_AUTOSPEED,CH0,hCore);
//Configure transmit channel
BTI429_ChConfig(CHCFG429_SEQALL,CH4,hCore);
//Create 3 transmit messages
comp_airspeed.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
tot_air_temp.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
altitude_rate.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
//Set up arrays and transmit intervals
msgaddr[0] = comp_airspeed.addr;
min_intrvl[0] = 63;
max_intrvl[0] = 125;
msgaddr[1] = tot_air_temp.addr;
min_intrvl[1] = 250;
max_intrvl[1] = 500;
msgaddr[2] = altitude_rate.addr;
min_intrvl[2] = 32;
max_intrvl[2] = 62;
//Build Schedule
BTI429_SchedBuild(3,msgaddr,min_intrvl,max_intrvl,CH4,hCore);
//Initialize data
BTI429_MsgDataWr(0206,comp_airspeed.addr,hCore);
BTI429_MsgDataWr(0211,tot_air_temp.addr,hCore);
BTI429_MsgDataWr(0212,altitude_rate.addr,hCore);
//Define filters
msgdefault.addr = BTI429_FilterDefault(MSGCRT429_DEFAULT,CH0,hCore); 
latitude.addr = BTI429_FilterSet(MSGCRT429_SEQ,0310,SDIALL,CH0,hCore); 
longitude.addr = BTI429_FilterSet(MSGCRT429_SEQ,0311,SDIALL,CH0,hCore);
```
<span id="page-15-0"></span>*Figure 2.3 —Example Monitor Program (Continued on Next Page)*

*(Continued from previous page)*

```
//Configure the Sequential Record
BTICard_SeqConfig(SEQCFG_DEFAULT,hCore);
//Start the core
BTICard_CardStart(hCore);
while (!done)
{
       seqcount = BTICard_SeqRd(seqbuf,hCore); //Read the Sequential Record
       BTICard_SeqFindInit(seqbuf,seqcount,&sfinfo); //Initialize Find functions
       while(!BTICard_SeqFindNext429(&pRec429,&sfinfo)) //Find/process 429 records
       {
              //Write to disk, display data, etc. as desired. For example:
              printf("\nData:%08lX Time Stamp:%08lX",
                      pRec429->data,
                      pRec429->timestamp);
              //Also, read and write message data as desired
       {
}
BTICard_CardStop(hCore); //Stop each core
BTICard_CardClose(hCard); //Close the Device
```
*Figure 2.3 [—Example Monitor Program](#page-15-0) (Continued)*

The preceding examples illustrated the most important BTIDriver functions. Your programs for your Device can be modeled after the code in these examples. Complete source code for these examples may be found on the distribution disk and detailed function descriptions are found in chapter [4.](#page-37-0) Further information may be contained in the README files on the distribution disk.

# <span id="page-17-0"></span>**3. Advanced Operation**

The purpose of this chapter is to provide you with a deeper understanding of how your Device works internally. Such insight will help you use the BTIDriver functions for more advanced applications. This chapter describes how the capabilities of the Device are implemented in its internal data structures. Some operational details omitted from the previous chapter are also included.

### <span id="page-17-1"></span>**3.1 Overview**

The unique power of your BTIDriver Device comes from the on-board resources that implement many of the Device capabilities, thereby freeing the host processor to perform other tasks. The speed of these resources allow the Device to simultaneously process all ARINC 429 channel features including transmit scheduling, receive filtering, and monitoring of bus activity. Additionally, the Device manages the interface to the host, arbitrating access to the on-board memory, and provides low-level ARINC 429 encoding and decoding. The on-board memory contains all configuration data structures described in the following sections.

All exchanges between the host and ARINC 429 databuses are buffered by various data structures in the Device memory. The host writes messages for transmission to designated data structures, and the Device transmits them according to its configuration. Similarly, the Device receives messages from the ARINC 429 databus(es) and stores them in its memory. The host can then read the received messages from designated locations. User software accesses these structures through calls to API functions.

The figure below provides an overview of the flow of messages between the primary data structures in the Device. These data structures and their interactions are explained in detail in following sections. It will be helpful to refer back to this diagram.

![](_page_17_Figure_6.jpeg)

*Figure 3.1—Message Flow Between Primary Data Structures in Typical 429 Device*

### <span id="page-17-3"></span><span id="page-17-2"></span>**3.2 Message Records**

Message Records are used by both receive and transmit channels to buffer incoming and outgoing messages. This was illustrated in the example programs in Chapter [2.](#page-8-0) A Message Record structure (see the figure below) includes space for additional information that may be used if the corresponding options are enabled when the Device is configured. By default, only the Reserved, Activity, and Message fields are used. The Device updates the fields continuously while it is activated (i.e., between BTICard\_CardStart and BTICard\_CardStop). The rest of this section describes these data fields. (Note that some options are mutually exclusive.) Sections [3.3](#page-20-3) and [3.4](#page-21-1) describe how the receive and transmit channels use Message Records.

![](_page_18_Figure_3.jpeg)

*Figure 3.2—Message Record Structure*

## <span id="page-18-2"></span><span id="page-18-0"></span>*3.2.1 Reserved*

<span id="page-18-1"></span>The reserved words may be used by internal processes and are not intended for end users.

### *3.2.2 Activity*

The Activity word provides a variety of useful information about the message. The following describes the fields within the Activity word:

Ch #: The channel number is an eight-bit field identifying the source or destination channel of a message according to the Device channel numbering. The Device senses the channel and automatically fills in this value, which is in the range of 0 to 255.

SPD: This bit indicates the speed of the channel. A zero indicates low speed (12.5 Kbps) and a one indicates high speed (100 Kbps).

ERR: This bit indicates an error was detected in a word, and only has meaning for received words. It is the logical OR of GAP, PAR, LONG, BIT, and TO.

GAP: Indicates that the word was not preceded by a gap of at least four bit times. This bit only has meaning for received words.

PAR: Indicates that the word was received with a parity error. This bit only has meaning for received words.

LONG: Indicates that the word was received with more than 32 bits, of which only 32 bits are represented in the Message field. This bit only has meaning for received words.

BIT: Indicates that the word was received with some kind of timing error in at least one bit. This bit only has meaning for received words.

TO: Indicates that a time-out occurred, probably caused by a short word (less than 32 bits) or a noise burst that looked like the start of a word. This bit only has meaning for received words.

HIT: Indicates that the Message Record has been processed by either transmission or reception of a message, so this bit is normally set when the Device is operating. The BTI429\_MsgIs-Accessed function returns the value of the Hit bit and then clears it. When the Hit bit is set, the user knows the message has been processed at least once since the previous call to BTI429\_MsgIsAccessed. This bit has meaning for both received and transmitted words.

### <span id="page-19-0"></span>*3.2.3 ARINC Word*

The ARINC word is the 32-bit data value of the ARINC 429 message. Bits seven through zero contain the label. See Chapter [4](#page-37-0) for a complete interpretation of an ARINC 429 message.

### <span id="page-19-1"></span>*3.2.4 List Buffer Pointer*

When a List Buffer is associated with a Message Record, the ARINC word field is replaced with a pointer to the List Buffer. See Section [3.7](#page-27-0) for more information on List Buffers.

### <span id="page-19-2"></span>*3.2.5 Time-Tag*

The time-tag is a 64-bit value derived from an internal clock. The resolution and range of the time-tag is dependent on the Device and its capabilities. For example, some Devices can be configured to use binary time values or BCD IRIG based time values (see BTICard\_TimerStatus for more information). Also, on some Devices the resolution of time-tag values may be adjusted with the BTICard\_TimerResolution function. Please see the BTICard\_Timer?? and BTICard\_IRIG?? function synopses in Chapte[r 5](#page-164-0) for more information on Device dependencies.

Time-tag location is generation-dependent (refer to Chapter [6\)](#page-291-0). For 3G and 4G Devices (when using the binary timer), the time-tag of a transmitted word represents when the word is loaded into the encoder, *not* when the word is actually transmitted. Similarly, the time-tag of a received word represents when the word was read from the decoder. Thus, the time-tag can deviate slightly from the actual time of the bus activity. For 5G, 5G+, and 6G Devices, the time-tag represents when the word is actually transmitted and received, thus these time-tags represent the actual time of the bus activity. 5G Devices will time-tag at the end of a transmitted or received message, whereas 5G+ and 6G Devices will time-tag at the beginning of a transmitted or received message.

### <span id="page-19-3"></span>*3.2.6 Hit Counter*

This number is incremented every time the Message Record is accessed by the Device, so it represents the number of times a message has been received or transmitted. This option may be enabled for all messages on the channel by BTI429\_ChConfig or for a single message by BTI429\_MsgCreate, BTI429\_FilterDefault, and BTI429\_FilterSet. Since the Hit Counter uses the same field as the time-tag, the Hit Counter may not be used concurrently with any of the timerelated fields.

### <span id="page-20-0"></span>*3.2.7 Elapsed Time*

The Elapsed Time is the most recent transmit interval (i.e., the time between the two most recent receptions or transmissions of a particular message.) The Device calculates this value by subtracting the previous time-tag from the current time-tag. Resolution of the Elapsed Time is the same as the time-tag.

### <span id="page-20-1"></span>*3.2.8 Min/Max Elapsed Time*

The Minimum and Maximum Elapsed Times are the worst case Elapsed Times since the Device was last activated. The Device calculates the Elapsed Time, and if it is not between the current minimum and maximum, the appropriate value is updated. The Elapsed Time and Min/Max Elapsed Time options are enabled by the BTI429\_ChConfig function. This function also initializes the minimum time and the maximum time to zero.

### <span id="page-20-2"></span>*3.2.9 Decoder Gap*

For Message Records associated with a receive channel, the decoded inter-word gap time preceding the current message is saved. This gap time has a resolution of one-half bit times, and has a maximum range of eight bit times. This is useful for analyzing the measured gap time if a decoder GAP error occurs (as discussed in Section [3.2.2\)](#page-18-1).

### <span id="page-20-3"></span>**3.3 Filter Tables**

Each receive channel has a Filter Table, as shown in the following figure. A Filter Table is an array of pointers to Message Records. It contains one pointer for every label/SDI combination (256 x 4 pointers). The label/SDI bits of an ARINC word form an index into the Filter Table.

### <span id="page-20-4"></span>*3.3.1 How Filter Tables Work*

When a word is received from the ARINC 429 databus, the Device examines the label and SDI bits and retrieves the corresponding pointer from the Filter Table. If the pointer is zero, the Device quits processing the word. Otherwise, the Device writes the word to the Message Record to which the Filter Table points for that label/SDI. Other processing may follow depending on which options are enabled. For example, if the Elapsed Time option is enabled, the Device calculates the time elapsed since the last reception of the word and writes this to the Message Record.

![](_page_20_Figure_12.jpeg)

<span id="page-20-5"></span>Figure 3.3—Filter Tables

### <span id="page-21-0"></span>*3.3.2 Configuring the Filter Tables*

The user sets Filter Table entries to point to Message Records with the BTI429\_FilterSet and BTI429\_FilterDefault functions. BTI429\_ChConfig sets up the Filter Table and therefore must precede any calls to BTI429\_FilterSet or BTI429\_FilterDefault. Any number of Filter Table entries may point to the same Message Record. For example, the SDIALL constant used in the BTI429\_FilterSet function sets the pointers for all four SDIs of a particular label to the same Message Record, as shown for label 376 in the above figure.

### <span id="page-21-1"></span>**3.4 Transmit Schedules**

A Schedule controls the transmission of words onto the ARINC 429 databus and is executed by the Device firmware. There are two ways to create a Schedule. Chapter [2](#page-8-0) demonstrated the easiest way: using the BTI429\_SchedBuild function to automatically construct a Schedule. This section describes the Schedule in more detail and tells how to explicitly construct a Schedule. 5G+ and 6G Devices can be configured for Playback Mode as well as Schedule Mode (refer to Chapter [6](#page-291-0) for Device generation). Refer to Section [3.8](#page-30-0) for more information about Playback Mode.

### <span id="page-21-2"></span>*3.4.1 How Schedules Work*

The Schedule must be loaded by the host processor into the Device's on-board memory as a series of Command Blocks. Typically, each transmit channel is allocated 512 Command Blocks as shown in the below figure. Each Command Block contains one of the opcodes shown in the table below.

![](_page_21_Figure_8.jpeg)

<span id="page-21-3"></span>*Figure 3.4—Transmit Schedule*

| Opcode Name      | Function             | Description                                                       |  |
|------------------|----------------------|-------------------------------------------------------------------|--|
| MESSAGE          | BTI429_SchedMsg      | Transmits the ARINC 429 word from the Message Record              |  |
| GAP              | BTI429_SchedGap      | Inserts a timed gap between transmissions (allows asynch msgs)    |  |
| BTI429_SchedData |                      | Transmits a ARINC 429 word and gap without a Message Record       |  |
| DATA             |                      | (Device Dependent)                                                |  |
| ENTRY            | BTI429_SchedEntry    | Indicates the starting point for the Schedule                     |  |
| HALT             | BTI429_SchedHalt     | Halts the Schedule                                                |  |
| PAUSE            | BTI429_SchedPause    | Halts processing of Schedule until BTI429_ChResume is called      |  |
| RESTART          | BTI429_SchedRestart  | Restarts the Schedule at the beginning                            |  |
| LOG              | BTI429_SchedLog      | Generates Event Log List entry (see Section 3.10.1)               |  |
| BRANCH           | BTI429_SchedBranch   | Jumps to specified Command Block and resumes execution.           |  |
| CALL             | BTI429_SchedCall     | Jumps to specified Cmd Block, saving the return address on stack. |  |
| RETURN           | BTI429_SchedReturn   | Returns to address following last call.                           |  |
| NOP              | BTI429_SchedNop      | No operation.                                                     |  |
| PULSE            | BTI429_SchedPulse    | Pulse an external discrete signal                                 |  |
| LISTGAP          | BTI429_SchedGapList  | Inserts a conditional timed gap for asynchronous messages         |  |
| FIXEDGAP         | BTI429_SchedGapFixed | Inserts a timed gap that does not allow asynchronous messages     |  |

*Table 3.1 —Command Blocks in the Transmit Schedule*

<span id="page-22-0"></span>Though very complex Schedules may be created using the special commands listed in the above table, the typical Schedule consists of a loop of MESSAGE and GAP Command Blocks. When the Device processes a MESSAGE Command Block, it retrieves the ARINC 429 word to be transmitted from the Message Record pointed to by the operand in the Command Block (see the figure shown above). The Device loads the word into the encoder and may update fields of the Message Record, depending on which options are enabled for that message. It then proceeds to the next Command Block.

A GAP Command Block triggers transmission of the current contents of the encoder and specifies the period the transmitter must wait before starting another transmission.

A DATA Command Block (Device Dependent) directly stores the data to transmit instead of the address of a message. A DATA Command Block contains an ARINC 429 word, the number of bits (of the word) to transmit, and a following gap. This Command Block is useful for simple data transmissions by eliminating the overhead of a Message Record. As such, activity and time-tag information must be accessed from Sequential Monitor records rather than a Message Record. A Schedule is required for any transmission from a Device operating in Schedule Mode. Even if the Device is to transmit only one word one time, a Schedule must be created. To transmit one word at a time, the Schedule could consist of a MESSAGE Command Block followed by a dummy GAP Command Block and a HALT Command Block.

A few rules must be followed when developing a Transmit Schedule:

- 1. A MESSAGE Command Block should be followed by either another MESSAGE Command Block or a GAP Command Block. No transmission of the message occurs until a subsequent MESSAGE or GAP Command Block is processed.
- 2. The parameter in a GAP Command Block is the total time from the end of one message to the start of the next messages. The gap is measured in bit times at the speed set for that channel. For long gap times, GAP Command Blocks may be strung together.
- 3. If a MESSAGE Command Block follows another MESSAGE Command Block, then a 4 bit time gap is automatically inserted between the two messages.
- 4. There is an implicit RESTART at the end of every Schedule, so the Schedule runs in a loop unless directed otherwise (e.g., by a HALT Command Block).
- 5. A subroutine (the destination for any CALL or BRANCH*)* should precede the use of the CALL or BRANCH in the Schedule. The main starting point should follow subroutines and is indicated by the ENTRY Command Block. Subroutines are not supported by all Devices. Please refer to the Device Dependency section of the scheduling functions in Chapter [4](#page-37-0) for details.
- 6. As previously mentioned, a DATA Command Block does not update any Message Records. Thus, activity and time-tag information may be accessed by configuring the Transmitter Channel to record the entire channel in the Sequential Monitor. Alternatively, an ARINC 429 Receiver Channel could be used to update the Message Record in a Filter Table. This is because ARINC 429 messages from MESSAGE and DATA Command Blocks appear the same on the databus.

### <span id="page-23-0"></span>*3.4.2 Creating a Schedule*

The Schedule is programmed explicitly using BTI429\_SchedMsg**,**

BTI429\_SchedGap, and other scheduling functions shown in Table 3.1 . Each of these functions makes an entry into the next available Command Block at the end of the current Schedule. The opcode in the Command Block corresponds to the name of the function, and the operand is specified in the function parameters. Only the MESSAGE, GAP, and DATA opcodes specifically relate to the transmission of ARINC 429 words. The other opcodes control the processing of the Schedule. The details of each scheduling function, as well as its Device dependency, is explained in Chapter [4.](#page-37-0) The following section demonstrates the use of the scheduling functions.

## <span id="page-23-1"></span>*3.4.3 Example of a Schedule with Explicit Timing*

The transmit example in Chapter [2](#page-8-0) used BTI429\_SchedBuild to automatically construct the transmit Schedule required to meet given timing requirements. In contrast, this example specifies timing explicitly. Additionally, it verifies the transmitter's timing performance. It configures a channel to receive data from the transmitter through its internal, wraparound, self-test feature and records the minimum and maximum transmit intervals.

For this example, suppose that we want to transmit the two messages shown in the table below. We choose target transmit intervals that are the approximate averages of the minimum and maximum transmit intervals given in the ARINC 429 specification. A transmit Schedule that meets these timing requirements (accounting for word length and interword gaps) is shown in the figure below. If transmission of this sequence of words and gaps is repeated, the timing requirements for each word will be met. At slow speed (12.5 Kbps), a bit time is 0.08 ms, and at high speed (100 Kbps), it is 0.01 ms, so each word takes either 2.56 ms or 0.32 ms.

<span id="page-23-2"></span>

| ARINC Word<br>(octal label) | Minimum<br>Transmit Interval | Maximum<br>Transmit Interval | Target Interval |
|-----------------------------|------------------------------|------------------------------|-----------------|
| Computed Airspeed (206)     | 62.5 ms                      | 125 ms                       | 90 ms           |
| Altitude Rate (212)         | 31.3 ms                      | 62.5 ms                      | 45 ms           |

*Table 3.2 —Example Transmit Intervals*

![](_page_24_Figure_2.jpeg)

*Figure 3.5—Transmit Schedule for Low-Speed Timing Requirements of Table 3.2*

<span id="page-24-0"></span>The code for the following example begins by creating message structures. MSGSTRUCT429 has members for the address of the Message Record and the value of the data; it is used as in previous examples. We will transmit from two Message Records, and we will receive these transmissions into a different pair of Message Records. MSGFIELDS429 is a structure with members for each of the fields in a Message Record. Its use is explained later.

After obtaining the handle of the core with BTICard\_CardOpen and BTICard\_CoreOpen we use BTI429\_ChConfig to configure a receive channel (CH0) with the Min/Max Elapsed Time enabled. The Min/Max Elapsed Time option is explained in Section [3.2.8.](#page-20-1)

Next, with the BTI429\_FilterSet functions, we allocate Message Records in which to save messages for the given labels. As in the examples of Chapter [2,](#page-8-0) we are ignoring SDI bits.

The next BTI429\_ChConfig configures a transmit channel (CH4) with the self-test option enabled. The self-test option sends the output from CH4 back into all of the receive channels. Only one transmit channel should be connected to the self-test bus at any given time. BTI429\_MsgCreate is used to create the two messages to be transmitted.

```
HCARD hCard;
HCORE hCore;
MSGSTRUCT429 xmt_airspeed, xmt_altitude, rcv_airspeed, rcv_altitude;
MSGFIELDS429 airspeed_record, altitude_record;
INT cardnum = 0; //Assumes only one Device has been installed
INT corenum = 0; //Assumes only one core on the Device
BTICard_CardOpen(&hCard,cardnum); //Open the Device
BTICard_CoreOpen(&hCore,corenum,hCard); //Open each core
BTICard_CardReset(hCore); //Reset each core
//Configure receive channel and filters
BTI429_ChConfig(CHCFG429_MAXMIN,CH0,hCore);
rcv_airspeed.addr = BTI429_FilterSet(MSGCRT429_DEFAULT,0206,SDIALL,CH0,hCore);
rcv_altitude.addr = BTI429_FilterSet(MSGCRT429_DEFAULT,0212,SDIALL,CH0,hCore);
//Configure transmit channel and create messages
BTI429_ChConfig(CHCFG429_SELFTEST,CH4,hCore);
xmt_airspeed.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
xmt_altitude.addr = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
//Create the transmit Schedule
BTI429_SchedMsg(xmt_altitude.addr,CH4,hCore);
BTI429_SchedMsg(xmt_airspeed.addr,CH4,hCore);
BTI429_SchedGap(495,CH4,hCore);
BTI429_SchedMsg(xmt_altitude.addr,CH4,hCore);
BTI429_SchedGap(531,CH4,hCore);
//Initialize the message records from which the Device will transmit 
BTI429_MsgDataWr(0x0000008A,xmt_altitude.addr,hCore);
BTI429_MsgDataWr(0x00000086,xmt_airspeed.addr,hCore);
```
<span id="page-24-1"></span>*Figure 3.6—Example with Explicit Transmit Schedule (Continued on next page)*

*(Continued from previous page)*

```
BTICard_CardStart(hCore);
```

```
//Read and print min/max times
while (!done)
{
 BTI429_MsgBlockRd(&airspeed_record,rcv_airspeed.addr,hCore);
 BTI429_MsgBlockRd(&altitude_record,rcv_altitude.addr,hCore);
 printf("Altitude max interval: %u \n", altitude_record.maxtime);
 printf("Altitude min interval: %u \n", altitude_record.mintime);
 printf("Airspeed max interval: %u \n", airspeed_record.maxtime);
 printf("Airspeed min interval: %u \n", airspeed_record.mintime);
}
BTICard_CardStop(hCore); //Stop each core
BTICard_CardClose(hCard); //Close the Device
```
*[Figure 3.6—Example with Explicit Transmit Schedule](#page-24-1) (continued)*

The BTI429\_SchedMsg and BTI429\_SchedGap functions create the Schedule using the sequence of messages and gaps shown in [Figure 3.5.](#page-24-0) Notice that the minimum (4-bit) interword gap need not be explicitly scheduled. In addition, the time parameter of the BTI429\_SchedGap function is in units of bit times. At low speed, a bit time is 0.08 ms.

The BTI429\_MsgDataWr function initializes the values to be transmitted for altitude and airspeed rate. The octal labels 206 and 212 translate to hexadecimal 86 and 8A, respectively. We do not care what the data values are for this example, so we have zeroed all but the label bits. After BTICard\_CardStart commands the core to begin operating, the two messages are transmitted in accordance with the Schedule. Meanwhile, the receiver records these transmissions along with the minimum and maximum times between successive receptions of each message.

Within the while loop we use BTI429\_MsgBlockRd to read each Message Record into the application. A pointer (e.g., & airspeed\_record) to the destination structure (MSGFIELDS429), which we declared at the beginning of the code, is a parameter of this function. The minimum and maximum elapsed time values are members of that structure as shown in [Figure 3.2.](#page-18-2) The printf functions display the measured ranges of the transmit intervals, which should correlate very closely to our expected values.

The core is stopped using BTICard\_CardStop**.** The min/max measurements can be read from the core before or after calling BTICard\_CardStop. Schedules involving more messages may be implemented the same way, though, as mentioned in Chapter [2,](#page-8-0) calculating the timing requirements becomes substantially more difficult. Other actions, such as Event Log List entries, may also be scheduled.

### <span id="page-25-0"></span>**3.5 TSM: Theory of Operation**

Systems with high reliability and fidelity requirements need system-level time synchronization to reduce the effort involved to compare event timing across interconnected devices. In a distributed system, each device generally contains a local oscillator to clock digital logic circuitry, including that of the local timing source. Crystal oscillators naturally 'drift' from their fundamental frequencies over time and are also subject to aging, temperature and radiation effects. Each crystal oscillator will drift from its fundamental frequency uniquely, leading to situations for example, where two oscillators in a system having the 'same' frequency may be slightly off in different directions from the fundamental: one fast and one slow. In this event, one device will increment its timer too quickly, and the other will increment its timer too slowly, relative to the norm. Left un-managed over several seconds this may lead to measurable difference in the time between the two devices of the system introducing uncertainty in event and error timestamping.

To counter this effect, system designers typically use a single source to periodically broadcast system time. Each device of the system will load the received time at a timing mark and then increment until the next time broadcast is received. This approach reduces the possible system time difference to a function of the time broadcast period.

A drawback to this approach, however, is that non-monotonically increasing timestamps can be generated by devices receiving broadcast time, especially in systems with high-resolution timestamping. If the local oscillator is 'fast' compared to the system time source, the device time will be ahead of the system time. When the broadcast time is received the device time will 'jump' back to the slower system time. If two timestamps are generated by the device, occurring on each side of the timing mark, the later timestamp can actually show a lower time value than the earlier timestamp. Inconsistent timestamps can increase debug and validation efforts as additional energy is needed to understand event sequences.

To solve this problem, Astronics AES introduces the Timing Synchronization Manager (TSM) in the OmniBus II family products. The TSM provides the ability to synchronize time between an external reference (IRIG PCM, IRIG AM, PPS) and the protocol Cores on the OB2 family product. Additionally, using one of the above references or a 10MHz input, the TSM can adjust the frequency of the local clock oscillator up to 425ppm to reduce clock drift between the OBB2 and the external reference. Use of the drift control functionality can help increase synchronization between the OB2 family Core Timer and the external reference during the gap between timing samples. The TSM can adjust for not only the clock frequency differences (drift), but also can adjust the frequency of the local clock to account for the time differences. Instead of 'jumping' to the new broadcast time, introducing discontinuities in timestamps, the TSM can adjust the local clock frequency such that the time delta is minimized at the next timing interval.

The TSM can be the source of system time by calling BTICard\_TSMSourceConfig and passing the appropriate flags for IRIG PCM, IRIG AM, PPS or 10MHz. The TSM can also be a sync for system time by calling BTICard\_TSMSyncConfig, which takes arguments for both time and drift. The TSM can synchronize time and drift to some combinations of different sources.

The TSM also provides numerous configuration options. See BTICard\_TSM\* functions in Chapter [5](#page-164-0) for details.

### <span id="page-26-0"></span>**3.6 Sequential Monitor**

The Sequential Monitor records a time-tagged history of selected ARINC 429 messages transmitted and received by the core. This history is stored in a buffer called the Sequential Record. There is one Sequential Record per core, so even if the core supports multiple channels of the same or different protocols, all messages associated with the core are stored in the one Sequential Record (see Chapter [4](#page-37-0) for more information). Individual channels and/or individual messages within a channel may be selectively recorded. The filtering of desired messages is controlled as described in Section [2.6.](#page-12-0) By default, recording halts when the on-board Sequential Record is full. This happens in order to prevent unread data from being overwritten when the host gets behind in reading the data from Sequential Record. If the host continues to read from the Sequential Record, then it will not become full and therefore not halt recording. An alternate mode for the Sequential Monitor is to be configured with the SEQCFG\_CONTINUOUS flag in BTICard\_SeqConfig**,** in which recording is not halted. In this mode, it automatically wraps around and continues recording, overwriting old messages. This mode is useful to keep a history of the most recently monitored data in the Sequential Record rather than removing it from the Device while it is running. The core can log an entry in the Event Log List when the Sequential Record either halts or wraps around (depending on the selected option). Alternatively, the core can log an entry on every n th message recorded into the Sequential Record. (See Section [3.10.1](#page-35-2) for more information on enabling entries into the Event Log List.)

The Sequential Monitor records data to the Sequential Record only while the core is running (BTICard\_CardStart). However, while the core is running, the Sequential Monitor can be stopped and restarted without affecting other core functions. BTICard\_SeqStart is used to start the Sequential Monitor; it also stops and initializes it if necessary before restarting it. BTICard\_SeqStop stops data from being added to the Sequential Record. If BTICard\_SeqResume is called after BTICard\_SeqStop, data recording continues and the original data is not lost.

Head and tail pointers are used to keep track of the location of the most recently entered data and the oldest data that needs to be read. When the core adds a message to the Sequential Record, it updates the head pointer; when the host reads the contents of the Sequential Record, the tail pointer is updated. The Sequential Record may be read using any one of a family of functions: BTICard\_SeqRd reads a single record at a time, while BTICard\_SeqBlkRd and BTICard\_Seq-CommRd read as many records as they can (except for BUSBox BB1xxx series Devices). Note that in the special case of BUSBox BB1xxx series Devices, all three functions read as many records as they can and produce similar results. As long as one of these functions is used to read the Sequential Record, the head and tail pointers are automatically maintained.

Two additional options control the contents of the Sequential Monitor: Interval mode and Delta mode. In Interval mode, the core only records the first instance of a message in successive intervals of time (as illustrated in the figure below). In Delta mode, an entry is added to the Sequential Record for a message only when the data changes. Both options are Device dependent and are enabled by including the proper flags in the BTICard\_SeqConfig function. Please refer to Chapte[r 5](#page-164-0) for details.

![](_page_27_Figure_5.jpeg)

### <span id="page-27-1"></span><span id="page-27-0"></span>**3.7 List Buffers**

By providing a separate location for every label and SDI combination, the Message Record array simplifies retrieving received data and updating transmitted data. However, each Message Record holds only one ARINC word. Alternatively, List Buffers can be used to store sequences of words. Lists Buffers may be used to buffer a changing receive message, to transmit a predefined sequence of words, or to support file transfer protocols such as ARINC 615.

A List Buffer may service messages on either a receive or a transmit channel. When a List Buffer is associated with a message, the ARINC word field in the Message Record (shown in [Figure 3.2\)](#page-18-2) is replaced with a pointer to the List Buffer. The different types of List Buffers have the same structure (see the figure below), but operate very differently as described in the following sections. Example programs that use list buffering may be found on the distribution disk.

List Buffers are defined by the user using the BTI429\_ListRcvCreate, BTI429\_ListXmtCreate, and BTI429\_ListAsyncCreate functions. The size of each List Buffer is measured in the number of ARINC words it holds. The number of List Buffers that can be allocated and the size of each are limited only by available Device memory.

### <span id="page-28-0"></span>*3.7.1 Receive List Buffers*

There are two types of List Buffers (FIFO and ping-pong) that can be associated with a receive message. The type is specified using predefined constants when the list is created with BTI429\_ListRcvCreate.

![](_page_28_Figure_4.jpeg)

*Figure 3.8—Receive and Transmit List Buffer Structure*

#### <span id="page-28-2"></span>**FIFO List Buffer:**

When the value of received data in a particular label/SDI is rapidly changing and it is important not to lose any of the data, then a FIFO receive List Buffer may be used. This may be especially true in communications or file transfer protocols such as ARINC 615 where there are streams of words with identical label fields separated by minimum (4-bit) gap times. Thus, a Device participating in such a protocol must be capable of buffering long sequences of words.

The Device adds messages to a FIFO List Buffer as they are received, and the user retrieves them sequentially with the BTI429\_ListDataRd function. The user is not required to maintain the pointers as long as the BTI429\_ListDataRd function is used to access the buffer. The pointers are automatically changed when a new word is entered into the list (i.e., when it is received) and when a word is read from the list using BTI429\_ListDataRd. If messages are not read from the receive List Buffer fast enough, the buffer wraps around and overwrites old messages. The Device can generate an Event Log List entry to signal this occurrence.

### **Ping-Pong List Buffer:**

The ping-pong List Buffer guarantees data integrity by preventing a problem that can occur when only a single buffer is used on certain Devices. The problem happens when the host computer and the onboard processor simultaneously access the same message, causing the data being read by the host to contain part of the old message and part of the new message. The ping-pong List Buffer solves this problem by using multiple memory locations, so that BTI429\_ListDataRd always reads the most recent complete copy of a received message. Data integrity is guaranteed by some Devices when BTI429\_MsgCommRd is used, and therefore those Devices do not support ping-pong list buffers. Please refer to BTI429\_ListRcvCreate in [4](#page-37-0) for details.

### <span id="page-28-1"></span>*3.7.2 Interrupts*

If your Device supports hardware interrupts, you can configure it to issue a hardware interrupt each time an entry is made in the Event Log List, which virtually becomes an interrupt log list.

Using hardware interrupts requires an interrupt service routine and an understanding of the computer's operating system. The BTICard\_IntInstall function is used to enable the hardware interrupt and to associate the interrupt service routine with the interrupts from a core. To identify the source of the interrupt, the interrupt service routine calls and analyzes the response of the BTICard\_EventLogRd function, just as it did when polling in the previous section. Before returning, the interrupt service routine must call BTICard\_IntClear to clear the hardware interrupt. More discussion may be found under BTICard\_IntInstall in Chapter [5.](#page-164-0) Also, see the interrupt examples on the software distribution disk.

## <span id="page-29-0"></span>*3.7.3 Scheduled Transmit List Buffers*

There are three types of List Buffers (FIFO, ping-pong, and circular) that may be associated with scheduled transmit messages. A scheduled transmit List Buffer is attached to a Message Record and is created using BTI429\_ListXmtCreate. The type is specified using predefined constants. The user writes words sequentially to the List Buffer using the BTI429\_ListDataWr function. The user is not required to maintain the pointers as long as the BTI429\_ListDataWr function is used. Transmission timing is controlled by the transmit Schedule. When the Schedule indicates that a message should be transmitted, the next message in the list is used.

### **FIFO List Buffer:**

Whenever the Schedule indicates that a word should be transmitted from a Message Record associated with a FIFO List Buffer, the next available word is obtained from the FIFO List Buffer and transmitted by the Device. If messages are not updated fast enough by the host and all words have been transmitted at least once, then the last (most recent) word written by the host to the FIFO List Buffer is the word that is transmitted until another word is written by the host. The Device can generate an Event Log entry to signal that more data needs to be written by the host.

#### **Ping-Pong List Buffer:**

As in receive, the transmit ping-pong List Buffer guarantees data integrity when the host computer and the Device simultaneously access the same message, which could cause the data being transmitted by the Device to contain part of the old message and part of the new message. With a ping-pong List Buffer, the Device transmits the last complete message loaded using BTI429\_ListDataWr. Data integrity is guaranteed by some Devices when BTI429\_MsgCommWr is used, and therefore those Devices do not support ping-pong list buffers. Please refer to BTI429\_ListXmtCreate in [4](#page-37-0) for details.

### **Circular List Buffer:**

With a circular List Buffer, transmissions repeatedly loop through the entire list buffer. This feature would greatly simplify the transmission of a data pattern, for example a sine wave or ramp, since the whole pattern could be preloaded into the List Buffer rather than requiring the host computer to update the data value for each transmission.

### <span id="page-29-1"></span>*3.7.4 Asynchronous Transmit List Buffers*

Although there may be other uses, asynchronous transmit List Buffers are intended to support communications protocols such as ARINC 615. Your Device is capable of transmitting the asynchronous bursts of words typically involved in communications protocols without disrupting the timing of periodic words (the regular scheduled messages). A bidirectional communications protocol would involve the use of receive List Buffers and asynchronous transmit List Buffers.

Operation of the asynchronous transmit List Buffer is very simple. When an allocated asynchronous transmit List Buffer contains words that have not been transmitted (i.e. is not empty), the Device automatically transmits words from the asynchronous transmit List Buffer by interleaving them in the gaps of a running Schedule. The Device fills any scheduled gap with as many words as will fit from the asynchronous transmit List Buffer. The gap is then adjusted to preserve the timing of scheduled messages as shown in the below figure. This architecture allows an application program to load conveniently sized packets of data from a file and let the Device automatically manage their transmission. BTI429\_ListStatus may be used to determine whether a List Buffer is empty.

Unlike other List Buffers, an asynchronous transmit List Buffer is associated with a channel, not a Message Record. To implement an asynchronous transmit List Buffer, use

BTI429\_ListAsyncCreate to create the list and associate it with a channel. Then create a transmit Schedule automatically using BTI429\_SchedBuild (Sectio[n 2.6\)](#page-12-0) or explicitly using BTI429\_Sched-Msg and BTI429\_SchedGap (Section [3.4\)](#page-21-1). If only asynchronous messages are to be transmitted and no messages are to be scheduled, then a dummy Schedule must be created explicitly with a single large gap. Once the Schedule is running, use BTI429\_ListDataWr to pass the data for transmission. The distribution disk has example programs that illustrate the use of asynchronous transmit List Buffers.

Asynchronous transmit List Buffers are FIFO buffers. Words written to the list with BTI429\_ListDataWr are transmitted only once in the order they are written. When the list is empty, no words are transmitted from the list until the host writes new words to it. As with other List Buffers, the user is not required to maintain the pointers as long as BTI429\_ListDataWr is used to add words to the list.

![](_page_30_Figure_5.jpeg)

*Figure 3.9—Operation of an Asynchronous Transmit List Buffer*

### <span id="page-30-3"></span><span id="page-30-0"></span>**3.8 Playback Mode**

5G+ and 6G Devices may be configured for Playback Mode rather than Schedule Mode (refer to Chapter [6](#page-291-0) for Device generation). Schedule Mode is beneficial in transmission applications requiring large amounts of data, Message Records, and a circular/repeating schedule. Refer to Section [3.4](#page-21-1) for more information about Schedule Mode. Playback Mode is beneficial in transmission applications requiring more user interaction such as data loaders, file transfers, ARINC 615, and monitor/playback debugging. For example, ARINC 429 traffic may be collected in a sequential record and then transmitted later (playback) for post analysis.

### <span id="page-30-1"></span>*3.8.1 Playback Configuration*

Playback Mode configures the Transmit Channel as a first-in/first-out (FIFO) buffer of Command Blocks rather than a circular loop of scheduled Command Blocks. In Playback Mode, all accesses to the card are performed through Play functions rather than Sched functions. These functions are directly tied to the mode configuration and should not be interchanged.

BTI429\_ChConfig configures the Transmit Channel for Playback Mode when directed by CHCFG429\_PLAYBACK. This creates a Playback FIFO in the on-board memory capable of buffering Command Blocks. Each Transmit Channel has a dedicated Playback FIFO, and each Command Block in the FIFO is transmitted only once unlike the circular nature of Schedule Mode.

### <span id="page-30-2"></span>*3.8.2 Playback Operation*

Once configured, the Playback FIFO is ready to accept Command Blocks from the host processor. Command Blocks written to the Playback FIFO will be transmitted when the Transmit Channel is

running (e.g. BTICard\_CardStart), or the Command Blocks will be buffered in the Playback FIFO when the Transmit Channel is not running. Buffered Command Blocks are transmitted (in the order received) once the Transmit Channel is started, and gaps are inserted on the bus when the FIFO is empty. Command Blocks may be written and transmitted without stopping the Transmit Channel. Commands Blocks may only be written to the on-board Playback FIFO with BTI429\_PlayBlockWr, which transfers a buffer of Command Blocks from the host processor to the Playback FIFO with highthroughput performance. Command Blocks are individually put in the host processor's buffer (not the Playback FIFO) with BTI429\_PlayPut\* commands and then collectively transferred to the Playback FIFO with BTI429\_PlayBlockWr. The Playback FIFO may be polled for status and availability with BTI429\_PlayStatus. Refer to [4](#page-37-0) for more information on Playback functions.

### <span id="page-31-0"></span>*3.8.3 Playback Example*

In this section, we describe an example program that transmits a collection of data in Playback Mode. To simplify the example, it can be assumed the various data words (*dataa*, *datab*, *datax*, etc) are set and/or updated elsewhere. This example is more focused on demonstrating the interaction between the host buffer (on the Host) and the Playback FIFO (on the Device).

The Host first allocates a buffer of Command Blocks to store the various commands. Note that all Playback Command Blocks are 8 USHORTs, therefore the Host buffer must be in multiples of 8 (CMDSIZE). The channel is then configured for Playback Mode with BTI429\_ChConfig and CHCFG429\_PLAYBACK. This creates the Playback FIFO on the Device, and the Playback FIFO is immediately capable of accepting Command Blocks.

The Host then preloads some Data Command Blocks to the host buffer. Each Data Command Block in this example will transmit 32 bits of the provided data word with a 4 bit-time following gap. The Host then preloads an Event Log Command Block. Event Logs could be used to notify the host when the Playback FIFO needs more data. Each BTI429\_PlayPut\* puts the Command Block in *playbuf* at *offset* then increments the host buffer offset after executing. Note *offset* is in units of CMDSIZE (not USHORTs), thus each BTI429\_PlayPut\* need only increment *offset* by one. The specified number of Command Blocks (same value as *offset*) are transferred from the host buffer to the Playback FIFO using BTI429\_PlayBlockWr. Again, note that the play block *count* is in units of CMDSIZE (not USHORTs).

The Playback FIFO is not processed and no data is transmitted until BTICard\_CardStart is called. Once the Card is started, it will process the Playback FIFO. The Card will add an entry to the Event Log List after starting transmission of the preceding message. When the Host reads the Event Log entry, the Host loads additional data into the Playback FIFO while the channel is still running. Command Blocks loaded into the Playback FIFO (while running) will be immediately processed and transmitted in the order queued.

```
#define CMDSIZE 8 //Each Command Blocks is 8 USHORTs
HCARD hCard;
HCORE hCore;
USHORT playbuf[CMDSIZE*512]; //Create host buffer for 512 Command Blocks
INT offset = 0; //Host buffer Command Block offset
INT cardnum = 0; //Assumes only one Device has been installed
INT corenum = 0; //Assumes only one core on the Device
USHORT type; //Event Log entry typeval
ULONG info; //Event Log entry infoval
INT chan; //Event Log entry channel
BTICard_CardOpen(&hCard,cardnum); //Open the Device
BTICard_CoreOpen(&hCore,corenum,hCard); //Open each core
BTICard_CardReset(hCore); //Reset each core
BTICard_EventLogConfig(LOGCFG_ENABLE,256,hCore); //Initialize Event Log
//Configure transmit channel for Playback Mode
BTI429_ChConfig(CHCFG429_PLAYBACK,CH4,hCore);
//Preload some data
offset = 0; //Set offset to start of host buf
BTI429_PlayPutData(dataa,32,4,offset++,playbuf); //Put data in host buf
BTI429_PlayPutData(datab,32,4,offset++,playbuf); //Put data in host buf
BTI429_PlayPutData(datac,32,4,offset++,playbuf); //Put data in host buf
BTI429_PlayPutLog(COND429_ALWAYS,0,offset++,playbuf);//Put log in host buf
BTI429_PlayBlockWr(playbuf,offset,CH4,hCore); //Send host buf to Device
//Start card to transmit FIFO contents
BTICard_CardStart(hCore);
while (!done)
{
 //Event Log will notify Host to load more data
 if(BTICard_EventLogRd(&type,&info,&chan,hCore) && (chan==CH4))
 {
 //Send more data to Playback FIFO once Device transmits previous data
 offset = 0;
 BTI429_PlayPutData(datax,32,4,offset++,playbuf);
 BTI429_PlayPutData(datay,32,4,offset++,playbuf);
 BTI429_PlayPutData(dataz,32,4,offset++,playbuf);
 BTI429_PlayPutLog(COND429_ALWAYS,0,offset++,playbuf);
 BTI429_PlayBlockWr(playbuf,offset,CH4,hCore);
 }
}
BTICard_CardStop(hCore); //Stop the core
BTICard_CardClose(hCard); //Close the Device
```
*Figure 3.10—Example of Simple Playback Data Transmit*

## <span id="page-32-2"></span><span id="page-32-0"></span>**3.9 Error Injection**

In some situations, a user may want to evaluate a receiver's response to protocol errors in a transmitted message. For such situations, the user can selectively direct the Device to inject a variety of errors.

## <span id="page-32-1"></span>*3.9.1 Parity Errors*

ARINC 429 specifies that messages (32-bit words) should be transmitted with odd parity, so the Device defaults to odd parity. However, any transmit or receive channel on the Device can be configured using BTI429\_ChConfig so the parity is odd, even, or used as data. When used as data, the parity circuits do not generate or check parity. Also, transmit channels and transmit messages can be made to invert the parity bit (i.e., send a parity error) using other constants in BTI429\_ChConfig and BTI429\_MsgCreate. Please refer to the function descriptions in Chapter [4](#page-37-0) for the appropriate constants to set these options.

## <span id="page-33-0"></span>*3.9.2 Gap Errors*

According to the ARINC 429 specification, the gap between messages should be at least four (4) bit times. This requirement is automatically met when transmit Schedules are created using BTI429\_SchedBuild or the explicit scheduling described in Section [3.4.](#page-21-1) Normally, two sequential messages in a Schedule are automatically separated by a four bit time gap. If a gap of less than four bit times is desired, then an explicit Schedule must be created by either using BTI429\_SchedMsg followed by BTI429\_SchedGap or by using BTI429\_SchedMsgEx for the message preceding the short gap. A parameter in BTI429\_SchedMsgEx specifies the length of the gap.

5G+ and 6G Device gaps default to units of bit-times; however, gaps may alternatively be configured in units of microseconds (refer to Chapter [6](#page-291-0) for Device generation). When BTI429\_ChConfig is called with CHCFG429\_GAP1US, the channel interprets all gap parameters (*gapval*) as microsecond values rather than bit-times. All functions with gap parameters are affected when operating the channel in this mode such as BTI429\_SchedGap, BTI429\_SchedMsgEx, BTI429\_SchedData, BTI429\_PlayPutGap, BTI429\_PlayPutData, etc. Refer to [4](#page-37-0) for more information on operating the channel in microsecond gap mode.

## <span id="page-33-1"></span>*3.9.3 Parametric Bit Count*

5G+ and 6G Devices are capable of transmitting short and long messages (refer to Chapter [6](#page-291-0) for Device generation). Short messages may have bit counts as low as one data bit. Additionally, a scheduled zero bit-time gap (CHCFG429\_GAPBIT mode) or zero microsecond gap (CHCFG429\_GAP1US mode) between two messages will appear as one long data word (of more than 32 contiguous bits) to an ARINC 429 receiver. By scheduling multiple messages (each separated by a scheduled zero gap), 5G+ and 6G Devices are capable of transmitting long data words with limitless bit counts. Parametric bit counts (combined with scheduled 0 gaps) may test an ARINC 429 receiver for short or long data word errors.

The following example transmits a long data word (of 65 contiguous bits) that has been implemented as 3 separate messages with zero bit-time gaps. The example simultaneously stores the data and bit count of each message with the BTI429\_MsgDataCountWr function. Messages of less than 32-bits may require more Device resource bandwidth than available (depending on loading of the Device and host interaction); therefore, larger individual message bitcounts will improve deterministic gaps. Refer to [4](#page-37-0) for more information on this function.

```
HCARD hCard;
HCORE hCore;
INT cardnum = 0; //Assumes only one Device has been installed
INT corenum = 0; //Assumes only one core on the Device
MSGADDR msgaddr[3]; //Implement 65-bit data word as 3 messages
ULONG data[3];
BTICard_CardOpen(&hCard,cardnum); //Open the Device
BTICard_CoreOpen(&hCore,corenum,hCard); //Open the core
BTICard_CardReset(hCore); //Reset the core
BTI429_ChConfig(CHCFG429_DEFAULT,CH4,hCore); //Default configuration
//Create messages
msgaddr[0] = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
msgaddr[1] = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
msgaddr[2] = BTI429_MsgCreate(MSGCRT429_DEFAULT,hCore);
//Simultaneously write data and bitcount
BTI429_MsgDataCountWr(data[0],32,msgaddr[0],hCore); //(first 32 of 65 bits)
BTI429_MsgDataCountWr(data[1],17,msgaddr[1],hCore); //(middle 17 of 65 bits)
BTI429_MsgDataCountWr(data[2],16,msgaddr[2],hCore); //(last 16 of 65 bits)
//Schedule messages with "gaps" to form one 65-bit long data word
BTI429_SchedMsg(msgaddr[0],CH4,hCore);
BTI429_SchedGap(0,CH4,hCore);
BTI429_SchedMsg(msgaddr[1],CH4,hCore);
BTI429_SchedGap(0,CH4,hCore);
BTI429_SchedMsg(msgaddr[2],CH4,hCore);
BTICard_CardStart(hCore);
while (!done)
{
 //Device repeatedly transmits a 65-bit long data word
 //followed by a default 4 bittime gap
}
BTICard_CardStop(hCore); //Stop the core
BTICard_CardClose(hCard); //Close the Device
```
<span id="page-34-2"></span>*Figure 3.11—Example of Long Parametric Message*

### <span id="page-34-0"></span>*3.9.4 Waveform Configuration*

6G Devices with parametric waveform capability provide control over transmit amplitude, offset voltage, null voltage, common-mode bias, rise time, and fall time (refer to Chapter [6](#page-291-0) for Device generation). Various functions may be used to configure the waveform parameters, so the recommended function depends on level of application complexity. [Table 3.1](#page-22-0) shows application-based levels of waveform configuration with the associated function parameter. Waveform configuration is Device-dependent, so refer to the hardware manual for Device capabilities. Please refer to the BTI429\_ParamWaveformConfigEx reference in [4](#page-37-0) for a waveform figure and a more detailed description of Waveform Configuration.

<span id="page-34-1"></span>

|                              | Amplitude           | Offset /<br>Null                | CM<br>Bias | Rise<br>Time | Fall<br>Time |
|------------------------------|---------------------|---------------------------------|------------|--------------|--------------|
| BTI429_ParamAmplitudeConfig  | dacval              | -                               | -          | -            | -            |
| BTI429_ParamWaveformConfig   | highvolt<br>lowvolt | highvolt<br>lowvolt<br>nullvolt | -          | -            | -            |
| BTI429_ParamWaveformConfigEx | highvolt<br>lowvolt | highvolt<br>lowvolt<br>nullvolt | biasvolt   | highrise     | lowrise      |

*Table 3.3 —6G Parametric Waveform Functions*

Applications that only modify transmit amplitude may use BTI429\_ParamAmplitudeConfig with the *dacval* parameter. This function will adjust the differential amplitude of the ARINC 429 transmitter while configuring default voltages/times for all other parameters. The *dacval* parameter is in differential units of 2.5mV for 6G Devices. A standard ARINC 429 waveform may be configured using a 0x0FA0 *dacval*.

Applications requiring changes to signal offset and/or null voltages should use BTI429\_ParamWaveformConfig to adjust the differential amplitude of the high state (*highvolt*), low state (*lowvolt*), and null state (*nullvolt*). A standard ARINC 429 waveform may be configured with a *highvolt* of 10000 (+10V)*,* a *lowvolt* of -10000 (-10V), and a *nullvolt* of 0 (0V). Note that *highvolt, lowvolt,* and *nullvolt* represent differential amplitude in millivolts.

Common-mode bias applications may adjust the voltages of both legs (positive and negative) of the ARINC 429 transmitter by using BTI429\_ParamWaveformConfigEx, the PARAMCFG429\_CMBIAS flag, and the *biasvolt* parameter. This parameter specifies the single-ended (not differential) voltage of both legs of the transmitter. Note that *biasvolt* represents single-ended voltage in millivolts.

Applications requiring changes to the ARINC 429 transmitter's transition time should use BTI429\_ParamWaveformConfigEx to adjust the rise-time and fall-time of various pulses. *highrise* sets the rise-time and fall-time (0%-to-100%) of the high state; whereas *lowrise* sets the rise-time and fall-time (0%-to-100%) of the low state. Both *highrise* and *lowrise* are in units of nanoseconds, and standard ARINC 429 waveforms have identical *highrise*/*lowrise* values of 1.8µs (highspeed) or 10µs (lowspeed). These parameters may be useful when choosing to preserve either slew rate or risetime/fall-time of a modified waveform. Note that the exact rise-time and fall-time depend on several factors, such as where on the databus it is measured, analog and digital delays in the onboard circuits, and other analog characteristics of the databus.

### <span id="page-35-0"></span>*3.9.5 Output State*

6G Devices with Output State capability provide control over the state of each supported transmitter's positive/negative leg (refer to Chapter [6](#page-291-0) for Device generation). This transmitter feature is useful for testing single-leg error susceptibility of other ARINC 429 receivers due to the differential nature of the ARINC 429 databus. Using BTI429\_ChOutputStateSet, each transmitter leg may be individually configured in standard operation (OUTSTATE429\_SIGNAL), open/lifted from the transmitter (OUTSTATE429\_OPEN), or shorted to ground (OUTSTATE429\_GROUND). The following table provides some example configurations:

| ChOutputStateSet pos | ChOutputStateSet neg | Transmitter State                                       |  |
|----------------------|----------------------|---------------------------------------------------------|--|
| OUTSTATE429_SIGNAL   | OUTSTATE429_OPEN     | Positive leg standard<br>Negative leg opened/lifted     |  |
| OUTSTATE429_GROUND   | OUTSTATE429_SIGNAL   | Positive leg shorted to ground<br>Negative leg standard |  |

*Table 3.4 —6G Transmitter Output State Examples*

### <span id="page-35-3"></span><span id="page-35-1"></span>**3.10 Special Events**

In some software programs, it may be necessary to know when a special event has happened. Examples of special events are when a specific message is received, when the transmit Schedule reaches a certain point, when an error occurs, or when the Sequential Record or a List Buffer needs service. An Event Log List is used to record these special events for each core. Notification that the special event has occurred can happen through polling or interrupts. If your Device supports multiple protocols, the Event Log List can contain events from more than just ARINC 429 related activity. See the description of BTICard\_EventLogRd in Chapter [5](#page-164-0) for a table of event types for different protocols.

### <span id="page-35-2"></span>*3.10.1 Event Log List*

To use an Event Log List, it is necessary to create the Event Log List and to enable the specific events that are to be recorded. The

BTICard\_EventLogConfig function creates the Event Log List for a core. To enable a specific event, use the corresponding enabling constant in the enabling function. Many BTIDriver functions can be configured to enable Event Log List entries. See the description of BTICard\_EventLogRd in Chapter [5](#page-164-0) for a table that lists the enabling function(s) for each event.

The Event Log List is a circular buffer, which records all events in order of occurrence. An entry is added to this list each time an enabled event occurs. An event is identified by reading and evaluating its entry from the Event Log List. Each Event Log List entry contains three fields. The description of BTICard\_EventLogRd in Chapte[r 5](#page-164-0) summarizes the meanings and values of these parameters.

### <span id="page-36-0"></span>*3.10.2 Polling*

When the program is running, the Event Log List may be polled using BTICard\_EventLogRd. This function returns a zero if the Event Log List is empty. Otherwise, it may be evaluated to determine the source of the event. See the description of BTICard\_EventLogRd in Chapter [5](#page-164-0) for a table that describes the Event Log List fields. Each entry in the Event Log List may be read only once, since BTICard\_EventLogRd automatically increments the list pointers each time it is called. The BTI-Card\_EventLogRd function returns the oldest entry from this list and updates the tail pointer.

# <span id="page-37-0"></span>**4. BTI429 Function Reference**

This chapter provides detailed information on the primary ARINC 429 BTIDriver functions for Ballard Devices. The descriptions and examples discussed here are intended for use with programs written in the C language. Users of other languages should contact Ballard Support for assistance.

## <span id="page-37-1"></span>**4.1 Overview of the BTIDriver API**

The general naming convention for BTIDriver functions consists of a prefix/category/action format. The functions that make up the BTIDriver library are either specific to a particular avionics databus protocol or are protocol independent. The ARINC 429–specific functions are prefixed by BTI429\_ (see [Table](#page-39-1)  [4.1\)](#page-39-1). Functions for other protocols are documented in separate manuals (See the following chapter for protocol independent functions). The functions fall into several operational categories. The initial letters of a function name after the prefix indicate the category to which it belongs (e.g., *Ch, Msg, Sched*, etc.). These initial characters are followed by an action. For example, the function BTI429\_MsgCreate is a member of the "Message" category and causes an ARINC 429 message to be created. In this chapter, the functions are listed alphabetically without regard to the prefix.

### <span id="page-37-2"></span>*4.1.1 "handle" Parameters*

Nearly all functions require a *handle* parameter. The handle is always the last parameter in any function that requires it. The first function called in a program is BTICard\_CardOpen, which returns a card handle (hCard). This card handle is passed to BTICard\_CoreOpen, which returns a core handle (hCore). Most functions then take this core handle; the only functions that require a card handle are BTICard\_CoreOpen and BTICard\_CardClose. Please refer to Section [2.4](#page-9-1) for a complete discussion of handles, cards, and cores.

### <span id="page-37-3"></span>*4.1.2 "ctrlflags" Parameters*

Many functions have a *ctrlflags* parameter. Each bit controls an option in this bitmask parameter. Constants are defined in the header file for these parameters. The name of a constant reflects the function in which it is used (e.g., CHCFG429\_DEFAULT is used in the BTI429\_ChConfig function). Option parameters are always first in the parameter list of a function that accepts them. The default options can always be selected by using the ??\_DEFAULT constant where ?? depends on the function in which it is used (e.g., CHCFG429\_DEFAULT). When multiple options are selected, the constants should be bitwise OR-ed together. The default options are shown in bold in this chapter. Since the default constants are defined as zero, only non-default constants actually need to be included in the OR-ing. The constants defined in the header file should be used by name (not value) in your code since the values are subject to change.

### <span id="page-37-4"></span>*4.1.3 Schedule Indices (SCHNDX)*

All of the scheduling functions (BTI429\_Sched??) return a value of type SCHNDX (Schedule index). These functions append the Command Block index to the Schedule. This index is a parameter of some of the advanced scheduling functions (e.g., BTI429\_SchedCall and BTI429\_SchedBranch).

### <span id="page-37-5"></span>*4.1.4 "channel" Parameters*

Some functions take a *channel* parameter to specify which ARINC 429 channel applies to the function. The header file defines the constants CH0, CH1, etc., which may be used for this purpose. Please refer to the user manual of your Ballard 429 Device or run the test program to determine which channels are receive and which are transmit.

### <span id="page-38-0"></span>*4.1.5 "message" Parameters*

Message data and related information such as the time-tag are stored in individual message structures on the Device. All of the message manipulation functions (e.g., BTI429\_MsgDataRd) require a *message* address parameter that uniquely identifies a message structure. Several different functions (e.g., BTI429\_MsgCreate**,** BTI429\_FilterSet**,** BTI429\_FilterDefault) return the message address.

The message values are 32-bit parameters specifying the value of an ARINC 429 word. The ARINC 429 word may contain a label, parity bit, SDI, SSM, and data bits. The functions expect the message value to be in a "reversed label" ARINC 429 format. The relationship between the two formats is shown below:

![](_page_38_Figure_5.jpeg)

*Figure 4.1—Standard and Reversed Label Formats of the ARINC 429 Word*

### <span id="page-38-3"></span><span id="page-38-1"></span>*4.1.6 Error Values*

Type ERRVAL functions return a negative value if an error occurs, or zero if successful. This value can be interpreted by BTICard\_ErrDescStr, which returns a string describing the specified error. This same error information may also be obtained from the header file.

## <span id="page-38-2"></span>*4.1.7 UINT16/UINT32*

BTIDriver functions can be called by applications that run on both 32-bit systems and 64-bt systems, but BTIDriver functions always assume 'short' integers are 16-bits long and 'long' integers are 32 bits long. An application running on some 64-bit operating systems (for example, Linux) assumes a 'short' integer is 16-bits long, but a 'long' integer is 64-bits long. To resolve this discrepancy and to keep types consistent, applications that call BTIDriver functions from 64-bit operating systems supported by BTIDriver should use the UINT16 type instead of USHORT, and the UINT32 type instead of ULONG.

### <span id="page-39-0"></span>**4.2 Function Summaries**

The table below summarizes the BTI429\_ functions. The functions that appear in bold in the following tables were illustrated in the body of this manual in the example programs.

| CHANNEL Functions                                |                                                                                                                            |
|--------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------|
| BTI429_ChClear                                   | Clears either a transmit Schedule or a receiver Filter Table                                                               |
| BTI429_ChConfig                                  | Configures either a transmit or a receive channel                                                                          |
| BTI429_ChGetCount                                | Gets the receiver and transmitter channel count                                                                            |
| BTI429_ChGetInfo                                 | Gets information about the specified channel on the specified core                                                         |
| BTI429_ChIs429                                   | Checks to see if the specified channel is ARINC 429                                                                        |
| BTI429_ChIsRcv                                   | Checks if the specified channel is a receiver                                                                              |
| BTI429_ChIsXmt                                   | Checks if the specified channels is a transmitter                                                                          |
| BTI429_ChOutputStateSet                          | Sets the output state of a Transmit Channel leg to Open/Ground/etc                                                         |
| BTI429_ChPause                                   | Pauses operation of a channel                                                                                              |
| BTI429_ChPauseCheck                              | Checks to see if the specified channel is paused                                                                           |
| BTI429_ChResume                                  | Resumes operation of a previously paused channel                                                                           |
| BTI429_ChStart                                   | Starts operation of a previously stopped channel                                                                           |
| BTI429_ChStop                                    | Stops operation of a channel                                                                                               |
| BTI429_ChSyncDefine                              | Defines the sync pulse settings for the specified channel                                                                  |
| BTI429_ChTriggerDefine                           | Defines the transmit trigger settings for the specified channel                                                            |
| CMD Functions                                    |                                                                                                                            |
| BTI429_CmdShotRd                                 | Reads the single-shot bit for the specified index                                                                          |
| BTI429_CmdShotWr                                 | Sets the single-shot bit for the specified index                                                                           |
| BTI429_CmdSkipRd                                 | Reads the skip bit for the specified index                                                                                 |
| BTI429_CmdSkipWr                                 | Sets the skip bit for the specified index                                                                                  |
| BTI429_CmdStepRd                                 | Reads the step bit for the specified index                                                                                 |
| BTI429_CmdStepWr                                 | Sets the step bit for the specified index                                                                                  |
| FILTER Functions                                 |                                                                                                                            |
| BTI429_FilterDefault                             | Creates a default message, and points the entire table to that message                                                     |
| BTI429_FilterRd                                  | Reads the message address associated with a filter location                                                                |
| BTI429_FilterSet                                 | Creates a message, and points the specified filter location to that message                                                |
| BTI429_FilterWr                                  | Writes a message address to the specified filter location                                                                  |
| LIST Functions                                   |                                                                                                                            |
| BTI429_ListAsyncCreate                           | Creates an asynchronous transmit List Buffer                                                                               |
| BTI429_ListDataBlkRd                             | Reads multiple messages from the List Buffer                                                                               |
| BTI429_ListDataBlkWr                             | Writes multiple messages to the List Buffer                                                                                |
| BTI429_ListDataRd                                | Reads the next data value associated with a List Buffer                                                                    |
| BTI429_ListDataWr                                | Writes the next data value associated with a List Buffer                                                                   |
| BTI429_ListRcvCreate                             | Creates a receive List Buffer                                                                                              |
| BTI429_ListStatus                                | Checks the status of the List Buffer                                                                                       |
| BTI429_ListXmtCreate                             | Creates a transmit List Buffer                                                                                             |
| MESSAGE Functions                                |                                                                                                                            |
| BTI429_MsgBlockRd                                | Reads an entire Message Record on the Device                                                                               |
| BTI429_MsgBlockWr                                | Writes an entire Message Record on the Device                                                                              |
| BTI429_MsgCountRd                                | Reads the bitcount associated with a message                                                                               |
| BTI429_MsgCountWr                                | Writes the bitcount associated with a message                                                                              |
| BTI429_MsgCommRd                                 | Reads an entire Message Record on the Device (non-contended access)                                                        |
| BTI429_MsgCommWr                                 | Writes an entire Message Record on the Device (non-contended access)                                                       |
| BTI429_MsgCreate                                 | Creates and initializes a Message Record                                                                                   |
| BTI429_MsgDataCountWr                            | Writes the data and bitcount associated with a message                                                                     |
| BTI429_MsgDataRd                                 | Reads the data associated with a message                                                                                   |
| BTI429_MsgDataWr                                 | Writes the data associated with a message                                                                                  |
| BTI429_MsgGroupBlockRd<br>BTI429_MsgGroupBlockWr | Reads multiple, entire Message Records in a single operation                                                               |
| BTI429_MsgGroupRd                                | Writes multiple, entire Message Records in a single operation                                                              |
| BTI429_MsgGroupWr                                | Reads the data from multiple Message Records in a single operation                                                         |
| BTI429_MsgIsAccessed                             | Writes the data from multiple Message Records in a single operation<br>Returns the value of the Hit bit and then clears it |
| BTI429_MsgMultiSkipWr                            | Writes the state of the skip bit for multiple messages                                                                     |
| BTI429_MsgSkipRd                                 | Reads the state of the skip bit for the specified message                                                                  |
| BTI429_MsgSkipWr                                 | Writes the state of the skip bit for the specified message                                                                 |
| BTI429_MsgSyncDefine                             | Defines the sync pulse settings for the specified message                                                                  |
| BTI429_MsgTriggerDefine                          | Defines the transmit trigger settings for the specified message                                                            |
|                                                  | Table 4.1—ARINC 429 BTI429_ functions                                                                                      |

<span id="page-39-1"></span>*(Continued on next page)*

| PARAMETRIC Functions                   |                                                                                                    |
|----------------------------------------|----------------------------------------------------------------------------------------------------|
| BTI429_ParamAmplitudeConfig            | Configures parametric amplitude control on the specified transmit channel                          |
| BTI429_ParamBitCountBuild              | Builds a paramflag field from a bitcount (for BTI429_MsgBlockWr)                                   |
| BTI429_ParamBitRateConfig              | Configures parametric frequency control on the specified channel                                   |
| BTI429_ParamWaveformConfig             | Configures parametric waveform control on the specified transmit channel                           |
| SCHEDULE Functions                     |                                                                                                    |
| BTI429_SchedBranch                     | Inserts a BRANCH command into the Schedule                                                         |
| BTI429_SchedBuild                      | Builds a Schedule based on minimum and maximum transmit intervals                                  |
| BTI429_SchedCall                       | Inserts a CALL command into the Schedule                                                           |
| BTI429_SchedData                       | Inserts a DATA Command Block into the Schedule                                                     |
| BTI429_SchedEntry                      | Sets the starting point of the Schedule                                                            |
| BTI429_SchedGap                        | Inserts an intermessage gap into the Schedule (calls either SchedGapFixed or                       |
|                                        | SchedGapList based on the configuration of an asynchronous List Buffer)                            |
| BTI429_SchedGapFixed                   | Inserts a fixed gap into the Schedule.                                                             |
| BTI429_SchedGapList                    | Inserts a conditional intermessage gap into the Schedule (used for                                 |
|                                        | asynchronous List Buffers)                                                                         |
| BTI429_SchedHalt                       | Inserts a HALT Command Block into the Schedule                                                     |
| BTI429_SchedLog                        | Inserts an LOG Command Block into the Schedule                                                     |
| BTI429_SchedMode                       | Sets options for the SchedBuild function                                                           |
| BTI429_SchedMsg                        | Inserts a message into the Schedule                                                                |
| BTI429_SchedMsgEx                      | Inserts a message with a specified gap value (of less than 4 bit times)                            |
| BTI429_SchedPause                      | Inserts a PAUSE Command Block into the Schedule                                                    |
| BTI429_SchedPulse                      | Inserts a discrete PULSE Command Block into the Schedule                                           |
| BTI429_SchedRestart                    | Inserts a RESTART Command Block into the Schedule                                                  |
| BTI429_SchedReturn                     | Inserts a RETURN command into the Schedule                                                         |
| PLAYBACK Functions                     |                                                                                                    |
| BTI429_PlayBlockWr                     | Transfers a buffer of Command Blocks from the host to the Playback FIFO                            |
| BTI429_PlayPutData                     | Inserts a DATA Command Block into a host buffer of Command Blocks                                  |
| BTI429_PlayPutGap                      | Inserts a GAP Command Block into a host buffer of Command Blocks                                   |
| BTI429_PlayPutLog                      | Inserts a LOG Command Block into a host buffer of Command Blocks                                   |
| BTI429_PlayPutPulse0                   | Inserts a PULSE0 Command Block into a host buffer of Command Blocks                                |
| BTI429_PlayPutPulse1                   | Inserts a PULSE1 Command Block into a host buffer of Command Blocks                                |
|                                        |                                                                                                    |
| BTI429_PlayStatus                      | Reads Playback FIFO Command Block capability/availability                                          |
| UTILITY Functions                      |                                                                                                    |
| BTI429_BCDGetData                      | Extracts data value from BCD word                                                                  |
| BTI429_BCDGetMant                      | Extracts mantissa from a BCD word                                                                  |
| BTI429_BCDGetSign                      | Extracts the sign from a BCD word                                                                  |
| BTI429_BCDGetSSM                       | Extracts SSM field from BCD word                                                                   |
| BTI429_BCDGetVal                       | Calculates the value of a BCD word                                                                 |
| BTI429_BCDPutData                      | Inserts data value into BCD word                                                                   |
| BTI429_BCDPutMant                      | Inserts mantissa value into BCD word                                                               |
| BTI429_BCDPutSign                      | Inserts the sign into a BCD word                                                                   |
| BTI429_BCDPutSSM                       | Inserts SSM field into BCD word                                                                    |
| BTI429_BCDPutVal                       | Inserts the value into a BCD word                                                                  |
| BTI429_BNRGetData                      | Extracts data value from BNR word                                                                  |
| BTI429_BNRGetMant                      | Extracts mantissa from a BNR word                                                                  |
| BTI429_BNRGetSign                      | Extracts the sign from a BNR word                                                                  |
| BTI429_BNRGetSSM                       | Extracts SSM field from BNR word                                                                   |
| BTI429_BNRGetVal                       | Calculates the value of a BNR word                                                                 |
| BTI429_BNRPutData                      | Inserts data value into BNR word                                                                   |
| BTI429_BNRPutMant                      | Inserts mantissa value into BNR word                                                               |
| BTI429_BNRPutSign                      | Inserts the sign into a BNR word                                                                   |
| BTI429_BNRPutSSM                       | Inserts the SSM field into a BNR word                                                              |
| BTI429_BNRPutVal                       | Inserts the value into a BNR word                                                                  |
| BTI429_FldGetData                      | Extracts data field from ARINC 429 word                                                            |
| BTI429_FldGetLabel                     | Extracts label field from ARINC 429 word                                                           |
| BTI429_FldGetParity                    | Extracts parity bit from ARINC 429 word                                                            |
| BTI429_FldGetSDI                       | Extracts SDI field from ARINC 429 word                                                             |
| BTI429_FldGetValue                     | Extracts specified field from ARINC 429 word                                                       |
| BTI429_FldPutData                      | Inserts the data field into an ARINC 429 word                                                      |
| BTI429_FldPutLabel                     | Inserts the label field into an ARINC 429 word                                                     |
| BTI429_FldPutSDI<br>BTI429_FldPutValue | Inserts the SDI field into an ARINC 429 word<br>Inserts the specified field into an ARINC 429 word |

*[Table 4.1—ARINC 429 BTI429\\_ functions](#page-39-1) (Continued)*

### <span id="page-41-0"></span>**4.3 Function Detail**

The following pages contain descriptions of the BTIDriver functions (in alphabetical order without regard to prefix). The constants in bold in the tables are the default options. Note that the "BTI429\_" prefixes have been omitted from the headings for easier reading, but all BTIDriver functions must begin with the appropriate prefix in source code.

### **BCDGetData**

```
ULONG BTI429_BCDGetData
(
      ULONG msg, //BCD word to extract data from
      USHORT msb, //Most significant bit of BCD field
      USHORT lsb //Least significant bit of BCD field
```
)

#### **RETURNS**

32-bit value of data field containing up to a maximum of 21 bits (19 bits from the BCD data field plus the 2-bit SDI field).

#### **DESCRIPTION**

Extracts the data field of the BCD word in *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the BCD field respectively. The specified bits are converted to a 32-bit unsigned value. No other conversions, such as resolution or sign, are made.

The function assumes the BCD word has the following format:

|  | BCD DATA<br>PARITY SSM |  |  |  |  |  |  |  |  |  |  |  |  |  |  | SDI |  | LABEL |  |  |  |  |                                                                                             |                         |  |
|--|------------------------|--|--|--|--|--|--|--|--|--|--|--|--|--|--|-----|--|-------|--|--|--|--|---------------------------------------------------------------------------------------------|-------------------------|--|
|  |                        |  |  |  |  |  |  |  |  |  |  |  |  |  |  |     |  |       |  |  |  |  | 32 13 130 29 28 27 28 25 22 120 19 18 17 16 15 14 13 12 11 110 9 1 1 1 2 13 1 4 1 5 1 6 1 7 |                         |  |
|  |                        |  |  |  |  |  |  |  |  |  |  |  |  |  |  |     |  |       |  |  |  |  |                                                                                             | ARINC 429<br>BIT NUMBER |  |

The bits from the parity and SSM fields are not be included in the return value. When specifying a *lsb* value in the SDI field (bits 9 or 10), the bits are treated as data for this function.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*lsb* and *msb* values are limited to the BCD data/SDI field (bits 9 through 29).

```
BTI429_BCDPutData, BTI429_BCDGetSign
```
### **BCDGetMant**

```
ULONG BTI429_BCDGetMant
(
     ULONG msg, //BCD word to extract data from
     USHORT sigdig //Number of significant digits
)
```
#### **RETURNS**

The data field mantissa.

#### **DESCRIPTION**

Extracts the mantissa value from the data field of the BCD word specified in *msg*. *sigdig* specifies the number of significant BCD digits (characters) in the data field. The result is converted to a 32 bit unsigned value and returned. No other conversion, such as scaling by the resolution value, is made.

The function assumes the BCD data field is divided into the following fields:

![](_page_43_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Per the ARINC 429 specification, the *sigdig* value is defined for the following number of significant digits: 3, 4, 5, and 6. Use the BTI429\_BCDGetData function for other non-standard data formats.

#### **SEE ALSO**

BTI429\_BCDPutMant

### **BCDGetSign**

USHORT BTI429\_BCDGetSign ( ULONG *msg //BCD word to extract data from* )

#### **RETURNS**

A non-zero value if sign of BCD word is negative, otherwise zero if the sign is positive.

#### **DESCRIPTION**

Extracts the sign of the BCD word specified in *msg*. The result is non-zero if the sign of the BCD word is negative (SSM field equals 11 binary). Otherwise, the function returns a zero value.

The function assumes the SSM field is located at bits 30 through 31 as shown below:

![](_page_44_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BCDPutSign

### **BCDGetSSM**

USHORT BTI429\_BCDGetSSM ( ULONG *msg //BCD word to extract data from* )

#### **RETURNS**

Value of the 2-bit SSM field.

#### **DESCRIPTION**

Extracts the SSM field of the BCD word in *msg*. The function assumes the SSM field is located at bits 30 through 31 as shown below:

![](_page_45_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BCDPutSSM

### **BCDGetVal**

```
VOID BTI429_BCDGetVal
(
     LPSTR buf, //Pointer to buffer to receive ASCII string
     ULONG msg, //BCD word to extract data from
     USHORT sigdig, //Number of significant digits
     LPCSTR resolstr //Pointer to resolution string
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Extracts the data field of the BCD word specified in *msg* and calculates the value. The resulting ASCII string is written to *buf*. This string may contain a decimal point and may be signed.

*sigdig* specifies the number of significant digits (characters) in the BCD data field. *resolstr* points to a null-terminated ASCII string specifying the resolution of the BCD data. This resolution string is a decimal real number that represents the value of the least significant digit (e.g., 0.1). It may contain a decimal point if needed, but should not have a sign or exponent.

The function assumes the BCD data field is divided into the following fields:

![](_page_46_Figure_10.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Per the ARINC 429 specification, the *sigdig* value is defined for the following number of significant digits: 3, 4, 5, and 6. Use of functions BTI429\_BCDGetData and BTI429\_BCDGetSign and additional scaling by the resolution value may be required for non-standard data formats.

#### **SEE ALSO**

BTI429\_BCDPutVal, BTI429\_BCDGetData, BTI429\_BCDGetSign

### **BCDPutData**

```
ULONG BTI429_BCDPutData
(
      ULONG msg, //32-bit BCD word
      ULONG value, //New data value
      USHORT msb, //Most significant bit of BCD field
      USHORT lsb //Least significant bit of BCD field
)
```
#### **RETURNS**

The new 32-bit BCD word with the data field inserted.

#### **DESCRIPTION**

Inserts *value* into the data field of the BCD word specified by *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the field respectively. *value* is converted to BCD and inserted into the specified bits (within bits 9 through 29). No other conversion is made.

The function assumes the BCD word has the following format:

![](_page_47_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*lsb* and *msb* values are limited to the BCD data/SDI field (bits 9 through 29). A maximum of 21 bits (when *msb*=29 and *lsb*=9) of *value* is used by this function.

#### **SEE ALSO**

BTI429\_BCDGetData

### **BCDPutMant**

```
ULONG BTI429_BCDPutMant
(
     ULONG msg, //32-bit BCD word
     ULONG value, //New data value
     USHORT sigdig, //Number of significant digits
     USHORT sign //Sign for SSM field
)
```
#### **RETURNS**

The new 32-bit BCD word with the data field inserted.

#### **DESCRIPTION**

Inserts the mantissa value into the data field of the BCD word specified in *msg*. *sigdig* specifies the number of significant digits (characters) in the BCD field. *value* is converted to BCD and inserted into the data field. The BCD data field is assumed to be divided into the following fields:

![](_page_48_Figure_8.jpeg)

If sign is non-zero, the value 11 (binary) is inserted into the SSM field to denote a signed value. Otherwise, the value 00 (binary) is inserted into the SSM field. No other conversion is made.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BCDGetMant

### **BCDPutSign**

```
ULONG BTI429_BCDPutSign
(
      ULONG msg, //32-bit BCD word
      USHORT sign //Sign for SSM field
)
```
#### **RETURNS**

The new 32-bit BCD word with the SSM field inserted.

#### **DESCRIPTION**

Inserts *sign* into the SSM field of the BCD word specified in *msg*. If *sign* is non-zero, the value 11 (binary) is inserted into the SSM field to specify a signed value. Otherwise, the value 00 (binary) is inserted into the SSM field.

The function assumes the SSM field is located at bits 30 through 31 as shown below:

![](_page_49_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BCDGetSign

### **BCDPutSSM**

```
ULONG BTI429_BCDPutSSM
(
     ULONG msg, //32-bit BCD word
     USHORT value //2-bit value of SSM field
)
```
#### **RETURNS**

The new 32-bit BCD word with the SSM field inserted.

#### **DESCRIPTION**

Inserts value into the SSM field of the BCD word in msg. The function assumes the SSM field is located at bits 30 through 31 as shown below:

| PARITY<br>SSM | BCD DATA                                                                                                                | LABEL<br>SDI             |
|---------------|-------------------------------------------------------------------------------------------------------------------------|--------------------------|
|               | 32   31   30   28   27   26   23   22   21   20   19   18   17   16   15   12   11   10   9   1   2   3   4   5   6   7 |                          |
|               |                                                                                                                         | ARINC 429<br>RT NI IMRER |

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BCDGetSSM

### **BCDPutVal**

```
ULONG BTI429_BCDPutVal
(
     LPCSTR buf, //Pointer to buffer containing ASCII string
     ULONG msg, //32-bit BCD word
     USHORT sigdig, //Number of significant digits
     LPCSTR resolstr //Pointer to resolution string
)
```
#### **RETURNS**

The new 32-bit BCD word with the data field inserted.

#### **DESCRIPTION**

Inserts a new value into the data field of the BCD word specified by *msg*. *buf* holds an ASCII string that contains the value to insert. It may contain a decimal point and may be signed.

*sigdig* specifies the number of significant digits (characters) in the BCD data field. *resolstr* points to a null-terminated ASCII string specifying the resolution of the BCD data. This resolution string is a decimal real number that represents the value of the least significant digit (e.g., 0.1). It may contain a decimal point if needed, but should not have a sign or exponent.

The BCD data field is assumed to be divided into the following fields:

![](_page_51_Figure_10.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Per the ARINC 429 specification, the *sigdig* value is defined for the following number of significant digits: 3, 4, 5, and 6. Use of functions

BTI429\_BCDPutData and BTI429\_BCDPutSign and additional scaling by the resolution value may be required for non-standard data formats.

```
BTI429_BCDGetVal, BTI429_BCDPutData, BTI429_BCDPutSign
```
### **BNRGetData**

```
ULONG BTI429_BNRGetData
(
      ULONG msg, //BNR word to extract data from
      USHORT msb, //Most significant bit of BNR field
      USHORT lsb //Least significant bit of BNR field
)
```
**RETURNS**

32-bit value of data field containing up to a maximum of 20 bits (18 bits from the BCD data field plus the 2-bit SDI field).

#### **DESCRIPTION**

Extracts the data field of the BNR word in *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the BNR

data field respectively. The specified bits are converted to a 32-bit unsigned value. No other conversions, such as resolution or sign, are made.

The function assumes the BNR word has the following format:

![](_page_52_Figure_10.jpeg)

The bits from the parity and SSM fields are not be included in the return value. When specifying a *lsb* value in the SDI field (bits 9 or 10), the bits are treated as data for this function.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*lsb* and *msb* values are limited to the BNR data/SDI field (bits 9 through 28).

#### **SEE ALSO**

BTI429\_BNRPutData, BTI429\_BNRGetSign

### **BNRGetMant**

```
ULONG BTI429_BNRGetMant
(
     ULONG msg, //BNR word to extract data from
     USHORT sigdig //Number of significant digits
)
```
#### **RETURNS**

The data field mantissa (right-adjusted).

#### **DESCRIPTION**

Extracts the mantissa value from the data field of the BNR word specified in *msg*. *sigdig* specifies the number of significant digits in the data field. If the SSM field of *msg* specifies a signed value, then the two's complement of the data field is returned. No other conversion, such as scaling by the resolution value, is made.

The BNR data field is signed if bit 29 in the SSM field is non-zero. The BNR data field is assumed to be left-adjusted at bit 28 as shown below:

| PARITY |  | SSM |  |  |  | BNR DATA |  |  |  |  |  |  |  |  | SDI |  |  |  |  |  |  |  |  | LABEL |                                                                                                                    |  |            |  |  |  |
|--------|--|-----|--|--|--|----------|--|--|--|--|--|--|--|--|-----|--|--|--|--|--|--|--|--|-------|--------------------------------------------------------------------------------------------------------------------|--|------------|--|--|--|
|        |  |     |  |  |  |          |  |  |  |  |  |  |  |  |     |  |  |  |  |  |  |  |  |       | 32   31   30   28   27   26   23   22   20   19   18   17   16   13   12   11   10   9   1   2   3   4   5   6   7 |  |            |  |  |  |
|        |  |     |  |  |  |          |  |  |  |  |  |  |  |  |     |  |  |  |  |  |  |  |  |       | ARINC 429                                                                                                          |  | BIT NUMBER |  |  |  |

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

The *sigdig* values may range from 1 to 20 bits.

#### **SEE ALSO**

BTI429\_BNRPutMant

### **BNRGetSign**

USHORT BTI429\_BNRGetSign ( ULONG *msg //BNR word to extract data from* )

#### **RETURNS**

A non-zero value if sign of BNR word is negative, otherwise zero if the sign is positive.

#### **DESCRIPTION**

Extracts the sign of the BNR word specified in *msg*. The result is non-zero if the sign of the BNR word is negative (bit 29 of the SSM field is non-zero). Otherwise, the function returns a zero value.

The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_54_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BNRPutSign, BTI429\_BNRGetSSM

### **BNRGetSSM**

USHORT BTI429\_BNRGetSSM ( ULONG *msg //BNR word to extract SSM field from* )

#### **RETURNS**

Value of SSM field.

#### **DESCRIPTION**

Extracts the SSM field from the BNR word in msg. The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_55_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BNRPutSSM, BTI429\_BNRGetSign

### **BNRGetVal**

```
VOID BTI429_BNRGetVal
(
     LPSTR buf, //Pointer to buffer to receive ASCII string
     ULONG msg, //BNR word to extract data from
     USHORT sigbit, //Number of significant bits
     LPCSTR resolstr //Pointer to resolution string
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Extracts the data field of the BNR word specified in *msg* and calculates the value. The resulting ASCII string is written to *buf*. This string may contain a decimal point and a negative sign.

*sigbit* specifies the number of significant bits in the BNR data field. *resolstr* points to a nullterminated ASCII string specifying the resolution of the BNR data. This resolution string is a decimal real number that represents the value of the least significant bit (e.g., 0.25). It may contain a decimal point if needed, but should not have a sign or exponent.

The function assumes the data field is left-adjusted at bit 28 as shown below:

| PARITY | SSM |  | BNR DATA |  |  |  |                                                                                                                     |  |  |  |  |  |  |  |  | LABEL<br>SDI |  |  |  |  |  |  |  |  |  |  |  |                          |  |  |  |
|--------|-----|--|----------|--|--|--|---------------------------------------------------------------------------------------------------------------------|--|--|--|--|--|--|--|--|--------------|--|--|--|--|--|--|--|--|--|--|--|--------------------------|--|--|--|
|        |     |  |          |  |  |  | 32   31   30   29   27   26   25   20   19   18   17   16   15   12   11   10   9   1   2   3   4   5   6   7   (8) |  |  |  |  |  |  |  |  |              |  |  |  |  |  |  |  |  |  |  |  |                          |  |  |  |
|        |     |  |          |  |  |  |                                                                                                                     |  |  |  |  |  |  |  |  |              |  |  |  |  |  |  |  |  |  |  |  | ARINC 429<br>RT NI IMRER |  |  |  |

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

The *sigdig* values may range from 1 to 20 bits. The sign of the result is determined by the SSM fields as described by the BTI429\_BNRGetSign function.

#### **SEE ALSO**

BTI429\_BNRPutVal, BTI429\_BNRGetSign

### **BNRPutData**

```
ULONG BTI429_BNRPutData
(
      ULONG msg, //32-bit BNR word
      ULONG value, //New data value
      USHORT msb, //Most significant bit of BNR field
      USHORT lsb //Least significant bit of BNR field
)
```
#### **RETURNS**

The new 32-bit BNR word with the data field inserted.

#### **DESCRIPTION**

Inserts *value* into the data field of the BNR word specified by *msg*. *msb* and *lsb* specify the ARINC 429 bit numbers of the most significant and least significant bits of the field respectively. *value* is converted to BNR and inserted into the specified bits (within bits 9 through 28). No other conversion, such as

resolution or sign, is made.

The function assumes the BNR word has the following format:

![](_page_57_Figure_10.jpeg)

The bits from the parity and SSM fields are not modified. When specifying a *lsb* value in the SDI field (bits 9 or 10), the bits are treated as data for this function.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*lsb* and *msb* values are limited to the BNR data/SDI field (bits 9 through 28). A maximum of 20 bits (when *msb*=28 and *lsb*=9) of *value* is used by this function.

#### **SEE ALSO**

BTI429\_BNRGetData, BTI429\_BNRPutSSM, BTI429\_BNRPutSign

### **BNRPutMant**

```
ULONG BTI429_BNRPutMant
(
     ULONG msg, //32-bit BNR word
     ULONG value, //New data value
     USHORT sigbit, //Number of significant bits
     USHORT twos //Two's complement field
)
```
#### **RETURNS**

The new 32-bit BNR word with the data field inserted.

#### **DESCRIPTION**

Inserts the mantissa value into the data field of the BNR word specified in *msg*. *sigbit* specifies the number of significant bits in the BNR field. *value* is converted to BNR and inserted into the data field.

The function assumes the BNR data field is left-adjusted at bit 28 as shown below:

| PARITY |  | SSM |  |  |  |  |  |  |  | BNR DATA |  |  |  |  |  |  |  |  | SDI |  | LABEL |  |  |                                                                                                              |  |            |
|--------|--|-----|--|--|--|--|--|--|--|----------|--|--|--|--|--|--|--|--|-----|--|-------|--|--|--------------------------------------------------------------------------------------------------------------|--|------------|
|        |  |     |  |  |  |  |  |  |  |          |  |  |  |  |  |  |  |  |     |  |       |  |  | 32 13 1 30 129 128 127 126 125 124 13 118 1 17 16 15 14 13 12 11 11 10 1 9   1 1 2 1 3 1 4 1 5 1 6 1 7 1 6 1 |  |            |
|        |  |     |  |  |  |  |  |  |  |          |  |  |  |  |  |  |  |  |     |  |       |  |  | ARINC 429                                                                                                    |  | BIT NUMBER |

If *twos* is non-zero, then the two's complement of *value* is inserted into the data field, and bit 29 of the SSM field is set to one. Otherwise, bit 29 of the SSM field is set to zero.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

The *sigdig* values may range from 1 to 20 bits. No resolution scaling is done by this function. .

#### **SEE ALSO**

BTI429\_BNRGetMant

### **BNRPutSign**

```
ULONG BTI429_BNRPutSign
(
      ULONG msg, //32-bit BNR word
      USHORT twos //Two's complement field
)
```
#### **RETURNS**

The new 32-bit BNR word with the SSM field inserted.

#### **DESCRIPTION**

Inserts *twos* into the SSM field of the BNR word specified in *msg*. If *twos* is non-zero, then bit 29 of the SSM field is set to one. Otherwise, bit 29 of the SSM field is set to zero.

The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_59_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_BNRGetSign, BTI429\_BNRPutSSM

### **BNRPutSSM**

```
ULONG BTI429_BNRPutSSM
(
      ULONG msg, //32-bit BNR word
      USHORT value //3-bit value of SSM field
)
```
### **RETURNS**

The new 32-bit BNR word with the SSM field inserted.

#### **DESCRIPTION**

Inserts *value* into the 3-bit SSM field of the BNR word specified by *msg*. The function assumes the SSM field is located at bits 29 through 31 as shown below:

![](_page_60_Figure_9.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

The sign is generally specified by bit 29. Changing this value may change the sign.

#### **SEE ALSO**

BTI429\_BNRGetSSM, BTI429\_BNRGetSign

### **BNRPutVal**

```
ULONG BTI429_BNRPutVal
(
     LPCSTR buf, //Pointer to buffer containing ASCII string
     ULONG msg, //32-bit BNR word
     USHORT sigbit, //Number of significant bits
     LPCSTR resolstr //Pointer to resolution string
)
```
#### **RETURNS**

The new 32-bit BNR word with the data field inserted.

#### **DESCRIPTION**

Inserts *buf* into the data field of the BNR word specified by *msg*. *buf* holds an ASCII string containing the value to insert. This string may contain a decimal point and may be signed.

*sigbit* specifies the number of significant bits in the BNR data field. *resolstr* points to a nullterminated ASCII string specifying the resolution of the BNR data. This resolution string is a decimal real number that represents the value of the least significant bit (e.g., 0.25). It may contain a decimal point if needed, but should not have a sign or exponent.

The function assumes the data field is left-adjusted at bit 28 as shown below:

| PARITY                                                                                                              |  | SSM | BNR DATA |  |  |  |  |  |  | SDI |  |  |  |  |  |  |  | LABEL |  |  |  |  |  |  |  |  |           |            |
|---------------------------------------------------------------------------------------------------------------------|--|-----|----------|--|--|--|--|--|--|-----|--|--|--|--|--|--|--|-------|--|--|--|--|--|--|--|--|-----------|------------|
| 32   31   30   29   27   26   25   20   19   18   17   16   13   12   11   10   9   1   2   3   4   5   6   7   (8) |  |     |          |  |  |  |  |  |  |     |  |  |  |  |  |  |  |       |  |  |  |  |  |  |  |  |           |            |
|                                                                                                                     |  |     |          |  |  |  |  |  |  |     |  |  |  |  |  |  |  |       |  |  |  |  |  |  |  |  | ARINC 429 | BIT NUMBER |

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

The *sigdig* values may range from 1 to 20 bits. The sign of the result is determined by the SSM fields as described by the BTI429\_BNRGetSign function.

#### **SEE ALSO**

BTI429\_BNRGetVal, BTI429\_BNRPutData, BTI429\_BNRPutSign

### **ChClear**

```
ERRVAL BTI429_ChClear
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Clears the contents of the specified channel. If the channel is a transmitter, all Command Blocks in the transmit Schedule are deleted. If the channel is a receiver, all filters are deleted. The contents of the Message Records are unaffected. The configuration options previously set by BTI429\_ChConfig are unchanged.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_CardReset, BTI429\_ChConfig

### **ChConfig**

```
ERRVAL BTI429_ChConfig
(
     ULONG ctrlflags, //Selects channel options
     INT channel, //Number of channel
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Configures the specified transmit or receive channel of the specified Device by performing the following steps:

- 1. Stops the channel.
- 2. Clears transmit Schedule for the specified channel (Filter Tables are not affected).
- 3. Writes Device options defined by *ctrlflags*.
- 4. Restarts the channel if previously started and not disabled by CHCFG429\_INACTIVE.

The CHCFG429\_GAP1US flag configures the channel to interpret all future gap parameters (*gap*/*gapval*) as microsecond values rather than bit-times. All functions with gap parameters are affected when operating the channel in this mode such as but not limited to:

BTI429\_SchedGap, BTI429\_SchedMsgEx, BTI429\_SchedData, BTI429\_PlayPutGap, BTI429\_PlayPutData

| ctrlflags            |                                                                                 |     |     |  |  |  |  |  |  |
|----------------------|---------------------------------------------------------------------------------|-----|-----|--|--|--|--|--|--|
| Constant             | Description                                                                     | Rcv | Xmt |  |  |  |  |  |  |
| CHCFG429_DEFAULT     | Select all default settings (bold below)                                        | x   | x   |  |  |  |  |  |  |
| CHCFG429_LOWSPEED    | Select low speed messages (12.5 Kbps)                                           | x   | x   |  |  |  |  |  |  |
| CHCFG429_HIGHSPEED   | Select high speed messages (100 Kbps)                                           | x   | x   |  |  |  |  |  |  |
| CHCFG429_AUTOSPEED   | Select auto speed detection of messages                                         | x   |     |  |  |  |  |  |  |
| CHCFG429_PARODD      | Select odd parity                                                               | x   | x   |  |  |  |  |  |  |
| CHCFG429_PAREVEN     | Select even parity                                                              | x   | x   |  |  |  |  |  |  |
| CHCFG429_PARDATA     | Select parity bit as data (ignores parity)                                      | x   | x   |  |  |  |  |  |  |
| CHCFG429_ACTIVE      | Enable channel activity                                                         | x   | x   |  |  |  |  |  |  |
| CHCFG429_INACTIVE    | Disable channel activity                                                        | x   | x   |  |  |  |  |  |  |
| CHCFG429_SEQSEL      | Sequential monitoring selected at message level                                 | x   | x   |  |  |  |  |  |  |
| CHCFG429_SEQALL      | Every message will be recorded in the Sequential Record                         | x   | x   |  |  |  |  |  |  |
| CHCFG429_NOLOGHALT   | No entry will be made in the Event Log List on a HALT command                   |     | x   |  |  |  |  |  |  |
| CHCFG429_LOGHALT     | An entry will be made in the Event Log List on a HALT command                   |     | x   |  |  |  |  |  |  |
| CHCFG429_NOLOGPAUSE  | No entry will be made in the Event Log List on a PAUSE command                  |     | x   |  |  |  |  |  |  |
| CHCFG429_LOGPAUSE    | An entry will be made in the Event Log List on a PAUSE command                  |     | x   |  |  |  |  |  |  |
| CHCFG429_NOLOGERR    | No entry will be made in the Event Log List when a decoder detects an error     | x   |     |  |  |  |  |  |  |
| CHCFG429_LOGERR      | An entry will be made in the Event Log List when a decoder detects an error     | x   |     |  |  |  |  |  |  |
| CHCFG429_TIMETAGOFF  | The time-tag option is selected at the message level                            | x   | x   |  |  |  |  |  |  |
| CHCFG429_TIMETAG     | All messages will record a time-tag.                                            | x   | x   |  |  |  |  |  |  |
| CHCFG429_ELAPSEOFF   | The elapse timing option is selected at the message level                       | x   | x   |  |  |  |  |  |  |
| CHCFG429_ELAPSE      | All messages will record an elapsed time.                                       | x   | x   |  |  |  |  |  |  |
| CHCFG429_MAXMINOFF   | Max and min repetition rates are selected at the message level                  | x   | x   |  |  |  |  |  |  |
| CHCFG429_MAX         | All messages will record a max time                                             | x   | x   |  |  |  |  |  |  |
| CHCFG429_MIN         | All messages will record a min time                                             | x   | x   |  |  |  |  |  |  |
| CHCFG429_MAXMIN      | All messages will record a max and a min time                                   | x   | x   |  |  |  |  |  |  |
| CHCFG429_NOHIT       | The Hit Counter is selected at the message level                                | x   | x   |  |  |  |  |  |  |
| CHCFG429_HIT         | The Hit Counter is selected for all messages (can't have Hit Counter with       |     |     |  |  |  |  |  |  |
|                      | time-tag, elapse, or max/min timing)                                            | x   | x   |  |  |  |  |  |  |
| CHCFG429_SELFTESTOFF | This channel will transmit/receive on the operational bus and not the self-test |     |     |  |  |  |  |  |  |
|                      | bus.                                                                            | x   | x   |  |  |  |  |  |  |

| ctrlflags          |                                                                                                                                                                                                                                                                        |     |     |  |  |  |  |  |  |
|--------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----|-----|--|--|--|--|--|--|
| Constant           | Description                                                                                                                                                                                                                                                            | Rcv | Xmt |  |  |  |  |  |  |
| CHCFG429_SELFTEST  | This channel will transmit/receive on the internal self-test bus and not the<br>operational bus. Only one transmitter can be on the self-test bus at a time;<br>therefore, only the last transmit channel configured to use the self-test bus will<br>use this option. | x   | x   |  |  |  |  |  |  |
| CHCFG429_SYNCOFF   | The SYNCOUT signal option will be selected at the message level                                                                                                                                                                                                        | x   | x   |  |  |  |  |  |  |
| CHCFG429_SYNC      | The SYNCOUT signal will be active for all messages of this channel                                                                                                                                                                                                     | x   | x   |  |  |  |  |  |  |
| CHCFG429_EXTOFF    | Messages are selected for an external trigger at the message level                                                                                                                                                                                                     |     | x   |  |  |  |  |  |  |
| CHCFG429_EXTTRIG   | All messages for this channel will be externally triggered                                                                                                                                                                                                             |     | x   |  |  |  |  |  |  |
| CHCFG429_NOERR     | Error injection is selected at the message level                                                                                                                                                                                                                       |     | x   |  |  |  |  |  |  |
| CHCFG429_PARERR    | All messages will have a parity error (inverts the parity bit)                                                                                                                                                                                                         |     | x   |  |  |  |  |  |  |
| CHCFG429_UNPAUSE   | The channel will initially be unpaused                                                                                                                                                                                                                                 | x   | x   |  |  |  |  |  |  |
| CHCFG429_PAUSE     | The channel will initially be paused                                                                                                                                                                                                                                   | x   | x   |  |  |  |  |  |  |
| CHCFG429_NOLOOPMAX | Disable Schedule maximum loop count                                                                                                                                                                                                                                    |     | x   |  |  |  |  |  |  |
| CHCFG429_LOOPMAX   | Enable Schedule maximum loop count                                                                                                                                                                                                                                     |     | x   |  |  |  |  |  |  |
| CHCFG429_GAPBIT    | Enable bit-time gaps                                                                                                                                                                                                                                                   |     | x   |  |  |  |  |  |  |
| CHCFG429_GAP1US    | Enable microsecond gaps (Device Dependent)                                                                                                                                                                                                                             |     | x   |  |  |  |  |  |  |
| CHCFG429_SCHEDULE  | Configure for Schedule Mode                                                                                                                                                                                                                                            |     | x   |  |  |  |  |  |  |
| CHCFG429_PLAYBACK  | Configure for Playback FIFO Mode (Device Dependent)                                                                                                                                                                                                                    |     | x   |  |  |  |  |  |  |

#### *(Continued from previous page)*

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

5G Devices do not support the CHCFG429\_LOOPMAX flag.

Only 5G+ and 6G Devices support the CHCFG429\_GAP1US and CHCFG429\_PLAYBACK flags.

#### **WARNINGS**

The function clears any previous configuration of the channel.

Channels configured with CHCFG429\_SCHEDULE must only use BTI429\_Sched\* functions, whereas channels configured with CHCFG429\_PLAYBACK must only use BTI429\_Play\* functions.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop

### **ChConfigEx**

```
ERRVAL BTI429_ChConfigEx
(
     ULONG ctrlflags, //Selects channel options
     USHORT count, //Number of entries in the schedule
     INT channel, //Number of channel
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Configures the channel similar to BTI429\_ChConfig, but with the addition of the parameter *count* which is used to specify either the number of schedule entries to allocate for the schedule (CHCFG429\_SCHEDULE) or the number of Command Block entries for the Playback FIFO (CHCFG429\_PLAYBACK). BTI429\_ChConfig defaults to 512 entries, and *count* can be used to allocate a different number.

#### **DEVICE DEPENDENCY**

3G and 4G Devices support up to 4089 entries, while 5G, 5G+, and 6G Devices support up to 8187 entries.

Only 5G+ and 6G Devices support Playback.

#### **WARNINGS**

The function clears any previous configuration of the channel.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop

### **ChGetCount**

```
VOID BTI429_ChGetCount
(
      LPINT rcvcount, //Pointer to variable to hold receiver count
      LPINT xmtcount, //Pointer to variable to hold transmitter count
      HCORE hCore //Core handle
```
)

#### **RETURNS**

None.

#### **DESCRIPTION**

Determines the transmitter and receiver channel count, and puts them in the variables pointed to by *rcvcount* and *xmtcount*.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_ChIsRcv, BTI429\_ChIsXmt, BTI429\_ChIs429, BTI429\_ChGetInfo

### **ChGetInfo**

```
ULONG BTI429_ChGetInfo
(
     USHORT infotype, //Type of information to be returned
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
**RETURNS**

The requested information about the specified channel.

#### **DESCRIPTION**

Provides information about the functionality of the specified *channel*. The *infotype* constant listed below may be used to specify the requested information.

| infotype         |                   |                                                                                               |  |  |  |  |  |  |  |  |
|------------------|-------------------|-----------------------------------------------------------------------------------------------|--|--|--|--|--|--|--|--|
| Constant         | Returns           | Description                                                                                   |  |  |  |  |  |  |  |  |
|                  | 1=TRUE            | Channel is parametric                                                                         |  |  |  |  |  |  |  |  |
| INFO429_PARAM    | 0=FALSE           | Channel is not parametric                                                                     |  |  |  |  |  |  |  |  |
| INFO429_PWAVE    | 1=TRUE<br>0=FALSE | Channel supports parametric waveform (DAC)<br>Channel does not support parametric<br>waveform |  |  |  |  |  |  |  |  |
| INFO429_CMBIAS   | 1=TRUE<br>0=FALSE | Channel supports common-mode DC bias<br>Channel does not support common-mode DC<br>bias       |  |  |  |  |  |  |  |  |
| INFO429_OUTSTATE | 1=TRUE<br>0=FALSE | Channel supports Leg lifting/shorting<br>Channel does not support Leg lifting/shorting        |  |  |  |  |  |  |  |  |
| INFO429_PLAYBACK | 1=TRUE<br>0=FALSE | Channel supports playback mode<br>Channel does not support playback mode                      |  |  |  |  |  |  |  |  |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Playback is determined per core, not per channel. Passing the INFO429\_PLAYBACK flag will return TRUE even on channels that cannot be playback (receive) channels if the core supports playback.

```
BTI429_ParamAmplitudeConfig, 
BTI429_ParamBitRateConfig, BTI429_ParamWaveformConfig, 
BTI429_ParamWaveformConfigEx, 
BTI429_ChOutputStateSet
```
### ChIs429

```
BOOL BTI429_ChIs429
(
      INT channel, //Channel number to test
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the channel is ARINC 429, otherwise FALSE.

#### **DESCRIPTION**

Checks to see if the channel number specified by *channel* is a 429 channel.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_ChGetInfo, BTI429\_ChIsRcv, BTI429\_ChIsXmt, BTI429\_ChGetCount

### **ChIsRcv**

```
BOOL BTI429_ChIsRcv 
(
      INT channel, //Channel number to test
      HCORE hCore //Core handle
)
```
### **RETURNS**

TRUE if the channel is an ARINC 429 receiver, or FALSE if it is not a receiver.

#### **DESCRIPTION**

Checks to see if the channel number specified by *channel* is a receive channel.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

If this function returns a value of zero, do not assume that the channel must then be a transmitter, because the channel may not exist at all. A call to BTI429\_ChIsXmt must be made to be sure that the channel is a transmitter.

#### **SEE ALSO**

BTI429\_ChIsXmt, BTI429\_ChGetCount, BTI429\_ChIs429

### **ChIsXmt**

```
BOOL BTI429_ChIsXmt 
(
      INT channel, //Channel number to test
      HCORE hCore //Core handle
)
```
### **RETURNS**

TRUE if the channel is a transmitter, or FALSE if it is not a transmitter.

#### **DESCRIPTION**

Checks to see if the channel number specified by *channel* is a transmit channel.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

If this function returns a value of zero, do not assume that the channel must then be a receiver, because the channel may not exist at all. A call to BTI429\_ChIsRcv must be made to be sure that the channel is a receiver.

#### **SEE ALSO**

BTI429\_ChIsRcv, BTI429\_ChGetCount, BTI429\_ChIs429

### **ChOutputStateSet**

```
ERRVAL BTI429_ChOutputStateSet
(
      ULONG pos, //Output state of transmitter's positive leg
      ULONG neg, //Output state of transmitter's negative leg
      INT channum, //Channel number to test
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Sets the output state of each Transmit Channel leg (*pos*/*neg*) based on one of the following constants:

|                      | pos/neg                                   |
|----------------------|-------------------------------------------|
| Constant             | Description                               |
| OUTSTATE429_NOCHANGE | Keep leg in its current state (default)   |
| OUTSTATE429_SIGNAL   | Connect the leg to normal transmit signal |
| OUTSTATE429_OPEN     | Leave the leg open                        |
| OUTSTATE429_GROUND   | Short the leg to ground                   |

Note that *pos* and *neg* may use different constants and need not match output states (e.g. only ground the positive leg). Only one constant can be used per leg.

#### **DEVICE DEPENDENCY**

Applies to 6G Devices with output state functionality. A channel can be tested for functionality using BTI429\_ChGetInfo. Please consult the Device hardware manual for more information.

#### **WARNINGS**

This function is used for error injection; therefore, a transmitter leg may be shorted to ground and/or opened (floating) as instructed. Use caution that these output states do not damage any external databus hardware.

#### **SEE ALSO**

BTI429\_ChGetInfo

### **ChPause**

```
VOID BTI429_ChPause
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Pauses the operation of the channel specified by *channel*. All activity on the channel ceases. The channel remains paused until the channel is resumed by BTI429\_ChResume. BTI429\_ChConfig initializes the channel as either unpaused (default) or paused.

*Note: A transmit channel can also be paused when the Device encounters a PAUSE Command Block in the transmit Schedule.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Do not confuse this channel pause with either channel enable or the card-level controls. Channel enable is controlled by BTI429\_ChConfig, BTI429\_ChStart, and BTI429\_ChStop. Card-level controls are activated through BTICard\_CardStart, BTICard\_CardStop, and BTICard\_CardResume.

#### **SEE ALSO**

BTI429\_ChResume, BTI429\_SchedPause

### **ChPauseCheck**

```
INT BTI429_ChPauseCheck
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
### **RETURNS**

A non-zero value if the channel is paused, or zero if the channel is not paused.

#### **DESCRIPTION**

Determines whether the channel specified by *channel* is paused.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_ChPause, BTI429\_ChResume

### **ChResume**

```
VOID BTI429_ChResume
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Resumes the operation of the channel specified by *channel* after it has been paused by BTI429\_ChPause or the Device has encountered a PAUSE Command Block in the transmit Schedule. BTI429\_ChConfig initializes the channel as either unpaused (default) or paused. If the Device is running, all activity on the channel will begin. If the Device is stopped, channel activity will begin when the Device is started.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Do not confuse this channel pause with either channel enable or the card-level controls. Channel enable is controlled by BTI429\_ChConfig, BTI429\_ChStart, and BTI429\_ChStop. Card-level controls are activated through BTICard\_CardStart, BTICard\_CardStop, and BTICard\_CardResume.

#### **SEE ALSO**

BTI429\_ChPause, BTI429\_SchedPause

### **ChStart**

```
BOOL BTI429_ChStart
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the channel was previously enabled, otherwise FALSE.

#### **DESCRIPTION**

Enables the operation of the channel specified by *channel*. If it is a transmit channel, the Schedule restarts at the beginning. The channel remains enabled until BTI429\_ChStop is called or a HALT Command Block is encountered in the transmit Schedule. If the Device is stopped, then channel activity begins when the Device is started with BTICard\_CardStart.

BTI429\_ChStart and BTI429\_ChStop enable and disable a channel. BTI429\_ChConfig initializes the channel as either enabled (default) or disabled. These functions allow the channel to be stopped and reconfigured with different settings while other channels are running.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Do not confuse this channel enable with either channel pause or the card-level controls. Channel pause is controlled by BTI429\_ChConfig, BTI429\_ChPause, and BTI429\_ChResume. Cardlevel controls are activated through BTICard\_CardStart, BTICard\_CardStop, and BTICard\_CardResume.

#### **SEE ALSO**

BTI429\_ChStop, BTICard\_CardStart, BTICard\_CardStop, BTI429\_SchedHalt

### **ChStop**

```
BOOL BTI429_ChStop
(
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
### **RETURNS**

TRUE if the channel was previously enabled, otherwise FALSE.

#### **DESCRIPTION**

Disables operation of the channel specified by *channel*. If a message is being sent or received, the processing is allowed to finish before the channel is halted. Use BTI429\_ChStart to reenable the channel.

BTI429\_ChStart and BTI429\_ChStop enable and disable a channel. BTI429\_ChConfig initializes the channel as either enabled (default) or disabled. These functions allow the channel to be stopped and reconfigured with different settings while other channels are running.

*Note: A transmit channel can also be stopped when the Device encounters a HALT Command Block in the transmit Schedule.* 

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Do not confuse this channel enable with either channel pause or the card-level controls. Channel pause is controlled by BTI429\_ChConfig, BTI429\_ChPause, and BTI429\_ChResume. Cardlevel controls are activated through:

BTICard\_CardStart, BTICard\_CardStop BTICard\_CardResume

```
BTI429_ChStart, BTI429_SchedHalt, BTICard_CardStart, BTICard_CardStop
```
### **ChSyncDefine**

```
ERRVAL BTI429_ChSyncDefine
(
      BOOL enable, //Enable/disable external sync output
      USHORT syncmask, //Line(s) used for sync output
      USHORT pinpolarity,//Active pin polarity (high/low)
      INT channel, //Channel number (transmit or receive)
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Defines the sync output settings for *channel* on *hCore* and configures all messages to output a sync signal. This sync signal appears on the line(s) specified by *syncmask* with the polarity specified by *pinpolarity* (see tables below). When enabled, the sync signal is active during the transmission or reception of the message. BTI429\_ChSyncDefine may be called during run time to redefine the sync output settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Sync outputs are both generation dependent and device dependent. Refer to Chapter [6](#page-291-0) and the following tables for more information.

| syncmask          |                               |    |    |     |    |
|-------------------|-------------------------------|----|----|-----|----|
| Constant          | Description                   | 4G | 5G | 5G+ | 6G |
| SYNCMASK_SYNCA    | Selects discrete sync line A  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGA | Selects PXIe TRIG sync line A |    |    |     | *  |
| SYNCMASK_SYNCB    | Selects discrete sync line B  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGB | Selects PXIe TRIG sync line B |    |    |     | *  |
| SYNCMASK_SYNCC    | Selects discrete sync line C  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGC | Selects PXIe TRIG sync line C |    |    |     | *  |
| SYNCMASK_PXISTARC | Selects PXIe STAR sync line C |    |    |     | *  |

*\* PXIe Syncs are only available on PXIe Devices. Refer to BTICard\_CardSyncValid for device dependent support*

| pinpolarity    |                                           |  |  |
|----------------|-------------------------------------------|--|--|
| Constant       | Description                               |  |  |
| SYNCPOL_SYNCAL | Sets active low polarity for sync line A  |  |  |
| SYNCPOL_SYNCAH | Sets active high polarity for sync line A |  |  |
| SYNCPOL_SYNCBL | Sets active low polarity for sync line B  |  |  |
| SYNCPOL_SYNCBH | Sets active high polarity for sync line B |  |  |
| SYNCPOL_SYNCCL | Sets active low polarity for sync line C  |  |  |
| SYNCPOL_SYNCCH | Sets active high polarity for sync line C |  |  |

Alternatively, to configure selected transmit message(s) to output a sync pulse, use BTI429\_Msg-SyncDefine.

#### **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices. 3G Devices, which have a single sync line, can call BTI429\_ChConfig with the CHCFG429\_SYNC flag. The mapping of sync lines is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgSyncDefine, BTICard\_CardSyncValid

### **ChTriggerDefine**

```
ERRVAL BTI429_ChTriggerDefine
(
      BOOL enable, //Enable/disable external trigger
      USHORT trigmask, //Line(s) used for trigger signal
      USHORT trigval, //Active/inactive condition for trigger line(s)
      USHORT pinpolarity,//Active pin polarity (high/low) 
      INT xmtchan, //Transmit channel number
      HCORE hCore //Core handle
```
)

#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Defines the transmit trigger settings for *xmtchan* on *hCore* and configures every message in the transmit schedule to wait for the defined trigger signal before transmitting. The input line(s) are specified by *trigmask* with an active trigger state being the combination of *trigval* and *pinpolarity* (see tables below).

BTI429\_ChTriggerDefine may be called during run time to redefine the trigger input settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Only one *trigmask* may be selected per trigger line (A, B, and C), and all combined states must be true for the trigger to occur. Triggers are both generation dependent and device dependent. Refer to Chapter [6](#page-291-0) and the following tables for more information.

| trigmask          |                                  |    |    |     |    |
|-------------------|----------------------------------|----|----|-----|----|
| Constant          | Description                      | 4G | 5G | 5G+ | 6G |
| TRIGMASK_TRIGA    | Selects discrete trigger line A  | x  | x  | x   | x  |
| TRIGMASK_PXITRIGA | Selects PXIe TRIG trigger line A |    |    |     | *  |
| TRIGMASK_PXISTARA | Selects PXIe STAR trigger line A |    |    |     | *  |
| TRIGMASK_TRIGB    | Selects discrete trigger line B  | x  | x  | x   | x  |
| TRIGMASK_PXITRIGB | Selects PXIe TRIG trigger line B |    |    |     | *  |
| TRIGMASK_PXISTARB | Selects PXIe STAR trigger line B |    |    |     | *  |
| TRIGMASK_TRIGC    | Selects discrete trigger line C  | x  | x  | x   | x  |
| TRIGMASK_PXITRIGC | Selects PXIe TRIG trigger line C |    |    |     | *  |
| TRIGMASK_PXISTARC | Selects PXIe STAR trigger line C |    |    |     | *  |

*\* PXIe Syncs are only available on PXIe Devices. Refer to BTICard\_CardTriggerValid for device dependent support*

| trigval          |                            |  |  |
|------------------|----------------------------|--|--|
| Constant         | Description                |  |  |
| TRIGVAL_TRIGAOFF | Trigger on line A inactive |  |  |
| TRIGVAL_TRIGAON  | Trigger on line A active   |  |  |
| TRIGVAL_TRIGBOFF | Trigger on line B inactive |  |  |
| TRIGVAL_TRIGBON  | Trigger on line B active   |  |  |
| TRIGVAL_TRIGCOFF | Trigger on line C inactive |  |  |
| TRIGVAL_TRIGCON  | Trigger on line C active   |  |  |

| pinpolarity    |                                               |    |    |     |    |
|----------------|-----------------------------------------------|----|----|-----|----|
| Constant       | Description                                   | 4G | 5G | 5G+ | 6G |
| TRIGPOL_TRIGAL | Sets active low polarity for trigger line A   | x  | x  | x   | x  |
| TRIGPOL_TRIGAH | Sets active high polarity for trigger line A  | x  | x  | x   | x  |
| TRIGPOL_TRIGAF | Sets active on falling edge of trigger line A |    |    |     | x  |
| TRIGPOL_TRIGAR | Sets active on rising edge of trigger line A  |    |    |     | x  |
| TRIGPOL_TRIGBL | Sets active low polarity for trigger line B   | x  | x  | x   | x  |
| TRIGPOL_TRIGBH | Sets active high polarity for trigger line B  | x  | x  | x   | x  |
| TRIGPOL_TRIGBF | Sets active on falling edge of trigger line B |    |    |     | x  |
| TRIGPOL_TRIGBR | Sets active on rising edge of trigger line B  |    |    |     | x  |
| TRIGPOL_TRIGCL | Sets active low polarity for trigger line C   | x  | x  | x   | x  |
| TRIGPOL_TRIGCH | Sets active high polarity for trigger line C  | x  | x  | x   | x  |
| TRIGPOL_TRIGCF | Sets active on falling edge of trigger line C |    |    |     | x  |
| TRIGPOL_TRIGCR | Sets active on rising edge of trigger line C  |    |    |     | x  |

Alternatively, to associate selected message(s) to a trigger signal, use BTI429\_MsgTriggerDefine.

#### **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices. 3G Devices, which have a single external trigger, can call BTI429\_ChConfig with the CHCFG429\_EXTTRIG flag. The mapping of trigger lines is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgTriggerDefine, BTICard\_CardTrigger, BTICard\_CardTriggerEx, BTICard\_CardTriggerValid

### **CmdShotRd**

```
BOOL BTI429_CmdShotRd
(
     SCHNDX index, //Schedule index of item to read
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the single-shot bit is set, otherwise FALSE if not set.

#### **DESCRIPTION**

Reads the value of the single-shot bit for the schedule opcode specified by *index*.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

#### **SEE ALSO**

BTI429\_CmdShotWr

### **CmdShotWr**

```
ERRVAL BTI429_CmdShotWr
(
     BOOL value, //Value of single-shot bit
     SCHNDX index, //Schedule index of item to single-shot
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Sets the single-shot bit to *value* for the schedule entry specified by *index*. When set to TRUE, the single-shot bit instructs the schedule to process the specified opcode one time, and then to set the skip bit after processing is complete. The single-shot bit is FALSE (disabled) by default.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

#### **SEE ALSO**

BTI429\_CmdShotRd

### **CmdSkipRd**

```
BOOL BTI429_CmdSkipRd
(
     SCHNDX index, //Schedule index of item to read
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the skip bit is set, otherwise FALSE if not set.

#### **DESCRIPTION**

Reads the value of the skip bit for the schedule opcode specified by *index*.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

#### **SEE ALSO**

BTI429\_CmdSkipWr

### **CmdSkipWr**

```
ERRVAL BTI429_CmdSkipWr
(
     BOOL value, //Value of skip bit
     SCHNDX index, //Schedule index of item to skip
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Sets the skip bit to *value* for the schedule entry specified by *index*. When set to TRUE, the skip bit instructs the schedule to skip over processing the specified opcode. The skip bit is FALSE (disabled) by default.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

#### **SEE ALSO**

BTI429\_CmdSkipRd

### **CmdStepRd**

```
BOOL BTI429_CmdStepRd
(
     SCHNDX index, //Schedule index of item to read
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the step bit is set, otherwise FALSE if not set.

#### **DESCRIPTION**

Reads the value of the step bit for the schedule opcode specified by *index*.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

The user is especially cautioned to consider the effects of stepping through opcodes that depend upon or generate hardware timing.

#### **SEE ALSO**

BTI429\_CmdStepWr

### **CmdStepWr**

```
ERRVAL BTI429_CmdStepWr
(
     BOOL value, //Value of step bit
     SCHNDX index, //Schedule index of item to step
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Sets the step bit to *value* for the schedule entry specified by *index*. When set to TRUE, the step bit instructs the schedule to pause after processing the specified opcode. The step bit is FALSE (disabled) by default.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Some scheduling functions insert more than one opcode into the schedule. Therefore, it may be necessary to call this function on multiple schedule index values to get the desired effect.

The user is especially cautioned to consider the effects of stepping through opcodes that depend upon or generate hardware timing.

#### **SEE ALSO**

BTI429\_CmdStepRd

### **FilterDefault**

```
MSGADDR BTI429_FilterDefault
(
     ULONG ctrlflags, //Selects message options
     INT channel, //Number of receive channel
     HCORE hCore //Core handle
)
```
#### **RETURNS**

Address of the Message Record the function created and placed in the Filter Table.

#### **DESCRIPTION**

Creates a Message Record with the options specified in *ctrlflags*, and then sets it as the default Message Record for the channel specified by *channel*. Received messages that do not meet the criteria of specific filters are saved in the default Message Record. If no default filter was created and a message does not match a specific filter, then the message is skipped and not saved in memory.

The options that can be used in *ctrlflags* are listed below. Please note that only the receiver options can be used with this function.

| ctrlflags           |                                                                                  |     |     |  |
|---------------------|----------------------------------------------------------------------------------|-----|-----|--|
| Constant            | Description                                                                      | Rcv | Xmt |  |
| MSGCRT429_DEFAULT   | Select all default settings (bold below)                                         | x   | x   |  |
| MSGCRT429_NOSEQ     | This message will not get recorded in the Sequential Record                      | x   | x   |  |
| MSGCRT429_SEQ       | This message will get recorded in the Sequential Record                          | x   | x   |  |
| MSGCRT429_NOLOG     | This message will not create an entry in the Event Log List                      | x   | x   |  |
| MSGCRT429_LOG       | This message will create an entry in the Event Log List                          | x   | x   |  |
| MSGCRT429_NOTIMETAG | This message will not record a time-tag                                          | x   | x   |  |
| MSGCRT429_TIMETAG   | This message will record a time-tag                                              | x   | x   |  |
| MSGCRT429_NOELAPSE  | This message will not record an Elapsed Time                                     | x   | x   |  |
| MSGCRT429_ELAPSE    | This message will record an Elapsed Time                                         | x   | x   |  |
| MSGCRT429_NOMAXMIN  | This message will not record maximum and minimum repetition rates                | x   | x   |  |
| MSGCRT429_MAX       | This message will record maximum repetition rates                                | x   | x   |  |
| MSGCRT429_MIN       | This message will record minimum repetition rates                                | x   | x   |  |
| MSGCRT429_MAXMIN    | This message will record maximum and minimum repetition rates                    | x   | x   |  |
| MSGCRT429_NOHIT     | This message will not record a Hit Counter                                       | x   | x   |  |
| MSGCRT429_HIT       | This message will record a Hit Counter (can't have Hit Counter with time-tag,    | x   | x   |  |
|                     | elapse, or max/min timing)                                                       |     |     |  |
| MSGCRT429_NOSKIP    | This message will not be skipped                                                 | x   | x   |  |
| MSGCRT429_SKIP      | This message will be skipped, and none of the options will be processed          | x   | x   |  |
| MSGCRT429_NOSYNC    | This message will not generate a SYNCOUT signal                                  |     | x   |  |
| MSGCRT429_SYNC      | This message will generate a SYNCOUT signal                                      |     | x   |  |
| MSGCRT429_NOEXTRIG  | This message will be triggered immediately                                       |     | x   |  |
| MSGCRT429_EXTRIG    | This message will wait for an EXTRIG* pulse to be triggered                      |     | x   |  |
| MSGCRT429_NOERR     | This message will not have a parity error                                        |     | x   |  |
| MSGCRT429_PARERR    | This message will have a parity error                                            |     | X   |  |
| MSGCRT429_WIPE      | The data fields of this message will initially be wiped to a value               | x   | x   |  |
| MSGCRT429_NOWIPE    | The data fields of this message will initially be left unchanged                 | x   | x   |  |
| MSGCRT429_WIPE0     | The data fields of this message will be wiped with a value of zeros (this option | x   | x   |  |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |  |
| MSGCRT429_WIPE1     | The data fields of this message will be wiped with a value of ones (this option  | x   | x   |  |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |  |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

This function initializes all filters (all label/SDI combinations) for the specified channel. Any filters previously created for the channel are overwritten. Therefore, BTI429\_FilterDefault must precede any calls to BTI429\_FilterSet.

#### **SEE ALSO**

BTI429\_FilterSet, BTI429\_FilterRd, BTI429\_FilterWr

### **FilterRd**

```
MSGADDR BTI429_FilterRd
(
      INT label, //Label value
      INT sdi, //SDI pattern
      INT channel, //Number of receive channel
      HCORE hCore //Core handle
```
)

#### **RETURNS**

Address of the Message Record pointed to by the Filter for the given parameters.

#### **DESCRIPTION**

Reads the address of the Message Record pointed to by the Filter Table for the specified *channel*, *label*, and *sdi* values.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

This value reads the address of the Message Record that a filter is pointing to, not the ARINC 429 data word. Use BTI429\_MsgDataRd to read the ARINC 429 data from a Message Record.

#### **SEE ALSO**

BTI429\_FilterWr, BTI429\_FilterSet, BTI429\_FilterDefault, BTI429\_MsgDataRd

### **FilterSet**

```
MSGADDR BTI429_FilterSet
(
     ULONG ctrlflags, //Selects message options
     INT label, //Label value to receive
     INT sdi, //SDI patterns to receive
     INT channel, //Number of receive channel
     HCORE hCore //Core handle
)
```
#### **RETURNS**

Address of the Message Record the function created and placed in the Filter Table.

#### **DESCRIPTION**

Creates a filter for a receive channel by creating a Message Record with the options specified in *ctrlflags*, then setting it as the Message Record for the specified *channel*, *label*, and *sdi* values.

Filters are used to sort and save (by label and SDI) messages that are received over a given databus. During operation, when the label and SDI in a received message match the label and SDI information in a specific filter, the message is stored in a specific Message Record location.

*sdi* allows one or more SDI combinations to be specified. A filter is created for each specified SDI. The predefined constants listed below can be used to specify the SDI. When a combination of SDIs are selected, the constants should be OR-ed together.

| sdi       |                                   |
|-----------|-----------------------------------|
| Constants | Description                       |
| SDI00     | Selects only the SDI value of 00. |
| SDI01     | Selects only the SDI value of 01. |
| SDI10     | Selects only the SDI value of 10. |
| SDI11     | Selects only the SDI value of 11. |
| SDIALL    | Selects all the SDI values.       |

The options that can be used in *ctrlflags* are listed below. Please note that only the receiver options can be used with this function.

| ctrlflags           |                                                                               |     |     |
|---------------------|-------------------------------------------------------------------------------|-----|-----|
| Constant            | Description                                                                   | Rcv | Xmt |
| MSGCRT429_DEFAULT   | Select all default settings (bold below)                                      | x   | x   |
| MSGCRT429_NOSEQ     | This message will not get recorded in the Sequential Record                   | x   | x   |
| MSGCRT429_SEQ       | This message will get recorded in the Sequential Record                       | x   | x   |
| MSGCRT429_NOLOG     | This message will not create an entry in the Event Log List                   | x   | x   |
| MSGCRT429_LOG       | This message will create an entry in the Event Log List                       | x   | x   |
| MSGCRT429_NOTIMETAG | This message will not record a time-tag                                       | x   | x   |
| MSGCRT429_TIMETAG   | This message will record a time-tag                                           | x   | x   |
| MSGCRT429_NOELAPSE  | This message will not record an Elapsed Time                                  | x   | x   |
| MSGCRT429_ELAPSE    | This message will record an Elapsed Time                                      | x   | x   |
| MSGCRT429_NOMAXMIN  | This message will not record maximum and minimum repetition rates             | x   | x   |
| MSGCRT429_MAX       | This message will record maximum repetition rates                             | x   | x   |
| MSGCRT429_MIN       | This message will record minimum repetition rates                             | x   | x   |
| MSGCRT429_MAXMIN    | This message will record maximum and minimum repetition rates                 | x   | x   |
| MSGCRT429_NOHIT     | This message will not record a Hit Counter                                    | x   | x   |
| MSGCRT429_HIT       | This message will record a Hit Counter (can't have Hit Counter with time-tag, | x   | x   |
|                     | elapse, or max/min timing)                                                    |     |     |
| MSGCRT429_NOSKIP    | This message will not be skipped                                              | x   | x   |
| MSGCRT429_SKIP      | This message will be skipped, and none of the options will be processed       | x   | x   |
| MSGCRT429_NOSYNC    | This message will not generate a SYNCOUT signal                               |     | x   |
| MSGCRT429_SYNC      | This message will generate a SYNCOUT signal                                   |     | x   |
| MSGCRT429_NOEXTRIG  | This message will be triggered immediately                                    |     | x   |
| MSGCRT429_EXTRIG    | This message will wait for an EXTRIG* pulse to be triggered                   |     | x   |
| MSGCRT429_NOERR     | This message will not have a parity error                                     |     | x   |
| MSGCRT429_PARERR    | This message will have a parity error                                         |     | x   |
| MSGCRT429_WIPE      | The data fields of this message will initially be wiped to a value            | x   | x   |
| MSGCRT429_NOWIPE    | The data fields of this message will initially be left unchanged              | x   | x   |
| MSGCRT429_WIPE0     | The data fields of this message will be wiped with a value of zeros (this     | x   | x   |
|                     | option does not get used if MSGCRT429_NOWIPE is used)                         |     |     |
| MSGCRT429_WIPE1     | The data fields of this message will be wiped with a value of ones (this      | x   | x   |
|                     | option does not get used if MSGCRT429_NOWIPE is used)                         |     |     |

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FilterDefault, BTI429\_FilterRd, BTI429\_FilterWr

### **FilterWr**

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

#### **DESCRIPTION**

Writes the message address (*message*) of the Message Record into the Filter Table position specified by *channel*, *label*, and *sdi*. The valid range of *sdi* is 0-3.

This function is most useful to assign multiple labels to point to one Message Record. After calling BTI429\_FilterSet for the first label of interest, this function could be called with the message address that was returned by BTI429\_FilterSet for each of the remaining labels. This would point all the labels of interest to one Message Record.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTI429_FilterRd, BTI429_FilterSet, BTI429_FilterDefault
```
### **FldGetData**

```
ULONG BTI429_FldGetData
(
      ULONG msg //32-bit ARINC 429 word
)
```
#### **RETURNS**

The 24-bit field including both the data and parity of an ARINC 429 word.

#### **DESCRIPTION**

Extracts the 23-bit data and 1-bit parity fields of the ARINC 429 word in *msg*. The extracted 24-bit field is right-shifted and zero-filled as shown below:

![](_page_92_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldPutData

### **FldGetLabel**

USHORT BTI429\_FldGetLabel ( ULONG *msg //32-bit ARINC 429 word* )

#### **RETURNS**

The 8-bit label field of an ARINC 429 word.

#### **DESCRIPTION**

Extracts the 8-bit label field by masking the ARINC 429 word in *msg* as shown below:

![](_page_93_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldPutLabel

### **FldGetParity**

USHORT BTI429\_FldGetParity ( ULONG *msg //32-bit ARINC 429 word* )

#### **RETURNS**

The parity bit of an ARINC 429 word.

#### **DESCRIPTION**

Extracts the parity bit of the ARINC 429 word in *msg*. The extracted parity bit is right-shifted and zero-filled as shown below:

![](_page_94_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldGetData

### **FldGetSDI**

USHORT BTI429\_FldGetSDI ( ULONG *msg //32-bit ARINC 429 word* )

#### **RETURNS**

The 2-bit SDI field of an ARINC 429 word.

#### **DESCRIPTION**

Extracts the 2-bit SDI field of the ARINC 429 word in msg. The extracted SDI field is right-shifted and zero-filled as shown below:

![](_page_95_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldPutSDI

### **FldGetValue**

```
ULONG BTI429_FldGetValue
(
     ULONG msg, //32-bit ARINC 429 word
     USHORT startbit, //Starting bit number of BCD field
     USHORT endbit //Ending bit number of BCD field
)
```
#### **RETURNS**

The specified field of an ARINC 429 word.

#### **DESCRIPTION**

Extracts the value of a specified a bit field from the ARINC 429 word in *msg*. *startbit* (zero-based) and *endbit* (zero-based) determine the lowest and highest bit position of the field to extract. The extracted field is right-shifted and zero-filled.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldPutValue

### **FldPutData**

```
ULONG BTI429_FldPutData
(
      ULONG msg, //32-bit ARINC 429 word
      ULONG data //New 23-bit data field value
)
```
#### **RETURNS**

The new 32-bit ARINC 429 word with the data field inserted.

#### **DESCRIPTION**

Inserts a 24-bit field including both data and parity into the ARINC 429 word in *msg*. *data* is leftshifted and packed into *msg* as shown below:

![](_page_97_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldGetData

### **FldPutLabel**

```
ULONG BTI429_FldPutLabel
(
      ULONG msg, //32-bit ARINC 429 word
      USHORT label //New 8-bit label field value
)
```
### **RETURNS**

The new 32-bit ARINC 429 word with the label field inserted.

#### **DESCRIPTION**

Inserts an 8-bit label field value into the ARINC 429 word in *msg*. *label* is packed into *msg* as shown below:

![](_page_98_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldGetLabel

### **FldPutSDI**

```
ULONG BTI429_FldPutSDI
(
      ULONG msg, //32-bit ARINC 429 word
      USHORT sdi //New 2-bit SDI field value
)
```
#### **RETURNS**

The new 32-bit ARINC 429 word with the SDI field inserted.

#### **DESCRIPTION**

Inserts a 2-bit SDI field value into the ARINC 429 word in *msg*. *sdi* is left-shifted and packed into *msg* as shown below:

![](_page_99_Figure_8.jpeg)

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldGetSDI

### **FldPutValue**

```
ULONG BTI429_FldPutValue
(
     ULONG msg, //32-bit ARINC 429 word
     ULONG data, //New 23-bit data field value
     USHORT startbit, //Starting bit position of field
     USHORT endbit //Ending bit position of field
)
```
#### **RETURNS**

The new 32-bit ARINC 429 word with the specified field inserted.

#### **DESCRIPTION**

Inserts a bit field value into the ARINC 429 word in *msg*. *startbit* (zero-based) and *endbit* (zerobased) specify the low and high bit positions of the field.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_FldGetValue

### **ListAsyncCreate**

```
LISTADDR BTI429_ListAsyncCreate
(
     ULONG ctrlflags, //Selects list options
     INT count, //One more than the number of entries in list
     INT channel, //Channel number to associate with List Buffer
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The Device address of the list if successful, otherwise zero.

#### **DESCRIPTION**

Creates an asynchronous transmit List Buffer the size of *count* entries. The List Buffer is connected with the channel specified by *channel*. Every time a gap is encountered in a transmit Schedule, if data exists in the list, it will be transmitted during the gap time. If data does not exist in the list, then the gap time is

executed instead (gap times are processed in 36 bit-time increments, which is the size of a word plus a minimum gap). The maximum number of entries that may be stored in the list is *count* - 1 for FIFO mode and *count* for Circular mode.

*ctrlflags* specifies what type of List Buffer is created and the options associated with the List Buffer. Only FIFO mode is valid for an asynchronous List Buffer. ARINC 429 words are passed to the asynchronous List Buffer with BTI429\_ListDataWr and are transmitted only once.

| ctrlflags           |                                                                                       |  |
|---------------------|---------------------------------------------------------------------------------------|--|
| Constant            | Description                                                                           |  |
| LISTCRT429_DEFAULT  | Select all default settings (bold below)                                              |  |
| LISTCRT429_FIFO     | Selects FIFO mode                                                                     |  |
| LISTCRT429_PINGPONG | Selects ping-pong mode                                                                |  |
| LISTCRT429_CIRCULAR | Selects circular mode                                                                 |  |
| LISTCRT429_NOLOG    | An entry will not be created in the Event Log List when the List Buffer is empty/full |  |
| LISTCRT429_LOG      | An entry will be created in the Event Log List when the List Buffer is empty/full     |  |

#### **DEVICE DEPENDENCY**

3G and 4G Devices support up to 16,376 list entries while 5G, 5G+, and 6G Devices support up to 32,764 list entries.

#### **WARNINGS**

Normally, this function should be used to associate an asynchronous List Buffer with a transmit channel before the Schedule is created for that channel. Do not use BTI429\_MsgDataRd and BTI429\_MsgDataWr as they will return incorrect results.

```
BTI429_ListRcvCreate, BTI429_ListXmtCreate, BTI429_ListDataWr, 
BTI429_ListDataRd
```
### **ListDataBlkRd**

```
BOOL BTI429_ListDataBlkRd
(
     ULONG data[], //Pointer to destination buffer
     LPUSHORT datacount, //Size of destination, number of words read
     LISTADDR list, //List from which to read data
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A non-zero value if the function succeeded, or zero if unable to read the list.

#### **DESCRIPTION**

Reads multiple 32-bit ARINC messages from the *list* and automatically updates the pointers for the next message. It is similar to BTI429\_ListDataRd except it reads multiple messages from the list in a single operation. *data* points to the buffer to receive the data words.

When called, *datacount* must point to a variable that contains maximum number of 32-bit ARINC messages that *data* can hold. On return, the variable will contain the number of 32-bit ARINC messages written to *data*.

The *list* must have been returned when the list was created using BTI429\_ListRcvCreate.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Function fails if the list is in Ping-Pong mode.

```
BTI429_ListDataBlkWr, BTI429_ListDataRd, BTI429_MsgDataRd, 
BTI429_ListRcvCreate, BTI429_ListXmtCreate
```
### **ListDataBlkWr**

```
BOOL BTI429_ListDataBlkWr
(
     ULONG data[], //Pointer to source buffer
     LPUSHORT datacount, //Size of source, number of words written
     LISTADDR list, //List to write data to
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A non-zero value if the function succeeded, or zero if unable to wrote to the list.

#### **DESCRIPTION**

Writes multiple 32-bit ARINC messages to the *list* and automatically updates the pointers for the next message. It is similar to BTI429\_ListDataWr except it writes multiple messages to the *list* in a single operation. *data* points to the buffer containing the data words to write.

When called, *datacount* must contain the total number of 32-bit data words that *data* contains.

The *list* must have been returned when the *list* was created using BTI429\_ListXmtCreate or BTI429\_ListAsyncCreate.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Function fails if the list is in Ping-Pong mode.

```
BTI429_ListDataBlkRd, BTI429_ListDataWr, BTI429_MsgDataWr, 
BTI429_ListRcvCreate, BTI429_ListXmtCreate
```
### **ListDataRd**

```
ULONG BTI429_ListDataRd
(
     LISTADDR list, //List Buffer from which to read data
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The 32-bit value of the ARINC word if there is a word in the list, or zero if it is empty.

#### **DESCRIPTION**

This function reads one message from the list, and automatically updates the pointers for the next message. It is similar to BTI429\_MsgDataRd except it reads from a List Buffer. The *list* parameter is the value returned when the List Buffer was created using BTI429\_ListRcvCreate. The position of the message to be read is determined by the mode of the list as follows:

- Circular mode: Not valid for a receive List Buffer.
- FIFO mode: Reads the oldest complete message received.
- Ping-Pong mode: Reads the newest complete message received.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*list* must be configured as a receive List Buffer using BTI429\_ListRcvCreate.

```
BTI429_MsgDataRd, BTI429_ListDataWr, BTI429_ListRcvCreate, 
BTI429_ListXmtCreate
```
### **ListDataWr**

```
BOOL BTI429_ListDataWr
(
     ULONG value, //32-bit ARINC word value
     LISTADDR list, //List to write new data
     HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the operation was successful, or FALSE if an error occurred or the function was unable to write to the list.

#### **DESCRIPTION**

This function writes one message to the list and automatically updates the pointers for the next message. It is similar to BTI429\_MsgDataWr except it writes the 32-bit ARINC data value specified by *value* to a List Buffer. The *list* parameter is the value returned when the List Buffer was created using BTI429\_ListXmtCreate or BTI429\_ListAsyncCreate. The position to which the message is written in the list is determined by the mode of the list as follows:

- Circular mode: This mode is intended as a preloaded value List Buffer. With a circular List Buffer, this function will write to the next available position and will overwrite data in the buffer when it wraps around.
- FIFO mode: Data is written to one end of the list and is transmitted and removed from the other end of list. This function returns a zero if the List Buffer is full.
- Ping-Pong mode: When writing is complete, the data will be used for the next message transmission.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

*list* must be configured as a write transmit List Buffer using either BTI429\_ListXmtCreate or BTI429\_ListAsyncCreate

```
BTI429_MsgDataWr, BTI429_ListDataRd, BTI429_ListRcvCreate, 
BTI429_ListXmtCreate, BTI429_ListAsyncCreate
```
### **ListRcvCreate**

```
LISTADDR BTI429_ListRcvCreate
(
     ULONG ctrlflags, //Selects list options
     INT count, //One more than the number of entries in list
     MSGADDR message, //Message address to associate with List Buffer
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The Device address of the list if successful, otherwise zero.

#### **DESCRIPTION**

Creates a receive List Buffer the size of *count* entries. The List Buffer is connected with a Message Record so that the data is processed in the list instead of in the Message Record. The *ctrlflags* specify what type of List Buffer is created and the options associated with the List Buffer. Only FIFO and ping-pong modes are applicable to a receive List Buffer. The maximum number of entries that may be stored in the list is *count* - 1 for FIFO mode and *count* for Circular mode.

| ctrlflags           |                                                                                       |  |
|---------------------|---------------------------------------------------------------------------------------|--|
| Constant            | Description                                                                           |  |
| LISTCRT429_DEFAULT  | Select all default settings (bold below)                                              |  |
| LISTCRT429_FIFO     | Selects FIFO mode                                                                     |  |
| LISTCRT429_PINGPONG | Selects ping-pong mode                                                                |  |
| LISTCRT429_CIRCULAR | Selects circular mode                                                                 |  |
| LISTCRT429_NOLOG    | An entry will not be created in the Event Log List when the List Buffer is empty/full |  |
| LISTCRT429_LOG      | An entry will be created in the Event Log List when the List Buffer is empty/full     |  |

#### **DEVICE DEPENDENCY**

LISTCRT429\_PINGPONG is not supported by 5G, 5G+, and 6G Devices since they inherently have protection for data coherency (use BTI429\_MsgCommRd and BTI429\_MsgCommWr instead). If LISTCRT429\_PINGPONG is used against 5G and 6G Devices the API call will fail and returns LISTADDR of NULL.

3G and 4G Devices support up to 16,376 list entries while 5G, 5G+, and 6G Devices support up to 32,764 list entries.

#### **WARNINGS**

After connecting *message* with a List Buffer, the functions BTI429\_ListDataRd and BTI429\_ListDataWr must be used to read or write the data. Do not use BTI429\_MsgDataRd and BTI429\_MsgDataWr as they will return incorrect results.

```
BTI429_ListXmtCreate, BTI429_ListAsyncCreate, BTI429_ListDataWr, 
BTI429_ListDataRd
```
### **ListStatus**

```
INT BTI429_ListStatus
(
     LISTADDR list, //Address of the List Buffer
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The status value of the specified List Buffer.

#### **DESCRIPTION**

Checks the status of the List Buffer specified by *list*. The status value can be tested using the predefined constants below:

| Constant     | Description                     |
|--------------|---------------------------------|
| STAT_EMPTY   | List Buffer is empty            |
| STAT_PARTIAL | List Buffer is partially filled |
| STAT_FULL    | List Buffer is full             |
| STAT_OFF     | List Buffer is off              |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTI429_ListAsyncCreate, BTI429_ListDataRd, BTI429_ListDataWr, 
BTI429_ListRcvCreate, BTI429_ListXmtCreate
```
### **ListXmtCreate**

```
LISTADDR BTI429_ListXmtCreate
(
     ULONG ctrlflags, //Selects list options
     INT count, //One more than the number of entries in list
     MSGADDR message, //Message address to associate with List Buffer
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The Device address of the list if successful, otherwise zero.

#### **DESCRIPTION**

Creates a transmit List Buffer the size of *count* entries. The List Buffer is connected with a Message Record so that the data is processed in the list instead of in the Message Record. The *ctrlflags* specify what type of List Buffer is created and the options associated with the List Buffer. The maximum number of entries that may be stored in the list is *count* - 1 for FIFO mode and *count* for Circular mode.

| ctrlflags           |                                                                                       |  |
|---------------------|---------------------------------------------------------------------------------------|--|
| Constant            | Description                                                                           |  |
| LISTCRT429_DEFAULT  | Select all default settings (bold below)                                              |  |
| LISTCRT429_FIFO     | Selects FIFO mode                                                                     |  |
| LISTCRT429_PINGPONG | Selects ping-pong mode                                                                |  |
| LISTCRT429_CIRCULAR | Selects circular mode                                                                 |  |
| LISTCRT429_NOLOG    | An entry will not be created in the Event Log List when the List Buffer is empty/full |  |
| LISTCRT429_LOG      | An entry will be created in the Event Log List when the List Buffer is empty/full     |  |

#### **DEVICE DEPENDENCY**

LISTCRT429\_PINGPONG is not supported by 5G, 5G+, and 6G Devices since they inherently have protection for data coherency (use BTI429\_MsgCommRd and BTI429\_MsgCommWr instead). If LISTCRT429\_PINGPONG is used against 5G and 6G Devices the API call will fail and returns LISTADDR of NULL.

3G and 4G Devices support up to 16,376 list entries while 5G, 5G+, and 6G Devices support up to 32,764 list entries.

#### **WARNINGS**

After connecting *message* with a List Buffer, the functions BTI429\_ListDataRd and BTI429\_ListDataWr must be used to read or write the data. Do not use BTI429\_MsgDataRd and BTI429\_MsgDataWr as they will return incorrect results.

```
BTI429_ListRcvCreate, BTI429_ListAsyncCreate BTI429_ListDataWr, 
BTI429_ListDataRd
```
### **MsgBlockRd**

```
MSGADDR BTI429_MsgBlockRd
(
     LPMSGFIELDS429 msgfields, //Pointer to destination structure
     MSGADDR message, //Message from which to read
     HCORE hCore //Core handle
```
)

#### **RETURNS**

The address of the message structure that was read.

#### **DESCRIPTION**

Reads an entire Message Record from the Device.

| MSGFIELDS429 structure |        |                                                                                                                           |  |
|------------------------|--------|---------------------------------------------------------------------------------------------------------------------------|--|
| Field                  | Size   | Description                                                                                                               |  |
| msgopt                 | USHORT | Message options fields. Do not modify these fields.                                                                       |  |
| msgact                 | USHORT | Message activity. See table below for detail.                                                                             |  |
| msgdata                | ULONG  | Message data value. A 32-bit ARINC 429 data word value.                                                                   |  |
| listptr                | ULONG  | List Buffer pointer. Used instead of msgdata when in List Buffer mode.                                                    |  |
| timetag                | ULONG  | Lower 32 bits of the time-tag value.                                                                                      |  |
| hitcount               | ULONG  | Hit Counter value. Used instead of time-tag when in Hit Counter mode.                                                     |  |
| maxtime                | ULONG  | Maximum repetition rate. 32 bits with resolution equal to time-tag resolution.                                            |  |
| elapsetime             | ULONG  | Elapsed time. 32 bits with resolution equal to time-tag resolution. Used instead of<br>maxtime when in Elapsed Time mode. |  |
| mintime                | ULONG  | Minimum repetition rate. 32 bits with resolution equal to time-tag resolution.                                            |  |
| userptr                | ULONG  | Reserved                                                                                                                  |  |
| timetagh               | ULONG  | Upper 32 bits of the time-tag value                                                                                       |  |
| decgap                 | USHORT | Measured decoder gap time (4 bits)                                                                                        |  |
| paramflag              | USHORT | Parametric bitrate configuration (includes bitcount)                                                                      |  |

The msgact field may be tested by AND-ing the values returned with the constants from the following table:

| msgact field      |                                                                                             |  |  |  |
|-------------------|---------------------------------------------------------------------------------------------|--|--|--|
| Constant          | Description                                                                                 |  |  |  |
| MSGACT429_CHMASK  | The channel number mask value. Shift the result right with MSGACT429_CHSHIFT.               |  |  |  |
| MSGACT429_CHSHIFT | Channel number shift value. See CHMASK above.                                               |  |  |  |
| MSGACT429_SPD     | This bit reflects the speed detected. A one signifies high speed (100 Kbps), and a zero     |  |  |  |
|                   | signifies low speed (12.5 Kbps).                                                            |  |  |  |
| MSGACT429_ERR     | If set, it signifies that an error occurred in receiving this word. The type of error is    |  |  |  |
|                   | defined by the following bits.                                                              |  |  |  |
| MSGACT429_GAP     | Gap Error. A gap of less than four bit times preceded the word.                             |  |  |  |
| MSGACT429_PAR     | Parity error. A parity error was detected in the word.                                      |  |  |  |
| MSGACT429_LONG    | Long word error. A word of more than 32 bits was detected.                                  |  |  |  |
| MSGACT429_BIT     | Bit timing error. An error occurred while decoding the bits of the word (short bits or long |  |  |  |
|                   | bits).                                                                                      |  |  |  |
| MSGACT429_TO      | Time out error. The decoder timed out while receiving a word (short word).                  |  |  |  |
| MSGACT429_HIT     | Signifies that the message has been processed by the firmware (the Hit bit).                |  |  |  |

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

Message field *paramflag* only applies to 5G+ and 6G Devices; *rsvd* for all other Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgBlockWr, BTI429\_MsgCommRd BTI429\_MsgDataRd, BTI429\_MsgDataWr, BTI429\_FilterSet, BTI429\_FilterDefault

### **MsgBlockWr**

```
MSGADDR BTI429_MsgBlockWr
(
     LPMSGFIELDS429 msgfields, //Pointer to source structure
     MSGADDR message, //Message to write to
     HCORE hCore //Core handle
)
```
**RETURNS**

The address of the message structure that was written.

#### **DESCRIPTION**

Writes an entire Message Record to the Device. This function is used to modify certain fields in a Message Record after the Message Record was read using BTI429\_MsgBlockRd. Only the msgdata, hitcount, mintime, maxtime, and paramflag fields should be modified. All other fields should be restored to the value read. See BTI429\_MsgBlockRd for a list of the Message Record fields.

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

Message field paramflag only applies to 6G Devices; rsvd for all other Devices.

#### **WARNINGS**

Do not modify any fields other than the five listed above.

5G+ and 6G Devices must use BTI429\_ParamBitCountBuild when writing *bitcount* to *paramflag*. Failure to use BTI429\_ParamBitCountBuild will result in a default of 32 data word bits.

```
BTI429_MsgBlockRd, BTI429_MsgCommWr, BTI429_MsgDataRd, BTI429_MsgDataWr
BTI429_MsgCreate, BTI429_FilterSet, BTI429_FilterDefault, 
BTI429_ParamBitCountBuild
```
### **MsgCountRd**

```
INT BTI429_MsgCountRd
(
      MSGADDR message, //Message from which to read
      HCORE hCore //Core handle
)
```
#### **RETURNS**

*bitcount* value from the *paramflag* field of the Message Record.

#### **DESCRIPTION**

Reads the number of valid ARINC 429 data word bits from the Message Record specified by *message*. This value indicates the number of bits the channel will transmit if parametric bit count is supported by the hardware.

#### **DEVICE DEPENDENCY**

Applies to transmit channels of 5G+ and 6G Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgCountWr, BTI429\_MsgDataCountWr, BTI429\_MsgBlockRd

### **MsgCountWr**

```
ERRVAL BTI429_MsgCountWr
(
      INT bitcount, //Number of bits of data to transmit (1-32)
      MSGADDR message, //Message to receive new data and bitcount
      HCORE hCore //Core handle
)
```
**RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes the *bitcount* value into the *paramflag* field of the Message Record specified by *message*. *bitcount* indicates the number of ARINC 429 data word bits the channel will transmit for applications of less than 32 bits.

#### **DEVICE DEPENDENCY**

Applies to transmit channels of 5G+ and 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgCountRd, BTI429\_MsgDataCountWr, BTI429\_MsgBlockRd

### **MsgCommRd**

```
MSGADDR BTI429_MsgCommRd
(
     LPMSGFIELDS429 msgfields, //Pointer to destination structure
     MSGADDR message, //Message from which to read
     HCORE hCore //Core handle
```
)

#### **RETURNS**

The address of the message structure that was read.

#### **DESCRIPTION**

Reads an entire message structure from the core. Similar to BTI429\_MsgBlockRd, except it uses non-contended accesses of Device memory. See BTI429\_MsgBlockRd for a list of the Message Record fields.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTI429_MsgBlockRd, BTI429_MsgCommWr, BTI429_MsgDataRd, BTI429_MsgDataWr, 
BTI429_FilterSet, BTI429_FilterDefault
```
### **MsgCommWr**

```
MSGADDR BTI429_MsgCommWr
(
     LPMSGFIELDS429 msgfields, //Pointer to source structure
     MSGADDR message, //Message to write to
     HCORE hCore //Core handle
)
```
**RETURNS**

The address of the message structure that was written.

#### **DESCRIPTION**

Writes an entire message structure to the core. Similar to BTI429\_MsgBlkWr, except it uses noncontended accesses of Device memory. This function is used to modify certain fields in a Message Record after the Message Record was read using BTI429\_MsgBlockRd. Only the *msgdata*, *hitcount*, *mintime*, *maxtime*, and *paramflag* fields should be modified. All other fields should be restored to the value read. See BTI429\_MsgBlockRd for a list of the Message Record fields.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

Message field *paramflag* only applies to 6G Devices; *rsvd* for all other Devices.

#### **WARNINGS**

Do not modify any fields other than the five listed above.

5G+ and 6G Devices must use BTI429\_ParamBitCountBuild when writing *bitcount* to *paramflag*. Failure to use BTI429\_ParamBitCountBuild will result in a default of 32 data word bits.

```
BTI429_MsgCommRd, BTI429_MsgBlockWr, BTI429_MsgDataWr, BTI429_MsgCreate
BTI429_FilterSet, BTI429_FilterDefault, BTI429_ParamBitCountBuild
```
### **MsgCreate**

```
MSGADDR BTI429_MsgCreate
(
      ULONG ctrlflags, //Selects message options
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The Device address of the Message Record if successful, otherwise zero.

#### **DESCRIPTION**

Allocates memory for a Message Record and initializes the record with the options specified in *ctrlflags*. The options that can be used in *ctrlflags* are listed below. Please note that only the transmitter options can be used with this function.

| ctrlflags           |                                                                                  |     |     |  |
|---------------------|----------------------------------------------------------------------------------|-----|-----|--|
| Constant            | Description                                                                      | Rcv | Xmt |  |
| MSGCRT429_DEFAULT   | Select all default settings (bold below)                                         | x   | x   |  |
| MSGCRT429_NOSEQ     | This message will not get recorded in the Sequential Record                      | x   | x   |  |
| MSGCRT429_SEQ       | This message will get recorded in the Sequential Record                          | x   | x   |  |
| MSGCRT429_NOLOG     | This message will not create an entry in the Event Log List                      | x   | x   |  |
| MSGCRT429_LOG       | This message will create an entry in the Event Log List                          | x   | x   |  |
| MSGCRT429_NOTIMETAG | This message will not record a time-tag                                          | x   | x   |  |
| MSGCRT429_TIMETAG   | This message will record a time-tag                                              | x   | x   |  |
| MSGCRT429_NOELAPSE  | This message will not record an Elapsed Time                                     | x   | x   |  |
| MSGCRT429_ELAPSE    | This message will record an Elapsed Time                                         | x   | x   |  |
| MSGCRT429_NOMAXMIN  | This message will not record maximum and minimum repetition rates                | x   | x   |  |
| MSGCRT429_MAX       | This message will record maximum repetition rates                                | x   | x   |  |
| MSGCRT429_MIN       | This message will record minimum repetition rates                                | x   | x   |  |
| MSGCRT429_MAXMIN    | This message will record maximum and minimum repetition rates                    | x   | x   |  |
| MSGCRT429_NOHIT     | This message will not record a Hit Counter                                       | x   | x   |  |
| MSGCRT429_HIT       | This message will record a Hit Counter (can't have Hit Counter with time-tag,    | x   | x   |  |
|                     | elapse, or max/min timing)                                                       |     |     |  |
| MSGCRT429_NOSKIP    | This message will not be skipped                                                 | x   | x   |  |
| MSGCRT429_SKIP      | This message will be skipped, and none of the options will be processed          | x   | x   |  |
| MSGCRT429_NOSYNC    | This message will not generate a SYNCOUT signal                                  |     | x   |  |
| MSGCRT429_SYNC      | This message will generate a SYNCOUT signal                                      |     | x   |  |
| MSGCRT429_NOEXTRIG  | This message will be triggered immediately                                       |     | x   |  |
| MSGCRT429_EXTRIG    | This message will wait for an EXTRIG* pulse to be triggered                      |     | x   |  |
| MSGCRT429_NOERR     | This message will not have a parity error                                        |     | x   |  |
| MSGCRT429_PARERR    | This message will have a parity error                                            |     | x   |  |
| MSGCRT429_WIPE      | This data fields of this message will initially be wiped to a value              | x   | x   |  |
| MSGCRT429_NOWIPE    | The data fields of this message will initially be left unchanged                 | x   | x   |  |
| MSGCRT429_WIPE0     | The data fields of this message will be wiped with a value of zeros (this option | x   | x   |  |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |  |
| MSGCRT429_WIPE1     | The data fields of this message will be wiped with a value of ones (this option  | x   | x   |  |
|                     | does not get used if MSGCRT429_NOWIPE is used)                                   |     |     |  |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgDataRd, BTI429\_MsgDataWr, BTI429\_MsgBlockRd, BTI429\_MsgBlockWr BTI429\_MsgCommRd, BTI429\_MsgCommWr

### **MsgDataCountWr**

```
ERRVAL BTI429_MsgDataCountWr
(
      ULONG dataval, //Value of data to write to message
      INT bitcount, //Number of bits of dataval to transmit (1-32)
      MSGADDR message, //Message to receive new data and bitcount
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes the 32-bit ARINC data value specified by *dataval* into the Message Record specified by *message*. Also writes the *bitcount* value into the *paramflag* field of the Message Record specified by *message*. *bitcount* indicates the number of ARINC 429 data word bits the channel will transmit for applications of less than 32 bits.

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgCountRd, BTI429\_MsgDataRd, BTI429\_MsgBlockRd

### **MsgDataRd**

```
ULONG BTI429_MsgDataRd
(
      MSGADDR message, //Message from which to read
      HCORE hCore //Core handle
)
```
#### **RETURNS**

32-bit value of the ARINC data word.

#### **DESCRIPTION**

Reads the 32-bit value of the ARINC data word from the Message Record specified by *message*.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTI429_MsgDataWr, BTI429_MsgCreate, BTI429_MsgBlockRd, BTI429_MsgCommRd
BTI429_FilterSet, BTI429_FilterDefault
```
### **MsgDataWr**

```
VOID BTI429_MsgDataWr
(
     ULONG value, //Value of data to write to message
     MSGADDR message, //Message to receive new data
     HCORE hCore //Core handle
```
)

#### **RETURNS**

None.

#### **DESCRIPTION**

Writes the 32-bit ARINC data value specified by value into the Message Record specified by message.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

```
BTI429_MsgDataRd, BTI429_MsgDataCountWr, BTI429_MsgCountWr, 
BTI429_MsgCreate, BTI429_MsgBlockRd, BTI429_MsgCommWr, BTI429_MsgBlockWr
```
### **MsgGroupBlockRd**

```
VOID BTI429_MsgGroupBlockRd
(
     MSGFIELDS429 msgfields[], //Array of destination structures
     MSGADDR msgs[], //Array of message addresses
     INT nummsgs, //Number of messages to read
     HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Reads a series of Message Records from the Device. *messages* points to an array of message addresses. The contents of each message in *messages* is written to its respective entry in *msgfields*. There must be *numsgs* entries in both *messages* and *msgfields*. See BTI429\_MsgBlockRd for a list of the Message Record fields.

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

None.

```
BTI429_MsgBlockWr, BTI429_MsgCommRd, BTI429_MsgDataRd, 
BTI429_MsgGroupBlockWr, BTI429_MsgDataWr, BTI429_FilterSet, 
BTI429_FilterDefault
```
### **MsgGroupBlockWr**

```
VOID BTI429_MsgGroupBlockWr
(
     LPMSGFIELDS429 msgfields, //Pointer to source structures
     MSGADDR msgs[], //Messages to write to
     INT nummsgs, //Number of messages to write
     HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Writes a series of Message Records to the Device. *messages* points to an array of message addresses. Each entry in *msgfields* is written to its respective message in *messages*. There must be *numsgs* entries in both *msgs* and *msgfields*.

This function is used to modify certain fields in Message Records after the Message Records were read using BTI429\_MsgGroupBlockRd or BTI429\_MsgBlockRd. Only the *msgdata*, *hitcount*, *mintime*, *maxtime*, and *paramflag* fields should be modified. All other fields should be restored to the value read. See BTI429\_MsgBlockRd for a list of the Message Record fields.

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

Message field *paramflag* only applies to 5G+ and 6G Devices; *rsvd* for all other Devices.

#### **WARNINGS**

Do not modify any fields other than the five listed above.

5G+ and 6G Devices must use BTI429\_ParamBitCountBuild when writing *bitcount* to *paramflag*. Failure to use BTI429\_ParamBitCountBuild will result in a default of 32 data word bits.

```
BTI429_MsgGroupBlockRd, BTI429_MsgBlockRd, BTI429_MsgDataRd, 
BTI429_MsgDataWr, BTI429_MsgCreate, BTI429_FilterSet, 
BTI429_FilterDefault, BTI429_ParamBitCountBuild
```
### **MsgGroupRd**

```
VOID BTI429_MsgGroupRd
(
     ULONG msgdata[], //Pointer to destination array 
     MSGADDR msgs[], //Array of Messages to read from
     INT nummsgs, //Number of messages to read
     HCORE hCore //Core handle
)
```
**RETURNS**

None.

#### **DESCRIPTION**

Reads the 32-bit ARINC data word from multiple Message Records in a single operation. It is similar to BTI429\_MsgDataRd except it reads *nummsgs* Message Records instead of just one Message Record. The array *msgs* points to the message addresses to read from. The array *msgdata* points to the memory to receive the 32-bit ARINC data word of each message.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgGroupWr, BTI429\_MsgDataRd

### **MsgGroupWr**

```
VOID BTI429_MsgGroupWr
(
     ULONG msgdata[], //Pointer to source array 
     MSGADDR msgs[], //Array of Messages to write to
     INT nummsgs, //Number of messages to write
     HCORE hCore //Core handle
)
```
### **RETURNS**

None.

### **DESCRIPTION**

Writes the 32-bit ARINC data word to multiple Message Records in a single operation. It is similar to BTI429\_MsgDataWr except it writes *nummsgs* Message Records instead of just one Message Record. The array *msgdata* points to the 32-bit ARINC data words to write. The array *msgs* points to message addresses to write to.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgGroupRd, BTI429\_MsgDataWr

### **MsgIsAccessed**

```
BOOL BTI429_MsgIsAccessed
(
      MSGADDR message, //Message to test if it has been accessed
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if a message has been accessed (if the Hit bit is set), otherwise FALSE.

#### **DESCRIPTION**

This function indicates that a Message Record has been processed by either transmission or reception of a message. BTI429\_MsgIsAccessed returns the value of the Hit bit, then clears it.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTI429_MsgDataRd, BTI429_MsgCreate, BTI429_MsgBlockRd, BTI429_MsgCommRd, 
BTI429_MsgBlockWr, BTI429_FilterSet, BTI429_FilterDefault
```
### **MsgMultiSkipWr**

```
VOID BTI429_MsgMultiSkipWr
(
     BOOL skip[], //Array of skip values to write
     MSGADDR message[], //Array of message addresses
     INT count, //Number of messages to update
     HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if all messages were successfully updated, otherwise FALSE.

#### **DESCRIPTION**

Writes the state of the skip bit for each of the Message Records specified by *message*. If the skip bit is FALSE (zero), the message will be processed as normal. If the bit is TRUE (non-zero), the message will be skipped and will not be processed.

The skip state of the nth element of the *message* array is set by the nth element of the *skip* array.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgSkipRd, BTI429\_MsgSkipWr

### **MsgSkipRd**

```
BOOL BTI429_MsgSkipRd
(
      MSGADDR message, //Message to read from
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The state of the skip bit for a message.

#### **DESCRIPTION**

Reads the state of the skip bit for the Message Record specified by *message*. If the bit is zero, the message will be processed as normal. If the bit is non-zero, the message will be skipped and will not be processed.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgSkipRd, BTI429\_MsgMultiSkipWr

### **MsgSkipWr**

```
VOID BTI429_MsgSkipWr
(
     BOOL skip, //Skip value to write
     MSGADDR message, //Message to write to
     HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Writes the state of the skip bit for the Message Record specified by *message*. If the bit is zero, the message will be processed as normal. If the bit is non-zero, the message will be skipped and will not be processed.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgSkipRd, BTI429\_MsgMultiSkipWr

### **MsgSyncDefine**

```
ERRVAL BTI429_MsgSyncDefine
(
      BOOL enable, //Enable/disable external sync pulse
      USHORT syncmask, //Line(s) used for sync output
      USHORT pinpolarity,//Active pin polarity (high/low)
      MSGADDR message, //Transmit message to drive sync pulse
      HCORE hCore //Core handle
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Defines the sync output conditions for a transmit message specified by *message* and configures it to output a sync signal. This sync signal appears on the line(s) specified by *syncmask* with the polarity specified by *pinpolarity* (see tables below). BTI429\_MsgSyncDefine may be called during run time to redefine the sync output settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Sync outputs are both generation dependent and device dependent. Refer to the Device Dependency table and the following tables for more information.

| syncmask          |                               |    |    |     |    |
|-------------------|-------------------------------|----|----|-----|----|
| Constant          | Description                   | 4G | 5G | 5G+ | 6G |
| SYNCMASK_SYNCA    | Selects discrete sync line A  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGA | Selects PXIe TRIG sync line A |    |    |     | *  |
| SYNCMASK_SYNCB    | Selects discrete sync line B  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGB | Selects PXIe TRIG sync line B |    |    |     | *  |
| SYNCMASK_SYNCC    | Selects discrete sync line C  | x  | x  | x   | x  |
| SYNCMASK_PXITRIGC | Selects PXIe TRIG sync line C |    |    |     | *  |
| SYNCMASK_PXISTARC | Selects PXIe STAR sync line C |    |    |     | *  |

*\* PXIe Syncs are only available on PXIe Devices. Refer to BTICard\_CardSyncValid for device dependent support*

| pinpolarity    |                                           |  |  |  |
|----------------|-------------------------------------------|--|--|--|
| Constant       | Description                               |  |  |  |
| SYNCPOL_SYNCAL | Sets active low polarity for sync line A  |  |  |  |
| SYNCPOL_SYNCAH | Sets active high polarity for sync line A |  |  |  |
| SYNCPOL_SYNCBL | Sets active low polarity for sync line B  |  |  |  |
| SYNCPOL_SYNCBH | Sets active high polarity for sync line B |  |  |  |
| SYNCPOL_SYNCCL | Sets active low polarity for sync line C  |  |  |  |
| SYNCPOL_SYNCCH | Sets active high polarity for sync line C |  |  |  |

Alternatively, to configure all messages on a specified channel to output a sync pulse, use BTI-429\_ChSyncDefine.

#### **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices. 3G Devices, which have only a single sync line, can call BTI429\_MsgCreate with the MSGCRT429\_SYNC flag. The mapping of sync lines to discretes is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function only applies to transmit messages.

```
BTI429_ChSyncDefine, BTICard_CardSyncValid
```
### **MsgTriggerDefine**

```
ERRVAL BTI429_MsgTriggerDefine
(
      BOOL enable, //Enable/disable external trigger
      USHORT trigmask, //Line(s) used for trigger signal
      USHORT trigval, //Active/inactive condition for trigger line(s)
      USHORT pinpolarity,//Active pin polarity (high/low)
      MSGADDR message, //Message address
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Defines the trigger input settings for the message specified by *message* and associates it with a trigger signal. The input line(s) are specified by *trigmask* with an active trigger state being the combination of *trigval* and *pinpolarity* (see tables below). When tagged and encountered in a transmit schedule, the *message* waits for the defined trigger condition before transmitting. BTI429\_MsgTriggerDefine may be called during run time to redefine the trigger input settings.

The constants in the tables below may be bitwise OR-ed together to configure multiple lines. Only one *trigmask* may be selected per trigger line (A, B, and C), and all combined states must be true for the trigger to occur. Triggers are both generation dependent and device dependent. Refer to the Device Dependency table and the following tables for more information.

| trigmask          |                                  |    |    |     |    |
|-------------------|----------------------------------|----|----|-----|----|
| Constant          | Description                      | 4G | 5G | 5G+ | 6G |
| TRIGMASK_TRIGA    | Selects discrete trigger line A  | x  | x  | x   | x  |
| TRIGMASK_PXITRIGA | Selects PXIe TRIG trigger line A |    |    |     | *  |
| TRIGMASK_PXISTARA | Selects PXIe STAR trigger line A |    |    |     | *  |
| TRIGMASK_TRIGB    | Selects discrete trigger line B  | x  | x  | x   | x  |
| TRIGMASK_PXITRIGB | Selects PXIe TRIG trigger line B |    |    |     | *  |
| TRIGMASK_PXISTARB | Selects PXIe STAR trigger line B |    |    |     | *  |
| TRIGMASK_TRIGC    | Selects discrete trigger line C  | x  | x  | x   | x  |
| TRIGMASK_PXITRIGC | Selects PXIe TRIG trigger line C |    |    |     | *  |
| TRIGMASK_PXISTARC | Selects PXIe STAR trigger line C |    |    |     | *  |

*\* PXIe Syncs are only available on PXIe Devices. Refer to BTICard\_CardTriggerValid for device dependent support*

| trigval          |                            |  |
|------------------|----------------------------|--|
| Constant         | Description                |  |
| TRIGVAL_TRIGAOFF | Trigger on line A inactive |  |
| TRIGVAL_TRIGAON  | Trigger on line A active   |  |
| TRIGVAL_TRIGBOFF | Trigger on line B inactive |  |
| TRIGVAL_TRIGBON  | Trigger on line B active   |  |
| TRIGVAL_TRIGCOFF | Trigger on line C inactive |  |
| TRIGVAL_TRIGCON  | Trigger on line C active   |  |

| pinpolarity    |                                               |    |    |     |    |
|----------------|-----------------------------------------------|----|----|-----|----|
| Constant       | Description                                   | 4G | 5G | 5G+ | 6G |
| TRIGPOL_TRIGAL | Sets active low polarity for trigger line A   | x  | x  | x   | x  |
| TRIGPOL_TRIGAH | Sets active high polarity for trigger line A  | x  | x  | x   | x  |
| TRIGPOL_TRIGAF | Sets active on falling edge of trigger line A |    |    |     | x  |
| TRIGPOL_TRIGAR | Sets active on rising edge of trigger line A  |    |    |     | x  |
| TRIGPOL_TRIGBL | Sets active low polarity for trigger line B   | x  | x  | x   | x  |
| TRIGPOL_TRIGBH | Sets active high polarity for trigger line B  | x  | x  | x   | x  |
| TRIGPOL_TRIGBF | Sets active on falling edge of trigger line B |    |    |     | x  |
| TRIGPOL_TRIGBR | Sets active on rising edge of trigger line B  |    |    |     | x  |
| TRIGPOL_TRIGCL | Sets active low polarity for trigger line C   | x  | x  | x   | x  |
| TRIGPOL_TRIGCH | Sets active high polarity for trigger line C  | x  | x  | x   | x  |
| TRIGPOL_TRIGCF | Sets active on falling edge of trigger line C |    |    |     | x  |
| TRIGPOL_TRIGCR | Sets active on rising edge of trigger line C  |    |    |     | x  |

Alternatively, to associate all messages on a transmit channel with a trigger signal, use BTI429\_ChTriggerDefine.

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices . 3G Devices, which have a single external trigger can call BTI429\_MsgCreate with the MSGCRT429\_EXTRIG flag. The mapping of trigger lines to discretes is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

```
BTI429_ChTriggerDefine, BTICard_CardTrigger, BTICard_CardTriggerEx, 
BTICard_CardTriggerValid
```
### **ParamAmplitudeConfig**

```
ERRVAL BTI429_ParamAmplitudeConfig
(
     ULONG configval, //Configuration options to set
     USHORT dacval, //12-bit digital-analog converter value
     INT xmtchan, //Transmit channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Enables variable transmit amplitude control as defined by *configval* (see table below) on the transmit channel specified by *xmtchan* and sets the digital-to-analog converter to *dacval*. If this parametric control is not used or is disabled, then the amplitude reverts to default (full) amplitude.

| configval           |                                          |
|---------------------|------------------------------------------|
| Constant            | Description                              |
| PARAMCFG429_DEFAULT | Select all default settings (bold below) |
| PARAMCFG429_AMPLON  | Enables parametric amplitude control     |
| PARAMCFG429_AMPLOFF | Disables parametric amplitude control    |

#### **DEVICE DEPENDENCY**

This function may only be used with a 429 transmit channel with parametric capability. A channel can be tested for parametric capability using BTI429\_ChGetInfo.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_ParamBitRateConfig, BTI429\_ChGetInfo

### ParamBitCountBuild

```
USHORT BTI429_ParamBitCountBuild
(
      INT bitcount //Value of bitcount
)
```
#### **RETURNS**

Message field formatted for paramflag (containing bitcount).

#### **DESCRIPTION**

Adds *bitcount* to the *paramflag* field of a message record. That record can be updated on the card with BTI429\_MsgBlockWr.

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI429\_MsgBlockWr

### **ParamBitRateConfig**

```
ERRVAL BTI429_ParamBitRateConfig
(
     ULONG configval, //Configuration options to set
     USHORT divval, //Divide value
     INT channel, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Configures *channel* for non-standard bus frequency as defined by *configval* (see table below) and sets the clock divider value to *divval*. If parametric bit rate control is not set or turned off, then the frequency reverts to the default value.

| configval              |                                          |  |
|------------------------|------------------------------------------|--|
| Constant               | Description                              |  |
| PARAMCFG429_DEFAULT    | Select all default settings (bold below) |  |
| PARAMCFG429_BITRATEON  | Enables parametric bit rate control      |  |
| PARAMCFG429_BITRATEOFF | Disables parametric bit rate control     |  |

#### **DEVICE DEPENDENCY**

Applies to all 5G, 5G+, and 6G Devices. For 3G or 4G, this function may only be used on a 429 channel with parametric capability. A 3G or 4G channel can be tested for bit rate parametric capability using BTI429\_ChGetInfo.

Below are the formulas for rate calculations using *divval*. 5G Devices support a maximum *divval* of 255. 5G+ and 6G Devices support a minimum *divval* of 100 (200kHz/25kHz) and a maximum *divval* of 1023 (19.5kHz/2.4kHz).

| Formulas for Rate Calculations in kHz |                         |                    |
|---------------------------------------|-------------------------|--------------------|
| Generation/Type                       | High Speed<br>Low Speed |                    |
| 4G Encoder and Decoder                | 4,000 / (divval + 1)    | 500 / (divval + 1) |
| 5G Decoder                            | 200 / (divval + 1)      | 25 / (divval + 1)  |
| 5G Encoder                            | 1,000 / (divval + 1)    | 125 / (divval + 1) |
| 5G+ and 6G Encoder and Decoder        | 20,000 / divval         | 2,500 / divval     |

#### **WARNINGS**

When converting an application from a 5G Device to a 5G+ Device (e.g UA14XX to UA24XX), calls to ParamBitRateConfig must update *divval* with the value calculated from the formula in the above table. Failure to do so will result in an incorrect bus frequency.

#### **SEE ALSO**

BTI429\_ParamAmplitudeConfig, BTI429\_ChGetInfo

### **ParamWaveformConfig**

```
ERRVAL BTI429_ParamWaveformConfig
(
      ULONG configval, //Configuration options to set
      INT highvolt, //High state differential amplitude (in millivolts)
      INT lowvolt, //Low state differential amplitude (in millivolts)
      INT nullvolt, //Null state differential amplitude (in millivolts)
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```

```
RETURNS
```
A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

```
Configures channel as defined by configval for parametric transmitter waveforms. All 
parameters (highvolt, lowvolt, and nullvolt) are internally passed to 
BTI429_ParamWaveformConfigEx with all other waveform parameters (biasvolt, highrise, 
and lowrise) configured as default values. Refer to BTI429_ParamWaveformConfigEx for 
more information regarding configval values, parameter definitions, and waveform examples.
```
#### **DEVICE DEPENDENCY**

Applies to 6G Devices with parametric waveform capability. A channel can be tested for functionality using BTI429\_ChGetInfo. Please consult the Device hardware manual for more information.

#### **WARNINGS**

Exact times and voltages depend on several factors: such as where the measurement occurs on the databus, analog and digital delays in the onboard circuits, and other analog characteristics of the databus.

```
BTI429_ParamAmplitudeConfig, BTI429_ParamWaveformConfigEx, 
BTI429_ChGetInfo
```
### **ParamWaveformConfigEx**

```
ERRVAL BTI429_ParamWaveformConfigEx
(
    ULONG configval, //Configuration options to set
    INT channel, //Channel number
    HCORE hCore //Core handle
)
```
LPPARAMFIELDS429 *paramflds*, *//Structure of waveform parameters*

#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Configures *channel* as defined by *configval* (see table below) for parametric transmitter waveforms. Changes to *biasvolt* requires the PARAMCFG429\_CMBIAS constant. Default parameter values are selected with PARAMCFG429\_WAVEOFF; refer to the following tables.

| configval             |                                               |
|-----------------------|-----------------------------------------------|
| Constant              | Description                                   |
| PARAMCFG429_DEFAULT   | Select all default settings (bold below)      |
| PARAMCFG429_WAVEON    | Enables parametric waveform control (default) |
| PARAMCFG429_WAVEOFF   | Disables parametric waveform control          |
| PARAMCFG429_CMBIASOFF | Disables common-mode DC bias (default)        |
| PARAMCFG429_CMBIAS    | Enables common-mode DC bias                   |

The following figure and tables describe the waveform parameters and demonstrate various configurations for the ARINC 429 parametric waveform. Note that *highvolt*, *lowvolt*, and *nullvolt* are differential voltages; whereas, *biasvolt* is a single-ended voltage. Limits, resolution, and accuracy for each parameter are determined by the Device hardware; refer to the hardware manual more information.

| paramflds |                                      |                     |                                                   |  |  |
|-----------|--------------------------------------|---------------------|---------------------------------------------------|--|--|
| Parameter | Description                          | Units               | PARAMCFG429_WAVEOFF                               |  |  |
| highvolt  | High state<br>differential amplitude | millivolts<br>(mV)  | 10000 (10V)                                       |  |  |
| lowvolt   | Low state<br>differential amplitude  | millivolts<br>(mV)  | -10000 (-10V)                                     |  |  |
| nullvolt  | Null state<br>differential amplitude | millivolts<br>(mV)  | 0 (0V)                                            |  |  |
| biasvolt  | Single-ended<br>common-mode bias     | millivolts<br>(mV)  | 0 (0V)                                            |  |  |
| highrise  | High state<br>rise and fall time     | nanoseconds<br>(ns) | Lowspeed: 10000 (10µs)<br>Highspeed: 1800 (1.8µs) |  |  |
| lowrise   | Low state<br>rise and fall time      | nanoseconds<br>(ns) | Lowspeed: 10000 (10µs)<br>Highspeed: 1800 (1.8µs) |  |  |

![](_page_135_Figure_12.jpeg)

|                               | Example  | Default | Double | Half   | 5V Null | 10V Offset    | 5V Common |
|-------------------------------|----------|---------|--------|--------|---------|---------------|-----------|
|                               |          |         | Ampl   | Ampl   | Offset  | Entire Signal | Mode Bias |
|                               | highvolt | 10000   | 20000  | 5000   | 10000   | 20000         | 10000     |
|                               | nullvolt | 0       | 0      | 0      | 5000    | 10000         | 0         |
| Parameter                     | lowvolt  | -10000  | -20000 | -5000  | -10000  | 0             | -10000    |
|                               | biasvolt | 0       | 0      | 0      | 0       | 0             | 5000      |
|                               | VD1      | 10 V    | 20 V   | 5 V    | 10 V    | 20 V          | 10 V      |
|                               | VDN      | 0 V     | 0 V    | 0 V    | 5 V     | 10 V          | 0 V       |
|                               | VD0      | -10 V   | -20 V  | -5 V   | -10 V   | 0 V           | -10 V     |
| (Refer To Figure)<br>Waveform | VP1      | 5 V     | 10 V   | 2.5 V  | 5 V     | 10 V          | 10 V      |
|                               | VPN      | 0 V     | 0 V    | 0 V    | 2.5 V   | 5 V           | 5 V       |
|                               | VP0      | -5 V    | -10 V  | -2.5 V | -5 V    | 0 V           | 0 V       |
|                               | VN1      | -5 V    | -10 V  | -2.5 V | -5 V    | -10 V         | 0 V       |
|                               | VNN      | 0 V     | 0 V    | 0 V    | -2.5 V  | -5 V          | 5 V       |
|                               | VN0      | 5 V     | 10 V   | 2.5 V  | 5 V     | 0 V           | 10 V      |

|                   | Example  | Low Speed Examples |                          |                               | High Speed Examples |                        |                             |
|-------------------|----------|--------------------|--------------------------|-------------------------------|---------------------|------------------------|-----------------------------|
|                   |          | Default            | Double Ampl<br>Std. Slew | Double Ampl<br>Std. Rise/Fall | Default             | Half Ampl<br>Std. Slew | Half Ampl<br>Std. Rise/Fall |
| Parameter         | highrise | 10000              | 20000                    | 10000                         | 1800                | 900                    | 1800                        |
|                   | lowrise  | 10000              | 20000                    | 10000                         | 1800                | 900                    | 1800                        |
|                   | tH       | 10 µs              | 20 µs                    | 10 µs                         | 1.8 µs              | 0.9 µs                 | 1.8 µs                      |
| (Refer To Figure) | tL       | 10 µs              | 20 µs                    | 10 µs                         | 1.8 µs              | 0.9 µs                 | 1.8 µs                      |
| Waveform          | SlewH    | 1 V/µs             | 1 V/µs                   | 2 V/µs                        | 5.6 V/µs            | 5.6 V/µs               | 2.8 V/µs                    |
|                   | SlewL    | 1 V/µs             | 1 V/µs                   | 2 V/µs                        | 5.6 V/µs            | 5.6 V/µs               | 2.8 V/µs                    |

#### **DEVICE DEPENDENCY**

Applies to 6G Devices with parametric waveform capability. Common-mode bias is supported on a subset of all parametric-capable channels. A channel can be tested for parametric waveform and common-mode bias functionality using BTI429\_ChGetInfo. Please consult the Device hardware manual for more information.

#### **WARNINGS**

Exact times and voltages depend on several factors: such as where the measurement occurs on the databus, analog and digital delays in the onboard circuits, and other analog characteristics of the databus.

```
BTI429_ParamAmplitudeConfig, BTI429_ParamWaveformConfig, BTI429_ChGetInfo
```
### **PlayBlockWr**

```
ERRVAL BTI429_PlayBlockWr
(
     LPUSHORT playbuf, //Pointer to start of host playback buffer
     INT count, //Command Blocks to write to Playback FIFO
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Transfers *count* number of Command Blocks from the start of *playbuf* host playback buffer to the Device Playback FIFO. Refer to the *Playback Mode* section earlier in this manual for more information.

![](_page_137_Figure_8.jpeg)

*Example of Transferring 3 (of 4) Command Blocks to Device*

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig with CHCFG429\_PLAYBACK must precede this function. Additionally, any Command Block to be transferred must have been written to *playbuf* with BTI429\_PlayPut\* (e.g. BTI429\_PlayPutData) prior to calling this function; BTI429\_PlayBlockWr does not modify *playbuf*.

The Playback FIFO may be polled for status and availability with BTI429\_PlayStatus. Polling the Playback FIFO prevents overflow conditions.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that both *count* and Playback FIFO status are in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum *count*.

```
BTI429_ChConfig, BTI429_PlayPut*, BTI429_PlayStatus
```
### **PlayPutData**

```
ERRVAL BTI429_PlayPutData
(
      ULONG dataval, //Data word to Playback
      INT bitcount, //Bitcount to Playback
      USHORT gapval, //Following gap to Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes a DATA Command Block into *playbuf* at *offset*. A DATA Command Block directly stores the data to transmit instead of the address of a message. When a DATA Command Block is played back, the *channel* will transmit *bitcount* (number of bits) of *dataval* followed by the a following *gapval* length gap. Note that *gapval* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

![](_page_138_Figure_8.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum *offset*.

#### **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayBlockWr, BTI429\_PlayStatus

### **PlayPutGap**

```
ERRVAL BTI429_PlayPutGap
(
      USHORT gapval, //Gap to Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes a GAP Command Block into *playbuf* at *offset*. Note that *gapval* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

![](_page_139_Picture_8.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum offset.

```
BTI429_ChConfig, BTI429_PlayBlockWr, BTI429_PlayStatus
```
### **PlayPutLog**

```
ERRVAL BTI429_PlayPutLog
(
      ULONG condition, //Value to test during Playback
      USHORT tagval, //Event tag value during Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes a conditional LOG Command Block into *playbuf* at *offset*. A conditional LOG Command Block causes the Device to generate an Event Log List entry if *condition* evaluates as TRUE. The event type placed in the Event Log List is EVENTTYPE\_429OPCODE and the user-specified value *tagval* is used as the info value. Entries are read out of the Event Log List using BTICard\_EventLogRd. Refer to BTI429\_SchedLog for (the same) condition options.

![](_page_140_Picture_8.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum *offset*.

#### **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayBlockWr, BTI429\_PlayStatus, BTI429\_SchedLog

### **PlayPutPulse0**

```
ERRVAL BTI429_PlayPutPulse0
(
      INT dionum, //DIO number to pulse "Off" during Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes a PULSE0 Command Block into *playbuf* at offset. A PULSE0 Command Block sets the state of the specified *dionum* to the "Off" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

Note that by using this function in combination with BTI429\_PlayPutData, BTI429\_PlayPutGap, and/or other playback entries, the I/O signals can be used to frame data messages, groups of messages, or to create arbitrary pulse width signals.

![](_page_141_Figure_9.jpeg)

*Example of Putting Command Block into playbuf at offset 2*

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum offset.

Playback internally uses prefetching to minimize intermessage gap time. Due to prefetching and asynchronous discrete operation, the absolute timing of the pulse from a *SchedPulse* entry can vary by a gap/message transmission. When comparing the occurrence of playback discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine.

#### **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayBlockWr, BTI429\_PlayStatus

### **PlayPutPulse1**

```
ERRVAL BTI429_PlayPutPulse1
(
      INT dionum, //DIO number to pulse "On" during Playback
      INT offset, //Command Block offset for host playback buffer
      LPUSHORT playbuf //Pointer to start of host playback buffer
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Writes a PULSE1 Command Block into *playbuf* at offset. A PULSE1 Command Block sets the state of the specified dionum to the "On" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

Note that by using this function in combination with BTI429\_PlayPutData, BTI429\_PlayPutGap, and/or other playback entries, the I/O signals can be used to frame data messages, groups of messages, or to create arbitrary pulse width signals.

![](_page_142_Picture_9.jpeg)

*Example of Putting Command Block into* playbuf *at offset 2*

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function does not write to the Device. Refer to BTI429\_PlayBlockWr.

All Playback Command Blocks are 8 USHORTs, therefore the host buffer must be in multiples of 8. Note that *offset* is in units of Command Blocks (not USHORTs); therefore, the host playback buffer must be 8 times larger than the maximum offset.

Playback internally uses prefetching to minimize intermessage gap time. Due to prefetching and asynchronous discrete operation, the absolute timing of the pulse from a SchedPulse entry can vary by a gap/message transmission. When comparing the occurrence of playback discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine.

#### **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayBlockWr, BTI429\_PlayStatus

### **PlayStatus**

```
INT BTI429_PlayStatus
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs, or the Playback FIFO's available Command Block space if successful.

#### **DESCRIPTION**

Polls the Playback FIFO for *channel* and reports number of Command Blocks the FIFO is able to accept. A full Playback FIFO will return 0; an empty FIFO will return the FIFO capacity.

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig with CHCFG429\_PLAYBACK must precede this function.

All Playback Command Blocks are 8 USHORTs, thus the return value is in units of Command Blocks (not USHORTs).

#### **SEE ALSO**

BTI429\_ChConfig, BTI429\_PlayPut\*, BTI429\_PlayBlockWr

### **SchedBranch**

```
SCHNDX BTI429_SchedBranch
(
      ULONG condition, //Condition for branch
      SCHNDX destindex, //Destination index for branch
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a conditional BRANCH Command Block to the current end of the Schedule. A conditional BRANCH Command Block causes the Device to branch to the index in the Schedule specified by *destindex* if *condition* evaluates as TRUE.

The *condition* flags listed below may be used to specify the branch condition.

| condition        |                            |  |  |  |
|------------------|----------------------------|--|--|--|
| Constant         | Description                |  |  |  |
| COND429_ALWAYS   | Always branch              |  |  |  |
| COND429_DIO1ACT  | Branch if DIO1 is active   |  |  |  |
| COND429_DIO1NACT | Branch if DIO1 is inactive |  |  |  |
| COND429_DIO2ACT  | Branch if DIO2 is active   |  |  |  |
| COND429_DIO2NACT | Branch if DIO2 is inactive |  |  |  |
| COND429_DIO3ACT  | Branch if DIO3 is active   |  |  |  |
| COND429_DIO3NACT | Branch if DIO3 is inactive |  |  |  |
| COND429_DIO4ACT  | Branch if DIO4 is active   |  |  |  |
| COND429_DIO4NACT | Branch if DIO4 is inactive |  |  |  |
| COND429_DIO5ACT  | Branch if DIO5 is active   |  |  |  |
| COND429_DIO5NACT | Branch if DIO5 is inactive |  |  |  |
| COND429_DIO6ACT  | Branch if DIO6 is active   |  |  |  |
| COND429_DIO6NACT | Branch if DIO6 is inactive |  |  |  |
| COND429_DIO7ACT  | Branch if DIO7 is active   |  |  |  |
| COND429_DIO7NACT | Branch if DIO7 is inactive |  |  |  |
| COND429_DIO8ACT  | Branch if DIO8 is active   |  |  |  |
| COND429_DIO8NACT | Branch if DIO8 is inactive |  |  |  |

#### **DEVICE DEPENDENCY**

Does not apply to 5G Devices. The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function. When creating subroutines, BTI429\_SchedEntry needs to be called to point to the main section. The Command Block pointed to by *destindex* must have been previously inserted in the Schedule.

#### **SEE ALSO**

BTI429\_SchedEntry, BTI429\_SchedCall, BTI429\_SchedReturn

### **SchedBuild**

```
ERRVAL BTI429_SchedBuild
(
```

```
HCORE hCore //Core handle
```

```
INT nummsgs, //Number of messages to Schedule
MSGADDR msgs[], //Array of message addresses
INT min[], //Array of message frequencies
INT max[], //Array of message frequencies
INT channel, //Channel number of transmitter
```

```
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Clears any transmit Schedule for the specified channel and creates a new transmit Schedule that sequences messages at given intervals. The new Schedule will consist of *nummsgs* messages, each transmitted within an interval specified by the *min* and *max* interval arrays (in units of milliseconds). *msgs* points to an array of message addresses, each previously generated by a call to BTI429\_MsgCreate.

The nth element of the *msgs* array uses the nth element of the *min* and *max* intervals arrays to create the Schedule.

The function schedules messages and gaps to generate the specified transmit intervals. If the Schedule cannot be generated, an error is returned.

If the range of every interval is zero (the min and max value of each pair are the same), then no range checking is performed on the resulting schedule.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function as well as a call to BTI429\_MsgCreate for each message to be Scheduled.

#### **SEE ALSO**

BTI429\_SchedMsg, BTI429\_SchedMsgEx, BTI429\_SchedGap

### **SchedCall**

```
SCHNDX BTI429_SchedCall
(
      ULONG condition, //Condition for call
      SCHNDX destindex, //Destination index of subroutine
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a conditional CALL Command Block to the current end of the Schedule. A conditional CALL Command Block causes the Schedule to execute the subroutine at the index in the Schedule specified by *destindex* if *condition* evaluates as TRUE.

The *condition* flags listed below may be used to specify the call condition.

| condition        |                            |  |  |  |
|------------------|----------------------------|--|--|--|
| Constant         | Description                |  |  |  |
| COND429_ALWAYS   | Always branch              |  |  |  |
| COND429_DIO1ACT  | Branch if DIO1 is active   |  |  |  |
| COND429_DIO1NACT | Branch if DIO1 is inactive |  |  |  |
| COND429_DIO2ACT  | Branch if DIO2 is active   |  |  |  |
| COND429_DIO2NACT | Branch if DIO2 is inactive |  |  |  |
| COND429_DIO3ACT  | Branch if DIO3 is active   |  |  |  |
| COND429_DIO3NACT | Branch if DIO3 is inactive |  |  |  |
| COND429_DIO4ACT  | Branch if DIO4 is active   |  |  |  |
| COND429_DIO4NACT | Branch if DIO4 is inactive |  |  |  |
| COND429_DIO5ACT  | Branch if DIO5 is active   |  |  |  |
| COND429_DIO5NACT | Branch if DIO5 is inactive |  |  |  |
| COND429_DIO6ACT  | Branch if DIO6 is active   |  |  |  |
| COND429_DIO6NACT | Branch if DIO6 is inactive |  |  |  |
| COND429_DIO7ACT  | Branch if DIO7 is active   |  |  |  |
| COND429_DIO7NACT | Branch if DIO7 is inactive |  |  |  |
| COND429_DIO8ACT  | Branch if DIO8 is active   |  |  |  |
| COND429_DIO8NACT | Branch if DIO8 is inactive |  |  |  |

#### **DEVICE DEPENDENCY**

Does not apply to 5G Devices. The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function. After creating subroutines, BTI429\_SchedEntry needs to be called to point to the main section. Every use of BTI429\_SchedCall must have a corresponding BTI429\_SchedReturn. The Command Block pointed to by *destindex* must have been previously inserted in the Schedule.

#### **SEE ALSO**

BTI429\_SchedEntry, BTI429\_SchedBranch, BTI429\_SchedReturn

### **SchedData**

```
SCHNDX BTI429_SchedData
(
     ULONG dataval, //Data word to schedule
     INT bitcount, //Bitcount to schedule
     USHORT gapval, //Following gap to schedule
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a DATA Command Block to the current end of the Schedule. A DATA Command Block directly stores the data to transmit instead of the address of a message. DATA Command Blocks are useful for simple data transmissions by eliminating the overhead of a Message Record. When a DATA Command Block is encountered in the Schedule, the *channel* will transmit *bitcount*  (number of bits) of *dataval* followed by the a following *gapval* length gap. Note that *gapval*  units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

#### **DEVICE DEPENDENCY**

Applies to 5G+ and 6G Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

DATA Command Blocks do not access/update Message Records. As such, activity and time-tag information must be accessed from Sequential Monitor records rather than a Message Record.

```
BTI429_SchedEntry, BTICard_SeqRd, BTI429_ChConfig
```
### **SchedEntry**

```
SCHNDX BTI429_SchedEntry
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Sets the next available location in the Schedule as the beginning of the Schedule. This operation is only necessary if subroutines are used in a Schedule.

To create a Schedule with subroutines, first define the subroutines by calling the desired Schedule functions while saving the returned Schedule indices. Then call BTI429\_SchedEntry to set the starting point of the Schedule. Then build the main part of the Schedule by calling the other Schedule functions that include the commands that call the subroutines (i.e., BTI429\_SchedCall and BTI429\_SchedBranch).

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

#### **SEE ALSO**

BTI429\_SchedBranch, BTI429\_SchedCall, BTI429\_SchedReturn

### **SchedGap**

```
SCHNDX BTI429_SchedGap
(
      USHORT gap, //Gap value (in bit times or microseconds)
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
**RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a GAP Command Block to the current end of the Schedule. When a GAP Command Block is encountered in the Schedule, it triggers the transmission of any preceding MESSAGE command Block as well as the specified gap before the next message can be transmitted. Note that g*ap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

Depending on the configuration of an asynchronous List Buffer, this function will internally call either BTI429\_SchedGapFixed or BTI429\_SchedGapList. This allows the user to include or leave out an asynchronous List Buffer without rebuilding the Schedule.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

#### **SEE ALSO**

BTI429\_SchedMsg, BTI429\_SchedGapFixed, BTI429\_SchedGapList

### **SchedGapFixed**

```
SCHNDX BTI429_SchedGapFixed
(
      USHORT gap, //Gap value (in bit times or microseconds)
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a FIXEDGAP Command Block to the current end of the Schedule. When a FIXEDGAP Command Block is encountered in the Schedule, it triggers the transmission of any preceding MESSAGE command Block as well as the specified gap before the next message can be transmitted. The gap time is fixed so no asynchronous messages can be transmitted during this time. Note that *gap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

*Note: This is an advanced function, and for most applications, the BTI429\_SchedGap function is preferred.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

#### **SEE ALSO**

BTI429\_SchedGap, BTI429\_SchedGapList

### **SchedGapList**

```
SCHNDX BTI429_SchedGapList
(
     USHORT gap, //Gap value (in bit times or microseconds)
     LISTADDR list, //Address of asynchronous List Buffer
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a conditional LISTGAP Command Block to the current end of the Schedule. A conditional LISTGAP Command Block specifies *gap* before the next Scheduled message can be transmitted. During this gap time, if a message exists in the asynchronous List Buffer, it is transmitted in the gap time. Note that g*ap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

*Note: This is an advanced function, and for most applications, the BTI429\_SchedGap function is preferred.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function. In addition, an asynchronous List Buffer must be configured using BTI429\_ListAsyncCreate before calling this function.

#### **SEE ALSO**

BTI429\_SchedGap, BTI429\_SchedGapFixed, BTI429\_ListAsyncCreate

### **SchedHalt**

```
SCHNDX BTI429_SchedHalt
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a HALT Command Block to the current end of the Schedule. A HALT Command Block stops the Schedule until the channel is re-enabled using BTI429\_ChStart. When a HALT Command Block is encountered, it has the same effect as executing the BTI429\_ChStop function.

*Note: Execution of this function does NOT halt the Schedule. The Schedule is halted only when the resulting Schedule executes the HALT Command Block after BTICard\_CardStart starts the Device.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

#### **SEE ALSO**

BTI429\_SchedPause, BTI429\_ChStart, BTI429\_ChStop

### **SchedLog**

```
SCHNDX BTI429_SchedLog
(
     ULONG condition, //Value to test
     USHORT tagval, //Event tag value
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a conditional LOG Command Block to the current end of the schedule. A conditional LOG Command Block causes the Device to generate an Event Log List entry if condition evaluates as TRUE. The event type placed in the Event Log List is EVENTTYPE\_429OPCODE and the userspecified value *tagval* is used as the info value. Entries are read out of the Event Log List using BTICard\_EventLogRd.

The condition flags listed below may be used to specify the Event condition.

| Condition        |                                      |  |
|------------------|--------------------------------------|--|
| Constant         | Description                          |  |
| COND429_ALWAYS   | Always branch                        |  |
| COND429_DIO1ACT  | Create log entry if DIO1 is active   |  |
| COND429_DIO1NACT | Create log entry if DIO1 is inactive |  |
| COND429_DIO2ACT  | Create log entry if DIO2 is active   |  |
| COND429_DIO2NACT | Create log entry if DIO2 is inactive |  |
| COND429_DIO3ACT  | Create log entry if DIO3 is active   |  |
| COND429_DIO3NACT | Create log entry if DIO3 is inactive |  |
| COND429_DIO4ACT  | Create log entry if DIO4 is active   |  |
| COND429_DIO4NACT | Create log entry if DIO4 is inactive |  |
| COND429_DIO5ACT  | Create log entry if DIO5 is active   |  |
| COND429_DIO5NACT | Create log entry if DIO5 is inactive |  |
| COND429_DIO6ACT  | Create log entry if DIO6 is active   |  |
| COND429_DIO6NACT | Create log entry if DIO6 is inactive |  |
| COND429_DIO7ACT  | Create log entry if DIO7 is active   |  |
| COND429_DIO7NACT | Create log entry if DIO7 is inactive |  |
| COND429_DIO8ACT  | Create log entry if DIO8 is active   |  |
| COND429_DIO8NACT | Create log entry if DIO8 is inactive |  |

#### **DEVICE DEPENDENCY**

5G Devices only support the COND429\_ALWAYS flag. The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

#### **SEE ALSO**

BTICard\_EventLogRd

### **SchedMode**

```
ERRVAL BTI429_SchedMode
(
      ULONG modeval //Schedule Build mode options
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Sets scheduling options for the BTI429\_SchedBuild function. The parameter *modeval* controls various options such as the scheduling method, time base and execution environment of the BTI429\_SchedBuild function. The following is a list of the different values that can be OR'ed together for the *modeval* options. Only one METHOD may be specified per call.

| Modeval                  |                                                                                                                                                                                                                       |  |
|--------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--|
| Constant                 | Description                                                                                                                                                                                                           |  |
| SCHEDMODE_DEFAULT        | Selects all default settings (bold below)                                                                                                                                                                             |  |
| SCHEDMODE_METHOD_NORMAL  | Selects the normal scheduling method                                                                                                                                                                                  |  |
| SCHEDMODE_METHOD_QUICK   | Selects the quick scheduling method                                                                                                                                                                                   |  |
| SCHEDMODE_METHOD_BOTH    | Uses the quick method first then uses the normal method if the quick method fails to                                                                                                                                  |  |
|                          | build a schedule.                                                                                                                                                                                                     |  |
| SCHEDMODE_MILLISEC       | Sets the min and max periods to be specified in milliseconds                                                                                                                                                          |  |
| SCHEDMODE_MICROSEC       | Sets the min and max periods to be specified in microseconds                                                                                                                                                          |  |
| SCHEDMODE_REMOTE         | For RPC Devices, specifies to perform calculations remotely                                                                                                                                                           |  |
| SCHEDMODE_LOCAL          | For RPC Devices, specifies to perform calculations locally                                                                                                                                                            |  |
| SCHEDMODE_SKIPRANGECHECK | Skips range checking of a message when the min period equals the max period.<br>Attempts to meet the rate, but schedule will succeed even if the messages with same<br>min/max cannot transmit at a specified period. |  |
| SCHEDMODE_RANGECHECK     | Performs range checking on all messages. SchedBuild fails if any messages are out<br>of range.                                                                                                                        |  |

The normal scheduling method uses our traditional algorithm for scheduling messages. It is the default method to preserve schedule timing in established applications. The quick scheduling method significantly reduces the schedule build times and improves the ability to schedule highduty cycle schedules. It is recommended to use the SCHEDMODE\_METHOD\_BOTH which first uses the quick scheduling method, and then uses the normal scheduling method if necessary.

For RPC Devices, SCHEDMODE\_REMOTE and SCHEDMODE\_LOCAL control where the scheduling calculations are executed. SCHEDMODE\_REMOTE can be faster for many schedules or when network latency is high. SCHEDMODE\_LOCAL can offer performance improvements for more complex schedules and if network latency is low.

The purpose of SCHEDMODE\_SKIPRANGECHECK is to simplify scheduling by using a "best effort" mode. For messages with identical min/max transmit interval values, SchedBuild successfully generates automatic schedules even if there are absolute timing violations for the messages with the identical min/max times. Refer to the following example:

|  | Example Schedule |
|--|------------------|
|--|------------------|

| MESSAGE | MIN   | MAX    |
|---------|-------|--------|
| Label 1 | 90 ms | 110 ms |
| Label 2 | 37 ms | 37 ms  |
| ···     | ···   | ···    |
| Label n | ···   | ···    |

#### Example Results

The bus loading will change depending on the number of labels. Label 1 and Label 2 are not multiples of each other; although not required, this can cause SchedBuild to fail when heavily loaded. As an example using Label 2, three times the 37ms interval is 111ms. When SCHEDMODE\_SKIPFRAMECHECK is set, the interval for Label 2 is allowed to vary slightly, and the end result may be slightly modified for a successful build using 37ms, 37ms, and 36ms. When SCHEDMODE\_RANGECHECK is set, the function will fail because the interval is out of range.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Calling this function sets the operational mode for BTI429\_SchedBuild for ALL Devices in the same process space (i.e. same application). To use different schedule modes on different channels and/or different hardware, the BTI429\_SchedMode function must be called prior to each subsequent call to BTI429\_SchedBuild.

#### **SEE ALSO**

BTI429\_SchedBuild

### **SchedMsg**

```
SCHNDX BTI429_SchedMsg
(
     MSGADDR message, //Address of message
     INT channel, //Channel number of transmitter
     HCORE hCore //Core handle
```
)

#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a MESSAGE command Block to the current end of the Schedule. When a MESSAGE command Block is encountered in the Schedule, the message value specified by *message* is loaded into the transmit channel's encoder. The message will be transmitted when the Schedule subsequently encounters either a GAP Command Block or another MESSAGE command Block. If a MESSAGE command Block is followed by another MESSAGE command Block, the gap time between the two messages is automatically set to four bit times. More or less gap time can be explicitly set with BTI429\_SchedGap or BTI429\_SchedMsgEx.

*Note: Execution of this function does NOT transmit the message. The message is transmitted only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function. In addition, the message must have been created with BTI429\_MsgCreate.

#### **SEE ALSO**

BTI429\_SchedMsgEx, BTI429\_MsgCreate

### **SchedMsgEx**

```
SCHNDX BTI429_SchedMsgEx
(
      MSGADDR message, //Address of message
      USHORT gap, //Gap to follow message (in bit times or microseconds)
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a MESSAGE command Block to the current end of the Schedule with a user-specified gap value that can be less than the automatic minimum of 4 bit times. A MESSAGE command Block loads the message value specified by *message* into the transmit channel's encoder. The message will be transmitted when the Schedule encounters either a GAP Command Block or another MESSAGE command Block.

*gap* specifies the gap value (0 to 65,535) to follow the message. This gap value allows the user to explicitly specify a gap value of less (or more) than four bit times. This gap value is overwritten if followed by a GAP Command Block. Note that g*ap* units are determined by BTI429\_ChConfig with either CHCFG429\_GAPBIT or CHCFG429\_GAP1US.

*Note: Execution of this function does NOT transmit the message. The message is transmitted only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

Only 5G+ and 6G Devices support microsecond gap times.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function. In addition, the message must have been created with BTI429\_MsgCreate.

#### **SEE ALSO**

BTI429\_SchedMsg, BTI429\_MsgCreate

### **SchedPause**

```
SCHNDX BTI429_SchedPause
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a PAUSE Command Block to the current end of the Schedule. A PAUSE Command Block pauses the channel specified by channel until the Device is unpaused using BTI429\_ChResume. When a PAUSE Command Block is encountered, it has the same effect as executing the BTI429\_ChPause function.

*Note: Execution of this function does NOT pause the channel. The channel is paused only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

Because of internal prefetching on 3G/4G/5G Devices, when scheduling a SchedPause entry two very small preceding gaps (SchedGap with a gapval of 1) should be inserted. If these additional gaps are not inserted on 3G/4G/5G Devices, the schedule could pause before the preceding message is transmitted.

#### **SEE ALSO**

BTI429\_ChPause, BTI429\_ChResume, BTI429\_SchedHalt, BTI429\_SchedRestart

### **SchedPulse**

```
SCHNDX BTI429_SchedPulse
(
      INT dionum, //Dio number to pulse
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended schedule entry if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Schedules a pair of opcodes that pulse the specified *dionum* to the "On" state followed by the "Off" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

#### **DEVICE DEPENDENCY**

The level of the *dionum* "On" and "Off" states, as well as the mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

The SCHNDX returned is for the second of the pair of opcodes that are scheduled. The opcodes are scheduled sequentially, so the SCHNDX of the first pulse opcode can be derived from the return value by subtracting one.

Schedules internally use prefetching to minimize intermessage gap time. Because of schedule prefetching and asynchronous discrete operation, the absolute timing of the pulse from a SchedPulse entry can vary by a gap/message transmission. When comparing the occurrence of scheduled discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine or BTI429\_MsgSyncDefine.

```
BTI429_SchedPulse0, BTI429_SchedPulse1, BTICard_ExtDIORd, 
BTICard_ExtDIOWr
```
### **SchedPulse0**

```
SCHNDX BTI429_SchedPulse0
(
      INT dionum, //DIO number to pulse
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended schedule entry if successful, or a negative value if an error occurred.

#### **DESCRIPTION**

Sets the state of the specified *dionum* to the "Off" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

*Note that by using this function in combination with BTI429\_SchedGap, BTI429\_SchedMessage and/or other schedule entries, the I/O signals can be used to frame messages, groups of messages, or to create arbitrary pulse width signals.*

#### **DEVICE DEPENDENCY**

The level of the *dionum* "On" and "Off" states, as well as the mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

Schedules internally use prefetching to minimize intermessage gap time. Because of schedule prefetching and asynchronous discrete operation, the absolute timing of the pulse from a SchedPulse entry can vary by a gap/message transmission. When comparing the occurrence of scheduled discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine or BTI429\_MsgSyncDefine.

#### **SEE ALSO**

BTI429\_SchedPulse, BTI429\_SchedPulse1, BTICard\_ExtDIORd, BTICard\_ExtDIOWr

### **SchedPulse1**

```
SCHNDX BTI429_SchedPulse1
(
      INT dionum, //DIO number to pulse
      INT channel, //Channel number
      HCORE hCore //Core handle
)
```
**RETURNS**

The index of the appended schedule entry if successful, or a negative value if an error occurred.

#### **DESCRIPTION**

Sets the state of the specified *dionum* to the "On" state. Use BTICard\_ExtDIOWr for normal updating of the discrete I/O signals.

Note that by using this function in combination with BTI429\_SchedGap, BTI429\_SchedMessage and/or other schedule entries, the I/O signals can be used to frame messages, groups of messages, or to create arbitrary pulse width signals.

#### **DEVICE DEPENDENCY**

The level of the *dionum* "On" and "Off" states, as well as the mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

Schedules internally use prefetching to minimize intermessage gap time. Because of schedule prefetching and asynchronous discrete operation, the absolute timing of the pulse from a SchedPulse entry can vary by a gap/message transmission. When comparing the occurrence of scheduled discrete transitions with adjacent 429 messages, the discrete can appear to transition early.

For discrete output pulses synchronous to ARINC 429 databus activity, use BTI429\_ChSyncDefine or BTI429\_MsgSyncDefine.

#### **SEE ALSO**

BTI429\_SchedPulse, BTI429\_SchedPulse0, BTICard\_ExtDIORd, BTICard\_ExtDIOWr

### **SchedRestart**

```
SCHNDX BTI429_SchedRestart
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a RESTART Command Block to the current end of the Schedule. A RESTART Command Block restarts the Schedule back at the beginning. A RESTART Command Block is automatically appended to the end of the Schedule, so this function does not need to be called for simple Schedules.

*Note: Execution of this function does NOT restart the Schedule. The Schedule is restarted only when the resulting Schedule is executed after the channel is enabled and the Device is started.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

#### **SEE ALSO**

BTI429\_SchedEntry

### **SchedReturn**

```
SCHNDX BTI429_SchedReturn
(
      INT channel, //Channel number of transmitter
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The index of the appended Command Block if successful, or a negative value if an error occurs.

#### **DESCRIPTION**

Appends a RETURN Command Block to the current end of the Schedule. A RETURN Command Block returns the Schedule to the point at which the last CALL command was made. For every CALL command there must be a RETURN command to insure proper operation.

#### **DEVICE DEPENDENCY**

Does not apply to 5G Devices.

#### **WARNINGS**

A call to BTI429\_ChConfig must precede this function.

#### **SEE ALSO**

BTI429\_SchedCall

# <span id="page-164-0"></span>**5. BTICard Function References**

This chapter provides detailed information on the BTIDriver protocol independent (BTICard) functions for Ballard Devices. The descriptions and examples discussed here are intended for use with programs written in the C language. Users of other languages should contact Ballard Support for assistance.

### <span id="page-164-1"></span>**5.1 Function Summaries**

[Table 5.1](#page-164-2) summarizes the BTICard functions.

| BIT Functions            |                                                                                |  |  |
|--------------------------|--------------------------------------------------------------------------------|--|--|
| BTICard_BITConfig        | Configures the Built-In Test functionality of the card                         |  |  |
| BTICard_BITInitiate      | Performs a hardware test on the specified card                                 |  |  |
| BTICard_BITStatusClr     | Clears the historical maximum and minimum values                               |  |  |
| BTICard_BITStatusRd      | Reads the current status of Built-In Test (BIT)                                |  |  |
| CARD Functions           |                                                                                |  |  |
| BTICard_CardClose        | Disables access to a Device and releases its hardware resources                |  |  |
| BTICard_CardGetInfo      | Returns various information from the Device                                    |  |  |
| BTICard_CardIsRunning    | Checks whether core is running                                                 |  |  |
| BTICard_CardOpen         | Enables access to a Device and secures hardware resources                      |  |  |
| BTICard_CardProductStr   | Returns the name of the Device                                                 |  |  |
| BTICard_CardReset        | Resets the core hardware; destroys all existing configuration data             |  |  |
| BTICard_CardResume       | Resumes operation of the core                                                  |  |  |
| BTICard_CardStart        | Starts operation of the core                                                   |  |  |
| BTICard_CardStop         | Stops operation of the core                                                    |  |  |
| BTICard_CardSyncValid    | Returns all available sync lines for the specified core                        |  |  |
| BTICard_CardTest         | Performs a hardware test on the specified core                                 |  |  |
| BTICard_CardTrigger      | Generates a software-simulated trigger signal on all available trigger lines   |  |  |
| BTICard_CardTriggerEx    | Generates a software-simulated trigger signal on the specified trigger line(s) |  |  |
| BTICard_CardTriggerValid | Returns all available trigger lines for the specified core                     |  |  |
| BTICard_CardTypeStr      | Returns the type or model number of the Device                                 |  |  |
| BTICard_CoreOpen         | Enables access to a specified core                                             |  |  |
| EVENT Functions          |                                                                                |  |  |
| BTICard_EventLogConfig   | Enables events and initializes the Event Log List                              |  |  |
| BTICard_EventLogRd       | Reads an entry from the Event Log List                                         |  |  |
| BTICard_EventLogStatus   | Checks the status of the Event Log List                                        |  |  |
| I/O Functions            |                                                                                |  |  |
| BTICard_ExtDIOMonConfig  | Enables Sequential monitoring on specific transitions of the digital I/O pins  |  |  |
| BTICard_ExtDIOEnWr       | Sets both the value and output enable of the specified digital I/O pin         |  |  |
| BTICard_ExtDIORd         | Reads the value of the specified digital I/O pin                               |  |  |
| BTICard_ExtDIOWr         | Sets the value of the specified digital I/O pin                                |  |  |
| BTICard_ExtLEDRd         | Reads the on/off value of the LED                                              |  |  |
| BTICard_ExtLEDWr         | Sets the on/off value of the LED                                               |  |  |
| BTICard_ExtStatusLEDRd   | Reads the on/off value and color of the Status LED                             |  |  |
| BTICard_ExtStatusLEDWr   | Sets the on/off value and color of the Status LED                              |  |  |
| INTERRUPT Functions      |                                                                                |  |  |
| BTICard_IntClear         | Clears the interrupt from the core (OS-dependent)                              |  |  |
| BTICard_IntInstall       | Associates an event object with interrupts from the core (OS-dependent)        |  |  |
| BTICard_IntUninstall     | Removes association between event objects and interrupts<br>(OS-dependent)     |  |  |

<span id="page-164-2"></span>*Table 5.1 —Protocol-Independent (BTICard\_ ) Functions (Continued Next Page)*

| IRIG TIME Functions           |                                                                           |
|-------------------------------|---------------------------------------------------------------------------|
| BTICard_IRIGConfig            | Configures the IRIG timer on the specified core                           |
| BTICard_IRIGFieldGet??        | Returns the ?? field (days, hours, etc.) from an IRIG time-tag            |
| BTICard_IRIGFieldPut??        | Writes the ?? field (days, hours, etc.) to an IRIG time-tag               |
| BTICard_IRIGInputThresholdGet | Gets the threshold of the IRIG input circuitry                            |
| BTICard_IRIGInputThresholdSet | Sets the threshold of the IRIG input circuitry                            |
| BTICard_IRIGRd                | Reads the current value of the IRIG timer on the specified core           |
| BTICard_IRIGSyncStatus        | Reports whether the IRIG timer is locked in sync with the IRIG bus        |
| BTICard_IRIGTimeBCDToBin      | Converts IRIG BCD value to a microsecond binary time                      |
| BTICard_IRIGTimeBCDToNanoBin  | Converts IRIG BCD value to a nanosecond binary time                       |
| BTICard_IRIGTimeBinToBCD      | Converts a microsecond binary time value to IRIG BCD value                |
| BTICard_IRIGTimeNanoBinToBCD  | Converts a nanosecond binary time value to IRIG BCD value                 |
| BTICard_IRIGWr                | Sets (initializes) the IRIG timer on the specified core                   |
|                               |                                                                           |
| PXIE Functions                |                                                                           |
| BTICard_PXIStatus             | Returns user-requested status for PXIe cards                              |
| SEQUENTIAL RECORD Functions   |                                                                           |
| BTICard_SeqBlkRd              | Reads multiple records out of the Sequential Record (use for few records) |
| BTICard_SeqCommRd             | Reads multiple records out of the Sequential Record (use for many         |
|                               | records)                                                                  |
| BTICard_SeqConfig             | Configures the Sequential Monitor                                         |
| BTICard_SeqDMARd              | Reads multiple records out of the Sequential Record (use for many         |
|                               | records)                                                                  |
| BTICard_SeqFindCheckVersion   | Tests the version number of the specified record                          |
| BTICard_SeqFindInit           | Initializes the BTICard_SeqFindNext?? functions                           |
| BTICard_SeqFindMore1553       | Finds the extra 1553 record fields, when present                          |
| BTICard_SeqFindNext           | Finds the next message in the Sequential Record buffer                    |
| BTICard_SeqFindNext1553       | Finds the next MIL-STD-1553 message in the Sequential Record buffer       |
| BTICard_SeqFindNext429        | Finds the next ARINC 429 message in the Sequential Record buffer          |
| BTICard_SeqFindNext708        | Finds the next ARINC 708 message in the Sequential Record buffer          |
| BTICard_SeqFindNext717        | Finds the next ARINC 717 message in the Sequential Record buffer          |
| BTICard_SeqFindNextDIO        | Finds the next DIO message in the Sequential Record buffer                |
| BTICard_SeqFindNextEBR        | Finds the next EBR-1553 message in the Sequential Record buffer           |
| BTICard_SeqInterval           | Sets the interval value if using Interval mode                            |
| BTICard_SeqIsRunning          | Determines whether the Sequential Record is running                       |
| BTICard_SeqLogFrequency       | Specifies the period for Sequential Record Event Log List entries         |
| BTICard_SeqRd                 | Reads a single record out of the Sequential Record                        |
| BTICard_SeqResume             | Resumes recording of the Sequential Record where it stopped               |
| BTICard_SeqStart              | Starts recording at the beginning of the Sequential Record                |
| BTICard_SeqStatus             | Checks the status of the Sequential Record                                |
| BTICard_SeqStop               | Stops data from being added to the Sequential Record                      |
|                               |                                                                           |
| SYSMON Functions              |                                                                           |
| BTICard_SysMonClear           | Resets the historic maximum and minimum values for all sensors            |
| BTICard_SysMonDescGet         | Returns the description of a sensor                                       |
| BTICard_SysMonMaxRd           | Reads the historical maximum value of a sensor                            |
| BTICard_SysMonMinRd           | Reads the historical minimum value of a sensor                            |
| BTICard_SysMonNomRd           | Reads the nominal value of a voltage sensor                               |
| BTICard_SysMonThresholdGet    | Reads the user thresholds of a temperature sensor                         |
| BTICard_SysMonThresholdSet    | Sets the user thresholds of a temperature sensor                          |
| BTICard_SysMonTypeGet         | Returns the type of sensor                                                |
| BTICard_SysMonUserStr         | Returns a formatted value string of sensor data                           |
| BTICard_SysMonValRd           | Reads the current sensor value                                            |
| TIMER Functions               |                                                                           |
| BTICard_Timer64Rd             | Reads the current value of the Device timer (64 bit)                      |
| BTICard_Timer64Wr             | Writes a value to the Device timer (64 bit)                               |
| BTICard_TimerClear            | Clears the Device timer                                                   |
| BTICard_TimerRd               | Reads the current value of the Device timer (32 bit)                      |
| BTICard_TimerResolution       | Selects a time-tag timer resolution                                       |
| BTICard_TimerStatus           | Checks status of the timer configuration                                  |
| BTICard_TimerWr               | Writes the a value to the Device timer (32 bit)                           |
|                               |                                                                           |

*[Table 5.1—](#page-164-2)Protocol-Independent (BTICard\_ ) Functions (Continued) (Continued Next Page)*

| Timing Synchronization Manager (TSM) Functions |                                                                           |  |
|------------------------------------------------|---------------------------------------------------------------------------|--|
| BTICard_TSMDriftMaxGet                         | Reads the current maximum drift bias limit (ppt)                          |  |
| BTICard_TSMDriftMaxSet                         | Sets the maximum drift bias limit (ppt)                                   |  |
| BTICard_TSMDriftRd                             | Reads the current commanded drift bias (ppt)                              |  |
| BTICard_TSMDriftWr                             | Sets the drift bias (ppt)                                                 |  |
| BTICard_TSMInputDelayCompGet                   | Reads the current input delay compensation (ns)                           |  |
| BTICard_TSMInputDelayCompSet                   | Sets the input delay compensation (adjusts timer to IRIG delays) (ns)     |  |
| BTICard_TSMInputThresholdGet                   | Reads the DAC value for the requested input                               |  |
| BTICard_TSMInputThresholdSet                   | Sets the DAC value for the requested input                                |  |
| BTICard_TSMIRIGControlRd                       | Reads 'control' field from last received IRIG message                     |  |
| BTICard_TSMIRIGControlWr                       | Writes 'control' field to outgoing IRIG messages                          |  |
| BTICard_TSMIRIGYearsRd                         | Reads 'Years' field from device, Set via IRIG if enabled                  |  |
| BTICard_TSMIRIGYearsWr                         | Writes (BCD) year to device, Sent via IRIG if enabled.                    |  |
| BTICard_TSMJumpThresholdSet                    | Sets minimum value that forces device timer to 'jump' (ns)                |  |
| BTICard_TSMJumpThresholdGet                    | Reads currently set value that forces device timer to 'jump' (ns)         |  |
| BTICard_TSMReset                               | Resets the Timing Synchronization Manager                                 |  |
| BTICard_TSMSourceConfig                        | Sets the output (source) configuration for the TSM (IRIG, PPS, 10 MHz)    |  |
| BTICard_TSMStatus                              | Queries the current TSM operating status                                  |  |
| BTICard_TSMSyncConfig                          | Sets the input (sync) configuration for the TSM (IRIG, PPS, 10 MHz, Host) |  |
| BTICard_TSMTimerRd                             | Reads the current device time (ns)                                        |  |
| BTICard_TSMTimerRelWr                          | Jumps the current device time by the input amount (ns)                    |  |
| BTICard_TSMTimerRolloverGet                    | Reads the current year rollover value (ns)                                |  |
| BTICard_TSMTimerRolloverSet                    | Writes the year rollover value (ns)                                       |  |
| BTICard_TSMTimerWr                             | Writes the value to the TSM (Core) Timer (ns)                             |  |
| UTILITY Functions                              |                                                                           |  |
| BTICard_ErrDescStr                             | Returns the description of the specified error value                      |  |
| BTICard_ValFromAscii                           | Creates an integer value from an ASCII string                             |  |
| BTICard_ValGetBits                             | Extracts a bit field from an integer value                                |  |
| BTICard_ValPutBits                             | Puts a bit field into an integer value                                    |  |
| BTICard_ValToAscii                             | Creates an ASCII string from an integer                                   |  |

*[Table 5.1—](#page-164-2)Protocol-Independent (BTICard\_ ) Functions (Continued)*

### <span id="page-166-0"></span>**5.2 Function Detail**

The following pages contain descriptions of the BTIDriver functions (in alphabetical order without regard to prefix). The constants in bold in the tables are the default options. Note that the "BTICard\_" prefix have been omitted from the headings for easier reading, but all BTIDriver functions must begin with the appropriate prefix in source code.

### **BITConfig**

```
ERRVAL BTICard_BITConfig
```

```
(
     ULONG ctrlflags, //Selects configuration options
     HCARD hCard //Card handle
```
### )

#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Configures the Continuous Built-In Test (CBIT) functionality as defined by ctrlflags (see table below) for the card specified by hCard. Various functional blocks in the CBIT system can be enabled or disabled using the flags in the table below. Each of these blocks report to the BIT Status register that can be read using BTICard\_BITStatusRd. These blocks can also be configured to generate an Event Log list entry when an error occurs.

| ctrlflags                 |                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |  |  |
|---------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--|--|
| Constant                  | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                  |  |  |
| BITCFG_DEFAULT            | Select all default settings (bold below)                                                                                                                                                                                                                                                                                                                                                                                                                                     |  |  |
| BITCFG_MEMECC_ENABLE      | Enables the memory interface to operate in ECC mode. In this mode, if a single<br>bit error occurs, the read value will be corrected and<br>BITSTAT_SINGLE_BIT_ERR will be set in the BIT Status register. If a double<br>bit error occurs, the read value can't be corrected so the card will be stopped<br>and BITSTAT_DOUBLE_BIT_ERR will be set in the BIT Status register.                                                                                              |  |  |
| BITCFG_MEMECC_DISABLE     | Disables ECC operation of the memory.                                                                                                                                                                                                                                                                                                                                                                                                                                        |  |  |
| BITCFG_FPGA_ENABLE        | Enables monitoring for Single Event Upsets (SEU) in the FPGA configuration.<br>In this mode, if a Single Event Upset is detected the card will be stopped and<br>BITSTAT_CBIT_FPGA_ERR will be set in the BIT Status register.                                                                                                                                                                                                                                               |  |  |
| BITCFG_FPGA_DISABLE       | Disables monitoring for Single Event Upsets in the FPGA configuration.                                                                                                                                                                                                                                                                                                                                                                                                       |  |  |
| BITCFG_PROTOCOL_ENABLE    | Enables CBIT in the 1553 Protocol Engine. Every 1553 word transmitted by the<br>Card will be monitored and checked for accuracy. If the transceiver is damaged<br>or there is a collision on the bus, the protocol error bit will be set in the BIT<br>Status register. In addition, the MSGERR1553_SYSTEM bit will also be set in the<br>1553 Message Record and Sequential Record of the transmission that failed.                                                         |  |  |
| BITCFG_PROTOCOL_DISABLE   | Disables CBIT in the 1553 Protocol Engine.                                                                                                                                                                                                                                                                                                                                                                                                                                   |  |  |
| BITCFG_MEMECC_NOLOG       | Does not generate an event log entry when the ECC decoder detects a single<br>or double bit error in the on-card memory.                                                                                                                                                                                                                                                                                                                                                     |  |  |
| BITCFG_MEMECC_LOG         | Generates an event log entry when the ECC decoder detects a single or double<br>bit error in the on-card memory.                                                                                                                                                                                                                                                                                                                                                             |  |  |
| BITCFG_FPGA_NOLOG         | Does not generate an event log entry when an SEU is detected in the<br>FPGA Configuration.                                                                                                                                                                                                                                                                                                                                                                                   |  |  |
| BITCFG_FPGA_LOG           | Generates an event log entry when an SEU is detected in the FPGA<br>Configuration.                                                                                                                                                                                                                                                                                                                                                                                           |  |  |
| BITCFG_PROTOCOL_NOLOG     | Does not generate an event log entry when 1553 CBIT detects an<br>error.                                                                                                                                                                                                                                                                                                                                                                                                     |  |  |
| BITCFG_PROTOCOL_LOG       | Generates an event log entry when 1553 CBIT detects an error.                                                                                                                                                                                                                                                                                                                                                                                                                |  |  |
| BITCFG_CARD_STOPPED_NOLOG | Does not generate an event log entry when the Card is stopped due to<br>CBIT Errors.                                                                                                                                                                                                                                                                                                                                                                                         |  |  |
| BITCFG_CARD_STOPPED_LOG   | Generates an event log entry when the Card is stopped due to CBIT<br>Errors. When a temperature or voltage sensor value exceeds the<br>System limits for safe operation, the Card will automatically stop<br>protocol activity to reduce power draw. The Card will also stop when<br>an uncorrectable (double bit) error is detected in the memory or an<br>SEU is detected in the FPGA configuration. The card is stopped to<br>prevent the transmission of corrupted data. |  |  |

*(Continued on next page)*

#### (Continued from previous page)

| BITCFG_SYSMON_NOLOG | Does not generate an event log entry when SysMon detects that a<br>temperature sensor has exceeded the user definable thresholds.                                                                                   |  |
|---------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--|
| BITCFG_SYSMON_LOG   | Generates an event log entry when the SysMon detects that a temperature<br>sensor has exceeded the user definable thresholds. User definable thresholds<br>can be configured by calling BTICard_SysMonThresholdSet. |  |

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function will clear any errors in the status read by BTICard\_BITStatusRd. In order to generate Event Log list entries, the Event Log must be configured by calling BTICard\_EventLogConfig.

#### **SEE ALSO**

BTICard\_BITStatusRd, BTICard\_EventLogConfig, BTICard\_SysMonThresholdSet

### **BITInitiate**

```
ERRVAL BTICard_BITInitiate
```

```
(
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Executes a read and write memory test on the card specified by hCard. When the test completes, the Card is left in the same state as after a call to BTICard\_CardReset.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

Do not call when the Card is connected to an active databus. The function disrupts normal databus operation of the Card and the results will be unpredictable.

#### **SEE ALSO**

BTICard\_CardReset

### **BITStatusClear**

```
ERRVAL BTICard_BITStatusClear
(
     ULONG statval, //Mask of bits to clear
     HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Each bit in statval clears the corresponding bit in the BIT Status register for the Card specified by hCard. The constants in the table below may be bitwise OR-ed together to clear each specified status bit.

| Constant               | Description                             |  |
|------------------------|-----------------------------------------|--|
| BITSTAT_CARD_STOPPED   | CBIT fatal system error, card stopped   |  |
| BITSTAT_CBIT_FPGA_ERR  | CBIT FPGA Single Event Upset (SEU)      |  |
| BITSTAT _PROTOCOL_ERR  | CBIT Protocol Error                     |  |
| BITSTAT_SINGLE_BIT_ERR | CBIT ECC Single Bit Error (correctable) |  |
| BITSTAT_DOUBLE_BIT_ERR | CBIT ECC Double Bit Error               |  |
| BITSTAT_SYSMON_ERR     | CBIT SysMon Error                       |  |

Please refer to BTICard\_BITStatusRd for more information on these error flags.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_BITStatusRd

### **BITStatusRd**

```
ULONG BTICard_BITStatusRd
(
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The value of the BIT Status register.

#### **DESCRIPTION**

Reads the BIT Status register of the Card specified by *hCard*. The status value can be tested using the following predefined constants:

| Constant               | Description                                |
|------------------------|--------------------------------------------|
| BITSTAT_CARD_STOPPED   | PBIT/CBIT fatal system error, card stopped |
| BITSTAT_CBIT_FPGA_ERR  | CBIT FPGA Single Event Upset (SEU)         |
| BITSTAT_ PROTOCOL_ERR  | CBIT Protocol Error                        |
| BITSTAT_SINGLE_BIT_ERR | PBIT/CBIT ECC Single Bit Error (corrected) |
| BITSTAT_DOUBLE_BIT_ERR | PBIT/CBIT ECC Double Bit Error             |
| BITSTAT_SYSMON_ERR     | CBIT SysMon Error                          |

For SysMon errors, the position of the sensor's error bit is determined by the sensor index number.

```
if (BTICard_BITStatusRd(hCard) & (1 << index))
{
 // Handle error on the sensor at <index>
}
```
For more information on the other BIT errors and enabling/disabling reporting refer to BTICard\_BITConfig.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_BITStatusClear, BTICard\_BITConfig

### **CardClose**

ERRVAL **BTICard\_CardClose**

```
(
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Disables access to the specified Device and releases the associated hardware resources (e.g., memory and I/O space, interrupt number, and DMA channel). BTICard\_CardClose closes the Device and all of its cores opened with BTICard\_CoreOpen. This function does not stop the core(s) from operating (use BTICard\_CardStop to stop each core).

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Before a program terminates, this function MUST be called to release the associated hardware resources. This is especially important in Microsoft Windows operating systems.

#### **SEE ALSO**

BTICard\_CardOpen, BTICard\_CoreOpen, BTICard\_CardStop

### **CardGetInfo**

```
ULONG BTICard_CardGetInfo
(
     USHORT infotype, //Selects type of info to return
     INT channum, //Channel number
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The information specified by infotype.

#### **DESCRIPTION**

Returns various information from the Device specified by *hCore*. *infotype* selects the type of information to return (see table below).

*channum* is reserved for future use.

| infotype           |                                                                         |  |  |  |
|--------------------|-------------------------------------------------------------------------|--|--|--|
| Constant           | Description                                                             |  |  |  |
| INFOTYPE_PROD      | Returns the product info, which is usually the model number of the core |  |  |  |
| INFOTYPE_HWGEN     | Returns the hardware generation                                         |  |  |  |
|                    | 4 = 4G                                                                  |  |  |  |
|                    | 5 = 5G, 5G+                                                             |  |  |  |
|                    | 6 = 6G                                                                  |  |  |  |
| INFOTYPE_PLAT      | Returns the platform:                                                   |  |  |  |
|                    | 4734H = '4G'                                                            |  |  |  |
|                    | 4735H = '5G', '5G+'                                                     |  |  |  |
|                    | 4736H = '6G'                                                            |  |  |  |
| INFOTYPE_CARDTYPE  | 0001H = BUSBOX                                                          |  |  |  |
|                    | 0002H = PCI4G                                                           |  |  |  |
|                    | 0003H = cPCI4G                                                          |  |  |  |
|                    | 0004H = RPC                                                             |  |  |  |
|                    | 0005H = PMC4G                                                           |  |  |  |
|                    | 0011H = PM42                                                            |  |  |  |
|                    | 0012H = PCIE5G                                                          |  |  |  |
|                    | 0013H = USB5G                                                           |  |  |  |
|                    | 0014H = PCI5GV6                                                         |  |  |  |
|                    | 0015H = PM15                                                            |  |  |  |
|                    | 0016H = AB3000                                                          |  |  |  |
|                    | 0017H = PE1000                                                          |  |  |  |
|                    | 0018H = PCI6G                                                           |  |  |  |
|                    | 0019H = PXI6G                                                           |  |  |  |
|                    | 001AH = OBB6G                                                           |  |  |  |
|                    | 001BH = OBB2USB                                                         |  |  |  |
|                    | 001DH = AB3SN                                                           |  |  |  |
|                    | 001EH = ME1000                                                          |  |  |  |
|                    | 001FH = NG1/NG2/NG3                                                     |  |  |  |
|                    | 0020H = UA2000                                                          |  |  |  |
| INFOTYPE_VERSIONEX | Returns the firmware version of the form MMmmii                         |  |  |  |
|                    | MM = Major version (bits 16-23)                                         |  |  |  |
|                    | mm = Minor version (bits 8-15)                                          |  |  |  |
| INFOTYPE_DATE      | ii = Minor-minor version (bits 0-7)                                     |  |  |  |
|                    | Returns the firmware date of the form YYYYMMDD                          |  |  |  |
|                    | YYYY = Year (bits 16-31)<br>MM = Month (bits 8-15)                      |  |  |  |
|                    | DD = Day (bits 0-7)                                                     |  |  |  |
| INFOTYPE_SERIALNUM | Returns the serial number                                               |  |  |  |
| INFOTYPE_1553COUNT | Returns the number of channels of the specified protocol                |  |  |  |
| INFOTYPE_429COUNT  |                                                                         |  |  |  |
| INFOTYPE_717COUNT  |                                                                         |  |  |  |
| INFOTYPE_708COUNT  |                                                                         |  |  |  |
| INFOTYPE_422COUNT  |                                                                         |  |  |  |
| INFOTYPE_CSDBCOUNT |                                                                         |  |  |  |
| INFOTYPE_DIOCOUNT  |                                                                         |  |  |  |
| INFOTYPE_EBRCOUNT  |                                                                         |  |  |  |

#### **DEVICE DEPENDENCY**

See above table

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_CardProductStr, BTICard\_CardTypeStr

### **CardIsRunning**

BOOL **BTICard\_CardIsRunning**

```
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the core is still running, otherwise FALSE.

#### **DESCRIPTION**

Determines whether the core specified by *hCore* is running.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop

### **CardOpen**

ERRVAL **BTICard\_CardOpen**

```
(
      LPHCARD lpHandle, //Pointer to the card handle
      INT cardnum //Card number of Device
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Enables access to a Device. If BTICard\_CardOpen finds the Device that has been assigned *cardnum*, it performs a quick hardware self-test of the Device. Since this function opens the Device and provides a card handle parameter required by BTICard\_CoreOpen (which returns the core handle used by all other functions), this function is always the first BTIDriver function called by a program.

Card numbers are assigned to Devices by the operating system or the user. If only one Device has been installed, the system defaults the card number to zero. How the system assigns card numbers for multiple Devices and how the number can be changed by the user is OS-dependent. See the README.TXT file for your operating system on the distribution disk for more information. A test program for determining the card number(s) is provided on the distribution disk. The card numbers assigned to BTIDriver Devices are specific to BTIDriver-compliant Devices, so there is no conflict when non-BTIDriver-compliant Devices use those same card numbers.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

BTICard\_CardClose must be called to release the hardware resources before the program terminates.

#### **SEE ALSO**

BTICard\_CardClose

### **CardProductStr**

```
LPCSTR BTICard_CardProductStr
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A pointer to a character string describing the Device specified by *hCore*.

#### **DESCRIPTION**

Returns specific product information for the Device specified by *hCore*.

BTICard\_CardTypeStr identifies the family to which a Device belongs. Inside of that family, BTICard\_CardProductStr specifies product information such as model number, level or functionality, or configuration. Combine these functions to identify your Device.

### **DEVICE DEPENDENCY**

Applies to all Devices.

**WARNINGS** None.

**SEE ALSO** BTICard\_CardGetInfo, BTICard\_CardTypeStr

### **CardReset**

```
VOID BTICard_CardReset
```

```
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Stops and performs a hardware reset on the core specified by *hCore*. If a message is being processed, the processing is allowed to finish before the core is halted.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

Does not reset historic maximum and minimum Sysmon sensor values; to reset these values use BTICard\_SysMonClear.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop, BTICard\_SysMonClear

### **CardResume**

```
ERRVAL BTICard_CardResume
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Reactivates the specified core from the point at which it was stopped (using BTICard\_CardStop). The following table compares the difference between calling BTICard\_CardResume and BTICard\_CardStart:

| Feature            | When CardStart is called….                                 | When CardResume is called….                                          |
|--------------------|------------------------------------------------------------|----------------------------------------------------------------------|
| Transmit Schedule  | Execution starts at the start of the transmit<br>Schedule. | Execution resumes at the point the transmit<br>Schedule was stopped. |
| Event Log List     | Any unread entries in the Event Log List are               | Any unread records in the Event Log List are                         |
|                    | cleared before the core is started.                        | preserved as the core is resumed.                                    |
| Sequential Monitor | Any unread records in the Sequential Record                | Any unread records in the Sequential Record                          |
|                    | are cleared before the core is started.                    | are preserved as the core is resumed.                                |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTICard\_CardStop must precede this function.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardStop

### **CardStart**

```
ERRVAL BTICard_CardStart
(
```

```
HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Activates all configured channels of the specified core. The Sequential Monitor and Event Log List are cleared and begin operation at the start of their allocated buffers.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

The core continues operating even after an application program ends unless BTICard\_CardStop halts it. Even after BTICard\_CardStart, individual channels may not transmit or receive if they are disabled or paused. See the channel configuration and control functions for each protocol.

#### **SEE ALSO**

BTICard\_CardStop, BTICard\_CardIsRunning

### **CardStop**

```
BOOL BTICard_CardStop
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the core was active, otherwise FALSE.

#### **DESCRIPTION**

Stops operation of all the channel on the specified core. If a message is being processed, the processing is allowed to finish before the core is halted.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_CardStart, BTICard\_CardIsRunning

### **CardSyncValid**

```
USHORT BTICard_CardSyncValid
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A bitwise OR-ed value of *syncmask* (refer to table) for Device's valid syncs.

| syncmask          |                               |  |  |  |  |
|-------------------|-------------------------------|--|--|--|--|
| Constant          | Description                   |  |  |  |  |
| SYNCMASK_SYNCA    | Selects discrete sync line A  |  |  |  |  |
| SYNCMASK_PXITRIGA | Selects PXIe TRIG sync line A |  |  |  |  |
| SYNCMASK_SYNCB    | Selects discrete sync line B  |  |  |  |  |
| SYNCMASK_PXITRIGB | Selects PXIe TRIG sync line B |  |  |  |  |
| SYNCMASK_SYNCC    | Selects discrete sync line C  |  |  |  |  |
| SYNCMASK_PXITRIGC | Selects PXIe TRIG sync line C |  |  |  |  |
| SYNCMASK_PXISTARC | Selects PXIe STAR sync line C |  |  |  |  |

#### **DESCRIPTION**

This function may be called from an application to verify Device-specific sync support.

#### **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices.

The mapping of sync lines is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

```
 BTI1553_BCSyncDefine, BTI1553_MsgSyncDefine, BTI1553_RTSyncDefine
```
### **CardTest**

```
ERRVAL BTICard_CardTest
(
```

```
USHORT level, //Level of tests to perform
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Executes a hardware test specified by *level* on the core specified by *hCore*. When the test completes, the core is left in the same state as after a BTICard\_CardReset call.

| Level        |                                                                                                                                                                                        |  |  |
|--------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--|--|
| Constant     | Description                                                                                                                                                                            |  |  |
| TEST_LEVEL_0 | Tests the I/O interface of the core. The test reads and writes each I/O with a walking-bit pattern.                                                                                    |  |  |
| TEST_LEVEL_1 | In addition to Level 0, this level tests the memory interface of the core. The test performs a pattern<br>test of the RAM.                                                             |  |  |
| TEST_LEVEL_2 | In addition to previous levels, this level tests the communication process of the core. The test<br>performs a pattern test of the RAM using the communication process.                |  |  |
| TEST_LEVEL_3 | In addition to previous levels, this level tests the encoders and decoders of the core. Where possible,<br>the core monitors its own transmissions to validate protocol functionality. |  |  |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

This function disrupts normal operation of the core. For TEST\_LEVEL\_3, do not use this function when the core is connected to an active databus, as the results will be unpredictable.

#### **SEE ALSO**

BTICard\_CardReset

### **CardTrigger**

```
VOID BTICard_CardTrigger
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Generates a software-simulated external trigger signal on all available trigger lines. For Devices with multiple trigger lines, BTICard\_CardTriggerEx can be used to specify lines individually.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

## **WARNINGS**

None.

```
BTICard_CardTriggerEx, BTI1553_BCTriggerDefine,BTI1553_MsgTriggerDefine, 
BTI1553_ErrorTriggerDefine
```
### **CardTriggerEx**

```
VOID BTICard_CardTriggerEx
(
      USHORT trigmask, //Line(s) to trigger via software
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Simulates an external trigger signal on the trigger input line(s) specified by *trigmask*. The constants in the table below may be bitwise OR-ed together to trigger multiple lines.

| trigmask       |                        |  |  |  |
|----------------|------------------------|--|--|--|
| Constant       | Description            |  |  |  |
| TRIGMASK_TRIGA | Selects trigger line A |  |  |  |
| TRIGMASK_TRIGB | Selects trigger line B |  |  |  |
| TRIGMASK_TRIGC | Selects trigger line C |  |  |  |

#### **DEVICE DEPENDENCY**

Though this function is intended for 4G, 5G, 5G+, and 6G Devices, which have multiple trigger lines, using a trigmask value of TRIGMASK\_TRIGA on 3G Devices produces the same result as BTICard\_CardTrigger.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_CardTrigger,BTI1553\_BCTriggerDefine,BTI1553\_MsgTriggerDefine, BTI1553\_ErrorTriggerDefine,BTICard\_CardTriggerValid

### **CardTriggerValid**

```
USHORT BTICard_CardTriggerValid
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A bitwise OR-ed value of *trigmask* (refer to table) for Device's valid triggers.

| trigmask          |                                  |  |  |  |
|-------------------|----------------------------------|--|--|--|
| Constant          | Description                      |  |  |  |
| TRIGMASK_TRIGA    | Selects discrete trigger line A  |  |  |  |
| TRIGMASK_PXITRIGA | Selects PXIe TRIG trigger line A |  |  |  |
| TRIGMASK_PXISTARA | Selects PXIe STAR trigger line A |  |  |  |
| TRIGMASK_TRIGB    | Selects discrete trigger line B  |  |  |  |
| TRIGMASK_PXITRIGB | Selects PXIe TRIG trigger line B |  |  |  |
| TRIGMASK_PXISTARB | Selects PXIe STAR trigger line B |  |  |  |
| TRIGMASK_TRIGC    | Selects discrete trigger line C  |  |  |  |
| TRIGMASK_PXITRIGC | Selects PXIe TRIG trigger line C |  |  |  |
| TRIGMASK_PXISTARC | Selects PXIe STAR trigger line C |  |  |  |

#### **DESCRIPTION**

This function may be called from an application to verify Device-specific trigger support.

#### **DEVICE DEPENDENCY**

Applies to 4G, 5G, 5G+, and 6G Devices.

PXIe Triggers are only available on PXIe Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTI1553\_BCTriggerDefine,BTI1553\_ErrorTriggerDefine, BTI1553\_MsgTriggerDefine

### **CardTypeStr**

LPCSTR **BTICard\_CardTypeStr** ( HCORE *hCore //Core handle* )

#### **RETURNS**

A pointer to a character string describing the Device specified by *hCore*.

#### **DESCRIPTION**

Returns the type of Device specified by *hCore*.

BTICard\_CardTypeStr identifies the family to which a Device belongs. Inside of that family, BTICard\_CardProductStr specifies product information such as model number, level of functionality, or configuration. Combine these functions to identify your Device.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_CardGetInfo, BTICard\_CardProductStr

### **CoreOpen**

```
ERRVAL BTICard_CoreOpen
```
LPHCORE *lphCore*, *//Pointer to a core handle* INT *corenum*, *//Core number* HCARD *hCard //Card handle*

#### **RETURNS**

(

)

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Enables access to a core (the presence of multiple cores is Device-dependent).

BTICard\_CardOpen must first be called to obtain the card handle (*hCard*). BTICard\_CoreOpen finds the core on the Device specified by *hCard that* has been assigned *corenum*, and returns a handle to that core. BTICard\_CoreOpen must be called for each core that you wish to access in your program. BTICard\_CardClose will close all cores opened with BTICard\_CoreOpen.

If you pass the card handle to a function (such as a channel function) instead of a core handle, it will only access the first (or only) core.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

BTICard\_CardOpen must be called before this function. BTICard\_CoreOpen must be called for each core that you wish to access in your program.

#### **SEE ALSO**

BTICard\_CardOpen, BTICard\_CardClose

### **ErrDescStr**

```
LPCSTR BTICard_ErrDescStr
(
     ERRVAL errval, //An error value
     HCORE hCore //Core handle
```
)

#### **RETURNS**

A pointer to a character string describing the error specified by *errval*.

#### **DESCRIPTION**

Describes the error value specified by *errval*.

All functions of type ERRVAL return a negative value if an error occurs, or zero if successful. BTICard\_ErrDescStr returns a description of the specified error value.

*Note: This is a utility function and does not access the Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

**WARNINGS** None.

**SEE ALSO**

ERRVAL type functions

### **EventLogConfig**

```
ERRVAL BTICard_EventLogConfig
(
     USHORT ctrlflags //Selects the configuration options
     USHORT count //Number of entries in the Event Log List
     HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Configures and enables the Event Log List of the core specified by *hCore*. The maximum number of entries that may be contained in the Event Log List is set by *count*. Ctrlflags can be one of the following constants:

| ctrlflags      |                                     |
|----------------|-------------------------------------|
| Constant       | Condition                           |
| LOGCFG_DEFAULT | Selects all default (bold) settings |
| LOGCFG_ENABLE  | Enables the Event Log List          |
| LOGCFG_DISABLE | Disables the Event Log List         |

#### **DEVICE DEPENDENCY**

The size of the Event Log for 5G, 5G+, and 6G Devices is always 256 entries regardless of count.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_EventLogRd, BTICard\_EventLogStatus

### **EventLogRd**

```
ULONG BTICard_EventLogRd
(
      LPUSHORT typeval, //Pointer to variable to receive type value
      LPULONG infoval, //Pointer to variable to receive info value
      LPINT channel, //Pointer to variable to receive channel value
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The address of the entry in the Event Log List, or zero if it is empty and there are no entries to read.

#### **DESCRIPTION**

Reads the next entry from the Event Log List and advances the pointer. The type of event and channel that generated the entry are passed through *typeval* and *channel.* An information word associated with the event is passed through *infoval*.

The value of *typeval* determines the meaning of the *infoval* value (see table below). Note that the Event Log List records events from all protocols implemented on the specified core. The first two event types, regarding the Sequential Record, are protocol-independent. The rest of the table is subdivided by protocol.

|              | typeval                  | Description                                                                 | infoval                                | Refer to…                                                                                  |
|--------------|--------------------------|-----------------------------------------------------------------------------|----------------------------------------|--------------------------------------------------------------------------------------------|
| General      | EVENTTYPE_SEQFULL        | Sequential Record full (halted)<br>or overwritten                           | Address of last<br>entry               | BTICard_SeqConfig                                                                          |
|              | EVENTTYPE_SEQFREQ        | th entry (user-specified)<br>n                                              | Address of last<br>entry               | BTICard_SeqConfig                                                                          |
|              | EVENTTYPE_BITERROR       | BIT system detected an error                                                | BIT Status at the<br>time of the Error | BTICard_BITConfig                                                                          |
|              | EVENTTYPE_1553MSG        | Message processed                                                           | Address of the<br>Message<br>structure | BTI1553_BCCreateMsg<br>BTI1553_RTCreateMsg<br>BTI1553_BCCreateList<br>BTI1553_RTCreateList |
| MIL-STD-1553 | EVENTTYPE_1553OPCODE     | BC Schedule encountered a<br>LOG command                                    | User-assigned<br>tag value             | BTI1553_BCSchedLog                                                                         |
|              | EVENTTYPE_1553HALT       | BC Schedule encountered a<br>HALT command                                   | Address of the<br>Schedule entry       | BTI1553_BCSchedHalt                                                                        |
|              | EVENTTYPE_1553PAUSE      | BC Schedule encountered a<br>PAUSE command                                  | Address of the<br>Schedule entry       | BTI1553_BCSchedPause                                                                       |
|              | EVENTTYPE_1553LIST       | List Buffer empty/ full<br>(underflow or overflow). Does<br>not work in 4G. | List Buffer<br>address                 | BTI1553_BCCreateList<br>BTI1553_RTCreateList                                               |
| Serial       | EVENTTYPE_422TXTHRESHOLD | TX FIFO under threshold                                                     | None                                   | BTI422_FIFOConfigTx                                                                        |
|              | EVENTTYPE_422TXFIFO      | TX FIFO underflow                                                           | None                                   | BTI422_FIFOConfigTx                                                                        |
|              | EVENTTYPE_422RXTHRESHOLD | RX FIFO over threshold                                                      | None                                   | BTI422_FIFOConfigRx                                                                        |
|              | EVENTTYPE_422RXFIFO      | RX FIFO overflow                                                            | None                                   | BTI422_FIFOConfigRx                                                                        |
|              | EVENTTYPE_422RXERROR     | RX error                                                                    | None                                   | BTI422_FIFOConfigRx                                                                        |

(Continued on next page)

|             | typeval              | Description                              | infoval                   | Refer to…                 |
|-------------|----------------------|------------------------------------------|---------------------------|---------------------------|
|             | EVENTTYPE_429MSG     | Message received or                      | Address of the Message    | BTI429_MsgCreate          |
|             |                      | transmitted                              | Record                    | BTI429_FilterDefault      |
|             |                      |                                          |                           | BTI429_FilterSet          |
|             | EVENTTYPE_429OPCODE  | A transmit Schedule                      | User-assigned tag value   |                           |
|             |                      | encountered a LOG                        |                           | BTI429_SchedLog           |
| ARINC 429   |                      | command                                  |                           |                           |
|             | EVENTTYPE_429HALT    | A transmit Schedule                      | Address of the Schedule   | BTI429_ChConfig           |
|             |                      | encountered a HALT                       | entry                     | BTI429_SchedHalt          |
|             |                      | command                                  |                           |                           |
|             | EVENTTYPE_429PAUSE   | A transmit Schedule                      | Address of the Schedule   | BTI429_ChConfig           |
|             |                      | encountered a                            | entry                     | BTI429_SchedPause         |
|             | EVENTTYPE_429LIST    | PAUSE command<br>List Buffer empty or    | List Buffer address       | BTI429_ListAsyncCreate    |
|             |                      | full (underflow or                       |                           | BTI429_ListRcvCreate      |
|             |                      | overflow)                                |                           | BTI429_ListXmtCreate      |
|             | EVENTTYPE_429ERR     | A decoder error was                      | Address of the message    |                           |
|             |                      | detected                                 | that contained the error  | BTI429_ChConfig           |
|             | EVENTTYPE_717WORD    | Processed 717 word                       | Word address              | BTI717_SubFrmWordConfig   |
|             |                      |                                          |                           | BTI717_SuperFrmWordConfig |
|             | EVENTTYPE_717SUBFRM  | Processed 717                            | Subframe number           | BTI717_SubFrmWordConfig   |
| ARINC 717   |                      | subframe                                 |                           | BTI717_SuperFrmWordConfig |
|             | EVENTTYPE_717SYNCERR | 717 receive channel                      | Channel number            | BTI717_ChConfig           |
|             |                      | lost sync                                |                           |                           |
|             | EVENTTYPE_708MSG     | Message received/                        | Message index             |                           |
|             |                      | transmitted                              |                           | BTI708_RcvBuild           |
|             |                      |                                          |                           | BTI708_XmtBuild           |
| ARINC 708   |                      |                                          |                           |                           |
|             | EVENTTYPE_EBRMSG     | Message processed                        | Address of the Message    | BTIEBR_BCCreateMsg        |
|             |                      |                                          | structure                 | BTIEBR_RTCreateMsg        |
|             |                      |                                          |                           | BTIEBR_BCCreateList       |
|             |                      |                                          |                           | BTIEBR_RTCreateList       |
|             | EVENTTYPE_EBROPCODE  | BC Schedule                              | User-assigned tag value   |                           |
|             |                      | encountered a LOG                        |                           | BTIEBR_BCSchedLog         |
| EBR-1553    |                      | command                                  |                           |                           |
|             | EVENTTYPE_EBRHALT    | BC Schedule                              | Address of the Schedule   |                           |
|             |                      | encountered a HALT                       | entry                     | BTIEBR_BCSchedHalt        |
|             |                      | command                                  |                           |                           |
|             | EVENTTYPE_EBRPAUSE   | BC Schedule                              | Address of the Schedule   |                           |
|             |                      | encountered a                            | entry                     | BTIEBR_BCSchedPause       |
|             | EVENTTYPE_EBRLIST    | PAUSE command                            |                           |                           |
|             |                      | List Buffer empty/ full<br>(underflow or | List Buffer address       | BTIEBR_BCCreateList       |
|             |                      | overflow)                                |                           | BTIEBR_RTCreateList       |
|             | EVENTTYPE_DIOEDGE    | An edge transition                       | 8-bit mask specifying     |                           |
|             |                      | occurred                                 | which I/O had an edge, 8- | BTIDIO_BankConfig         |
|             |                      |                                          | bit state of the bank I/O | BTIDIO_DiscreteConfig     |
| Discrete IO |                      |                                          | after edge.               |                           |
|             | EVENTTYPE_DIOFAULT   | A fault occurred                         | 8-bit mask specifying     | BTIDIO_BankConfig         |
|             |                      |                                          | which I/O had a fault     | BTIDIO_DiscreteConfig     |

| (Continued from previous page) |  |  |
|--------------------------------|--|--|
|                                |  |  |

#### **DEVICE DEPENDENCY**

The value passed back in *channel* may not be valid for all types of events. If an event does not have an associated channel value, it is filled in with the value FFh.

A typeval of EVENTTYPE\_1553LIST does not work in 4G (see [Table 6.1\)](#page-291-1).

#### **WARNINGS**

This function should be preceded by a call to BTICard\_EventLogConfig. To use this function, it is not necessary to install an interrupt handler.

#### **SEE ALSO**

BTICard\_EventLogConfig

### **EventLogStatus**

```
INT BTICard_EventLogStatus
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The status value of the Event Log List.

#### **DESCRIPTION**

Checks the status of the Event Log List without removing an entry. The status value can be tested using the predefined constants below:

| Constant     | Description                        |
|--------------|------------------------------------|
| STAT_EMPTY   | Event Log List is empty            |
| STAT_PARTIAL | Event Log List is partially filled |
| STAT_FULL    | Event Log List is full             |
| STAT_OFF     | Event Log List is off              |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

When the buffer is full it wraps around and overwrites previous entries.

#### **SEE ALSO**

BTICard\_EventLogConfig, BTICard\_EventLogRd

### **ExtDIOMonConfig**

```
ERRVAL BTICard_ExtDIOMonConfig
(
      USHORT risemask, //Discrete bitmask to monitor rising edges
      USHORT fallmask, //Discrete bitmask to monitor falling edges
      INT banknum, //Bank number of DIO
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Enables Sequential Monitoring on specific transitions of discrete inputs. Discrete inputs are sampled at a minimum rate of 100 kHz and compared to the previously sampled values. If the digital values of any discretes specified in the risemask have transitioned from a zero to a one, or the digital values of any discretes specified in the fallmask have transitioned from a one to a zero, then a Sequential Record will be created. To disable previously-enabled monitor settings, call BTICard\_ExtDIOMonConfig again with *risemask* and *fallmask* both set to zero.

The *risemask* and *fallmask* are a bit mask specifying up to 16 discrete input signals. For banknum 0, the LSB of each value corresponds to dionum 1 and the MSB corresponds to *dionum* 16. See table below for dionum higher than 16.

| dionum to banknum and bitmask Translation |         |             |             |  |
|-------------------------------------------|---------|-------------|-------------|--|
| dionum Range                              | banknum | bitmask LSB | bitmask MSB |  |
| 1-16                                      | 0       | 1           | 16          |  |
| 17-32                                     | 1       | 17          | 32          |  |
| 33-48                                     | 2       | 33          | 48          |  |
| 49-64                                     | 3       | 49          | 64          |  |

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+ and 6G Devices. The mapping of dionum to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

By default, avionics discretes are active low. A grounded avionics discrete will have a digital value of one. If the polarity for that discrete input has been changed, the digital value compared for rising and falling edges will be reversed.

#### **SEE ALSO**

BTICard\_SeqFindNextDIO

### **ExtDIOEnWr**

#### BOOL **BTICard\_ExtDIOEnWr**

```
(
      INT dionum, //Specifies the DIO number
      BOOL dioval, //The value to set
      BOOL dioen, //The output enable to set
      HCORE hCore //Core handle
```
#### )

#### **RETURNS**

None.

#### **DESCRIPTION**

Sets the digital I/O pin specified by dionum to the value specified by dioval with an output enable specified by dioen*.* A dioen of zero tri-states the pin, and a dioen of one sets the pin to the value specified by dioval. A dioval of zero sets the pin to inactive, and a *dioval* of one sets the pin to active.

#### **DEVICE DEPENDENCY**

Only applies to 6G Devices. If the DIO referenced by *dionum* cannot be tri-stated, then *dioen* will be ignored.

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

Some discretes are avionics discretes while others are digital I/O discretes. When using the digital I/O as an output (*dioen* set to one), do not drive the digital I/O pin from an external source as this may damage the Device. Please consult the hardware manual for the Device.

#### **SEE ALSO**

BTICard\_ExtDIORd, BTICard\_ExtDIOWr

### **ExtDIORd**

#### BOOL **BTICard\_ExtDIORd**

```
(
      INT dionum, //Specifies the DIO number
      HCORE hCore //Core handle
)
```
#### **RETURNS**

Status of the digital I/O pin. Returns a zero if the pin is inactive or a one if the pin is active.

#### **DESCRIPTION**

Reads the status of the digital I/O pin specified by *dionum*.

#### **DEVICE DEPENDENCY**

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ExtDIOWr, BTICard\_ExtDIOENWr

### **ExtDIOWr**

```
VOID BTICard_ExtDIOWr
(
      INT dionum, //Specifies the DIO number
      BOOL dioval, //The value to set
```

```
HCORE hCore //Core handle
```
)

#### **RETURNS**

None.

#### **DESCRIPTION**

Sets the digital I/O pin specified by *dionum* to the value specified by *dioval*. A *dioval* of zero sets the pin to inactive, and a *dioval* of one sets the pin to active.

#### **DEVICE DEPENDENCY**

The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device.

#### **WARNINGS**

Some discretes are avionics discretes while others are digital I/O discretes. When using the digital I/O as an output (as this function does), do not drive the digital I/O pin from an external source as this may damage the Device. Please consult the hardware manual for the Device.

#### **SEE ALSO**

BTICard\_ExtDIORd, BTICard\_ExtDIOENWr

### **ExtLEDRd**

### INT **BTICard\_ExtLEDRd**

```
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

Returns a zero if the LED is off or a one if the LED is on.

#### **DESCRIPTION**

Reads the state of the on-board LED.

#### **DEVICE DEPENDENCY**

4G, 5G, 5G+, and 6G Devices have a user-controlled LED for each core. For all other Devices, please refer to the hardware manual.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ExtLEDWr

### **ExtLEDWr**

```
VOID BTICard_ExtLEDWr
(
      INT ledval, //New state of the LED
      HCORE hCore //Core handle
)
```
**RETURNS** None.

#### **DESCRIPTION**

Sets the state of the onboard LED. An *ledval* of zero turns the LED off and an *ledval* of one turns the LED on.

#### **DEVICE DEPENDENCY**

4G, 5G, 5G+, and 6G Devices have a user-controlled LED for each core. For all other Devices, please refer to the hardware manual.

#### **WARNINGS**

None.

**SEE ALSO**

BTICard\_ExtLEDRd

### **ExtStatusLEDRd**

#### VOID **BTICard\_ExtStatusLEDRd**

```
LPINT ledval, //Pointer to variable to receive LED state
     LPINT ledcolor, //Pointer to variable to receive LED color
     HCORE hCore //Core handle
)
```
#### **RETURNS**

(

None.

#### **DESCRIPTION**

Reads the state of the onboard Status LED. A zero value will be passed to *ledval* if the LED is off, and a one value if the LED is on.

#### **DEVICE DEPENDENCY**

4G Devices have a red status LED.

BUSBox BB1xxx Devices have a multi-color Status LED. The color state of that LED can be read through *ledcolor.* A zero value indicates a red color, and a one value indicates a green color.

For all other Devices, please refer to the Device specific hardware manual.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ExtStatusLEDWr, BTICard\_ExtLEDRd

### **ExtStatusLEDWr**

#### VOID **BTICard\_ExtStatusLEDWr**

```
(
```
)

```
HCORE hCore //Core handle
```
INT *ledval*, *//New state of the LED* INT *ledcolor*, *//New color of the LED*

### **RETURNS**

None.

#### **DESCRIPTION**

Sets the state of the onboard Status LED. An *ledval* of zero turns the LED off and an *ledval* of one turns the LED on.

#### **DEVICE DEPENDENCY**

4G Devices have a red status LED.

BUSBox BB1xxx Devices have a multi-color Status LED. The color state of that LED can be controlled through *ledcolor.* A zero value indicates a red color, and a one value indicates a green color.

RPC Devices have a red Status LED that indicates a succesful booting of the Device. Afterwards, the Status LED can be controlled with this function.

UA1xxx and UA2xxx Devices have a red Status LED that is both user-controllable and can indicate that a MIL-STD-1553 or EBR-1553 protocol protocol error has occurred. The on/off state of the Status LED is controlled through *ledval* when the *ledcolor* value is set to zero. The enabling/disabling of the 1553 protocol error indication is controlled through *ledval* when the *ledcolor* value is set to one. When this feature is enabled, the Status LED will be automaticaly set by Device hardware when a MIL-STD 1553 or EBR-1553 protocol error occurs. The Status LED can then be cleared by calling this function.

For all other Devices, please refer to the Device specific hardware manual.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ExtStatusLEDRd, BTICard\_ExtLEDWr

### **IntClear**

```
VOID BTICard_IntClear
```

```
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Clears the interrupt from the core so it is ready for the next interrupt. Typically, the user's worker thread calls this function. Because the core cannot process another interrupt until the current one is cleared, BTICard\_IntClear should be called after each interrupt has been processed.

#### **DEVICE DEPENDENCY**

Applies to all Devices except those controlled via RPC.

#### **WARNINGS**

If another interrupt occurs before BTICard\_IntClear is called, the new interrupt is lost.

#### **SEE ALSO**

BTICard\_IntInstall, BTICard\_IntUninstall

### **IntInstall**

```
ERRVAL BTICard_IntInstall
```

```
(
     HCORE hCore //Core handle
```

```
LPVOID hEvent, //Handle of a WIN32 event object
```

```
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

BTICard\_IntInstall associates a WIN32 event object with interrupts from the core specified by *hCore*. If the function is successful, any interrupt issued from *hCore* causes the event object specified by *hEvent* to be set to the signaled state.

The user's application must ensure that the event object is set to the unsignaled state before the core issues the first interrupt. This can be done when creating the event object with the WIN32 API function CreateEvent.

Create a worker thread, which immediately goes to sleep by calling a WIN32 API wait function like WaitForSingleObject. When the Device issues an interrupt, the event object is signaled, and the worker thread wakes up to respond to the interrupt. The interrupt is generated whenever an entry is written to the Event Log List.

It is the user's responsibility to clear the interrupt from the core by calling BTICard\_IntClear in the worker thread. Note that event objects are never polled.

*Note: BTICard\_IntInstall should be called separately for each core on the Device, and there should be separate interrupt service threads for each core.*

#### **DEVICE DEPENDENCY**

Applies to all Devices except those controlled via RPC.

#### **WARNINGS**

If this function is used, BTICard\_IntUninstall MUST be called before the user's program terminates. It removes the association between the Device and the event object.

#### **SEE ALSO**

BTICard\_EventLogRd, BTICard\_IntUninstall

### **IntUninstall**

ERRVAL **BTICard\_IntUninstall**

```
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Removes the association between interrupts from the core specified by *hCore* and WIN32 event objects created by the BTICard\_IntInstall function. The Event Log List of the core remains unchanged.

#### **DEVICE DEPENDENCY**

Applies to all Devices except those controlled via RPC.

#### **WARNINGS**

This function must be called before the user's application terminates if BTICard\_IntInstall has been called.

#### **SEE ALSO**

BTICard\_IntInstall

### **IRIGConfig**

```
ERRVAL BTICard_IRIGConfig
```

```
(
     ULONG ctrlflags, //Selects IRIG configuration options
     HCORE hCore //Core handle
```
)

#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Configures the onboard IRIG circuit as defined by *ctrlflags* (see table below) for the core specified by *hCore*. IRIG timers are configured and enabled for each core independently.

| Ctrlflags       |                                          |             |     |
|-----------------|------------------------------------------|-------------|-----|
| Constant        | Description                              | Rcv         | Xmt |
| IRIGCFG_DEFAULT | Select all default settings (bold below) | x           | x   |
| IRIGCFG_ENABLE  | Enables the IRIG timer                   | x           | x   |
| IRIGCFG_DISABLE | Disables the IRIG timer                  | x           | x   |
| IRIGCFG_SPEEDB  | Enables IRIGB timing                     | x           | x   |
| IRIGCFG_SPEEDA  | Enables IRIGA timing                     | x           | x   |
|                 | IRIGCFG_INTERNAL Use internal IRIG bus   | x           | x   |
|                 | IRIGCFG_EXTERNAL Use external IRIG bus   | x           | x   |
| IRIGCFG_SLAVE   | IRIG timer for this core is a slave      | x           |     |
| IRIGCFG_MASTER  | IRIG timer for this core is the master   |             | x   |
| IRIGCFG_PPS     | Enables pulse per second signaling       | x           | x   |
| IRIGCFG_PWM     | Enables pulse width modulated signaling  | x           | x   |
| IRIGCFG_AM      | Enables amplitude modulated signaling    | 5G, 5G+, 6G | 6G  |

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices. Amplitude modulated decoding is only supported by 5G, 5G+, and 6G Devices. Amplitude modulated transmission is only supported by 6G Devices. When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format.

#### **WARNINGS**

Rounding is used when the IRIGCFG\_PPS option is enabled. Values get rounded up when above 500 ms and are rounded down when below 500 ms if signaling is configured for pulse per second (PPS).

#### **SEE ALSO**

BTICard\_IRIGRd,BTICard\_IRIGWr,BTICard\_IRIGInputThresholdSet, BTICard\_IRIGInputThresholdGet, BTICard\_TimerStatus

### **IRIGFieldGet??**

```
ULONG BTICard_IRIGFieldGetDays
ULONG BTICard_IRIGFieldGetHours
ULONG BTICard_IRIGFieldGetMin
ULONG BTICard_IRIGFieldGetSec
ULONG BTICard_IRIGFieldGetMillisec
ULONG BTICard_IRIGFieldGetMicrosec
(
      ULONG irigvalh, //Upper 32 bits of the 64-bit BCD IRIG time-tag
      ULONG irigvall, //Lower 32 bits of the 64-bit BCD IRIG time-tag
)
```
**RETURNS**

The integer value of the IRIG field for which the function is named.

#### **DESCRIPTION**

Extracts the specified BCD field from the 64-bit IRIG time-tag, converts it to an integer, and returns the integer.

An IRIG time-tag is divided into the following BCD fields:

| RSVD                     | DAYS |  | HOURS | MINUTES                                                                                         |  |  |
|--------------------------|------|--|-------|-------------------------------------------------------------------------------------------------|--|--|
|                          |      |  |       |                                                                                                 |  |  |
|                          |      |  |       | 64   63   61   60   59   58   53   52   51   50   49   48   47   40   3   38   37   36   5   33 |  |  |
|                          |      |  |       |                                                                                                 |  |  |
| iriovalh (unner 32 bits) |      |  |       |                                                                                                 |  |  |

| SECONDS                  | MILLISECONDS                                                                                                      |  | MICROSECONDS |  |  |  |  |  |  |  |
|--------------------------|-------------------------------------------------------------------------------------------------------------------|--|--------------|--|--|--|--|--|--|--|
|                          |                                                                                                                   |  |              |  |  |  |  |  |  |  |
|                          | 32   30   28   27   26   25   24   20   19   18   17   16   13   12   11   10   9   8   7 ` 6 ` 5   4 ' 3 ` 2 ` 1 |  |              |  |  |  |  |  |  |  |
|                          |                                                                                                                   |  |              |  |  |  |  |  |  |  |
| iriarall (lower 32 hits) |                                                                                                                   |  |              |  |  |  |  |  |  |  |

*Note: These are utility functions and do not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_IRIGFieldPut??

### **IRIGFieldPut??**

```
VOID BTICard_IRIGFieldPutDays
VOID BTICard_IRIGFieldPutHours
VOID BTICard_IRIGFieldPutMin
VOID BTICard_IRIGFieldPutSec
VOID BTICard_IRIGFieldPutMillisec
VOID BTICard_IRIGFieldPutMicrosec
(
      ULONG value, //Field value to write to the BCD IRIG time
      LPULONG irigvalh, //Pointer to a variable for the upper 32 bits
      LPULONG irigvall, //Pointer to a variable for the lower 32 bits
)
```
#### **RETURNS**

Nothing.

#### **DESCRIPTION**

Converts an integer (*value*) to BCD and inserts the BCD value into the specified field in the 64-bit IRIG time-tag.

|  | An IRIG time-tag is divided into the following BCD fields: |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|--|------------------------------------------------------------|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|--|
|  |                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|  |                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|  |                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |
|  |                                                            |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |  |

| SECONDS                    | MILLISECONDS | MICROSECONDS                                                                                                               |  |  |  |  |  |  |  |  |
|----------------------------|--------------|----------------------------------------------------------------------------------------------------------------------------|--|--|--|--|--|--|--|--|
|                            |              |                                                                                                                            |  |  |  |  |  |  |  |  |
|                            |              | 32   30   28   27   26   25   24   23   22   20   19   18   17   16   13   2   11   10   9   8   7 ` 6 ` 5   4 ` 3 ` 2 ` 1 |  |  |  |  |  |  |  |  |
| iniaren 17 (lowor 32 hite) |              |                                                                                                                            |  |  |  |  |  |  |  |  |

*Note: These are utility functions and do not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_IRIGFieldGet??

### **IRIGInputThresholdGet**

ERRVAL **BTICard\_IRIGInputThresholdGet**

```
(
     LPUSHORT dacval, //Pointer to a digital-analog converter value
     HCORE hCore //Core handle
```

```
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Gets the threshold of the IRIG input circuitry. The parameter *dacval* represents a linear voltage scale from 0x0000 to 0xFFF0 see product manual for voltage range. Only the most significant 12 bits are used.

#### **DEVICE DEPENDENCY**

Applies to all 5G, 5G+, and 6G Devices, except AB1xxx, NG1, NG2, and NG3 Devices.

#### **WARNINGS**

Should only be used when IRIG circuit is configured for Amplitude Modulated (AM) input signaling. Changing the input threshold affects both AM and Pulse Width Modulated decoding.

#### **SEE ALSO**

BTICard\_IRIGConfig, BTICard\_IRIGInputThresholdSet

### **IRIGInputThresholdSet**

ERRVAL **BTICard\_IRIGInputThresholdSet**

```
USHORT dacval, //Digital-analog converter value
HCORE hCore //Core handle
```

```
)
```
(

#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Sets the threshold of the IRIG input circuitry. The parameter *dacval* represents a linear voltage scale from 0x0000 to 0xFFF0 see product manual for voltage range. Only the most significant 12 bits are used.

#### **DEVICE DEPENDENCY**

Applies to all 5G, 5G+, and 6G Devices, except AB1xxx, NG1, NG2, and NG3 Devices.

#### **WARNINGS**

Should only be used when IRIG circuit is configured for Amplitude Modulated (AM) input signaling. Changing the input threshold affects both AM and Pulse Width Modulated decoding.

#### **SEE ALSO**

BTICard\_IRIGConfig, BTICard\_IRIGInputThresholdGet

### **IRIGRd**

(

)

ERRVAL **BTICard\_IRIGRd**

```
LPBTIIRIGTIME irigtime, //Pointer to an IRIG time structure
HCORE hCore //Core handle
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Reads the current value of the IRIG timer from the core specified by *hCore* and puts the value into the *irigtime* structure.

| BTIIRIGTIME structure |        |                                            |  |  |  |  |  |  |
|-----------------------|--------|--------------------------------------------|--|--|--|--|--|--|
| Field                 | Size   | Description                                |  |  |  |  |  |  |
| days                  | USHORT | Day of the year (see Device Dependency)    |  |  |  |  |  |  |
| hours                 | USHORT | Hours after midnight (0–23)                |  |  |  |  |  |  |
| min                   | USHORT | Minutes after the hour (0–59)              |  |  |  |  |  |  |
| sec                   | USHORT | Seconds after the minute (0–59)            |  |  |  |  |  |  |
| msec                  | USHORT | Milliseconds after the second (0–999)      |  |  |  |  |  |  |
| usec                  | USHORT | Microseconds after the millisecond (0–999) |  |  |  |  |  |  |

*Note: To read the binary timer, see BTICard\_TimerRd.*

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices.

For 4G Devices, the days count 0-365 with 0=January 1st .

For 5G and 5G+ Devices, the days count 0-364 with 0=January 1st .

For 6G Devices, the days count to a user programmable rollover.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_IRIGWr, BTICard\_IRIGConfig, BTICard\_TimerRd

### **IRIGSyncStatus**

BOOL **BTICard\_IRIGSyncStatus**

```
(
      HCORE hCore //Core handle
```
)

#### **RETURNS**

TRUE if the IRIG timer is synchronized or FALSE if it is not synchronized.

#### **DESCRIPTION**

Reports the status of the IRIG timer on *hCore* in synchronizing to the signal on the IRIG bus.

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_IRIGConfig, BTICard\_IRIGWr, BTICard\_IRIGRd

### **IRIGTimeBCDToBin**

```
VOID BTICard_IRIGTimeBCDToBin
(
      LPULONG timevalh, //Pointer to upper 32 bits of binary time µs value
      LPULONG timevall, //Pointer to lower 32 bits of binary time µs value
      ULONG irigvalh, //Upper 32 bits of BCD IRIG time value
      ULONG irigvall, //Lower 32 bits of BCD IRIG time value
```

```
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Converts the 64 bit IRIG BCD time value to the equivalent binary time value (in microseconds).

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTICard_IRIGTimeBinToBCD, BTICard_IRIGTimeNanoBinToBCD,
BTICard_IRIGTimeBCDToNanoBin
```
### **IRIGTimeBCDToNanoBin**

```
VOID BTICard_IRIGTimeBCDToNanoBin
(
      LPULONG timevalh, //Pointer to upper 32 bits of binary time ns value
      LPULONG timevall, //Pointer to lower 32 bits of binary time ns value
      ULONG irigvalh, //Upper 32 bits of BCD IRIG time value
      ULONG irigvall, //Lower 32 bits of BCD IRIG time value
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Converts the 64 bit IRIG BCD time value to the equivalent binary time value (in nanoseconds).

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTICard_IRIGTimeNanoBinToBCD, BTICard_IRIGTimeBinToBCD, 
BTICard_IRIGTimeBCDToBin
```
### **IRIGTimeBinToBCD**

#### VOID **BTICard\_IRIGTimeBinToBCD**

```
(
      LPULONG irigvalh, //Pointer to upper 32 bits of BCD IRIG time value
      LPULONG irigvall, //Pointer to lower 32 bits of BCD IRIG time value
      ULONG timevalh, //Upper 32 bits of binary time µs value
      ULONG timevall, //Lower 32 bits of binary time µs value
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Converts the binary time value (in microseconds) to the equivalent 64 bit IRIG BCD time value.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTICard_IRIGTimeBCDToBin, BTICard_IRIGTimeBCDToNanoBin,
BTICard_IRIGTimeNanoBinToBCD
```
### **IRIGTimeNanoBinToBCD**

```
VOID BTICard_IRIGTimeNanoBinToBCD
(
      LPULONG irigvalh, //Pointer to upper 32 bits of BCD IRIG time value
      LPULONG irigvall, //Pointer to lower 32 bits of BCD IRIG time value
      ULONG timevalh, //Upper 32 bits of binary time ns value
      ULONG timevall, //Lower 32 bits of binary time ns value
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Converts the binary time value (in nanoseconds) to the equivalent 64 bit IRIG BCD time value.

*Note: This is a utility function and does not access any Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTICard_IRIGTimeBCDToNanoBin, BTICard_IRIGTimeBCDToBin,
BTICard_IRIGTimeBinToBCD
```
### **IRIGWr**

ERRVAL **BTICard\_IRIGWr**

```
LPBTIIRIGTIME irigtime, //Pointer to an IRIG time array
HCORE hCore //Core handle
```
)

(

#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Sets the IRIG timer to *irigtime* on the core specified by *hCore*.

| IRIGTIME structure |        |                                         |  |  |  |  |  |  |
|--------------------|--------|-----------------------------------------|--|--|--|--|--|--|
| Field              | Size   | Description                             |  |  |  |  |  |  |
| days               | USHORT | Day of the year (see Device Dependency) |  |  |  |  |  |  |
| hours              | USHORT | Hours after midnight (0–23)             |  |  |  |  |  |  |
| min                | USHORT | Minutes after hour (0–59)               |  |  |  |  |  |  |
| sec                | USHORT | Seconds after minute (0–59)             |  |  |  |  |  |  |
| msec               | USHORT | Milliseconds after minute (0–999)       |  |  |  |  |  |  |
| usec               | USHORT | Microseconds after millisecond (0–999)  |  |  |  |  |  |  |

*Note: To write to the binary timer, see BTICard\_TimerWr.*

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices.

For 4G Devices, the days count 0-365 with 0=January 1st .

For 5G and 5G+ Devices, the days count 0-364 with 0=January 1st .

For 6G Devices, the days count to a user programmable rollover.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_IRIGRd, BTICard\_IRIGConfig, BTICard\_TimerWr

### **PXIStatus**

#### ERRVAL **BTICard\_PXIStatus**

**(**

)

```
LPULONG infoptr, //Pointer to variable to receive info
USHORT infotype, //Info status type
HCORE hCore //Core handle
```
**RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

The desired status is returned through *infoptr* by setting *infotype* to one of the following parameters:

| Status Selection |                                        |
|------------------|----------------------------------------|
| infotype         | infoptr                                |
| PXITYPE_GEOADDR  | Geographical Address                   |
| PXITYPE_CLKSEL   | PXIe Clock Selection                   |
|                  | 0 = PXIe_CLK100                        |
|                  | 1 = Onboard 100 MHz Oscillator         |
| PXITYPE_TRIGVERS | Version of the BTI PXIe Trigger Engine |
| PXITYPE_OUTEN    | Bitmask of PXI_TRIG drive status       |

#### **DEVICE DEPENDENCY**

Only applies to 6G Devices with PXIe functionality.

Please consult the Device hardware manual for more information.

#### **WARNINGS**

None.

**SEE ALSO**

None.

### **SeqBlkRd**

```
ULONG BTICard_SeqBlkRd
(
     LPUSHORT seqbuf, //Pointer to Sequential Record buffer
     HCORE hCore //Core handle
)
```

```
ULONG bufcount, //Size of the buffer (in 16-bit words)
LPULONG blockcount, //Number of records copied to the buffer
```
#### **RETURNS**

The number of 16-bit words copied to the user-supplied buffer.

#### **DESCRIPTION**

Copies as many available complete records as possible from the Sequential Record on the core to a buffer (*seqbuf*). The function returns the number of 16-bit words copied and the puts the number of records copied into *blockcount*. The larger the buffer size (*bufcount*) the greater the number of records that can be copied by a single call to this function. The data read is effectively removed from the Sequential Record on the core. This allows an infinite amount of data to be gathered as long as this function (or one of the others in the table below) is called frequently enough to prevent the Sequential Record on the core from overflowing.

There are four functions that read from the Sequential Record. BTICard\_SeqRd reads a single record; BTICard\_SeqBlkRd, BTICard\_SeqCommRd, and BTICard\_SeqDMARd read multiple records. Any one of these functions may be used in most applications. The difference lies in their speed of execution under different conditions and availability by Device. The table below compares the four functions that read from the Sequential Record and gives some rationale for selecting one over another:

| Function          | Function<br>Overhead | Per Record<br>Overhead | Use When…                                                                |  |
|-------------------|----------------------|------------------------|--------------------------------------------------------------------------|--|
| BTICard_SeqRd     | low                  | n/a                    | Expect one or no records per function call                               |  |
| BTICard_SeqBlkRd  | low                  | high                   | Expect a small number of records per function call                       |  |
| BTICard_SeqCommRd | high                 | low                    | Expect a large number of records per function call                       |  |
| BTICard_SeqDMARd  | low                  | low                    | Expect a large number of records per function call<br>(Device-dependent) |  |

#### **DEVICE DEPENDENCY**

On 3G Devices, BTICard\_SeqRd, \_SeqBlkRd, and \_SeqCommRd all read multiple records in the same manner.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqConfig,BTICard\_SeqCommRd, BTICard\_SeqDMARd,BTICard\_SeqBlkRd, BTICard\_SeqFindInit, BTICard\_SeqFindNext??

### **SeqCommRd**

```
USHORT BTICard_SeqCommRd
(
      LPUSHORT seqbuf, //Pointer to Sequential Record buffer
      USHORT bufcount, //Size of the buffer (in 16-bit words)
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The number of 16-bit words copied to the user-supplied buffer.

#### **DESCRIPTION**

Copies as many available complete records as possible from the Sequential Record on the core to a buffer (*seqbuf*) and returns the number of 16-bit words copied. The larger the buffer size (*bufcount*) the greater the number of records that can be copied by a single call to this function. The data read is effectively removed from the Sequential Record on the core. This allows an infinite amount of data to be gathered as long as this function (or one of the others in the table below) is called frequently enough to prevent the Sequential Record on the core from overflowing.

There are four functions that read from the Sequential Record. BTICard\_SeqRd reads a single record; BTICard\_SeqBlkRd, BTICard\_SeqCommRd, and BTICard\_SeqDMARd read multiple records. Any one of these functions may be used in most applications. The difference lies in their speed of execution under different conditions and availability by Device. The table below compares the four functions that read from the Sequential Record and gives some rationale for selecting one over another:

| Function          | Function | Per Record | Use When…                                             |  |
|-------------------|----------|------------|-------------------------------------------------------|--|
|                   | Overhead | Overhead   |                                                       |  |
| BTICard_SeqRd     | low      | n/a        | Expect one or no records per function call            |  |
| BTICard_SeqBlkRd  | low      | high       | Expect a small number of records per function call    |  |
| BTICard_SeqCommRd | high     | low        | Expect a large number of records per function call    |  |
| BTICard_SeqDMARd  | low      | low        | Need to offload application from reading monitor data |  |
|                   |          |            | (Device-dependent)                                    |  |

#### **DEVICE DEPENDENCY**

On 3G Devices, BTICard\_SeqRd, \_SeqBlkRd, and \_SeqCommRd all read multiple records in the same manner.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqBlkRd, BTICard\_SeqDMARd, BTICard\_SeqRd,BTICard\_SeqFindInit, BTICard\_SeqFindNext??

### **SeqConfig**

ERRVAL **BTICard\_SeqConfig**

(

ULONG *ctrlflags, //Selects configuration options* HCORE *hCore //Core handle*

#### ) **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Configures the Sequential Monitor of the core by allocating an onboard buffer and initializing internal pointers associated with the buffer.

| ctrlflags         |                                                                                   |  |
|-------------------|-----------------------------------------------------------------------------------|--|
| Constant          | Description                                                                       |  |
| SEQCFG_DEFAULT    | Select all default settings (bold below)                                          |  |
| SEQCFG_DISABLE    | Disable Sequential Record                                                         |  |
| SEQCFG_DMA        | Enables DMA mode (Device-dependent)                                               |  |
| SEQCFG_TCPNODELAY | Disable Nagle's algorithm on sequential DMAfor RPC devices (Device-dependent)     |  |
| SEQCFG_FILLHALT   | Enable Sequential Record in fill and halt mode                                    |  |
| SEQCFG_CONTINUOUS | Enable Sequential Record in continuous mode                                       |  |
| SEQCFG_DELTA      | Enable Sequential Record in delta mode (ARINC 429 only, 4G only)                  |  |
| SEQCFG_INTERVAL   | Enable Sequential Record in interval mode (ARINC 429 only, 4G only)               |  |
| SEQCFG_16K        | Allocate a 16 K Sequential Record buffer                                          |  |
| SEQCFG_32K        | Allocate a 32 K Sequential Record buffer                                          |  |
| SEQCFG_64K        | Allocate a 64 K Sequential Record buffer                                          |  |
| SEQCFG_128K       | Allocate a 128 K Sequential Record buffer                                         |  |
| SEQCFG_ALLAVAIL   | Allocate all available memory to the Sequential Record                            |  |
| SEQCFG_NOLOGFULL  | Do not generate an entry in the Event Log List when the Sequential Record is full |  |
| SEQCFG_LOGFULL    | Generate an entry in the Event Log List when the Sequential Record is full        |  |
| SEQCFG_NOLOGFREQ  | Do not generate entries in the Event Log List at user-defined frequency (see      |  |
|                   | BTICard_SeqLogFrequency)                                                          |  |
| SEQCFG_LOGFREQ    | Generate entries in the Event Log List at user-defined frequency (see             |  |
|                   | BTICard_SeqLogFrequency)                                                          |  |

*Note: It is highly recommended that the SEQCFG\_FILLHALT mode be used for the Sequential Record. This mode will allow for continuous recording of databus activity as long as the host keeps up with reading out record data. To allow the host flexibility in reading the Sequential Record, it is also recommended to use a value of SEQCFG\_128K for the size of the buffer.*

When using the SEQCFG\_CONTINOUS mode, databus activity will be continuously written to the Sequential Record without regard for the host reading data from the buffer. If the host attempts to read from it while the Device is running, the data returned could be corrupted. Therefore, when in this mode the Sequential Record should only be read while stopping and resuming the monitor using BTICard\_SeqStop and BTICard\_SeqResume.

#### **DEVICE DEPENDENCY**

5G, 5G+, and 6G Devices always have a 16MB Sequential Record buffer. 5G, 5G+, and 6G 429 Devices do not support SEQCFG\_INTERVAL and SEQCFG\_DELTA flags. If used, they will simply be ignored.

Sequential DMA is supported on all 4G, 5G, 5G+, and 6G Devices; however, sequential DMA is not supported on 4G Devices when operating over USB.

#### **WARNINGS**

If the SEQCFG\_ALLAVAIL flag is used, BTICard\_SeqConfig should be the last function called that allocates memory before BTICard\_CardStart is called.

#### **SEE ALSO**

BTICard\_SeqRd, BTICard\_SeqInterval, BTICard\_SeqLogFrequency

### **SeqDMARd**

```
USHORT BTICard_SeqDMARd
(
```

```
LPUSHORT seqbuf, //Pointer to Sequential Record buffer
     ULONG bufcount, //Size of the buffer (in 16-bit words)
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The number of 16-bit words copied to the user-supplied buffer.

#### **DESCRIPTION**

Copies as many available complete records as possible from the Sequential Record on the core to the buffer *seqbuf* and returns the number of 16-bit words copied. The larger the buffer size (*bufcount*) the greater the number of records that can be copied by a single call to this function. The Sequential Record data is read from Host memory since it was already transferred from the core memory to the Host in a DMA process. This allows an infinite amount of data to be gathered as long as this function (or one of the others in the table below) is called frequently enough to prevent the Sequential Record on the core from overflowing.

There are four functions that read from the Sequential Record. BTICard\_SeqRd reads a single record; BTICard\_SeqBlkRd, BTICard\_SeqCommRd, and BTICard\_SeqDMARd read multiple records. Any one of these functions may be used in most applications. The difference lies in their speed of execution under different conditions and availability by Device. The table below compares the four functions that read from the Sequential Record and gives some rationale for selecting one over another:

| Function          | Function | Per Record                                                   | Use When…                                          |  |
|-------------------|----------|--------------------------------------------------------------|----------------------------------------------------|--|
|                   | Overhead | Overhead                                                     |                                                    |  |
| BTICard_SeqRd     | low      | n/a<br>Expect one or no records per function call            |                                                    |  |
| BTICard_SeqBlkRd  | low      | High                                                         | Expect a small number of records per function call |  |
| BTICard_SeqCommRd | high     | Low<br>Expect a large number of records per function call    |                                                    |  |
| BTICard_SeqDMARd  | low      | Low<br>Need to offload application from reading monitor data |                                                    |  |
|                   |          |                                                              | (Device-dependent)                                 |  |

#### **DEVICE DEPENDENCY**

Sequential DMA is supported on all 4G, 5G, 5G+, and 6G Devices; however, sequential DMA is not supported on 4G Devices when operating over USB.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqBlkRd, BTICard\_SeqRd, BTICard\_SeqDMARd, BTICard\_SeqFindInit, BTICard\_SeqFindNext??

### **SeqFindCheckVersion**

```
BOOL BTICard_SeqFindCheckVersion
(
      LPUSHORT pRecord, //Pointer to a record
      USHORT version, //Version number to test
)
```
#### **RETURNS**

TRUE if record pointed to by *pRecord* is equal to or greater than the version number represented by *version*, otherwise FALSE.

#### **DESCRIPTION**

Checks to see if the version number of the record pointed to by *pRecord* is equal to or greater than the constant passed for *version*. Use this function to test the eligibility of a given record for a version-dependent application of a BTICard\_SeqFindMore?? function.

| version  |                             |  |  |
|----------|-----------------------------|--|--|
| Constant | Description                 |  |  |
| SEQVER_0 | Sequential Record Version 0 |  |  |
| SEQVER_1 | Sequential Record Version 1 |  |  |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

### **SEE ALSO**

BTICard\_SeqFindInit, BTICard\_SeqFindNext??

### **SeqFindInit**

```
ERRVAL BTICard_SeqFindInit
(
     LPUSHORT seqbuf, //Pointer to a Sequential Record buffer
     ULONG seqcount, //Number of 16-bit words in the buffer
     LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Initializes the structure (*sfinfo*) used by other BTICard\_SeqFindNext?? functions for finding records within a Sequential Record buffer. *seqbuf* is a pointer to the start of a buffer containing Sequential Record data and *seqcount* is the number of 16-bit words in the buffer.

*sfinfo* contains information that is used by the various BTICard\_SeqFindNext?? functions. Each time a BTICard\_SeqFindNext?? function is called, the *sfinfo* structure is updated to indicate where to resume the search with the next BTICard\_SeqFindNext?? function. Since these find functions pick up where they left off, based on *sfinfo*, it is necessary to call BTICard\_SeqFindInit whenever a find function is to start at the beginning of the buffer.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

This function causes the BTICard\_SeqFindNext?? functions to start their search at the beginning of the Sequential Record buffer.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindNext??

### **SeqFindMore1553**

```
ERRVAL BTICard_SeqFindMore1553
(
      LPSEQRECORDMORE1553 *pRecMore, //Address of pointer to a structure
      LPSEQRECORD1553 pRecBase //Pointer to a structure
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Finds the extra fields at the end of a MIL-STD-1553 record in the Sequential Record buffer pointed to by *pRecBase* and updates *\*pRecMore* to point to those fields. BTICard\_SeqFindNext1553 must be called before each call to BTICard\_SeqFindMore1553 to seed the *pRecBase* structure with the first portion of a 1553 message. Repeatedly calling

BTICard\_SeqFindNext1553 and BTICard\_SeqFindMore1553 returns the 1553 records in the Sequential Record one at a time until the end of the buffer is reached (at which time BTICard\_SeqFindNext1553 returns an error value).

Part of the time-tag and the measured RT response times are recorded in the extra fields, as shown in the table below.

| SEQRECORDMORE1553 structure |        |                                              |                          |  |
|-----------------------------|--------|----------------------------------------------|--------------------------|--|
| Field                       | Size   | Description                                  | Version (of base record) |  |
| timestamph                  | ULONG  | Upper 32 bits of the time-tag value          | 1 or greater             |  |
| resptime1                   | USHORT | First RT response time (in units of 0.1 µs)  | 1 or greater             |  |
| resptime2                   | USHORT | Second RT response time (in units of 0.1 µs) | 1 or greater             |  |

#### **DEVICE DEPENDENCY**

Applies only to 4G, 5G, 5G+, and 6G Devices, which add extra fields to 1553 records of version 1 or greater. The version number of the base record pointed to by *pRecBase* can be tested using BTICard\_SeqFindCheckVersion.

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

BTICard\_SeqFindNext1553 must be called before each call to BTICard\_SeqFindMore1553.

#### **SEE ALSO**

BTICard\_SeqFindInit, BTICard\_SeqFindNext1553

### **SeqFindNext**

```
ERRVAL BTICard_SeqFindNext
(
     LPUSHORT *pRecord, //Address of pointer
     LPUSHORT seqtype, //Pointer to variable to receive type value
     LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Finds the next record (*\*pRecord*) in the Sequential Record buffer (regardless of protocol). The protocol for that record is indicated by *seqtype* as shown below. The *sfinfo* structure is also updated.

| seqtype      |                                        |
|--------------|----------------------------------------|
| Constant     | Description                            |
| SEQTYPE_429  | Sequential Record type is ARINC 429    |
| SEQTYPE_717  | Sequential Record type is ARINC 717    |
| SEQTYPE_1553 | Sequential Record type is MIL-STD-1553 |
| SEQTYPE_708  | Sequential Record type is ARINC 708    |
| SEQTYPE_CSDB | Sequential Record type is CSDB         |
| SEQTYPE_DIO  | Sequential Record type is DIO          |
| SEQTYPE_USER | Sequential Record type is user-defined |
| SEQTYPE_EBR  | Sequential Record type is EBR-1553     |

Calling this function repeatedly steps through the records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. To handle the record data, cast the *\*pRecord* value to a structure pointer defined in the protocol-specific BTICard\_SeqFindNext?? functions.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

To make this function start its search at the beginning of the Sequential Record buffer, the *sfinfo* structure must first be initialized with BTICard\_SeqFindInit. Otherwise, it finds the next record from where it left off.

```
BTICard_SeqConfig, BTICard_SeqRd, BTICard_SeqFindInit, 
BTICard_SeqFindNext??
```
### **SeqFindNext1553**

```
ERRVAL BTICard_SeqFindNext1553
(
     LPSEQRECORD1553 *pRecord, //Address of pointer to a structure
     LPSEQFINDINFO sfinfo //Pointer to structure
)
```

```
RETURNS
```
A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Finds the next MIL-STD-1553 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data in the *sfinfo* structure. Calling this function repeatedly returns the 1553 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD1553 allows for easy handling of the data.

| SEQRECORD1553 structure |        |                                                            |              |
|-------------------------|--------|------------------------------------------------------------|--------------|
| Field                   | Size   | Description                                                | Version      |
| type                    | USHORT | The protocol and version number of this record             | All versions |
| count                   | USHORT | Size of the record, used to parse the data-stream          | All versions |
| timestamp               | ULONG  | Lower 32 bits of the time-tag value                        | All versions |
| activity                | USHORT | Activity value (see table below for details)               | All versions |
| error                   | USHORT | Error value (see table below for details)                  | All versions |
| cwd1                    | USHORT | Command word 1 value                                       | All versions |
| cwd2                    | USHORT | Command word 2 value                                       | All versions |
| swd1                    | USHORT | Status word 1value                                         | All versions |
| swd2                    | USHORT | Status word 2 value                                        | All versions |
| datacount               | USHORT | Number of MIL-STD-1553 data words                          | All versions |
| data[]                  | USHORT | Array of 1553 data words (don't exceed data[datacount -1]) | All versions |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity and error fields may be tested by AND-ing the values returned with the constants from the tables below:

| MIL-STD-1553 activity field |                                                                   |  |
|-----------------------------|-------------------------------------------------------------------|--|
| Constant                    | Description                                                       |  |
| MSGACT1553_CHMASK           | The channel number. Shift the result right by MSGACT1553_CHSHIFT. |  |
| MSGACT1553_CHSHIFT          | Channel number shift value. See CHMASK above.                     |  |
| MSGACT1553_XMTCWD1          | Command word 1 was transmitted.                                   |  |
| MSGACT1553_XMTCWD2          | Command word 2 was transmitted.                                   |  |
| MSGACT1553_XMTSWD1          | Status word 1 was transmitted.                                    |  |
| MSGACT1553_XMTSWD2          | Status word 2 was transmitted.                                    |  |
| MSGACT1553_RCVCWD1          | Command word 1 was received.                                      |  |
| MSGACT1553_RCVCWD2          | Command word 2 was received.                                      |  |
| MSGACT1553_RCVSWD1          | Status word 1 was received.                                       |  |
| MSGACT1553_RCVSWD2          | Status word 2 was received.                                       |  |
| MSGACT1553_XMTDWD           | Data word was transmitted.                                        |  |
| MSGACT1553_RCVDWD           | Data word was received.                                           |  |
| MSGACT1553_BUS              | Message was transmitted/received on bus A (0) or B (1).           |  |

| MIL-STD-1553 error field |                                                                                         |  |
|--------------------------|-----------------------------------------------------------------------------------------|--|
| Constant                 | Description                                                                             |  |
| MSGERR1553_NORESP        | No response was received from the RT                                                    |  |
| MSGERR1553_ANYERR        | Set if MSGERR1553_NORESP is false and any other error bits are set                      |  |
| MSGERR1553_PROTOCOL      | A protocol error occurred                                                               |  |
| MSGERR1553_SYNC          | Wrong polarity of the sync pulse                                                        |  |
| MSGERR1553_DATACOUNT     | Too many/too few data words                                                             |  |
| MSGERR1553_MANCH         | Manchester error                                                                        |  |
| MSGERR1553_PARITY        | Parity error                                                                            |  |
| MSGERR1553_WORD          | Word error                                                                              |  |
| MSGERR1553_RETRY         | All attempts to retry transmission of this message failed (BC only)                     |  |
| MSGERR1553_SYSTEM        | Internal system error occurred                                                          |  |
| MSGERR1553_HIT           | Indicates that this message was transmitted or received since this bit was last cleared |  |
|                          | (always set)                                                                            |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT1553\_CHMASK and right-shifting the result by MSGACT1553\_CHSHIFT. The resulting value is the channel number associated with the MIL-STD-1553 record.

channel = (activity & MSGACT1553\_CHMASK) >> MSGACT1553\_CHSHIFT;

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit, BTICard\_SeqFindMore1553

### **SeqFindNext429**

```
ERRVAL BTICard_SeqFindNext429
(
      LPSEQRECORD429 *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Finds the next ARINC 429 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the 429 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD429 allows for easy handling of the data.

| SEQRECORD429 structure |        |                                                                                 |              |
|------------------------|--------|---------------------------------------------------------------------------------|--------------|
| Field                  | Size   | Description                                                                     | Version      |
| type                   | USHORT | The protocol and version number of this record                                  | All versions |
| count                  | USHORT | Size of the record, used to parse the data-stream                               | All versions |
| timestamp              | ULONG  | Lower 32 bits of the time-tag value                                             | All versions |
| activity               | USHORT | Activity (see table below for details)                                          | All versions |
| decgap                 | USHORT | Gap preceding the 429 word in half bit-times (up to a maximum of 7.5 bit times) | 1 or greater |
| data                   | ULONG  | 32-bit ARINC 429 data word value                                                | All versions |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value                                             | 1 or greater |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The *decgap* field is a 4-bit value that measures the gap preceding the decoded word. If *decgap* indicates 7.5 bit times (F hexadecimal), then the gap is 7.5 bit times or greater.

The activity field may be tested by AND-ing the value returned with the constants from the table below:

| ARINC 429 activity field |                                                                                                                             |
|--------------------------|-----------------------------------------------------------------------------------------------------------------------------|
| Constant                 | Description                                                                                                                 |
| MSGACT429_CHMASK         | The channel number. Shift the result right by MSGACT429_CHSHIFT.                                                            |
| MSGACT429_CHSHIFT        | Channel number shift value. See CHMASK above.                                                                               |
| MSGACT429_SPD            | This bit reflects the speed detected. A one signifies high speed (100 Kbps), and a zero<br>signifies low speed (12.5 Kbps). |
| MSGACT429_ERR            | If set, it signifies that an error occurred in receiving this word. The type of error is defined by                         |
|                          | the following bits.                                                                                                         |
| MSGACT429_GAP            | Gap Error: A gap of less than four bit times preceded the word.                                                             |
| MSGACT429_PAR            | Parity error: A parity error was detected in the word.                                                                      |
| MSGACT429_LONG           | Long word error: A word of more than 32-bits was detected.                                                                  |
| MSGACT429_BIT            | Bit timing error: An error occurred while decoding the bits of the word (short bits or long bits).                          |
| MSGACT429_TO             | Time out error: The decoder timed out while receiving a word (short word).                                                  |
| MSGACT429_HIT            | Signifies that the message has been processed by the firmware (the Hit bit).                                                |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT429\_CHMASK and right-shifting the result by MSGACT429\_CHSHIFT. The resulting value is the channel number associated with the ARINC 429 record.

channel = (activity & MSGACT429\_CHMASK) >> MSGACT429\_CHSHIFT;

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit

### **SeqFindNext708**

```
ERRVAL BTICard_SeqFindNext708
(
      LPSEQRECORD708 *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Finds the next ARINC 708 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the 708 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD708 allows for easy handling of the data.

| SEQRECORD708 structure |        |                                                                  |              |
|------------------------|--------|------------------------------------------------------------------|--------------|
| Field                  | Size   | Description                                                      | Version      |
| type                   | USHORT | The protocol and version number of this record                   | All versions |
| count                  | USHORT | Size of the record, used to parse the data-stream                | All versions |
| timestamp              | ULONG  | Lower 32 bits of the time-tag value                              | All versions |
| activity               | USHORT | Activity (see table below for details)                           | All versions |
| datacount              | USHORT | Number of data words                                             | All versions |
| data[100]              | USHORT | ARINC 708 data word values (100 16-bit data words)               | All versions |
| extra[16]              | USHORT | Additional data (if variable bit mode is enabled)                | 1 or greater |
| bitcount               | USHORT | Number of bits in this message (if variable bit mode is enabled) | 1 or greater |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value                              | 1 or greater |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity field may be tested by AND-ing the value returned with the constants from the table below:

| ARINC 708 activity field |                                                                                          |  |
|--------------------------|------------------------------------------------------------------------------------------|--|
| Constant                 | Description                                                                              |  |
| MSGACT708_CHMASK         | The channel number. Shift the result right by MSGACT708_CHSHIFT.                         |  |
| MSGACT708_CHSHIFT        | Channel number shift value. See CHMASK above.                                            |  |
| MSGACT708_ERR            | This bit is set if any of the error bits are set.                                        |  |
| MSGACT708_DSYNC          | No data sync at end of word.                                                             |  |
| MSGACT708_MANCH          | Manchester error.                                                                        |  |
| MSGACT708_WORD           | Word error.                                                                              |  |
| MSGACT708_LONG           | Long word error: A word of more than 1600 bits was detected.                             |  |
| MSGACT708_SHORT          | Short word error: A word of less than 1600 bits was detected.                            |  |
| MSGACT708_TO             | Time out error: The decoder timed out while receiving a word.                            |  |
| MSGACT708_HIT            | Indicates that this message was transmitted or received since this bit was last cleared. |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT708\_CHMASK and right-shifting the result by MSGACT708\_CHSHIFT. The resulting value is the channel number associated with the ARINC 708 record.

channel = (activity & MSGACT708\_CHMASK) >> MSGACT708\_CHSHIFT;

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit

### **SeqFindNext717**

```
ERRVAL BTICard_SeqFindNext717
(
      LPSEQRECORD717 *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Finds the next ARINC 717 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the 717 records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORD717 allows for easy handling of the data.

| SEQRECORD717 structure |        |                                                   |              |
|------------------------|--------|---------------------------------------------------|--------------|
| Field                  | Size   | Description                                       | Version      |
| type                   | USHORT | The protocol and version number of this record    | All versions |
| count                  | USHORT | Size of the record, used to parse the data-stream | All versions |
| timestamp              | ULONG  | Lower 32-bits of the time-tag value               | All versions |
| activity               | USHORT | Activity (see table below for details)            | All versions |
| wordnum                | USHORT | Number of words                                   | All versions |
| subframe               | USHORT | Number of subframes                               | All versions |
| superframe             | USHORT | Number of superframes                             | All versions |
| data                   | USHORT | 12-bit ARINC 717 data word value in LSBs          | All versions |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value               | 1 or greater |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity field may be tested by AND-ing the value returned with the constants from the table below:

| ARINC 717 activity field |                                                                                          |  |
|--------------------------|------------------------------------------------------------------------------------------|--|
| Constant                 | Description                                                                              |  |
| MSGACT717_CHMASK         | The channel number. Shift the result right by MSGACT717_CHSHIFT.                         |  |
| MSGACT717_CHSHIFT        | Channel number shift value. See CHMASK above.                                            |  |
| MSGACT717_SPDMASK        | The current speed mask value.                                                            |  |
| MSGACT717_64WPS          | The current speed is 64 wps (words per second).                                          |  |
| MSGACT717_128WPS         | The current speed is 128 wps.                                                            |  |
| MSGACT717_256WPS         | The current speed is 256 wps.                                                            |  |
| MSGACT717_512WPS         | The current speed is 512 wps.                                                            |  |
| MSGACT717_1024WPS        | The current speed is 1024 wps.                                                           |  |
| MSGACT717_2048WPS        | The current speed is 2048 wps.                                                           |  |
| MSGACT717_4096WPS        | The current speed is 4096 wps.                                                           |  |
| MSGACT717_8192WPS        | The current speed is 8192 wps.                                                           |  |
| MSGACT717_TO             | Time out error: The decoder timed out while receiving a (short) word.                    |  |
| MSGACT717_HIT            | Indicates that this message was transmitted or received since this bit was last cleared. |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACT717\_CHMASK and right-shifting the result by MSGACT717\_CHSHIFT. The resulting value is the channel number associated with the ARINC 717 record.

channel = (activity & MSGACT717\_CHMASK) >> MSGACT717\_CHSHIFT;

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit

### **SeqFindNextDIO**

```
ERRVAL BTICard_SeqFindNextDIO
(
      LPSEQRECORDDIO *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Finds the next DIO record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data from the *sfinfo* structure. Calling this function repeatedly returns the DIO records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORDDIO allows for easy handling of the data.

| SEQRECORDDIO structure |        |                                                |              |
|------------------------|--------|------------------------------------------------|--------------|
| Field                  | Size   | Description                                    | Version      |
| type                   | USHORT | The protocol and version number of this record | All versions |
| count                  | USHORT | The length of this record                      | All versions |
| bank                   | USHORT | Number of the bank                             | All versions |
| state                  | USHORT | State of the bank                              | 0 only       |
| timestamp              | ULONG  | Lower 32 bits of the time-tag value            | All versions |
| timestamph             | ULONG  | Upper 32 bits of the time-tag value            | All versions |
| change                 | USHORT | Bitmask of discrete inputs that changed value  | 1 or greater |
| value                  | USHORT | Current value of discrete inputs               | 1 or greater |

#### **DEVICE DEPENDENCY**

Applies to all 5G, 5G+, and 6G Devices. The mapping of *dionum* to physical discrete I/O is hardware dependent. Please consult the hardware manual for the Device. Also applies to 4G Devices with one or more discrete I/O modules (832 module). Please consult the OmniBus Discrete IO User's Manual for usage with 4G Devices.

When IRIG is enabled on a 4G Device, time-tags in Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

```
BTICard_ExtDIOMonConfig,BTICard_SeqConfig, BTICard_SeqRd, 
BTICard_SeqFindInit
```
### **SeqFindNextEBR**

```
ERRVAL BTICard_SeqFindNextEBR
(
      LPSEQRECORDEBR *pRecord, //Address of pointer to a structure
      LPSEQFINDINFO sfinfo //Pointer to structure
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Finds the next EBR-1553 record in the Sequential Record buffer and updates *\*pRecord* to point to that record. This function uses and updates data in the *sfinfo* structure. Calling this function repeatedly returns the EBR records one at a time until the end of the buffer is reached (at which time it returns an error value). Thus, messages can be individually saved to disk, displayed on screen, etc. Using the predefined Sequential Record structure SEQRECORDEBR allows for easy handling of the data.

| SEQRECORDEBR structure |        |                                                          |              |
|------------------------|--------|----------------------------------------------------------|--------------|
| Field                  | Size   | Description                                              | Version      |
| type                   | UINT16 | The protocol and version number of this record           | All versions |
| count                  | UINT16 | Word count in record                                     | All versions |
| activity               | UINT16 | Activity value (see table below for details)             | All versions |
| error                  | UINT16 | Error value (see table below for details)                | All versions |
| timestamp              | UINT32 | Lower 32 bits of the time-tag value                      | All versions |
| timestamph             | UINT32 | Upper 32 bits of the time-tag value                      | All versions |
| cwd                    | UINT16 | Command word value                                       | All versions |
| cwdinfo                | UINT16 | RT Link the command word was sent on                     | All versions |
| swd                    | UINT16 | Status wordvalue                                         | All versions |
| swdinfo                | UINT16 | RT Link the status word was sent on                      | All versions |
| resptime               | UINT16 | RT response time (in 100ths of µs)                       | All versions |
| datacount              | UINT16 | Number EBR-1553 data words                               | All versions |
| data[]                 | UINT16 | Array of EBR data words (don't exceed data[datacount-1]) | All versions |

The version number can be tested using BTICard\_SeqFindCheckVersion.

The activity and error fields may be tested by AND-ing the values returned with the constants from the tables below:

| EBR-1553 activity field |                                                                               |  |  |
|-------------------------|-------------------------------------------------------------------------------|--|--|
| Constant                | Description                                                                   |  |  |
| MSGACTEBR_CHMASK        | The channel number mask value. Shift the result right with MSGACTEBR_CHSHIFT. |  |  |
| MSGACTEBR_CHSHIFT       | Channel number shift value. See CHMASK above.                                 |  |  |
| MSGACTEBR_LINKMASK      | The RT Link number. Shift the result right by MSGACTEBR_LINKSHIFT.            |  |  |
| MSGACTEBR_LINKSHIFT     | Link number shift value. See LINKMASK above.                                  |  |  |
| MSGACTEBR_XMTCWD        | The command word was transmitted.                                             |  |  |
| MSGACTEBR_XMTSWD        | The status word was transmitted.                                              |  |  |
| MSGACTEBR_RCVCWD        | The command word was received.                                                |  |  |
| MSGACTEBR_RCVSWD        | The status word was received.                                                 |  |  |
| MSGACTEBR_XMTDWD        | Data words were transmitted.                                                  |  |  |
| MSGACTEBR_RCVDWD        | Data words were received.                                                     |  |  |
| MSGACTEBR_MODEMASK      | LHub Mode mask value                                                          |  |  |
| MSGACTEBR_MODESHIFT     | LHub Mode shift value                                                         |  |  |

| EBR-1553 error field |                                                                                                         |  |  |
|----------------------|---------------------------------------------------------------------------------------------------------|--|--|
| Constant             | Description                                                                                             |  |  |
| MSGERREBR_NORESP     | No response was received from the RT                                                                    |  |  |
| MSGERREBR_ANYERR     | Set if any other error bits are set                                                                     |  |  |
| MSGERREBR_PROTOCOL   | A protocol error occurred                                                                               |  |  |
| MSGERREBR_SYNC       | Wrong polarity of the sync pulse                                                                        |  |  |
| MSGERREBR_DATACOUNT  | Too many/too few data words                                                                             |  |  |
| MSGERREBR_MANCH      | Manchester error                                                                                        |  |  |
| MSGERREBR_PARITY     | Parity error                                                                                            |  |  |
| MSGERREBR_WORD       | Word error                                                                                              |  |  |
| MSGERREBR_RETRY      | All attempts to retry transmission of this message failed (BC only)                                     |  |  |
| MSGERREBR_SYSTEM     | Internal system error occurred                                                                          |  |  |
| MSGERREBR_LHUB       | Indicates unexpected LHUB activity was detected                                                         |  |  |
| MSGERREBR_HIT        | Indicates that this message was transmitted or received since this bit was last cleared<br>(always set) |  |  |

Extract the channel number from the activity word by AND-ing the activity field with MSGACTEBR\_CHMASK and right-shifting the result by MSGACTEBR\_CHSHIFT. The resulting value is the channel number associated with the EBR-1553 record.

channel = (activity & MSGACTEBR\_CHMASK) >> MSGACTEBR\_CHSHIFT;

| Cwdinfo field           |                                                                               |  |
|-------------------------|-------------------------------------------------------------------------------|--|
| Constant<br>Description |                                                                               |  |
| CWDINFOEBR_RESVMASK     | Reserved for future use. Do not use.                                          |  |
| CWDINFOEBR_LINKMASK     | The link number mask value. Shift the result right with CWDINFOEBR_LINKSHIFT. |  |
| CWDINFOEBR_LINKSHIFT    | Link number shift value. See LINKMASK above.                                  |  |

| Swdinfo field        |                                                                               |  |  |
|----------------------|-------------------------------------------------------------------------------|--|--|
| Constant             | Description                                                                   |  |  |
| SWDINFOEBR_RESVMASK  | Reserved for future use. Do not use.                                          |  |  |
| SWDINFOEBR_LINKMASK  | The link number mask value. Shift the result right with SWDINFOEBR_LINKSHIFT. |  |  |
| SWDINFOEBR_LINKSHIFT | Link number shift value. See LINKMASK above.                                  |  |  |

The RT Link number that the command was sent on can be extracted from cwdinfo field through the same process.

cwdlink = (cwdinfo & CWDINFOEBR\_LINKMASK) >> CWDINFOEBR\_LINKSHIFT;

This is also done to determine the RT Link number the response was seen on using the swdinfo field.

swdlink = (swdinfo & SWDINFOEBR\_LINKMASK) >> SWDINFOEBR\_LINKSHIFT;

The configured Logical Hub mode can also be extracted from the activity word. The MODE bits must be masked and shifted by doing the following

mode = (activity & MSGACTEBR\_MODEMASK) >> MSGACTEBR\_MODESHIFT;

Then the mode can be directly compared against the following values

| LHub Mode       |                              |  |
|-----------------|------------------------------|--|
| Constant        | Description                  |  |
| LHUBMODE_SPEC   | The LHub is in "spec" mode   |  |
| LHUBMODE_SWITCH | The LHub is in "switch" mode |  |
| LHUBMODE_LINK   | The LHub is in "link" mode   |  |

#### Comparison is as follows

if (mode == LHUBMODE\_SPEC)

#### **DEVICE DEPENDENCY**

When IRIG is enabled on a 4G Device, time-tags in message structures and Sequential Records will be in BCD format (see BTICard\_TimerStatus).

#### **WARNINGS**

Must be preceded by a call to BTICard\_SeqFindInit.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd, BTICard\_SeqFindInit

### **SeqInterval**

```
INT BTICard_SeqInterval
(
     INT interval, //Interval time (in seconds)
     INT mode, //Mode to determine interval value
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The actual interval value that the core will use.

#### **DESCRIPTION**

Sets the interval time for the Sequential Monitor, and is used when the Sequential Record has been configured with the SEQCFG\_INTERVAL flag. In Interval mode, the Sequential Monitor records only the first occurrence of selected messages within the specified interval. The availability of the Interval mode is both Device- and protocol-dependent.

The core cannot accommodate all interval values that could be passed through *interval*. The specified mode helps determine the actual interval that will be used. The constants below should be used to set the mode:

| mode                 |                                                    |  |  |  |
|----------------------|----------------------------------------------------|--|--|--|
| Constant             | Description                                        |  |  |  |
| INTERVALMODE_CLOSEST | Uses the value closest to the specified interval   |  |  |  |
| INTERVALMODE_LESS    | Uses the value just less than specified interval   |  |  |  |
| INTERVALMODE_GREATER | Uses the value just greater the specified interval |  |  |  |

#### **DEVICE DEPENDENCY**

Applies only to 3G and 4G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd

### **SeqIsRunning**

```
BOOL BTICard_SeqIsRunning
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the Sequential Record is still active, otherwise FALSE.

#### **DESCRIPTION**

Determines whether the Sequential Record is active and is typically used when the Sequential Record has been configured with the SEQCFG\_FILLHALT flag. In which case, recording halts when the on-board Sequential Record is full. This prevents unread data from being overwritten when the host gets behind in reading data from the Sequential Record. Thus, in fill and halt mode BTICard\_SeqIsRunning effectively indicates whether the buffer is full or not.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqRd

### **SeqLogFrequency**

```
USHORT BTICard_SeqLogFrequency
(
```

```
USHORT logfreq, //Frequency of Event Log List entries
HCORE hCore //Core handle
```

```
)
```
#### **RETURNS**

The previous value of the frequency of Event Log List entries.

#### **DESCRIPTION**

Sets the Event Log List frequency for the Sequential Monitor. It is used when the Sequential Record has been configured with the SEQCFG\_LOGFREQ flag in BTICard\_SeqConfig. The Sequential Record generates an Event Log List entry after it records *logfreq* amount of records. The user specifies the value of *logfreq*. For example, a value of 1 enables an Event Log List entry after every record, a value of 2 after every second record, and so on. It continues in this manner until the Sequential Record is stopped.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTICard_SeqConfig, BTICard_SeqRd, BTICard_EventLogConfig, 
BTICard_EventLogRd
```
### **SeqRd**

```
USHORT BTICard_SeqRd
(
      LPUSHORT seqbuf, //Pointer to Sequential Record buffer
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The number of 16-bit words copied to the user-supplied buffer.

#### **DESCRIPTION**

Copies up to a single record at a time from the Sequential Record on the core to a buffer (*seqbuf*). The function returns the number of 16-bit words copied. The data read is effectively removed from the Sequential Record on the core. This allows an infinite amount of data to be gathered as long as this function (or one of the others in the table below) is called frequently enough to prevent the Sequential Record on the core from overflowing.

There are four functions that read from the Sequential Record. BTICard\_SeqRd reads a single record; BTICard\_SeqBlkRd, BTICard\_SeqCommRd, and BTICard\_SeqDMARd read multiple records. Any one of these functions may be used in most applications. The difference lies in their speed of execution under different conditions and availability by Device. The table below compares the four functions that read from the Sequential Record and gives some rationale for selecting one over another:

| Function          | Function<br>Overhead | Per Record<br>Overhead | Use When…                                                                   |
|-------------------|----------------------|------------------------|-----------------------------------------------------------------------------|
| BTICard_SeqRd     | low                  | n/a                    | Expect one or no records per function call                                  |
| BTICard_SeqBlkRd  | low                  | High                   | Expect a small number of records per function call                          |
| BTICard_SeqCommRd | high                 | Low                    | Expect a large number of records per function call                          |
| BTICard_SeqDMARd  | low                  | Low                    | Need to offload application from reading monitor data<br>(Device-dependent) |

#### **DEVICE DEPENDENCY**

On 3G Devices, BTICard\_SeqRd, BTICard\_SeqBlkRd, and BTICard\_SeqCommRd all read multiple records in the same manner.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqConfig, BTICard\_SeqBlkRd, BTICard\_SeqCommRd, BTICard\_SeqFindInit, BTICard\_SeqFindNext??

### **SeqResume**

```
BOOL BTICard_SeqResume
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the Sequential Record was previously running, otherwise FALSE.

#### **DESCRIPTION**

Resumes operation of the Sequential Record at the point at which it was stopped using BTICard\_SeqStop. Use this function to continue recording data to the Sequential Record without overwriting previous records.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

A call to BTICard\_SeqStop must precede this function.

#### **SEE ALSO**

BTICard\_SeqStart, BTICard\_SeqStop

### **SeqStart**

```
BOOL BTICard_SeqStart
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the Sequential Record was previously running, otherwise FALSE.

#### **DESCRIPTION**

Starts recording of the Sequential Record. If necessary, it also stops and clears the Sequential Record before restarting it.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

If this function is called after BTICard\_SeqStop, recording starts at the beginning of the buffer and previous data is overwritten. To add to previous data without erasing it, use BTICard\_SeqResume instead.

#### **SEE ALSO**

BTICard\_SeqStop, BTICard\_SeqResume

### **SeqStatus**

```
BOOL BTICard_SeqStatus
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The status value of the Sequential Record.

#### **DESCRIPTION**

Checks the status of the Sequential Record. The status value can be tested using the predefined constants below:

| Constant     | Description                           |
|--------------|---------------------------------------|
| STAT_EMPTY   | Sequential Record is empty            |
| STAT_PARTIAL | Sequential Record is partially filled |
| STAT_FULL    | Sequential Record is full             |
| STAT_OFF     | Sequential Record is off              |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

The operation of the SeqStatus is configuration and Device dependent. All 4G Devices and 5G, 5G+, 6G RPC Devices configured for DMA can return STAT\_EMPTY when the internal sequential buffer is empty but the DMA buffer contains data.

```
BTICard_SeqStart, BTICard_SeqStop, BTICard_SeqResume
```
## **SeqStop**

```
BOOL BTICard_SeqStop
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

TRUE if the Sequential Record was previously running, otherwise FALSE.

#### **DESCRIPTION**

Suspends the recording of data to the Sequential Record before the buffer is filled. If BTICard\_SeqResume is subsequently called, recording is resumed at the point at which it was stopped without overwriting previous records. If BTICard\_SeqStart is called after BTICard\_SeqStop, recording starts at the beginning of the buffer and previous data is overwritten.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

**WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SeqStart, BTICard\_SeqResume

### **SysMonClear**

ERRVAL **BTICard\_SysMonClear**

```
(
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Resets the historic maximum and minimum values for all sensors on the card specified by *hCard*.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SysMonTypeGet

### **SysMonDescGet**

```
LPCSTR BTICard_SysMonDescGet
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A pointer to a character string describing the sensor or NULL if the sensor is not present.

#### **DESCRIPTION**

Provides a formatted string that describes the sensor specified by *index*.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SysMonTypeGet

### **SysMonMaxRd**

```
INT BTICard_SysMonMaxRd
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The historic maximum value of a sensor or SYSMONRD\_NOTVALID if the sensor is not present or the historic maximum value is not valid.

#### **DESCRIPTION**

Reads the historic maximum value from the sensor specified by *index*. The value is in units of mV, mA, or m°C depending on the sensor type. Call BTICard\_SysMonUserStr to convert the value to a formatted string.

To reset the historic maximum value, call BTICard\_SysMonClear.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function will return SYSMONRD\_NOTVALID for up to 800 ms after the first call to BTICard\_CardOpen and up to 150 ms after any call to BTICard\_SysMonClear to allow SysMon to get valid data.

*Note: Due to occasionally spurious values that may be reported by the voltage/current sensor, a second read of the system monitor to confirm an error is recommended prior to software acting on the assumed error data. See BTICard\_SysMonValRd for additional information.*

```
BTICard_SysMonClear, BTICard_SysMonMinRd, BTICard_SysMonUserStr, 
BTICard_SysMonValRd
```
### **SysMonMinRd**

```
INT BTICard_SysMonMinRd
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The historic minimum value of a sensor or SYSMONRD\_NOTVALID if the sensor is not present or the historic minimum value is not valid.

#### **DESCRIPTION**

Reads the historic minimum value from the sensor specified by *index*. The value is in units of mV, mA, or m°C depending on the sensor type. Call BTICard\_SysMonUserStr to convert the value to a formatted string.

To reset the historic minimum value, call BTICard\_SysMonClear.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function will return SYSMONRD\_NOTVALID for up to 800 ms after the first call to BTICard\_CardOpen and up to 150 ms after any call to BTICard\_SysMonClear to allow SysMon to get valid data.

*Note: Due to occasionally spurious values that may be reported by the voltage/current sensor, a second read of the system monitor to confirm an error is recommended prior to software acting on the assumed error data. See BTICard\_SysMonValRd for additional information.*

```
BTICard_SysMonClear, BTICard_SysMonMaxRd, BTICard_SysMonUserStr, 
BTICard_SysMonValRd
```
### **SysMonNomRd**

```
INT BTICard_SysMonNomRd
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The nominal voltage for a voltage sensor or SYSMONRD\_NOTVALID if the sensor is not present.

#### **DESCRIPTION**

Reads the nominal voltage from the voltage sensor specified by *index*. The value is in units of mV. Call BTICard\_SysMonUserStr to convert the value to a formatted string.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

Does not apply to temperature and current sensors.

#### **SEE ALSO**

BTICard\_SysMonUserStr, BTICard\_SysMonValRd

### **SysMonThresholdGet**

```
ERRVAL BTICard_SysMonThresholdGet
(
      BOOL *enable,//Pointer to variable to receive enable value
      LPINT minval,//Pointer to variable to receive minimum threshold value
      LPINT maxval,//Pointer to variable to receive maximum threshold value
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs or zero if successful.

#### **DESCRIPTION**

Reads the user definable thresholds, in units of m°C, for the temperature sensor specified by *index*. These user thresholds are disabled and set to System Limits at power on and can be modified by calling BTICard\_SysMonThresholdSet. The enable value and thresholds are passed through *enable*, *minval*, and *maxval* respectively.

Call BTICard\_SysMonUserStr to convert *minval* or *maxval* to a formatted string.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

Due to rounding, *minval* and *maxval* may not match what was set in BTICard\_SysMonThresholdSet. The values may be rounded to the nearest resolution.

Does not apply to voltage and current sensors.

#### **SEE ALSO**

BTICard\_BITStatusRd, BTICard\_SysMonThresholdSet

### **SysMonThresholdSet**

```
ERRVAL BTICard_SysMonThresholdSet
(
      BOOL enable, //Enable for the Sensor
      INT minval, //Minimum threshold value
      INT maxval, //Maximum threshold value
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Sets the user definable thresholds, in units of m°C, for the temperature sensor specified by *index*. The enable value and thresholds are passed through *enable*, *minval*, and *maxval* respectively. Once enabled, if the sensor value exceeds the the user definable thresholds, the BIT Status register will indicate a fault which can be read by calling BTICard\_BITStatusRd.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

Due to rounding, *minval* and *maxval* may not match what was set in BTICard\_SysMonThresholdSet. The values may be rounded to the nearest resolution.

Does not apply to voltage and current sensors.

minval and maxval should not exceed the system limits. Values exceeding the limits will be clamped to the system minimum and maximum accordingly. System limits vary by product (See product manual for the system limits).

#### **SEE ALSO**

BTICard\_BITStatusRd, BTICard\_SysMonThresholdSet

### **SysMonTypeGet**

```
ULONG BTICard_SysMonTypeGet
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The type of sensor or SYSMONTYPE\_NONE if the sensor is not present.

#### **DESCRIPTION**

Reports the type of the sensor specified by *index by returning one of the* predefined constants below:

| Constant           | Description           |
|--------------------|-----------------------|
| SYSMONTYPE_NONE    | Sensor is not present |
| SYSMONTYPE_TEMP    | Temperature Sensor    |
| SYSMONTYPE_VOLTAGE | Voltage Sensor        |
| SYSMONTYPE_CURRENT | Current Sensor        |

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SysMonDescGet

### **SysMonUserStr**

```
LPCSTR BTICard_SysMonUserStr
(
      INT value, //Value to be formatted
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

A pointer to a character string containing the value and units for a sensor or NULL if sensor is not present.

#### **DESCRIPTION**

Returns a formatted character string containing the value and units for the sensor specified by *index*. The parameter *value* is typically read by calling BTICard\_SysMonValRd, BTICard\_SysMonNomRd, BTICard\_SysMonMinRd, or BTICard\_SysMonMaxRd.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_SysMonValRd, BTICard\_SysMonNomRd, BTICard\_SysMonMinRd, BTICard\_SysMonMaxRd

### **SysMonValRd**

```
INT BTICard_SysMonValRd
(
      INT index, //Specifies the sensor index
      HCARD hCard //Card handle
)
```
#### **RETURNS**

The current value for a sensor or SYSMONRD\_NOTVALID if the sensor is not present.

#### **DESCRIPTION**

Reads the current value of the sensor specified by *index*. The units for the current value are in mV, mA, or m°C depending on the sensor type. Call BTICard\_SysMonUserStr to convert the value to a formatted string.

#### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

This function will return SYSMONRD\_NOTVALID for up to 800 ms after the first call to BTICard\_CardOpen and up to 150 ms after any call to BTICard\_SysMonClear to allow SysMon to get valid data.

*Note: Due to occasionally spurious values that may be reported by the voltage/current sensor, a second read of the system monitor to confirm an error is recommended prior to software acting on the assumed error data. A minimum wait time of 150 ms is required before issuing a second read of the system monitor to ensure that a new value has been sampled. Call BTICard\_SysMonClear to clear min/max values if a spurious voltage or current values is read.*

#### **SEE ALSO**

BTICard\_SysMonUserStr, BTICard\_SysMonMaxRd, BTICard\_SysMonMinRd

### **Timer64Rd**

```
ERRVAL BTICard_Timer64Rd
```

```
(
     LPULONG valueh, //Pointer to upper 32 bits of the timer value
     LPULONG valuel, //Pointer to lower 32 bits of the timer value
     HCORE hCore //Core handle
```
)

#### **RETURNS**

A negative value if an error occurs, or zero if successful.

#### **DESCRIPTION**

Reads the current value of the binary timer from the specified Device.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

6G Devices have a 64-bit binary timer with a 1 ns resolution.

5G and 5G+ Devices have a 48-bit binary timer with a 1 s resolution.

3G and 4G Devices have a 32-bit binary timer with an adjustable resolution. To use the IRIG timer for a specified 4G Device instead of the default binary timer, see BTICard\_IRIGConfig.

Please see BTICard\_TimerRd for a discussion of Device-dependent timer differences.

#### **WARNINGS**

None.

```
BTICard_TimerClear, BTICard_TimerRd, BTICard_TimerWr, BTICard_Timer64Wr, 
BTICard_IRIGConfig, BTICard_IRIGRd, BTICard_IRIGWr
```
### **Timer64Wr**

```
VOID BTICard_Timer64Wr
(
```

```
HCORE hCore //Core handle
```
ULONG *valueh, //Upper 32 bits of the timer value* ULONG *valuel, //Lower 32 bits of the timer value*

#### **RETURNS**

)

None.

#### **DESCRIPTION**

Writes the timer value of the binary timer for the specified Device.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

6G Devices have a 64-bit binary timer with a 1 ns resolution.

5G and 5G+ Devices have a 48-bit binary timer with a 1 s resolution.

3G and 4G Devices have a 32-bit binary timer with an adjustable resolution. To use the IRIG timer for a specified 4G Device instead of the default binary timer, see BTICard\_IRIGConfig.

Please see BTICard\_TimerWr for a discussion of Device-dependent timer differences.

#### **WARNINGS**

None.

```
BTICard_TimerClear, BTICard_TimerRd, BTICard_TimerWr, BTICard_Timer64Rd, 
BTICard_IRIGConfig, BTICard_IRIGRd, BTICard_IRIGWr
```
### **TimerClear**

```
VOID BTICard_TimerClear
```

```
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Clears the Device timer to zero.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

```
BTICard_TimerRd, BTICard_TimerResolution, BTICard_IRIGConfig, 
BTICard_IRIGRd, BTICard_IRIGWr
```
### **TimerRd**

```
ULONG BTICard_TimerRd
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The current Device timer value.

#### **DESCRIPTION**

Reads the lower 32-bits of the current value of the binary timer from the specified Device.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

6G Devices have a 64-bit binary timer with a 1 ns resolution.

5G and 5G+ Devices have a 48-bit binary timer with a 1 s resolution.

For 3G and 4G Devices, the binary timer exists in two parts: a hardware DSP timer, and a software extended value. Together these values make a 48-bit time value. BTICard\_TimerResolution is used to adjust which bits of this 48-bit value are used to make the 32-bit time-tag used in message structures and Sequential Records. BTICard\_TimerRd and BTICard\_TimerWr functions only read from and write to the software extended portion of this time value and not the hardware portion (due to the complexity of accounting for the rollover from the Host, an accurate reading of both the hardware and software part is not possible). The software extended portion used by BTICard\_TimerRd and BTICard\_TimerWr has a resolution of 4.096 ms.

To use the IRIG timer on a 4G Device to generate time-tag values for message structures and Sequential Records instead of the default binary timer, see BTICard\_IRIGConfig.

#### **WARNINGS**

Use caution to account for rollover when using this function. For example, the lower 32-bits of a 1 ns binary timer will rollover approximately every 4 seconds.

Refer to BTICard\_Timer64Rd for access to the full binary timer.

```
BTICard_Timer64Rd, BTICard_Timer64Wr, BTICard_TimerClear, 
BTICard_TimerResolution, BTICard_TimerWr, BTICard_IRIGConfig, 
BTICard_IRIGRd, BTICard_IRIGWr
```
### **TimerResolution**

```
INT BTICard_TimerResolution
(
     INT timerresol, //Selects the timer resolution
     HCORE hCore //Core handle
)
```
#### **RETURNS**

The value of the previous resolution. Refer to *timerresol* for return values.

#### **DESCRIPTION**

Selects the resolution for the time-tag timer on the specified Device. Timer resolution can be read (without modifying) by setting *timerresol* to *TIMERRESOL\_CURRENT*. *timerresol* must be one of the following predefined constants:

| timerresol         |            |                              |  |  |
|--------------------|------------|------------------------------|--|--|
| Constant           | Resolution | 3G/4G Range (hr:min:sec)     |  |  |
| TIMERRESOL_CURRENT | current    | -                            |  |  |
| TIMERRESOL_1US     | 1 µs       | 1:11:34                      |  |  |
| TIMERRESOL_16US    | 16 µs      | 19:05:19                     |  |  |
| TIMERRESOL_1024US  | 1024 µs    | 50 days                      |  |  |
| TIMERRESOL_1NS     | 1 ns       | 6G Only (Refer to Table 6.1) |  |  |

#### **DEVICE DEPENDENCY**

Only 3G and 4G Devices have resolutions that can be modified. To use the IRIG timer for a specified 4G core instead of the default binary timer, see BTICard\_IRIGConfig. The 5G and 5G+ binary timer resolution is always 1 s (*TIMERRESOL\_1US*), and has a range of 365 days. The 6G binary timer resolution is always 1 ns and defaults to a range of 365 days. The 6G binary timer is capable of larger ranges, see BTICard\_TSMTimerRolloverSet.

#### **WARNINGS**

After changing the resolution on a 3G/4G Device, a call to BTICard\_TimerClear should be made to clear the timer.

```
BTICard_TimerClear, BTICard_IRIGConfig, BTICard_IRIGRd, BTICard_IRIGWr, 
BTICard_TSMTimerRolloverSet
```
### **TimerStatus**

```
INT BTICard_TimerStatus
(
      HCORE hCore //Core handle
)
```
#### **RETURNS**

The status value of the timer configuration.

#### **DESCRIPTION**

This function determines the status of how the timer for a core is configured. Some devices have configurable modes that affect elements of data structures. For example, 4G Devices allow for BCD or binary time-tag formatting.

The status value can be tested using the predefined constants below:

| Constant           | Description                    |
|--------------------|--------------------------------|
| TIMETAG_FORMAT_BCD | Time-tags are in BCD format    |
| TIMETAG_FORMAT_BIN | Time-tags are in binary format |

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

3G Devices and 4G Devices (when configured to use binary formatted time-tags) latch the time-tag value when processed by the Device firmware. This will create some minor variability in time-tags from message to message. 6G, 5G+, 5G, and 4G Devices (when configured to use IRIG BCD formatted time-tags) latch the time-tag value when processed by the Device hardware. This results in a very consistent and accurate time-tag value.

Some ARINC 429 messages of 4G Devices (when configured for binary formatted time-tags) may have identical time-tags due to batch processing of messages in the same Device firmware time slot. This is most noticeable in the Sequential Record when comparing time-tags of messages.

```
BTICard_IRIGConfig, BTICard_IRIGTimeBCDToBin, BTICard_IRIGTimeBinToBCD, 
BTICard_SeqFind??
```
### **TimerWr**

```
VOID BTICard_TimerWr
(
      ULONG value, //Value of the timer
      HCORE hCore //Core handle
)
```
#### **RETURNS**

None.

#### **DESCRIPTION**

Writes *value* to the lower 32-bits of the binary timer of the specified Device.

#### **DEVICE DEPENDENCY**

Applies to all Devices.

6G Devices have a 64-bit binary timer with a 1 ns resolution.

5G and 5G+ Devices have a 48-bit binary timer with a 1 s resolution.

For 3G and 4G Devices, the binary timer exists in two parts: a hardware DSP timer, and a software extended value. Together these values make a 48-bit time value. BTICard\_TimerResolution is used to adjust which bits of this 48-bit value are used to make the 32-bit time-tag used in message structures and Sequential Records. BTICard\_TimerRd and BTICard\_TimerWr functions only read from and write to the software extended portion of this time value and not the hardware portion (due to the complexity of accounting for the rollover from the Host, an accurate reading of both the hardware and software part is not possible). The software extended portion used by BTICard\_TimerRd and BTICard\_TimerWr has a resolution of 4.096 ms.

To use the IRIG timer on a 4G Device to generate time-tag values for message structures and Sequential Records instead of the default binary timer, see BTICard\_IRIGConfig.

#### **WARNINGS**

Use caution to account for rollover when using this function. For example, the lower 32-bits of a 1 ns binary timer will rollover approximately every 4 seconds.

Refer to BTICard\_Timer64Wr for access to the full binary timer.

```
BTICard_Timer64Wr, BTICard_Timer64Rd, BTICard_TimerRd, 
BTICard_TimerResolution, BTICard_IRIGConfig, BTICard_IRIGRd, 
BTICard_IRIGWr
```
### **TSMDriftMaxGet**

#### ERRVAL **BTICard\_TSMDriftMaxGet**

```
LPULONG driftptr,//Pointer to variable to hold current drift max(ppt)
HCORE handleval //Device handle
```
)

(

#### **RETURNS**

A negative value if an error, otherwise zero.

#### **DESCRIPTION**

Reads the current value of the maximum drift adjustment limit and places the value into the location pointed to by *driftptr*. Value is in parts-per-trillion.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

# **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMDriftMaxSet

### **TSMDriftMaxSet**

#### ERRVAL **BTICard\_TSMDriftMaxSet**

```
ULONG drift, //Value for maximum drift (ppt)
HCORE handleval //Device handle
```
### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the value *drift* to the Device's maximum drift adjustment limit. This value is the limit the device will attempt to bias the clock frequency to match an incoming source frequency. Value is in parts-per-trillion and is limited to 425 ppm (425,000,000 ppt).

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMDriftMaxGet

### **TSMDriftRd**

ERRVAL **BTICard\_TSMDriftRd**

```
(
```
LPINT *driftptr, //Pointer to variable to hold current drift (ppt)* HCORE *handleval //Device handle* 

)

#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Places the current two's complement representation of the commanded drift into the location pointed to by *driftptr*. Value is in parts-per-trillion.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

**WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMDriftWr

### **TSMDriftWr**

ERRVAL **BTICard\_TSMDriftWr**

```
INT drift, //Commanded drift value (ppt)
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the input two's complement representation to the Device's drift adjustment value. When used in conjunction with BTICard\_TSMSyncConfig called with TSMCFG\_HOST, the device will adjust the clock frequency by the amount of the input PPT. A negative number represents a slower frequency. Value is in parts-per-trillion and is limited to the range from -425,000,000 *drift* < 425,000,000.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

The Device will not adjust the frequency past the value set by BTICard\_TSMDriftMaxSet**.**

#### **SEE ALSO**

BTICard\_TSMDriftRd, BTICard\_TSMSyncConfig, BTICard\_TSMDriftMaxSet

### **TSMInputDelayCompGet**

```
ERRVAL BTICard_TSMInputDelayCompGet
```

```
LPINT delayptr, //Pointer to variable to hold current input 
                  // compensation (ns)
HCORE handleval //Device handle
```
)

(

#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the current value of the input compensation and places the value into the location pointed to by *delayptr*. Value is in nanoseconds.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMInputDelayCompSet

### **TSMInputDelayCompSet**

```
ERRVAL BTICard_TSMInputDelayCompSet
```

```
(
     INT delay, //Value for input compensation (ns)
     HCORE handleval //Device handle 
)
```
#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the value *delay* to the Device's input compensation. The Device will adjust the internal time by the set amount to account for flight and logic delays in the circuitry and wiring when synchronizing time to an external IRIG or PPS source. Value is in nanoseconds and valid range is 0 to 1000000 (1 ms).

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMInputDelayCompGet, BTICard\_TSMSyncConfig

### **TSMInputThresholdGet**

```
ERRVAL BTICard_TSMInputThresholdGet
```

```
(
  LPUSHORT dacval, //Pointer to variable to hold current DAC value
  INT pinindex, //Index indicating which input DAC threshold to read
  HCORE handleval //Device handle
```
)

#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the current value of the input threshold DAC setting and places the value into the location pointed to by *dacval*. Value is an unsigned representation.

Available *pinindex* values are shown below:

| pinindex        |                              |  |  |
|-----------------|------------------------------|--|--|
| Constant        | Description                  |  |  |
| TSMPIN_PWMIRIG0 | Threshold for IRIG PCM/PPS   |  |  |
| TSMPIN_PWMIRIG1 | Threshold for IRIG PCM/PPS   |  |  |
| TSMPIN_AMIRIGH* | Threshold for IRIG AM, Mark  |  |  |
| TSMPIN_AMIRIGL  | Threshold for IRIG AM, Space |  |  |
| TSMPIN_10MHZ    | Threshold for 10MHz          |  |  |

Note: By default the Device will auto-calculate the optimal AM IRIG Mark threshold. To disable auto-calculation for AM IRIG Mark threshold, use the flag *TSMCFG\_AMDACUSER* when calling *BTICard\_TSMSyncConfig.*

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

When using AM IRIG, unless BTICard\_TSMSyncConfig is called with the TSMCFG\_AMDACUSER flag, the Device will automatically calculate the optimal threshold. Overriding the automatic threshold is not recommended for reliable performance.

#### **SEE ALSO**

BTICard\_TSMInputDelayCompGet, BTICard\_TSMSyncConfig

### **TSMInputThresholdSet**

```
ERRVAL BTICard_TSMInputThresholdSet
```

```
(
  LPUSHORT dacval, //Value for selected input threshold
  INT pinindex, //Index indicating which input DAC threshold to set
  HCORE handleval //Device handle
```
)

#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Sets the selected input threshold DAC value to the input *dacval*. *dacval* is an unsigned representation.

Available *pinindex* are shown below:

| pinindex        |                              |  |  |  |
|-----------------|------------------------------|--|--|--|
| Constant        | Description                  |  |  |  |
| TSMPIN_PWMIRIG0 | Threshold for IRIG PCM/PPS   |  |  |  |
| TSMPIN_PWMIRIG1 | Threshold for IRIG PCM/PPS   |  |  |  |
| TSMPIN_AMIRIGH* | Threshold for IRIG AM, Mark  |  |  |  |
| TSMPIN_AMIRIGL  | Threshold for IRIG AM, Space |  |  |  |
| TSMPIN_10MHZ    | Threshold for 10 MHz         |  |  |  |

*Note: By default the Device will auto-calculate the optimal AM IRIG threshold. To disable auto-calculation for AM IRIG use the flag TSMCFG\_AMDACUSER when calling BTICard\_TSMSyncConfig.*

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

When using AM IRIG, unless BTICard\_TSMSyncConfig is called with the TSMCFG\_AMDACUSER flag, the Device will automatically calculate the optimal threshold. Overriding the automatic thresholds is not recommended for reliable performance.

```
BTICard_TSMInputDelayCompSet, BTICard_TSMSyncConfig
```
### **TSMIRIGControlRd**

```
ERRVAL BTICard_TSMIRIGControlRd
```

```
LPULONG ctrlptr, //Pointer to variable to hold IRIG control field
HCORE handleval //Device handle
```
### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the last received "Control Function" field from a received IRIG message and places the value into the location pointed to by *ctrlptr*. Control Function bits 10 to 27 are placed in bits 0 to 17 of the ULONG.

![](_page_271_Figure_9.jpeg)

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

```
BTICard_TSMIRIGControlWr, BTICard_TSMIRIGYearsWr, BTICard_TSMIRIGYearsRd, 
BTICard_TSMSyncConfig
```
### **TSMIRIGControlWr**

```
ERRVAL BTICard_TSMIRIGControlWr
```

```
ULONG ctrlval, //Value for outgoing IRIG control field
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the value to the Device to send in the "Control Function" field of outgoing IRIG messages. Control Function bits 10 to 27 are taken from bits 0 to 17 of the *ctrlval*, with Control Function bit 10 located at *ctrlval* bit 0.

![](_page_272_Figure_9.jpeg)

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

**WARNINGS**

None.

```
BTICard_TSMIRIGControlRd, BTICard_TSMIRIGYearsWr, BTICard_TSMIRIGYearsRd, 
BTICard_TSMSourceConfig
```
### **TSMIRIGYearsRd**

```
ERRVAL BTICard_TSMIRIGYearsRd
```

```
LPULONG year, //Pointer to variable to hold year value
HCORE handleval //Device handle
```
### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads Device's Year and places the value into the location pointed to by *year*. Value is an unsigned integer value between 2000 and 2099.

When using IRIG time synchronization via BTICard\_TSMSyncConfig the Device's year value will be automatically populated with the Year field of the received message. Otherwise, the Device's year value can be set by the user via BTICard\_TSMIRIGYearsWr and the value will increment at the time indicated via BTICard\_TSMTimerRolloverSet**.** For IRIG formats without a Years field, this function can be ignored.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

**WARNINGS**

None.

```
BTICard_TSMIRIGControlWr, BTICard_TSMIRIGControlRd, 
BTICard_TSMIRIGYearsWr, BTICard_TSMSyncConfig, 
BTICard_TSMTimerRolloverSet
```
### **TSMIRIGYearsWr**

#### ERRVAL **BTICard\_TSMIRIGYearsWr**

```
ULONG year, //Value to set Device year
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Sets the Device's Years to the input value *year,* which is an unsigned integer value between 2000 and 2099.

If Device has been configured via BTICard\_TSMSourceConfig to output IRIG, the Device will send *year* in an IRIG message.

The Device's Years value will increment at the time indicated via BTICard\_TSMTimerRolloverSet**.**

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

When using IRIG time synchronization via BTICard\_TSMSyncConfig the Device's year value will be automatically populated with the Year field of the received message, overwriting the User set value.

#### **SEE ALSO**

BTICard\_TSMIRIGYearsRd, BTICard\_TSMSyncConfig, BTICard\_TSMSourceConfig, BTICard\_TSMTimerRolloverSet

### **TSMJumpThresholdGet**

ERRVAL **BTICard\_TSMJumpThresholdGet**

```
(
 LPULONG drift, //Pointer to variable to hold current offset drift 
     // threshold (ppt)
 LPULONG offset, //Pointer to variable to hold current offset jump 
  // threshold (ns)
 HCORE handleval //Device handle
```
#### **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the current values of the offset drift threshold and offset jump threshold and places the values into the location pointed to by *drift* and *offset* respectively. *drift* is in Parts-per-Trillion and *offset* is in nanoseconds. See BTICard\_TSMJumpThresholdSet for details on how these thresholds work.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMJumpThresholdSet

### **TSMJumpThresholdSet**

```
ERRVAL BTICard_TSMJumpThresholdSet
(
      ULONG drift, //Value for offset drift threshold (ppt)
      ULONG offset, //Value for offset jump threshold (ns)
      HCORE handleval //Device handle 
)
```
#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes the values *drift and offset* to the Device's IRIG/PPS drift and time thresholding limits.

When using the Device to synchronize to an single external IRIG/PPS source for both time and drift modes, it is possible to 'drift' the timer to match the external time source time by overcorrecting the frequency of the Device's clock to relatively speed or slow the timer's incrementing. This will cause the timer to move slowly towards the target time value rather than immediately 'jumping' to the received time. This may be useful to prevent discontinuities in protocol timestamps.

*The drift* threshold is used to set the maximum drift adjustment a time delta can force onto the clock frequency. Note that this is in addition to any drift difference due to frequency variation between timing samples and the sum is clipped by the value set by *BTICard\_TSMDriftMaxSet*. The value is in parts-per-trillion unsigned representation with a valid range of 0 to 425,000,000.

The *offset* threshold is used to set the minimum offset value that will force the timer to 'jump' to the received time. When the time received via IRIG/PPS is more than the *offset* threshold from the Device's internal time, the Device's time will snap to the received time. Value is in nanoseconds, unsigned representation with a valid range of 0 to 0xFFFFFFFF.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

```
BTICard_TSMJumpThresholdGet, BTICard_TSMSyncConfig,
BTICard_TSMDriftMaxSet
```
### **TSMReset**

#### ERRVAL **BTICard\_TSMReset**

HCORE *handleval //Device handle* 

# )

(

#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Commands the Timing Synchronization Manager to reset. Resets Device time and mode settings.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

TSM functionality is not reset on BTICard\_CardReset.

This function will not reset input threshold DAC values.

#### **SEE ALSO**

BTICard\_CardReset

### **TSMSourceConfig**

```
ERRVAL BTICard_TSMSourceConfig
(
```

```
ULONG sourcecfg //Specifies configuration 
HCORE handleval //Device handle
```
#### **RETURNS**

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Sets the Device's TSM as the output timing source with the options defined by *sourcecfg*. The constants below define the available operating modes of the TSM, which is able to source a single IRIG or PPS timing mode and simultaneously output the 10 MHz signal. Use TSMCFG\_IRIGA and TSMCFG\_IRIGB to select the IRIG speed.

The TSM is able to "relay" an IRIG timing input to a PPS timing output. To use this feature, call BTICard\_TSMSyncConfig first with the desired IRIG input, and call

BTICard\_TSMSourceConfig second with the desired PPS output, ensuring that the input and output are on different pins.

| sourcecfg       |                                                                            |  |
|-----------------|----------------------------------------------------------------------------|--|
| Constant        | Description                                                                |  |
| TSMCFG_NONE     | Select all default settings (bold below).                                  |  |
| TSMCFG_PPS0     | Outputs PPS signal                                                         |  |
| TSMCFG_PPS1     | Outputs PPS signal                                                         |  |
| TSMCFG_PWMIRIG0 | Outputs PWM IRIG signal at speed selected by TSMCFG_IRIGA and TSMCFG_IRIGB |  |
| TSMCFG_PWMIRIG1 | Outputs PWM IRIG signal at speed selected by TSMCFG_IRIGA and TSMCFG_IRIGB |  |
| TSMCFG_AMIRIG   | Outputs AM IRIG signal at speed selected by TSMCFG_IRIGA and TSMCFG_IRIGB  |  |
| TSMCFG_10MHZ    | Outputs 10 MHz signal                                                      |  |
| TSMCFG_IRIGA    | Selects 100 ms period IRIG                                                 |  |
| TSMCFG_IRIGB    | Selects 1 s period IRIG                                                    |  |

See specific hardware manuals for information regarding TSM pin specifications.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMSyncConfig

### **TSMStatus**

```
ERRVAL BTICard_TSMStatus
```

```
LPULONG statusptr //Pointer to value to store current status
HCORE handleval //Device handle
```
### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the Device's TSM status and loads the results into the location specified by statusptr*.* The status value can be tested using the predefined constants below:

| *statusptr          |                                                 |  |
|---------------------|-------------------------------------------------|--|
| Constant            | Description                                     |  |
| TSMSTAT_IRIGPRES    | Input IRIG signal is toggling and decodable     |  |
| TSMSTAT_IRIGBITSYNC | Input IRIG signal is toggling                   |  |
| TSMSTAT_IRIGSYNC    | TSM is locked to IRIG input                     |  |
| TSMSTAT_10MHZPRES   | Input 10 MHz signal is toggling                 |  |
| TSMSTAT_10MHZSYNC   | TSM is locked to 10 MHz input                   |  |
| TSMSTAT_PPSPRES     | Input PPS signal is toggling                    |  |
| TSMSTAT_PPSSYNC     | TSM is locked to PPS input                      |  |
| TSMSTAT_OUTOFBOUNDS | Drift control is outside of set drift threshold |  |

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

If the TSM clock and input signal source are drifting more than the drift threshold returned by BTICard\_TSMDriftMaxGet, the TSM will not lock to the source and the associated synchronization status {TSMSTAT\_IRIGSYNC, TSMSTAT\_PPSSYNC, TSMSTAT\_10MHZSYNC} will not be asserted.

#### **SEE ALSO**

BTICard\_TSMSyncConfig, BTICard\_TSMSourceConfig, BTICard\_TSMDriftMaxGet

### **TSMSyncConfig**

```
ERRVAL BTICard_TSMSyncConfig
(
      ULONG timeconfig //Specifies time configuration 
      ULONG driftconfig //Specifies drift configuration 
      HCORE handleval //Device handle 
)
```
#### **RETURNS**

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Sets the Device's TSM to synchronize time and/or drift to an external source with options defined by *timeconfig and driftconfig as shown in the tables below*.

The TSM can synchronize time to a single IRIG or PPS input as set by *timeconfig and drift to* a single IRIG, PPS or 10 MHz input as set by *driftconfig.* 

Using time synchronization with an IRIG source will set the Device's timer to the time received via IRIG. Using time synchronization with a PPS source will round the timer to the nearest second at the PPS timing mark. When using a PPS input source, it is recommended to call BTICard\_TSMTimerWr to set the Device's time to the system time.

Using the TSM drift controls (set by *driftconfig*) will speed or slow the Device's clock by up to 425 PPM to account for the clock drift between the Device clock and the input source clock.

If BTICard\_TSMSyncConfig is not called, the host can still adjust the time using BTICard\_TSMTimerWr.

| timeconfig       |                                                                            |  |
|------------------|----------------------------------------------------------------------------|--|
| Constant         | Description                                                                |  |
| TSMCFG_NONE      | Select all default settings (bold below). Timer is free-running            |  |
| TSMCFG_PPS0      | Synchronizes time to PPS signal                                            |  |
| TSMCFG_PPS1      | Synchronizes time to PPS signal                                            |  |
| TSMCFG_PWMIRIG0  | Synchronizes time to PCM IRIG signal at speed selected by TSMCFG_IRIGA and |  |
|                  | TSMCFG_IRIGB                                                               |  |
| TSMCFG_PWMIRIG1  | Synchronizes time to PCM IRIG signal at speed selected by TSMCFG_IRIGA and |  |
|                  | TSMCFG_IRIGB                                                               |  |
| TSMCFG_AMIRIG    | Synchronizes time to AM IRIG signal at speed selected by TSMCFG_IRIGA and  |  |
|                  | TSMCFG_IRIGB                                                               |  |
| TSMCFG_IRIGA     | Selects 100 ms period IRIG                                                 |  |
| TSMCFG_IRIGB     | Selects 1 s period IRIG                                                    |  |
| TSMCFG_AMDACAUTO | Automatically selects appropriate AM IRIG voltage threshold                |  |
| TSMCFG_AMDACUSER | Allows user to select AM IRIG voltage threshold                            |  |

| driftconfig      |                                                                        |  |
|------------------|------------------------------------------------------------------------|--|
| Constant         | Description                                                            |  |
| TSMCFG_NONE      | Select all default settings (bold below). Clock frequency is unbiased. |  |
| TSMCFG_PPS0      | Adjusts clock frequency to match that of source of PPS signal          |  |
| TSMCFG_PPS1      | Adjusts clock frequency to match that of source of PPS signal          |  |
| TSMCFG_PWMIRIG0  | Adjusts clock frequency to match that of source of PCM IRIG signal     |  |
| TSMCFG_PWMIRIG1  | Adjusts clock frequency to match that of source of PCM IRIG signal     |  |
| TSMCFG_AMIRIG    | Adjusts clock frequency to match that of source of AM IRIG signal      |  |
| TSMCFG_10MHZ     | Adjusts clock frequency to match that of source of 10 MHz signal       |  |
| TSMCFG_HOST      | Allows user to adjust clock frequency via BTICARD_TSMDriftWr           |  |
| TSMCFG_IRIGA     | Selects 100 ms period IRIG                                             |  |
| TSMCFG_IRIGB     | Selects 1 s period IRIG                                                |  |
| TSMCFG_AMDACAUTO | Automatically selects appropriate AM IRIG voltage threshold            |  |
| TSMCFG_AMDACUSER | Allows user to select AM IRIG voltage threshold                        |  |

| Drift Input | Time Input |
|-------------|------------|
| None        | None       |
| None        | IRIG       |
| None        | PPS        |
| 10 MHz      | None       |
| 10 MHz      | IRIG       |
| 10 MHz      | PPS        |
| IRIG        | IRIG       |
| PPS         | None       |
| PPS         | IRIG       |
| PPS         | PPS        |

The TSM can use drift and time controls simultaneously with valid configurations of sources defined in a table below.

*TSM valid drift and time combinations*

See specific hardware manuals for information regarding TSM pin specifications.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMSourceConfig

### **TSMTimerRd**

#### ERRVAL **BTICard\_TSMDTimerRd**

```
LPULONGLONG valueptr, //Pointer to variable to hold current time 
          //(ns)
    HCORE handleval //Device handle 
)
```
#### **RETURNS**

(

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the current value of the Device timer and places the value into the location pointed to by value*ptr*. Value is in nanoseconds.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMTimerWr

### **TSMTimerRelWr**

```
ERRVAL BTICard_TSMTimerRelWr
```

```
LONGLONG value, //Value to jump timer (ns)
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Commands the Device to 'jump' the timer by the number of nanoseconds in *value*. A negative *value* will 'jump' the timer backwards in time.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

'Jumping' the timer backwards can cause out of order protocol timestamps.

Using a *value* that is large enough to push the timer value over the rollover value will cause the timer to immediately rollover and start incrementing from zero. Writing a negative number with magnitude larger than the current timer value will cause the timer to wrap around and rollover (depending on current rollover value).

#### **SEE ALSO**

BTICard\_TSMTimerWr

### **TSMTimerRolloverGet**

ERRVAL **BTICard\_TSMTimerRolloverGet**

```
LPULONGLONG valueptr, //Pointer to variable to hold timer rollover 
     //(ns)
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Reads the Device's current timer rollover and places the value into the location pointed to by *valueptr*. Value is in nanoseconds. Default rollover value is exactly 365 days, but can be adjusted by BTICard\_TSMTimerRolloverSet to account for leap years and seconds. Value is in nanoseconds and valid range is 1000 to 2^64 - 1.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMTimerRolloverSet

### **TSMTimerRolloverSet**

```
ERRVAL BTICard_TSMTimerRolloverSet
```

```
ULONGLONG value, //Value for timer rollover (ns)
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Writes *value* to the Device's timer rollover. This will signal the Device to increment the Years field and reset the timer to zero. Default rollover value is exactly 365 days, but can be adjusted to account for leap years and leap seconds. value is in nanoseconds and valid range is 1000 to 2^64 - 1.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_TSMTimerRolloverGet

### **TSMTimerWr**

#### ERRVAL **BTICard\_TSMTimerWr**

```
ULONGLONG value, //Value to set timer (ns)
HCORE handleval //Device handle
```
#### **RETURNS**

(

)

A negative value if an error occurs, otherwise zero.

#### **DESCRIPTION**

Commands the Device to set the timer to the input *value*. Valid range is 0 to (2^64)-1.

#### **DEVICE DEPENDENCY**

Applies to Core A of all 6G Devices.

#### **WARNINGS**

Writing a timer value higher than the timer rollover (set by BTICard\_TSMTimerRolloverSet**)** will cause the timer to rollover to zero.

#### **SEE ALSO**

BTICard\_TSMTimerRd, BTICard\_TSMTimerRelWr, BTICard\_TSMTimerRolloverSet

### **ValFromAscii**

```
ULONG BTICard_ValFromAscii
(
      LPCSTR asciistr, //ASCII string to convert
      INT radixval //Radix of string
)
```
#### **RETURNS**

The converted integer numeric value.

#### **DESCRIPTION**

Converts a string representation of a 32-bit value with the specified radix to an integer. Processing stops at the first null terminator. *radixval* can be any positive integer, but is commonly 16 for hexadecimal, 8 for octal, or 10 for decimal.

*Note: This is a utility function and does not access the Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

No check is made for invalid characters.

#### **SEE ALSO**

BTICard\_ValToAscii

### **ValGetBits**

```
ULONG BTICard_ValGetBits
(
     ULONG oldvalue, //The old value
     INT startbit, //Position of starting bit of field
     INT endbit //Position of ending bit of field
)
```
#### **RETURNS**

The value of the extracted bit field.

#### **DESCRIPTION**

Extracts the specified bit field from the 32-bit integer *oldvalue*. The result is obtained by masking the field and shifting the *endbit* to the LSB of the return value. The LSB is bit number zero.

*Note: This is a utility function and does not access the Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

**WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ValPutBits

### **ValPutBits**

#### USHORT **BTICard\_ValPutBits**

```
(
     ULONG oldvalue, //The old value
     ULONG newfld, //The value of the new field
     INT startbit, //Position of starting bit of field
     INT endbit //Position of ending bit of field
```
# )

#### **RETURNS**

The integer value with the inserted bit field.

#### **DESCRIPTION**

Inserts a bit field into a 32-bit integer value. The *oldval* is masked and OR-ed with the shifted value of *newfld*. The LSB is bit number zero.

*Note: This is a utility function and does not access the Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

#### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ValGetBits

### **ValToAscii**

```
LPSTR BTICard_ValToAscii
(
     ULONG value, //The value to be converted
     LPSTR asciistr, //A string to receive the results
     INT numbits, //The number of significant bits
     INT radixval, //The radix value
)
```
#### **RETURNS**

An ASCII string representing the integer.

#### **DESCRIPTION**

Creates a string representation of an integer in a specified radix. The string is copied to *asciistr* and is also returned. The string is always null-terminated. *asciistr* is assumed to be large enough to hold the resulting string.

The length of the string is determined by *numbits* and *radixval* and is padded by leading zeros. *radixval* can be any positive integer but is commonly 16 for hexadecimal, 8 for octal, or 10 for decimal. For example, a string representation of a value with 16 significant bits and a radix of 16 will always be 4 characters long followed by a null terminator.

*Note: This is a utility function and does not access the Device hardware.*

#### **DEVICE DEPENDENCY**

Applies to all Devices.

### **WARNINGS**

None.

#### **SEE ALSO**

BTICard\_ValFromAscii

# <span id="page-291-0"></span>**6. Device Dependence**

The BTIDriver unified API supports many generations of Ballard hardware Devices. This crosscompatibility allows for application reuse when migrating from one Device to another. Each successive generation of Ballard hardware Devices tries to build upon the feature set of the previous one. Therefore, not all features supported by this API apply to all hardware Devices. Functions that depend upon a particular hardware Device will reference the products listed in the following table by generation or by other functionality.

| Product                                          |   | Generation/Group |    |     |    |     |  |  |
|--------------------------------------------------|---|------------------|----|-----|----|-----|--|--|
|                                                  |   | 4G               | 5G | 5G+ | 6G | RPC |  |  |
| OmniBusBox II (262-xxx-xxx, 264-xxx-xxx-xxx-xxx) |   |                  |    |     | x  | x   |  |  |
| OmniBus II PXIe (222-xxx-xxx)                    |   |                  |    |     | x  |     |  |  |
| OmniBus II PCIe (212-xxx-xxx)                    |   |                  |    |     | x  |     |  |  |
| NG1 (NG1-xx-xxx)                                 |   |                  |    | x   |    | x   |  |  |
| NG2 (NG2xxxxx-xx-xx-xx-xxx)                      |   |                  |    | x   |    | x   |  |  |
| NG3 (NG3xxxxx-xx-xx-xx-xx-xx-xxx)                |   |                  |    | x   |    | x   |  |  |
| UA2000 (UA2xxx)                                  |   |                  |    | x   |    |     |  |  |
| ME1000 (ME1xxx)                                  |   |                  |    | x   |    |     |  |  |
| AB3000 Series N (AB3xxxN)                        |   |                  |    | x   |    | x   |  |  |
| Mx5 (Mx5x-xx-xx)                                 |   |                  | x  |     |    |     |  |  |
| UA1000 (UA1xxx)                                  |   |                  | x  |     |    |     |  |  |
| PE1000                                           |   |                  | x  |     |    |     |  |  |
| PM1553-5, PM429-2                                |   |                  | x  |     |    |     |  |  |
| LE1553-5, LP1553-5, LE429-5, LP429-5             |   |                  | x  |     |    |     |  |  |
| AB3000 Series 1 and 2 (AB3xxx)                   |   |                  | x  |     |    | x   |  |  |
| Avionics BusBox 2000 (AB2xxx)                    |   |                  | x  |     |    | x   |  |  |
| Avionics BusBox 1000 (AB1xxx)                    |   |                  | x  |     |    | x   |  |  |
| OmniBusBox (162-xxx-xxx)                         |   | x                |    |     |    | x   |  |  |
| OmniBus VME (152-xxx-xxx, 154-xxx-xxx-xxx-xxx)   |   | x                |    |     |    | x   |  |  |
| OmniBus PMC (141-xxx)                            |   | x                |    |     |    |     |  |  |
| OmniBus cPCI (121-xxx, 122-xxx-xxx)              |   | x                |    |     |    |     |  |  |
| OmniBus PCI (111-xxx, 112-xxx-xxx)               |   | x                |    |     |    |     |  |  |
| BUSBox (BB1xxx)                                  | x |                  |    |     |    |     |  |  |

*Table 6.1—Devices Grouped by Generation and Functionality*

<span id="page-291-1"></span>*Note: The protocol(s) outlined in this manual may not be supported by all devices. See the device manual for more information* 

# <span id="page-292-0"></span>**7. Multi-Protocol / Device Programs**

A single software application can be written to simultaneously operate many similar or dissimilar BTIDriver-compliant products, each supporting a single or multiple avionics databus protocols. This section provides information needed to write software programs to control multiple Devices and Devices that support more than one protocol.

### <span id="page-292-1"></span>**7.1 Programming Rules**

Guidelines for writing multi-Device and multi-protocol programs are summarized in the following rules. The discussion in the rest of this section further explains these rules.

- 1. A card number for each Device is assigned by the operating system. If only one BTIDrivercompliant Device exists on the system, it is assigned card number zero (0) by the operating system.
- 2. A core number for each core on the Device is set by the architecture of the Device. If only one core exists on the Device, it is core number zero.
- 3. A test utility is provided with the Device for indicating and associating the card number with each individual Device and the core number of each core. A utility for reassigning the card number may also be included with the Device. The card numbers assigned to BTIDrivercompliant Devices are specific to them, so there is no conflict when devices that are not BTIDriver-compliant use those same card numbers.
- 4. The card handle returned by BTICard\_CardOpen is passed to BTICard\_CoreOpen to obtain the core handle used by all channels and all protocols on that core.
- 5. The recommended programming practice is to use the card handle only in BTICard\_CoreOpen and BTICard\_CardClose (i.e., to obtain core handles and to release the resources back to the operating system at the end of the program). All other functions needing a handle should use the core handle.
- 6. If a card handle is used in place of a core handle, it has the same effect as when the handle for core number zero is used. Programs for single-core Devices can be written without using core handles, but they would be more easily ported to other Devices by following the recommendation of using core handles.
- 7. Card functions (those prefixed with BTICard\_) are shared with all protocols and channels on the core specified by the core handle. For instance, BTICard\_CardStart starts all channels on the core (independent of protocol). Note that using a card handle with this function only starts channels on core number zero.
- 8. Different protocol functions may be interleaved in the program between the common BTICard\_ functions.

## <span id="page-292-2"></span>**7.2 BTICard\_ Functions**

BTICard\_ functions are common to all protocols supported by the core. When a BTICard\_ function is used, all protocols on the core specified by the core handle are affected. Programs supporting different protocols may be combined into a single program by interleaving the protocol-specific functions with common BTICard\_ functions. A normal application would use BTICard\_CardOpen and BTICard\_CardClose once for each Device and BTICard\_CoreOpen once for each core. Similarly,

BTICard\_ functions like BTICard\_CardStart and BTICard\_CardStop apply to all channels and protocols on the specified core.

### <span id="page-293-0"></span>**7.3 Sequential Record**

Each core has one Sequential Record, independent of how many different protocols it supports. The format of individual records within the Sequential Record differs between protocols. There are two ways of scanning through a Sequential Record: by protocol-specific records or by every record. To scan by protocol, use the BTICard\_SeqFindNext?? function to find the next record with the ?? protocol. For instance, the BTICard\_SeqFindNext429 and BTICard\_SeqFindNext1553 functions are used to find the next ARINC 429 or MIL-STD-1553 record respectively. To scan through every record, use the BTICard\_SeqFindNext function, which finds the next record and returns the type (429, 1553, etc.) of the record it found. The different BTICard\_SeqFindNext?? functions should not be mixed within a sequence without first using BTICard\_SeqFindInit. Note that the BTICard\_SeqFindNext?? functions do not use a handle, so they do not access the Device. They work from a copy of the Sequential Record in the computer's memory. Thus, they may be used to process a Sequential Record that had been previously saved to a hard disk.

### <span id="page-293-1"></span>**7.4 Event Log List**

As with the Sequential Record, there is one Event Log List per core, independent of how many protocols are supported. However, all records in the Event Log List have the same format. To determine the cause of the event and the protocol associated with it, test the type value passed through BTICard\_EventLogRd. There are some event types that are common between protocols and some that are unique to specific protocols.

### <span id="page-293-2"></span>**7.5 Using Multiple Devices**

A program that uses more than one Device can be viewed as a combination of programs for the individual Devices. Every BTIDriver function in the individual programs would appear in the combined program and may be interleaved so as to provide the desired functionality. All BTICard\_ functions affect only the Device specified by the handle (e.g. each Device needs its own BTICard\_CardOpen and BTICard\_CardClose functions, and each core needs its own BTICard\_CoreOpen, BTICard\_CardStart, and BTICard\_CardStop functions). If interrupts are used, there should be separate interrupt service threads for each core on each Device. In a similar way, non-BTIDrivercompliant devices may be combined into the program using the API for that device(s).

# <span id="page-294-0"></span>**8. Revision History**

The following revisions have been made to this manual:

| Revision | Date              | Description                                                                                                                                                                                                                                                                             |
|----------|-------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| A        | July 12, 2001     | Original release of this manual.                                                                                                                                                                                                                                                        |
| B        | January 23, 2009  | Major revision of both the text and the appendices to<br>accommodate the features and functionality of multi-core and 5G<br>Devices. Many functions are new or modified.                                                                                                                |
| B1       | August 7, 2009    | Minor revision of both the text and appendices to fix typos and<br>add clarifications to function usage.                                                                                                                                                                                |
| C        | January 6, 2010   | Major revision of both the text and the appendices to<br>accommodate the features and functionality of USB 429 Devices.                                                                                                                                                                 |
| D        | June 14, 2011     | Major revision of both the text and the appendices to<br>accommodate the features and functionality of Mx5 Devices.<br>Added BIT and SysMon functions.                                                                                                                                  |
| E        | November 21, 2013 | Minor revision of both the text and the appendices to<br>accommodate SysMon function warnings and list function<br>warning. Added MsgMultiSkipWr and SchedPulse functions.<br>Added AB3000, PM1553-5, USB 708, and USB Multi Device<br>references. Some function descriptions modified. |
| F        | June 27, 2017     | Major revision to include new and updated BTI429 API functions<br>for OmniBus II products in Chapter 4 and a new BTICard_<br>function in Chapter 5. Added/edited text in Appendices as well.                                                                                            |
| F1       | May 7, 2018       | Added new BTICard_ function in Chapter 5 (CardGetInfo).                                                                                                                                                                                                                                 |
| 7        | December 2, 2024  | Major revision to upgrade style/format, update and standardize<br>device dependencies with the addition of 5G+, standardize the<br>BTICARD references, and other miscellaneous updates.                                                                                                 |