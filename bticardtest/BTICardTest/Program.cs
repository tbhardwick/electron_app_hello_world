using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Security;
using System.Security.Principal;

namespace BTICardTest
{
    class Program
    {
        // Constants moved from BTICard class
        private const string BTICARD_DLL = "BTICARD64.dll";
        private const string BTI429_DLL = "BTI42964.dll";

        // Test levels
        public const int TEST_LEVEL_0 = 0;  // Test I/O interface
        public const int TEST_LEVEL_1 = 1;  // Test memory interface
        public const int TEST_LEVEL_2 = 2;  // Test communication process
        public const int TEST_LEVEL_3 = 3;  // Test bus transceiver

        // Error codes
        public const int ERR_NONE = 0;
        public const int ERR_NOCARD = -13;  // CardOpen() could not find a card at the specified address
        public const int ERR_NOIO = -14;    // CardOpen() could not find the I/O ports
        public const int ERR_NOMEM = -15;   // CardOpen() could not find the memory
        public const int ERR_BADHANDLE = -21; // Bad handle specified
        public const int ERR_RCVRNOTCONFIG = -25; // Receiver not configured

        // Channel configuration
        public const uint CHCFG429_DEFAULT = 0x00000000;    // Default settings
        public const uint CHCFG429_HIGHSPEED = 0x00000001;  // High speed (100kHz)
        public const uint CHCFG429_LOWSPEED = 0x00000000;   // Low speed (12.5kHz)
        public const uint CHCFG429_PARODD = 0x00000000;     // Odd parity
        public const uint CHCFG429_PAREVEN = 0x00000010;    // Even parity

