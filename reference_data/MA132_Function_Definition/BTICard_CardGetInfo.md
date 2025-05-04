# **CardGetInfo**

```
ULONG BTICard_CardGetInfo
(
     USHORT infotype, //Selects type of info to return
     INT channum, //Channel number
     HCORE hCore //Core handle
)
```
## **RETURNS**

The information specified by infotype.

## **DESCRIPTION**

Returns various information from the Device specified by *hCore*. *infotype* selects the type of information to return (see table below).

*channum* is reserved for future use.

| infotype           |                                                                         |
|--------------------|-------------------------------------------------------------------------|
| Constant           | Description                                                             |
| INFOTYPE_PROD      | Returns the product info, which is usually the model number of the core |
| INFOTYPE_HWGEN     | Returns the hardware generation                                         |
|                    | 4 = 4G                                                                  |
|                    | 5 = 5G, 5G+                                                             |
|                    | 6 = 6G                                                                  |
| INFOTYPE_PLAT      | Returns the platform:                                                   |
|                    | 4734H = '4G'                                                            |
|                    | 4735H = '5G', '5G+'                                                     |
|                    | 4736H = '6G'                                                            |
| INFOTYPE_CARDTYPE  | 0001H = BUSBOX                                                          |
|                    | 0002H = PCI4G                                                           |
|                    | 0003H = cPCI4G                                                          |
|                    | 0004H = RPC                                                             |
|                    | 0005H = PMC4G                                                           |
|                    | 0011H = PM42                                                            |
|                    | 0012H = PCIE5G                                                          |
|                    | 0013H = USB5G                                                           |
|                    | 0014H = PCI5GV6                                                         |
|                    | 0015H = PM15                                                            |
|                    | 0016H = AB3000                                                          |
|                    | 0017H = PE1000                                                          |
|                    | 0018H = PCI6G                                                           |
|                    | 0019H = PXI6G                                                           |
|                    | 001AH = OBB6G                                                           |
|                    | 001BH = OBB2USB                                                         |
|                    | 001DH = AB3SN                                                           |
|                    | 001EH = ME1000                                                          |
|                    | 001FH = NG1/NG2/NG3                                                     |
|                    | 0020H = UA2000                                                          |
| INFOTYPE_VERSIONEX | Returns the firmware version of the form MMmmii                         |
|                    | MM = Major version (bits 16-23)                                         |
|                    | mm = Minor version (bits 8-15)                                          |
|                    | ii = Minor-minor version (bits 0-7)                                     |
| INFOTYPE_DATE      | Returns the firmware date of the form YYYYMMDD                          |
|                    | YYYY = Year (bits 16-31)                                                |
|                    | MM = Month (bits 8-15)                                                  |
|                    | DD = Day (bits 0-7)                                                     |
| INFOTYPE_SERIALNUM | Returns the serial number                                               |
| INFOTYPE_1553COUNT | Returns the number of channels of the specified protocol                |
| INFOTYPE_429COUNT  |                                                                         |
| INFOTYPE_717COUNT  |                                                                         |
| INFOTYPE_708COUNT  |                                                                         |
| INFOTYPE_422COUNT  |                                                                         |
| INFOTYPE_CSDBCOUNT |                                                                         |
| INFOTYPE_DIOCOUNT  |                                                                         |
| INFOTYPE_EBRCOUNT  |                                                                         |

## **DEVICE DEPENDENCY**

See above table

#### **WARNINGS**

None.

## **SEE ALSO**

BTICard\_CardProductStr, BTICard\_CardTypeStr