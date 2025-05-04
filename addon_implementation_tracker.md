# C++ Addon Implementation Tracker

Use this checklist to track the implementation status of the required BTI/ARINC 429 functions in the C++ addon.

Check the box when a function is implemented, tested, and integrated.

## 1. Device Management (`BTICARD`)

- [x] `addon_CardOpenStr(cardString)` -> `cardOpen(number)` *(Convention Updated)*
- [x] `addon_CardClose(handle)`
- [x] `addon_CardReset(handle)`
- [x] `addon_CardGetInfo(handle, infoType, channelNum)`

## 2. ARINC 429 Channel Configuration (`BTI429`)

- [x] `addon_429_ChConfig(handle, channelNum, configVal)`
- [x] `addon_429_ChStart(handle, channelNum)`
- [x] `addon_429_ChStop(handle, channelNum)`

## 3. ARINC 429 Data Transmission (`BTI429`)

*(Choose one or both based on implementation needs)*
- [x] `addon_429_ListXmtCreate(handle, channelNum, count, configVal)`
- [x] `addon_429_ListDataWr(handle, listAddr, value)`
- [x] `addon_429_ListDataBlkWr(handle, listAddr, dataArray)`

## 4. ARINC 429 Data Reception (`BTI429`)

*(Choose one or both based on implementation needs)*
- [x] `addon_429_ListRcvCreate(handle, channelNum, count, configVal)`
- [x] `addon_429_ListDataRd(handle, listAddr)`
- [x] `addon_429_ListDataBlkRd(handle, listAddr, maxCount)`
- [x] `addon_429_ListStatus(handle, listAddr)`

## 5. ARINC 429 Filtering (`BTI429`)

- [x] `addon_429_FilterSet(handle, channelNum, label, sdiMask, configVal)`
- [x] `addon_429_FilterDefault(handle, channelNum, configVal)`

## Additional Implementation Notes

- [x] DLL Loading Logic (32/64 bit)
- [x] Asynchronous Handling (for Receive)
- [x] BTI Constants Definition
- [x] N-API Setup & Module Exports