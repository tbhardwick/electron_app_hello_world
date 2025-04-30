using System;
using System.Runtime.InteropServices;

namespace UA2430_Test
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

        // Error codes
        public const int ERR_NONE = 0;
        public const int ERR_CARD_NOT_FOUND = -1;
        public const int ERR_CARD_IN_USE = -2;
        public const int ERR_INVALID_HANDLE = -3;
        public const int ERR_INVALID_PARAMETER = -4;
        public const int ERR_TIMEOUT = -5;
        public const int ERR_BUFFER_TOO_SMALL = -6;
        public const int ERR_HARDWARE_FAILURE = -7;
        public const int ERR_DRIVER_FAILURE = -8;
        public const int ERR_FIRMWARE_FAILURE = -9;
        public const int ERR_MEMORY_ALLOCATION = -10;
        public const int ERR_NO_RESOURCES = -11;
        public const int ERR_FEATURE_NOT_SUPPORTED = -12;
        public const int ERR_OPERATION_CANCELLED = -13;
        public const int ERR_OPERATION_PENDING = -14;
        public const int ERR_BUFFER_EMPTY = -15;
        public const int ERR_BUFFER_FULL = -16;
        public const int ERR_BUFFER_OVERFLOW = -17;
        public const int ERR_INVALID_STATE = -18;
        public const int ERR_BAD_CRC = -19;
        public const int ERR_DATA_ERROR = -20;
        public const int ERR_NO_POWER = -21;

        // Card test levels
        public const int TEST_LEVEL_0 = 0;  // Test I/O interface
        public const int TEST_LEVEL_1 = 1;  // Test basic functionality
        public const int TEST_LEVEL_2 = 2;  // Test extended functionality

        // DLL function imports
        [DllImport(LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTICard_CardOpen(ref IntPtr cardHandle, int cardNumber);

        [DllImport(LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTICard_CardClose(IntPtr cardHandle);

        [DllImport(LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTICard_CardTest0(IntPtr cardHandle);

        [DllImport(LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTICard_CardTest1(IntPtr cardHandle);

        [DllImport(LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTICard_CardTest2(IntPtr cardHandle);

        [DllImport(LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr BTICard_ErrDesc(int errorCode, IntPtr cardHandle);

        [DllImport(LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTICard_GetCardInfo(IntPtr cardHandle, out int serialNumber, out int firmwareVersion, out int driverVersion);

        [DllImport(LIBNAME64, EntryPoint = "_BTICard_CardTypeStr@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr _BTICard_CardTypeStr(IntPtr handleval);
        public static string BTICard_CardTypeStr(IntPtr handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardTypeStr(handleval)); }

        [DllImport(LIBNAME64, EntryPoint = "_BTICard_CardProductStr@4", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr _BTICard_CardProductStr(IntPtr handleval);
        public static string BTICard_CardProductStr(IntPtr handleval) { return Marshal.PtrToStringAnsi(_BTICard_CardProductStr(handleval)); }

        [DllImport(LIBNAME64, EntryPoint = "_BTICard_DriverInfoStr@0", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr _BTICard_DriverInfoStr();
        public static string BTICard_DriverInfoStr() { return Marshal.PtrToStringAnsi(_BTICard_DriverInfoStr()); }

        [DllImport(LIBNAME64, EntryPoint = "_BTICard_ChGetCount@12", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void BTICard_ChGetCount(ref int rcvcount, ref int xmtcount, IntPtr handleval);
    }
} 