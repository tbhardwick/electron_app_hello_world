using System;
using System.Runtime.InteropServices;

namespace Cargo_Door_Base.BTI
{
    public class BTI429
    {
        /**
        *
        *  BTI429 WIN32 DRIVER  Version 1.12.14  (02/03/2020)
        *  Copyright 2001-2020
        *  Astronics Advanced Electronic Systems Corp.
        *  www.astronics.com
        *  Ballard.Support@astronics.com
        *
        *  NAME:   BTI429NET.CS -- C# 32-bit/64-bit
        *                      BTI429 Driver Include Header File.
        *
        **/

        //OS Compatibility definitions
#if BUILT_FOR_WINDOWS
			const string LIBNAME32 = "BTI429.dll";
			const string LIBNAME64 = "BTI42964.dll";
#elif BUILT_FOR_LINUX
			const string LIBNAME32 = "libbti429.so";
			const string LIBNAME64 = "libbti429.so";
#else
        const string LIBNAME32 = "BTI429.dll";
        const string LIBNAME64 = "BTI42964.dll";
#endif

        /**
		*
		*  Structs used by the BTI429 Driver.
		*
		**/

        [StructLayout(LayoutKind.Sequential)]
        public struct MSGSTRUCT429
        {
            public uint addr;
            public uint data;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSGFIELDS429
        {
            public ushort msgopt;              //Driver writes message optionvals
            public ushort msgact;              //Card writes message activity
            public uint msgdata_listptr; //Card/User writes message data (Rcv/Xmt) - //Driver writes list buffer pointer
            public uint timetag_hitcount; //Card writes timetag - //Card writes hit count
            public uint maxtime_elapsetime; //Card writes max time - //Card writes elapsed time
            public uint mintime;             //Card writes min time
            public uint userptr;             //Driver writes user code pointer
            public uint timetagh;            //Card writes extended timetag
            public ushort decgap;              //Card writes measured decoder gap time (rsvd for xmt msg)
            public ushort paramflag;           //Driver/User writes parametric bitrate configuration
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PARAMFIELDS429
        {
            public int highvolt;  // Absolute voltage of the first half of one-bit (in millivolts)
            public int lowvolt;  // Absolute voltage of the second half of one-bit (in millivolts)
            public int nullvolt;  // Absolute voltage of the second half of one-bit (in millivolts)
            public int highrise;  // rise and fall time of the one-bit (in nanoseconds)
            public int lowrise;  // Rise and fall time of the zero-bit (in nanoseconds)
            public int biasvolt;  // Common-mode bias of the entire signal (in millivolts)
        }

        /**
        *
        *  BTI429 Driver functions.
        *
        **/

        /**
        *
        *  Internal functions used by the BTI429 Driver.
        *
        **/

        private static class BTI429_32
        {
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDGetData@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDGetData(uint msg, ushort msb, ushort lsb);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDGetMant@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDGetMant(uint msg, ushort sigdig);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDGetSign@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BCDGetSign(uint msg);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDGetSSM@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BCDGetSSM(uint msg);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDGetValue@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern double BTI429_BCDGetValue(uint msg, ushort sigdig, double resolstr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDPutData@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutData(uint msg, uint value, ushort msb, ushort lsb);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDPutMant@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutMant(uint msg, uint value, ushort sigdig, ushort sign);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDPutSign@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutSign(uint msg, ushort sign);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDPutSSM@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutSSM(uint msg, ushort value);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDPutValue@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutValue(double dblvalue, uint msg, ushort sigdig, double dblresolution);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRAngularGetValue@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern double BTI429_BNRAngularGetValue(uint msg, ushort sigbit, int bplusminus180);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRAngularPutValue@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRAngularPutValue(double dblvalue, uint msg, ushort sigbit);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRGetData@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRGetData(uint msg, ushort msb, ushort lsb);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRGetMant@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRGetMant(uint msg, ushort sigbit);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRGetSign@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BNRGetSign(uint msg);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRGetSSM@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BNRGetSSM(uint msg);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRGetValue@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern double BTI429_BNRGetValue(uint msg, ushort sigbit, double dblresolution);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRPutData@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutData(uint msg, uint value, ushort msb, ushort lsb);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRPutMant@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutMant(uint msg, uint value, ushort sigbit, ushort twos);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRPutSign@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutSign(uint msg, ushort twos);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRPutSSM@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutSSM(uint msg, ushort value);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRPutValue@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutValue(double dblvalue, uint msg, ushort sigbit, double dblresolution);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChClear(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChConfig(uint configval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChGetCount@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ChGetCount(ref int rcvcount, ref int xmtcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChGetInfo@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ChGetInfo(ushort infotype, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChIs429@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChIs429(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChIsRcv@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChIsRcv(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChIsXmt@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChIsXmt(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChPause@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ChPause(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChPauseCheck@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChPauseCheck(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChResume@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ChResume(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChStart@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChStart(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChStop@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChStop(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChSyncDefine@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChSyncDefine(int enableflag, ushort syncmask, ushort pinpolarity, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChTriggerDefine@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChTriggerDefine(int enableflag, ushort trigmask, ushort trigval, ushort pinpolarity, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_DriverInfoStr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTI429_DriverInfoStr();
            public static string BTI429_DriverInfoStr() { return Marshal.PtrToStringAnsi(_BTI429_DriverInfoStr()); }
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_FilterClear(uint basevaladdr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterDefault@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterDefault(uint configval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterRd(int labelval, int sdival, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterSet@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterSet(uint configval, int labelval, int sdimask, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterWr@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_FilterWr(uint msgaddr, int labelval, int sdival, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldGetData@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldGetData(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldGetLabel@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetLabel(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldGetParity@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetParity(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldGetSDI@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetSDI(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldGetValue@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldGetValue(uint msgval, ushort startbit, ushort endbit);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldPutData@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutData(uint msgval, uint data);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldPutLabel@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutLabel(uint msgval, ushort label);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldPutSDI@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutSDI(uint msgval, ushort sdi);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldPutValue@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutValue(uint msgval, uint dataval, ushort startbit, ushort endbit);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListAsyncCreate@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListAsyncCreate(uint listconfigval, int count, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListDataRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListDataRd(uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListDataWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataWr(uint value, uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListRcvCreate@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListRcvCreate(uint listconfigval, int count, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListStatus@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListStatus(uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListXmtCreate@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListXmtCreate(uint listconfigval, int count, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgBlockRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgBlockRd(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgBlockWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgBlockWr(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgCountRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgCountRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgCountWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgCountWr(int bitcount, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgCreate@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgCreate(uint configval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgDataCountWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgDataCountWr(uint dataval, int bitcount, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgDataRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgDataRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgDataWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgDataWr(uint value, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgGroupBlockRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupBlockRd([In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MSGFIELDS429[] msgfields, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgGroupBlockWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupBlockWr([In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MSGFIELDS429[] msgfields, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgGroupRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupRd(uint[] msgdataptr, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgGroupWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupWr(uint[] msgdataptr, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgIsAccessed@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgIsAccessed(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgSkipRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgSkipRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgSkipWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgSkipWr(int skip, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgSyncDefine@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgSyncDefine(int enableflag, ushort syncmask, ushort pinpolarity, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgTriggerDefine@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgTriggerDefine(int enableflag, ushort trigmask, ushort trigval, ushort pinpolarity, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ParamAmplitudeConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamAmplitudeConfig(uint configval, ushort dacval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ParamAmplitudeGet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamAmplitudeGet(ref ushort dacval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ParamBitCountBuild@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_ParamBitCountBuild(int bitcount);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ParamBitRateConfig@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamBitRateConfig(uint configval, ushort divval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ParamBitRateGet@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamBitRateGet(ref ushort divval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayBlockWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayBlockWr(ushort[] playbuf, int count, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayPutData@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutData(uint dataval, int bitcount, ushort gapval, int offset, ushort[] playbuf);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayPutGap@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutGap(ushort gapval, int offset, ushort[] playbuf);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayPutLog@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutLog(uint condition, ushort tagval, int offset, ushort[] playbuf);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayPutPulse0@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutPulse0(int dionum, int offset, ushort[] playbuf);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayPutPulse1@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutPulse1(int dionum, int offset, ushort[] playbuf);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayStatus@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayStatus(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBranch@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBranch(uint condition, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBranchUpdate@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBranchUpdate(int destindex, int opcodeindex, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBuild@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuild(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCall@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCall(uint condition, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCallUpdate@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCallUpdate(int destindex, int opcodeindex, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedData@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedData(uint dataval, int bitcount, ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedEntry@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedEntry(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedGap@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedGap(ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedGapFixed@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedGapFixed(ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedGapList@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedGapList(ushort gapval, uint listaddr, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedHalt@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedHalt(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedLog@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedLog(uint condition, ushort tagval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedMode@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedMode(uint modeval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedMsg@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedMsg(uint msgaddr, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedMsgEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedMsgEx(uint msgaddr, ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedPause@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPause(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedPulse@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPulse(int dionum, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedRestart@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedRestart(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedReturn@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedReturn(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDGetVal@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_BCDGetVal(System.Text.StringBuilder buf, uint msg, ushort sigdig, string resolstr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BCDPutVal@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutVal(string buf, uint msg, ushort sigdig, string resolstr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRGetVal@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_BNRGetVal(System.Text.StringBuilder buf, uint msg, ushort sigbit, string resolstr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_BNRPutVal@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutVal(string buf, uint msg, ushort sigbit, string resolstr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChConfigEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChConfigEx(uint configval, ushort count, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ChOutputStateSet@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChOutputStateSet(uint pos, uint neg, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdAddr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdAddr(int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdAlloc@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdAlloc(int count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdAllocEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdAllocEx(uint configval, int count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdBaseRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdBaseRd(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdBaseWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdBaseWr(uint addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdClear(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdCountRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdCountRd(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdCountWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdCountWr(ushort countval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdCtrlRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdCtrlRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdCtrlWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdCtrlWr(ushort ctrlval, int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdCurrRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdCurrRd(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdCurrWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdCurrWr(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdInit@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdInit(ushort opcode, ref ushort block);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdInsert@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdInsert(ref ushort block, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdMaxLoopRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdMaxLoopRd(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdMaxLoopWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdMaxLoopWr(ushort countval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdRd(ref ushort block, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdRestart@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdRestart(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdShotRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdShotRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdShotWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdShotWr(int value, int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdSkipRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdSkipRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdSkipWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdSkipWr(int value, int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdStackClr@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdStackClr(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdStartRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdStartRd(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdStartWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdStartWr(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdStepRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdStepRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdStepWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdStepWr(int value, int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdTotalRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdTotalRd(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdTotalWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdTotalWr(int countval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_CmdWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdWr(ref ushort block, uint addrval, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ConvFromArinc@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ConvFromArinc(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ConvToArinc@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ConvToArinc(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_DllUsageCount@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_DllUsageCount();
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterAddr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterAddr(int labelval, int sdival, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterCreate@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterCreate(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FilterFill@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_FilterFill(uint msgaddr, uint basevaladdr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldGetParBit@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetParBit(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_FldPutParBit@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutParBit(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_GetChanCount@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_GetChanCount(ref int rcvcount, ref int xmtcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_IsChan@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_IsChan(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_IsRcvChan@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_IsRcvChan(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_IsXmtChan@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_IsXmtChan(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_LabelReverse@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_LabelReverse(uint msgval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListAddr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListAddr(int index, uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListAddrEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListAddrEx(int index, uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListAdvance@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListAdvance(uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListClear@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListClear(uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListConfig(uint configval, int count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListConfigEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListConfigEx(uint configval, int count, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListDataBlkRd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataBlkRd(uint[] dataptr, ref ushort datacountptr, uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListDataBlkWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataBlkWr(uint[] dataptr, ushort datacount, uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListDataBlkWrEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataBlkWrEx(uint[] dataptr, ref ushort datacountptr, uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListDataRdEx@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListDataRdEx(int index, uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListDataWrEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListDataWrEx(uint value, int index, uint listaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListInit@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListInit(uint listaddr, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListNextRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListNextRd(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListNextWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListNextWr(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListPostRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPostRd(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListPostWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPostWr(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListPreRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPreRd(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ListPreWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPreWr(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgActivityRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_MsgActivityRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgActivityWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgActivityWr(ushort value, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgCheck@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_MsgCheck(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgCommRd@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgCommRd(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgCommWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgCommWr(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgConfig@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgConfig(uint configval, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgMultiSkipWr@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgMultiSkipWr(int[] skip, uint[] msgaddr, int msgcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgOptRd@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_MsgOptRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgOptWr@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgOptWr(ushort msgopt, uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_MsgReset@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgReset(uint msgaddr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_PlayPutUser@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutUser(uint useraddr, int offset, ushort[] playbuf);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ParamWaveformConfig@24", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamWaveformConfig(uint configval, int highvolt, int lowvolt, int nullvolt, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_ParamWaveformConfigEx@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamWaveformConfigEx(uint configval, ref PARAMFIELDS429 paramflds, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched1Build@44", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched1Build(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiodin, int[] maxperiodin, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched2AdvanceMessage@48", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2AdvanceMessage(int entries, int panicindex, int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched2AdvanceNoMessage@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2AdvanceNoMessage(int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched2Analyze@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_Sched2Analyze(int nummsgs, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched2Build@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2Build(int nummsgs, uint[] msgaddrptr, int[] minlist, int[] maxlist, int speedflag, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched2BuildEx@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2BuildEx(ref int styleptr, int nummsgs, int[] minlist, int[] maxlist, int speedflag, int microflag, int interceptflag);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched2BuildExx@60", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2BuildExx(int style, int speedflag, int microflag, int debugvalflag, int tryflag, int nummsgs, int[] minlist, int[] maxlist, ref int entriesptr, ref int maxentriesptr, nint infoptr, uint[] msgaddrptr, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched3Build@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched3Build(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched3BuildEx@44", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched3BuildEx(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiodin, int[] maxperiodin, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched3Calc@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_Sched3Calc(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched3Check@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched3Check(ref int minperiod, ref int maxperiod, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched3ToBit@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_Sched3ToBit(uint value, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched4AdvanceMessage@48", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4AdvanceMessage(int entries, int panicindex, int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched4AdvanceNoMessage@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4AdvanceNoMessage(int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched4Analyze@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_Sched4Analyze(int nummsgs, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched4Build@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4Build(int nummsgs, uint[] msgaddrptr, int[] minlist, int[] maxlist, int speedflag, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched4BuildEx@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4BuildEx(ref int styleptr, int nummsgs, int minorframetime, int majorframetime, int[] targetlist, int[] minlist, int[] maxlist, int speedflag, int microflag, int interceptflag);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Sched4BuildExx@72", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4BuildExx(int style, int speedflag, int microflag, int debugvalflag, int tryflag, int nummsgs, int minorframetime, int majorframetime, int[] targetlist, int[] minlist, int[] maxlist, ref int entriesptr, ref int maxentriesptr, nint infoptr, uint[] msgaddrptr, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedAdvance@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedAdvance(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedAgain@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedAgain(int index, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBranchEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBranchEx(ushort condition1, ushort condition2, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBuildBuf@32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildBuf(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int bufcount, ref byte bufptr, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBuildBufEx@48", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildBufEx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int debugval, int microflag, int algorithm, int bufcount, ref byte bufptr, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBuildEx@28", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildEx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBuildExx@40", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildExx(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, nint buf, uint bufsize, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBuildExxx@44", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildExxx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, int debugval, int microflag, int algorithm, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedBuildExxxx@44", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildExxxx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, int debugval, int microflag, int algorithm, int interceptflag, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCalc@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedCalc(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCallEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCallEx(ushort condition1, ushort condition2, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCheck@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCheck(int[] minperiod, int[] maxperiod, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCheckEnd@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckEnd();
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCheckMiddle@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckMiddle(uint msgnum, uint minlen, uint midlen, uint maxlen);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCheckReal@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckReal(uint msgnum, uint minlen, uint midlen, uint maxlen);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedCheckStart@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckStart();
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedConflicts@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedConflicts(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedDisplay@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern void BTI429_SchedDisplay(uint nummsgs, ref uint count, ref uint* data);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedDivide@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedDivide(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedFix@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedFix(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedFixEnd@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedFixEnd();
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedFixMiddle@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedFixMiddle(uint conflicts);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedFixStart@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedFixStart();
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedHook@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedHook(int index, nint ptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedInsert@32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsert(uint eventvals, uint index, uint msgnum, ref uint msgaddr, uint gapval, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedInsert2@32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsert2(uint eventvals, uint index, int msgnum, ref uint msgaddr, uint gapval, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedInsertEnd@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsertEnd(uint eventvals, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedInsertStart@16", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsertStart(uint eventvals, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedIntercept@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedIntercept(int typeval, uint msgaddr, uint gapval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedLayout@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedLayout(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedLogEx@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedLogEx(ushort condition1, ushort condition2, ushort tagval, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedMaxval@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedMaxval(uint[] arrayval, uint arrayvalcount);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedMinval@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedMinval(uint[] arrayval, uint arrayvalcount);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedNop@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedNop(int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedParse@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedParse(int basevalmsgnum, ref uint msgaddr, int channum, nint infoptr, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedPreprocess@36", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPreprocess(int nummsgs, int speedflag, uint[] msgaddrptr, int[] minlist, int[] maxlist, int debugvalflag, int[] targetlist, ref int minorframetime, ref int majorframetime);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedPulse0@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPulse0(int dionum, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedPulse1@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPulse1(int dionum, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedReset@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedReset(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedRotate@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedRotate(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedShift@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedShift(int msgnum, int value, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedSlide@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedSlide(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedSlideEnd@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSlideEnd();
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedSlideMiddle@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSlideMiddle(uint index, uint count, uint conflicts);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedSlideStart@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSlideStart(uint factor);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedSort@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSort(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedToBit@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedToBit(uint value, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedToMsec@8", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedToMsec(uint value, nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedUnsort@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedUnsort(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedUser@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedUser(uint useraddr, int channum, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedWalk@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedWalk(nint infoptr);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_SchedWriteBuf@20", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedWriteBuf(ref byte bufptr, int bufcount, uint schaddr, int schedcount, nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_TestProtocol@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_TestProtocol(nint handleval);
            [DllImport(LIBNAME32, EntryPoint = "_BTI429_Transpose12@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_Transpose12(ushort val);
        }

        private static class BTI429_64
        {
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDGetData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDGetData(uint msg, ushort msb, ushort lsb);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDGetMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDGetMant(uint msg, ushort sigdig);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDGetSign", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BCDGetSign(uint msg);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDGetSSM", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BCDGetSSM(uint msg);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDGetValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern double BTI429_BCDGetValue(uint msg, ushort sigdig, double resolstr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDPutData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutData(uint msg, uint value, ushort msb, ushort lsb);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDPutMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutMant(uint msg, uint value, ushort sigdig, ushort sign);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDPutSign", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutSign(uint msg, ushort sign);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDPutSSM", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutSSM(uint msg, ushort value);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDPutValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutValue(double dblvalue, uint msg, ushort sigdig, double dblresolution);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRAngularGetValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern double BTI429_BNRAngularGetValue(uint msg, ushort sigbit, int bplusminus180);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRAngularPutValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRAngularPutValue(double dblvalue, uint msg, ushort sigbit);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRGetData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRGetData(uint msg, ushort msb, ushort lsb);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRGetMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRGetMant(uint msg, ushort sigbit);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRGetSign", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BNRGetSign(uint msg);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRGetSSM", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_BNRGetSSM(uint msg);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRGetValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern double BTI429_BNRGetValue(uint msg, ushort sigbit, double dblresolution);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRPutData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutData(uint msg, uint value, ushort msb, ushort lsb);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRPutMant", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutMant(uint msg, uint value, ushort sigbit, ushort twos);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRPutSign", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutSign(uint msg, ushort twos);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRPutSSM", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutSSM(uint msg, ushort value);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRPutValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutValue(double dblvalue, uint msg, ushort sigbit, double dblresolution);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChClear(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChConfig(uint configval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChGetCount", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ChGetCount(ref int rcvcount, ref int xmtcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChGetInfo", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ChGetInfo(ushort infotype, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChIs429", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChIs429(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChIsRcv", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChIsRcv(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChIsXmt", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChIsXmt(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChPause", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ChPause(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChPauseCheck", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChPauseCheck(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChResume", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ChResume(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChStart(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChStop", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChStop(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChSyncDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChSyncDefine(int enableflag, ushort syncmask, ushort pinpolarity, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChTriggerDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChTriggerDefine(int enableflag, ushort trigmask, ushort trigval, ushort pinpolarity, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_DriverInfoStr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern nint _BTI429_DriverInfoStr();
            public static string BTI429_DriverInfoStr() { return Marshal.PtrToStringAnsi(_BTI429_DriverInfoStr()); }
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_FilterClear(uint basevaladdr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterDefault", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterDefault(uint configval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterRd(int labelval, int sdival, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterSet(uint configval, int labelval, int sdimask, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_FilterWr(uint msgaddr, int labelval, int sdival, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldGetData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldGetData(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldGetLabel", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetLabel(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldGetParity", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetParity(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldGetSDI", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetSDI(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldGetValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldGetValue(uint msgval, ushort startbit, ushort endbit);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldPutData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutData(uint msgval, uint data);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldPutLabel", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutLabel(uint msgval, ushort label);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldPutSDI", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutSDI(uint msgval, ushort sdi);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldPutValue", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutValue(uint msgval, uint dataval, ushort startbit, ushort endbit);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListAsyncCreate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListAsyncCreate(uint listconfigval, int count, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListDataRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListDataRd(uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListDataWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataWr(uint value, uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListRcvCreate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListRcvCreate(uint listconfigval, int count, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListStatus(uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListXmtCreate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListXmtCreate(uint listconfigval, int count, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgBlockRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgBlockRd(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgBlockWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgBlockWr(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgCountRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgCountRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgCountWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgCountWr(int bitcount, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgCreate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgCreate(uint configval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgDataCountWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgDataCountWr(uint dataval, int bitcount, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgDataRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgDataRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgDataWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgDataWr(uint value, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgGroupBlockRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupBlockRd([In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MSGFIELDS429[] msgfields, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgGroupBlockWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupBlockWr([In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MSGFIELDS429[] msgfields, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgGroupRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupRd(uint[] msgdataptr, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgGroupWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgGroupWr(uint[] msgdataptr, uint[] msgaddrptr, int nummsgs, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgIsAccessed", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgIsAccessed(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgSkipRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgSkipRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgSkipWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgSkipWr(int skip, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgSyncDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgSyncDefine(int enableflag, ushort syncmask, ushort pinpolarity, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgTriggerDefine", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgTriggerDefine(int enableflag, ushort trigmask, ushort trigval, ushort pinpolarity, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ParamAmplitudeConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamAmplitudeConfig(uint configval, ushort dacval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ParamAmplitudeGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamAmplitudeGet(ref ushort dacval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ParamBitCountBuild", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_ParamBitCountBuild(int bitcount);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ParamBitRateConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamBitRateConfig(uint configval, ushort divval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ParamBitRateGet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamBitRateGet(ref ushort divval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayBlockWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayBlockWr(ushort[] playbuf, int count, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayPutData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutData(uint dataval, int bitcount, ushort gapval, int offset, ushort[] playbuf);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayPutGap", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutGap(ushort gapval, int offset, ushort[] playbuf);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayPutLog", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutLog(uint condition, ushort tagval, int offset, ushort[] playbuf);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayPutPulse0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutPulse0(int dionum, int offset, ushort[] playbuf);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayPutPulse1", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutPulse1(int dionum, int offset, ushort[] playbuf);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayStatus", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayStatus(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBranch", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBranch(uint condition, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBranchUpdate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBranchUpdate(int destindex, int opcodeindex, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBuild", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuild(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCall", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCall(uint condition, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCallUpdate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCallUpdate(int destindex, int opcodeindex, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedData", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedData(uint dataval, int bitcount, ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedEntry", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedEntry(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedGap", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedGap(ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedGapFixed", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedGapFixed(ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedGapList", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedGapList(ushort gapval, uint listaddr, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedHalt", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedHalt(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedLog", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedLog(uint condition, ushort tagval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedMode", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedMode(uint modeval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedMsg", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedMsg(uint msgaddr, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedMsgEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedMsgEx(uint msgaddr, ushort gapval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedPause", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPause(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedPulse", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPulse(int dionum, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedRestart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedRestart(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedReturn", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedReturn(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDGetVal", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_BCDGetVal(System.Text.StringBuilder buf, uint msg, ushort sigdig, string resolstr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BCDPutVal", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BCDPutVal(string buf, uint msg, ushort sigdig, string resolstr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRGetVal", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_BNRGetVal(System.Text.StringBuilder buf, uint msg, ushort sigbit, string resolstr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_BNRPutVal", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_BNRPutVal(string buf, uint msg, ushort sigbit, string resolstr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChConfigEx(uint configval, ushort count, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ChOutputStateSet", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ChOutputStateSet(uint pos, uint neg, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdAddr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdAddr(int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdAlloc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdAlloc(int count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdAllocEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdAllocEx(uint configval, int count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdBaseRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_CmdBaseRd(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdBaseWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdBaseWr(uint addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdClear(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdCountRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdCountRd(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdCountWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdCountWr(ushort countval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdCtrlRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdCtrlRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdCtrlWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdCtrlWr(ushort ctrlval, int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdCurrRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdCurrRd(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdCurrWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdCurrWr(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdInit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdInit(ushort opcode, ref ushort block);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdInsert", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdInsert(ref ushort block, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdMaxLoopRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdMaxLoopRd(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdMaxLoopWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdMaxLoopWr(ushort countval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdRd(ref ushort block, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdRestart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdRestart(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdShotRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdShotRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdShotWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdShotWr(int value, int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdSkipRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdSkipRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdSkipWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdSkipWr(int value, int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdStackClr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdStackClr(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdStartRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_CmdStartRd(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdStartWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdStartWr(ushort addrval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdStepRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdStepRd(int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdStepWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdStepWr(int value, int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdTotalRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdTotalRd(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdTotalWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_CmdTotalWr(int countval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_CmdWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_CmdWr(ref ushort block, uint addrval, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ConvFromArinc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ConvFromArinc(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ConvToArinc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ConvToArinc(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_DllUsageCount", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_DllUsageCount();
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterAddr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterAddr(int labelval, int sdival, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterCreate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FilterCreate(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FilterFill", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_FilterFill(uint msgaddr, uint basevaladdr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldGetParBit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_FldGetParBit(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_FldPutParBit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_FldPutParBit(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_GetChanCount", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_GetChanCount(ref int rcvcount, ref int xmtcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_IsChan", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_IsChan(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_IsRcvChan", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_IsRcvChan(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_IsXmtChan", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_IsXmtChan(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_LabelReverse", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_LabelReverse(uint msgval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListAddr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListAddr(int index, uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListAddrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListAddrEx(int index, uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListAdvance", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListAdvance(uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListClear", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListClear(uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListConfig(uint configval, int count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListConfigEx(uint configval, int count, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListDataBlkRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataBlkRd(uint[] dataptr, ref ushort datacountptr, uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListDataBlkWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataBlkWr(uint[] dataptr, ushort datacount, uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListDataBlkWrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListDataBlkWrEx(uint[] dataptr, ref ushort datacountptr, uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListDataRdEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListDataRdEx(int index, uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListDataWrEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListDataWrEx(uint value, int index, uint listaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListInit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ListInit(uint listaddr, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListNextRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListNextRd(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListNextWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_ListNextWr(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListPostRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPostRd(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListPostWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPostWr(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListPreRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPreRd(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ListPreWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_ListPreWr(uint listaddr, ref ushort header, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgActivityRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_MsgActivityRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgActivityWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgActivityWr(ushort value, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgCheck", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_MsgCheck(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgCommRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgCommRd(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgCommWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgCommWr(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgConfig(uint configval, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgMultiSkipWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_MsgMultiSkipWr(int[] skip, uint[] msgaddr, int msgcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgOptRd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_MsgOptRd(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgOptWr", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_MsgOptWr(ushort msgopt, uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_MsgReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_MsgReset(uint msgaddr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_PlayPutUser", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_PlayPutUser(uint useraddr, int offset, ushort[] playbuf);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ParamWaveformConfig", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamWaveformConfig(uint configval, int highvolt, int lowvolt, int nullvolt, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_ParamWaveformConfigEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_ParamWaveformConfigEx(uint configval, ref PARAMFIELDS429 paramflds, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched1Build", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched1Build(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiodin, int[] maxperiodin, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched2AdvanceMessage", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2AdvanceMessage(int entries, int panicindex, int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched2AdvanceNoMessage", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2AdvanceNoMessage(int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched2Analyze", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_Sched2Analyze(int nummsgs, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched2Build", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2Build(int nummsgs, uint[] msgaddrptr, int[] minlist, int[] maxlist, int speedflag, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched2BuildEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2BuildEx(ref int styleptr, int nummsgs, int[] minlist, int[] maxlist, int speedflag, int microflag, int interceptflag);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched2BuildExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched2BuildExx(int style, int speedflag, int microflag, int debugvalflag, int tryflag, int nummsgs, int[] minlist, int[] maxlist, ref int entriesptr, ref int maxentriesptr, nint infoptr, uint[] msgaddrptr, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched3Build", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched3Build(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched3BuildEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched3BuildEx(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiodin, int[] maxperiodin, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched3Calc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_Sched3Calc(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched3Check", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched3Check(ref int minperiod, ref int maxperiod, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched3ToBit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_Sched3ToBit(uint value, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched4AdvanceMessage", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4AdvanceMessage(int entries, int panicindex, int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched4AdvanceNoMessage", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4AdvanceNoMessage(int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched4Analyze", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_Sched4Analyze(int nummsgs, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched4Build", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4Build(int nummsgs, uint[] msgaddrptr, int[] minlist, int[] maxlist, int speedflag, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched4BuildEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4BuildEx(ref int styleptr, int nummsgs, int minorframetime, int majorframetime, int[] targetlist, int[] minlist, int[] maxlist, int speedflag, int microflag, int interceptflag);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Sched4BuildExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_Sched4BuildExx(int style, int speedflag, int microflag, int debugvalflag, int tryflag, int nummsgs, int minorframetime, int majorframetime, int[] targetlist, int[] minlist, int[] maxlist, ref int entriesptr, ref int maxentriesptr, nint infoptr, uint[] msgaddrptr, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedAdvance", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedAdvance(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedAgain", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedAgain(int index, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBranchEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBranchEx(ushort condition1, ushort condition2, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBuildBuf", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildBuf(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int bufcount, ref byte bufptr, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBuildBufEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildBufEx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int debugval, int microflag, int algorithm, int bufcount, ref byte bufptr, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBuildEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildEx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBuildExx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildExx(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, nint buf, uint bufsize, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBuildExxx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildExxx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, int debugval, int microflag, int algorithm, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedBuildExxxx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedBuildExxxx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, int debugval, int microflag, int algorithm, int interceptflag, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCalc", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedCalc(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCallEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCallEx(ushort condition1, ushort condition2, int destindex, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCheck", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedCheck(int[] minperiod, int[] maxperiod, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCheckEnd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckEnd();
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCheckMiddle", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckMiddle(uint msgnum, uint minlen, uint midlen, uint maxlen);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCheckReal", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckReal(uint msgnum, uint minlen, uint midlen, uint maxlen);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedCheckStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedCheckStart();
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedConflicts", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedConflicts(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedDisplay", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            unsafe public static extern void BTI429_SchedDisplay(uint nummsgs, ref uint count, ref uint* data);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedDivide", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedDivide(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedFix", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedFix(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedFixEnd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedFixEnd();
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedFixMiddle", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedFixMiddle(uint conflicts);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedFixStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedFixStart();
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedHook", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedHook(int index, nint ptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedInsert", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsert(uint eventvals, uint index, uint msgnum, ref uint msgaddr, uint gapval, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedInsert2", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsert2(uint eventvals, uint index, int msgnum, ref uint msgaddr, uint gapval, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedInsertEnd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsertEnd(uint eventvals, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedInsertStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedInsertStart(uint eventvals, int interceptflag, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedIntercept", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedIntercept(int typeval, uint msgaddr, uint gapval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedLayout", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedLayout(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedLogEx", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedLogEx(ushort condition1, ushort condition2, ushort tagval, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedMaxval", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedMaxval(uint[] arrayval, uint arrayvalcount);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedMinval", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedMinval(uint[] arrayval, uint arrayvalcount);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedNop", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedNop(int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedParse", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedParse(int basevalmsgnum, ref uint msgaddr, int channum, nint infoptr, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedPreprocess", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPreprocess(int nummsgs, int speedflag, uint[] msgaddrptr, int[] minlist, int[] maxlist, int debugvalflag, int[] targetlist, ref int minorframetime, ref int majorframetime);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedPulse0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPulse0(int dionum, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedPulse1", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedPulse1(int dionum, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedReset", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedReset(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedRotate", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedRotate(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedShift", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedShift(int msgnum, int value, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedSlide", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedSlide(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedSlideEnd", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSlideEnd();
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedSlideMiddle", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSlideMiddle(uint index, uint count, uint conflicts);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedSlideStart", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSlideStart(uint factor);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedSort", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedSort(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedToBit", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedToBit(uint value, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedToMsec", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedToMsec(uint value, nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedUnsort", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern void BTI429_SchedUnsort(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedUser", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedUser(uint useraddr, int channum, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedWalk", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern uint BTI429_SchedWalk(nint infoptr);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_SchedWriteBuf", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_SchedWriteBuf(ref byte bufptr, int bufcount, uint schaddr, int schedcount, nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_TestProtocol", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern int BTI429_TestProtocol(nint handleval);
            [DllImport(LIBNAME64, EntryPoint = "BTI429_Transpose12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
            public static extern ushort BTI429_Transpose12(ushort val);
        }

        public static uint BTI429_BCDGetData(uint msg, ushort msb, ushort lsb)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDGetData(msg, msb, lsb);
            else
                return BTI429_64.BTI429_BCDGetData(msg, msb, lsb);
        }
        public static uint BTI429_BCDGetMant(uint msg, ushort sigdig)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDGetMant(msg, sigdig);
            else
                return BTI429_64.BTI429_BCDGetMant(msg, sigdig);
        }
        public static ushort BTI429_BCDGetSign(uint msg)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDGetSign(msg);
            else
                return BTI429_64.BTI429_BCDGetSign(msg);
        }
        public static ushort BTI429_BCDGetSSM(uint msg)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDGetSSM(msg);
            else
                return BTI429_64.BTI429_BCDGetSSM(msg);
        }
        public static double BTI429_BCDGetValue(uint msg, ushort sigdig, double resolstr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDGetValue(msg, sigdig, resolstr);
            else
                return BTI429_64.BTI429_BCDGetValue(msg, sigdig, resolstr);
        }
        public static uint BTI429_BCDPutData(uint msg, uint value, ushort msb, ushort lsb)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDPutData(msg, value, msb, lsb);
            else
                return BTI429_64.BTI429_BCDPutData(msg, value, msb, lsb);
        }
        public static uint BTI429_BCDPutMant(uint msg, uint value, ushort sigdig, ushort sign)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDPutMant(msg, value, sigdig, sign);
            else
                return BTI429_64.BTI429_BCDPutMant(msg, value, sigdig, sign);
        }
        public static uint BTI429_BCDPutSign(uint msg, ushort sign)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDPutSign(msg, sign);
            else
                return BTI429_64.BTI429_BCDPutSign(msg, sign);
        }
        public static uint BTI429_BCDPutSSM(uint msg, ushort value)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDPutSSM(msg, value);
            else
                return BTI429_64.BTI429_BCDPutSSM(msg, value);
        }
        public static uint BTI429_BCDPutValue(double dblvalue, uint msg, ushort sigdig, double dblresolution)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDPutValue(dblvalue, msg, sigdig, dblresolution);
            else
                return BTI429_64.BTI429_BCDPutValue(dblvalue, msg, sigdig, dblresolution);
        }
        public static double BTI429_BNRAngularGetValue(uint msg, ushort sigbit, int bplusminus180)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRAngularGetValue(msg, sigbit, bplusminus180);
            else
                return BTI429_64.BTI429_BNRAngularGetValue(msg, sigbit, bplusminus180);
        }
        public static uint BTI429_BNRAngularPutValue(double dblvalue, uint msg, ushort sigbit)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRAngularPutValue(dblvalue, msg, sigbit);
            else
                return BTI429_64.BTI429_BNRAngularPutValue(dblvalue, msg, sigbit);
        }
        public static uint BTI429_BNRGetData(uint msg, ushort msb, ushort lsb)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRGetData(msg, msb, lsb);
            else
                return BTI429_64.BTI429_BNRGetData(msg, msb, lsb);
        }
        public static uint BTI429_BNRGetMant(uint msg, ushort sigbit)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRGetMant(msg, sigbit);
            else
                return BTI429_64.BTI429_BNRGetMant(msg, sigbit);
        }
        public static ushort BTI429_BNRGetSign(uint msg)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRGetSign(msg);
            else
                return BTI429_64.BTI429_BNRGetSign(msg);
        }
        public static ushort BTI429_BNRGetSSM(uint msg)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRGetSSM(msg);
            else
                return BTI429_64.BTI429_BNRGetSSM(msg);
        }
        public static double BTI429_BNRGetValue(uint msg, ushort sigbit, double dblresolution)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRGetValue(msg, sigbit, dblresolution);
            else
                return BTI429_64.BTI429_BNRGetValue(msg, sigbit, dblresolution);
        }
        public static uint BTI429_BNRPutData(uint msg, uint value, ushort msb, ushort lsb)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRPutData(msg, value, msb, lsb);
            else
                return BTI429_64.BTI429_BNRPutData(msg, value, msb, lsb);
        }
        public static uint BTI429_BNRPutMant(uint msg, uint value, ushort sigbit, ushort twos)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRPutMant(msg, value, sigbit, twos);
            else
                return BTI429_64.BTI429_BNRPutMant(msg, value, sigbit, twos);
        }
        public static uint BTI429_BNRPutSign(uint msg, ushort twos)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRPutSign(msg, twos);
            else
                return BTI429_64.BTI429_BNRPutSign(msg, twos);
        }
        public static uint BTI429_BNRPutSSM(uint msg, ushort value)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRPutSSM(msg, value);
            else
                return BTI429_64.BTI429_BNRPutSSM(msg, value);
        }
        public static uint BTI429_BNRPutValue(double dblvalue, uint msg, ushort sigbit, double dblresolution)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRPutValue(dblvalue, msg, sigbit, dblresolution);
            else
                return BTI429_64.BTI429_BNRPutValue(dblvalue, msg, sigbit, dblresolution);
        }
        public static int BTI429_ChClear(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChClear(channum, handleval);
            else
                return BTI429_64.BTI429_ChClear(channum, handleval);
        }
        public static int BTI429_ChConfig(uint configval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChConfig(configval, channum, handleval);
            else
                return BTI429_64.BTI429_ChConfig(configval, channum, handleval);
        }
        public static void BTI429_ChGetCount(ref int rcvcount, ref int xmtcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ChGetCount(ref rcvcount, ref xmtcount, handleval);
            else
                BTI429_64.BTI429_ChGetCount(ref rcvcount, ref xmtcount, handleval);
        }
        public static uint BTI429_ChGetInfo(ushort infotype, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChGetInfo(infotype, channum, handleval);
            else
                return BTI429_64.BTI429_ChGetInfo(infotype, channum, handleval);
        }
        public static int BTI429_ChIs429(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChIs429(channum, handleval);
            else
                return BTI429_64.BTI429_ChIs429(channum, handleval);
        }
        public static int BTI429_ChIsRcv(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChIsRcv(channum, handleval);
            else
                return BTI429_64.BTI429_ChIsRcv(channum, handleval);
        }
        public static int BTI429_ChIsXmt(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChIsXmt(channum, handleval);
            else
                return BTI429_64.BTI429_ChIsXmt(channum, handleval);
        }
        public static void BTI429_ChPause(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ChPause(channum, handleval);
            else
                BTI429_64.BTI429_ChPause(channum, handleval);
        }
        public static int BTI429_ChPauseCheck(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChPauseCheck(channum, handleval);
            else
                return BTI429_64.BTI429_ChPauseCheck(channum, handleval);
        }
        public static void BTI429_ChResume(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ChResume(channum, handleval);
            else
                BTI429_64.BTI429_ChResume(channum, handleval);
        }
        public static int BTI429_ChStart(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChStart(channum, handleval);
            else
                return BTI429_64.BTI429_ChStart(channum, handleval);
        }
        public static int BTI429_ChStop(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChStop(channum, handleval);
            else
                return BTI429_64.BTI429_ChStop(channum, handleval);
        }
        public static int BTI429_ChSyncDefine(int enableflag, ushort syncmask, ushort pinpolarity, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChSyncDefine(enableflag, syncmask, pinpolarity, channum, handleval);
            else
                return BTI429_64.BTI429_ChSyncDefine(enableflag, syncmask, pinpolarity, channum, handleval);
        }
        public static int BTI429_ChTriggerDefine(int enableflag, ushort trigmask, ushort trigval, ushort pinpolarity, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval);
            else
                return BTI429_64.BTI429_ChTriggerDefine(enableflag, trigmask, trigval, pinpolarity, channum, handleval);
        }
        public static unsafe string BTI429_DriverInfoStr()
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_DriverInfoStr();
            else
                return BTI429_64.BTI429_DriverInfoStr();
        }
        public static int BTI429_FilterClear(uint basevaladdr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterClear(basevaladdr, handleval);
            else
                return BTI429_64.BTI429_FilterClear(basevaladdr, handleval);
        }
        public static uint BTI429_FilterDefault(uint configval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterDefault(configval, channum, handleval);
            else
                return BTI429_64.BTI429_FilterDefault(configval, channum, handleval);
        }
        public static uint BTI429_FilterRd(int labelval, int sdival, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterRd(labelval, sdival, channum, handleval);
            else
                return BTI429_64.BTI429_FilterRd(labelval, sdival, channum, handleval);
        }
        public static uint BTI429_FilterSet(uint configval, int labelval, int sdimask, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterSet(configval, labelval, sdimask, channum, handleval);
            else
                return BTI429_64.BTI429_FilterSet(configval, labelval, sdimask, channum, handleval);
        }
        public static int BTI429_FilterWr(uint msgaddr, int labelval, int sdival, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterWr(msgaddr, labelval, sdival, channum, handleval);
            else
                return BTI429_64.BTI429_FilterWr(msgaddr, labelval, sdival, channum, handleval);
        }
        public static uint BTI429_FldGetData(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldGetData(msgval);
            else
                return BTI429_64.BTI429_FldGetData(msgval);
        }
        public static ushort BTI429_FldGetLabel(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldGetLabel(msgval);
            else
                return BTI429_64.BTI429_FldGetLabel(msgval);
        }
        public static ushort BTI429_FldGetParity(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldGetParity(msgval);
            else
                return BTI429_64.BTI429_FldGetParity(msgval);
        }
        public static ushort BTI429_FldGetSDI(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldGetSDI(msgval);
            else
                return BTI429_64.BTI429_FldGetSDI(msgval);
        }
        public static uint BTI429_FldGetValue(uint msgval, ushort startbit, ushort endbit)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldGetValue(msgval, startbit, endbit);
            else
                return BTI429_64.BTI429_FldGetValue(msgval, startbit, endbit);
        }
        public static uint BTI429_FldPutData(uint msgval, uint data)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldPutData(msgval, data);
            else
                return BTI429_64.BTI429_FldPutData(msgval, data);
        }
        public static uint BTI429_FldPutLabel(uint msgval, ushort label)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldPutLabel(msgval, label);
            else
                return BTI429_64.BTI429_FldPutLabel(msgval, label);
        }
        public static uint BTI429_FldPutSDI(uint msgval, ushort sdi)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldPutSDI(msgval, sdi);
            else
                return BTI429_64.BTI429_FldPutSDI(msgval, sdi);
        }
        public static uint BTI429_FldPutValue(uint msgval, uint dataval, ushort startbit, ushort endbit)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldPutValue(msgval, dataval, startbit, endbit);
            else
                return BTI429_64.BTI429_FldPutValue(msgval, dataval, startbit, endbit);
        }
        public static uint BTI429_ListAsyncCreate(uint listconfigval, int count, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListAsyncCreate(listconfigval, count, channum, handleval);
            else
                return BTI429_64.BTI429_ListAsyncCreate(listconfigval, count, channum, handleval);
        }
        public static uint BTI429_ListDataRd(uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListDataRd(listaddr, handleval);
            else
                return BTI429_64.BTI429_ListDataRd(listaddr, handleval);
        }
        public static int BTI429_ListDataWr(uint value, uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListDataWr(value, listaddr, handleval);
            else
                return BTI429_64.BTI429_ListDataWr(value, listaddr, handleval);
        }
        public static uint BTI429_ListRcvCreate(uint listconfigval, int count, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListRcvCreate(listconfigval, count, msgaddr, handleval);
            else
                return BTI429_64.BTI429_ListRcvCreate(listconfigval, count, msgaddr, handleval);
        }
        public static int BTI429_ListStatus(uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListStatus(listaddr, handleval);
            else
                return BTI429_64.BTI429_ListStatus(listaddr, handleval);
        }
        public static uint BTI429_ListXmtCreate(uint listconfigval, int count, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListXmtCreate(listconfigval, count, msgaddr, handleval);
            else
                return BTI429_64.BTI429_ListXmtCreate(listconfigval, count, msgaddr, handleval);
        }
        public static uint BTI429_MsgBlockRd(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgBlockRd(ref msgfields, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgBlockRd(ref msgfields, msgaddr, handleval);
        }
        public static uint BTI429_MsgBlockWr(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgBlockWr(ref msgfields, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgBlockWr(ref msgfields, msgaddr, handleval);
        }
        public static int BTI429_MsgCountRd(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgCountRd(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgCountRd(msgaddr, handleval);
        }
        public static int BTI429_MsgCountWr(int bitcount, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgCountWr(bitcount, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgCountWr(bitcount, msgaddr, handleval);
        }
        public static uint BTI429_MsgCreate(uint configval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgCreate(configval, handleval);
            else
                return BTI429_64.BTI429_MsgCreate(configval, handleval);
        }
        public static int BTI429_MsgDataCountWr(uint dataval, int bitcount, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgDataCountWr(dataval, bitcount, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgDataCountWr(dataval, bitcount, msgaddr, handleval);
        }
        public static uint BTI429_MsgDataRd(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgDataRd(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgDataRd(msgaddr, handleval);
        }
        public static void BTI429_MsgDataWr(uint value, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgDataWr(value, msgaddr, handleval);
            else
                BTI429_64.BTI429_MsgDataWr(value, msgaddr, handleval);
        }
        public static void BTI429_MsgGroupBlockRd([In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MSGFIELDS429[] msgfields, uint[] msgaddrptr, int nummsgs, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgGroupBlockRd(msgfields, msgaddrptr, nummsgs, handleval);
            else
                BTI429_64.BTI429_MsgGroupBlockRd(msgfields, msgaddrptr, nummsgs, handleval);
        }
        public static void BTI429_MsgGroupBlockWr([In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] MSGFIELDS429[] msgfields, uint[] msgaddrptr, int nummsgs, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgGroupBlockWr(msgfields, msgaddrptr, nummsgs, handleval);
            else
                BTI429_64.BTI429_MsgGroupBlockWr(msgfields, msgaddrptr, nummsgs, handleval);
        }
        public static void BTI429_MsgGroupRd(uint[] msgdataptr, uint[] msgaddrptr, int nummsgs, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgGroupRd(msgdataptr, msgaddrptr, nummsgs, handleval);
            else
                BTI429_64.BTI429_MsgGroupRd(msgdataptr, msgaddrptr, nummsgs, handleval);
        }
        public static void BTI429_MsgGroupWr(uint[] msgdataptr, uint[] msgaddrptr, int nummsgs, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgGroupWr(msgdataptr, msgaddrptr, nummsgs, handleval);
            else
                BTI429_64.BTI429_MsgGroupWr(msgdataptr, msgaddrptr, nummsgs, handleval);
        }
        public static int BTI429_MsgIsAccessed(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgIsAccessed(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgIsAccessed(msgaddr, handleval);
        }
        public static int BTI429_MsgSkipRd(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgSkipRd(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgSkipRd(msgaddr, handleval);
        }
        public static void BTI429_MsgSkipWr(int skip, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgSkipWr(skip, msgaddr, handleval);
            else
                BTI429_64.BTI429_MsgSkipWr(skip, msgaddr, handleval);
        }
        public static int BTI429_MsgSyncDefine(int enableflag, ushort syncmask, ushort pinpolarity, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgSyncDefine(enableflag, syncmask, pinpolarity, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgSyncDefine(enableflag, syncmask, pinpolarity, msgaddr, handleval);
        }
        public static int BTI429_MsgTriggerDefine(int enableflag, ushort trigmask, ushort trigval, ushort pinpolarity, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgTriggerDefine(enableflag, trigmask, trigval, pinpolarity, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgTriggerDefine(enableflag, trigmask, trigval, pinpolarity, msgaddr, handleval);
        }
        public static int BTI429_ParamAmplitudeConfig(uint configval, ushort dacval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ParamAmplitudeConfig(configval, dacval, channum, handleval);
            else
                return BTI429_64.BTI429_ParamAmplitudeConfig(configval, dacval, channum, handleval);
        }
        public static int BTI429_ParamAmplitudeGet(ref ushort dacval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ParamAmplitudeGet(ref dacval, channum, handleval);
            else
                return BTI429_64.BTI429_ParamAmplitudeGet(ref dacval, channum, handleval);
        }
        public static ushort BTI429_ParamBitCountBuild(int bitcount)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ParamBitCountBuild(bitcount);
            else
                return BTI429_64.BTI429_ParamBitCountBuild(bitcount);
        }
        public static int BTI429_ParamBitRateConfig(uint configval, ushort divval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ParamBitRateConfig(configval, divval, channum, handleval);
            else
                return BTI429_64.BTI429_ParamBitRateConfig(configval, divval, channum, handleval);
        }
        public static int BTI429_ParamBitRateGet(ref ushort divval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ParamBitRateGet(ref divval, channum, handleval);
            else
                return BTI429_64.BTI429_ParamBitRateGet(ref divval, channum, handleval);
        }
        public static int BTI429_PlayBlockWr(ushort[] playbuf, int count, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayBlockWr(playbuf, count, channum, handleval);
            else
                return BTI429_64.BTI429_PlayBlockWr(playbuf, count, channum, handleval);
        }
        public static int BTI429_PlayPutData(uint dataval, int bitcount, ushort gapval, int offset, ushort[] playbuf)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayPutData(dataval, bitcount, gapval, offset, playbuf);
            else
                return BTI429_64.BTI429_PlayPutData(dataval, bitcount, gapval, offset, playbuf);
        }
        public static int BTI429_PlayPutGap(ushort gapval, int offset, ushort[] playbuf)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayPutGap(gapval, offset, playbuf);
            else
                return BTI429_64.BTI429_PlayPutGap(gapval, offset, playbuf);
        }
        public static int BTI429_PlayPutLog(uint condition, ushort tagval, int offset, ushort[] playbuf)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayPutLog(condition, tagval, offset, playbuf);
            else
                return BTI429_64.BTI429_PlayPutLog(condition, tagval, offset, playbuf);
        }
        public static int BTI429_PlayPutPulse0(int dionum, int offset, ushort[] playbuf)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayPutPulse0(dionum, offset, playbuf);
            else
                return BTI429_64.BTI429_PlayPutPulse0(dionum, offset, playbuf);
        }
        public static int BTI429_PlayPutPulse1(int dionum, int offset, ushort[] playbuf)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayPutPulse1(dionum, offset, playbuf);
            else
                return BTI429_64.BTI429_PlayPutPulse1(dionum, offset, playbuf);
        }
        public static int BTI429_PlayStatus(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayStatus(channum, handleval);
            else
                return BTI429_64.BTI429_PlayStatus(channum, handleval);
        }
        public static int BTI429_SchedBranch(uint condition, int destindex, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBranch(condition, destindex, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBranch(condition, destindex, channum, handleval);
        }
        public static int BTI429_SchedBranchUpdate(int destindex, int opcodeindex, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBranchUpdate(destindex, opcodeindex, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBranchUpdate(destindex, opcodeindex, channum, handleval);
        }
        public static int BTI429_SchedBuild(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBuild(nummsgs, msgaddr, minperiod, maxperiod, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBuild(nummsgs, msgaddr, minperiod, maxperiod, channum, handleval);
        }
        public static int BTI429_SchedCall(uint condition, int destindex, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedCall(condition, destindex, channum, handleval);
            else
                return BTI429_64.BTI429_SchedCall(condition, destindex, channum, handleval);
        }
        public static int BTI429_SchedCallUpdate(int destindex, int opcodeindex, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedCallUpdate(destindex, opcodeindex, channum, handleval);
            else
                return BTI429_64.BTI429_SchedCallUpdate(destindex, opcodeindex, channum, handleval);
        }
        public static int BTI429_SchedData(uint dataval, int bitcount, ushort gapval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedData(dataval, bitcount, gapval, channum, handleval);
            else
                return BTI429_64.BTI429_SchedData(dataval, bitcount, gapval, channum, handleval);
        }
        public static int BTI429_SchedEntry(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedEntry(channum, handleval);
            else
                return BTI429_64.BTI429_SchedEntry(channum, handleval);
        }
        public static int BTI429_SchedGap(ushort gapval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedGap(gapval, channum, handleval);
            else
                return BTI429_64.BTI429_SchedGap(gapval, channum, handleval);
        }
        public static int BTI429_SchedGapFixed(ushort gapval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedGapFixed(gapval, channum, handleval);
            else
                return BTI429_64.BTI429_SchedGapFixed(gapval, channum, handleval);
        }
        public static int BTI429_SchedGapList(ushort gapval, uint listaddr, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedGapList(gapval, listaddr, channum, handleval);
            else
                return BTI429_64.BTI429_SchedGapList(gapval, listaddr, channum, handleval);
        }
        public static int BTI429_SchedHalt(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedHalt(channum, handleval);
            else
                return BTI429_64.BTI429_SchedHalt(channum, handleval);
        }
        public static int BTI429_SchedLog(uint condition, ushort tagval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedLog(condition, tagval, channum, handleval);
            else
                return BTI429_64.BTI429_SchedLog(condition, tagval, channum, handleval);
        }
        public static int BTI429_SchedMode(uint modeval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedMode(modeval);
            else
                return BTI429_64.BTI429_SchedMode(modeval);
        }
        public static int BTI429_SchedMsg(uint msgaddr, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedMsg(msgaddr, channum, handleval);
            else
                return BTI429_64.BTI429_SchedMsg(msgaddr, channum, handleval);
        }
        public static int BTI429_SchedMsgEx(uint msgaddr, ushort gapval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedMsgEx(msgaddr, gapval, channum, handleval);
            else
                return BTI429_64.BTI429_SchedMsgEx(msgaddr, gapval, channum, handleval);
        }
        public static int BTI429_SchedPause(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedPause(channum, handleval);
            else
                return BTI429_64.BTI429_SchedPause(channum, handleval);
        }
        public static int BTI429_SchedPulse(int dionum, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedPulse(dionum, channum, handleval);
            else
                return BTI429_64.BTI429_SchedPulse(dionum, channum, handleval);
        }
        public static int BTI429_SchedRestart(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedRestart(channum, handleval);
            else
                return BTI429_64.BTI429_SchedRestart(channum, handleval);
        }
        public static int BTI429_SchedReturn(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedReturn(channum, handleval);
            else
                return BTI429_64.BTI429_SchedReturn(channum, handleval);
        }
        public static void BTI429_BCDGetVal(System.Text.StringBuilder buf, uint msg, ushort sigdig, string resolstr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_BCDGetVal(buf, msg, sigdig, resolstr);
            else
                BTI429_64.BTI429_BCDGetVal(buf, msg, sigdig, resolstr);
        }
        public static uint BTI429_BCDPutVal(string buf, uint msg, ushort sigdig, string resolstr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BCDPutVal(buf, msg, sigdig, resolstr);
            else
                return BTI429_64.BTI429_BCDPutVal(buf, msg, sigdig, resolstr);
        }
        public static void BTI429_BNRGetVal(System.Text.StringBuilder buf, uint msg, ushort sigbit, string resolstr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_BNRGetVal(buf, msg, sigbit, resolstr);
            else
                BTI429_64.BTI429_BNRGetVal(buf, msg, sigbit, resolstr);
        }
        public static uint BTI429_BNRPutVal(string buf, uint msg, ushort sigbit, string resolstr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_BNRPutVal(buf, msg, sigbit, resolstr);
            else
                return BTI429_64.BTI429_BNRPutVal(buf, msg, sigbit, resolstr);
        }
        public static int BTI429_ChConfigEx(uint configval, ushort count, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChConfigEx(configval, count, channum, handleval);
            else
                return BTI429_64.BTI429_ChConfigEx(configval, count, channum, handleval);
        }
        public static int BTI429_ChOutputStateSet(uint pos, uint neg, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ChOutputStateSet(pos, neg, channum, handleval);
            else
                return BTI429_64.BTI429_ChOutputStateSet(pos, neg, channum, handleval);
        }
        public static uint BTI429_CmdAddr(int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdAddr(index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdAddr(index, channum, handleval);
        }
        public static uint BTI429_CmdAlloc(int count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdAlloc(count, handleval);
            else
                return BTI429_64.BTI429_CmdAlloc(count, handleval);
        }
        public static uint BTI429_CmdAllocEx(uint configval, int count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdAllocEx(configval, count, handleval);
            else
                return BTI429_64.BTI429_CmdAllocEx(configval, count, handleval);
        }
        public static uint BTI429_CmdBaseRd(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdBaseRd(channum, handleval);
            else
                return BTI429_64.BTI429_CmdBaseRd(channum, handleval);
        }
        public static void BTI429_CmdBaseWr(uint addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdBaseWr(addrval, channum, handleval);
            else
                BTI429_64.BTI429_CmdBaseWr(addrval, channum, handleval);
        }
        public static int BTI429_CmdClear(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdClear(channum, handleval);
            else
                return BTI429_64.BTI429_CmdClear(channum, handleval);
        }
        public static ushort BTI429_CmdCountRd(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdCountRd(channum, handleval);
            else
                return BTI429_64.BTI429_CmdCountRd(channum, handleval);
        }
        public static void BTI429_CmdCountWr(ushort countval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdCountWr(countval, channum, handleval);
            else
                BTI429_64.BTI429_CmdCountWr(countval, channum, handleval);
        }
        public static ushort BTI429_CmdCtrlRd(int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdCtrlRd(index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdCtrlRd(index, channum, handleval);
        }
        public static void BTI429_CmdCtrlWr(ushort ctrlval, int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdCtrlWr(ctrlval, index, channum, handleval);
            else
                BTI429_64.BTI429_CmdCtrlWr(ctrlval, index, channum, handleval);
        }
        public static ushort BTI429_CmdCurrRd(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdCurrRd(channum, handleval);
            else
                return BTI429_64.BTI429_CmdCurrRd(channum, handleval);
        }
        public static void BTI429_CmdCurrWr(ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdCurrWr(addrval, channum, handleval);
            else
                BTI429_64.BTI429_CmdCurrWr(addrval, channum, handleval);
        }
        public static void BTI429_CmdInit(ushort opcode, ref ushort block)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdInit(opcode, ref block);
            else
                BTI429_64.BTI429_CmdInit(opcode, ref block);
        }
        public static int BTI429_CmdInsert(ref ushort block, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdInsert(ref block, channum, handleval);
            else
                return BTI429_64.BTI429_CmdInsert(ref block, channum, handleval);
        }
        public static ushort BTI429_CmdMaxLoopRd(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdMaxLoopRd(channum, handleval);
            else
                return BTI429_64.BTI429_CmdMaxLoopRd(channum, handleval);
        }
        public static void BTI429_CmdMaxLoopWr(ushort countval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdMaxLoopWr(countval, channum, handleval);
            else
                BTI429_64.BTI429_CmdMaxLoopWr(countval, channum, handleval);
        }
        public static int BTI429_CmdRd(ref ushort block, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdRd(ref block, addrval, handleval);
            else
                return BTI429_64.BTI429_CmdRd(ref block, addrval, handleval);
        }
        public static void BTI429_CmdRestart(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdRestart(channum, handleval);
            else
                BTI429_64.BTI429_CmdRestart(channum, handleval);
        }
        public static int BTI429_CmdShotRd(int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdShotRd(index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdShotRd(index, channum, handleval);
        }
        public static int BTI429_CmdShotWr(int value, int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdShotWr(value, index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdShotWr(value, index, channum, handleval);
        }
        public static int BTI429_CmdSkipRd(int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdSkipRd(index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdSkipRd(index, channum, handleval);
        }
        public static int BTI429_CmdSkipWr(int value, int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdSkipWr(value, index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdSkipWr(value, index, channum, handleval);
        }
        public static void BTI429_CmdStackClr(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdStackClr(channum, handleval);
            else
                BTI429_64.BTI429_CmdStackClr(channum, handleval);
        }
        public static ushort BTI429_CmdStartRd(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdStartRd(channum, handleval);
            else
                return BTI429_64.BTI429_CmdStartRd(channum, handleval);
        }
        public static void BTI429_CmdStartWr(ushort addrval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdStartWr(addrval, channum, handleval);
            else
                BTI429_64.BTI429_CmdStartWr(addrval, channum, handleval);
        }
        public static int BTI429_CmdStepRd(int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdStepRd(index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdStepRd(index, channum, handleval);
        }
        public static int BTI429_CmdStepWr(int value, int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdStepWr(value, index, channum, handleval);
            else
                return BTI429_64.BTI429_CmdStepWr(value, index, channum, handleval);
        }
        public static int BTI429_CmdTotalRd(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdTotalRd(channum, handleval);
            else
                return BTI429_64.BTI429_CmdTotalRd(channum, handleval);
        }
        public static void BTI429_CmdTotalWr(int countval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_CmdTotalWr(countval, channum, handleval);
            else
                BTI429_64.BTI429_CmdTotalWr(countval, channum, handleval);
        }
        public static int BTI429_CmdWr(ref ushort block, uint addrval, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_CmdWr(ref block, addrval, handleval);
            else
                return BTI429_64.BTI429_CmdWr(ref block, addrval, handleval);
        }
        public static uint BTI429_ConvFromArinc(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ConvFromArinc(msgval);
            else
                return BTI429_64.BTI429_ConvFromArinc(msgval);
        }
        public static uint BTI429_ConvToArinc(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ConvToArinc(msgval);
            else
                return BTI429_64.BTI429_ConvToArinc(msgval);
        }
        public static int BTI429_DllUsageCount()
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_DllUsageCount();
            else
                return BTI429_64.BTI429_DllUsageCount();
        }
        public static uint BTI429_FilterAddr(int labelval, int sdival, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterAddr(labelval, sdival, channum, handleval);
            else
                return BTI429_64.BTI429_FilterAddr(labelval, sdival, channum, handleval);
        }
        public static uint BTI429_FilterCreate(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterCreate(handleval);
            else
                return BTI429_64.BTI429_FilterCreate(handleval);
        }
        public static int BTI429_FilterFill(uint msgaddr, uint basevaladdr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FilterFill(msgaddr, basevaladdr, handleval);
            else
                return BTI429_64.BTI429_FilterFill(msgaddr, basevaladdr, handleval);
        }
        public static ushort BTI429_FldGetParBit(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldGetParBit(msgval);
            else
                return BTI429_64.BTI429_FldGetParBit(msgval);
        }
        public static uint BTI429_FldPutParBit(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_FldPutParBit(msgval);
            else
                return BTI429_64.BTI429_FldPutParBit(msgval);
        }
        public static void BTI429_GetChanCount(ref int rcvcount, ref int xmtcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_GetChanCount(ref rcvcount, ref xmtcount, handleval);
            else
                BTI429_64.BTI429_GetChanCount(ref rcvcount, ref xmtcount, handleval);
        }
        public static int BTI429_IsChan(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_IsChan(channum, handleval);
            else
                return BTI429_64.BTI429_IsChan(channum, handleval);
        }
        public static int BTI429_IsRcvChan(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_IsRcvChan(channum, handleval);
            else
                return BTI429_64.BTI429_IsRcvChan(channum, handleval);
        }
        public static int BTI429_IsXmtChan(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_IsXmtChan(channum, handleval);
            else
                return BTI429_64.BTI429_IsXmtChan(channum, handleval);
        }
        public static uint BTI429_LabelReverse(uint msgval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_LabelReverse(msgval);
            else
                return BTI429_64.BTI429_LabelReverse(msgval);
        }
        public static uint BTI429_ListAddr(int index, uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListAddr(index, listaddr, handleval);
            else
                return BTI429_64.BTI429_ListAddr(index, listaddr, handleval);
        }
        public static uint BTI429_ListAddrEx(int index, uint listaddr, ref ushort header, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListAddrEx(index, listaddr, ref header, handleval);
            else
                return BTI429_64.BTI429_ListAddrEx(index, listaddr, ref header, handleval);
        }
        public static int BTI429_ListAdvance(uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListAdvance(listaddr, handleval);
            else
                return BTI429_64.BTI429_ListAdvance(listaddr, handleval);
        }
        public static int BTI429_ListClear(uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListClear(listaddr, handleval);
            else
                return BTI429_64.BTI429_ListClear(listaddr, handleval);
        }
        public static uint BTI429_ListConfig(uint configval, int count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListConfig(configval, count, handleval);
            else
                return BTI429_64.BTI429_ListConfig(configval, count, handleval);
        }
        public static uint BTI429_ListConfigEx(uint configval, int count, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListConfigEx(configval, count, handleval);
            else
                return BTI429_64.BTI429_ListConfigEx(configval, count, handleval);
        }
        public static int BTI429_ListDataBlkRd(uint[] dataptr, ref ushort datacountptr, uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListDataBlkRd(dataptr, ref datacountptr, listaddr, handleval);
            else
                return BTI429_64.BTI429_ListDataBlkRd(dataptr, ref datacountptr, listaddr, handleval);
        }
        public static int BTI429_ListDataBlkWr(uint[] dataptr, ushort datacount, uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListDataBlkWr(dataptr, datacount, listaddr, handleval);
            else
                return BTI429_64.BTI429_ListDataBlkWr(dataptr, datacount, listaddr, handleval);
        }
        public static int BTI429_ListDataBlkWrEx(uint[] dataptr, ref ushort datacountptr, uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListDataBlkWrEx(dataptr, ref datacountptr, listaddr, handleval);
            else
                return BTI429_64.BTI429_ListDataBlkWrEx(dataptr, ref datacountptr, listaddr, handleval);
        }
        public static uint BTI429_ListDataRdEx(int index, uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListDataRdEx(index, listaddr, handleval);
            else
                return BTI429_64.BTI429_ListDataRdEx(index, listaddr, handleval);
        }
        public static void BTI429_ListDataWrEx(uint value, int index, uint listaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ListDataWrEx(value, index, listaddr, handleval);
            else
                BTI429_64.BTI429_ListDataWrEx(value, index, listaddr, handleval);
        }
        public static int BTI429_ListInit(uint listaddr, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListInit(listaddr, msgaddr, handleval);
            else
                return BTI429_64.BTI429_ListInit(listaddr, msgaddr, handleval);
        }
        public static uint BTI429_ListNextRd(uint listaddr, ref ushort header, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListNextRd(listaddr, ref header, handleval);
            else
                return BTI429_64.BTI429_ListNextRd(listaddr, ref header, handleval);
        }
        public static uint BTI429_ListNextWr(uint listaddr, ref ushort header, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ListNextWr(listaddr, ref header, handleval);
            else
                return BTI429_64.BTI429_ListNextWr(listaddr, ref header, handleval);
        }
        public static void BTI429_ListPostRd(uint listaddr, ref ushort header, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ListPostRd(listaddr, ref header, handleval);
            else
                BTI429_64.BTI429_ListPostRd(listaddr, ref header, handleval);
        }
        public static void BTI429_ListPostWr(uint listaddr, ref ushort header, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ListPostWr(listaddr, ref header, handleval);
            else
                BTI429_64.BTI429_ListPostWr(listaddr, ref header, handleval);
        }
        public static void BTI429_ListPreRd(uint listaddr, ref ushort header, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ListPreRd(listaddr, ref header, handleval);
            else
                BTI429_64.BTI429_ListPreRd(listaddr, ref header, handleval);
        }
        public static void BTI429_ListPreWr(uint listaddr, ref ushort header, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_ListPreWr(listaddr, ref header, handleval);
            else
                BTI429_64.BTI429_ListPreWr(listaddr, ref header, handleval);
        }
        public static ushort BTI429_MsgActivityRd(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgActivityRd(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgActivityRd(msgaddr, handleval);
        }
        public static void BTI429_MsgActivityWr(ushort value, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgActivityWr(value, msgaddr, handleval);
            else
                BTI429_64.BTI429_MsgActivityWr(value, msgaddr, handleval);
        }
        public static ushort BTI429_MsgCheck(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgCheck(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgCheck(msgaddr, handleval);
        }
        public static uint BTI429_MsgCommRd(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgCommRd(ref msgfields, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgCommRd(ref msgfields, msgaddr, handleval);
        }
        public static uint BTI429_MsgCommWr(ref MSGFIELDS429 msgfields, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgCommWr(ref msgfields, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgCommWr(ref msgfields, msgaddr, handleval);
        }
        public static int BTI429_MsgConfig(uint configval, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgConfig(configval, msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgConfig(configval, msgaddr, handleval);
        }
        public static int BTI429_MsgMultiSkipWr(int[] skip, uint[] msgaddr, int msgcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgMultiSkipWr(skip, msgaddr, msgcount, handleval);
            else
                return BTI429_64.BTI429_MsgMultiSkipWr(skip, msgaddr, msgcount, handleval);
        }
        public static ushort BTI429_MsgOptRd(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgOptRd(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgOptRd(msgaddr, handleval);
        }
        public static void BTI429_MsgOptWr(ushort msgopt, uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_MsgOptWr(msgopt, msgaddr, handleval);
            else
                BTI429_64.BTI429_MsgOptWr(msgopt, msgaddr, handleval);
        }
        public static uint BTI429_MsgReset(uint msgaddr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_MsgReset(msgaddr, handleval);
            else
                return BTI429_64.BTI429_MsgReset(msgaddr, handleval);
        }
        public static int BTI429_PlayPutUser(uint useraddr, int offset, ushort[] playbuf)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_PlayPutUser(useraddr, offset, playbuf);
            else
                return BTI429_64.BTI429_PlayPutUser(useraddr, offset, playbuf);
        }
        public static int BTI429_ParamWaveformConfig(uint configval, int highvolt, int lowvolt, int nullvolt, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ParamWaveformConfig(configval, highvolt, lowvolt, nullvolt, channum, handleval);
            else
                return BTI429_64.BTI429_ParamWaveformConfig(configval, highvolt, lowvolt, nullvolt, channum, handleval);
        }
        public static int BTI429_ParamWaveformConfigEx(uint configval, ref PARAMFIELDS429 paramflds, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_ParamWaveformConfigEx(configval, ref paramflds, channum, handleval);
            else
                return BTI429_64.BTI429_ParamWaveformConfigEx(configval, ref paramflds, channum, handleval);
        }
        public static int BTI429_Sched1Build(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiodin, int[] maxperiodin, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched1Build(basevalmsgnum, nummsgs, msgaddr, minperiodin, maxperiodin, speed, microflag, debugvalflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched1Build(basevalmsgnum, nummsgs, msgaddr, minperiodin, maxperiodin, speed, microflag, debugvalflag, interceptflag, channum, handleval);
        }
        public static int BTI429_Sched2AdvanceMessage(int entries, int panicindex, int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched2AdvanceMessage(entries, panicindex, nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched2AdvanceMessage(entries, panicindex, nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
        }
        public static int BTI429_Sched2AdvanceNoMessage(int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched2AdvanceNoMessage(nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched2AdvanceNoMessage(nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
        }
        public static void BTI429_Sched2Analyze(int nummsgs, nint infoptr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_Sched2Analyze(nummsgs, infoptr);
            else
                BTI429_64.BTI429_Sched2Analyze(nummsgs, infoptr);
        }
        public static int BTI429_Sched2Build(int nummsgs, uint[] msgaddrptr, int[] minlist, int[] maxlist, int speedflag, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched2Build(nummsgs, msgaddrptr, minlist, maxlist, speedflag, microflag, debugvalflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched2Build(nummsgs, msgaddrptr, minlist, maxlist, speedflag, microflag, debugvalflag, interceptflag, channum, handleval);
        }
        public static int BTI429_Sched2BuildEx(ref int styleptr, int nummsgs, int[] minlist, int[] maxlist, int speedflag, int microflag, int interceptflag)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched2BuildEx(ref styleptr, nummsgs, minlist, maxlist, speedflag, microflag, interceptflag);
            else
                return BTI429_64.BTI429_Sched2BuildEx(ref styleptr, nummsgs, minlist, maxlist, speedflag, microflag, interceptflag);
        }
        public static int BTI429_Sched2BuildExx(int style, int speedflag, int microflag, int debugvalflag, int tryflag, int nummsgs, int[] minlist, int[] maxlist, ref int entriesptr, ref int maxentriesptr, nint infoptr, uint[] msgaddrptr, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched2BuildExx(style, speedflag, microflag, debugvalflag, tryflag, nummsgs, minlist, maxlist, ref entriesptr, ref maxentriesptr, infoptr, msgaddrptr, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched2BuildExx(style, speedflag, microflag, debugvalflag, tryflag, nummsgs, minlist, maxlist, ref entriesptr, ref maxentriesptr, infoptr, msgaddrptr, interceptflag, channum, handleval);
        }
        public static int BTI429_Sched3Build(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched3Build(nummsgs, msgaddr, minperiod, maxperiod, speed, microflag, debugvalflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched3Build(nummsgs, msgaddr, minperiod, maxperiod, speed, microflag, debugvalflag, interceptflag, channum, handleval);
        }
        public static int BTI429_Sched3BuildEx(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiodin, int[] maxperiodin, int speed, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched3BuildEx(basevalmsgnum, nummsgs, msgaddr, minperiodin, maxperiodin, speed, microflag, debugvalflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched3BuildEx(basevalmsgnum, nummsgs, msgaddr, minperiodin, maxperiodin, speed, microflag, debugvalflag, interceptflag, channum, handleval);
        }
        public static uint BTI429_Sched3Calc(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched3Calc(infoptr);
            else
                return BTI429_64.BTI429_Sched3Calc(infoptr);
        }
        public static int BTI429_Sched3Check(ref int minperiod, ref int maxperiod, nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched3Check(ref minperiod, ref maxperiod, infoptr);
            else
                return BTI429_64.BTI429_Sched3Check(ref minperiod, ref maxperiod, infoptr);
        }
        public static uint BTI429_Sched3ToBit(uint value, nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched3ToBit(value, infoptr);
            else
                return BTI429_64.BTI429_Sched3ToBit(value, infoptr);
        }
        public static int BTI429_Sched4AdvanceMessage(int entries, int panicindex, int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched4AdvanceMessage(entries, panicindex, nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched4AdvanceMessage(entries, panicindex, nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
        }
        public static int BTI429_Sched4AdvanceNoMessage(int nummsgs, nint infoptr, ref int nextdeltaptr, int ulmastertimer, ref uint msgaddrptr, int debugvalflag, int tryflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched4AdvanceNoMessage(nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched4AdvanceNoMessage(nummsgs, infoptr, ref nextdeltaptr, ulmastertimer, ref msgaddrptr, debugvalflag, tryflag, interceptflag, channum, handleval);
        }
        public static void BTI429_Sched4Analyze(int nummsgs, nint infoptr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_Sched4Analyze(nummsgs, infoptr);
            else
                BTI429_64.BTI429_Sched4Analyze(nummsgs, infoptr);
        }
        public static int BTI429_Sched4Build(int nummsgs, uint[] msgaddrptr, int[] minlist, int[] maxlist, int speedflag, int microflag, int debugvalflag, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched4Build(nummsgs, msgaddrptr, minlist, maxlist, speedflag, microflag, debugvalflag, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched4Build(nummsgs, msgaddrptr, minlist, maxlist, speedflag, microflag, debugvalflag, interceptflag, channum, handleval);
        }
        public static int BTI429_Sched4BuildEx(ref int styleptr, int nummsgs, int minorframetime, int majorframetime, int[] targetlist, int[] minlist, int[] maxlist, int speedflag, int microflag, int interceptflag)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched4BuildEx(ref styleptr, nummsgs, minorframetime, majorframetime, targetlist, minlist, maxlist, speedflag, microflag, interceptflag);
            else
                return BTI429_64.BTI429_Sched4BuildEx(ref styleptr, nummsgs, minorframetime, majorframetime, targetlist, minlist, maxlist, speedflag, microflag, interceptflag);
        }
        public static int BTI429_Sched4BuildExx(int style, int speedflag, int microflag, int debugvalflag, int tryflag, int nummsgs, int minorframetime, int majorframetime, int[] targetlist, int[] minlist, int[] maxlist, ref int entriesptr, ref int maxentriesptr, nint infoptr, uint[] msgaddrptr, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Sched4BuildExx(style, speedflag, microflag, debugvalflag, tryflag, nummsgs, minorframetime, majorframetime, targetlist, minlist, maxlist, ref entriesptr, ref maxentriesptr, infoptr, msgaddrptr, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_Sched4BuildExx(style, speedflag, microflag, debugvalflag, tryflag, nummsgs, minorframetime, majorframetime, targetlist, minlist, maxlist, ref entriesptr, ref maxentriesptr, infoptr, msgaddrptr, interceptflag, channum, handleval);
        }
        public static uint BTI429_SchedAdvance(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedAdvance(infoptr);
            else
                return BTI429_64.BTI429_SchedAdvance(infoptr);
        }
        public static int BTI429_SchedAgain(int index, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedAgain(index, channum, handleval);
            else
                return BTI429_64.BTI429_SchedAgain(index, channum, handleval);
        }
        public static int BTI429_SchedBranchEx(ushort condition1, ushort condition2, int destindex, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBranchEx(condition1, condition2, destindex, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBranchEx(condition1, condition2, destindex, channum, handleval);
        }
        public static int BTI429_SchedBuildBuf(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int bufcount, ref byte bufptr, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBuildBuf(nummsgs, msgaddr, minperiod, maxperiod, bufcount, ref bufptr, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBuildBuf(nummsgs, msgaddr, minperiod, maxperiod, bufcount, ref bufptr, channum, handleval);
        }
        public static int BTI429_SchedBuildBufEx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int debugval, int microflag, int algorithm, int bufcount, ref byte bufptr, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBuildBufEx(nummsgs, msgaddr, minperiod, maxperiod, speed, debugval, microflag, algorithm, bufcount, ref bufptr, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBuildBufEx(nummsgs, msgaddr, minperiod, maxperiod, speed, debugval, microflag, algorithm, bufcount, ref bufptr, channum, handleval);
        }
        public static int BTI429_SchedBuildEx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBuildEx(nummsgs, msgaddr, minperiod, maxperiod, speed, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBuildEx(nummsgs, msgaddr, minperiod, maxperiod, speed, channum, handleval);
        }
        public static int BTI429_SchedBuildExx(int basevalmsgnum, int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, nint buf, uint bufsize, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBuildExx(basevalmsgnum, nummsgs, msgaddr, minperiod, maxperiod, speed, buf, bufsize, channum, handleval);
            else
                return BTI429_64.BTI429_SchedBuildExx(basevalmsgnum, nummsgs, msgaddr, minperiod, maxperiod, speed, buf, bufsize, channum, handleval);
        }
        public static int BTI429_SchedBuildExxx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, int debugval, int microflag, int algorithm, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBuildExxx(nummsgs, msgaddr, minperiod, maxperiod, speed, channum, debugval, microflag, algorithm, handleval);
            else
                return BTI429_64.BTI429_SchedBuildExxx(nummsgs, msgaddr, minperiod, maxperiod, speed, channum, debugval, microflag, algorithm, handleval);
        }
        public static int BTI429_SchedBuildExxxx(int nummsgs, uint[] msgaddr, int[] minperiod, int[] maxperiod, int speed, int channum, int debugval, int microflag, int algorithm, int interceptflag, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedBuildExxxx(nummsgs, msgaddr, minperiod, maxperiod, speed, channum, debugval, microflag, algorithm, interceptflag, handleval);
            else
                return BTI429_64.BTI429_SchedBuildExxxx(nummsgs, msgaddr, minperiod, maxperiod, speed, channum, debugval, microflag, algorithm, interceptflag, handleval);
        }
        public static uint BTI429_SchedCalc(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedCalc(infoptr);
            else
                return BTI429_64.BTI429_SchedCalc(infoptr);
        }
        public static int BTI429_SchedCallEx(ushort condition1, ushort condition2, int destindex, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedCallEx(condition1, condition2, destindex, channum, handleval);
            else
                return BTI429_64.BTI429_SchedCallEx(condition1, condition2, destindex, channum, handleval);
        }
        public static int BTI429_SchedCheck(int[] minperiod, int[] maxperiod, nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedCheck(minperiod, maxperiod, infoptr);
            else
                return BTI429_64.BTI429_SchedCheck(minperiod, maxperiod, infoptr);
        }
        public static void BTI429_SchedCheckEnd()
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedCheckEnd();
            else
                BTI429_64.BTI429_SchedCheckEnd();
        }
        public static void BTI429_SchedCheckMiddle(uint msgnum, uint minlen, uint midlen, uint maxlen)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedCheckMiddle(msgnum, minlen, midlen, maxlen);
            else
                BTI429_64.BTI429_SchedCheckMiddle(msgnum, minlen, midlen, maxlen);
        }
        public static void BTI429_SchedCheckReal(uint msgnum, uint minlen, uint midlen, uint maxlen)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedCheckReal(msgnum, minlen, midlen, maxlen);
            else
                BTI429_64.BTI429_SchedCheckReal(msgnum, minlen, midlen, maxlen);
        }
        public static void BTI429_SchedCheckStart()
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedCheckStart();
            else
                BTI429_64.BTI429_SchedCheckStart();
        }
        public static uint BTI429_SchedConflicts(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedConflicts(infoptr);
            else
                return BTI429_64.BTI429_SchedConflicts(infoptr);
        }
        unsafe public static void BTI429_SchedDisplay(uint nummsgs, ref uint count, ref uint* data)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedDisplay(nummsgs, ref count, ref data);
            else
                BTI429_64.BTI429_SchedDisplay(nummsgs, ref count, ref data);
        }
        public static int BTI429_SchedDivide(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedDivide(infoptr);
            else
                return BTI429_64.BTI429_SchedDivide(infoptr);
        }
        public static uint BTI429_SchedFix(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedFix(infoptr);
            else
                return BTI429_64.BTI429_SchedFix(infoptr);
        }
        public static void BTI429_SchedFixEnd()
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedFixEnd();
            else
                BTI429_64.BTI429_SchedFixEnd();
        }
        public static void BTI429_SchedFixMiddle(uint conflicts)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedFixMiddle(conflicts);
            else
                BTI429_64.BTI429_SchedFixMiddle(conflicts);
        }
        public static void BTI429_SchedFixStart()
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedFixStart();
            else
                BTI429_64.BTI429_SchedFixStart();
        }
        public static void BTI429_SchedHook(int index, nint ptr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedHook(index, ptr);
            else
                BTI429_64.BTI429_SchedHook(index, ptr);
        }
        public static int BTI429_SchedInsert(uint eventvals, uint index, uint msgnum, ref uint msgaddr, uint gapval, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedInsert(eventvals, index, msgnum, ref msgaddr, gapval, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_SchedInsert(eventvals, index, msgnum, ref msgaddr, gapval, interceptflag, channum, handleval);
        }
        public static int BTI429_SchedInsert2(uint eventvals, uint index, int msgnum, ref uint msgaddr, uint gapval, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedInsert2(eventvals, index, msgnum, ref msgaddr, gapval, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_SchedInsert2(eventvals, index, msgnum, ref msgaddr, gapval, interceptflag, channum, handleval);
        }
        public static int BTI429_SchedInsertEnd(uint eventvals, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedInsertEnd(eventvals, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_SchedInsertEnd(eventvals, interceptflag, channum, handleval);
        }
        public static int BTI429_SchedInsertStart(uint eventvals, int interceptflag, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedInsertStart(eventvals, interceptflag, channum, handleval);
            else
                return BTI429_64.BTI429_SchedInsertStart(eventvals, interceptflag, channum, handleval);
        }
        public static int BTI429_SchedIntercept(int typeval, uint msgaddr, uint gapval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedIntercept(typeval, msgaddr, gapval, channum, handleval);
            else
                return BTI429_64.BTI429_SchedIntercept(typeval, msgaddr, gapval, channum, handleval);
        }
        public static void BTI429_SchedLayout(nint infoptr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedLayout(infoptr);
            else
                BTI429_64.BTI429_SchedLayout(infoptr);
        }
        public static int BTI429_SchedLogEx(ushort condition1, ushort condition2, ushort tagval, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedLogEx(condition1, condition2, tagval, channum, handleval);
            else
                return BTI429_64.BTI429_SchedLogEx(condition1, condition2, tagval, channum, handleval);
        }
        public static uint BTI429_SchedMaxval(uint[] arrayval, uint arrayvalcount)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedMaxval(arrayval, arrayvalcount);
            else
                return BTI429_64.BTI429_SchedMaxval(arrayval, arrayvalcount);
        }
        public static uint BTI429_SchedMinval(uint[] arrayval, uint arrayvalcount)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedMinval(arrayval, arrayvalcount);
            else
                return BTI429_64.BTI429_SchedMinval(arrayval, arrayvalcount);
        }
        public static int BTI429_SchedNop(int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedNop(channum, handleval);
            else
                return BTI429_64.BTI429_SchedNop(channum, handleval);
        }
        public static int BTI429_SchedParse(int basevalmsgnum, ref uint msgaddr, int channum, nint infoptr, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedParse(basevalmsgnum, ref msgaddr, channum, infoptr, handleval);
            else
                return BTI429_64.BTI429_SchedParse(basevalmsgnum, ref msgaddr, channum, infoptr, handleval);
        }
        public static int BTI429_SchedPreprocess(int nummsgs, int speedflag, uint[] msgaddrptr, int[] minlist, int[] maxlist, int debugvalflag, int[] targetlist, ref int minorframetime, ref int majorframetime)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedPreprocess(nummsgs, speedflag, msgaddrptr, minlist, maxlist, debugvalflag, targetlist, ref minorframetime, ref majorframetime);
            else
                return BTI429_64.BTI429_SchedPreprocess(nummsgs, speedflag, msgaddrptr, minlist, maxlist, debugvalflag, targetlist, ref minorframetime, ref majorframetime);
        }
        public static int BTI429_SchedPulse0(int dionum, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedPulse0(dionum, channum, handleval);
            else
                return BTI429_64.BTI429_SchedPulse0(dionum, channum, handleval);
        }
        public static int BTI429_SchedPulse1(int dionum, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedPulse1(dionum, channum, handleval);
            else
                return BTI429_64.BTI429_SchedPulse1(dionum, channum, handleval);
        }
        public static int BTI429_SchedReset(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedReset(infoptr);
            else
                return BTI429_64.BTI429_SchedReset(infoptr);
        }
        public static uint BTI429_SchedRotate(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedRotate(infoptr);
            else
                return BTI429_64.BTI429_SchedRotate(infoptr);
        }
        public static int BTI429_SchedShift(int msgnum, int value, nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedShift(msgnum, value, infoptr);
            else
                return BTI429_64.BTI429_SchedShift(msgnum, value, infoptr);
        }
        public static int BTI429_SchedSlide(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedSlide(infoptr);
            else
                return BTI429_64.BTI429_SchedSlide(infoptr);
        }
        public static void BTI429_SchedSlideEnd()
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedSlideEnd();
            else
                BTI429_64.BTI429_SchedSlideEnd();
        }
        public static void BTI429_SchedSlideMiddle(uint index, uint count, uint conflicts)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedSlideMiddle(index, count, conflicts);
            else
                BTI429_64.BTI429_SchedSlideMiddle(index, count, conflicts);
        }
        public static void BTI429_SchedSlideStart(uint factor)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedSlideStart(factor);
            else
                BTI429_64.BTI429_SchedSlideStart(factor);
        }
        public static void BTI429_SchedSort(nint infoptr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedSort(infoptr);
            else
                BTI429_64.BTI429_SchedSort(infoptr);
        }
        public static uint BTI429_SchedToBit(uint value, nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedToBit(value, infoptr);
            else
                return BTI429_64.BTI429_SchedToBit(value, infoptr);
        }
        public static uint BTI429_SchedToMsec(uint value, nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedToMsec(value, infoptr);
            else
                return BTI429_64.BTI429_SchedToMsec(value, infoptr);
        }
        public static void BTI429_SchedUnsort(nint infoptr)
        {
            if (IntPtr.Size == 4)
                BTI429_32.BTI429_SchedUnsort(infoptr);
            else
                BTI429_64.BTI429_SchedUnsort(infoptr);
        }
        public static int BTI429_SchedUser(uint useraddr, int channum, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedUser(useraddr, channum, handleval);
            else
                return BTI429_64.BTI429_SchedUser(useraddr, channum, handleval);
        }
        public static uint BTI429_SchedWalk(nint infoptr)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedWalk(infoptr);
            else
                return BTI429_64.BTI429_SchedWalk(infoptr);
        }
        public static int BTI429_SchedWriteBuf(ref byte bufptr, int bufcount, uint schaddr, int schedcount, nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_SchedWriteBuf(ref bufptr, bufcount, schaddr, schedcount, handleval);
            else
                return BTI429_64.BTI429_SchedWriteBuf(ref bufptr, bufcount, schaddr, schedcount, handleval);
        }
        public static int BTI429_TestProtocol(nint handleval)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_TestProtocol(handleval);
            else
                return BTI429_64.BTI429_TestProtocol(handleval);
        }
        public static ushort BTI429_Transpose12(ushort val)
        {
            if (IntPtr.Size == 4)
                return BTI429_32.BTI429_Transpose12(val);
            else
                return BTI429_64.BTI429_Transpose12(val);
        }

        /**
        *
        *  Channel number constants
        *
        **/

        public const uint CH0 = 0x0000;           //Channel 0
        public const uint CH1 = 0x0001;           //Channel 1
        public const uint CH2 = 0x0002;           //Channel 2
        public const uint CH3 = 0x0003;           //Channel 3
        public const uint CH4 = 0x0004;           //Channel 4
        public const uint CH5 = 0x0005;           //Channel 5
        public const uint CH6 = 0x0006;           //Channel 6
        public const uint CH7 = 0x0007;           //Channel 7
        public const uint CH8 = 0x0008;           //Channel 8
        public const uint CH9 = 0x0009;           //Channel 9
        public const uint CH10 = 0x000A;           //Channel 10
        public const uint CH11 = 0x000B;           //Channel 11
        public const uint CH12 = 0x000C;           //Channel 12
        public const uint CH13 = 0x000D;           //Channel 13
        public const uint CH14 = 0x000E;           //Channel 14
        public const uint CH15 = 0x000F;           //Channel 15
        public const uint CH16 = 0x0010;           //Channel 16
        public const uint CH17 = 0x0011;           //Channel 17
        public const uint CH18 = 0x0012;           //Channel 18
        public const uint CH19 = 0x0013;           //Channel 19
        public const uint CH20 = 0x0014;           //Channel 20
        public const uint CH21 = 0x0015;           //Channel 21
        public const uint CH22 = 0x0016;           //Channel 22
        public const uint CH23 = 0x0017;           //Channel 23
        public const uint CH24 = 0x0018;           //Channel 24
        public const uint CH25 = 0x0019;           //Channel 25
        public const uint CH26 = 0x001A;           //Channel 26
        public const uint CH27 = 0x001B;           //Channel 27
        public const uint CH28 = 0x001C;           //Channel 28
        public const uint CH29 = 0x001D;           //Channel 29
        public const uint CH30 = 0x001E;           //Channel 30
        public const uint CH31 = 0x001F;           //Channel 31

        /**
        *
        *  SDI selection constants
        *
        **/

        public const uint SDI00 = 0x0001;	//Enable SDI00
        public const uint SDI01 = 0x0002;	//Enable SDI01
        public const uint SDI10 = 0x0004;	//Enable SDI10
        public const uint SDI11 = 0x0008;	//Enable SDI11
        public const uint SDIALL = 0x000F;	//Enable all SDIs

        /**
        *
        *  Channel configuration options
        *
        **/

        public const uint CHCFG429_DEFAULT = 0x00000000;	//Select all default settings (XMT & RCV) (default)
        public const uint CHCFG429_HIGHSPEED = 0x00000001;	//Select high speed (XMT & RCV)
        public const uint CHCFG429_AUTOSPEED = 0x00000002;	//Select auto speed detection (RCV)
        public const uint CHCFG429_LOWSPEED = 0x00000000;	//Select low speed (XMT & RCV) (default)
        public const uint CHCFG429_SELFTEST = 0x00000004;	//Enable internal wraparound (XMT & RCV)
        public const uint CHCFG429_SELFTESTOFF = 0x00000000;	//Disable internal wraparound (XMT & RCV) (default)
        public const uint CHCFG429_SYNC = 0x00000008;	//Sync Enable (XMT & RCV)
        public const uint CHCFG429_SYNCOFF = 0x00000000;	//Disable sync output (XMT & RCV) (default)
        public const uint CHCFG429_PARODD = 0x00000000;	//Parity odd (XMT & RCV) (default)
        public const uint CHCFG429_PAREVEN = 0x00000010;	//Parity even (XMT & RCV)
        public const uint CHCFG429_PARDATA = 0x00000020;	//Parity bit as data (XMT & RCV)
        public const uint CHCFG429_ACTIVE = 0x00000000;	//Enable channel activity (XMT & RCV) (default)
        public const uint CHCFG429_INACTIVE = 0x00000040;	//Disable channel activity (XMT & RCV)
        public const uint CHCFG429_EXTTRIG = 0x00000080;	//Enable external trigger for all messages (XMT)
        public const uint CHCFG429_EXTOFF = 0x00000000;	//External trigger is enabled on message level (XMT) (default)
        public const uint CHCFG429_PARERR = 0x00000100;	//Enable parity error for all messages (XMT)
        public const uint CHCFG429_NOERR = 0x00000000;	//Errors are enabled on message level (XMT) (default)
        public const uint CHCFG429_HIT = 0x00000200;	//Hit counter is enabled for all messages
        public const uint CHCFG429_NOHIT = 0x00000000;	//Hit counter is enabled on message level (default)
        public const uint CHCFG429_TIMETAG = 0x00000400;	//Enable time-tag for all message records
        public const uint CHCFG429_TIMETAGOFF = 0x00000000;	//Time-tag is enabled on message level (default)
        public const uint CHCFG429_ELAPSE = 0x00000800;	//Enable elapse time for all messages
        public const uint CHCFG429_ELAPSEOFF = 0x00000000;	//Elapse time is enabled on message level (default)
        public const uint CHCFG429_MAX = 0x00001000;	//Enable max repetition rate monitoring for all messages
        public const uint CHCFG429_MIN = 0x00002000;	//Enable min repetition rate monitoring for all messages
        public const uint CHCFG429_MAXMIN = 0x00003000;	//Enable max and min repetition rate monitoring for all messages
        public const uint CHCFG429_MAXMINOFF = 0x00000000;	//Repetition rate monitoring is enabled on message level (default)
        public const uint CHCFG429_SEQALL = 0x00080000;	//Record entire channel in sequential record
        public const uint CHCFG429_SEQSEL = 0x00000000;	//Sequential record recording is enabled at message level (default)
        public const uint CHCFG429_LOOPMAX = 0x00100000;	//Enable schedule maximum loop count
        public const uint CHCFG429_NOLOOPMAX = 0x00000000;	//Disable schedule maximum loop count (default)
        public const uint CHCFG429_LOGHALT = 0x00200000;	//Enable event log on schedule halt
        public const uint CHCFG429_NOLOGHALT = 0x00000000;	//No event log on schedule halt (default)
        public const uint CHCFG429_LOGPAUSE = 0x00400000;	//Enable event log on schedule pause
        public const uint CHCFG429_NOLOGPAUSE = 0x00000000;	//No event log on schedule pause (default)
        public const uint CHCFG429_LOGERR = 0x00800000;	//Enable event log on decoder errors.
        public const uint CHCFG429_NOLOGERR = 0x00000000;	//No event log on decoder errors (default)
        public const uint CHCFG429_PAUSE = 0x01000000;	//Mark channel as paused
        public const uint CHCFG429_UNPAUSE = 0x00000000;	//Mark channel as unpaused (default)
        public const uint CHCFG429_GAP1US = 0x00004000;	//Enable microsecond gaps
        public const uint CHCFG429_GAPBIT = 0x00000000;	//No microsecond gaps
        public const uint CHCFG429_PLAYBACK = 0x00008000;	//Configure Tx Channel for Hardware Playback (FIFO mode)
        public const uint CHCFG429_SCHEDULE = 0x00000000;	//Configure Tx Channel to use Schedule (default)

        /**
        *
        *  Message configuration options
        *
        **/

        public const uint MSGCRT429_DEFAULT = 0x00000000;	//Default settings
        public const uint MSGCRT429_NOSEQ = 0x00000000;	//Message will not be recorded in sequential record (default)
        public const uint MSGCRT429_SEQ = 0x00000001;	//Message will be recorded in sequential record
        public const uint MSGCRT429_NOLOG = 0x00000000;	//Message will not generate event log (default)
        public const uint MSGCRT429_LOG = 0x00000002;	//Message will generate event log
        public const uint MSGCRT429_NOSKIP = 0x00000000;	//Message will not be skipped (default)
        public const uint MSGCRT429_SKIP = 0x00000008;	//Message will be skipped
        public const uint MSGCRT429_NOTIMETAG = 0x00000000;	//Message will not record time-tag (default)
        public const uint MSGCRT429_TIMETAG = 0x00000010;	//Message will record time-tag
        public const uint MSGCRT429_NOELAPSE = 0x00000000;	//Message will not record elapse time (default)
        public const uint MSGCRT429_ELAPSE = 0x00000020;	//Message will record elapse time
        public const uint MSGCRT429_NOMAXMIN = 0x00000000;	//Message will not record min/max time (default)
        public const uint MSGCRT429_MAX = 0x00000040;	//Message will record max time
        public const uint MSGCRT429_MIN = 0x00000080;	//Message will record min time
        public const uint MSGCRT429_MAXMIN = 0x000000C0;	//Message will record min/max time
        public const uint MSGCRT429_NOSYNC = 0x00000000;	//No sync will be generated for message (default)
        public const uint MSGCRT429_SYNC = 0x00000100;	//Sync will be generated for message
        public const uint MSGCRT429_NOERR = 0x00000000;	//No error will be generated for message (default)
        public const uint MSGCRT429_PARERR = 0x00000200;	//Parity error will be generated for message
        public const uint MSGCRT429_NOHIT = 0x00000000;	//Message will not record hit count (default)
        public const uint MSGCRT429_HIT = 0x00000400;	//Message will record hit count
        public const uint MSGCRT429_NOEXTRIG = 0x00000000;	//Message will not be externally triggered (default)
        public const uint MSGCRT429_EXTRIG = 0x00001000;	//Message will be externally triggered
        public const uint MSGCRT429_WIPE = 0x00000000;	//Enables message clear (default)
        public const uint MSGCRT429_NOWIPE = 0x80000000;	//Disables message clear
        public const uint MSGCRT429_WIPE0 = 0x00000000;	//Initialize data with zeros (default)
        public const uint MSGCRT429_WIPE1 = 0x40000000;	//Initialize data with ones

        /**
        *
        *  List buffer options
        *
        **/

        public const uint LISTCRT429_DEFAULT = 0x00000000;	//Select all default settings
        public const uint LISTCRT429_FIFO = 0x00000000;	//Enable FIFO mode (default)
        public const uint LISTCRT429_PINGPONG = 0x00000001;	//Enable ping-pong mode
        public const uint LISTCRT429_CIRCULAR = 0x00000002;	//Enable circular mode
        public const uint LISTCRT429_RCV = 0x00000010;	//User will read from list buffer
        public const uint LISTCRT429_XMT = 0x00000020;	//User will write to list buffer
        public const uint LISTCRT429_ASYNC = 0x00000040;	//Asynchronous mode
        public const uint LISTCRT429_NOLOG = 0x00000000;	//Do not generate event log when list buffer empty/full (default)
        public const uint LISTCRT429_LOG = 0x00000100;	//Generate event log when list buffer empty/full

        /**
        *
        *  Parametric configuration options
        *
        **/

        public const uint PARAMCFG429_DEFAULT = 0x00000000;	//Select all default settings (default)
        public const uint PARAMCFG429_AMPLON = 0x00000000;	//Enables parametric amplitude control (default)
        public const uint PARAMCFG429_AMPLOFF = 0x00000001;	//Disables parametric amplitude control
        public const uint PARAMCFG429_BITRATEON = 0x00000000;	//Enables parametric bit rate control (default)
        public const uint PARAMCFG429_BITRATEOFF = 0x00000002;	//Disables parametric bit rate control
        public const uint PARAMCFG429_WAVEON = 0x00000000;	//Enables parametric waveform control (default)
        public const uint PARAMCFG429_WAVEOFF = 0x00000004;	//Disables parametric waveform control
        public const uint PARAMCFG429_LINKOFF = 0x00000000;	//Each channel operates independently (default)
        public const uint PARAMCFG429_LINK = 0x00000008;	//This channel's output is linked to another channel
        public const uint PARAMCFG429_CMBIASOFF = 0x00000000;	//Disables common-mode DC bias (default)
        public const uint PARAMCFG429_CMBIAS = 0x00000010;	//Enables common-mode DC bias

        /**
        *
        *  Scheduling conditions.
        *
        **/

        public const uint COND429_ALWAYS = 0x00000000;	//Unconditional
        public const uint COND429_DIO1ACT = 0x00000001;	//Condition on digital I/O #1 active
        public const uint COND429_DIO1NACT = 0x00000002;	//Condition on digital I/O #1 not active
        public const uint COND429_DIO2ACT = 0x00000004;	//Condition on digital I/O #2 active
        public const uint COND429_DIO2NACT = 0x00000008;	//Condition on digital I/O #2 not active
        public const uint COND429_DIO3ACT = 0x00000010;	//Condition on digital I/O #3 active
        public const uint COND429_DIO3NACT = 0x00000020;	//Condition on digital I/O #3 not active
        public const uint COND429_DIO4ACT = 0x00000040;	//Condition on digital I/O #4 active
        public const uint COND429_DIO4NACT = 0x00000080;	//Condition on digital I/O #4 not active
        public const uint COND429_DIO5ACT = 0x00000100;	//Condition on digital I/O #5 active
        public const uint COND429_DIO5NACT = 0x00000200;	//Condition on digital I/O #5 not active
        public const uint COND429_DIO6ACT = 0x00000400;	//Condition on digital I/O #6 active
        public const uint COND429_DIO6NACT = 0x00000800;	//Condition on digital I/O #6 not active
        public const uint COND429_DIO7ACT = 0x00001000;	//Condition on digital I/O #7 active
        public const uint COND429_DIO7NACT = 0x00002000;	//Condition on digital I/O #7 not active
        public const uint COND429_DIO8ACT = 0x00004000;	//Condition on digital I/O #8 active
        public const uint COND429_DIO8NACT = 0x00008000;	//Condition on digital I/O #8 not active

        /**
        *
        *  Bit fields of the message activity flag.
        *
        **/

        public const uint MSGACT429_CHMASK = 0xFF00;	//Channel number mask value
        public const uint MSGACT429_CHSHIFT = 0x0008;	//Channel number shift value
        public const uint MSGACT429_SPD = 0x0080;	//Bus speed
        public const uint MSGACT429_ERR = 0x0040;	//Error bit
        public const uint MSGACT429_GAP = 0x0020;	//Gap error bit
        public const uint MSGACT429_PAR = 0x0010;	//Parity error bit
        public const uint MSGACT429_LONG = 0x0008;	//Long word error bit
        public const uint MSGACT429_BIT = 0x0004;	//Bit time error bit
        public const uint MSGACT429_TO = 0x0002;	//Time out error bit
        public const uint MSGACT429_HIT = 0x0001;	//Always set

        /**
        *
        *  Channel info fields.
        *
        **/

        public const uint INFO429_PARAM = 0x0001;	//Channel supports parametric control
        public const uint INFO429_PWAVE = 0x0002;	//Channel supports parametric waveform (DAC)
        public const uint INFO429_CMBIAS = 0x0003;	//Channel supports common-mode DC bias
        public const uint INFO429_OUTSTATE = 0x0004;	//Channel supports Leg lifting/shorting
        public const uint INFO429_PLAYBACK = 0x0005;	//Channel supports hardware playback mode
        public const uint INFO429_PBITCOUNT = 0x0006;	//Channel supports parametric bitcounts
        public const uint INFO429_SCHDATA = 0x0007;	//Channel supports SchedData
        public const uint INFO429_GAP1US = 0x0008;	//Channel supports microsecond gaps

        /**
        *
        *  Scheduling algorithm options
        *
        **/

        public const uint SCHEDMODE_DEFAULT = 0x0000;	//Select all default settings
        public const uint SCHEDMODE_METHOD_NORMAL = 0x0000;	//Choose the normal scheduling method
        public const uint SCHEDMODE_METHOD_QUICK = 0x0001;	//Choose the quick scheduling method
        public const uint SCHEDMODE_METHOD_BOTH = 0x0002;	//Use all methods: use the quick method first if fail then use the normal method finally use the Legacy method
        public const uint SCHEDMODE_METHOD_LEGACY = 0x0004;	//Use all methods: use the Legacy l43 method first if fail then quick method if fail then use the normal method
        public const uint SCHEDMODE_METHOD_MASK = 0x000F;	//Mask for method setting
        public const uint SCHEDMODE_MILLISEC = 0x0000;	//Specify periods in milliseconds
        public const uint SCHEDMODE_MICROSEC = 0x0010;	//Specify periods in microseconds
        public const uint SCHEDMODE_REMOTE = 0x0000;	//Perform calculations remotely if applicable
        public const uint SCHEDMODE_LOCAL = 0x0020;	//Perform calculations locally if applicable
        public const uint SCHEDMODE_SKIPRANGECHECK = 0x0000;	//Skips range checking of a message when the min period equals the max period. Attempts to meet the rate but schedule will succeed even if the messages with same min/max cannot transmit at a specified period
        public const uint SCHEDMODE_RANGECHECK = 0x0040;	//Performs range checking on all messages. SchedBuild fails if any messages are out of range.
        public const uint SCHEDMODE_DEFAULT_ALGOR = 0x0000;	//Choose the default algorithm
        public const uint SCHEDMODE_QUICK_ALGOR = 0x0001;	//Choose the quick algorithm
        public const uint SCHEDMODE_BOTH_ALGOR = 0x0002;	//Use all algorithms: Use the quick algorithm first if fail then use the normal algorithm finally use the Legacy algorithm
        public const uint SCHEDMODE_ALGOR_MASK = 0x000F;	//Mask for algorithm setting

        /**
        *
        *  Transmit channel output options
        *
        **/

        public const uint OUTSTATE429_NOCHANGE = 0x00000000;	// Keep leg in its current state (default)
        public const uint OUTSTATE429_SIGNAL = 0x00000001;	// Connect the leg to normal transmit signal
        public const uint OUTSTATE429_OPEN = 0x00000002;	// Leave the leg open
        public const uint OUTSTATE429_GROUND = 0x00000004;	// Short the leg to ground

    }	//end class
}	//end namespace
