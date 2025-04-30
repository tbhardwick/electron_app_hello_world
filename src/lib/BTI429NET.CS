using System;
using System.Runtime.InteropServices;

namespace UA2430_Test
{
    public class BTI429
    {
        // DLL function imports for ARINC 429 functionality
        [DllImport(BTICARD.LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTI429_ConfigureChannel(IntPtr cardHandle, int channelNumber, int direction, int speed);

        [DllImport(BTICARD.LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTI429_StartChannel(IntPtr cardHandle, int channelNumber);

        [DllImport(BTICARD.LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTI429_StopChannel(IntPtr cardHandle, int channelNumber);

        [DllImport(BTICARD.LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTI429_TransmitMessage(IntPtr cardHandle, int channelNumber, uint label, uint data);

        [DllImport(BTICARD.LIBNAME64, CallingConvention = CallingConvention.Cdecl)]
        public static extern int BTI429_ReceiveMessage(IntPtr cardHandle, int channelNumber, out uint label, out uint data);

        // Constants
        public const int DIRECTION_TRANSMIT = 0;
        public const int DIRECTION_RECEIVE = 1;
        public const int SPEED_LOW = 0;  // 12.5 kHz
        public const int SPEED_HIGH = 1; // 100 kHz
    }
} 