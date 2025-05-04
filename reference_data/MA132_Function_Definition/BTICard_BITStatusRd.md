# **BITStatusRd**

```
ULONG BTICard_BITStatusRd
(
      HCARD hCard //Card handle
)
```
## **RETURNS**

The value of the BIT Status register.

## **DESCRIPTION**

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

## **SEE ALSO**

BTICard\_BITStatusClear, BTICard\_BITConfig