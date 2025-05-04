# **BITStatusClear**

```
ERRVAL BTICard_BITStatusClear
(
     ULONG statval, //Mask of bits to clear
     HCARD hCard //Card handle
)
```
### **RETURNS**

A negative value if an error occurs or zero if successful.

## **DESCRIPTION**

Each bit in statval clears the corresponding bit in the BIT Status register for the Card specified by hCard. The constants in the table below may be bitwise OR-ed together to clear each specified status bit.

| Constant               | Description                             |
|------------------------|-----------------------------------------|
| BITSTAT_CARD_STOPPED   | CBIT fatal system error, card stopped   |
| BITSTAT_CBIT_FPGA_ERR  | CBIT FPGA Single Event Upset (SEU)      |
| BITSTAT _PROTOCOL_ERR  | CBIT Protocol Error                     |
| BITSTAT_SINGLE_BIT_ERR | CBIT ECC Single Bit Error (correctable) |
| BITSTAT_DOUBLE_BIT_ERR | CBIT ECC Double Bit Error               |
| BITSTAT_SYSMON_ERR     | CBIT SysMon Error                       |

Please refer to BTICard\_BITStatusRd for more information on these error flags.

### **DEVICE DEPENDENCY**

Applies to 5G, 5G+, and 6G Devices that support BIT/SysMon functionality. Please consult the hardware manual for the Device.

#### **WARNINGS**

None.

## **SEE ALSO**

BTICard\_BITStatusRd