        // BTICard DLL imports moved into Program class
        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int BTICard_CardOpen(out IntPtr hCard, int cardnum);

        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int BTICard_CoreOpen(out IntPtr hCore, int corenum, IntPtr hCard);

        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int BTICard_CardClose(IntPtr hCard);

        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int BTICard_CardTest(int testlevel, IntPtr hCore);

        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern void BTICard_CardReset(IntPtr hCore);

        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "BTICard_CardTypeStr")]
        private static extern IntPtr Native_CardTypeStr(IntPtr hCore);
        // Helper moved into Program class
        public static string BTICard_CardTypeStr(IntPtr hCore) { return Marshal.PtrToStringAnsi(Native_CardTypeStr(hCore)); }

        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "BTICard_CardProductStr")]
        private static extern IntPtr Native_CardProductStr(IntPtr hCore);
        // Helper moved into Program class
        public static string BTICard_CardProductStr(IntPtr hCore) { return Marshal.PtrToStringAnsi(Native_CardProductStr(hCore)); }

        [DllImport(BTICARD_DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "BTICard_ErrDescStr")]
        private static extern IntPtr Native_ErrDescStr(int errval, IntPtr hCore);
        // Helper moved into Program class
        public static string BTICard_ErrDescStr(int errval, IntPtr hCore) { return Marshal.PtrToStringAnsi(Native_ErrDescStr(errval, hCore)); }

        // BTI429 DLL imports moved into Program class
        [DllImport(BTI429_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int BTI429_TestProtocol(IntPtr hCore);

        [DllImport(BTI429_DLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int BTI429_ChConfig(uint configval, int channum, IntPtr hCore);

        [DllImport(BTI429_DLL, CallingConvention = CallingConvention.StdCall, EntryPoint = "BTI429_DriverInfoStr")]
        private static extern IntPtr Native_DriverInfoStr();
        // Helper moved into Program class
        public static string BTI429_DriverInfoStr() { return Marshal.PtrToStringAnsi(Native_DriverInfoStr()); }

        // Kernel32 DLL imports moved into Program class
        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);

        // VerifyDllsExist moved into Program class
        public static bool VerifyDllsExist()
        {
            string[] requiredDlls = { BTICARD_DLL, BTI429_DLL };
            bool allExist = true;

            string systemDir = Environment.SystemDirectory;
            string appDir = AppDomain.CurrentDomain.BaseDirectory;

            foreach (string dll in requiredDlls)
            {
                string appDllPath = Path.Combine(appDir, dll);
                string sysDllPath = Path.Combine(systemDir, dll);

                if (File.Exists(appDllPath))
                {
                    Console.WriteLine($"Found DLL in application directory: {dll}");
                    if (!TryLoadDll(appDllPath))
                    {
                        allExist = false;
                    }
                }
                else if (File.Exists(sysDllPath))
                {
                    Console.WriteLine($"Found DLL in system directory: {dll}");
                    if (!TryLoadDll(sysDllPath))
                    {
                        allExist = false;
                    }
                }
                else
                {
                    Console.WriteLine($"DLL not found: {dll}");
                    Console.WriteLine($"Checked locations:");
                    Console.WriteLine($"  - {appDllPath}");
                    Console.WriteLine($"  - {sysDllPath}");
                    allExist = false;
                }
            }

            return allExist;
        }

        // TryLoadDll moved into Program class
        private static bool TryLoadDll(string dllPath)
        {
            IntPtr dllHandle = LoadLibrary(dllPath);
            if (dllHandle == IntPtr.Zero)
            {
                int error = GetLastError();
                Console.WriteLine($"Failed to load {Path.GetFileName(dllPath)}. Error code: {error}");
                return false;
            }
            else
            {
                Console.WriteLine($"Successfully loaded {Path.GetFileName(dllPath)}");
                return true;
            }
        }

        // IsAdministrator moved into Program class
        static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("BTI Card Test Application");
            Console.WriteLine("========================\n");

            // Check if running as administrator
            if (!IsAdministrator())
            {
                Console.WriteLine("Warning: Application is not running with administrator privileges.");
                Console.WriteLine("Some functionality may not work correctly.\n");
            }

            IntPtr hCard = IntPtr.Zero;
            IntPtr hCore = IntPtr.Zero;

            try
            {
                // Verify DLLs (Calling method within the same class)
                Console.WriteLine("Verifying BTI Card DLLs...");
                if (!VerifyDllsExist()) // No BTICard. prefix
                {
                    Console.WriteLine("One or more required DLLs are missing or cannot be loaded.");
                    return;
                }
                Console.WriteLine("All required DLLs verified.\n");

                // Open Card and Core (Calling method within the same class)
                Console.WriteLine("Opening Card 0, Core 0...");
                int cardNum = 0;
                int coreNum = 0;
                int result = BTICard_CardOpen(out hCard, cardNum); // No BTICard. prefix
                if (result != ERR_NONE) // Use constant defined in Program class
                {
                    Console.WriteLine($"Failed to open card {cardNum}. Error {result} ({BTICard_ErrDescStr(result, IntPtr.Zero)})"); // No BTICard. prefix
                    return;
                }
                Console.WriteLine("Card opened successfully.");

                result = BTICard_CoreOpen(out hCore, coreNum, hCard); // No BTICard. prefix
                if (result != ERR_NONE)
                {
                    Console.WriteLine($"Failed to open core {coreNum} on card {cardNum}. Error {result} ({BTICard_ErrDescStr(result, hCard)})"); // No BTICard. prefix
                    return;
                }
                Console.WriteLine("Core opened successfully.\n");

                // Reset Core (Good practice before configuration/testing)
                Console.WriteLine("Resetting Core... (Commented out for testing)");
                // BTICard_CardReset(hCore); // Let's see if CardTest requires the core *not* be explicitly reset first
                // Console.WriteLine("Core reset.\n");

                // Get driver information (Calling method within the same class)
                string driverInfo = BTI429_DriverInfoStr(); // No BTICard. prefix
                Console.WriteLine($"Driver Info: {driverInfo}\n");

                // Get Card Type and Product (Calling method within the same class)
                string cardType = BTICard_CardTypeStr(hCore); // No BTICard. prefix
                string productStr = BTICard_CardProductStr(hCore); // No BTICard. prefix
                Console.WriteLine($"Card Type: {cardType}");
                Console.WriteLine($"Product: {productStr}\n");

                // Run tests
                Console.WriteLine("Running card self-tests...\n");

                /* 
                // Configure channel 0 (Example - may not be required for CardTest)...
                // Let's comment this out to see if ChConfig interferes with CardTest
                Console.WriteLine("Configuring channel 0 (Example - may not be required for CardTest)...");
                int configResult = BTI429_ChConfig(CHCFG429_LOWSPEED, 0, hCore); // No BTICard. prefix
                if (configResult != ERR_NONE)
                {
                    Console.WriteLine($"Low speed config failed: {BTICard_ErrDescStr(configResult, hCore)}. Trying high speed..."); // No BTICard. prefix
                    configResult = BTI429_ChConfig(CHCFG429_HIGHSPEED, 0, hCore); // No BTICard. prefix
                    if (configResult != ERR_NONE)
                    {
                         Console.WriteLine($"High speed config failed: {BTICard_ErrDescStr(configResult, hCore)}"); // No BTICard. prefix
                    }
                }
                if (configResult == ERR_NONE) Console.WriteLine("Channel 0 configured.");
                */

                // Test Level 0 (Calling method within the same class)
                Console.WriteLine("\nRunning I/O Interface Test (Level 0)...");
                result = BTICard_CardTest(TEST_LEVEL_0, hCore); // No BTICard. prefix
                ReportTestResult("I/O Interface Test", result, hCore);

                /* // Commenting out higher levels for now
                // Test Level 1 (Calling method within the same class)
                Console.WriteLine("\nRunning Memory Interface Test (Level 1)...");
                result = BTICard_CardTest(TEST_LEVEL_1, hCore); // No BTICard. prefix
                ReportTestResult("Memory Interface Test", result, hCore);

                // Test Level 2 (Calling method within the same class)
                Console.WriteLine("\nRunning Communication Process Test (Level 2)...");
                result = BTICard_CardTest(TEST_LEVEL_2, hCore); // No BTICard. prefix
                ReportTestResult("Communication Process Test", result, hCore);

                // Test Level 3 (Calling method within the same class)
                Console.WriteLine("\nRunning Bus Transceiver Test (Level 3)...");
                Console.WriteLine("Warning: Level 3 test may interfere if connected to an active bus.");
                result = BTICard_CardTest(TEST_LEVEL_3, hCore); // No BTICard. prefix
                ReportTestResult("Bus Transceiver Test", result, hCore);

                // Test ARINC 429 protocol (Calling method within the same class)
                Console.WriteLine("\nRunning ARINC 429 Protocol Test...");
                result = BTI429_TestProtocol(hCore); // No BTICard. prefix
                ReportTestResult("ARINC 429 Protocol Test", result, hCore);
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                if (ex is DllNotFoundException)
                {
                    Console.WriteLine("Make sure the BTI Card driver DLLs are in the system path (C:\\Windows\\System32) or application directory.");
                }
                else if (ex is SecurityException)
                {
                    Console.WriteLine("Access denied. Make sure you are running the application with appropriate permissions (Administrator?).");
                }
                else
                {
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
            }
            finally
            {
                // Ensure card is closed even if errors occur
                if (hCard != IntPtr.Zero)
                {
                    Console.WriteLine("\nClosing card...");
                    BTICard_CardClose(hCard); // No BTICard. prefix
                    Console.WriteLine("Card closed.");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // ReportTestResult now calls BTICard_ErrDescStr defined in the same class
        static void ReportTestResult(string testName, int result, IntPtr hCore)
        {
            if (result == ERR_NONE)
            {
                Console.WriteLine($"{testName}: PASSED");
            }
            else
            {
                string errorDesc = BTICard_ErrDescStr(result, hCore != IntPtr.Zero ? hCore : IntPtr.Zero);
                Console.WriteLine($"{testName}: FAILED - Error {result} ({errorDesc})");
            }
        }
    }
}
