# **Ballard UA2000 USB Avionics Interfaces**

**User Manual**

![](_page_0_Picture_2.jpeg)

![](_page_0_Picture_3.jpeg)

December 1, 2022 Rev. 2

#### **Export Controlled Technology**

*This information is controlled for export in accordance with the U.S. Export Administration Regulations ("EAR") under EAR99. Export, reexport, or transfer without a license or other authorization from the Bureau of Industry and Security is prohibited.*

Copyright 2022 Astronics Advanced Electronic Systems Corp.

**Astronics AES** 12950 Willows Road NE Kirkland, WA 98034 USA

Phone: +1.425.339.0281 Email: [Ballard.Sales@astronics.com](mailto:Ballard.Sales@astronics.com)

![](_page_0_Picture_10.jpeg)

MA235-20221201 Rev. 2

# **Copyright Notice**

*Copyright 2022 by Astronics Advanced Electronic Systems Corp.—hereafter referred to as Astronics AES. Permission to copy and distribute this manual is for the purchaser's private use only and is conditioned upon the purchaser's use and application with the hardware that was shipped with this manual. This notice allows no commercial resale or outside distribution rights. This material remains the property of Astronics Advanced Electronic Systems Corp. All other rights reserved by Astronics Advanced Electronic Systems Corp.*

#### **Safety Warning**

*Ballard products are not intended, warranted or certified for any particular use or application or for use in any application where failure of the products could lead directly to death, personal injury, or damage to property. Customers, licensees, and/or users are responsible for establishing and assuring suitability and proper use of Ballard products for their particular use or application.*

#### **Interference**

*This equipment generates, uses, and can radiate radio frequency energy and if not installed and used in accordance with the user manual, may cause interference to radio communications. Operation of this equipment in a residential area is likely to cause interference in which case the user, at their own expense, will be responsible for taking whatever measures that may be required to correct the interference.*

#### **Trademarks**

*Ballard Technology and CoPilot are registered trademarks of Astronics Advanced Electronic Systems Corp. BTIDriver is a trademark of Astronics Advanced Electronic Systems Corp. Windows is a trademark of Microsoft Corporation. All other product names or trademarks are property of their respective owners.*

# **FCC Declaration of Conformity**

#### **Responsible Party**

Astronics AES 12950 Willows Road NE Kirkland, WA 98034 USA

Phone : +1.425.339.0281

Fax : +1.425.339.0915

