using System;
using System.Runtime.InteropServices;

namespace Cargo_Door_Base.BTI
{
    public class BTICARD
    {
        /**
        *
        *  BTICard WIN32 DRIVER  Version 2.9.5  (10/29/2020)
        *  Copyright 2001-2020
        *  Astronics Advanced Electronic Systems Corp.
        *  www.astronics.com
        *  Ballard.Support@astronics.com
        *
        *  NAME:   BTICardNET.CS -- C# 32-bit/64-bit
        *                       BTICard Driver Include Header File.
        *
        **/

        //OS Compatibility definitions
#if BUILT_FOR_WINDOWS
			const string LIBNAME32 = "BTICARD.dll";
			const string LIBNAME64 = "BTICARD64.dll";
#elif BUILT_FOR_LINUX
			const string LIBNAME32 = "libbticard.so";
			const string LIBNAME64 = "libbticard.so";
#else
        const string LIBNAME32 = "BTICARD.dll";
        const string LIBNAME64 = "BTICARD64.dll";
#endif

        /**
		*
		*  Structs used by the BTICard Driver.
		*
		**/

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SEQRECORD1553
        {
            public ushort typeval;               //Valid in all versions
            public ushort count;              //Valid in all versions
            public uint timestamp;          //Valid in all versions
            public ushort activity;           //Valid in all versions
            public ushort errorval;              //Valid in all versions
            public ushort cwd1;               //Valid in all versions
            public ushort cwd2;               //Valid in all versions
            public ushort swd1;               //Valid in all versions
            public ushort swd2;               //Valid in all versions
            public ushort datacount;          //Valid in all versions
            public fixed ushort data[40];	//Variable length (don't exceed data[datacoun		t-1])
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SEQRECORDMORE1553
        {
            public uint timestamph;         //Valid if version of baseval record (SEQRECORD1553) >= 1
            public ushort resptime1;          //Valid if version of baseval record (SEQRECORD1553) >= 1
            public ushort resptime2;          //Valid if version of baseval record (SEQRECORD1553) >= 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SEQRECORD429
        {
            public ushort typeval;               //Valid in all versions
            public ushort count;              //Valid in all versions
            public uint timestamp;          //Valid in all versions
            public ushort activity;           //Valid in all versions
            public ushort decgap;             //Valid if version >= 1
            public uint data;               //Valid in all versions
            public uint timestamph;         //Valid if version >= 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SEQRECORD717
        {
            public ushort typeval;               //Valid in all versions
            public ushort count;              //Valid in all versions
            public uint timestamp;          //Valid in all versions
            public ushort activity;           //Valid in all versions
            public ushort wordnum;            //Valid in all versions
            public ushort subframe;           //Valid in all versions
            public ushort superframe;         //Valid in all versions
            public ushort data;               //Valid in all versions
            public ushort rsvd9;              //Valid if version >= 1
            public uint timestamph;         //Valid if version >= 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SEQRECORD708
        {
            public ushort typeval;               //Valid in all versions
            public ushort count;              //Valid in all versions
            public uint timestamp;          //Valid in all versions
            public ushort activity;           //Valid in all versions
            public ushort datacount;          //Valid in all versions
            public fixed ushort data[100];	//Valid in all ver		sions
            public fixed ushort extra[16];	//Valid if version >= 1
            public ushort bitcount;           //Valid if version >= 1
            public ushort rsvd123;            //Valid if version >= 1
            public uint timestamph;         //Valid if version >= 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SEQRECORDCSDB
        {
            public ushort typeval;               //Valid in all versions
            public ushort count;              //Valid in all versions
            public uint timestamp;          //Valid in all versions
            public uint timestamph;         //Valid in all versions
            public ushort activity;           //Valid in all versions
            public ushort datacount;          //Valid in all versions
            public fixed ushort data[32];	//Valid in all ver		sions
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SEQRECORDDIO
        {
            public ushort typeval;               //Valid in all versions
            public ushort count;              //Valid in all versions
            public ushort bank;               //Valid in all versions
            public ushort state;              //Valid in version 0
            public uint timestamp;          //Valid in all versions
            public uint timestamph;         //Valid in all versions
            public ushort change;             //Valid in version 1
            public ushort value;              //Valid in version 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct SEQRECORDEBR
        {
            public ushort typeval;               //Valid in all versions
            public ushort count;              //Valid in all versions
            public ushort activity;           //Valid in all versions
            public ushort errorval;              //Valid in all versions
            public uint timestamp;          //Valid in all versions
            public uint timestamph;         //Valid in all versions
            public ushort cwd;                //Valid in all versions
            public ushort cwdinfo;            //Valid in all versions
            public ushort swd;                //Valid in all versions
            public ushort swdinfo;            //Valid in all versions
            public ushort resptime;           //Valid in all versions
            public ushort datacount;          //Valid in all versions
            public fixed ushort data[40];	//Variable length (don't exceed data[datacoun		t-1])
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SEQFINDINFO
        {
            public unsafe ushort* pRecFirst;
            public unsafe ushort* pRecNext;
            public unsafe ushort* pRecLast;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BTIIRIGTIME
        {
            public ushort days;
            public ushort hours;
            public ushort min;
            public ushort sec;
            public ushort msec;
            public ushort usec;
        }

        /**
        *
        *  BTICard Driver functions.
        *
        **/

        private static class BTICARD_32
        {
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Add64@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Add64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_AddrDSP@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_AddrDSP(uint addr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_AddrHost@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_AddrHost(uint addr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_AsciiToMant@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_AsciiToMant(string str, ref uint mant, ref int exp);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_BCDToBin@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_BCDToBin(uint bcdval, int msb, int lsb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_BinToBCD@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_BinToBCD(uint oldbcdval, uint binval, int msb, int lsb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_BITConfig@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_BITConfig(uint configval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_BITInitiate@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_BITInitiate(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_BITStatusClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_BITStatusClear(uint statval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_BITStatusRd@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_BITStatusRd(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardClose@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardClose(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardCloseAll@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardCloseAll();
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardGetInfo@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_CardGetInfo(ushort infotype, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardGetInfoEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardGetInfoEx(ref ushort bufmodel, ushort bufmodelcount, ref ushort buffeature, ushort buffeaturecount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardIsRunning@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardIsRunning(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardNop@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardNop(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardOpen@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardOpen(ref nint lphandle, int cardnum);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardOpenStr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardOpenStr(ref nint lphandle, string cardstr);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardProductStr@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_CardProductStr(nint handleval);
            public static string BTICard_CardProductStr(nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardProductStr(handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardReserveRd@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_CardReserveRd(nint handleval);
            public static string BTICard_CardReserveRd(nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardReserveRd(handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardReserveWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardReserveWr(string str, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardReset@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardReset(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardResetEx@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardResetEx(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardResume@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardResume(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardShadow@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardShadow(int shadowval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardStart@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardStart(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardStop@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardStop(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardSyncEnable@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardSyncEnable(int enableflag, ushort syncmask, ushort pinpolarity, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardSyncValid@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CardSyncValid(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardSyncValidEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardSyncValidEx(ref ushort maskout, ushort maskin, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTest@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest(ushort level, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTest0@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest0(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTest1@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest1(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTest2@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest2(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTest3@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest3(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTrigger@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTrigger(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTriggerEnable@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTriggerEnable(int enableflag, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTriggerEnableEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTriggerEnableEx(int enableflag, ushort trigmask, ushort pinpolarity, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTriggerEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTriggerEx(ushort trigmask, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTriggerValid@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CardTriggerValid(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTriggerValidEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTriggerValidEx(ref ushort maskout, ushort maskin, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CardTypeStr@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_CardTypeStr(nint handleval);
            public static string BTICard_CardTypeStr(nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardTypeStr(handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARClr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARClr(ushort maskval, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARGet@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ChDARGet(ushort maskval, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARRdL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ChDARRdL(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARRdsW@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARRdsW(ushort[] valueptr, ushort addrval, int countval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARRdW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_ChDARRdW(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARSet@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARSet(ushort maskval, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARWrL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARWrL(uint value, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARWrsW@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARWrsW(ushort[] valueptr, ushort addrval, int countval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ChDARWrW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARWrW(ushort value, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CISRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CISRd(ushort[] buf, ushort bufcount, int cistype, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CISWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CISWr(ushort[] buf, ushort bufcount, int cistype, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommBufRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CommBufRd(ushort offset, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommBufWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CommBufWr(ushort value, ushort offset, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommCall@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommCall(uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommChannelReconfig@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommChannelReconfig(uint chmask, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommCheck@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommCheck(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommDisable@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommDisable(ushort command, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommDisableEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommDisableEx(ushort command, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommEnable@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommEnable(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommExternSRQ@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommExternSRQ(uint chmask, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommFillW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommFillW(ushort value, uint addrval, ushort count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommProtocolFunc@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommProtocolFunc(ushort opcode, ushort argcount, ref ushort argbuf, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommRdL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_CommRdL(uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommRdsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommRdsW(ushort[] valueptr, uint addrval, ushort count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CommRdW(uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommRun@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommRun(ref ushort dataptr, ushort datacount, ref ushort codeptr, ushort codecount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommWrL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CommWrL(uint value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommWrsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommWrsW(ushort[] valueptr, uint addrval, ushort count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CommWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CommWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcCheck@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcCheck(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcGetInfo@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcGetInfo(ref uint valueptr, ushort infotype, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemRdL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdL(ref uint valueptr, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemRdsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdsL(uint[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemRdsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdsW(ushort[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemRdW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdW(ref ushort valueptr, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemWrL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrL(uint value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemWrsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrsL(uint[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemWrsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrsW(ushort[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoProcMemWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_CoreOpen@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoreOpen(ref nint lphcore, int corenum, nint hcard);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARClr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARClr(ushort maskval, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARGet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DARGet(ushort maskval, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARRdL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_DARRdL(ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARRdsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_DARRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARSet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARSet(ushort maskval, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARWrL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARWrL(uint value, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARWrsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DARWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARWrW(ushort value, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Div@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Div(uint diva, uint divb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Div64@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Div64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DllUsageCount@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DllUsageCount();
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DriverInfoStr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_DriverInfoStr();
            public static string BTICard_DriverInfoStr() { return Marshal.PtrToStringAnsi(_BTICard_DriverInfoStr()); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspBioClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspBioClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspBioRd@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspBioRd(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspBioSet@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspBioSet(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspIntfClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntfClear(ushort intmask, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspIntfRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntfRd(ushort intmask, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspIntmClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntmClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspIntmSet@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntmSet(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspXfClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspXfClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspXfRd@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspXfRd(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_DspXfSet@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspXfSet(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ErrDesc@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ErrDesc(int errval, nint handleval);
            public static string BTICard_ErrDesc(int errval, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_ErrDesc(errval, handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ErrDescStr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ErrDescStr(int errval, nint handleval);
            public static string BTICard_ErrDescStr(int errval, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_ErrDescStr(errval, handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ErrName@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ErrName(int errval, nint handleval);
            public static string BTICard_ErrName(int errval, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_ErrName(errval, handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_EventLogClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_EventLogClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_EventLogConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_EventLogConfig(ushort configval, ushort count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_EventLogRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_EventLogRd(ref ushort typevalval, ref uint infoval, ref int channel, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_EventLogStatus@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_EventLogStatus(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExpandMant@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExpandMant(ref uint mant, ref int exp);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDinRd@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDinRd(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDinWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDinWr(int dinval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDIODirSet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDIODirSet(int dionum, int dirval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDIOEnWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDIOEnWr(int dionum, int dioval, int dioen, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDIOBankConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDIOBankConfig(uint configtype, ushort configmask, int banknum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDIOMonConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDIOMonConfig(ushort rise_edge, ushort fall_edge, int banknum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDIORd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDIORd(int dionum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtDIOWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDIOWr(int dionum, int dioval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtLEDRd@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtLEDRd(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtLEDWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtLEDWr(int ledval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtStatusLEDRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtStatusLEDRd(ref int ledon, ref int ledcolor, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ExtStatusLEDWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtStatusLEDWr(int ledon, int ledcolor, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileClose@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_FileClose(nint handle);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileErr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileErr();
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileErrClr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_FileErrClr();
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileErrSet@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_FileErrSet();
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileOpenRead@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint BTICard_FileOpenRead(string filename);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileOpenWrite@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint BTICard_FileOpenWrite(string filename);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileRead@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileRead(nint handle, nint buffer, ushort count);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileSeek@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileSeek(nint handle, uint offset);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileTell@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_FileTell(nint handle);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_FileWrite@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileWrite(nint handle, nint buffer, ushort count);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_GetHigh@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_GetHigh(uint val);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_GetHighL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_GetHighL(ulong val);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_GetLow@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_GetLow(uint val);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_GetLowL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_GetLowL(ulong val);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_GetTickCount@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_GetTickCount();
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_GlobalRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_GlobalRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_GlobalWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_GlobalWrW(ushort value, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleInfo@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_HandleInfo(System.Text.StringBuilder cardstr, ref int cardnum, ref uint sizval, nint* vxdptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleInfoEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleInfoEx(ref uint valueptr, uint typeval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleIsCard@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleIsCard(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleIsCore@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleIsCore(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleMakeCard@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleMakeCard(ref nint lphcard, ref int lpcorenum, nint hcore);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleMakeCore@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleMakeCore(ref nint lphcore, int corenum, nint hcard);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleMakeRPC@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleMakeRPC(ref nint hcard_remote, ref uint lphrpc, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HandleOkay@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleOkay(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HeapAlloc@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapAlloc(int section, uint wordcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HeapAllocAll@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapAllocAll(int section, ref uint wordcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HeapAllocEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapAllocEx(ushort configval, int section, uint wordcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HeapWipe@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapWipe(int section, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIFill@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIFill(ushort value, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIRdL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HPIRdL(ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIRdsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIRdsL(uint[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIRdsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_HPIRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIWrL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrL(uint value, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIWrsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrsL(uint[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIWrsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_HPIWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrW(ushort value, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IDRegRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_IDRegRd(int gate_array_num, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IDRegWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IDRegWr(ushort value, int gate_array_num, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IntClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntDisable@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntDisable(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntEnable@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntEnable(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntEnableCond@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntEnableCond(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntGet@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint BTICard_IntGet(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntInstall@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntInstall(nint hevent, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntReset@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IntReset(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IntUninstall@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntUninstall(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IORdL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IORdL(int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IORdsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IORdsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IORdsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IORdsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IORdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_IORdW(int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IOWINRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_IOWINRdW(int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IOWINWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWINWrW(ushort value, int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IOWrL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrL(uint value, int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IOWrsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IOWrsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IOWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrW(ushort value, int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGConfig@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGConfig(uint configval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldGetDays@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetDays(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldGetHours@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetHours(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldGetMicrosec@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetMicrosec(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldGetMillisec@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetMillisec(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldGetMin@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetMin(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldGetSec@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetSec(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldPutDays@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutDays(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldPutHours@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutHours(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldPutMicrosec@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutMicrosec(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldPutMillisec@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutMillisec(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldPutMin@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutMin(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGFieldPutSec@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutSec(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGInputThresholdGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGInputThresholdGet(ref ushort dacval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGInputThresholdSet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGInputThresholdSet(ushort dacval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGRd(ref BTIIRIGTIME irigtime, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGRdEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGRdEx(ref ushort timebuf, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGSyncStatus@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGSyncStatus(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGTimeBCDToBin@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeBCDToBin(ref uint timevalh, ref uint timevall, uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGTimeBCDToNanoBin@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeBCDToNanoBin(ref uint timevalh, ref uint timevall, uint irigvalh, uint irigvall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGTimeBinToBCD@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeBinToBCD(ref uint irigvalh, ref uint irigvall, uint timevalh, uint timevall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGTimeNanoBinToBCD@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeNanoBinToBCD(ref uint irigvalh, ref uint irigvall, uint timevalh, uint timevall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGWr(ref BTIIRIGTIME irigtime, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_IRIGWrEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGWrEx(ref ushort timebuf, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_KernIntStatus@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_KernIntStatus(ref int valueptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_KernStatus@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_KernStatus(ref uint valueptr, uint typeval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_KernStatusEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_KernStatusEx(ref uint valueptr, uint typeval, uint index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_MakeLong@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_MakeLong(ushort valh, ushort vall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_MakeQuad@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ulong BTICard_MakeQuad(uint valh, uint vall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_MakeWord@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_MakeWord(byte valh, byte vall);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_MantToAscii@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_MantToAscii(System.Text.StringBuilder buf, int mant, int exp);
            public static string BTICard_MantToAscii(System.Text.StringBuilder buf, int mant, int exp) { return Marshal.PtrToStringAnsi(_BTICard_MantToAscii(buf, mant, exp)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Mask@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Mask(uint dataval, ushort cntval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_MaxMant@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_MaxMant(ref uint mant, ref int exp);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Mod@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Mod(uint moda, uint modb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Mod64@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Mod64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Mul@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Mul(uint mula, uint mulb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Mul64@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Mul64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_NormalMant@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_NormalMant(ref uint mant, ref int exp);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_PortRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_PortRd(int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_PortWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_PortWr(ushort value, int addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ProgRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_ProgRdW(uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ProgWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ProgWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_PXIStatus@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_PXIStatus(ref uint infoptr, ushort infotype, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMFill@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMFill(ushort value, uint addrval, uint countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMRdB@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_RAMRdB(uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMRdL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_RAMRdL(uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMRdmL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdmL(uint[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMRdmW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdmW(ushort[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMRdsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMRdsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_RAMRdW(uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWipe@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWipe(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWipeEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWipeEx(ushort value, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWrB@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrB(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWrL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrL(uint value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWrmL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrmL(uint[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWrmW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrmW(ushort[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWrsL@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWrsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_RAMWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ReverseLong@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ReverseLong(uint value);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ReverseWord@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_ReverseWord(ushort value);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqBlkRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SeqBlkRd(ushort[] buf, uint bufcount, ref uint blkcnt, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqBlkRdEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SeqBlkRdEx(ushort[] buf, uint bufcount, uint maxblkcnt, ref uint blkcnt, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqCommRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqCommRd(ushort[] buf, ushort bufcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqConfig@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqConfig(uint configval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqConfigEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqConfigEx(uint configval, uint seqcount, ushort cardnum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqConfigExx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqConfigExx(uint configval, uint seqaddr, uint seqcount, ushort cardnum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqDMARd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SeqDMARd(ushort[] buf, uint bufcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindCheckVersion@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindCheckVersion(ref ushort precord, ushort version);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindInit@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindInit(ushort[] seqbuf, uint seqbufsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindMore1553@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindMore1553(ref SEQRECORDMORE1553* precmore, ref SEQRECORD1553 precbase);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindMore1553Ex@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindMore1553Ex(ref SEQRECORDMORE1553 precmore, ushort recordsize, ref SEQRECORD1553 precbase);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext(ref ushort* precord, ref ushort seqtype, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext1553@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext1553(ref SEQRECORD1553* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext1553Ex@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext1553Ex(ref SEQRECORD1553 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext429@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext429(ref SEQRECORD429* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext429Ex@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext429Ex(ref SEQRECORD429 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext708@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext708(ref SEQRECORD708* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext708Ex@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext708Ex(ref SEQRECORD708 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext717@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext717(ref SEQRECORD717* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNext717Ex@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext717Ex(ref SEQRECORD717 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNextCSDB@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNextCSDB(ref SEQRECORDCSDB* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNextCSDBEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextCSDBEx(ref SEQRECORDCSDB precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNextDIO@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNextDIO(ref SEQRECORDDIO* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNextDIOEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextDIOEx(ref SEQRECORDDIO precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNextEBR@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNextEBR(ref SEQRECORDEBR* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNextEBREx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextEBREx(ref SEQRECORDEBR precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqFindNextEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextEx(ushort[] precord, ushort recordcount, ref ushort seqtype, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqInterval@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqInterval(int interval, int mode, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqIntervalEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqIntervalEx(ushort shiftval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqIsRunning@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqIsRunning(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqLogFrequency@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqLogFrequency(ushort logfreq, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqRd(ushort[] buf, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqRdEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqRdEx(ushort[] buf, ushort bufcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqResume@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqResume(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqStart@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqStart(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqStatus@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqStatus(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SeqStop@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqStop(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Shl@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Shl(uint dataval, ushort cntval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Shr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Shr(uint dataval, ushort cntval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SignMant@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SignMant(string str);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Sub64@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Sub64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SwapEndianL@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapEndianL(ref uint value);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SwapEndianW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapEndianW(ref ushort valuea, ref ushort valueb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SwapIfBigEndianL@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfBigEndianL(ref uint value);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SwapIfBigEndianW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfBigEndianW(ref ushort valuea, ref ushort valueb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SwapIfLittleEndianL@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfLittleEndianL(ref uint value);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SwapIfLittleEndianW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfLittleEndianW(ref ushort valuea, ref ushort valueb);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonDACToVal@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonDACToVal(ushort dacval, int rangemin, int rangemax, ushort dacval_max, int scalefactor, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonDescGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_SysMonDescGet(int index, nint handleval);
            public static string BTICard_SysMonDescGet(int index, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_SysMonDescGet(index, handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonInit@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonInit(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonMaxRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonMaxRd(int index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonMinRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonMinRd(int index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonNomRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonNomRd(int index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonThresholdGet@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonThresholdGet(ref int enable, ref int min, ref int max, int index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonThresholdSet@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonThresholdSet(int enable, int min, int max, int index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonTypeGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SysMonTypeGet(int index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonUserStr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_SysMonUserStr(int value, int index, nint handleval);
            public static string BTICard_SysMonUserStr(int value, int index, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_SysMonUserStr(value, index, handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonValRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonValRd(int index, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonValStr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_SysMonValStr(int index, nint handleval);
            public static string BTICard_SysMonValStr(int index, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_SysMonValStr(index, handleval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_SysMonValToDAC@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SysMonValToDAC(int val, int rangemin, int rangemax, ushort dacval_max, int scalefactor, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TickTimerRemaining@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TickTimerRemaining(int timer);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TickTimerStart@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TickTimerStart(int milliseconds);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TickTimerValid@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TickTimerValid(int timer);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Timer64Rd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_Timer64Rd(ref uint valueh, ref uint valuel, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_Timer64Wr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Timer64Wr(uint valueh, uint valuel, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TimerClear@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_TimerClear(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TimerRd@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_TimerRd(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TimerResolution@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TimerResolution(int timerresol, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TimerResolutionEx@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_TimerResolutionEx(ushort timershift, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TimerStatus@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TimerStatus(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TimerWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_TimerWr(uint value, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMDriftMaxGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftMaxGet(ref uint driftptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMDriftMaxSet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftMaxSet(uint drift, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMDriftRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftRd(ref int driftptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMDriftRelWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftRelWr(int drift, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMDriftWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftWr(int drift, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMInputDelayCompGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputDelayCompGet(ref int delayptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMInputDelayCompSet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputDelayCompSet(int delay, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMInputThresholdAuto@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputThresholdAuto(int pinindex, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMInputThresholdGet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputThresholdGet(ref ushort dacval, int pinindex, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMInputThresholdSet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputThresholdSet(ushort dacval, int pinindex, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMIntTrigger@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIntTrigger(ref ulong timetagptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMIRIGControlRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGControlRd(ref uint ctrlptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMIRIGControlWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGControlWr(uint ctrlval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMIRIGYearsRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGYearsRd(ref uint year, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMIRIGYearsWr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGYearsWr(uint year, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMJumpThresholdGet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMJumpThresholdGet(ref uint drift, ref uint offset, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMJumpThresholdSet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMJumpThresholdSet(uint drift, uint offset, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMOffsetLastRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMOffsetLastRd(ref long offsetptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMReset@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMReset(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMSourceConfig@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMSourceConfig(uint sourcecfg, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMStatus@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMStatus(ref uint statusptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMSyncConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMSyncConfig(uint timeconfig, uint driftconfig, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMTimerRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRd(ref ulong valueptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMTimerRelWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRelWr(long value, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMTimerRolloverGet@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRolloverGet(ref ulong valueptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMTimerRolloverSet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRolloverSet(ulong value, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_TSMTimerWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerWr(ulong value, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValAsciiCmpi@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValAsciiCmpi(System.Text.StringBuilder str1, System.Text.StringBuilder str2);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValAsciiCpy@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValAsciiCpy(System.Text.StringBuilder strdest, string strsrc, int count);
            public static string BTICard_ValAsciiCpy(System.Text.StringBuilder strdest, string strsrc, int count) { return Marshal.PtrToStringAnsi(_BTICard_ValAsciiCpy(strdest, strsrc, count)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValAsciiTrimLead@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ValAsciiTrimLead(System.Text.StringBuilder buf);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValAsciiTrimTrail@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ValAsciiTrimTrail(System.Text.StringBuilder buf);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValFromAscii@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ValFromAscii(string asciistr, int radixval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValGetBits@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ValGetBits(uint oldvalue, int startbit, int endbit);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValIncAscii@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValIncAscii(System.Text.StringBuilder asciistr);
            public static string BTICard_ValIncAscii(System.Text.StringBuilder asciistr) { return Marshal.PtrToStringAnsi(_BTICard_ValIncAscii(asciistr)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValInccAscii@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValInccAscii(System.Text.StringBuilder asciistr);
            public static string BTICard_ValInccAscii(System.Text.StringBuilder asciistr) { return Marshal.PtrToStringAnsi(_BTICard_ValInccAscii(asciistr)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValIsLower@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValIsLower(int value);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValLenAscii@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValLenAscii(int numbits, int radixval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValPutBits@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ValPutBits(uint oldvalue, uint newfld, int startbit, int endbit);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValToAscii@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValToAscii(uint value, System.Text.StringBuilder asciistr, int numbits, int radixval);
            public static string BTICard_ValToAscii(uint value, System.Text.StringBuilder asciistr, int numbits, int radixval) { return Marshal.PtrToStringAnsi(_BTICard_ValToAscii(value, asciistr, numbits, radixval)); }
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_ValToUpper@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValToUpper(int value);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_VARRdL@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_VARRdL(ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_VARRdsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_VARRdW@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_VARRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_VARWrL@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARWrL(uint value, ushort addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_VARWrsW@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTICard_VARWrW@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARWrW(ushort value, ushort addrval, nint handleval);
        }

        private static class BTICARD_64
        {
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Add64", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Add64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_AddrDSP", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_AddrDSP(uint addr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_AddrHost", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_AddrHost(uint addr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_AsciiToMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_AsciiToMant(string str, ref uint mant, ref int exp);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_BCDToBin", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_BCDToBin(uint bcdval, int msb, int lsb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_BinToBCD", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_BinToBCD(uint oldbcdval, uint binval, int msb, int lsb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_BITConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_BITConfig(uint configval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_BITInitiate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_BITInitiate(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_BITStatusClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_BITStatusClear(uint statval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_BITStatusRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_BITStatusRd(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardClose", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardClose(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardCloseAll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardCloseAll();
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardGetInfo", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_CardGetInfo(ushort infotype, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardGetInfoEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardGetInfoEx(ref ushort bufmodel, ushort bufmodelcount, ref ushort buffeature, ushort buffeaturecount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardIsRunning", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardIsRunning(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardNop", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardNop(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardOpen", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardOpen(ref nint lphandle, int cardnum);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardOpenStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardOpenStr(ref nint lphandle, string cardstr);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardProductStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_CardProductStr(nint handleval);
            public static string BTICard_CardProductStr(nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardProductStr(handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardReserveRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_CardReserveRd(nint handleval);
            public static string BTICard_CardReserveRd(nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardReserveRd(handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardReserveWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardReserveWr(string str, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardReset(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardResetEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardResetEx(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardResume", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardResume(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardShadow", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardShadow(int shadowval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardStart(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardStop", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardStop(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardSyncEnable", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardSyncEnable(int enableflag, ushort syncmask, ushort pinpolarity, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardSyncValid", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CardSyncValid(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardSyncValidEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardSyncValidEx(ref ushort maskout, ushort maskin, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTest", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest(ushort level, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTest0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest0(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTest1", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest1(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTest2", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest2(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTest3", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTest3(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTrigger", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTrigger(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTriggerEnable", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTriggerEnable(int enableflag, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTriggerEnableEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTriggerEnableEx(int enableflag, ushort trigmask, ushort pinpolarity, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTriggerEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CardTriggerEx(ushort trigmask, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTriggerValid", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CardTriggerValid(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTriggerValidEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CardTriggerValidEx(ref ushort maskout, ushort maskin, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CardTypeStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_CardTypeStr(nint handleval);
            public static string BTICard_CardTypeStr(nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardTypeStr(handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARClr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARClr(ushort maskval, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ChDARGet(ushort maskval, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARRdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ChDARRdL(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARRdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARRdsW(ushort[] valueptr, ushort addrval, int countval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_ChDARRdW(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARSet(ushort maskval, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARWrL(uint value, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARWrsW(ushort[] valueptr, ushort addrval, int countval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ChDARWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ChDARWrW(ushort value, ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CISRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CISRd(ushort[] buf, ushort bufcount, int cistype, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CISWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CISWr(ushort[] buf, ushort bufcount, int cistype, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommBufRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CommBufRd(ushort offset, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommBufWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CommBufWr(ushort value, ushort offset, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommCall", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommCall(uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommChannelReconfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommChannelReconfig(uint chmask, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommCheck", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommCheck(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommDisable", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommDisable(ushort command, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommDisableEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommDisableEx(ushort command, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommEnable", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommEnable(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommExternSRQ", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommExternSRQ(uint chmask, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommFillW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommFillW(ushort value, uint addrval, ushort count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommProtocolFunc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommProtocolFunc(ushort opcode, ushort argcount, ref ushort argbuf, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommRdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_CommRdL(uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommRdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommRdsW(ushort[] valueptr, uint addrval, ushort count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_CommRdW(uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommRun", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommRun(ref ushort dataptr, ushort datacount, ref ushort codeptr, ushort codecount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CommWrL(uint value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CommWrsW(ushort[] valueptr, uint addrval, ushort count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CommWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_CommWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcCheck", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcCheck(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcGetInfo", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcGetInfo(ref uint valueptr, ushort infotype, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemRdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdL(ref uint valueptr, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemRdsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdsL(uint[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemRdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdsW(ushort[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemRdW(ref ushort valueptr, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrL(uint value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemWrsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrsL(uint[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrsW(ushort[] valueptr, uint addrval, uint count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoProcMemWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoProcMemWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_CoreOpen", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_CoreOpen(ref nint lphcore, int corenum, nint hcard);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARClr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARClr(ushort maskval, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DARGet(ushort maskval, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARRdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_DARRdL(ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARRdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_DARRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARSet(ushort maskval, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARWrL(uint value, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DARWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_DARWrW(ushort value, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Div", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Div(uint diva, uint divb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Div64", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Div64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DllUsageCount", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DllUsageCount();
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DriverInfoStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_DriverInfoStr();
            public static string BTICard_DriverInfoStr() { return Marshal.PtrToStringAnsi(_BTICard_DriverInfoStr()); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspBioClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspBioClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspBioRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspBioRd(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspBioSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspBioSet(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspIntfClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntfClear(ushort intmask, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspIntfRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntfRd(ushort intmask, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspIntmClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntmClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspIntmSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspIntmSet(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspXfClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspXfClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspXfRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspXfRd(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_DspXfSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_DspXfSet(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ErrDesc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ErrDesc(int errval, nint handleval);
            public static string BTICard_ErrDesc(int errval, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_ErrDesc(errval, handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ErrDescStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ErrDescStr(int errval, nint handleval);
            public static string BTICard_ErrDescStr(int errval, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_ErrDescStr(errval, handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ErrName", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ErrName(int errval, nint handleval);
            public static string BTICard_ErrName(int errval, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_ErrName(errval, handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_EventLogClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_EventLogClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_EventLogConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_EventLogConfig(ushort configval, ushort count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_EventLogRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_EventLogRd(ref ushort typevalval, ref uint infoval, ref int channel, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_EventLogStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_EventLogStatus(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExpandMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExpandMant(ref uint mant, ref int exp);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDinRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDinRd(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDinWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDinWr(int dinval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDIODirSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDIODirSet(int dionum, int dirval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDIOEnWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDIOEnWr(int dionum, int dioval, int dioen, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDIOBankConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDIOBankConfig(uint configtype, ushort configmask, int banknum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDIOMonConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDIOMonConfig(ushort rise_edge, ushort fall_edge, int banknum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDIORd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtDIORd(int dionum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtDIOWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtDIOWr(int dionum, int dioval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtLEDRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ExtLEDRd(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtLEDWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtLEDWr(int ledval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtStatusLEDRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtStatusLEDRd(ref int ledon, ref int ledcolor, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ExtStatusLEDWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ExtStatusLEDWr(int ledon, int ledcolor, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileClose", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_FileClose(nint handle);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileErr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileErr();
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileErrClr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_FileErrClr();
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileErrSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_FileErrSet();
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileOpenRead", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint BTICard_FileOpenRead(string filename);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileOpenWrite", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint BTICard_FileOpenWrite(string filename);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileRead", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileRead(nint handle, nint buffer, ushort count);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileSeek", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileSeek(nint handle, uint offset);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileTell", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_FileTell(nint handle);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_FileWrite", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_FileWrite(nint handle, nint buffer, ushort count);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_GetHigh", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_GetHigh(uint val);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_GetHighL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_GetHighL(ulong val);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_GetLow", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_GetLow(uint val);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_GetLowL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_GetLowL(ulong val);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_GetTickCount", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_GetTickCount();
            [DllImport(LIBNAME64, EntryPoint = "BTICard_GlobalRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_GlobalRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_GlobalWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_GlobalWrW(ushort value, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleInfo", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_HandleInfo(System.Text.StringBuilder cardstr, ref int cardnum, ref uint sizval, nint* vxdptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleInfoEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleInfoEx(ref uint valueptr, uint typeval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleIsCard", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleIsCard(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleIsCore", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleIsCore(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleMakeCard", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleMakeCard(ref nint lphcard, ref int lpcorenum, nint hcore);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleMakeCore", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleMakeCore(ref nint lphcore, int corenum, nint hcard);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleMakeRPC", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleMakeRPC(ref nint hcard_remote, ref uint lphrpc, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HandleOkay", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_HandleOkay(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HeapAlloc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapAlloc(int section, uint wordcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HeapAllocAll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapAllocAll(int section, ref uint wordcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HeapAllocEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapAllocEx(ushort configval, int section, uint wordcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HeapWipe", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HeapWipe(int section, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIFill", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIFill(ushort value, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIRdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_HPIRdL(ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIRdsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIRdsL(uint[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIRdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_HPIRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrL(uint value, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIWrsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrsL(uint[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_HPIWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_HPIWrW(ushort value, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IDRegRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_IDRegRd(int gate_array_num, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IDRegWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IDRegWr(ushort value, int gate_array_num, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IntClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntDisable", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntDisable(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntEnable", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntEnable(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntEnableCond", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntEnableCond(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint BTICard_IntGet(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntInstall", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntInstall(nint hevent, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IntReset(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IntUninstall", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IntUninstall(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IORdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IORdL(int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IORdsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IORdsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IORdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IORdsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IORdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_IORdW(int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IOWINRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_IOWINRdW(int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IOWINWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWINWrW(ushort value, int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IOWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrL(uint value, int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IOWrsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IOWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IOWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IOWrW(ushort value, int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGConfig(uint configval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldGetDays", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetDays(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldGetHours", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetHours(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldGetMicrosec", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetMicrosec(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldGetMillisec", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetMillisec(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldGetMin", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetMin(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldGetSec", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_IRIGFieldGetSec(uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldPutDays", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutDays(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldPutHours", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutHours(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldPutMicrosec", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutMicrosec(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldPutMillisec", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutMillisec(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldPutMin", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutMin(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGFieldPutSec", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGFieldPutSec(uint value, ref uint irigvalh, ref uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGInputThresholdGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGInputThresholdGet(ref ushort dacval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGInputThresholdSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGInputThresholdSet(ushort dacval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGRd(ref BTIIRIGTIME irigtime, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGRdEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGRdEx(ref ushort timebuf, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGSyncStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGSyncStatus(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGTimeBCDToBin", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeBCDToBin(ref uint timevalh, ref uint timevall, uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGTimeBCDToNanoBin", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeBCDToNanoBin(ref uint timevalh, ref uint timevall, uint irigvalh, uint irigvall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGTimeBinToBCD", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeBinToBCD(ref uint irigvalh, ref uint irigvall, uint timevalh, uint timevall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGTimeNanoBinToBCD", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_IRIGTimeNanoBinToBCD(ref uint irigvalh, ref uint irigvall, uint timevalh, uint timevall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGWr(ref BTIIRIGTIME irigtime, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_IRIGWrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_IRIGWrEx(ref ushort timebuf, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_KernIntStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_KernIntStatus(ref int valueptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_KernStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_KernStatus(ref uint valueptr, uint typeval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_KernStatusEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_KernStatusEx(ref uint valueptr, uint typeval, uint index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_MakeLong", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_MakeLong(ushort valh, ushort vall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_MakeQuad", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ulong BTICard_MakeQuad(uint valh, uint vall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_MakeWord", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_MakeWord(byte valh, byte vall);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_MantToAscii", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_MantToAscii(System.Text.StringBuilder buf, int mant, int exp);
            public static string BTICard_MantToAscii(System.Text.StringBuilder buf, int mant, int exp) { return Marshal.PtrToStringAnsi(_BTICard_MantToAscii(buf, mant, exp)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Mask", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Mask(uint dataval, ushort cntval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_MaxMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_MaxMant(ref uint mant, ref int exp);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Mod", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Mod(uint moda, uint modb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Mod64", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Mod64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Mul", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Mul(uint mula, uint mulb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Mul64", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Mul64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_NormalMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_NormalMant(ref uint mant, ref int exp);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_PortRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_PortRd(int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_PortWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_PortWr(ushort value, int addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ProgRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_ProgRdW(uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ProgWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ProgWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_PXIStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_PXIStatus(ref uint infoptr, ushort infotype, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMFill", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMFill(ushort value, uint addrval, uint countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMRdB", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_RAMRdB(uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMRdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_RAMRdL(uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMRdmL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdmL(uint[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMRdmW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdmW(ushort[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMRdsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMRdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMRdsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_RAMRdW(uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWipe", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWipe(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWipeEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWipeEx(ushort value, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWrB", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrB(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrL(uint value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWrmL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrmL(uint[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWrmW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrmW(ushort[] valueptr, uint[] addrptr, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWrsL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrsL(uint[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrsW(ushort[] valueptr, uint addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_RAMWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_RAMWrW(ushort value, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ReverseLong", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ReverseLong(uint value);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ReverseWord", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_ReverseWord(ushort value);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqBlkRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SeqBlkRd(ushort[] buf, uint bufcount, ref uint blkcnt, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqBlkRdEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SeqBlkRdEx(ushort[] buf, uint bufcount, uint maxblkcnt, ref uint blkcnt, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqCommRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqCommRd(ushort[] buf, ushort bufcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqConfig(uint configval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqConfigEx(uint configval, uint seqcount, ushort cardnum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqConfigExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqConfigExx(uint configval, uint seqaddr, uint seqcount, ushort cardnum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqDMARd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SeqDMARd(ushort[] buf, uint bufcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindCheckVersion", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindCheckVersion(ref ushort precord, ushort version);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindInit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindInit(ushort[] seqbuf, uint seqbufsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindMore1553", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindMore1553(ref SEQRECORDMORE1553* precmore, ref SEQRECORD1553 precbase);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindMore1553Ex", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindMore1553Ex(ref SEQRECORDMORE1553 precmore, ushort recordsize, ref SEQRECORD1553 precbase);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext(ref ushort* precord, ref ushort seqtype, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext1553", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext1553(ref SEQRECORD1553* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext1553Ex", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext1553Ex(ref SEQRECORD1553 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext429", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext429(ref SEQRECORD429* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext429Ex", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext429Ex(ref SEQRECORD429 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext708", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext708(ref SEQRECORD708* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext708Ex", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext708Ex(ref SEQRECORD708 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext717", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNext717(ref SEQRECORD717* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNext717Ex", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNext717Ex(ref SEQRECORD717 precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNextCSDB", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNextCSDB(ref SEQRECORDCSDB* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNextCSDBEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextCSDBEx(ref SEQRECORDCSDB precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNextDIO", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNextDIO(ref SEQRECORDDIO* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNextDIOEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextDIOEx(ref SEQRECORDDIO precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNextEBR", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern int BTICard_SeqFindNextEBR(ref SEQRECORDEBR* precord, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNextEBREx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextEBREx(ref SEQRECORDEBR precord, ushort recordsize, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqFindNextEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqFindNextEx(ushort[] precord, ushort recordcount, ref ushort seqtype, ref SEQFINDINFO sfinfo);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqInterval", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqInterval(int interval, int mode, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqIntervalEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqIntervalEx(ushort shiftval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqIsRunning", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqIsRunning(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqLogFrequency", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqLogFrequency(ushort logfreq, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqRd(ushort[] buf, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqRdEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SeqRdEx(ushort[] buf, ushort bufcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqResume", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqResume(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqStart(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqStatus(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SeqStop", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SeqStop(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Shl", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Shl(uint dataval, ushort cntval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Shr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_Shr(uint dataval, ushort cntval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SignMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SignMant(string str);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Sub64", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Sub64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SwapEndianL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapEndianL(ref uint value);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SwapEndianW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapEndianW(ref ushort valuea, ref ushort valueb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SwapIfBigEndianL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfBigEndianL(ref uint value);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SwapIfBigEndianW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfBigEndianW(ref ushort valuea, ref ushort valueb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SwapIfLittleEndianL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfLittleEndianL(ref uint value);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SwapIfLittleEndianW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_SwapIfLittleEndianW(ref ushort valuea, ref ushort valueb);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonDACToVal", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonDACToVal(ushort dacval, int rangemin, int rangemax, ushort dacval_max, int scalefactor, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonDescGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_SysMonDescGet(int index, nint handleval);
            public static string BTICard_SysMonDescGet(int index, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_SysMonDescGet(index, handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonInit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonInit(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonMaxRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonMaxRd(int index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonMinRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonMinRd(int index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonNomRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonNomRd(int index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonThresholdGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonThresholdGet(ref int enable, ref int min, ref int max, int index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonThresholdSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonThresholdSet(int enable, int min, int max, int index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonTypeGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_SysMonTypeGet(int index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonUserStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_SysMonUserStr(int value, int index, nint handleval);
            public static string BTICard_SysMonUserStr(int value, int index, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_SysMonUserStr(value, index, handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonValRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_SysMonValRd(int index, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonValStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_SysMonValStr(int index, nint handleval);
            public static string BTICard_SysMonValStr(int index, nint handleval) { return Marshal.PtrToStringAnsi(_BTICard_SysMonValStr(index, handleval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_SysMonValToDAC", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_SysMonValToDAC(int val, int rangemin, int rangemax, ushort dacval_max, int scalefactor, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TickTimerRemaining", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TickTimerRemaining(int timer);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TickTimerStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TickTimerStart(int milliseconds);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TickTimerValid", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TickTimerValid(int timer);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Timer64Rd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_Timer64Rd(ref uint valueh, ref uint valuel, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_Timer64Wr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_Timer64Wr(uint valueh, uint valuel, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TimerClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_TimerClear(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TimerRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_TimerRd(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TimerResolution", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TimerResolution(int timerresol, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TimerResolutionEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_TimerResolutionEx(ushort timershift, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TimerStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TimerStatus(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TimerWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_TimerWr(uint value, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMDriftMaxGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftMaxGet(ref uint driftptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMDriftMaxSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftMaxSet(uint drift, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMDriftRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftRd(ref int driftptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMDriftRelWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftRelWr(int drift, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMDriftWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMDriftWr(int drift, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMInputDelayCompGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputDelayCompGet(ref int delayptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMInputDelayCompSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputDelayCompSet(int delay, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMInputThresholdAuto", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputThresholdAuto(int pinindex, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMInputThresholdGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputThresholdGet(ref ushort dacval, int pinindex, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMInputThresholdSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMInputThresholdSet(ushort dacval, int pinindex, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMIntTrigger", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIntTrigger(ref ulong timetagptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMIRIGControlRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGControlRd(ref uint ctrlptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMIRIGControlWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGControlWr(uint ctrlval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMIRIGYearsRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGYearsRd(ref uint year, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMIRIGYearsWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMIRIGYearsWr(uint year, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMJumpThresholdGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMJumpThresholdGet(ref uint drift, ref uint offset, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMJumpThresholdSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMJumpThresholdSet(uint drift, uint offset, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMOffsetLastRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMOffsetLastRd(ref long offsetptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMReset(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMSourceConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMSourceConfig(uint sourcecfg, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMStatus(ref uint statusptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMSyncConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMSyncConfig(uint timeconfig, uint driftconfig, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMTimerRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRd(ref ulong valueptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMTimerRelWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRelWr(long value, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMTimerRolloverGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRolloverGet(ref ulong valueptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMTimerRolloverSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerRolloverSet(ulong value, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_TSMTimerWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_TSMTimerWr(ulong value, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValAsciiCmpi", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValAsciiCmpi(System.Text.StringBuilder str1, System.Text.StringBuilder str2);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValAsciiCpy", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValAsciiCpy(System.Text.StringBuilder strdest, string strsrc, int count);
            public static string BTICard_ValAsciiCpy(System.Text.StringBuilder strdest, string strsrc, int count) { return Marshal.PtrToStringAnsi(_BTICard_ValAsciiCpy(strdest, strsrc, count)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValAsciiTrimLead", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ValAsciiTrimLead(System.Text.StringBuilder buf);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValAsciiTrimTrail", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_ValAsciiTrimTrail(System.Text.StringBuilder buf);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValFromAscii", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ValFromAscii(string asciistr, int radixval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValGetBits", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ValGetBits(uint oldvalue, int startbit, int endbit);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValIncAscii", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValIncAscii(System.Text.StringBuilder asciistr);
            public static string BTICard_ValIncAscii(System.Text.StringBuilder asciistr) { return Marshal.PtrToStringAnsi(_BTICard_ValIncAscii(asciistr)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValInccAscii", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValInccAscii(System.Text.StringBuilder asciistr);
            public static string BTICard_ValInccAscii(System.Text.StringBuilder asciistr) { return Marshal.PtrToStringAnsi(_BTICard_ValInccAscii(asciistr)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValIsLower", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValIsLower(int value);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValLenAscii", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValLenAscii(int numbits, int radixval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValPutBits", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_ValPutBits(uint oldvalue, uint newfld, int startbit, int endbit);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValToAscii", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTICard_ValToAscii(uint value, System.Text.StringBuilder asciistr, int numbits, int radixval);
            public static string BTICard_ValToAscii(uint value, System.Text.StringBuilder asciistr, int numbits, int radixval) { return Marshal.PtrToStringAnsi(_BTICard_ValToAscii(value, asciistr, numbits, radixval)); }
            [DllImport(LIBNAME64, EntryPoint = "BTICard_ValToUpper", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTICard_ValToUpper(int value);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_VARRdL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTICard_VARRdL(ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_VARRdsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_VARRdW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTICard_VARRdW(ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_VARWrL", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARWrL(uint value, ushort addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_VARWrsW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTICard_VARWrW", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTICard_VARWrW(ushort value, ushort addrval, nint handleval);
        }

        public static void BTICard_Add64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_Add64(ref resulth, ref resultl, valah, valal, valbh, valbl);
            else
                BTICARD_64.BTICard_Add64(ref resulth, ref resultl, valah, valal, valbh, valbl);
        }
        public static uint BTICard_AddrDSP(uint addr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_AddrDSP(addr, handleval);
            else
                return BTICARD_64.BTICard_AddrDSP(addr, handleval);
        }
        public static uint BTICard_AddrHost(uint addr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_AddrHost(addr, handleval);
            else
                return BTICARD_64.BTICard_AddrHost(addr, handleval);
        }
        public static int BTICard_AsciiToMant(string str, ref uint mant, ref int exp)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_AsciiToMant(str, ref mant, ref exp);
            else
                return BTICARD_64.BTICard_AsciiToMant(str, ref mant, ref exp);
        }
        public static uint BTICard_BCDToBin(uint bcdval, int msb, int lsb)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_BCDToBin(bcdval, msb, lsb);
            else
                return BTICARD_64.BTICard_BCDToBin(bcdval, msb, lsb);
        }
        public static uint BTICard_BinToBCD(uint oldbcdval, uint binval, int msb, int lsb)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_BinToBCD(oldbcdval, binval, msb, lsb);
            else
                return BTICARD_64.BTICard_BinToBCD(oldbcdval, binval, msb, lsb);
        }
        public static int BTICard_BITConfig(uint configval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_BITConfig(configval, handleval);
            else
                return BTICARD_64.BTICard_BITConfig(configval, handleval);
        }
        public static int BTICard_BITInitiate(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_BITInitiate(handleval);
            else
                return BTICARD_64.BTICard_BITInitiate(handleval);
        }
        public static int BTICard_BITStatusClear(uint statval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_BITStatusClear(statval, handleval);
            else
                return BTICARD_64.BTICard_BITStatusClear(statval, handleval);
        }
        public static uint BTICard_BITStatusRd(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_BITStatusRd(handleval);
            else
                return BTICARD_64.BTICard_BITStatusRd(handleval);
        }
        public static int BTICard_CardClose(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardClose(handleval);
            else
                return BTICARD_64.BTICard_CardClose(handleval);
        }
        public static int BTICard_CardCloseAll()
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardCloseAll();
            else
                return BTICARD_64.BTICard_CardCloseAll();
        }
        public static uint BTICard_CardGetInfo(ushort infotype, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardGetInfo(infotype, channum, handleval);
            else
                return BTICARD_64.BTICard_CardGetInfo(infotype, channum, handleval);
        }
        public static int BTICard_CardGetInfoEx(ref ushort bufmodel, ushort bufmodelcount, ref ushort buffeature, ushort buffeaturecount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardGetInfoEx(ref bufmodel, bufmodelcount, ref buffeature, buffeaturecount, handleval);
            else
                return BTICARD_64.BTICard_CardGetInfoEx(ref bufmodel, bufmodelcount, ref buffeature, buffeaturecount, handleval);
        }
        public static int BTICard_CardIsRunning(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardIsRunning(handleval);
            else
                return BTICARD_64.BTICard_CardIsRunning(handleval);
        }
        public static int BTICard_CardNop(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardNop(handleval);
            else
                return BTICARD_64.BTICard_CardNop(handleval);
        }
        public static int BTICard_CardOpen(ref nint lphandle, int cardnum)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardOpen(ref lphandle, cardnum);
            else
                return BTICARD_64.BTICard_CardOpen(ref lphandle, cardnum);
        }
        public static int BTICard_CardOpenStr(ref nint lphandle, string cardstr)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardOpenStr(ref lphandle, cardstr);
            else
                return BTICARD_64.BTICard_CardOpenStr(ref lphandle, cardstr);
        }
        public static unsafe string BTICard_CardProductStr(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardProductStr(handleval);
            else
                return BTICARD_64.BTICard_CardProductStr(handleval);
        }
        public static unsafe string BTICard_CardReserveRd(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardReserveRd(handleval);
            else
                return BTICARD_64.BTICard_CardReserveRd(handleval);
        }
        public static int BTICard_CardReserveWr(string str, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardReserveWr(str, handleval);
            else
                return BTICARD_64.BTICard_CardReserveWr(str, handleval);
        }
        public static void BTICard_CardReset(nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CardReset(handleval);
            else
                BTICARD_64.BTICard_CardReset(handleval);
        }
        public static void BTICard_CardResetEx(nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CardResetEx(handleval);
            else
                BTICARD_64.BTICard_CardResetEx(handleval);
        }
        public static int BTICard_CardResume(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardResume(handleval);
            else
                return BTICARD_64.BTICard_CardResume(handleval);
        }
        public static int BTICard_CardShadow(int shadowval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardShadow(shadowval, handleval);
            else
                return BTICARD_64.BTICard_CardShadow(shadowval, handleval);
        }
        public static int BTICard_CardStart(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardStart(handleval);
            else
                return BTICARD_64.BTICard_CardStart(handleval);
        }
        public static int BTICard_CardStop(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardStop(handleval);
            else
                return BTICARD_64.BTICard_CardStop(handleval);
        }
        public static void BTICard_CardSyncEnable(int enableflag, ushort syncmask, ushort pinpolarity, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CardSyncEnable(enableflag, syncmask, pinpolarity, handleval);
            else
                BTICARD_64.BTICard_CardSyncEnable(enableflag, syncmask, pinpolarity, handleval);
        }
        public static ushort BTICard_CardSyncValid(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardSyncValid(handleval);
            else
                return BTICARD_64.BTICard_CardSyncValid(handleval);
        }
        public static int BTICard_CardSyncValidEx(ref ushort maskout, ushort maskin, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardSyncValidEx(ref maskout, maskin, handleval);
            else
                return BTICARD_64.BTICard_CardSyncValidEx(ref maskout, maskin, handleval);
        }
        public static int BTICard_CardTest(ushort level, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTest(level, handleval);
            else
                return BTICARD_64.BTICard_CardTest(level, handleval);
        }
        public static int BTICard_CardTest0(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTest0(handleval);
            else
                return BTICARD_64.BTICard_CardTest0(handleval);
        }
        public static int BTICard_CardTest1(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTest1(handleval);
            else
                return BTICARD_64.BTICard_CardTest1(handleval);
        }
        public static int BTICard_CardTest2(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTest2(handleval);
            else
                return BTICARD_64.BTICard_CardTest2(handleval);
        }
        public static int BTICard_CardTest3(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTest3(handleval);
            else
                return BTICARD_64.BTICard_CardTest3(handleval);
        }
        public static void BTICard_CardTrigger(nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CardTrigger(handleval);
            else
                BTICARD_64.BTICard_CardTrigger(handleval);
        }
        public static void BTICard_CardTriggerEnable(int enableflag, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CardTriggerEnable(enableflag, handleval);
            else
                BTICARD_64.BTICard_CardTriggerEnable(enableflag, handleval);
        }
        public static void BTICard_CardTriggerEnableEx(int enableflag, ushort trigmask, ushort pinpolarity, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CardTriggerEnableEx(enableflag, trigmask, pinpolarity, handleval);
            else
                BTICARD_64.BTICard_CardTriggerEnableEx(enableflag, trigmask, pinpolarity, handleval);
        }
        public static void BTICard_CardTriggerEx(ushort trigmask, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CardTriggerEx(trigmask, handleval);
            else
                BTICARD_64.BTICard_CardTriggerEx(trigmask, handleval);
        }
        public static ushort BTICard_CardTriggerValid(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTriggerValid(handleval);
            else
                return BTICARD_64.BTICard_CardTriggerValid(handleval);
        }
        public static int BTICard_CardTriggerValidEx(ref ushort maskout, ushort maskin, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTriggerValidEx(ref maskout, maskin, handleval);
            else
                return BTICARD_64.BTICard_CardTriggerValidEx(ref maskout, maskin, handleval);
        }
        public static unsafe string BTICard_CardTypeStr(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CardTypeStr(handleval);
            else
                return BTICARD_64.BTICard_CardTypeStr(handleval);
        }
        public static void BTICard_ChDARClr(ushort maskval, ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ChDARClr(maskval, addrval, channum, handleval);
            else
                BTICARD_64.BTICard_ChDARClr(maskval, addrval, channum, handleval);
        }
        public static int BTICard_ChDARGet(ushort maskval, ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ChDARGet(maskval, addrval, channum, handleval);
            else
                return BTICARD_64.BTICard_ChDARGet(maskval, addrval, channum, handleval);
        }
        public static uint BTICard_ChDARRdL(ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ChDARRdL(addrval, channum, handleval);
            else
                return BTICARD_64.BTICard_ChDARRdL(addrval, channum, handleval);
        }
        public static void BTICard_ChDARRdsW(ushort[] valueptr, ushort addrval, int countval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ChDARRdsW(valueptr, addrval, countval, channum, handleval);
            else
                BTICARD_64.BTICard_ChDARRdsW(valueptr, addrval, countval, channum, handleval);
        }
        public static ushort BTICard_ChDARRdW(ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ChDARRdW(addrval, channum, handleval);
            else
                return BTICARD_64.BTICard_ChDARRdW(addrval, channum, handleval);
        }
        public static void BTICard_ChDARSet(ushort maskval, ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ChDARSet(maskval, addrval, channum, handleval);
            else
                BTICARD_64.BTICard_ChDARSet(maskval, addrval, channum, handleval);
        }
        public static void BTICard_ChDARWrL(uint value, ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ChDARWrL(value, addrval, channum, handleval);
            else
                BTICARD_64.BTICard_ChDARWrL(value, addrval, channum, handleval);
        }
        public static void BTICard_ChDARWrsW(ushort[] valueptr, ushort addrval, int countval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ChDARWrsW(valueptr, addrval, countval, channum, handleval);
            else
                BTICARD_64.BTICard_ChDARWrsW(valueptr, addrval, countval, channum, handleval);
        }
        public static void BTICard_ChDARWrW(ushort value, ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ChDARWrW(value, addrval, channum, handleval);
            else
                BTICARD_64.BTICard_ChDARWrW(value, addrval, channum, handleval);
        }
        public static int BTICard_CISRd(ushort[] buf, ushort bufcount, int cistype, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CISRd(buf, bufcount, cistype, handleval);
            else
                return BTICARD_64.BTICard_CISRd(buf, bufcount, cistype, handleval);
        }
        public static int BTICard_CISWr(ushort[] buf, ushort bufcount, int cistype, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CISWr(buf, bufcount, cistype, handleval);
            else
                return BTICARD_64.BTICard_CISWr(buf, bufcount, cistype, handleval);
        }
        public static ushort BTICard_CommBufRd(ushort offset, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommBufRd(offset, handleval);
            else
                return BTICARD_64.BTICard_CommBufRd(offset, handleval);
        }
        public static void BTICard_CommBufWr(ushort value, ushort offset, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CommBufWr(value, offset, handleval);
            else
                BTICARD_64.BTICard_CommBufWr(value, offset, handleval);
        }
        public static int BTICard_CommCall(uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommCall(addrval, handleval);
            else
                return BTICARD_64.BTICard_CommCall(addrval, handleval);
        }
        public static int BTICard_CommChannelReconfig(uint chmask, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommChannelReconfig(chmask, handleval);
            else
                return BTICARD_64.BTICard_CommChannelReconfig(chmask, handleval);
        }
        public static int BTICard_CommCheck(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommCheck(handleval);
            else
                return BTICARD_64.BTICard_CommCheck(handleval);
        }
        public static int BTICard_CommDisable(ushort command, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommDisable(command, handleval);
            else
                return BTICARD_64.BTICard_CommDisable(command, handleval);
        }
        public static int BTICard_CommDisableEx(ushort command, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommDisableEx(command, handleval);
            else
                return BTICARD_64.BTICard_CommDisableEx(command, handleval);
        }
        public static int BTICard_CommEnable(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommEnable(handleval);
            else
                return BTICARD_64.BTICard_CommEnable(handleval);
        }
        public static int BTICard_CommExternSRQ(uint chmask, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommExternSRQ(chmask, handleval);
            else
                return BTICARD_64.BTICard_CommExternSRQ(chmask, handleval);
        }
        public static int BTICard_CommFillW(ushort value, uint addrval, ushort count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommFillW(value, addrval, count, handleval);
            else
                return BTICARD_64.BTICard_CommFillW(value, addrval, count, handleval);
        }
        public static int BTICard_CommProtocolFunc(ushort opcode, ushort argcount, ref ushort argbuf, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommProtocolFunc(opcode, argcount, ref argbuf, handleval);
            else
                return BTICARD_64.BTICard_CommProtocolFunc(opcode, argcount, ref argbuf, handleval);
        }
        public static uint BTICard_CommRdL(uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommRdL(addrval, handleval);
            else
                return BTICARD_64.BTICard_CommRdL(addrval, handleval);
        }
        public static int BTICard_CommRdsW(ushort[] valueptr, uint addrval, ushort count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommRdsW(valueptr, addrval, count, handleval);
            else
                return BTICARD_64.BTICard_CommRdsW(valueptr, addrval, count, handleval);
        }
        public static ushort BTICard_CommRdW(uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_CommRdW(addrval, handleval);
        }
        public static int BTICard_CommRun(ref ushort dataptr, ushort datacount, ref ushort codeptr, ushort codecount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommRun(ref dataptr, datacount, ref codeptr, codecount, handleval);
            else
                return BTICARD_64.BTICard_CommRun(ref dataptr, datacount, ref codeptr, codecount, handleval);
        }
        public static void BTICard_CommWrL(uint value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CommWrL(value, addrval, handleval);
            else
                BTICARD_64.BTICard_CommWrL(value, addrval, handleval);
        }
        public static int BTICard_CommWrsW(ushort[] valueptr, uint addrval, ushort count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CommWrsW(valueptr, addrval, count, handleval);
            else
                return BTICARD_64.BTICard_CommWrsW(valueptr, addrval, count, handleval);
        }
        public static void BTICard_CommWrW(ushort value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_CommWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_CommWrW(value, addrval, handleval);
        }
        public static int BTICard_CoProcCheck(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcCheck(handleval);
            else
                return BTICARD_64.BTICard_CoProcCheck(handleval);
        }
        public static int BTICard_CoProcGetInfo(ref uint valueptr, ushort infotype, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcGetInfo(ref valueptr, infotype, handleval);
            else
                return BTICARD_64.BTICard_CoProcGetInfo(ref valueptr, infotype, handleval);
        }
        public static int BTICard_CoProcMemRdL(ref uint valueptr, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemRdL(ref valueptr, addrval, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemRdL(ref valueptr, addrval, handleval);
        }
        public static int BTICard_CoProcMemRdsL(uint[] valueptr, uint addrval, uint count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemRdsL(valueptr, addrval, count, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemRdsL(valueptr, addrval, count, handleval);
        }
        public static int BTICard_CoProcMemRdsW(ushort[] valueptr, uint addrval, uint count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemRdsW(valueptr, addrval, count, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemRdsW(valueptr, addrval, count, handleval);
        }
        public static int BTICard_CoProcMemRdW(ref ushort valueptr, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemRdW(ref valueptr, addrval, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemRdW(ref valueptr, addrval, handleval);
        }
        public static int BTICard_CoProcMemWrL(uint value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemWrL(value, addrval, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemWrL(value, addrval, handleval);
        }
        public static int BTICard_CoProcMemWrsL(uint[] valueptr, uint addrval, uint count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemWrsL(valueptr, addrval, count, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemWrsL(valueptr, addrval, count, handleval);
        }
        public static int BTICard_CoProcMemWrsW(ushort[] valueptr, uint addrval, uint count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemWrsW(valueptr, addrval, count, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemWrsW(valueptr, addrval, count, handleval);
        }
        public static int BTICard_CoProcMemWrW(ushort value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoProcMemWrW(value, addrval, handleval);
            else
                return BTICARD_64.BTICard_CoProcMemWrW(value, addrval, handleval);
        }
        public static int BTICard_CoreOpen(ref nint lphcore, int corenum, nint hcard)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_CoreOpen(ref lphcore, corenum, hcard);
            else
                return BTICARD_64.BTICard_CoreOpen(ref lphcore, corenum, hcard);
        }
        public static void BTICard_DARClr(ushort maskval, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_DARClr(maskval, addrval, handleval);
            else
                BTICARD_64.BTICard_DARClr(maskval, addrval, handleval);
        }
        public static int BTICard_DARGet(ushort maskval, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DARGet(maskval, addrval, handleval);
            else
                return BTICARD_64.BTICard_DARGet(maskval, addrval, handleval);
        }
        public static uint BTICard_DARRdL(ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DARRdL(addrval, handleval);
            else
                return BTICARD_64.BTICard_DARRdL(addrval, handleval);
        }
        public static void BTICard_DARRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_DARRdsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_DARRdsW(valueptr, addrval, countval, handleval);
        }
        public static ushort BTICard_DARRdW(ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DARRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_DARRdW(addrval, handleval);
        }
        public static void BTICard_DARSet(ushort maskval, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_DARSet(maskval, addrval, handleval);
            else
                BTICARD_64.BTICard_DARSet(maskval, addrval, handleval);
        }
        public static void BTICard_DARWrL(uint value, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_DARWrL(value, addrval, handleval);
            else
                BTICARD_64.BTICard_DARWrL(value, addrval, handleval);
        }
        public static void BTICard_DARWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_DARWrsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_DARWrsW(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_DARWrW(ushort value, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_DARWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_DARWrW(value, addrval, handleval);
        }
        public static uint BTICard_Div(uint diva, uint divb)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_Div(diva, divb);
            else
                return BTICARD_64.BTICard_Div(diva, divb);
        }
        public static void BTICard_Div64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_Div64(ref resulth, ref resultl, valah, valal, valbh, valbl);
            else
                BTICARD_64.BTICard_Div64(ref resulth, ref resultl, valah, valal, valbh, valbl);
        }
        public static int BTICard_DllUsageCount()
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DllUsageCount();
            else
                return BTICARD_64.BTICard_DllUsageCount();
        }
        public static unsafe string BTICard_DriverInfoStr()
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DriverInfoStr();
            else
                return BTICARD_64.BTICard_DriverInfoStr();
        }
        public static int BTICard_DspBioClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspBioClear(handleval);
            else
                return BTICARD_64.BTICard_DspBioClear(handleval);
        }
        public static int BTICard_DspBioRd(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspBioRd(handleval);
            else
                return BTICARD_64.BTICard_DspBioRd(handleval);
        }
        public static int BTICard_DspBioSet(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspBioSet(handleval);
            else
                return BTICARD_64.BTICard_DspBioSet(handleval);
        }
        public static int BTICard_DspIntfClear(ushort intmask, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspIntfClear(intmask, handleval);
            else
                return BTICARD_64.BTICard_DspIntfClear(intmask, handleval);
        }
        public static int BTICard_DspIntfRd(ushort intmask, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspIntfRd(intmask, handleval);
            else
                return BTICARD_64.BTICard_DspIntfRd(intmask, handleval);
        }
        public static int BTICard_DspIntmClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspIntmClear(handleval);
            else
                return BTICARD_64.BTICard_DspIntmClear(handleval);
        }
        public static int BTICard_DspIntmSet(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspIntmSet(handleval);
            else
                return BTICARD_64.BTICard_DspIntmSet(handleval);
        }
        public static int BTICard_DspXfClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspXfClear(handleval);
            else
                return BTICARD_64.BTICard_DspXfClear(handleval);
        }
        public static int BTICard_DspXfRd(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspXfRd(handleval);
            else
                return BTICARD_64.BTICard_DspXfRd(handleval);
        }
        public static int BTICard_DspXfSet(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_DspXfSet(handleval);
            else
                return BTICARD_64.BTICard_DspXfSet(handleval);
        }
        public static unsafe string BTICard_ErrDesc(int errval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ErrDesc(errval, handleval);
            else
                return BTICARD_64.BTICard_ErrDesc(errval, handleval);
        }
        public static unsafe string BTICard_ErrDescStr(int errval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ErrDescStr(errval, handleval);
            else
                return BTICARD_64.BTICard_ErrDescStr(errval, handleval);
        }
        public static unsafe string BTICard_ErrName(int errval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ErrName(errval, handleval);
            else
                return BTICARD_64.BTICard_ErrName(errval, handleval);
        }
        public static int BTICard_EventLogClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_EventLogClear(handleval);
            else
                return BTICARD_64.BTICard_EventLogClear(handleval);
        }
        public static int BTICard_EventLogConfig(ushort configval, ushort count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_EventLogConfig(configval, count, handleval);
            else
                return BTICARD_64.BTICard_EventLogConfig(configval, count, handleval);
        }
        public static uint BTICard_EventLogRd(ref ushort typevalval, ref uint infoval, ref int channel, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_EventLogRd(ref typevalval, ref infoval, ref channel, handleval);
            else
                return BTICARD_64.BTICard_EventLogRd(ref typevalval, ref infoval, ref channel, handleval);
        }
        public static int BTICard_EventLogStatus(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_EventLogStatus(handleval);
            else
                return BTICARD_64.BTICard_EventLogStatus(handleval);
        }
        public static void BTICard_ExpandMant(ref uint mant, ref int exp)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExpandMant(ref mant, ref exp);
            else
                BTICARD_64.BTICard_ExpandMant(ref mant, ref exp);
        }
        public static int BTICard_ExtDinRd(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ExtDinRd(handleval);
            else
                return BTICARD_64.BTICard_ExtDinRd(handleval);
        }
        public static void BTICard_ExtDinWr(int dinval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExtDinWr(dinval, handleval);
            else
                BTICARD_64.BTICard_ExtDinWr(dinval, handleval);
        }
        public static void BTICard_ExtDIODirSet(int dionum, int dirval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExtDIODirSet(dionum, dirval, handleval);
            else
                BTICARD_64.BTICard_ExtDIODirSet(dionum, dirval, handleval);
        }
        public static void BTICard_ExtDIOEnWr(int dionum, int dioval, int dioen, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExtDIOEnWr(dionum, dioval, dioen, handleval);
            else
                BTICARD_64.BTICard_ExtDIOEnWr(dionum, dioval, dioen, handleval);
        }
        public static int BTICard_ExtDIOBankConfig(uint configtype, ushort configmask, int banknum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ExtDIOBankConfig(configtype, configmask, banknum, handleval);
            else
                return BTICARD_64.BTICard_ExtDIOBankConfig(configtype, configmask, banknum, handleval);
        }
        public static int BTICard_ExtDIOMonConfig(ushort rise_edge, ushort fall_edge, int banknum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ExtDIOMonConfig(rise_edge, fall_edge, banknum, handleval);
            else
                return BTICARD_64.BTICard_ExtDIOMonConfig(rise_edge, fall_edge, banknum, handleval);
        }
        public static int BTICard_ExtDIORd(int dionum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ExtDIORd(dionum, handleval);
            else
                return BTICARD_64.BTICard_ExtDIORd(dionum, handleval);
        }
        public static void BTICard_ExtDIOWr(int dionum, int dioval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExtDIOWr(dionum, dioval, handleval);
            else
                BTICARD_64.BTICard_ExtDIOWr(dionum, dioval, handleval);
        }
        public static int BTICard_ExtLEDRd(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ExtLEDRd(handleval);
            else
                return BTICARD_64.BTICard_ExtLEDRd(handleval);
        }
        public static void BTICard_ExtLEDWr(int ledval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExtLEDWr(ledval, handleval);
            else
                BTICARD_64.BTICard_ExtLEDWr(ledval, handleval);
        }
        public static void BTICard_ExtStatusLEDRd(ref int ledon, ref int ledcolor, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExtStatusLEDRd(ref ledon, ref ledcolor, handleval);
            else
                BTICARD_64.BTICard_ExtStatusLEDRd(ref ledon, ref ledcolor, handleval);
        }
        public static void BTICard_ExtStatusLEDWr(int ledon, int ledcolor, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ExtStatusLEDWr(ledon, ledcolor, handleval);
            else
                BTICARD_64.BTICard_ExtStatusLEDWr(ledon, ledcolor, handleval);
        }
        public static void BTICard_FileClose(nint handle)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_FileClose(handle);
            else
                BTICARD_64.BTICard_FileClose(handle);
        }
        public static int BTICard_FileErr()
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_FileErr();
            else
                return BTICARD_64.BTICard_FileErr();
        }
        public static void BTICard_FileErrClr()
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_FileErrClr();
            else
                BTICARD_64.BTICard_FileErrClr();
        }
        public static void BTICard_FileErrSet()
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_FileErrSet();
            else
                BTICARD_64.BTICard_FileErrSet();
        }
        public static nint BTICard_FileOpenRead(string filename)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_FileOpenRead(filename);
            else
                return BTICARD_64.BTICard_FileOpenRead(filename);
        }
        public static nint BTICard_FileOpenWrite(string filename)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_FileOpenWrite(filename);
            else
                return BTICARD_64.BTICard_FileOpenWrite(filename);
        }
        public static int BTICard_FileRead(nint handle, nint buffer, ushort count)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_FileRead(handle, buffer, count);
            else
                return BTICARD_64.BTICard_FileRead(handle, buffer, count);
        }
        public static int BTICard_FileSeek(nint handle, uint offset)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_FileSeek(handle, offset);
            else
                return BTICARD_64.BTICard_FileSeek(handle, offset);
        }
        public static uint BTICard_FileTell(nint handle)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_FileTell(handle);
            else
                return BTICARD_64.BTICard_FileTell(handle);
        }
        public static int BTICard_FileWrite(nint handle, nint buffer, ushort count)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_FileWrite(handle, buffer, count);
            else
                return BTICARD_64.BTICard_FileWrite(handle, buffer, count);
        }
        public static ushort BTICard_GetHigh(uint val)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_GetHigh(val);
            else
                return BTICARD_64.BTICard_GetHigh(val);
        }
        public static uint BTICard_GetHighL(ulong val)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_GetHighL(val);
            else
                return BTICARD_64.BTICard_GetHighL(val);
        }
        public static ushort BTICard_GetLow(uint val)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_GetLow(val);
            else
                return BTICARD_64.BTICard_GetLow(val);
        }
        public static uint BTICard_GetLowL(ulong val)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_GetLowL(val);
            else
                return BTICARD_64.BTICard_GetLowL(val);
        }
        public static uint BTICard_GetTickCount()
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_GetTickCount();
            else
                return BTICARD_64.BTICard_GetTickCount();
        }
        public static ushort BTICard_GlobalRdW(ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_GlobalRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_GlobalRdW(addrval, handleval);
        }
        public static void BTICard_GlobalWrW(ushort value, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_GlobalWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_GlobalWrW(value, addrval, handleval);
        }
        unsafe public static int BTICard_HandleInfo(System.Text.StringBuilder cardstr, ref int cardnum, ref uint sizval, nint* vxdptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleInfo(cardstr, ref cardnum, ref sizval, vxdptr, handleval);
            else
                return BTICARD_64.BTICard_HandleInfo(cardstr, ref cardnum, ref sizval, vxdptr, handleval);
        }
        public static int BTICard_HandleInfoEx(ref uint valueptr, uint typeval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleInfoEx(ref valueptr, typeval, handleval);
            else
                return BTICARD_64.BTICard_HandleInfoEx(ref valueptr, typeval, handleval);
        }
        public static int BTICard_HandleIsCard(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleIsCard(handleval);
            else
                return BTICARD_64.BTICard_HandleIsCard(handleval);
        }
        public static int BTICard_HandleIsCore(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleIsCore(handleval);
            else
                return BTICARD_64.BTICard_HandleIsCore(handleval);
        }
        public static int BTICard_HandleMakeCard(ref nint lphcard, ref int lpcorenum, nint hcore)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleMakeCard(ref lphcard, ref lpcorenum, hcore);
            else
                return BTICARD_64.BTICard_HandleMakeCard(ref lphcard, ref lpcorenum, hcore);
        }
        public static int BTICard_HandleMakeCore(ref nint lphcore, int corenum, nint hcard)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleMakeCore(ref lphcore, corenum, hcard);
            else
                return BTICARD_64.BTICard_HandleMakeCore(ref lphcore, corenum, hcard);
        }
        public static int BTICard_HandleMakeRPC(ref nint hcard_remote, ref uint lphrpc, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleMakeRPC(ref hcard_remote, ref lphrpc, handleval);
            else
                return BTICARD_64.BTICard_HandleMakeRPC(ref hcard_remote, ref lphrpc, handleval);
        }
        public static int BTICard_HandleOkay(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HandleOkay(handleval);
            else
                return BTICARD_64.BTICard_HandleOkay(handleval);
        }
        public static uint BTICard_HeapAlloc(int section, uint wordcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HeapAlloc(section, wordcount, handleval);
            else
                return BTICARD_64.BTICard_HeapAlloc(section, wordcount, handleval);
        }
        public static uint BTICard_HeapAllocAll(int section, ref uint wordcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HeapAllocAll(section, ref wordcount, handleval);
            else
                return BTICARD_64.BTICard_HeapAllocAll(section, ref wordcount, handleval);
        }
        public static uint BTICard_HeapAllocEx(ushort configval, int section, uint wordcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HeapAllocEx(configval, section, wordcount, handleval);
            else
                return BTICARD_64.BTICard_HeapAllocEx(configval, section, wordcount, handleval);
        }
        public static uint BTICard_HeapWipe(int section, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HeapWipe(section, handleval);
            else
                return BTICARD_64.BTICard_HeapWipe(section, handleval);
        }
        public static void BTICard_HPIFill(ushort value, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_HPIFill(value, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_HPIFill(value, addrval, countval, handleval);
        }
        public static uint BTICard_HPIRdL(ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HPIRdL(addrval, handleval);
            else
                return BTICARD_64.BTICard_HPIRdL(addrval, handleval);
        }
        public static void BTICard_HPIRdsL(uint[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_HPIRdsL(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_HPIRdsL(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_HPIRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_HPIRdsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_HPIRdsW(valueptr, addrval, countval, handleval);
        }
        public static ushort BTICard_HPIRdW(ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_HPIRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_HPIRdW(addrval, handleval);
        }
        public static void BTICard_HPIWrL(uint value, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_HPIWrL(value, addrval, handleval);
            else
                BTICARD_64.BTICard_HPIWrL(value, addrval, handleval);
        }
        public static void BTICard_HPIWrsL(uint[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_HPIWrsL(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_HPIWrsL(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_HPIWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_HPIWrsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_HPIWrsW(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_HPIWrW(ushort value, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_HPIWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_HPIWrW(value, addrval, handleval);
        }
        public static ushort BTICard_IDRegRd(int gate_array_num, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IDRegRd(gate_array_num, handleval);
            else
                return BTICARD_64.BTICard_IDRegRd(gate_array_num, handleval);
        }
        public static void BTICard_IDRegWr(ushort value, int gate_array_num, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IDRegWr(value, gate_array_num, handleval);
            else
                BTICARD_64.BTICard_IDRegWr(value, gate_array_num, handleval);
        }
        public static void BTICard_IntClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IntClear(handleval);
            else
                BTICARD_64.BTICard_IntClear(handleval);
        }
        public static int BTICard_IntDisable(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IntDisable(handleval);
            else
                return BTICARD_64.BTICard_IntDisable(handleval);
        }
        public static int BTICard_IntEnable(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IntEnable(handleval);
            else
                return BTICARD_64.BTICard_IntEnable(handleval);
        }
        public static int BTICard_IntEnableCond(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IntEnableCond(handleval);
            else
                return BTICARD_64.BTICard_IntEnableCond(handleval);
        }
        public static nint BTICard_IntGet(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IntGet(handleval);
            else
                return BTICARD_64.BTICard_IntGet(handleval);
        }
        public static int BTICard_IntInstall(nint hevent, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IntInstall(hevent, handleval);
            else
                return BTICARD_64.BTICard_IntInstall(hevent, handleval);
        }
        public static void BTICard_IntReset(nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IntReset(handleval);
            else
                BTICARD_64.BTICard_IntReset(handleval);
        }
        public static int BTICard_IntUninstall(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IntUninstall(handleval);
            else
                return BTICARD_64.BTICard_IntUninstall(handleval);
        }
        public static uint BTICard_IORdL(int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IORdL(addrval, handleval);
            else
                return BTICARD_64.BTICard_IORdL(addrval, handleval);
        }
        public static void BTICard_IORdsL(uint[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IORdsL(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_IORdsL(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_IORdsW(ushort[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IORdsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_IORdsW(valueptr, addrval, countval, handleval);
        }
        public static ushort BTICard_IORdW(int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IORdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_IORdW(addrval, handleval);
        }
        public static ushort BTICard_IOWINRdW(int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IOWINRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_IOWINRdW(addrval, handleval);
        }
        public static void BTICard_IOWINWrW(ushort value, int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IOWINWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_IOWINWrW(value, addrval, handleval);
        }
        public static void BTICard_IOWrL(uint value, int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IOWrL(value, addrval, handleval);
            else
                BTICARD_64.BTICard_IOWrL(value, addrval, handleval);
        }
        public static void BTICard_IOWrsL(uint[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IOWrsL(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_IOWrsL(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_IOWrsW(ushort[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IOWrsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_IOWrsW(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_IOWrW(ushort value, int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IOWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_IOWrW(value, addrval, handleval);
        }
        public static int BTICard_IRIGConfig(uint configval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGConfig(configval, handleval);
            else
                return BTICARD_64.BTICard_IRIGConfig(configval, handleval);
        }
        public static uint BTICard_IRIGFieldGetDays(uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGFieldGetDays(irigvalh, irigvall);
            else
                return BTICARD_64.BTICard_IRIGFieldGetDays(irigvalh, irigvall);
        }
        public static uint BTICard_IRIGFieldGetHours(uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGFieldGetHours(irigvalh, irigvall);
            else
                return BTICARD_64.BTICard_IRIGFieldGetHours(irigvalh, irigvall);
        }
        public static uint BTICard_IRIGFieldGetMicrosec(uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGFieldGetMicrosec(irigvalh, irigvall);
            else
                return BTICARD_64.BTICard_IRIGFieldGetMicrosec(irigvalh, irigvall);
        }
        public static uint BTICard_IRIGFieldGetMillisec(uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGFieldGetMillisec(irigvalh, irigvall);
            else
                return BTICARD_64.BTICard_IRIGFieldGetMillisec(irigvalh, irigvall);
        }
        public static uint BTICard_IRIGFieldGetMin(uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGFieldGetMin(irigvalh, irigvall);
            else
                return BTICARD_64.BTICard_IRIGFieldGetMin(irigvalh, irigvall);
        }
        public static uint BTICard_IRIGFieldGetSec(uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGFieldGetSec(irigvalh, irigvall);
            else
                return BTICARD_64.BTICard_IRIGFieldGetSec(irigvalh, irigvall);
        }
        public static void BTICard_IRIGFieldPutDays(uint value, ref uint irigvalh, ref uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGFieldPutDays(value, ref irigvalh, ref irigvall);
            else
                BTICARD_64.BTICard_IRIGFieldPutDays(value, ref irigvalh, ref irigvall);
        }
        public static void BTICard_IRIGFieldPutHours(uint value, ref uint irigvalh, ref uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGFieldPutHours(value, ref irigvalh, ref irigvall);
            else
                BTICARD_64.BTICard_IRIGFieldPutHours(value, ref irigvalh, ref irigvall);
        }
        public static void BTICard_IRIGFieldPutMicrosec(uint value, ref uint irigvalh, ref uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGFieldPutMicrosec(value, ref irigvalh, ref irigvall);
            else
                BTICARD_64.BTICard_IRIGFieldPutMicrosec(value, ref irigvalh, ref irigvall);
        }
        public static void BTICard_IRIGFieldPutMillisec(uint value, ref uint irigvalh, ref uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGFieldPutMillisec(value, ref irigvalh, ref irigvall);
            else
                BTICARD_64.BTICard_IRIGFieldPutMillisec(value, ref irigvalh, ref irigvall);
        }
        public static void BTICard_IRIGFieldPutMin(uint value, ref uint irigvalh, ref uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGFieldPutMin(value, ref irigvalh, ref irigvall);
            else
                BTICARD_64.BTICard_IRIGFieldPutMin(value, ref irigvalh, ref irigvall);
        }
        public static void BTICard_IRIGFieldPutSec(uint value, ref uint irigvalh, ref uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGFieldPutSec(value, ref irigvalh, ref irigvall);
            else
                BTICARD_64.BTICard_IRIGFieldPutSec(value, ref irigvalh, ref irigvall);
        }
        public static int BTICard_IRIGInputThresholdGet(ref ushort dacval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGInputThresholdGet(ref dacval, handleval);
            else
                return BTICARD_64.BTICard_IRIGInputThresholdGet(ref dacval, handleval);
        }
        public static int BTICard_IRIGInputThresholdSet(ushort dacval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGInputThresholdSet(dacval, handleval);
            else
                return BTICARD_64.BTICard_IRIGInputThresholdSet(dacval, handleval);
        }
        public static int BTICard_IRIGRd(ref BTIIRIGTIME irigtime, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGRd(ref irigtime, handleval);
            else
                return BTICARD_64.BTICard_IRIGRd(ref irigtime, handleval);
        }
        public static int BTICard_IRIGRdEx(ref ushort timebuf, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGRdEx(ref timebuf, handleval);
            else
                return BTICARD_64.BTICard_IRIGRdEx(ref timebuf, handleval);
        }
        public static int BTICard_IRIGSyncStatus(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGSyncStatus(handleval);
            else
                return BTICARD_64.BTICard_IRIGSyncStatus(handleval);
        }
        public static void BTICard_IRIGTimeBCDToBin(ref uint timevalh, ref uint timevall, uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGTimeBCDToBin(ref timevalh, ref timevall, irigvalh, irigvall);
            else
                BTICARD_64.BTICard_IRIGTimeBCDToBin(ref timevalh, ref timevall, irigvalh, irigvall);
        }
        public static void BTICard_IRIGTimeBCDToNanoBin(ref uint timevalh, ref uint timevall, uint irigvalh, uint irigvall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGTimeBCDToNanoBin(ref timevalh, ref timevall, irigvalh, irigvall);
            else
                BTICARD_64.BTICard_IRIGTimeBCDToNanoBin(ref timevalh, ref timevall, irigvalh, irigvall);
        }
        public static void BTICard_IRIGTimeBinToBCD(ref uint irigvalh, ref uint irigvall, uint timevalh, uint timevall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGTimeBinToBCD(ref irigvalh, ref irigvall, timevalh, timevall);
            else
                BTICARD_64.BTICard_IRIGTimeBinToBCD(ref irigvalh, ref irigvall, timevalh, timevall);
        }
        public static void BTICard_IRIGTimeNanoBinToBCD(ref uint irigvalh, ref uint irigvall, uint timevalh, uint timevall)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_IRIGTimeNanoBinToBCD(ref irigvalh, ref irigvall, timevalh, timevall);
            else
                BTICARD_64.BTICard_IRIGTimeNanoBinToBCD(ref irigvalh, ref irigvall, timevalh, timevall);
        }
        public static int BTICard_IRIGWr(ref BTIIRIGTIME irigtime, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGWr(ref irigtime, handleval);
            else
                return BTICARD_64.BTICard_IRIGWr(ref irigtime, handleval);
        }
        public static int BTICard_IRIGWrEx(ref ushort timebuf, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_IRIGWrEx(ref timebuf, handleval);
            else
                return BTICARD_64.BTICard_IRIGWrEx(ref timebuf, handleval);
        }
        public static int BTICard_KernIntStatus(ref int valueptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_KernIntStatus(ref valueptr, handleval);
            else
                return BTICARD_64.BTICard_KernIntStatus(ref valueptr, handleval);
        }
        public static int BTICard_KernStatus(ref uint valueptr, uint typeval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_KernStatus(ref valueptr, typeval, handleval);
            else
                return BTICARD_64.BTICard_KernStatus(ref valueptr, typeval, handleval);
        }
        public static int BTICard_KernStatusEx(ref uint valueptr, uint typeval, uint index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_KernStatusEx(ref valueptr, typeval, index, handleval);
            else
                return BTICARD_64.BTICard_KernStatusEx(ref valueptr, typeval, index, handleval);
        }
        public static uint BTICard_MakeLong(ushort valh, ushort vall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_MakeLong(valh, vall);
            else
                return BTICARD_64.BTICard_MakeLong(valh, vall);
        }
        public static ulong BTICard_MakeQuad(uint valh, uint vall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_MakeQuad(valh, vall);
            else
                return BTICARD_64.BTICard_MakeQuad(valh, vall);
        }
        public static ushort BTICard_MakeWord(byte valh, byte vall)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_MakeWord(valh, vall);
            else
                return BTICARD_64.BTICard_MakeWord(valh, vall);
        }
        public static unsafe string BTICard_MantToAscii(System.Text.StringBuilder buf, int mant, int exp)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_MantToAscii(buf, mant, exp);
            else
                return BTICARD_64.BTICard_MantToAscii(buf, mant, exp);
        }
        public static uint BTICard_Mask(uint dataval, ushort cntval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_Mask(dataval, cntval);
            else
                return BTICARD_64.BTICard_Mask(dataval, cntval);
        }
        public static void BTICard_MaxMant(ref uint mant, ref int exp)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_MaxMant(ref mant, ref exp);
            else
                BTICARD_64.BTICard_MaxMant(ref mant, ref exp);
        }
        public static uint BTICard_Mod(uint moda, uint modb)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_Mod(moda, modb);
            else
                return BTICARD_64.BTICard_Mod(moda, modb);
        }
        public static void BTICard_Mod64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_Mod64(ref resulth, ref resultl, valah, valal, valbh, valbl);
            else
                BTICARD_64.BTICard_Mod64(ref resulth, ref resultl, valah, valal, valbh, valbl);
        }
        public static uint BTICard_Mul(uint mula, uint mulb)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_Mul(mula, mulb);
            else
                return BTICARD_64.BTICard_Mul(mula, mulb);
        }
        public static void BTICard_Mul64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_Mul64(ref resulth, ref resultl, valah, valal, valbh, valbl);
            else
                BTICARD_64.BTICard_Mul64(ref resulth, ref resultl, valah, valal, valbh, valbl);
        }
        public static void BTICard_NormalMant(ref uint mant, ref int exp)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_NormalMant(ref mant, ref exp);
            else
                BTICARD_64.BTICard_NormalMant(ref mant, ref exp);
        }
        public static ushort BTICard_PortRd(int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_PortRd(addrval, handleval);
            else
                return BTICARD_64.BTICard_PortRd(addrval, handleval);
        }
        public static void BTICard_PortWr(ushort value, int addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_PortWr(value, addrval, handleval);
            else
                BTICARD_64.BTICard_PortWr(value, addrval, handleval);
        }
        public static ushort BTICard_ProgRdW(uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ProgRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_ProgRdW(addrval, handleval);
        }
        public static void BTICard_ProgWrW(ushort value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ProgWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_ProgWrW(value, addrval, handleval);
        }
        public static int BTICard_PXIStatus(ref uint infoptr, ushort infotype, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_PXIStatus(ref infoptr, infotype, handleval);
            else
                return BTICARD_64.BTICard_PXIStatus(ref infoptr, infotype, handleval);
        }
        public static void BTICard_RAMFill(ushort value, uint addrval, uint countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMFill(value, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_RAMFill(value, addrval, countval, handleval);
        }
        public static ushort BTICard_RAMRdB(uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_RAMRdB(addrval, handleval);
            else
                return BTICARD_64.BTICard_RAMRdB(addrval, handleval);
        }
        public static uint BTICard_RAMRdL(uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_RAMRdL(addrval, handleval);
            else
                return BTICARD_64.BTICard_RAMRdL(addrval, handleval);
        }
        public static void BTICard_RAMRdmL(uint[] valueptr, uint[] addrptr, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMRdmL(valueptr, addrptr, countval, handleval);
            else
                BTICARD_64.BTICard_RAMRdmL(valueptr, addrptr, countval, handleval);
        }
        public static void BTICard_RAMRdmW(ushort[] valueptr, uint[] addrptr, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMRdmW(valueptr, addrptr, countval, handleval);
            else
                BTICARD_64.BTICard_RAMRdmW(valueptr, addrptr, countval, handleval);
        }
        public static void BTICard_RAMRdsL(uint[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMRdsL(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_RAMRdsL(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_RAMRdsW(ushort[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMRdsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_RAMRdsW(valueptr, addrval, countval, handleval);
        }
        public static ushort BTICard_RAMRdW(uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_RAMRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_RAMRdW(addrval, handleval);
        }
        public static void BTICard_RAMWipe(nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWipe(handleval);
            else
                BTICARD_64.BTICard_RAMWipe(handleval);
        }
        public static void BTICard_RAMWipeEx(ushort value, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWipeEx(value, handleval);
            else
                BTICARD_64.BTICard_RAMWipeEx(value, handleval);
        }
        public static void BTICard_RAMWrB(ushort value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWrB(value, addrval, handleval);
            else
                BTICARD_64.BTICard_RAMWrB(value, addrval, handleval);
        }
        public static void BTICard_RAMWrL(uint value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWrL(value, addrval, handleval);
            else
                BTICARD_64.BTICard_RAMWrL(value, addrval, handleval);
        }
        public static void BTICard_RAMWrmL(uint[] valueptr, uint[] addrptr, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWrmL(valueptr, addrptr, countval, handleval);
            else
                BTICARD_64.BTICard_RAMWrmL(valueptr, addrptr, countval, handleval);
        }
        public static void BTICard_RAMWrmW(ushort[] valueptr, uint[] addrptr, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWrmW(valueptr, addrptr, countval, handleval);
            else
                BTICARD_64.BTICard_RAMWrmW(valueptr, addrptr, countval, handleval);
        }
        public static void BTICard_RAMWrsL(uint[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWrsL(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_RAMWrsL(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_RAMWrsW(ushort[] valueptr, uint addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWrsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_RAMWrsW(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_RAMWrW(ushort value, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_RAMWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_RAMWrW(value, addrval, handleval);
        }
        public static uint BTICard_ReverseLong(uint value)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ReverseLong(value);
            else
                return BTICARD_64.BTICard_ReverseLong(value);
        }
        public static ushort BTICard_ReverseWord(ushort value)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ReverseWord(value);
            else
                return BTICARD_64.BTICard_ReverseWord(value);
        }
        public static uint BTICard_SeqBlkRd(ushort[] buf, uint bufcount, ref uint blkcnt, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqBlkRd(buf, bufcount, ref blkcnt, handleval);
            else
                return BTICARD_64.BTICard_SeqBlkRd(buf, bufcount, ref blkcnt, handleval);
        }
        public static uint BTICard_SeqBlkRdEx(ushort[] buf, uint bufcount, uint maxblkcnt, ref uint blkcnt, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqBlkRdEx(buf, bufcount, maxblkcnt, ref blkcnt, handleval);
            else
                return BTICARD_64.BTICard_SeqBlkRdEx(buf, bufcount, maxblkcnt, ref blkcnt, handleval);
        }
        public static int BTICard_SeqClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqClear(handleval);
            else
                return BTICARD_64.BTICard_SeqClear(handleval);
        }
        public static ushort BTICard_SeqCommRd(ushort[] buf, ushort bufcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqCommRd(buf, bufcount, handleval);
            else
                return BTICARD_64.BTICard_SeqCommRd(buf, bufcount, handleval);
        }
        public static int BTICard_SeqConfig(uint configval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqConfig(configval, handleval);
            else
                return BTICARD_64.BTICard_SeqConfig(configval, handleval);
        }
        public static int BTICard_SeqConfigEx(uint configval, uint seqcount, ushort cardnum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqConfigEx(configval, seqcount, cardnum, handleval);
            else
                return BTICARD_64.BTICard_SeqConfigEx(configval, seqcount, cardnum, handleval);
        }
        public static int BTICard_SeqConfigExx(uint configval, uint seqaddr, uint seqcount, ushort cardnum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqConfigExx(configval, seqaddr, seqcount, cardnum, handleval);
            else
                return BTICARD_64.BTICard_SeqConfigExx(configval, seqaddr, seqcount, cardnum, handleval);
        }
        public static uint BTICard_SeqDMARd(ushort[] buf, uint bufcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqDMARd(buf, bufcount, handleval);
            else
                return BTICARD_64.BTICard_SeqDMARd(buf, bufcount, handleval);
        }
        public static int BTICard_SeqFindCheckVersion(ref ushort precord, ushort version)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindCheckVersion(ref precord, version);
            else
                return BTICARD_64.BTICard_SeqFindCheckVersion(ref precord, version);
        }
        public static int BTICard_SeqFindInit(ushort[] seqbuf, uint seqbufsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindInit(seqbuf, seqbufsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindInit(seqbuf, seqbufsize, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindMore1553(ref SEQRECORDMORE1553* precmore, ref SEQRECORD1553 precbase)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindMore1553(ref precmore, ref precbase);
            else
                return BTICARD_64.BTICard_SeqFindMore1553(ref precmore, ref precbase);
        }
        public static int BTICard_SeqFindMore1553Ex(ref SEQRECORDMORE1553 precmore, ushort recordsize, ref SEQRECORD1553 precbase)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindMore1553Ex(ref precmore, recordsize, ref precbase);
            else
                return BTICARD_64.BTICard_SeqFindMore1553Ex(ref precmore, recordsize, ref precbase);
        }
        unsafe public static int BTICard_SeqFindNext(ref ushort* precord, ref ushort seqtype, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext(ref precord, ref seqtype, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext(ref precord, ref seqtype, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindNext1553(ref SEQRECORD1553* precord, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext1553(ref precord, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext1553(ref precord, ref sfinfo);
        }
        public static int BTICard_SeqFindNext1553Ex(ref SEQRECORD1553 precord, ushort recordsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext1553Ex(ref precord, recordsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext1553Ex(ref precord, recordsize, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindNext429(ref SEQRECORD429* precord, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext429(ref precord, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext429(ref precord, ref sfinfo);
        }
        public static int BTICard_SeqFindNext429Ex(ref SEQRECORD429 precord, ushort recordsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext429Ex(ref precord, recordsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext429Ex(ref precord, recordsize, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindNext708(ref SEQRECORD708* precord, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext708(ref precord, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext708(ref precord, ref sfinfo);
        }
        public static int BTICard_SeqFindNext708Ex(ref SEQRECORD708 precord, ushort recordsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext708Ex(ref precord, recordsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext708Ex(ref precord, recordsize, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindNext717(ref SEQRECORD717* precord, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext717(ref precord, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext717(ref precord, ref sfinfo);
        }
        public static int BTICard_SeqFindNext717Ex(ref SEQRECORD717 precord, ushort recordsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNext717Ex(ref precord, recordsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNext717Ex(ref precord, recordsize, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindNextCSDB(ref SEQRECORDCSDB* precord, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNextCSDB(ref precord, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNextCSDB(ref precord, ref sfinfo);
        }
        public static int BTICard_SeqFindNextCSDBEx(ref SEQRECORDCSDB precord, ushort recordsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNextCSDBEx(ref precord, recordsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNextCSDBEx(ref precord, recordsize, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindNextDIO(ref SEQRECORDDIO* precord, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNextDIO(ref precord, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNextDIO(ref precord, ref sfinfo);
        }
        public static int BTICard_SeqFindNextDIOEx(ref SEQRECORDDIO precord, ushort recordsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNextDIOEx(ref precord, recordsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNextDIOEx(ref precord, recordsize, ref sfinfo);
        }
        unsafe public static int BTICard_SeqFindNextEBR(ref SEQRECORDEBR* precord, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNextEBR(ref precord, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNextEBR(ref precord, ref sfinfo);
        }
        public static int BTICard_SeqFindNextEBREx(ref SEQRECORDEBR precord, ushort recordsize, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNextEBREx(ref precord, recordsize, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNextEBREx(ref precord, recordsize, ref sfinfo);
        }
        public static int BTICard_SeqFindNextEx(ushort[] precord, ushort recordcount, ref ushort seqtype, ref SEQFINDINFO sfinfo)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqFindNextEx(precord, recordcount, ref seqtype, ref sfinfo);
            else
                return BTICARD_64.BTICard_SeqFindNextEx(precord, recordcount, ref seqtype, ref sfinfo);
        }
        public static int BTICard_SeqInterval(int interval, int mode, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqInterval(interval, mode, handleval);
            else
                return BTICARD_64.BTICard_SeqInterval(interval, mode, handleval);
        }
        public static ushort BTICard_SeqIntervalEx(ushort shiftval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqIntervalEx(shiftval, handleval);
            else
                return BTICARD_64.BTICard_SeqIntervalEx(shiftval, handleval);
        }
        public static int BTICard_SeqIsRunning(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqIsRunning(handleval);
            else
                return BTICARD_64.BTICard_SeqIsRunning(handleval);
        }
        public static ushort BTICard_SeqLogFrequency(ushort logfreq, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqLogFrequency(logfreq, handleval);
            else
                return BTICARD_64.BTICard_SeqLogFrequency(logfreq, handleval);
        }
        public static ushort BTICard_SeqRd(ushort[] buf, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqRd(buf, handleval);
            else
                return BTICARD_64.BTICard_SeqRd(buf, handleval);
        }
        public static ushort BTICard_SeqRdEx(ushort[] buf, ushort bufcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqRdEx(buf, bufcount, handleval);
            else
                return BTICARD_64.BTICard_SeqRdEx(buf, bufcount, handleval);
        }
        public static int BTICard_SeqResume(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqResume(handleval);
            else
                return BTICARD_64.BTICard_SeqResume(handleval);
        }
        public static int BTICard_SeqStart(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqStart(handleval);
            else
                return BTICARD_64.BTICard_SeqStart(handleval);
        }
        public static int BTICard_SeqStatus(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqStatus(handleval);
            else
                return BTICARD_64.BTICard_SeqStatus(handleval);
        }
        public static int BTICard_SeqStop(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SeqStop(handleval);
            else
                return BTICARD_64.BTICard_SeqStop(handleval);
        }
        public static uint BTICard_Shl(uint dataval, ushort cntval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_Shl(dataval, cntval);
            else
                return BTICARD_64.BTICard_Shl(dataval, cntval);
        }
        public static uint BTICard_Shr(uint dataval, ushort cntval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_Shr(dataval, cntval);
            else
                return BTICARD_64.BTICard_Shr(dataval, cntval);
        }
        public static ushort BTICard_SignMant(string str)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SignMant(str);
            else
                return BTICARD_64.BTICard_SignMant(str);
        }
        public static void BTICard_Sub64(ref uint resulth, ref uint resultl, uint valah, uint valal, uint valbh, uint valbl)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_Sub64(ref resulth, ref resultl, valah, valal, valbh, valbl);
            else
                BTICARD_64.BTICard_Sub64(ref resulth, ref resultl, valah, valal, valbh, valbl);
        }
        public static void BTICard_SwapEndianL(ref uint value)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_SwapEndianL(ref value);
            else
                BTICARD_64.BTICard_SwapEndianL(ref value);
        }
        public static void BTICard_SwapEndianW(ref ushort valuea, ref ushort valueb)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_SwapEndianW(ref valuea, ref valueb);
            else
                BTICARD_64.BTICard_SwapEndianW(ref valuea, ref valueb);
        }
        public static void BTICard_SwapIfBigEndianL(ref uint value)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_SwapIfBigEndianL(ref value);
            else
                BTICARD_64.BTICard_SwapIfBigEndianL(ref value);
        }
        public static void BTICard_SwapIfBigEndianW(ref ushort valuea, ref ushort valueb)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_SwapIfBigEndianW(ref valuea, ref valueb);
            else
                BTICARD_64.BTICard_SwapIfBigEndianW(ref valuea, ref valueb);
        }
        public static void BTICard_SwapIfLittleEndianL(ref uint value)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_SwapIfLittleEndianL(ref value);
            else
                BTICARD_64.BTICard_SwapIfLittleEndianL(ref value);
        }
        public static void BTICard_SwapIfLittleEndianW(ref ushort valuea, ref ushort valueb)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_SwapIfLittleEndianW(ref valuea, ref valueb);
            else
                BTICARD_64.BTICard_SwapIfLittleEndianW(ref valuea, ref valueb);
        }
        public static int BTICard_SysMonClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonClear(handleval);
            else
                return BTICARD_64.BTICard_SysMonClear(handleval);
        }
        public static int BTICard_SysMonDACToVal(ushort dacval, int rangemin, int rangemax, ushort dacval_max, int scalefactor, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonDACToVal(dacval, rangemin, rangemax, dacval_max, scalefactor, handleval);
            else
                return BTICARD_64.BTICard_SysMonDACToVal(dacval, rangemin, rangemax, dacval_max, scalefactor, handleval);
        }
        public static unsafe string BTICard_SysMonDescGet(int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonDescGet(index, handleval);
            else
                return BTICARD_64.BTICard_SysMonDescGet(index, handleval);
        }
        public static int BTICard_SysMonInit(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonInit(handleval);
            else
                return BTICARD_64.BTICard_SysMonInit(handleval);
        }
        public static int BTICard_SysMonMaxRd(int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonMaxRd(index, handleval);
            else
                return BTICARD_64.BTICard_SysMonMaxRd(index, handleval);
        }
        public static int BTICard_SysMonMinRd(int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonMinRd(index, handleval);
            else
                return BTICARD_64.BTICard_SysMonMinRd(index, handleval);
        }
        public static int BTICard_SysMonNomRd(int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonNomRd(index, handleval);
            else
                return BTICARD_64.BTICard_SysMonNomRd(index, handleval);
        }
        public static int BTICard_SysMonThresholdGet(ref int enable, ref int min, ref int max, int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonThresholdGet(ref enable, ref min, ref max, index, handleval);
            else
                return BTICARD_64.BTICard_SysMonThresholdGet(ref enable, ref min, ref max, index, handleval);
        }
        public static int BTICard_SysMonThresholdSet(int enable, int min, int max, int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonThresholdSet(enable, min, max, index, handleval);
            else
                return BTICARD_64.BTICard_SysMonThresholdSet(enable, min, max, index, handleval);
        }
        public static uint BTICard_SysMonTypeGet(int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonTypeGet(index, handleval);
            else
                return BTICARD_64.BTICard_SysMonTypeGet(index, handleval);
        }
        public static unsafe string BTICard_SysMonUserStr(int value, int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonUserStr(value, index, handleval);
            else
                return BTICARD_64.BTICard_SysMonUserStr(value, index, handleval);
        }
        public static int BTICard_SysMonValRd(int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonValRd(index, handleval);
            else
                return BTICARD_64.BTICard_SysMonValRd(index, handleval);
        }
        public static unsafe string BTICard_SysMonValStr(int index, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonValStr(index, handleval);
            else
                return BTICARD_64.BTICard_SysMonValStr(index, handleval);
        }
        public static ushort BTICard_SysMonValToDAC(int val, int rangemin, int rangemax, ushort dacval_max, int scalefactor, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_SysMonValToDAC(val, rangemin, rangemax, dacval_max, scalefactor, handleval);
            else
                return BTICARD_64.BTICard_SysMonValToDAC(val, rangemin, rangemax, dacval_max, scalefactor, handleval);
        }
        public static int BTICard_TickTimerRemaining(int timer)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TickTimerRemaining(timer);
            else
                return BTICARD_64.BTICard_TickTimerRemaining(timer);
        }
        public static int BTICard_TickTimerStart(int milliseconds)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TickTimerStart(milliseconds);
            else
                return BTICARD_64.BTICard_TickTimerStart(milliseconds);
        }
        public static int BTICard_TickTimerValid(int timer)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TickTimerValid(timer);
            else
                return BTICARD_64.BTICard_TickTimerValid(timer);
        }
        public static int BTICard_Timer64Rd(ref uint valueh, ref uint valuel, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_Timer64Rd(ref valueh, ref valuel, handleval);
            else
                return BTICARD_64.BTICard_Timer64Rd(ref valueh, ref valuel, handleval);
        }
        public static void BTICard_Timer64Wr(uint valueh, uint valuel, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_Timer64Wr(valueh, valuel, handleval);
            else
                BTICARD_64.BTICard_Timer64Wr(valueh, valuel, handleval);
        }
        public static void BTICard_TimerClear(nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_TimerClear(handleval);
            else
                BTICARD_64.BTICard_TimerClear(handleval);
        }
        public static uint BTICard_TimerRd(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TimerRd(handleval);
            else
                return BTICARD_64.BTICard_TimerRd(handleval);
        }
        public static int BTICard_TimerResolution(int timerresol, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TimerResolution(timerresol, handleval);
            else
                return BTICARD_64.BTICard_TimerResolution(timerresol, handleval);
        }
        public static ushort BTICard_TimerResolutionEx(ushort timershift, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TimerResolutionEx(timershift, handleval);
            else
                return BTICARD_64.BTICard_TimerResolutionEx(timershift, handleval);
        }
        public static int BTICard_TimerStatus(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TimerStatus(handleval);
            else
                return BTICARD_64.BTICard_TimerStatus(handleval);
        }
        public static void BTICard_TimerWr(uint value, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_TimerWr(value, handleval);
            else
                BTICARD_64.BTICard_TimerWr(value, handleval);
        }
        public static int BTICard_TSMDriftMaxGet(ref uint driftptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMDriftMaxGet(ref driftptr, handleval);
            else
                return BTICARD_64.BTICard_TSMDriftMaxGet(ref driftptr, handleval);
        }
        public static int BTICard_TSMDriftMaxSet(uint drift, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMDriftMaxSet(drift, handleval);
            else
                return BTICARD_64.BTICard_TSMDriftMaxSet(drift, handleval);
        }
        public static int BTICard_TSMDriftRd(ref int driftptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMDriftRd(ref driftptr, handleval);
            else
                return BTICARD_64.BTICard_TSMDriftRd(ref driftptr, handleval);
        }
        public static int BTICard_TSMDriftRelWr(int drift, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMDriftRelWr(drift, handleval);
            else
                return BTICARD_64.BTICard_TSMDriftRelWr(drift, handleval);
        }
        public static int BTICard_TSMDriftWr(int drift, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMDriftWr(drift, handleval);
            else
                return BTICARD_64.BTICard_TSMDriftWr(drift, handleval);
        }
        public static int BTICard_TSMInputDelayCompGet(ref int delayptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMInputDelayCompGet(ref delayptr, handleval);
            else
                return BTICARD_64.BTICard_TSMInputDelayCompGet(ref delayptr, handleval);
        }
        public static int BTICard_TSMInputDelayCompSet(int delay, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMInputDelayCompSet(delay, handleval);
            else
                return BTICARD_64.BTICard_TSMInputDelayCompSet(delay, handleval);
        }
        public static int BTICard_TSMInputThresholdAuto(int pinindex, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMInputThresholdAuto(pinindex, handleval);
            else
                return BTICARD_64.BTICard_TSMInputThresholdAuto(pinindex, handleval);
        }
        public static int BTICard_TSMInputThresholdGet(ref ushort dacval, int pinindex, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMInputThresholdGet(ref dacval, pinindex, handleval);
            else
                return BTICARD_64.BTICard_TSMInputThresholdGet(ref dacval, pinindex, handleval);
        }
        public static int BTICard_TSMInputThresholdSet(ushort dacval, int pinindex, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMInputThresholdSet(dacval, pinindex, handleval);
            else
                return BTICARD_64.BTICard_TSMInputThresholdSet(dacval, pinindex, handleval);
        }
        public static int BTICard_TSMIntTrigger(ref ulong timetagptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMIntTrigger(ref timetagptr, handleval);
            else
                return BTICARD_64.BTICard_TSMIntTrigger(ref timetagptr, handleval);
        }
        public static int BTICard_TSMIRIGControlRd(ref uint ctrlptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMIRIGControlRd(ref ctrlptr, handleval);
            else
                return BTICARD_64.BTICard_TSMIRIGControlRd(ref ctrlptr, handleval);
        }
        public static int BTICard_TSMIRIGControlWr(uint ctrlval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMIRIGControlWr(ctrlval, handleval);
            else
                return BTICARD_64.BTICard_TSMIRIGControlWr(ctrlval, handleval);
        }
        public static int BTICard_TSMIRIGYearsRd(ref uint year, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMIRIGYearsRd(ref year, handleval);
            else
                return BTICARD_64.BTICard_TSMIRIGYearsRd(ref year, handleval);
        }
        public static int BTICard_TSMIRIGYearsWr(uint year, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMIRIGYearsWr(year, handleval);
            else
                return BTICARD_64.BTICard_TSMIRIGYearsWr(year, handleval);
        }
        public static int BTICard_TSMJumpThresholdGet(ref uint drift, ref uint offset, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMJumpThresholdGet(ref drift, ref offset, handleval);
            else
                return BTICARD_64.BTICard_TSMJumpThresholdGet(ref drift, ref offset, handleval);
        }
        public static int BTICard_TSMJumpThresholdSet(uint drift, uint offset, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMJumpThresholdSet(drift, offset, handleval);
            else
                return BTICARD_64.BTICard_TSMJumpThresholdSet(drift, offset, handleval);
        }
        public static int BTICard_TSMOffsetLastRd(ref long offsetptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMOffsetLastRd(ref offsetptr, handleval);
            else
                return BTICARD_64.BTICard_TSMOffsetLastRd(ref offsetptr, handleval);
        }
        public static int BTICard_TSMReset(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMReset(handleval);
            else
                return BTICARD_64.BTICard_TSMReset(handleval);
        }
        public static int BTICard_TSMSourceConfig(uint sourcecfg, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMSourceConfig(sourcecfg, handleval);
            else
                return BTICARD_64.BTICard_TSMSourceConfig(sourcecfg, handleval);
        }
        public static int BTICard_TSMStatus(ref uint statusptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMStatus(ref statusptr, handleval);
            else
                return BTICARD_64.BTICard_TSMStatus(ref statusptr, handleval);
        }
        public static int BTICard_TSMSyncConfig(uint timeconfig, uint driftconfig, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMSyncConfig(timeconfig, driftconfig, handleval);
            else
                return BTICARD_64.BTICard_TSMSyncConfig(timeconfig, driftconfig, handleval);
        }
        public static int BTICard_TSMTimerRd(ref ulong valueptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMTimerRd(ref valueptr, handleval);
            else
                return BTICARD_64.BTICard_TSMTimerRd(ref valueptr, handleval);
        }
        public static int BTICard_TSMTimerRelWr(long value, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMTimerRelWr(value, handleval);
            else
                return BTICARD_64.BTICard_TSMTimerRelWr(value, handleval);
        }
        public static int BTICard_TSMTimerRolloverGet(ref ulong valueptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMTimerRolloverGet(ref valueptr, handleval);
            else
                return BTICARD_64.BTICard_TSMTimerRolloverGet(ref valueptr, handleval);
        }
        public static int BTICard_TSMTimerRolloverSet(ulong value, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMTimerRolloverSet(value, handleval);
            else
                return BTICARD_64.BTICard_TSMTimerRolloverSet(value, handleval);
        }
        public static int BTICard_TSMTimerWr(ulong value, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_TSMTimerWr(value, handleval);
            else
                return BTICARD_64.BTICard_TSMTimerWr(value, handleval);
        }
        public static int BTICard_ValAsciiCmpi(System.Text.StringBuilder str1, System.Text.StringBuilder str2)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValAsciiCmpi(str1, str2);
            else
                return BTICARD_64.BTICard_ValAsciiCmpi(str1, str2);
        }
        public static unsafe string BTICard_ValAsciiCpy(System.Text.StringBuilder strdest, string strsrc, int count)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValAsciiCpy(strdest, strsrc, count);
            else
                return BTICARD_64.BTICard_ValAsciiCpy(strdest, strsrc, count);
        }
        public static void BTICard_ValAsciiTrimLead(System.Text.StringBuilder buf)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ValAsciiTrimLead(buf);
            else
                BTICARD_64.BTICard_ValAsciiTrimLead(buf);
        }
        public static void BTICard_ValAsciiTrimTrail(System.Text.StringBuilder buf)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_ValAsciiTrimTrail(buf);
            else
                BTICARD_64.BTICard_ValAsciiTrimTrail(buf);
        }
        public static uint BTICard_ValFromAscii(string asciistr, int radixval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValFromAscii(asciistr, radixval);
            else
                return BTICARD_64.BTICard_ValFromAscii(asciistr, radixval);
        }
        public static uint BTICard_ValGetBits(uint oldvalue, int startbit, int endbit)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValGetBits(oldvalue, startbit, endbit);
            else
                return BTICARD_64.BTICard_ValGetBits(oldvalue, startbit, endbit);
        }
        public static unsafe string BTICard_ValIncAscii(System.Text.StringBuilder asciistr)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValIncAscii(asciistr);
            else
                return BTICARD_64.BTICard_ValIncAscii(asciistr);
        }
        public static unsafe string BTICard_ValInccAscii(System.Text.StringBuilder asciistr)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValInccAscii(asciistr);
            else
                return BTICARD_64.BTICard_ValInccAscii(asciistr);
        }
        public static int BTICard_ValIsLower(int value)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValIsLower(value);
            else
                return BTICARD_64.BTICard_ValIsLower(value);
        }
        public static int BTICard_ValLenAscii(int numbits, int radixval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValLenAscii(numbits, radixval);
            else
                return BTICARD_64.BTICard_ValLenAscii(numbits, radixval);
        }
        public static uint BTICard_ValPutBits(uint oldvalue, uint newfld, int startbit, int endbit)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValPutBits(oldvalue, newfld, startbit, endbit);
            else
                return BTICARD_64.BTICard_ValPutBits(oldvalue, newfld, startbit, endbit);
        }
        public static unsafe string BTICard_ValToAscii(uint value, System.Text.StringBuilder asciistr, int numbits, int radixval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValToAscii(value, asciistr, numbits, radixval);
            else
                return BTICARD_64.BTICard_ValToAscii(value, asciistr, numbits, radixval);
        }
        public static int BTICard_ValToUpper(int value)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_ValToUpper(value);
            else
                return BTICARD_64.BTICard_ValToUpper(value);
        }
        public static uint BTICard_VARRdL(ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_VARRdL(addrval, handleval);
            else
                return BTICARD_64.BTICard_VARRdL(addrval, handleval);
        }
        public static void BTICard_VARRdsW(ushort[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_VARRdsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_VARRdsW(valueptr, addrval, countval, handleval);
        }
        public static ushort BTICard_VARRdW(ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTICARD_32.BTICard_VARRdW(addrval, handleval);
            else
                return BTICARD_64.BTICard_VARRdW(addrval, handleval);
        }
        public static void BTICard_VARWrL(uint value, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_VARWrL(value, addrval, handleval);
            else
                BTICARD_64.BTICard_VARWrL(value, addrval, handleval);
        }
        public static void BTICard_VARWrsW(ushort[] valueptr, ushort addrval, int countval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_VARWrsW(valueptr, addrval, countval, handleval);
            else
                BTICARD_64.BTICard_VARWrsW(valueptr, addrval, countval, handleval);
        }
        public static void BTICard_VARWrW(ushort value, ushort addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTICARD_32.BTICard_VARWrW(value, addrval, handleval);
            else
                BTICARD_64.BTICard_VARWrW(value, addrval, handleval);
        }

        /**
        *
        *  External definitions
        *
        *  Note: To operate the BUSBox in serial (RS-232) mode, set
        *  the BUSBox_SerialMode = TRUE.  Optionally, you can set the
        *  BUSBox_BaudRate = baudvalue, but this defaults to the
        *  suggested 115200 baud.
        *
        **/

        /**
        *
        *  Core number constants
        *
        **/

        public const uint COREA = 0x0000;	//Selects Core A
        public const uint COREB = 0x0001;	//Selects Core B
        public const uint COREC = 0x0002;	//Selects Core C
        public const uint CORED = 0x0003;	//Selects Core D

        /**
        *
        *  Sequential Record configuration options
        *
        **/

        public const uint SEQCFG_DEFAULT = 0x00000000;	//Select all default settings
        public const uint SEQCFG_FILLHALT = 0x00000000;	//Enable sequential record in fill and halt mode (default)
        public const uint SEQCFG_DISABLE = 0x00000001;	//Disable sequential record
        public const uint SEQCFG_CONTINUOUS = 0x00000002;	//Enable sequential record in continuous mode
        public const uint SEQCFG_DMA = 0x00000004;	//Enable monitor in DMA mode
        public const uint SEQCFG_FREE = 0x00000008;	//Enable sequential record in free mode
        public const uint SEQCFG_DELTA = 0x00000010;	//Enable sequential record in delta mode
        public const uint SEQCFG_INTERVAL = 0x00000020;	//Enable sequential record in interval mode
        public const uint SEQCFG_NOLOGFULL = 0x00000000;	//Do not generate event log when sequential record is full (default)
        public const uint SEQCFG_LOGFULL = 0x00001000;	//Generate event log when sequential record is full
        public const uint SEQCFG_NOLOGFREQ = 0x00000000;	//Do not generate event logs at a user specified frequency (default)
        public const uint SEQCFG_LOGFREQ = 0x00002000;	//Generate event logs at user specified frequency
        public const uint SEQCFG_TCPNODELAY = 0x00004000;	//Disable Nagle's algorithm on sequential DMA for RPC devices
        public const uint SEQCFG_16K = 0x00000000;	//Allocate a 16K sequential record buffer (default)
        public const uint SEQCFG_ALLAVAIL = 0x01000000;	//Allocate all available memory to a sequential record buffer
        public const uint SEQCFG_32K = 0x02000000;	//Allocate a 32K sequential record buffer
        public const uint SEQCFG_64K = 0x04000000;	//Allocate a 64K sequential record buffer
        public const uint SEQCFG_128K = 0x08000000;	//Allocate a 128K sequential record buffer

        /**
        *
        *  Sequential Record type fields
        *
        **/

        public const uint SEQTYPE_MASK = 0x00FF;	//Sequential record type mask value
        public const uint SEQTYPE_429 = 0x0001;	//Sequential record type is ARINC 429
        public const uint SEQTYPE_717 = 0x0002;	//Sequential record type is ARINC 717
        public const uint SEQTYPE_1553 = 0x0003;	//Sequential record type is MIL-STD-1553
        public const uint SEQTYPE_708 = 0x0004;	//Sequential record type is ARINC 708
        public const uint SEQTYPE_USER = 0x0005;	//Sequential record type is User Defined
        public const uint SEQTYPE_CSDB = 0x0006;	//Sequential record type is CSDB
        public const uint SEQTYPE_DIO = 0x0007;	//Sequential record type is DIO
        public const uint SEQTYPE_EBR = 0x0008;	//Sequential record type is EBR-1553
        public const uint SEQTYPE_717SF = 0x0009;	//Sequential record type is ARINC 717 Subframe
        public const uint SEQTYPE_PMC10 = 0x000A;	//Sequential record type is PMC10
        public const uint SEQTYPE_RESTART = 0x00FF;	//Sequential record type is Restart

        /**
        *
        *  Sequential Record version fields
        *
        **/

        public const uint SEQVER_MASK = 0xFF00;	//Sequential record version mask value
        public const uint SEQVER_0 = 0x0000;	//Sequential record version number is 0
        public const uint SEQVER_1 = 0x0100;	//Sequential record version number is 1

        /**
        *
        *  Event log list configuration options
        *
        **/

        public const uint LOGCFG_DEFAULT = 0x00000000;	//Select all default settings
        public const uint LOGCFG_ENABLE = 0x00000000;	//Enable event log list (default)
        public const uint LOGCFG_DISABLE = 0x00000001;	//Disable event log list

        /**
        *
        *  IRIG timer configuration options
        *
        **/

        public const uint IRIGCFG_DEFAULT = 0x00000000;	//Select all default settings
        public const uint IRIGCFG_ENABLE = 0x00000000;	//Enable IRIG timer (default)
        public const uint IRIGCFG_DISABLE = 0x00000001;	//Disable IRIG timer
        public const uint IRIGCFG_SPEEDB = 0x00000000;	//Bit rate is IRIGB (default)
        public const uint IRIGCFG_SPEEDA = 0x00000002;	//Bit rate is IRIGA
        public const uint IRIGCFG_INTERNAL = 0x00000000;	//IRIG timer operates internally (default)
        public const uint IRIGCFG_EXTERNAL = 0x00000004;	//IRIG timer operates externally
        public const uint IRIGCFG_SLAVE = 0x00000000;	//IRIG timer is a slave  / receiver (default)
        public const uint IRIGCFG_MASTER = 0x00000008;	//IRIG timer is a master / transmitter
        public const uint IRIGCFG_PPS = 0x00000010;	//IRIG timer operates in PPS mode (rcv/xmt)
        public const uint IRIGCFG_PWM = 0x00000000;	//IRIG timer uses pulse width modulated signaling (rcv/xmt) (default)
        public const uint IRIGCFG_AM = 0x00000020;	//IRIG timer uses amplitude modulated signaling (rcv/xmt hw dependant)

        /**
        *
        *  IRIG timer field definitions
        *
        **/

        public const uint IRIGFIELD_USECLSB = 0x0000;	//Microseconds LSB in timestamp field
        public const uint IRIGFIELD_USECMSB = 0x000B;	//Microseconds MSB in timestamp field
        public const uint IRIGFIELD_MSECLSB = 0x000C;	//Milliseconds LSB in timestamp field
        public const uint IRIGFIELD_MSECMSB = 0x0017;	//Milliseconds MSB in timestamp field
        public const uint IRIGFIELD_SECLSB = 0x0018;	//Seconds LSB in timestamp field
        public const uint IRIGFIELD_SECMSB = 0x001F;	//Seconds MSB in timestamp field
        public const uint IRIGFIELD_MINLSB = 0x0000;	//Minutes LSB in timestamph field
        public const uint IRIGFIELD_MINMSB = 0x0007;	//Minutes MSB in timestamph field
        public const uint IRIGFIELD_HRSLSB = 0x0008;	//Hours LSB in timestamph field
        public const uint IRIGFIELD_HRSMSB = 0x000F;	//Hours MSB in timestamph field
        public const uint IRIGFIELD_DAYLSB = 0x0010;	//Days LSB in timestamph field
        public const uint IRIGFIELD_DAYMSB = 0x001B;	//Days MSB in timestamph field

        /**
        *
        *  C54x Global Registers
        *
        **/

        public const uint C54_GREG_IMR = 0x0000;	//Interrupt mask register
        public const uint C54_GREG_IFR = 0x0001;	//Interrupt flag register
        public const uint C54_GREG_ST0 = 0x0006;	//Status register 0
        public const uint C54_GREG_ST1 = 0x0007;	//Status register 1
        public const uint C54_GREG_AL = 0x0008;	//Accumulator A low word (bits 15-00)
        public const uint C54_GREG_AH = 0x0009;	//Accumulator A high word (bits 31-16)
        public const uint C54_GREG_AG = 0x000A;	//Accumulator A guars bits (bits 39-32)
        public const uint C54_GREG_BL = 0x000B;	//Accumulator B low word (bits 15-00)
        public const uint C54_GREG_BH = 0x000C;	//Accumulator B high word (bits 31-16)
        public const uint C54_GREG_BG = 0x000D;	//Accumulator B guard bits (bits 39-32
        public const uint C54_GREG_T = 0x000E;	//Temporary register
        public const uint C54_GREG_TRN = 0x000F;	//Transition register
        public const uint C54_GREG_AR0 = 0x0010;	//Auxilliary register 0
        public const uint C54_GREG_AR1 = 0x0011;	//Auxilliary register 1
        public const uint C54_GREG_AR2 = 0x0012;	//Auxilliary register 2
        public const uint C54_GREG_AR3 = 0x0013;	//Auxilliary register 3
        public const uint C54_GREG_AR4 = 0x0014;	//Auxilliary register 4
        public const uint C54_GREG_AR5 = 0x0015;	//Auxilliary register 5
        public const uint C54_GREG_AR6 = 0x0016;	//Auxilliary register 6
        public const uint C54_GREG_AR7 = 0x0017;	//Auxilliary register 7
        public const uint C54_GREG_SP = 0x0018;	//Stack pointer
        public const uint C54_GREG_BK = 0x0019;	//Circular-buffer size register
        public const uint C54_GREG_BRC = 0x001A;	//Block-repeat counter
        public const uint C54_GREG_RSA = 0x001B;	//Block-repeat start address
        public const uint C54_GREG_REA = 0x001C;	//Block-repeat end address
        public const uint C54_GREG_PMST = 0x001D;	//Processor mode status register
        public const uint C54_GREG_XPC = 0x001E;	//Program counter extension register
        public const uint C54_GREG_DRR20 = 0x0020;	//McBSP0 data receive register high
        public const uint C54_GREG_DRR10 = 0x0021;	//McBSP0 data receive register low
        public const uint C54_GREG_DXR20 = 0x0022;	//McBSP0 data transmit register high
        public const uint C54_GREG_DXR10 = 0x0023;	//McBSP0 data transmit register low
        public const uint C54_GREG_TIM = 0x0024;	//Timer count register
        public const uint C54_GREG_PRD = 0x0025;	//Timer period register
        public const uint C54_GREG_TCR = 0x0026;	//Timer control register
        public const uint C54_GREG_SWWSR = 0x0028;	//External interface software wait-state register
        public const uint C54_GREG_BSCR = 0x0029;	//External interface bank-switching control register
        public const uint C54_GREG_SWCR = 0x002B;	//Software wait-state control register
        public const uint C54_GREG_HPIC = 0x002C;	//Host port interface control register
        public const uint C54_GREG_DRR22 = 0x0030;	//McBSP2 data receive register high
        public const uint C54_GREG_DRR12 = 0x0031;	//McBSP2 data receive register low
        public const uint C54_GREG_DXR22 = 0x0032;	//McBSP2 data transmit register high
        public const uint C54_GREG_DXR12 = 0x0033;	//McBSP2 data transmit register low
        public const uint C54_GREG_SPSA2 = 0x0034;	//McBSP2 sub-address register
        public const uint C54_GREG_SPDR2 = 0x0035;	//McBSP2 sub-address data register
        public const uint C54_GREG_SPSA0 = 0x0038;	//McBSP0 sub-address register
        public const uint C54_GREG_SPDR0 = 0x0039;	//McBSP0 sub-address data register
        public const uint C54_GREG_DRR21 = 0x0040;	//McBSP1 data receive register high
        public const uint C54_GREG_DRR11 = 0x0041;	//McBSP1 data receive register low
        public const uint C54_GREG_DXR21 = 0x0042;	//McBSP1 data transmit register high
        public const uint C54_GREG_DXR11 = 0x0043;	//McBSP1 data transmit register low
        public const uint C54_GREG_SPSA1 = 0x0048;	//McBSP1 sub-address register
        public const uint C54_GREG_SPDR1 = 0x0049;	//McBSP1 sub-address data register
        public const uint C54_GREG_DMPREC = 0x0054;	//DMA channel priority and enable control
        public const uint C54_GREG_DMSBAR = 0x0055;	//DMA channel sub-address register
        public const uint C54_GREG_DMADI = 0x0056;	//DMA channel sub-address data with increment
        public const uint C54_GREG_DMADN = 0x0057;	//DMA channel sub-address data without increment
        public const uint C54_GREG_CLKMD = 0x0058;	//Clock-mode register

        /**
        *
        *  Event types.
        *
        **/

        public const uint EVENTTYPE_1553MSG = 0x0001;	//MIL-STD-1553 message
        public const uint EVENTTYPE_1553OPCODE = 0x0002;	//MIL-STD-1553 event log opcode
        public const uint EVENTTYPE_1553HALT = 0x0003;	//MIL-STD-1553 schedule halt
        public const uint EVENTTYPE_1553PAUSE = 0x0004;	//MIL-STD-1553 schedule pause
        public const uint EVENTTYPE_1553LIST = 0x0005;	//MIL-STD-1553 list buffer empty/full
        public const uint EVENTTYPE_1553SERIAL = 0x0006;	//MIL-STD-1553 serial empty
        public const uint EVENTTYPE_429MSG = 0x0011;	//ARINC 429 message
        public const uint EVENTTYPE_429OPCODE = 0x0012;	//ARINC 429 event log opcode
        public const uint EVENTTYPE_429HALT = 0x0013;	//ARINC 429 schedule halt
        public const uint EVENTTYPE_429PAUSE = 0x0014;	//ARINC 429 schedule pause
        public const uint EVENTTYPE_429LIST = 0x0015;	//ARINC 429 list buffer empty/full
        public const uint EVENTTYPE_429ERR = 0x0016;	//ARINC 429 decoder error detected
        public const uint EVENTTYPE_717WORD = 0x0021;	//ARINC 717 word received
        public const uint EVENTTYPE_717SUBFRM = 0x0022;	//ARINC 717 sub frame completed
        public const uint EVENTTYPE_717SYNCERR = 0x0023;	//ARINC 717 receive channel lost synchronization
        public const uint EVENTTYPE_708MSG = 0x0031;	//ARINC 708 message
        public const uint EVENTTYPE_SEQFULL = 0x0041;	//Sequential record full
        public const uint EVENTTYPE_SEQFREQ = 0x0042;	//Sequential record frequency
        public const uint EVENTTYPE_422TXTHRESHOLD = 0x0051;	//RS-422 TX under threshold
        public const uint EVENTTYPE_422TXFIFO = 0x0052;	//RS-422 TX underflow
        public const uint EVENTTYPE_422RXTHRESHOLD = 0x0053;	//RS-422 RX over threshold
        public const uint EVENTTYPE_422RXFIFO = 0x0054;	//RS-422 RX overflow
        public const uint EVENTTYPE_422RXERROR = 0x0055;	//RS-422 RX error
        public const uint EVENTTYPE_CSDBMSG = 0x0058;	//CSDB message
        public const uint EVENTTYPE_CSDBOPCODE = 0x0059;	//CSDB event log opcode
        public const uint EVENTTYPE_CSDBHALT = 0x005A;	//CSDB schedule halt
        public const uint EVENTTYPE_CSDBPAUSE = 0x005B;	//CSDB schedule pause
        public const uint EVENTTYPE_CSDBLIST = 0x005C;	//CSDB list buffer empty/full
        public const uint EVENTTYPE_CSDBERR = 0x005D;	//CSDB decoder error detected
        public const uint EVENTTYPE_CSDBSYNCERR = 0x005E;	//CSDB receive channel lost synchronization
        public const uint EVENTTYPE_DIOEDGE = 0x0060;	//DIO edge event
        public const uint EVENTTYPE_DIOFAULT = 0x0061;	//DIO fault event
        public const uint EVENTTYPE_BITERROR = 0x0071;	//Built-in Test error event
        public const uint EVENTTYPE_EBRMSG = 0x0081;	//EBR message
        public const uint EVENTTYPE_EBROPCODE = 0x0082;	//EBR event log opcode
        public const uint EVENTTYPE_EBRHALT = 0x0083;	//EBR schedule halt
        public const uint EVENTTYPE_EBRPAUSE = 0x0084;	//EBR schedule pause
        public const uint EVENTTYPE_EBRLIST = 0x0085;	//EBR list buffer empty/full
        public const uint EVENTTYPE_EBRRESV = 0x0086;	//EBR Reserved

        /**
        *
        *  Card Info types
        *
        **/

        public const uint INFOTYPE_PLAT = 0x0001;	//Returns the platform type
        public const uint INFOTYPE_PROD = 0x0002;	//Returns the product type
        public const uint INFOTYPE_GEN = 0x0003;	//Returns the generation number
        public const uint INFOTYPE_1553COUNT = 0x0004;	//Returns the 1553 channel count
        public const uint INFOTYPE_1553SIZE = 0x0005;	//Returns the 1553 channel size
        public const uint INFOTYPE_429COUNT = 0x0006;	//Returns the 429 channel count
        public const uint INFOTYPE_429SIZE = 0x0007;	//Returns the 429 channel size
        public const uint INFOTYPE_717COUNT = 0x0008;	//Returns the 717 channel count
        public const uint INFOTYPE_717SIZE = 0x0009;	//Returns the 717 channel size
        public const uint INFOTYPE_708COUNT = 0x000A;	//Returns the 708 channel count
        public const uint INFOTYPE_708SIZE = 0x000B;	//Returns the 708 channel size
        public const uint INFOTYPE_VERSION = 0x000C;	//Returns the version number
        public const uint INFOTYPE_DATE = 0x000D;	//Returns the version date
        public const uint INFOTYPE_CHINFO = 0x000E;	//Returns the channel info
        public const uint INFOTYPE_422COUNT = 0x000F;	//Returns the 422 port count
        public const uint INFOTYPE_422SIZE = 0x0010;	//Returns the 422 port size
        public const uint INFOTYPE_CSDBCOUNT = 0x0011;	//Returns the CSDB channel count
        public const uint INFOTYPE_CSDBSIZE = 0x0012;	//Returns the CSDB channel size
        public const uint INFOTYPE_DIOCOUNT = 0x0013;	//Returns the DIO bank count
        public const uint INFOTYPE_DIOSIZE = 0x0014;	//Returns the DIO bank size
        public const uint INFOTYPE_HWGEN = 0x0015;	//Returns the Hardware Generation
        public const uint INFOTYPE_EBRCOUNT = 0x0016;	//Returns the EBR channel count
        public const uint INFOTYPE_EBRSIZE = 0x0017;	//Returns the EBR channel size
        public const uint INFOTYPE_CARDTYPE = 0x0018;	//Returns the card type
        public const uint INFOTYPE_SERIALNUM = 0x0019;	//Returns the serial number
        public const uint INFOTYPE_VERSIONEX = 0x001A;	//Returns the version number including minor-minor

        /**
        *
        *  Co-Processor Info types
        *
        **/

        public const uint COPROCINFO_PLAT = 0x0001;	//Returns the platform type
        public const uint COPROCINFO_PROD = 0x0002;	//Returns the product type
        public const uint COPROCINFO_GEN = 0x0003;	//Returns the generation number
        public const uint COPROCINFO_VERSION = 0x0004;	//Returns the version number (major.minor)
        public const uint COPROCINFO_DATE = 0x0005;	//Returns the version date
        public const uint COPROCINFO_CISADDR = 0x0006;	//Returns the CIS address
        public const uint COPROCINFO_DMA = 0x0007;	//Returns whether or not CoProc supports DMA mode
        public const uint COPROCINFO_VERSIONEX = 0x0009;	//Returns the version number (major.minor.minorminor)
        public const uint COPROCINFO_PN = 0x000A;	//Returns software part number

        /**
        *
        *  CIS types
        *
        **/

        public const uint CISTYPE_CARD = 0x0001;	//Select card CIS
        public const uint CISTYPE_IOMODULE = 0x0002;	//Select I/O module CIS
        public const uint CISTYPE_PLX = 0x0003;	//Select PLX EEPROM
        public const uint CISTYPE_INFO = 0x0004;	//Select Info module CIS
        public const uint CISTYPE_ENV = 0x0005;	//Select environment
        public const uint CISTYPE_BIT = 0x0006;	//Select bitfile

        /**
        *
        *  Handle Info types
        *
        **/

        public const uint HANDINFO_CORENUM = 0x0001;	//Returns the Handle Core number
        public const uint HANDINFO_RPC = 0x0002;	//Returns the Handle Core number

        /**
        *
        *  Trigger flags
        *
        **/

        public const uint TRIGMASK_TRIGA = 0x0001;	//Selects trigger line A from default DIN
        public const uint TRIGMASK_TRIGB = 0x0002;	//Selects trigger line B from default DIN
        public const uint TRIGMASK_TRIGC = 0x0004;	//Selects trigger line C from default DIN
        public const uint TRIGMASK_PXITRIGA = 0x0010;	//Selects trigger line A from PXI_TRIG[0]
        public const uint TRIGMASK_PXITRIGB = 0x0020;	//Selects trigger line B from PXI_TRIG[1]
        public const uint TRIGMASK_PXITRIGC = 0x0040;	//Selects trigger line C from PXI_TRIG[2]
        public const uint TRIGMASK_PXISTARA = 0x0100;	//Selects trigger line A from PXIe_DSTARA
        public const uint TRIGMASK_PXISTARB = 0x0200;	//Selects trigger line B from PXIe_DSTARB
        public const uint TRIGMASK_PXISTARC = 0x0400;	//Selects trigger line C from PXI_STAR
        public const uint TRIGVAL_TRIGAOFF = 0x0000;	//Tests for trigger line A inactive
        public const uint TRIGVAL_TRIGAON = 0x0001;	//Tests for trigger line A active
        public const uint TRIGVAL_TRIGBOFF = 0x0000;	//Tests for trigger line B inactive
        public const uint TRIGVAL_TRIGBON = 0x0002;	//Tests for trigger line B active
        public const uint TRIGVAL_TRIGCOFF = 0x0000;	//Tests for trigger line C inactive
        public const uint TRIGVAL_TRIGCON = 0x0004;	//Tests for trigger line C active
        public const uint TRIGPOL_TRIGAL = 0x0000;	//Sets active low polarity for trigger line A
        public const uint TRIGPOL_TRIGAH = 0x0001;	//Sets active high polarity for trigger line A
        public const uint TRIGPOL_TRIGAF = 0x0010;	//Sets active on falling edge of trigger line A
        public const uint TRIGPOL_TRIGAR = 0x0011;	//Sets active on rising edge of trigger line A
        public const uint TRIGPOL_TRIGAMASK = 0x0011;	//Mask of all trigger line A options
        public const uint TRIGPOL_TRIGBL = 0x0000;	//Sets active low polarity for trigger line B
        public const uint TRIGPOL_TRIGBH = 0x0002;	//Sets active high polarity for trigger line B
        public const uint TRIGPOL_TRIGBF = 0x0020;	//Sets active on falling edge of trigger line B
        public const uint TRIGPOL_TRIGBR = 0x0022;	//Sets active on rising edge of trigger line B
        public const uint TRIGPOL_TRIGBMASK = 0x0022;	//Mask of all trigger line B options
        public const uint TRIGPOL_TRIGCL = 0x0000;	//Sets active low polarity for trigger line C
        public const uint TRIGPOL_TRIGCH = 0x0004;	//Sets active high polarity for trigger line C
        public const uint TRIGPOL_TRIGCF = 0x0040;	//Sets active on falling edge of trigger line C
        public const uint TRIGPOL_TRIGCR = 0x0044;	//Sets active on rising edge of trigger line C
        public const uint TRIGPOL_TRIGCMASK = 0x0044;	//Mask of all trigger line C options

        /**
        *
        *  Sync flags
        *
        **/

        public const uint SYNCMASK_SYNCA = 0x0001;	//Selects sync line A
        public const uint SYNCMASK_SYNCB = 0x0002;	//Selects sync line B
        public const uint SYNCMASK_SYNCC = 0x0004;	//Selects sync line C
        public const uint SYNCMASK_PXITRIGA = 0x0100;	//Selects sync line A to PXI_TRIG 0
        public const uint SYNCMASK_PXITRIGB = 0x0200;	//Selects sync line B to PXI_TRIG 1
        public const uint SYNCMASK_PXITRIGC = 0x0400;	//Selects sync line Cto PXI_TRIG 2
        public const uint SYNCMASK_PXISTARC = 0x4000;	//Selects sync line Cto PXI STAR
        public const uint SYNCPOL_SYNCAL = 0x0000;	//Sets active low polarity for sync line A
        public const uint SYNCPOL_SYNCAH = 0x0001;	//Sets active high polarity for sync line A
        public const uint SYNCPOL_SYNCBL = 0x0000;	//Sets active low polarity for sync line B
        public const uint SYNCPOL_SYNCBH = 0x0002;	//Sets active high polarity for sync line B
        public const uint SYNCPOL_SYNCCL = 0x0000;	//Sets active low polarity for sync line C
        public const uint SYNCPOL_SYNCCH = 0x0004;	//Sets active high polarity for sync line C

        /**
        *
        *  PXI Status flags
        *
        **/

        public const uint PXITYPE_GEOADDR = 0x0001;	//Return the Geographical Address
        public const uint PXITYPE_CLKSEL = 0x0002;	//Return the Clock Selection (Local oscillator vs PXIe_CLK100)
        public const uint PXITYPE_TRIGVERS = 0x0003;	//Return the Version of the triggers
        public const uint PXITYPE_OUTEN = 0x0004;	//Return the bitmask of output enables for dionums 33 to 48

        /**
        *
        *  Heap flags.
        *
        **/

        public const uint HEAPCFG_DEFAULT = 0x0000;	//Block is allocated within page
        public const uint HEAPCFG_TEST = 0x0001;	//Returns address but doesn't make it permanent
        public const uint HEAPCFG_NULL = 0x0002;	//Returns address but doesn't mark it as in use
        public const uint HEAPCFG_SPAN = 0x0004;	//Block can span pages
        public const uint HEAPCFG_QUICK = 0x0008;	//Does a quick allocation
        public const uint HEAPCFG_NOALIGN = 0x0010;	//Relaxes alignment rules to help fit
        public const uint HEAPSECT_SRAM = 0x0000;	//Heap section for primary SRAM

        /**
        *
        *  Timer resolutions.
        *
        **/

        public const int TIMERRESOL_CURRENT = 0;	//Keep the current timer resolution
        public const int TIMERRESOL_1US = 1;	//1us timer resolution
        public const int TIMERRESOL_16US = 2;	//16us timer resolution
        public const int TIMERRESOL_1024US = 3;	//1024us timer resolution
        public const int TIMERRESOL_1NS = 4;	//1ns timer resolution

        /**
        *
        *  Timer status.
        *
        **/

        public const int TIMETAG_FORMAT_BIN = 0;	//Timetag will be in binary format
        public const int TIMETAG_FORMAT_BCD = 1;	//Timetag will be in BCD format

        /**
        *
        *  Interval calculation modes.
        *
        **/

        public const int INTERVALMODE_CLOSEST = 1;	//Finds interval closest to value specified
        public const int INTERVALMODE_LESS = 2;	//Finds closest interval less than value specified
        public const int INTERVALMODE_GREATER = 3;	//Finds closest interval more than value specified

        /**
        *
        *  Test flags.
        *
        **/

        public const int TEST_LEVEL_0 = 0;	//Test I/O interface
        public const int TEST_LEVEL_1 = 1;	//Test memory interface
        public const int TEST_LEVEL_2 = 2;	//Test communication process
        public const int TEST_LEVEL_3 = 3;	//Test bus transceiver

        /**
        *
        *  Status flags.
        *
        **/

        public const int STAT_EMPTY = 0;	//Buffer is empty
        public const int STAT_PARTIAL = 1;	//Buffer is partially filled
        public const int STAT_FULL = 2;	//Buffer is full
        public const int STAT_OFF = 3;	//Buffer is off

        /**
        *
        *  SysMon flags
        *
        **/

        public const int SYSMONTYPE_NONE = 0;	//Sensor not present
        public const int SYSMONTYPE_TEMP = 1;	//Temperature sensor
        public const int SYSMONTYPE_VOLTAGE = 2;	//Voltage sensor
        public const int SYSMONTYPE_CURRENT = 3;	//Current sensor

        /**
        *
        *  SysMon Read Constant
        *
        **/

        public const int SYSMONRD_NOTVALID = -1000000;	//SysMon value not available

        /**
        *
        *  BIT flags
        *
        **/

        public const uint BITCFG_DEFAULT = 0x00000000;	//Select all default settings
        public const uint BITCFG_MEMECC_ENABLE = 0x00000000;	//Enables Stopping card operation on memory ECC failure  ie uncorrectable error(default)
        public const uint BITCFG_MEMECC_DISABLE = 0x00000001;	//Disables Stopping card operation on memory ECC failure  ie uncorrectable error
        public const uint BITCFG_FPGA_ENABLE = 0x00000000;	//Enables FPGA Single Event Upset (SEU) detection (default)
        public const uint BITCFG_FPGA_DISABLE = 0x00000002;	//Disables FPGA Single Event Upset (SEU) detection
        public const uint BITCFG_PROTOCOL_ENABLE = 0x00000000;	//Enables Protocol error reporting (default)
        public const uint BITCFG_PROTOCOL_DISABLE = 0x00000004;	//Disables Protocol error reporting
        public const uint BITCFG_MEMECC_NOLOG = 0x00000000;	//Do not generate event logs when ECC event occurs (default)
        public const uint BITCFG_MEMECC_LOG = 0x00000010;	//Generate event logs when ECC event occurs
        public const uint BITCFG_FPGA_NOLOG = 0x00000000;	//Do not generate event logs when FPGA Single Event Upset (SEU) event occurs (default)
        public const uint BITCFG_FPGA_LOG = 0x00000020;	//Generate event logs when FPGA Single Event Upset (SEU) event occurs
        public const uint BITCFG_PROTOCOL_NOLOG = 0x00000000;	//Do not generate event logs when Protocol error occurs (default)
        public const uint BITCFG_PROTOCOL_LOG = 0x00000040;	//Generate event logs when Protocol error occurs
        public const uint BITCFG_CARD_STOPPED_NOLOG = 0x00000000;	//Do not generate event logs when the card is stopped due to CBIT error (default)
        public const uint BITCFG_CARD_STOPPED_LOG = 0x00000080;	//Generate event logs when the card is stopped due to CBIT error
        public const uint BITCFG_SYSMON_NOLOG = 0x00000000;	//Do not generate event logs when System Monitor error occurs (default)
        public const uint BITCFG_SYSMON_LOG = 0x00000100;	//Generate event logs when System Monitor error occurs

        public const uint BITSTAT_NOSTATUS = 0x00000000;	//No errors or warnings for BIT
        public const uint BITSTAT_CARD_STOPPED = 0x20000000;	//Continuous BIT fatal system error card stopped
        public const uint BITSTAT_CBIT_FPGA_ERR = 0x10000000;	//Continuous BIT FPGA error (SEU)
        public const uint BITSTAT_SINGLE_BIT_ERR = 0x08000000;	//Continuous BIT Single Bit Error (Correctable)
        public const uint BITSTAT_DOUBLE_BIT_ERR = 0x04000000;	//Continuous BIT Double Bit Error
        public const uint BITSTAT_SYSMON_ERR = 0x0000FFFF;	//Continuous BIT System Monitor error
        public const uint BITSTAT_PROTOCOL_ERR = 0x00FF0000;	//Continuous BIT Protocol Error

        /**
        *
        *  Timing Synchronization Manager configuration options
        *
        **/

        public const uint TSMCFG_PPS0 = 0x00000001;	//PPS on DC Pin 0
        public const uint TSMCFG_PPS1 = 0x00000002;	//PPS on DC Pin 1
        public const uint TSMCFG_PWMIRIG0 = 0x00000004;	//DC IRIG on DC Pin 0
        public const uint TSMCFG_PWMIRIG1 = 0x00000008;	//DC IRIG on DC Pin 1
        public const uint TSMCFG_AMIRIG = 0x00000010;	//AM IRIG
        public const uint TSMCFG_10MHZ = 0x00000020;	//10MHz (output and Drift control input only)
        public const uint TSMCFG_HOST = 0x00000040;	//Host (or 1588) control (input-only)
        public const uint TSMCFG_PPSMODE = 0x00000080;	//IRIG is in PPS Mode (input-only)
        public const uint TSMCFG_AMDACAUTO = 0x00000000;	//Enable auto-threshold determination for the IRIG AM high dac
        public const uint TSMCFG_AMDACUSER = 0x00000100;	//User control of threshold for the IRIG AM high dac
        public const uint TSMCFG_IRIGDAYS = 0x00000200;	//Select IRIG days counting from 1-365.  Default is 0-364
        public const uint TSMCFG_NONE = 0x00000000;	//Timer is free-running
        public const uint TSMCFG_IRIGA = 0x00010000;	//Set IRIG Time Code to A
        public const uint TSMCFG_IRIGB = 0x00000000;	//Set IRIG Time Code to B (default)

        /**
        *
        *  Timing Synchronization Manager status flags
        *
        **/

        public const uint TSMSTAT_IRIGPRES = 0x00000001;	//Input IRIG signal is toggling and decodable
        public const uint TSMSTAT_IRIGBITSYNC = 0x00000002;	//Input IRIG signal is toggling
        public const uint TSMSTAT_IRIGSYNC = 0x00000004;	//TSM is locked to IRIG input
        public const uint TSMSTAT_10MHZPRES = 0x00000008;	//Input 10 MHz signal is toggling
        public const uint TSMSTAT_10MHZSYNC = 0x00000010;	//TSM is locked to 10 MHz input
        public const uint TSMSTAT_PPSPRES = 0x00000020;	//Input PPS signal is toggling
        public const uint TSMSTAT_PPSSYNC = 0x00000040;	//TSM is locked to PPS input
        public const uint TSMSTAT_OUTOFBOUNDS = 0x00000080;	//Timing source is beyond drift synchronization threshold

        /**
        *
        *  Timing Synchronization Manager pin selection values
        *
        **/

        public const uint TSMPIN_PWMIRIG0 = 0x0001;	//Index of PWM IRIG/PPS Pin 0 Threshold
        public const uint TSMPIN_PWMIRIG1 = 0x0002;	//Index of PWM IRIG/PPS Pin 1 Threshold
        public const uint TSMPIN_AMIRIGH = 0x0003;	//Index of AM IRIG Pin High Threshold
        public const uint TSMPIN_AMIRIGL = 0x0004;	//Index of AM IRIG Pin Low Threshold
        public const uint TSMPIN_10MHZ = 0x0005;	//Index of 10MHz Pin Threshold

        /**
        *
        *  DIO Configuration selection values
        *
        **/

        public const int EXTDIOBANKCFG_INPUTMODE = 1;	//Set input mode to Open/Ground or 28V/Open (0-Open/Gnd 1=28V/Open)

        /**
        *
        *  Other flags.
        *
        **/

        public const int RCV = 0;
        public const int XMT = 1;

        /**
        *
        *  Error types.
        *
        **/

        public const int ERR_NONE = 0;	//No error
        public const int ERR_UNKNOWN = -1;	//An unexpected error occurred
        public const int ERR_BADVER = -2;	//A bad version was encountered
        public const int ERR_BADPTR = -3;	//A bad pointer was passed
        public const int ERR_NOCORE = -4;	//The specified core number doesn't exist
        public const int ERR_BADPARAMS = -11;	//Function called with bad parameters
        public const int ERR_NOHANDLES = -12;	//CardOpen() already has allocated too many handles
        public const int ERR_NOCARD = -13;	//CardOpen() could not find a L43 card at the specified address
        public const int ERR_NOIO = -14;	//CardOpen() could not find the I/O ports
        public const int ERR_NOMEM = -15;	//CardOpen() could not find the memory
        public const int ERR_BAD16BIT = -16;	//Card is conflicting with another 16-bit card
        public const int ERR_WRONGMODEL = -17;	//Card does not support this feature
        public const int ERR_NOSEL = -18;	//CardOpen() could not allocate a memory selector
        public const int ERR_LOCK = -19;	//The communication process is locked up
        public const int ERR_TOOMANY = -20;	//Too many channels have been configured
        public const int ERR_BADHANDLE = -21;	//A bad handle was specified
        public const int ERR_GOODHANDLE = -22;	//The handle is still valid and should not be destroyed
        public const int ERR_NOTCHAN = -23;	//Not a valid channel
        public const int ERR_NOTXMT = -24;	//The Transmitter has not been configured
        public const int ERR_NOTRCV = -25;	//The Receiver has not been configured
        public const int ERR_NOTSEQ = -26;	//The Sequential Record has not been configured
        public const int ERR_ALLOC = -27;	//There is not enough memory to allocate
        public const int ERR_VXD = -28;	//An error occurred in the VXD
        public const int ERR_BADLABEL = -29;	//The specified label value is not valid
        public const int ERR_BADSDI = -30;	//The specified sdi value is not valid
        public const int ERR_BADMSG = -31;	//The specified command block is not a message block
        public const int ERR_BADSCHNDX = -32;	//Specified command index is out of range
        public const int ERR_BUFSIZE = -33;	//Insufficient space in user buffer
        public const int ERR_NOCONFIG = -34;	//The card has not been properly configured
        public const int ERR_CONFLICTS = -35;	//Unable to resolve conflicts
        public const int ERR_RANGE = -36;	//Schedule is out of range
        public const int ERR_FACTOR = -37;	//A bad factor value was specified
        public const int ERR_NOIOBOOT = -40;	//Could not talk to IO Boot port of DSP
        public const int ERR_BOOTFULL = -41;	//No space to add boot code
        public const int ERR_BOOTNUM = -42;	//There is no boot code with the specified number
        public const int ERR_ACCESS = -43;	//Unable to write to access register
        public const int ERR_ROMVERIFY = -44;	//Unable to verify the value written to the ROM
        public const int ERR_COUNT = -45;	//An invalid count was specified
        public const int ERR_CRC = -46;	//There was a bad checksum in the HEX file
        public const int ERR_FNAME = -47;	//Bad filenames were specified
        public const int ERR_FRDWR = -48;	//There was an error reading or writing the HEX file
        public const int ERR_HEX = -49;	//There was a bad hex character in the HEX file
        public const int ERR_INDEX = -51;	//The command block index was invalid or the schedule is full
        public const int ERR_NOMSGS = -52;	//No messages specified
        public const int ERR_TYPE = -54;	//There was a bad type value in the HEX file
        public const int ERR_ZEROLEN = -55;	//Zero length was specified
        public const int ERR_BADADDRESS = -56;	//A bad address was specified
        public const int ERR_MSGNDX = -57;	//A bad message index was specified
        public const int ERR_BADTA = -60;	//A bad terminal address was specified
        public const int ERR_BADFRAME = -61;	//A bad frame time was specified
        public const int ERR_NOTBC = -62;	//The BC has not been configured
        public const int ERR_NOTRT = -63;	//The RT has not been configured
        public const int ERR_NOTMON = -64;	//The monitor has not been configured
        public const int ERR_SELFIOFAIL = -71;	//I/O selftest failed
        public const int ERR_SELFMEMFAIL = -72;	//Memory selftest failed
        public const int ERR_SELFCOMMFAIL = -73;	//Communication selftest failed
        public const int ERR_SELFXMTFAIL = -74;	//Transmit selftest failed
        public const int ERR_PLXBUG = -75;	//PLX bug is causing problems
        public const int ERR_NOTSUPPORTED = -76;	//Base class does not support feature
        public const int ERR_DLL = -77;	//A required DLL is missing
        public const int ERR_SEQTYPE = -80;	//Invalid sequential record type value
        public const int ERR_SEQNEXT = -81;	//Next sequential record does not exist
        public const int ERR_SEQFINDINFO = -82;	//The SEQFINDINFO structure is not valid
        public const int ERR_SEQBASEPTR = -83;	//The base pointer passed is invalid
        public const int ERR_SEQMORE = -84;	//More (extended) record data does not exist
        public const int ERR_TIMEOUT = -90;	//Function timed out waiting for data
        public const int ERR_SUBFRMNUM = -101;	//Invalid SubFrame number was specified
        public const int ERR_WORDNUM = -102;	//Invalid Word number was specified
        public const int ERR_NOTINSYNC = -103;	//Not Synchronized to databus
        public const int ERR_SUPERFRM = -104;	//SuperFrame not configured
        public const int ERR_SUPERFRMNUM = -105;	//Invalid SuperFrame number was specified
        public const int ERR_BADPORT = -106;	//A bad port number was specified
        public const int ERR_NOTLHUB = -107;	//The Logical Hub has not been configured
        public const int ERR_UNDERFLOW = -108;	//The read failed because the buffer is empty
        public const int ERR_OVERFLOW = -109;	//The write failed because the buffer is full
        public const int ERR_ENDIAN = -110;	//An endianness mismatch was detected
        public const int ERR_NOCLK = -111;	//Protocol clock source is missing or invalid

    }	//end class
}	//end namespace