Web : [www.astronics.com](http://www.astronics.com/)

Email : [Ballard.Support@astronics.com](mailto:Ballard.Support@astronics.com)

#### **Products**

UA2000 1553, UA2000 429/717 and UA2000 708

Model numbers UA2xxx (where x is any numeric character).

This device complies with part 15 of the FCC Rules. Operation is subject to the following two conditions: (1) This device may not cause harmful interference, and (2) this device must accept any interference received, including interference that may cause undesired operation.

| 1. INTRODUCTION8                                                      |  |
|-----------------------------------------------------------------------|--|
| UA2000 Overview 8                                                     |  |
| Product Architecture8                                                 |  |
| UA2000 Configurations 9                                               |  |
| Avionics Databus Protocols 9                                          |  |
| Other Documentation 10                                                |  |
| Support and Service10                                                 |  |
| Updates 10                                                            |  |
| 2. INSTALLATION 11                                                    |  |
| Step 1: Read the Driver Installation Instructions11                   |  |
| Step 2: Connect the UA2000 to the Host Computer11                     |  |
| Step 3: Set the Card Number and Test the Installation 11              |  |
| Step 4: Connect to Databus I/O 12                                     |  |
| 3. OPERATION 13                                                       |  |
| CoPilot13                                                             |  |
| User Developed Software 14                                            |  |
| Translation Software for Older Products14                             |  |
|                                                                       |  |
|                                                                       |  |
| 4. TROUBLESHOOTING15                                                  |  |
| LED Indicators15                                                      |  |
| UA2000 Test Program 15                                                |  |
| 5. UA2000 FEATURES16                                                  |  |
| USB 3.116                                                             |  |
| MIL-STD-155316                                                        |  |
| ARINC 429 16                                                          |  |
| 5.3.1<br>Configurable Frequency 17                                    |  |
| ARINC 717 17                                                          |  |
| ARINC 708 17                                                          |  |
| Avionics Shunt Discrete I/O, Syncs, and Triggers 17                   |  |
| 5.6.1<br>Shunt Inputs 17                                              |  |
| 5.6.2<br>Shunt Input Considerations 18                                |  |
| 5.6.3<br>Shunt Outputs18<br>5.6.4<br>Shunt Output Considerations19    |  |
| 5.6.5<br>Shunt Discrete Input/Output Usage 19                         |  |
| IRIG Timer20                                                          |  |
| UA2000 Shared Interfaces21<br>5.8.1<br>ARINC 429/ARINC 717 Bi-Polar21 |  |

| 6. CONNECTOR INFORMATION22                                          |  |  |
|---------------------------------------------------------------------|--|--|
| USB Connector 22                                                    |  |  |
| Databus I/O Connector 22                                            |  |  |
| Databus I/O Connector Pin-outs 23                                   |  |  |
| 6.3.1<br>Model UA2110, UA2120, and UA2130 23                        |  |  |
| 6.3.2<br>Model UA2111, UA2121, UA2122, UA2131, and UA2133 24        |  |  |
| 6.3.3<br>Model UA214025                                             |  |  |
| 6.3.4<br>Model UA240127                                             |  |  |
| 6.3.5<br>Model UA241028                                             |  |  |
| 6.3.6<br>Model UA242029                                             |  |  |
| 6.3.7<br>Model UA243030                                             |  |  |
| 6.3.8<br>Model UA243131<br>6.3.9<br>Model UA244033                  |  |  |
| 6.3.10 Model UA271035                                               |  |  |
| 6.3.11 Model UA272036                                               |  |  |
| Standard MIL-STD-1553 Cables38                                      |  |  |
| 6.4.1<br>Single Channel Twinax Cable (16065) 38                     |  |  |
| 6.4.2<br>Two Channel Twinax Cable (16066)39                         |  |  |
| 6.4.3<br>Two Channel Twinax Cable with Auxilary Connector (16064)39 |  |  |
| ARINC 429 DB44 to DB15 Adapter Cable (16067) 40                     |  |  |
| ARINC 717 Cable (16078) 40                                          |  |  |
|                                                                     |  |  |
| 7. COUPLING AND TERMINATION41                                       |  |  |
| Bus Termination 41                                                  |  |  |
| Transformer versus Direct Coupling 41                               |  |  |
| 8. MECHANICAL DIAGRAMS44                                            |  |  |
|                                                                     |  |  |
| Dimensions44                                                        |  |  |
| Mounting Features 45                                                |  |  |
| 8.2.1<br>Securing UA2000s45                                         |  |  |
| 8.2.2<br>Stacking Multiple UA2000s45                                |  |  |
| 9. SPECIFICATIONS 46                                                |  |  |
| General46                                                           |  |  |
| Model Dependent Interfaces46                                        |  |  |
| Software 47                                                         |  |  |
| Environmental / Physical47                                          |  |  |
| Power 47                                                            |  |  |
|                                                                     |  |  |
| 10.REVISION HISTORY 49                                              |  |  |

| Table 1-1 – UA2000 Part Numbers 9                                         |  |
|---------------------------------------------------------------------------|--|
| Table 5-1 – Ballard MIL-STD-1553 Functionality Levels16                   |  |
| Table 5-2 – Hardware Versus Software Designation of Shunt Discrete I/O 20 |  |
| Table 5-3 – Input IRIG formats used by UA2000 21                          |  |
| Table 5-4 – Output IRIG formats used by UA2000 21                         |  |
| Table 5-5 – Electrical characteristics of UA2000 IRIG signals21           |  |
| Table 6-1 – Model UA2110, UA2120, and UA2130 Pinout 23                    |  |
| Table 6-2 – Model UA2111, UA2121, UA2122, UA2131, and UA2133 Pinout24     |  |
| Table 6-3 – Model UA2140 Pinout 26                                        |  |
| Table 6-4 – Model UA2401 Pinout 27                                        |  |
| Table 6-5 – Model UA2410 Pinout 28                                        |  |
| Table 6-6 – Model UA2420 Pinout 29                                        |  |
| Table 6-7 – Model UA2430 Pinout 30                                        |  |
| Table 6-8 – Model UA2431 Pinout 32                                        |  |
| Table 6-9 – Model UA2440 Pinout 34                                        |  |
| Table 6-10 – Model UA2710 Pinout 35                                       |  |
| Table 6-11 – Model UA2720 Pinout 37                                       |  |
| Table 6-12 – 16065 Cable Pinout38                                         |  |
| Table 6-13 – 16066 Cable Pinout39                                         |  |
| Table 6-14 – 16064 Cable Twinax Pinout 39                                 |  |
| Table 6-15 – 16064 Auxilary Connector Pinout39                            |  |
| Table 6-16 – 16067 Cable Pinout40                                         |  |
| Table 6-17 – 16078 Cable Pinout40                                         |  |

# **LIST OF FIGURES**

| Figure 1-1 – The Ballard UA20008                                 |
|------------------------------------------------------------------|
| Figure 1-2 – UA2000 Product Architecture 8                       |
| Figure 3-1 – Sample CoPilot Screen13                             |
| Figure 5-1 – UA2000 Discrete Shunt Input Circuit 18              |
| Figure 5-2 – UA2000 Discrete Shunt Output Circuit 19             |
| Figure 6-1 – USB-C Cable with Screw-Lock Feature22               |
| Figure 6-2 – Databus I/O Pin Numbers 22                          |
| Figure 6-3 – UA2000 1553 Twinax Cable 38                         |
| Figure 7-1 – Transformer Coupling to a Dual-Redundant Databus 42 |
| Figure 7-2 – Direct Connection to a Dual-Redundant Databus43     |
| Figure 8-1 – UA2000 Dimensions 44                                |
|                                                                  |

# <span id="page-7-0"></span>**1. Introduction**

This document is the user manual for the Ballard UA2000 family of products, including the MIL-STD-1553 Series, ARINC 429 & 717 Series, and the ARINC 708 Series. This guide introduces the UA2000 product, describes the installation process, references programming alternatives, and discusses special UA2000 features.

#### <span id="page-7-1"></span>**UA2000 Overview**

The Ballard UA2000 is a portable, self-contained interface connecting avionics databuses to a computer via the Universal Serial Bus (USB). The UA2000 can be configured and used for avionics development, production, acceptance testing, system simulation, maintenance, and file loading. Many common avionics databus protocols are supported, including MIL-STD-1553, ARINC 429, ARINC 717 and ARINC 708. In addition, the UA2000 provides an IRIG timer and input/output avionics shunt discrete signals.

![](_page_7_Picture_4.jpeg)

*Figure 1-1 – The Ballard UA2000*

#### <span id="page-7-3"></span><span id="page-7-2"></span>**Product Architecture**

![](_page_7_Figure_7.jpeg)

**MIL-STD-1553/ARINC 429/ARINC 717/ARINC 708/IRIG Avionics Discretes**

<span id="page-7-4"></span>*Figure 1-2 – UA2000 Product Architecture*

#### <span id="page-8-0"></span>**UA2000 Configurations**

Different UA2000 models support different databus protocols with various levels of functionality including MIL-STD-1553, ARINC 429, ARINC 717 and ARINC 708. [Table 1-1](#page-8-2) below shows the standard UA2000 models and their respective configurations.

| Model Number | Description                                                |
|--------------|------------------------------------------------------------|
| UA2110       | UA2000 with MIL-STD-1553 1 Ch. Bus Monitor                 |
| UA2111       | UA2000 with MIL-STD-1553 2 Ch. Bus Monitor                 |
| UA2120       | UA2000 with MIL-STD-1553 1 Ch. Single Function             |
| UA2121       | UA2000 with MIL-STD-1553 2 Ch. Single/Bus Monitor          |
| UA2122       | UA2000 with MIL-STD-1553 2 Ch. Single Function             |
| UA2130       | UA2000 with MIL-STD-1553 1 Ch. Multi Function              |
| UA2131       | UA2000 with MIL-STD-1553 2 Ch. Multi/Bus Monitor           |
| UA2133       | UA2000 with MIL-STD-1553 2 Ch. Multi Function              |
| UA2140       | UA2000 with MIL-STD-1553 4 Ch. Bus Monitor (non-redundant) |
| UA2401       | UA2000 with ARINC 717 (2R/2T)                              |
| UA2410       | UA2000 with ARINC 429 (1R/1T)                              |
| UA2420       | UA2000 with ARINC 429 (4R/2T)                              |
| UA2430       | UA2000 with ARINC 429 (8R/4T)                              |
| UA2431       | UA2000 with ARINC 429 (8R/4T) and ARINC 717 (2R/2T)        |
| UA2440       | UA2000 with ARINC 429 (12R/4T)                             |
| UA2710       | UA2000 with ARINC 708 (1R/1T)                              |
| UA2720       | UA2000 with ARINC 708 (2R/2T)                              |

*Table 1-1 – UA2000 Part Numbers*

<span id="page-8-2"></span>Each standard UA2000 provides discrete I/O and IRIG time synchronization.

The complete assembly model number is located on the product label. The configuration of an installed UA2000 may be determined by running the test program described in Section [2.](#page-10-0)

For future reference, we encourage you to record the model number and serial number of your UA2000. You may wish to use the space provided below:

Model No: \_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_

Serial No: \_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_

#### <span id="page-8-1"></span>**Avionics Databus Protocols**

Avionics databuses interconnect various electronic equipment (navigation, controls, displays, sensors, etc.) on an aircraft, much as a local area network (LAN) interconnects computers in an office. Data from one device is passed over the network to other devices that need it. There are several military and commercial avionics databus standards. The UA2000 supports some of the most common protocols, which are briefly described below:

- **MIL-STD-1553** is the protocol for military aircraft and other military and commercial applications. It is a digital, command-response, time-division multiplexing databus protocol.
- **ARINC 429**, one of the most prevalent ARINC (Aeronautical Radio INCorporated) standards, defines the transfer of digital data between commercial avionics systems. It uses broadcast bus topology and a label identification method for data words.
- **ARINC 708** defines an airborne pulse Doppler weather radar system for commercial aircraft. The Transmitter/Receiver unit sends data over the 708 display databus to the Control/Display Unit.

Data consists of 1600-bit words that are preceded and followed by a sync. The display databus is an adaptation of the proposed, but never approved, **ARINC 453** databus.

• **ARINC 717** is the equipment specification for the Digital Expandable Flight Data Acquisition and Recording System, which consists of a Digital Flight Data Acquisition Unit (DFDAU) and a Digital Flight Data Recorder (DFDR). The DFDAU accumulates data from the aircraft systems and transmits it to the DFDR on a Harvard biphase ARINC 717 databus. A similar bipolar ARINC 717 databus can connect the DFDAU to a Quick Access Recorder (QAR).

These and other standards are not limited to use in aircraft. They are used in many other military and industrial applications such as surface and space vehicles, process control, nuclear research, and oil exploration.

## <span id="page-9-0"></span>**Other Documentation**

Besides this manual, Astronics AES provides other documentation to facilitate operation of the UA2000.

Separate BTIDriver™ API programming manuals are available for each avionics protocol. These manuals provide information on the specific protocol and include basic and advanced programming instructions for users who intend to write their own software. They also contain a comprehensive reference for each function.

The software distribution disk accompanying the UA2000 device has example programs, drivers, and driver installation instructions for various operating systems, as well as other information, files and resources.

## <span id="page-9-1"></span>**Support and Service**

Astronics AES offers technical support before and after purchase of our products. Our hours of operation are 7:00 am to 5:00 pm Pacific Time (PT). Support and sales engineers are often available outside these hours as well. We invite your questions and comments on any of our products.

Telephone: +1.425.339.0281 Fax: +1.425.339.0915 Web: [astronics.com/](https://www.astronics.com/) Email: [Ballard.Support@astronics.com](mailto:Ballard.Support@astronics.com)

## <span id="page-9-2"></span>**Updates**

At Astronics AES, we take pride in high quality, reliable products that meet the needs of our customers. Because we are continually improving our products, we may issue periodic updates to our documentation and software. Register your product a[t www.astronics.com/BallardSupport](http://www.astronics.com/BallardSupport) for information on updates, customer service, and new products.

# <span id="page-10-0"></span>**2. Installation**

This section explains the procedures for installing a UA2000. There are four steps to installation:

[Step 1: Read the Driver Installation Instructions](#page-10-1)

[Step 2: Connect the UA2000 to the Host Computer](#page-10-2)

[Step 3: Set the Card Number and Test the Installation](#page-10-3)

[Step 4: Connect to Databus I/O](#page-11-0)

After the installation steps are completed, the UA2000 is ready to communicate on the databus(es).

#### <span id="page-10-1"></span>**Step 1: Read the Driver Installation Instructions**

Drivers allow programmatic control of the UA2000 from a host computer.

The driver installation procedures vary, depending on your computer's operating system. These procedures are kept on disk so they can be easily updated as operating systems evolve. Before proceeding with the installation, find, print, and review the driver installation procedure for your operating system.

#### **To install the driver software**:

- Insert the driver disk in your drive and browse to the folder for your product
- Print the driver installation instructions located in a README file on the software distribution disk in a folder specific to your product
- Follow the instructions from the README file

The installation procedure differs for each operating system, but in most cases, several files are copied to the host computer system and either the system registry is modified, or configuration files are created.

If you encounter problems, have installation questions, or cannot find instructions for your operating system, please contact Customer Service (see Section [1.6\)](#page-9-1).

#### <span id="page-10-2"></span>**Step 2: Connect the UA2000 to the Host Computer**

The UA2000 is bus-powered, so connecting the USB cable to the computer will power the device. When connected for the first time, both LEDs will turn on, when the driver has finished installation on the host computer, the red LED will turn off. This indicates a proper initialization of the UA2000. If the LEDs do not follow this pattern, consult the Troubleshooting Section (Section [4\)](#page-14-0).

The first time that the UA2000 is connected to the host computer, the UA2000 will be recognized as new hardware and the driver software installation will begin. Depending on the operating system, the driver software installation will either begin automatically or will require some user intervention (refer to the README file for operating system specific information).

#### <span id="page-10-3"></span>**Step 3: Set the Card Number and Test the Installation**

You must set a card number on the controlling computer for software to identify the UA2000.

Since many Ballard BTIDriver-compliant hardware devices (e.g., UA2000) can be concurrently connected to the same computer, software running on a given computer uses a unique card number to designate which hardware device is being accessed. If you have only one BTIDriver-compliant device connected to the computer, it is recommended that you set it to card number 0 because the example programs included with the driver software assume a card number of 0. After the card number has been set, you can then test the UA2000.

In Windows, the BTITST32.EXE test program can be used to assign and manage card numbers and to test the UA2000 (and other BTIDriver-compliant devices). The test program discovers all connected BTIDriver-compliant devices and displays important information about each device such as card number, configuration, serial number, and assembly part number. Running the test sequence verifies both the device hardware and the interface between the device and the computer. If the program does not detect any faults, it displays a "passed test" message.

This program and a README file with instructions are found in the Windows Test subfolder (UA2000 →WINDOWS →TEST) on the software distribution disk.

**NOTE** The Windows test program may be used at any time to determine or reassign the UA2000 card number.

If you need further assistance, contact Customer Support (see Section [1.6\)](#page-9-1).

#### <span id="page-11-0"></span>**Step 4: Connect to Databus I/O**

Connect the databuses to the UA2000 according to the pin assignment tables in Section [6.](#page-21-0) Be sure to follow the coupling and termination guidelines discussed in Section [7.](#page-40-0)

Connection of the ground pin(s) to the end system(s) is necessary for proper operation of MIL-STD-1553, ARINC 429, ARINC 717, ARINC 708, and the discrete I/O. There is no need to terminate unused signals and do not connect the reserved pin(s).

# <span id="page-12-0"></span>**3. Operation**

Now that the UA2000 has been installed it is ready for operation. A software application is used to control the device and to manipulate data. Whether using Ballard CoPilot® analyzer software or developing a custom application using Ballard BTIDriver API library, it is easy to operate the UA2000 and utilize its powerful interfaces.

### <span id="page-12-1"></span>**CoPilot**

A PC with CoPilot and the Ballard UA2000 makes a powerful, low-cost databus analyzer/simulator. CoPilot interfaces directly with the UA2000, eliminating the need to write custom software. CoPilot greatly simplifies such tasks as defining and scheduling bus messages and capturing and analyzing data. CoPilot is a Windows-based program that features a user-friendly GUI and many timesaving features. For example, bus messages can be automatically detected, posted in the hardware tree, and associated with the appropriate attributes from the database of equipment, message, and engineering unit specifications.

CoPilot users can quickly configure, run, and display the activity of multiple databuses in a unified view. Data can be observed and changed in engineering units while the bus is running. The Strip View graphically illustrates the history of the selected data values. Data can also be entered and viewed as virtual instruments (knobs, dials, gauges, etc.) that can be created by the user or automatically generated by dragging and dropping an item into the Control View.

Because CoPilot can host multiple channels and databus protocols in the same project, it is the ideal tool for operating UA2000. CoPilot can be purchased separately or with an UA2000. For more information or a free evaluation copy, please contact Customer Service (see Section [1.6\)](#page-9-1).

![](_page_12_Figure_6.jpeg)

<span id="page-12-2"></span>*Figure 3-1 – Sample CoPilot Screen*

## <span id="page-13-0"></span>**User Developed Software**

With only a few function calls, a program can operate the UA2000 and process messages to and from the avionics databuses. Functions include routines for transmitting, receiving, scheduling, recording, data manipulation, and time-tagging bus messages. Although most tasks require only a few API calls, the comprehensive API library includes a broad range of functions for specialized needs.

Example programs are included with the API on the software distribution disk. Detailed information about API functions and instructions on programming for the UA2000s is found in their corresponding API Programming Manual (i.e. MIL-STD-1553 API functions are found in the MIL-STD-1553 Programming Manual for BTIDriver-Compliant Devices). You can use this manual to learn how to create custom applications for any Ballard BTIDriver-compliant interface product.

## <span id="page-13-1"></span>**Translation Software for Older Products**

Astronics AES has developed translation software that allows applications written for older Ballard products to work with the BTIDriver API library. These translation DLLs are included on the software distribution disk, and when installed, will dynamically allow the old application to operate a UA2000 device. For further information, including installation instructions, refer to the README file distributed with the translation software.

# <span id="page-14-0"></span>**4. Troubleshooting**

This section provides information to assist in troubleshooting the UA2000. If the problem(s) cannot be resolved using the information in this section, please contact Customer Service (see Sectio[n 1.6\)](#page-9-1).

#### **Do not open the UA2000**

**WARNING** The UA2000 does not require routine maintenance or calibration, and there are no user serviceable parts inside. Opening the UA2000 may void the warranty. If the UA2000 needs repair, contact Customer Support (see Sectio[n 1.6\)](#page-9-1).

#### <span id="page-14-1"></span>**LED Indicators**

There are two LEDs on the UA2000. The green LED will illuminate when powered and will blink to indicate databus activity. The red LED is user controllable via software using the API function BTICard\_ExtStatusLEDWr. For more information on this function, consult one of the API programming manuals for the UA2000 (e.g., MIL-STD-1553 Programming Manual for BTIDriver-compliant Devices).

At power on, both LED indicators should turn on briefly, and then the red one will turn off. If this sequence does not happen, please perform the following steps:

- If both the red and green LEDs come on and stay on, verify that you have the driver software installed on your host computer.
- Check the USB cable to ensure it is properly connected.
- If using a hub, ensure that it is powered and that its power cable is properly connected.
- Try a different USB port.

#### <span id="page-14-2"></span>**UA2000 Test Program**

There are a couple options available to verify that the UA2000 is working correctly.

- **Test Program**: To test the device, run the test utility distributed on the driver disk for the specific host operating system. For example, on a Windows host computer run the BTITST32.EXE program included on the Windows Driver disk to verify the installation and hardware operation. These test utilities will display a passed or failed message and provide detailed information about the device hardware.
- **API Test Function**: The BTIDriver API includes the BTICard\_CardTest API function to verify operation of the hardware from a user developed application. It is advisable to include a call to this function at the beginning of an application. For more information on this function, consult the API programming manual for your model.

# <span id="page-15-0"></span>**5. UA2000 Features**

This section describes the capabilities available on many UA2000s. If additional information is needed than what is presented here, please contact Customer Service for assistance (see Section [1.6\)](#page-9-1).

#### <span id="page-15-1"></span>**USB 3.1**

The UA2000 uses the SuperSpeed USB 3.1, Gen 1 interface over a USB Type-C connector. This USB interface offers many benefits, including:

- SuperSpeed 5 Gbps transfer rate provides for full databus interaction
- Reversible cable plug for simplified connections
- Hot pluggable interface allows for insertion and removal of devices without host computer reboot
- Interrupt generation provides notification of Event Log information without the need to poll the device
- DMA mode of Sequential Monitor offloads host processor for bulk data transfers
- Backwards compatible support for high-speed (480 Mbps) USB 2.0 operation.

#### <span id="page-15-2"></span>**MIL-STD-1553**

Each MIL-STD-1553 channel is available in three levels of functionality (summarized in [Table 5-1\)](#page-15-4). Both the Single and Multi-function levels provide Bus Controller, Remote Terminal, or Monitor operation. Multi-function features also include concurrent BC/RT/Monitor operation and protocol error injection (word, gap, and message errors). The Bus Monitor function level provides a Monitor only operation which is hardware inhibited so that it can't transmit on the databus.

| Level Designator→        | S      | M     | BM          |
|--------------------------|--------|-------|-------------|
| BC/RT/MON Operation      | Single | Multi | Bus Monitor |
| BC or 32 RTs or MON      |       |      |            |
| BC and 32 RTs and MON    |        |      |             |
| MON Only                 |        |       |            |
| Protocol Error Injection |        |      |             |

*Table 5-1 – Ballard MIL-STD-1553 Functionality Levels*

#### <span id="page-15-4"></span><span id="page-15-3"></span>**ARINC 429**

ARINC 429 receive channels feature automatic speed detection and independent label and SDI filtering. Each transmit channel automatically maintains accurate label repetition rates. To support data transfer protocols, aperiodic words may be transmitted without altering the timing of periodic words. Both receive and transmit channels may be independently set for standard low or high-speed bit rate (12.5 or 100 Kbps). The UA2000 ARINC 429 may be a shared interface; please see the UA2000 Shared Interfaces Section [5.8](#page-20-0) for more information.

#### <span id="page-16-0"></span>*5.3.1 Configurable Frequency*

UA2000 ARINC 429 channels can be operated at non-standard speeds. This configurable frequency can be set in software for each transmit and receive channel. Thus, ARINC 429 channels may be used with equipment that varies from the ARINC 429 standard (such as some implementations of ARINC 575).

Use the BTI429\_ParamBitRateConfig API function to configure a channel for non-standard frequencies.

#### <span id="page-16-1"></span>**ARINC 717**

Each ARINC 717 channel is independently configurable for Harvard biphase or bipolar return-to-zero operation. Sub-frame data structures are used to transmit and receive from the ARINC 717 databus. In addition, super-frames can be enabled with configurable counter location and number of frames per super-frame.

UA2000s with ARINC 717 include two transmit and two receive channels. Each channel may be software configured for either biphase or bipolar signaling. The UA2000 ARINC 717 is a shared interface; please see the UA2000 Shared Interfaces Section [5.8](#page-20-0) for more information.

**NOTE** ARINC 717 biphase transmit channels do not meet all signal characteristics described in the ARINC 717 specification. The biphase receive channels remain compliant.

# <span id="page-16-2"></span>**ARINC 708**

ARINC 708 channels communicate on the airborne pulse Doppler weather radar display databus. Each channel can be independently switched to operate on either of two buses. ARINC 708 databuses use direct coupling and should be properly terminated at each end of the databus. See Section [7](#page-40-0) for more information about bus termination and direct coupling.

ARINC 708 channels can support messages with a user-defined number of bits. This variable bit length mode is software-selected with channel configuration functions. Other functions are provided in the BTIDriver API to read and write messages with a bit count of 1 to 1856 (116 x 16). This allows ARINC 708 channels to be used with other transfer protocols that vary from the standard (1600-bit word) display databus.

## <span id="page-16-3"></span>**Avionics Shunt Discrete I/O, Syncs, and Triggers**

The UA2000 has avionics shunt discretes which can be used to signal and detect events, determine status, and to drive loads. Shunt discretes may also be used as general-purpose digital I/O and have a wide range of avionics and general-purpose applications. Each avionics shunt discrete I/O pin is configured as both a shunt input and a shunt output.

#### <span id="page-16-4"></span>*5.6.1 Shunt Inputs*

A shunt input circuit pin is pulled up to a voltage source through a resistor. A load resistance applied between the pin and ground will shunt current from the source and generate a voltage at the pin. The pin voltage is compared with a reference voltage for input state detection. There are two defined states: the "Open" state in which a high impedance is applied to the pin, and the "Ground" state in which a low impedance is applied to the pin.

The UA2000 discrete shunt input circuit, illustrated in [Figure 5-1](#page-17-2), has a 9 kΩ pull-up resistor to a 5-volt source. An isolation diode provides protection against over-voltage at the pin. A load resistance connected between the input pin and ground will shunt current from the 5-volt source, through the forward biased diode and the 9 kΩ resistor. A series resistor limits current as a voltage is generated across the load which is compared to a reference voltage produced by an identical configuration. This results in a 3.25 volt switching voltage.

![](_page_17_Figure_3.jpeg)

*Figure 5-1 – UA2000 Discrete Shunt Input Circuit*

#### <span id="page-17-2"></span><span id="page-17-0"></span>*5.6.2 Shunt Input Considerations*

**Limits**: The UA2000 discrete shunt inputs can withstand up to 35 VDC applied to the pin. The discrete inputs are capable of interfacing with industry standard avionics discrete signals.

**Usage**: Some UA2000 discrete shunt inputs can be configured as input triggers using the Ballard BTIDriver API. Once the input has been configured as a trigger it may no longer be used as a discrete input. Refer to Sectio[n 5.6.5](#page-18-1) for additional information.

#### <span id="page-17-1"></span>*5.6.3 Shunt Outputs*

A shunt output is typically an open-collector circuit and is normally high impedance. When driven, the output sinks current to ground in a low impedance state. Shunt outputs can be used to communicate with an input discrete and/or to energize a load.

The UA2000 discrete shunt output circuit, illustrated in [Figure 5-2,](#page-18-2) is a low side switch capable of sinking up to 200 mA of current to ground through the external load (Zext). A diode protection circuit permits safe switching of inductive loads. Over-load detection limits the sink current and shuts the device down in an over-temperature condition. Each output circuit is wired in parallel with an input circuit (se[e Figure 5-1\)](#page-17-2) providing self-monitor capabilities.

![](_page_18_Figure_2.jpeg)

*Figure 5-2 – UA2000 Discrete Shunt Output Circuit*

#### <span id="page-18-2"></span><span id="page-18-0"></span>*5.6.4 Shunt Output Considerations*

**Limits**: The UA2000 discrete shunt outputs are open-ground switches capable of sinking up to 200mA. The discrete outputs can withstand up to 35 VDC and are capable of interfacing with industry standard avionics discrete signals.

**Usage**: Some UA2000 discrete shunt outputs can be configured as output syncs using the Ballard BTIDriver API. Once the output has been configured as a sync it may no longer be used as a discrete output. Refer to Section [5.6.5](#page-18-1) for additional information.

**Self-Monitor**: The UA2000 discrete shunt output circuits can be monitored by corresponding UA2000 discrete input circuits. Writing to a discrete can drive an enabled output and reading from that discrete will report the current state of the input. Once the output is driven, there is a finite period of time before the change of state on the corresponding input is detected. This delay (approximately 30 µs) is due to the latency of the host system and the analog delay of the input and output circuitry.

**Over-Load/Fault Reporting**: The UA2000 discrete shunt output circuits contain current limiting and thermal shutdown features. If a user attempts to sink too much current through an output discrete circuit, the output will begin current limiting. This is accomplished by increasing the resistance through the output, which causes the power dissipation and therefore the temperature to increase. The output continues to limit the current until the thermal limit is reached and then the output is automatically shut down. Once an output is shut down due to a fault, the output remains disabled until both the fault is cleared, and the user drives the output again. For this reason, it is important that the user corrects fault conditions before attempting to drive the output.

**High Current Drive**: Each shunt output is capable of sinking up to 200 mA of current. However, the user can wire multiple outputs in parallel to increase the maximum current sinking capability.

**Power-On**: After power-on, the shunt discrete I/O is in its default state with outputs open (high impedance).

#### <span id="page-18-1"></span>*5.6.5 Shunt Discrete Input/Output Usage*

The UA2000 shunt discrete inputs and outputs can be configured for additional uses as syncs and triggers. More than one discrete, each with an individually specified polarity, may be used in combination to define a sync or trigger state. For instance, a trigger may be defined as a particular state of only one input, or it may be defined as a particular combination of two or three inputs. Other triggers and syncs may use the same or different combinations of these lines.

Processes that are configured to be triggered by an external trigger can be triggered through software using the BTICard\_CardTriggerEx function. This is useful for software testing and does not require external trigger equipment. Refer to the BTIDriver software manuals for more information on programming these discretes and their use as syncs and triggers.

The Ballard BTIDriver API provides functions to read and write the discretes. The parameter dionum in the API functions (BTICard\_ExtDIO-Rd and BTICard\_ExtDIOWr) specifies which discrete to read or write. When DIOn (where n is a number) is described in other functions, it is synonymous with dionum (i.e., n = dionum).

[Table 5-2](#page-19-1) below shows the correlation between dionum and its hardware reference designator. The last column shows how these discretes are used as trigger inputs and sync outputs in the BTIDriver API functions.

| Hardware Reference | API dionum | API usage |
|--------------------|------------|-----------|
| DIO0               | 1          | Trigger A |
| DIO1               | 2          | Trigger B |
| DIO2               | 3          | Trigger C |
| DIO3               | 4          | -         |
| DIO4               | 9          | Sync A    |
| DIO5               | 10         | Sync B    |
| DIO6               | 11         | Sync C    |
| DIO7               | 12         | -         |

*Table 5-2 – Hardware Versus Software Designation of Shunt Discrete I/O*

## <span id="page-19-1"></span><span id="page-19-0"></span>**IRIG Timer**

An IRIG time signal contains a human-readable binary coded decimal (BCD) time value in days, hours, minutes, seconds, etc. and can be used to synchronize many devices. This allows timing data from all compatible devices to be easily correlated.

The UA2000 IRIG circuit can be configured as either a master or a slave. The IRIG timer pin is driven by the bidirectional buffer only when the IRIG timer is configured as a master. When the IRIG timer is configured as a slave, it will expect the IRIG signal to come from an external device.

The UA2000 internally uses a binary system timer that is free running and keeps time until either set by software or synchronized to an IRIG signal when configured as a slave. This system timer is also the source for the IRIG interface when configured as a master. The system timer has a resolution down to one microsecond.

| NOTE |
|------|
|------|

IRIG data can be encoded using Pulse Code Modulation (PCM), Modified Manchester Modulation, or Amplitude Modulation (AM). The UA2000 supports PCM master, PCM slave, or AM slave modulation modes.

There are several formats for IRIG timing. The UA2000 uses the IRIG formats indicated i[n Table 5-3](#page-20-2) and [Table 5-4.](#page-20-3) The characteristics of the external electrical interface to the IRIG pins are as shown in [Table 5-5.](#page-20-4)

| Format               | A<br>B     |   | 1000 pps                           |                     |
|----------------------|------------|---|------------------------------------|---------------------|
|                      |            |   | 100 pps                            |                     |
| Modulation Frequency | 0          | 1 | Pulse width coded                  | Amplitude modulated |
| Frequency/Resolution | 0          |   | No carrier/index count interval    |                     |
|                      |            | 2 |                                    | 1 kHz/1 ms (B only) |
|                      |            | 3 |                                    | 10 kHz/.1 ms        |
|                      |            | 4 |                                    | 100 kHz/10 ms       |
|                      |            | 5 |                                    | 1 MHz/1 ms          |
| Coded Expressions    | 0, 1, 2, 3 |   | Slave: Uses only BCD field (input) |                     |

*Table 5-3 – Input IRIG formats used by UA2000*

<span id="page-20-2"></span>

|                      | A | 1000 pps                        |  |
|----------------------|---|---------------------------------|--|
| Format               | B | 100 pps                         |  |
| Modulation Frequency | 0 | Pulse width coded               |  |
| Frequency/Resolution | 0 | No carrier/index count interval |  |
| Coded Expressions    | 2 | Master: BCD (output)            |  |

*Table 5-4 – Output IRIG formats used by UA2000*

<span id="page-20-3"></span>

| Item                    | Value                      |
|-------------------------|----------------------------|
| Input impedance (min)   | 96 kΩ                      |
| Input level             | -11.7 V to 15 V            |
| Input level threshold   | API Controlled (0 V-3.3 V) |
| Output level            | 0 to 3.3 V                 |
| Output drive capability | 20 mA                      |

*Table 5-5 – Electrical characteristics of UA2000 IRIG signals*

<span id="page-20-4"></span>For information on configuring and using the IRIG timer consult the BTIDriver API programming manuals.

## <span id="page-20-0"></span>**UA2000 Shared Interfaces**

Some UA2000s have channels with interface hardware that supports multiple protocols. These protocols share pins and can be configured through the API.

#### <span id="page-20-1"></span>*5.8.1 ARINC 429/ARINC 717 Bi-Polar*

When configured to use ARINC 717 bipolar Tx/Rx signaling, the channel may share transceiver circuitry with an ARINC 429 channel depending on the model. Until an ARINC 717 channel is configured for bipolar signaling, the shared transceiver circuitry defaults to ARINC 429 operation. Refer to Section [6](#page-21-0) for which pins are shared for a specific model

# <span id="page-21-0"></span>**6. Connector Information**

The UA2000 has a USB host connector and a 44 pin databus I/O connector. This section provides the information needed to connect to the general-purpose (including triggers, syncs, and discretes) and protocol-specific signals. Mating connector part numbers are included below; however, for convenience Astronics AES provides a mating connector and back shell with each UA2000 product except for the /NE order option.

#### <span id="page-21-1"></span>**USB Connector**

The UA2000 has a standard USB-C style connector which can accept any standard USB-C cable for connection to a host computer. The UA2000 has been designed with a special screw-lock retention feature for the USB cable. Included with the product are a USB-C to USB-C cable and a USB-C to USB-A cable. Both cables include thumb screws to secure the cable to the device. If using a different cable (or extending the supplied cables), the length of the USB cable should not exceed 6.6 feet (2m) to maintain USB 3.1 Gen 1 compatibility. These cables are not included with the /NE option.

![](_page_21_Picture_4.jpeg)

*Figure 6-1 – USB-C Cable with Screw-Lock Feature*

# <span id="page-21-3"></span><span id="page-21-2"></span>**Databus I/O Connector**

The databus I/O connector is a 44 pin D-Sub receptacle (Amphenol ICD44S13E6GX00LF) which is shown in [Figure](#page-21-4) 6-2. The recommended mating connector is a 44 pin D-Sub plug (Norcomp 180-044- 103L001) and metal back shell (Norcomp 979-025-030R121). A mating connector and back shell are included with each UA2000 (except for the /NE order option) to facilitate making custom cables for the device.

<span id="page-21-4"></span>![](_page_21_Picture_8.jpeg)

*Figure 6-2 – Databus I/O Pin Numbers*

#### <span id="page-22-0"></span>**Databus I/O Connector Pin-outs**

UA2000 pin assignments are listed in model specific sections below. Be sure to follow the coupling and termination guidelines provided in Section [7.](#page-40-0)

| Name   | Description                                     | Pin                                                                                   |
|--------|-------------------------------------------------|---------------------------------------------------------------------------------------|
| IRIG   | IRIG Signal                                     | 16                                                                                    |
| DIO00  | Discrete I/O 00                                 | 39                                                                                    |
| DIO01  | Discrete I/O 01                                 | 40                                                                                    |
| DIO02  | Discrete I/O 02                                 | 41                                                                                    |
| DIO03  | Discrete I/O 03                                 | 42                                                                                    |
| DIO04  | Discrete I/O 04                                 | 43                                                                                    |
| DIO05  | Discrete I/O 05                                 | 44                                                                                    |
| DIO06  | Discrete I/O 06                                 | 30                                                                                    |
| DIO07  | Discrete I/O 07                                 | 15                                                                                    |
| CH0AX  | CH0 BUS A transformer coupled (+)               | 2                                                                                     |
| CH0AXR | CH0 BUS A transformer coupled (−)               | 17                                                                                    |
| CH0AD  | CH0 BUS A direct coupled (+)                    | 3                                                                                     |
| CH0ADR | CH0 BUS A direct coupled (−)                    | 18                                                                                    |
| CH0AT  | CH0 BUS A direct coupled termination jumper (+) | 6                                                                                     |
| CH0ATR | CH0 BUS A direct coupled termination jumper (−) | 21                                                                                    |
| CH0BX  | CH0 BUS B transformer coupled (+)               | 5                                                                                     |
| CH0BXR | CH0 BUS B transformer coupled (−)               | 20                                                                                    |
| CH0BD  | CH0 BUS B direct coupled (+)                    | 4                                                                                     |
| CH0BDR | CH0 BUS B direct coupled (−)                    | 19                                                                                    |
| CH0BT  | CH0 BUS B direct coupled termination jumper (+) | 7                                                                                     |
| CH0BTR | CH0 BUS B direct coupled termination jumper (−) | 22                                                                                    |
| GND    | Ground                                          | 1, 14, 29                                                                             |
| RSVD   | Reserved                                        | 8, 9, 10, 11, 12, 13,<br>23, 24, 25, 26, 27, 28,<br>31, 32, 33, 34, 35, 36,<br>37, 38 |

## <span id="page-22-1"></span>*6.3.1 Model UA2110, UA2120, and UA2130*

*Table 6-1 – Model UA2110, UA2120, and UA2130 Pinout*

<span id="page-22-2"></span>

| NOTE | To enable on-board direct coupled termination, complete the connection between the<br>positive and negative ends of the termination jumper (e.g., CH0AT and CH0ATR). |
|------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|      |                                                                                                                                                                      |
| NOTE | Connection of the ground pin(s) to the end system(s) is necessary for proper operation<br>of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.                      |

| Name   | Description                                     | Pin                               |
|--------|-------------------------------------------------|-----------------------------------|
| IRIG   | IRIG Signal                                     | 16                                |
| DIO00  | Discrete I/O 00                                 | 39                                |
| DIO01  | Discrete I/O 01                                 | 40                                |
| DIO02  | Discrete I/O 02                                 | 41                                |
| DIO03  | Discrete I/O 03                                 | 42                                |
| DIO04  | Discrete I/O 04                                 | 43                                |
| DIO05  | Discrete I/O 05                                 | 44                                |
| DIO06  | Discrete I/O 06                                 | 30                                |
| DIO07  | Discrete I/O 07                                 | 15                                |
| CH0AX  | CH0 BUS A transformer coupled (+)               | 2                                 |
| CH0AXR | CH0 BUS A transformer coupled (−)               | 17                                |
| CH0AD  | CH0 BUS A direct coupled (+)                    | 3                                 |
| CH0ADR | CH0 BUS A direct coupled (−)                    | 18                                |
| CH0AT  | CH0 BUS A direct coupled termination jumper (+) | 6                                 |
| CH0ATR | CH0 BUS A direct coupled termination jumper (−) | 21                                |
| CH0BX  | CH0 BUS B transformer coupled (+)               | 5                                 |
| CH0BXR | CH0 BUS B transformer coupled (−)               | 20                                |
| CH0BD  | CH0 BUS B direct coupled (+)                    | 4                                 |
| CH0BDR | CH0 BUS B direct coupled (−)                    | 19                                |
| CH0BT  | CH0 BUS B direct coupled termination jumper (+) | 7                                 |
| CH0BTR | CH0 BUS B direct coupled termination jumper (−) | 22                                |
| CH1AX  | CH1 BUS A transformer coupled (+)               | 10                                |
| CH1AXR | CH1 BUS A transformer coupled (−)               | 25                                |
| CH1AD  | CH1 BUS A direct coupled (+)                    | 11                                |
| CH1ADR | CH1 BUS A direct coupled (−)                    | 26                                |
| CH1AT  | CH1 BUS A direct coupled termination jumper (+) | 8                                 |
| CH1ATR | CH1 BUS A direct coupled termination jumper (−) | 23                                |
| CH1BX  | CH1 BUS B transformer coupled (+)               | 13                                |
| CH1BXR | CH1 BUS B transformer coupled (−)               | 28                                |
| CH1BD  | CH1 BUS B direct coupled (+)                    | 12                                |
| CH1BDR | CH1 BUS B direct coupled (−)                    | 27                                |
| CH1BT  | CH1 BUS B direct coupled termination jumper (+) | 9                                 |
| CH1BTR | CH1 BUS B direct coupled termination jumper (−) | 24                                |
| GND    | Ground                                          | 1, 14, 29                         |
| RSVD   | Reserved                                        | 31, 32, 33, 34, 35,<br>36, 37, 38 |

#### <span id="page-23-0"></span>*6.3.2 Model UA2111, UA2121, UA2122, UA2131, and UA2133*

<span id="page-23-1"></span>*Table 6-2 – Model UA2111, UA2121, UA2122, UA2131, and UA2133 Pinout*

| NOTE | To enable on-board direct coupled termination, complete the connection between the<br>positive and negative ends of the termination jumper (e.g., CH0AT and CH0ATR). |
|------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|      |                                                                                                                                                                      |
| NOTE | Connection of the ground pin(s) to the end system(s) is necessary for proper operation<br>of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.                      |

#### <span id="page-24-0"></span>*6.3.3 Model UA2140*

| Name  | Description                               | Pin |
|-------|-------------------------------------------|-----|
| IRIG  | IRIG Signal                               | 16  |
| DIO00 | Discrete I/O 00                           | 39  |
| DIO01 | Discrete I/O 01                           | 40  |
| DIO02 | Discrete I/O 02                           | 41  |
| DIO03 | Discrete I/O 03                           | 42  |
| DIO04 | Discrete I/O 04                           | 43  |
| DIO05 | Discrete I/O 05                           | 44  |
| DIO06 | Discrete I/O 06                           | 30  |
| DIO07 | Discrete I/O 07                           | 15  |
| CH0X  | CH0 transformer coupled (+)               | 2   |
| CH0XR | CH0 transformer coupled (−)               | 17  |
| CH0D  | CH0 direct coupled (+)                    | 3   |
| CH0DR | CH0 direct coupled (−)                    | 18  |
| CH0T  | CH0 direct coupled termination jumper (+) | 6   |
| CH0TR | CH0 direct coupled termination jumper (−) | 21  |
| CH1X  | CH1 transformer coupled (+)               | 5   |
| CH1XR | CH1 transformer coupled (−)               | 20  |
| CH1D  | CH1 direct coupled (+)                    | 4   |
| CH1DR | CH1 direct coupled (−)                    | 19  |
| CH1T  | CH1 direct coupled termination jumper (+) | 7   |
| CH1TR | CH1 direct coupled termination jumper (−) | 22  |
| CH2X  | CH2 transformer coupled (+)               | 10  |
| CH2XR | CH2 transformer coupled (−)               | 25  |
| CH2D  | CH2 direct coupled (+)                    | 11  |
| CH2DR | CH2 direct coupled (−)                    | 26  |
| CH2T  | CH2 direct coupled termination jumper (+) | 8   |
| CH2TR | CH2 direct coupled termination jumper (−) | 23  |
| CH3X  | CH3 transformer coupled (+)               | 13  |
| CH3XR | CH3 transformer coupled (−)               | 28  |
| CH3D  | CH3 direct coupled (+)                    | 12  |

| Name  | Description                               | Pin                            |
|-------|-------------------------------------------|--------------------------------|
| CH3DR | CH3 direct coupled (−)                    | 27                             |
| CH3T  | CH3 direct coupled termination jumper (+) | 9                              |
| CH3TR | CH3 direct coupled termination jumper (−) | 24                             |
| GND   | Ground                                    | 1, 14, 29                      |
| RSVD  | Reserved                                  | 31, 32, 33, 34, 35, 36, 37, 38 |

*Table 6-3 – Model UA2140 Pinout*

<span id="page-25-0"></span>

| NOTE | To enable on-board direct coupled termination, complete the connection between the<br>positive and negative ends of the termination jumper (e.g., CH0T and CH0TR). |
|------|--------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| NOTE | Connection of the ground pin(s) to the end system(s) is necessary for proper operation<br>of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.                    |

#### <span id="page-26-0"></span>*6.3.4 Model UA2401*

| Name  | Description                    | Pin                                                         |  |  |
|-------|--------------------------------|-------------------------------------------------------------|--|--|
| IRIG  | IRIG Signal                    | 16                                                          |  |  |
| DIO00 | Discrete I/O 00                | 39                                                          |  |  |
| DIO01 | Discrete I/O 01                | 40                                                          |  |  |
| DIO02 | Discrete I/O 02                | 41                                                          |  |  |
| DIO03 | Discrete I/O 03                | 42                                                          |  |  |
| DIO04 | Discrete I/O 04                | 43                                                          |  |  |
| DIO05 | Discrete I/O 05                | 44                                                          |  |  |
| DIO06 | Discrete I/O 06                | 30                                                          |  |  |
| DIO07 | Discrete I/O 07                | 15                                                          |  |  |
| CH08P | ARINC 717 RCV BIPHASE CH08 (+) | 10                                                          |  |  |
| CH08N | ARINC 717 RCV BIPHASE CH08 (−) | 25                                                          |  |  |
| CH08P | ARINC 717 RCV BIPOLAR CH08 (+) | 8                                                           |  |  |
| CH08N | ARINC 717 RCV BIPOLAR CH08 (−) | 23                                                          |  |  |
| CH09P | ARINC 717 XMT BIPHASE CH09 (+) | 11                                                          |  |  |
| CH09N | ARINC 717 XMT BIPHASE CH09 (−) | 26                                                          |  |  |
| CH09P | ARINC 717 XMT BIPOLAR CH09 (+) | 35                                                          |  |  |
| CH09N | ARINC 717 XMT BIPOLAR CH09 (−) | 36                                                          |  |  |
| CH10P | ARINC 717 RCV BIPHASE CH10 (+) | 12                                                          |  |  |
| CH10N | ARINC 717 RCV BIPHASE CH10 (−) | 27                                                          |  |  |
| CH10P | ARINC 717 RCV BIPOLAR CH10 (+) | 9                                                           |  |  |
| CH10N | ARINC 717 RCV BIPOLAR CH10 (−) | 24                                                          |  |  |
| CH11P | ARINC 717 XMT BIPHASE CH11 (+) | 13                                                          |  |  |
| CH11N | ARINC 717 XMT BIPHASE CH11 (−) | 28                                                          |  |  |
| CH11P | ARINC 717 XMT BIPOLAR CH11 (+) | 37                                                          |  |  |
| CH11N | ARINC 717 XMT BIPOLAR CH11 (−) | 38                                                          |  |  |
| GND   | Ground                         | 1, 14, 29                                                   |  |  |
| RSVD  | Reserved                       | 2, 3, 4, 5, 6, 7, 17, 18, 19, 20,<br>21, 22, 31, 32, 33, 34 |  |  |

*Table 6-4 – Model UA2401 Pinout*

<span id="page-26-1"></span>

| NOTE | Connection of the ground pin(s) to the end system(s) is necessary for proper operation<br>of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708. |
|------|-------------------------------------------------------------------------------------------------------------------------------------------------|
|------|-------------------------------------------------------------------------------------------------------------------------------------------------|

#### <span id="page-27-0"></span>*6.3.5 Model UA2410*

| Name  | Description            | Pin                                                                                                           |
|-------|------------------------|---------------------------------------------------------------------------------------------------------------|
| IRIG  | IRIG Signal            | 16                                                                                                            |
| DIO00 | Discrete I/O 00        | 39                                                                                                            |
| DIO01 | Discrete I/O 01        | 40                                                                                                            |
| DIO02 | Discrete I/O 02        | 41                                                                                                            |
| DIO03 | Discrete I/O 03        | 42                                                                                                            |
| DIO04 | Discrete I/O 04        | 43                                                                                                            |
| DIO05 | Discrete I/O 05        | 44                                                                                                            |
| DIO06 | Discrete I/O 06        | 30                                                                                                            |
| DIO07 | Discrete I/O 07        | 15                                                                                                            |
| CH00P | ARINC 429 RCV CH00 (+) | 2                                                                                                             |
| CH00N | ARINC 429 RCV CH00 (−) | 17                                                                                                            |
| CH12P | ARINC 429 XMT CH12 (+) | 31                                                                                                            |
| CH12N | ARINC 429 XMT CH12 (−) | 32                                                                                                            |
| GND   | Ground                 | 1, 14, 29                                                                                                     |
| RSVD  | Reserved               | 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 18,<br>19, 20, 21, 22, 23, 24, 25, 26, 27, 28,<br>33, 34, 35, 36, 37, 38 |

*Table 6-5 – Model UA2410 Pinout*

<span id="page-27-1"></span>![](_page_27_Picture_5.jpeg)

**NOTE** Connection of the ground pin(s) to the end system(s) is necessary for proper operation of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.

#### <span id="page-28-0"></span>*6.3.6 Model UA2420*

| Name  | Description            | Pin                                                                              |  |
|-------|------------------------|----------------------------------------------------------------------------------|--|
| IRIG  | IRIG Signal            | 16                                                                               |  |
| DIO00 | Discrete I/O 00        | 39                                                                               |  |
| DIO01 | Discrete I/O 01        | 40                                                                               |  |
| DIO02 | Discrete I/O 02        | 41                                                                               |  |
| DIO03 | Discrete I/O 03        | 42                                                                               |  |
| DIO04 | Discrete I/O 04        | 43                                                                               |  |
| DIO05 | Discrete I/O 05        | 44                                                                               |  |
| DIO06 | Discrete I/O 06        | 30                                                                               |  |
| DIO07 | Discrete I/O 07        | 15                                                                               |  |
| CH00P | ARINC 429 RCV CH00 (+) | 2                                                                                |  |
| CH00N | ARINC 429 RCV CH00 (−) | 17                                                                               |  |
| CH01P | ARINC 429 RCV CH01 (+) | 3                                                                                |  |
| CH01N | ARINC 429 RCV CH01 (−) | 18                                                                               |  |
| CH02P | ARINC 429 RCV CH02 (+) | 4                                                                                |  |
| CH02N | ARINC 429 RCV CH02 (−) | 19                                                                               |  |
| CH03P | ARINC 429 RCV CH03 (+) | 5                                                                                |  |
| CH03N | ARINC 429 RCV CH03 (−) | 20                                                                               |  |
| CH12P | ARINC 429 XMT CH12 (+) | 31                                                                               |  |
| CH12N | ARINC 429 XMT CH12 (−) | 32                                                                               |  |
| CH13P | ARINC 429 XMT CH13 (+) | 33                                                                               |  |
| CH13N | ARINC 429 XMT CH13 (−) | 34                                                                               |  |
| GND   | Ground                 | 1, 14, 29                                                                        |  |
| RSVD  | Reserved               | 6, 7, 8, 9, 10, 11, 12, 13,<br>21, 22, 23, 24, 25, 26, 27, 28,<br>35, 36, 37, 38 |  |

*Table 6-6 – Model UA2420 Pinout*

<span id="page-28-1"></span>**NOTE** Connection of the ground pin(s) to the end system(s) is necessary for proper operation of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.

#### <span id="page-29-0"></span>*6.3.7 Model UA2430*

| Name  | Description            | Pin                            |  |
|-------|------------------------|--------------------------------|--|
| IRIG  | IRIG Signal            | 16                             |  |
| DIO00 | Discrete I/O 00        | 39                             |  |
| DIO01 | Discrete I/O 01        | 40                             |  |
| DIO02 | Discrete I/O 02        | 41                             |  |
| DIO03 | Discrete I/O 03        | 42                             |  |
| DIO04 | Discrete I/O 04        | 43                             |  |
| DIO05 | Discrete I/O 05        | 44                             |  |
| DIO06 | Discrete I/O 06        | 30                             |  |
| DIO07 | Discrete I/O 07        | 15                             |  |
| CH00P | ARINC 429 RCV CH00 (+) | 2                              |  |
| CH00N | ARINC 429 RCV CH00 (−) | 17                             |  |
| CH01P | ARINC 429 RCV CH01 (+) | 3                              |  |
| CH01N | ARINC 429 RCV CH01 (−) | 18                             |  |
| CH02P | ARINC 429 RCV CH02 (+) | 4                              |  |
| CH02N | ARINC 429 RCV CH02 (−) | 19                             |  |
| CH03P | ARINC 429 RCV CH03 (+) | 5                              |  |
| CH03N | ARINC 429 RCV CH03 (−) | 20                             |  |
| CH04P | ARINC 429 RCV CH04 (+) | 6                              |  |
| CH04N | ARINC 429 RCV CH04 (−) | 21                             |  |
| CH05P | ARINC 429 RCV CH05 (+) | 7                              |  |
| CH05N | ARINC 429 RCV CH05 (−) | 22                             |  |
| CH06P | ARINC 429 RCV CH06 (+) | 8                              |  |
| CH06N | ARINC 429 RCV CH06 (−) | 23                             |  |
| CH07P | ARINC 429 RCV CH07 (+) | 9                              |  |
| CH07N | ARINC 429 RCV CH07 (−) | 24                             |  |
| CH12P | ARINC 429 XMT CH12 (+) | 31                             |  |
| CH12N | ARINC 429 XMT CH12 (−) | 32                             |  |
| CH13P | ARINC 429 XMT CH13 (+) | 33                             |  |
| CH13N | ARINC 429 XMT CH13 (−) | 34                             |  |
| CH14P | ARINC 429 XMT CH14 (+) | 35                             |  |
| CH14N | ARINC 429 XMT CH14 (−) | 36                             |  |
| CH15P | ARINC 429 XMT CH15 (+) | 37                             |  |
| CH15N | ARINC 429 XMT CH15 (−) | 38                             |  |
| GND   | Ground                 | 1, 14, 29                      |  |
| RSVD  | Reserved               | 10, 11, 12, 13, 25, 26, 27, 28 |  |

|  |  |  | Table 6-7 – Model UA2430 Pinout |  |
|--|--|--|---------------------------------|--|
|--|--|--|---------------------------------|--|

<span id="page-29-1"></span>**NOTE** Connection of the ground pin(s) to the end system(s) is necessary for proper operation of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.

#### <span id="page-30-0"></span>*6.3.8 Model UA2431*

| Name  | Description                     | Pin |
|-------|---------------------------------|-----|
| IRIG  | IRIG Signal                     | 16  |
| DIO00 | Discrete I/O 00                 | 39  |
| DIO01 | Discrete I/O 01                 | 40  |
| DIO02 | Discrete I/O 02                 | 41  |
| DIO03 | Discrete I/O 03                 | 42  |
| DIO04 | Discrete I/O 04                 | 43  |
| DIO05 | Discrete I/O 05                 | 44  |
| DIO06 | Discrete I/O 06                 | 30  |
| DIO07 | Discrete I/O 07                 | 15  |
| CH00P | ARINC 429 RCV CH00 (+)          | 2   |
| CH00N | ARINC 429 RCV CH00 (−)          | 17  |
| CH01P | ARINC 429 RCV CH01 (+)          | 3   |
| CH01N | ARINC 429 RCV CH01 (−)          | 18  |
| CH02P | ARINC 429 RCV CH02 (+)          | 4   |
| CH02N | ARINC 429 RCV CH02 (−)          | 19  |
| CH03P | ARINC 429 RCV CH03 (+)          | 5   |
| CH03N | ARINC 429 RCV CH03 (−)          | 20  |
| CH04P | ARINC 429 RCV CH04 (+)          | 6   |
| CH04N | ARINC 429 RCV CH04 (−)          | 21  |
| CH05P | ARINC 429 RCV CH05 (+)          | 7   |
| CH05N | ARINC 429 RCV CH05 (−)          | 22  |
| CH06P | ARINC 429 RCV CH06 (+)*         | 8   |
| CH06N | ARINC 429 RCV CH06 (−)*         | 23  |
| CH07P | ARINC 429 RCV CH07 (+)*         | 9   |
| CH07N | ARINC 429 RCV CH07 (−)*         | 24  |
| CH08P | ARINC 717 RCV BIPHASE CH08 (+)  | 10  |
| CH08N | ARINC 717 RCV BIPHASE CH08 (−)  | 25  |
| CH08P | ARINC 717 RCV BIPOLAR CH08 (+)* | 8   |
| CH08N | ARINC 717 RCV BIPOLAR CH08 (−)* | 23  |
| CH09P | ARINC 717 XMT BIPHASE CH09 (+)  | 11  |
| CH09N | ARINC 717 XMT BIPHASE CH09 (−)  | 26  |
| CH09P | ARINC 717 XMT BIPOLAR CH09 (+)* | 35  |
| CH09N | ARINC 717 XMT BIPOLAR CH09 (−)* | 36  |
| CH10P | ARINC 717 RCV BIPHASE CH10 (+)  | 12  |
| CH10N | ARINC 717 RCV BIPHASE CH10 (−)  | 27  |
| CH10P | ARINC 717 RCV BIPOLAR CH10 (+)* | 9   |
| CH10N | ARINC 717 RCV BIPOLAR CH10 (−)* | 24  |
| CH11P | ARINC 717 XMT BIPHASE CH11 (+)  | 13  |

| Name  | Description                     | Pin       |
|-------|---------------------------------|-----------|
| CH11N | ARINC 717 XMT BIPHASE CH11 (−)  | 28        |
| CH11P | ARINC 717 XMT BIPOLAR CH11 (+)* | 37        |
| CH11N | ARINC 717 XMT BIPOLAR CH11 (−)* | 38        |
| CH12P | ARINC 429 XMT CH12 (+)          | 31        |
| CH12N | ARINC 429 XMT CH12 (−)          | 32        |
| CH13P | ARINC 429 XMT CH13 (+)          | 33        |
| CH13N | ARINC 429 XMT CH13 (−)          | 34        |
| CH14P | ARINC 429 XMT CH14 (+)*         | 35        |
| CH14N | ARINC 429 XMT CH14 (−)*         | 36        |
| CH15P | ARINC 429 XMT CH15 (+)*         | 37        |
| CH15N | ARINC 429 XMT CH15 (−)*         | 38        |
| GND   | Ground                          | 1, 14, 29 |

*Table 6-8 – Model UA2431 Pinout*

<span id="page-31-0"></span>

| NOTE | Pins designated by a * are shared pins and the functionality is set via software<br>configuration. Please refer to Section 5.8 for more information. |
|------|------------------------------------------------------------------------------------------------------------------------------------------------------|
| NOTE | Connection of the ground pin(s) to the end system(s) is necessary for proper operation<br>of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.      |

#### <span id="page-32-0"></span>*6.3.9 Model UA2440*

| Name  | Description            | Pin |
|-------|------------------------|-----|
| IRIG  | IRIG Signal            | 16  |
| DIO00 | Discrete I/O 00        | 39  |
| DIO01 | Discrete I/O 01        | 40  |
| DIO02 | Discrete I/O 02        | 41  |
| DIO03 | Discrete I/O 03        | 42  |
| DIO04 | Discrete I/O 04        | 43  |
| DIO05 | Discrete I/O 05        | 44  |
| DIO06 | Discrete I/O 06        | 30  |
| DIO07 | Discrete I/O 07        | 15  |
| CH00P | ARINC 429 RCV CH00 (+) | 2   |
| CH00N | ARINC 429 RCV CH00 (−) | 17  |
| CH01P | ARINC 429 RCV CH01 (+) | 3   |
| CH01N | ARINC 429 RCV CH01 (−) | 18  |
| CH02P | ARINC 429 RCV CH02 (+) | 4   |
| CH02N | ARINC 429 RCV CH02 (−) | 19  |
| CH03P | ARINC 429 RCV CH03 (+) | 5   |
| CH03N | ARINC 429 RCV CH03 (−) | 20  |
| CH04P | ARINC 429 RCV CH04 (+) | 6   |
| CH04N | ARINC 429 RCV CH04 (−) | 21  |
| CH05P | ARINC 429 RCV CH05 (+) | 7   |
| CH05N | ARINC 429 RCV CH05 (−) | 22  |
| CH06P | ARINC 429 RCV CH06 (+) | 8   |
| CH06N | ARINC 429 RCV CH06 (−) | 23  |
| CH07P | ARINC 429 RCV CH07 (+) | 9   |
| CH07N | ARINC 429 RCV CH07 (−) | 24  |
| CH08P | ARINC 429 RCV CH08 (+) | 10  |
| CH08N | ARINC 429 RCV CH08 (−) | 25  |
| CH09P | ARINC 429 RCV CH09 (+) | 11  |
| CH09N | ARINC 429 RCV CH09 (−) | 26  |
| CH10P | ARINC 429 RCV CH10 (+) | 12  |
| CH10N | ARINC 429 RCV CH10 (−) | 27  |
| CH11P | ARINC 429 RCV CH11 (+) | 13  |
| CH11N | ARINC 429 RCV CH11 (−) | 28  |
| CH12P | ARINC 429 XMT CH12 (+) | 31  |
| CH12N | ARINC 429 XMT CH12 (−) | 32  |
| CH13P | ARINC 429 XMT CH13 (+) | 33  |
| CH13N | ARINC 429 XMT CH13 (−) | 34  |
| CH14P | ARINC 429 XMT CH14 (+) | 35  |

| Name  | Description            | Pin       |
|-------|------------------------|-----------|
| CH14N | ARINC 429 XMT CH14 (−) | 36        |
| CH15P | ARINC 429 XMT CH15 (+) | 37        |
| CH15N | ARINC 429 XMT CH15 (−) | 38        |
| GND   | Ground                 | 1, 14, 29 |

*Table 6-9 – Model UA2440 Pinout*

<span id="page-33-0"></span>

#### <span id="page-34-0"></span>*6.3.10 Model UA2710*

| Name   | Description                     | Pin                                                                                              |
|--------|---------------------------------|--------------------------------------------------------------------------------------------------|
| CH0AD  | CH0 RCV BUSA (+) Default Bus    | 3                                                                                                |
| CH0ADR | CH0 RCV BUSA (−) Default Bus    | 18                                                                                               |
| CH0BD  | CH0 RCV BUSB (+) Optional Bus   | 4                                                                                                |
| CH0BDR | CH0 RCV BUSB (−) Optional Bus   | 19                                                                                               |
| CH1BD  | CH1 XMT BUSB (+) Default Bus    | 4                                                                                                |
| CH1BDR | CH1 XMT BUSB (−) Default Bus    | 19                                                                                               |
| CH1AD  | CH1 XMT BUSA (+) Optional Bus   | 3                                                                                                |
| CH1ADR | CH1 XMT BUSA (−) Optional Bus   | 18                                                                                               |
| CH0AT  | CH0 BUSA Termination Jumper (+) | 6                                                                                                |
| CH0ATR | CH0 BUSA Termination Jumper (−) | 21                                                                                               |
| CH0BT  | CH0 BUSB Termination Jumper (+) | 7                                                                                                |
| CH0BTR | CH0 BUSB Termination Jumper (−) | 22                                                                                               |
| CH1AT  | CH1 BUSA Termination Jumper (+) | 6                                                                                                |
| CH1ATR | CH1 BUSA Termination Jumper (−) | 21                                                                                               |
| CH1BT  | CH1 BUSB Termination Jumper (+) | 7                                                                                                |
| CH1BTR | CH1 BUSB Termination Jumper (−) | 22                                                                                               |
| DIO00  | Discrete I/O 00                 | 39                                                                                               |
| DIO01  | Discrete I/O 01                 | 40                                                                                               |
| DIO02  | Discrete I/O 02                 | 41                                                                                               |
| DIO03  | Discrete I/O 03                 | 42                                                                                               |
| DIO04  | Discrete I/O 04                 | 43                                                                                               |
| DIO05  | Discrete I/O 05                 | 44                                                                                               |
| DIO06  | Discrete I/O 06                 | 30                                                                                               |
| DIO07  | Discrete I/O 07                 | 15                                                                                               |
| IRIG   | IRIG Signal                     | 16                                                                                               |
| GND    | Ground                          | 1, 14, 29                                                                                        |
| RSVD   | Reserved                        | 2, 5, 8, 9, 10, 11, 12, 13, 17,<br>20, 23, 24, 25, 26, 27, 28, 31,<br>32, 33, 34, 35, 36, 37, 38 |

*Table 6-10 – Model UA2710 Pinout*

<span id="page-34-1"></span>

| NOTE | To enable on-board direct coupled termination, complete the connection between the<br>positive and negative ends of the termination jumper (e.g., CH0AT and CH0ATR).                                                   |
|------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|      |                                                                                                                                                                                                                        |
| NOTE | For transformer coupled applications (not typical) refer to Model UA2140 pinout table for<br>the corresponding transformer signal pairs. For further assistance, please contact<br>Customer Service (see Section 1.6). |
|      |                                                                                                                                                                                                                        |
| NOTE | Connection of the ground pin(s) to the end system(s) is necessary for proper operation<br>of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.                                                                        |

#### <span id="page-35-0"></span>*6.3.11 Model UA2720*

| Name   | Description                     | Pin |  |
|--------|---------------------------------|-----|--|
| CH0AD  | CH0 RCV BUSA (+) Default Bus    | 3   |  |
| CH0ADR | CH0 RCV BUSA (−) Default Bus    | 18  |  |
| CH0BD  | CH0 RCV BUSB (+) Optional Bus   | 4   |  |
| CH0BDR | CH0 RCV BUSB (−) Optional Bus   | 19  |  |
| CH1BD  | CH1 XMT BUSB (+) Default Bus    | 4   |  |
| CH1BDR | CH1 XMT BUSB (−) Default Bus    | 19  |  |
| CH1AD  | CH1 XMT BUSA (+) Optional Bus   | 3   |  |
| CH1ADR | CH1 XMT BUSA (−) Optional Bus   | 18  |  |
| CH2AD  | CH2 RCV BUSA (+) Default Bus    | 11  |  |
| CH2ADR | CH2 RCV BUSA (−) Default Bus    | 26  |  |
| CH2BD  | CH2 RCV BUSB (+) Optional Bus   | 12  |  |
| CH2BDR | CH2 RCV BUSB (−) Optional Bus   | 27  |  |
| CH3BD  | CH3 XMT BUSB (+) Default Bus    | 12  |  |
| CH3BDR | CH3 XMT BUSB (−) Default Bus    | 27  |  |
| CH3AD  | CH3 XMT BUSA (+) Optional Bus   | 11  |  |
| CH3ADR | CH3 XMT BUSA (−) Optional Bus   | 26  |  |
| CH0AT  | CH0 BUSA Termination Jumper (+) | 6   |  |
| CH0ATR | CH0 BUSA Termination Jumper (−) | 21  |  |
| CH0BT  | CH0 BUSB Termination Jumper (+) | 7   |  |
| CH0BTR | CH0 BUSB Termination Jumper (−) | 22  |  |
| CH1AT  | CH1 BUSA Termination Jumper (+) | 6   |  |
| CH1ATR | CH1 BUSA Termination Jumper (−) | 21  |  |
| CH1BT  | CH1 BUSB Termination Jumper (+) | 7   |  |
| CH1BTR | CH1 BUSB Termination Jumper (−) | 22  |  |
| CH2AT  | CH2 BUSA Termination Jumper (+) | 8   |  |
| CH2ATR | CH2 BUSA Termination Jumper (−) | 23  |  |
| CH2BT  | CH2 BUSB Termination Jumper (+) | 9   |  |
| CH2BTR | CH2 BUSB Termination Jumper (−) | 24  |  |
| CH3AT  | CH3 BUSA Termination Jumper (+) | 8   |  |
| CH3ATR | CH3 BUSA Termination Jumper (−) | 23  |  |
| CH3BT  | CH3 BUSB Termination Jumper (+) | 9   |  |
| CH3BTR | CH3 BUSB Termination Jumper (−) | 24  |  |
| DIO00  | Discrete I/O 00                 | 39  |  |
| DIO01  | Discrete I/O 01                 | 40  |  |
| DIO02  | Discrete I/O 02                 | 41  |  |
| DIO03  | Discrete I/O 03                 | 42  |  |
| DIO04  | Discrete I/O 04                 | 43  |  |
| DIO05  | Discrete I/O 05                 | 44  |  |

| Name  | Description     | Pin                                                             |
|-------|-----------------|-----------------------------------------------------------------|
| DIO06 | Discrete I/O 06 | 30                                                              |
| DIO07 | Discrete I/O 07 | 15                                                              |
| IRIG  | IRIG Signal     | 16                                                              |
| GND   | Ground          | 1, 14, 29                                                       |
| RSVD  | Reserved        | 2, 5, 10, 13, 17, 20, 25, 28, 31,<br>32, 33, 34, 35, 36, 37, 38 |

*Table 6-11 – Model UA2720 Pinout*

<span id="page-36-0"></span>

| NOTE | To enable on-board direct coupled termination, complete the connection between the<br>positive and negative ends of the termination jumper (e.g., CH0AT and CH0ATR).                                                   |  |
|------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--|
|      |                                                                                                                                                                                                                        |  |
| NOTE | For transformer coupled applications (not typical) refer to Model UA2140 pinout table for<br>the corresponding transformer signal pairs. For further assistance, please contact<br>Customer Service (see Section 1.6). |  |
|      |                                                                                                                                                                                                                        |  |
| NOTE | Connection of the ground pin(s) to the end system(s) is necessary for proper operation<br>of MIL-STD-1553, ARINC 429, ARINC 717, and ARINC 708.                                                                        |  |

#### <span id="page-37-0"></span>**Standard MIL-STD-1553 Cables**

Astronics AES offers a few different cables that are helpful for connecting a UA2000 1553 to MIL-STD-1553 databuses. Each UA2000 1553 (except /NE option) includes either a single channel, transformer coupled twinax cable (16065) or a two channel, transformer coupled twinax cable (16066) with the product. Additionally, Astronics AES also offers a two-channel transformer coupled twinax cable with an auxiliary D-Sub connector (16064) for accessing the Discrete I/O and IRIG signals. Pictured below is the 16065 cable which is representative of the MIL-STD-1553 cables for the UA2000 1553.

![](_page_37_Picture_4.jpeg)

*Figure 6-3 – UA2000 1553 Twinax Cable*

## <span id="page-37-3"></span><span id="page-37-1"></span>*6.4.1 Single Channel Twinax Cable (16065)*

The 16065 cable is included with single channel UA2000 1553 models.

<span id="page-37-2"></span>

| Signal | DB44 (M) | Twinax (M) | Twinax Cable Label |
|--------|----------|------------|--------------------|
| CH0AX  | 2        | Center     | CH0 BUS A          |
| CH0AXR | 17       | Outer      |                    |
| CH0BX  | 5        | Center     | CH0 BUS B          |
| CH0BXR | 20       | Outer      |                    |

*Table 6-12 – 16065 Cable Pinout*

#### <span id="page-38-0"></span>*6.4.2 Two Channel Twinax Cable (16066)*

| Signal | DB44 (M) | Twinax (M) | Twinax Cable Label |
|--------|----------|------------|--------------------|
| CH0AX  | 2        | Center     | CH0 BUS A          |
| CH0AXR | 17       | Outer      |                    |
| CH0BX  | 5        | Center     | CH0 BUS B          |
| CH0BXR | 20       | Outer      |                    |
| CH1AX  | 10       | Center     | CH1 BUS A          |
| CH1AXR | 25       | Outer      |                    |
| CH1BX  | 13       | Center     | CH1 BUS B          |
| CH1BXR | 28       | Outer      |                    |

The 16066 cable is included with two channel UA2000 1553 models.

*Table 6-13 – 16066 Cable Pinout*

#### <span id="page-38-2"></span><span id="page-38-1"></span>*6.4.3 Two Channel Twinax Cable with Auxilary Connector (16064)*

The 16064 cable is available separately and provides an additional auxiliary connector for interfacing to the Discrete I/O and IRIG signals. Contact Customer Service (see Sectio[n 1.6\)](#page-9-1) for more information.

| Signal | DB44 (M) | Twinax (M) | Twinax Cable Label |
|--------|----------|------------|--------------------|
| CH0AX  | 2        | Center     | CH0 BUS A          |
| CH0AXR | 17       | Outer      |                    |
| CH0BX  | 5        | Center     | CH0 BUS B          |
| CH0BXR | 20       | Outer      |                    |
| CH1AX  | 10       | Center     | CH1 BUS A          |
| CH1AXR | 25       | Outer      |                    |
| CH1BX  | 13       | Center     | CH1 BUS B          |
| CH1BXR | 28       | Outer      |                    |

*Table 6-14 – 16064 Cable Twinax Pinout*

<span id="page-38-3"></span>

| Signal | DB44 (M) | DB15 (F) |
|--------|----------|----------|
| DIO00  | 39       | 1        |
| DIO01  | 40       | 2        |
| DIO02  | 41       | 3        |
| DIO03  | 42       | 4        |
| DIO04  | 43       | 5        |
| DIO05  | 44       | 6        |
| DIO06  | 30       | 7        |
| DIO07  | 15       | 8        |
| IRIG   | 16       | 9        |
| GND    | 1        | 10       |

<span id="page-38-4"></span>*Table 6-15 – 16064 Auxilary Connector Pinout*

#### <span id="page-39-0"></span>**ARINC 429 DB44 to DB15 Adapter Cable (16067)**

The 16067 cable is an adapter cable that allows a UA24xx to be used in place of a CM429-1/4R2T or /2R1T device. The ARINC 429 channels from the UA2000 are remapped to conform to the connector and pinout of the older PCMCIA card. This allows for existing wiring harnesses to be plugged into the USB without modification. This cable carries only ARINC 429 and 2 DIO signals but does not remap the remaining DIO or any ARINC 717. Contact Customer Service (see Section [1.6\)](#page-9-1) for more information on using UA2000s to replace legacy PCMCIA cards.

| Signal | DB44 (M) | DB15 (M) |
|--------|----------|----------|
| CH00P  | 2        | 2        |
| CH00N  | 17       | 9        |
| CH01P  | 3        | 3        |
| CH01N  | 18       | 10       |
| CH02P  | 4        | 4        |
| CH02N  | 19       | 11       |
| CH03P  | 5        | 5        |
| CH03N  | 20       | 12       |
| CH12P  | 31       | 6        |
| CH12N  | 32       | 13       |
| CH13P  | 33       | 8        |
| CH13N  | 34       | 14       |
| DIO00  | 39       | 7        |
| DIO04  | 43       | 15       |
| GND    | 1        | 1        |

*Table 6-16 – 16067 Cable Pinout*

# <span id="page-39-2"></span><span id="page-39-1"></span>**ARINC 717 Cable (16078)**

Astronics AES offers the ARINC 717 Test Sets (TS-UA1401-01 and TS-UA1431-01). These test sets come with a CP-UA1401 or CP-UA1431 as well as the 16078 cable. The 16078 cable connects the CP-UA1401 or CP-UA1431 to the System Test Plug of a Boeing 737/757/767 aircraft. Contact Customer Service (see Section [1.6\)](#page-9-1) for more information. Cable pinouts are provided below.

| Signal                                     | DB44 (M) | 15 Pin Connector (M) |
|--------------------------------------------|----------|----------------------|
| 115 VAC                                    | -        | 2*                   |
| DFDR Playback + [ARINC 717 Channel 10 (+)] | 12       | 4                    |
| DFDR Playback – [ARINC 717 Channel 10 (-)] | 27       | 5                    |
| DFDR Data Out + [ARINC 717 Channel 8 (+)]  | 10       | 8                    |
| DFDR Data Out – [ARINC 717 Channel 8 (-)]  | 25       | 9                    |
| Ground                                     | 1        | 10                   |
| DFDAU Power                                | -        | 11*                  |

*Table 6-17 – 16078 Cable Pinout*

<span id="page-39-3"></span>**NOTE** Pins 2 and 11 of the 15-Pin Connector are connected to each other inside the connector

# <span id="page-40-0"></span>**7. Coupling and Termination**

Coupling and termination are important considerations for MIL-STD-1553 and ARINC 708 databuses. Electrically, these databuses have similar characteristics. Except where a protocol is specified, the following discussion applies to both. This section introduces concepts relating to coupling and termination. Actual interfaces, cabling, and part numbers will vary from product to product. All figures included in this section are used generically for illustration of these concepts.

# <span id="page-40-1"></span>**Bus Termination**

The main databus consists of a pair of twisted, shielded wires with a characteristic impedance in the range of 70 to 85 ohms. The databus must be terminated at both ends with a resistor to provide proper loading and to minimize signal reflection and degradation on the bus. The resistor value should be close to the characteristic impedance of the databus. The resulting total load on the databus is the two terminating resistors in parallel (about 39 ohms). Even with a very short databus, the load from the terminating resistors is still required. Notice how the resistors terminate the databuses in [Figure](#page-41-0) 7-1 and [Figure](#page-42-0) 7-2. Note that some Ballard products have on-board termination resistors that can be switched in manually or under software control.

*Note: The most common problem in a new system is an improperly terminated databus.*

#### <span id="page-40-2"></span>**Transformer versus Direct Coupling**

MIL-STD-1553 can be either direct or transformer coupled. Most military 1553 systems are transformer coupled. ARINC 708, however, is normally direct coupled.

Both coupling methods have a transformer as part of the terminal's interface, but transformer coupling has an additional external transformer coupler that isolates the stub from the main databus and reduces signal reflections. The signal level on the main bus is the same for both direct and transformer coupling. Though it is rarely done, systems can mix the use of direct and transformer coupling.

A terminal must be properly configured for either direct or transformer coupling. There is a difference between the terminal's internal interface circuit for direct and transformer coupling:

- 1. The transformer-coupled terminal has a lower turns ratio and no isolation resistors, but this is made up for in the external coupler, which has a step-up transformer and isolation resistors (see [Figure](#page-41-0) 7-1).
- 2. The direct-coupled terminal has a higher turns ratio and has isolation resistors that are connected directly to the main databus. Direct coupled stubs should be kept as short as possible (se[e Figure](#page-42-0) 7-2).

![](_page_41_Figure_2.jpeg)

<span id="page-41-0"></span>*Figure 7-1 – Transformer Coupling to a Dual-Redundant Databus*

![](_page_42_Figure_2.jpeg)

<span id="page-42-0"></span>*Figure 7-2 – Direct Connection to a Dual-Redundant Databus*

# <span id="page-43-0"></span>**8. Mechanical Diagrams**

#### <span id="page-43-1"></span>**Dimensions**

All dimensions in inches. To assist in integration, CAD data for the UA2000 is available upon request.

![](_page_43_Figure_3.jpeg)

![](_page_43_Figure_4.jpeg)

<span id="page-43-2"></span>*Figure 8-1 – UA2000 Dimensions*

#### <span id="page-44-0"></span>**Mounting Features**

A typical use of a UA2000 would be to simply lay it down on a desk or bench top. However, to facilitate other installations, the UA2000 has some additional mechanical features worth noting.

#### <span id="page-44-1"></span>*8.2.1 Securing UA2000s*

The side indentations of the UA2000 enclosure accommodate a one-inch strap for securing the device to another surface.

#### <span id="page-44-2"></span>*8.2.2 Stacking Multiple UA2000s*

The ribs on the bottom and the grooves on the top of the UA2000 enclosure align and fit into each other allowing units to stack on top of each other. This feature is useful when needing to secure multiple devices together.

# <span id="page-45-0"></span>**9. Specifications**

Technical specifications are subject to change without notice.

#### <span id="page-45-1"></span>**General**

- **USB Type-C Connector (USB-C)** Supports USB 3.1 Gen 1 SuperSpeed and High Speed Backwards compatible with USB 2.x High Speed Adapter cables (included) support USB Type-C and USB Type-A host ports
- **RAM** 64MB SDRAM
- **Flash** 16MB Flash
- **Timer** High resolution timer for time-tagging events (1µs resolution)
- **IRIG Input** IRIG A/B Format with Pulse Width Code Modulated TTL signal IRIG A/B Format with Amplitude Modulated signal Pulse Per Second (PPS) TTL signal

#### • **IRIG Output**

IRIG A/B Format with Pulse Width Code Modulated TTL signal Pulse Per Second (PPS) TTL signal

• **Avionics Shunt Discrete I/O** Input/Output: 8 Open/GND Sequential monitor of I/O changes

#### • **MTBF**

UA2000 1553 MTBF: 5,000,000 hrs UA2000 429/717 MTBF: 4,800,000 hrs UA2000 708 MTBF: 5,000,000 hrs

#### <span id="page-45-2"></span>**Model Dependent Interfaces**

#### • **MIL-STD-1553**

Compliant to MIL-STD-1553C electrical and protocol specifications BC/RT/MON (Single or Multi-Function) Hardware controlled transmit scheduling TA/SA message filtering Sequential monitor of databus activity

#### • **ARINC 429**

Periodic and asynchronous messages Hardware controlled transmit schedule Receive message filtering (Label/SDI) Sequential monitor of databus activity

#### • **ARINC 717**

Transmit and receive Bipolar and Biphase Sub-frame and super-frame support 64, 128, 256, 512, 1024, 2048, 4096, 8192 wps Sequential monitor of databus activity

#### • **ARINC 708**

Transmit and receive Hardware controlled transmit schedule Variable bit length operation (supports picture bus and other similar databuses) Sequential monitor of databus activity

#### <span id="page-46-0"></span>**Software**

- BTIDriver™ API compatible
- Translation DLLs for older Ballard devices

#### <span id="page-46-1"></span>**Environmental / Physical**

• **Industrial Temperature Range** Storage Temperature: −55°C to +100°C Component Temperature: −40°C to +85°C

#### • **Compact Enclosure**

Dimensions: 3.00 x 4.38 x 0.97 inch Weight: 5 oz Material: PC/PET blend (Xenoy) Color: Blue (default) or Flight Test Orange (/FTO option)

#### <span id="page-46-2"></span>**Power**

**NOTE** The maximum current allowed for a High-Speed USB device in the USB specification is 500 mA. UA2000 1553 Bus Monitor only models (UA2110, UA2111, UA2140) and most UA2000 429/717 models (UA24xx) adhere to this limit. UA2000 1553 Single Function models (UA212x), UA2000 1553 Multi-Function models (UA213x), UA2000 429/717 models with four 429 transmit channels (UA2430, UA2431, UA2440), and UA2000 708 models (UA27xx) do not adhere to this limit. In practice, most USB host ports allow devices to draw much more power than the 500mA limit. Additionally, SuperSpeed USB host ports (USB 3.1) provide at least 900mA of current. Please ensure that the USB host port provides adequate power for

your application. If using a hub, it is recommended that it be externally powered.

- **USB Bus Powered**
- **Voltage Range**

4.0V to 5.5V (at device)

#### • **UA2000 1553 Current Draw** (UA21XX Models):

|                        | Max Current by Channel Count            |                                          |  |
|------------------------|-----------------------------------------|------------------------------------------|--|
| Transmit<br>Duty Cycle | 1 Channel<br>(Single or Multi Function) | 2 Channels<br>(Single or Multi Function) |  |
| Idle                   | 204 mA                                  | 204 mA                                   |  |
| 50%                    | 374 mA                                  | 540 mA                                   |  |
| 75%                    | 459 mA                                  | 722 mA                                   |  |
| 100%                   | 540 mA                                  | 886 mA                                   |  |

- **UA2000 429/717 Current Draw** (full load) (UA24XX Models) Idle: 311 mA Max (UA2431): 537 mA Max (UA2440): 578 mA
- **UA2000 708 Current Draw** (UA27XX Models)

|                     | Max Current by Channel Count |        |
|---------------------|------------------------------|--------|
| Transmit Duty Cycle | 1R1T                         | 2R2T   |
| Idle                | 206 mA                       | 206 mA |
| 33% (Typical)       | 315 mA                       | 428 mA |
| 50%                 | 376 mA                       | 552 mA |
| 100%                | 546 mA                       | 894 mA |

# <span id="page-48-0"></span>**10. Revision History**

|  | The following revisions have been made to this manual: |
|--|--------------------------------------------------------|
|--|--------------------------------------------------------|

| Revision | Date             | Description                                                                   |
|----------|------------------|-------------------------------------------------------------------------------|
| A0       | August 17, 2020  | Initial release of this manual.                                               |
| A1       | August 31, 2020  | Updated Export Compliance statement.                                          |
| 2        | December 1, 2022 | Updated power specifications in Section 9.5. Updated branding and formatting. |

![](_page_49_Picture_0.jpeg)

#### **Astronics AES**

12950 Willows Road NE Kirkland, WA 98034 USA

**Contact** Phone: +1.425.339.0281 Email: [Ballard.Sales@astronics.com](mailto:Ballard.Sales@astronics.com)

[astronics.com/BallardTechnology](https://www.astronics.com/ballardtechnology)

![](_page_49_Picture_5.jpeg)

Astronics AES is committed to quality and is AS9100 and ISO 9001 registered.