using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Security;
using System.Security.Principal;

namespace BTICardTest
{
    // Remove the BTICard class wrapper again
    // Assume definitions are provided by an included file (e.g., from native-libs)

    class Program
    {
        // Constants needed by Program.Main
        // Test levels
        public const int TEST_LEVEL_0 = 0;
        public const int TEST_LEVEL_1 = 1;
        public const int TEST_LEVEL_2 = 2;
        public const int TEST_LEVEL_3 = 3;

        // Error codes
        public const int ERR_NONE = 0;
        // Define other error codes used in Main if needed

        // Channel configuration
        public const uint CHCFG429_DEFAULT = 0x00000000;
        public const uint CHCFG429_HIGHSPEED = 0x00000001;
        public const uint CHCFG429_LOWSPEED = 0x00000000;

        // --- P/Invoke signatures are assumed to be defined elsewhere --- 
        // --- and included via the .csproj file.                     ---
        // --- We will add them back here ONLY if the build fails     ---
        // --- due to missing definitions.                            ---

        // Example (signatures would be needed if not included via .csproj):
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int BTICard_CardOpen(out IntPtr hCard, int cardnum);
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int BTICard_CoreOpen(out IntPtr hCore, int corenum, IntPtr hCard);
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int BTICard_CardClose(IntPtr hCard);
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int BTICard_CardTest(int testlevel, IntPtr hCore);
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern void BTICard_CardReset(IntPtr hCore);
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "BTICard_CardTypeStr")]
         private static extern IntPtr Native_CardTypeStr(IntPtr hCore);
         public static string BTICard_CardTypeStr(IntPtr hCore) { return Marshal.PtrToStringAnsi(Native_CardTypeStr(hCore)); }
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "BTICard_CardProductStr")]
         private static extern IntPtr Native_CardProductStr(IntPtr hCore);
         public static string BTICard_CardProductStr(IntPtr hCore) { return Marshal.PtrToStringAnsi(Native_CardProductStr(hCore)); }
         [DllImport("BTICARD64.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "BTICard_ErrDescStr")]
         private static extern IntPtr Native_ErrDescStr(int errval, IntPtr hCore);
         public static string BTICard_ErrDescStr(int errval, IntPtr hCore) { return Marshal.PtrToStringAnsi(Native_ErrDescStr(errval, hCore)); }
         [DllImport("BTI42964.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int BTI429_TestProtocol(IntPtr hCore);
         [DllImport("BTI42964.dll", CallingConvention = CallingConvention.StdCall)]
         public static extern int BTI429_ChConfig(uint configval, int channum, IntPtr hCore);
         [DllImport("BTI42964.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "BTI429_DriverInfoStr")]
         private static extern IntPtr Native_DriverInfoStr();
         public static string BTI429_DriverInfoStr() { return Marshal.PtrToStringAnsi(Native_DriverInfoStr()); }

        // Kernel32 DLL imports
        [DllImport("kernel32.dll")]
        public static extern int GetLastError();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string libname);

        // IsAdministrator remains in Program class
        static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        // Verify/Load DLLs remain in Program class for now
        public static bool VerifyDllsExist()
        {
            string[] requiredDlls = { "BTICARD64.dll", "BTI42964.dll" }; // Use local constants or strings
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
                    if (!TryLoadDll(appDllPath)) { allExist = false; }
                }
                else if (File.Exists(sysDllPath))
                {
                    Console.WriteLine($"Found DLL in system directory: {dll}");
                    if (!TryLoadDll(sysDllPath)) { allExist = false; }
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
                // Verify DLLs
                Console.WriteLine("Verifying BTI Card DLLs...");
                if (!VerifyDllsExist())
                {
                    Console.WriteLine("One or more required DLLs are missing or cannot be loaded.");
                    return;
                }
                Console.WriteLine("All required DLLs verified.\n");

                // Open Card and Core (Calling directly)
                Console.WriteLine("Opening Card 0, Core 0...");
                int cardNum = 0;
                int coreNum = 0;
                int result = BTICard_CardOpen(out hCard, cardNum);
                if (result != ERR_NONE)
                {
                    Console.WriteLine($"Failed to open card {cardNum}. Error {result} ({BTICard_ErrDescStr(result, IntPtr.Zero)})");
                    return;
                }
                Console.WriteLine("Card opened successfully.");

                result = BTICard_CoreOpen(out hCore, coreNum, hCard);
                if (result != ERR_NONE)
                {
                    Console.WriteLine($"Failed to open core {coreNum} on card {cardNum}. Error {result} ({BTICard_ErrDescStr(result, hCard)})");
                    return;
                }
                Console.WriteLine("Core opened successfully.\n");

                // Reset Core (Commented out based on previous findings)
                Console.WriteLine("Resetting Core... (Commented out for testing)");
                // BTICard_CardReset(hCore);
                // Console.WriteLine("Core reset.\n");

                // Get driver information
                string driverInfo = BTI429_DriverInfoStr();
                Console.WriteLine($"Driver Info: {driverInfo}\n");

                // Get Card Type and Product
                string cardType = BTICard_CardTypeStr(hCore);
                string productStr = BTICard_CardProductStr(hCore);
                Console.WriteLine($"Card Type: {cardType}");
                Console.WriteLine($"Product: {productStr}\n");

                // Run tests
                Console.WriteLine("Running card self-tests...\n");

                /*
                // Configure channel 0 (Commented out)
                Console.WriteLine("Configuring channel 0 (Example - may not be required for CardTest)...");
                int configResult = BTI429_ChConfig(CHCFG429_LOWSPEED, 0, hCore);
                if (configResult != ERR_NONE)
                {
                    Console.WriteLine($"Low speed config failed: {BTICard_ErrDescStr(configResult, hCore)}. Trying high speed...");
                    configResult = BTI429_ChConfig(CHCFG429_HIGHSPEED, 0, hCore);
                    if (configResult != ERR_NONE)
                    {
                         Console.WriteLine($"High speed config failed: {BTICard_ErrDescStr(configResult, hCore)}");
                    }
                }
                if (configResult == ERR_NONE) Console.WriteLine("Channel 0 configured.");
                */

                // Test Level 0
                Console.WriteLine("\nRunning I/O Interface Test (Level 0)...");
                result = BTICard_CardTest(TEST_LEVEL_0, hCore);
                ReportTestResult("I/O Interface Test", result, hCore);

                // Test Level 1
                Console.WriteLine("\nRunning Memory Interface Test (Level 1)...");
                result = BTICard_CardTest(TEST_LEVEL_1, hCore);
                ReportTestResult("Memory Interface Test", result, hCore);

                // Test Level 2
                Console.WriteLine("\nRunning Communication Process Test (Level 2)...");
                result = BTICard_CardTest(TEST_LEVEL_2, hCore);
                ReportTestResult("Communication Process Test", result, hCore);

                // Test Level 3
                Console.WriteLine("\nRunning Bus Transceiver Test (Level 3)...");
                Console.WriteLine("Warning: Level 3 test may interfere if connected to an active bus.");
                result = BTICard_CardTest(TEST_LEVEL_3, hCore);
                ReportTestResult("Bus Transceiver Test", result, hCore);

                // Test ARINC 429 protocol
                Console.WriteLine("\nRunning ARINC 429 Protocol Test...");
                result = BTI429_TestProtocol(hCore);
                ReportTestResult("ARINC 429 Protocol Test", result, hCore);
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
                    BTICard_CardClose(hCard);
                    Console.WriteLine("Card closed.");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            // Console.ReadKey(); // Commented out: Not needed when run programmatically
        }

        // ReportTestResult needs access to BTICard_ErrDescStr
        static void ReportTestResult(string testName, int result, IntPtr hCore)
        {
            if (result == ERR_NONE)
            {
                Console.WriteLine($"{testName}: PASSED");
            }
            else
            {
                // Assuming BTICard_ErrDescStr is available (defined above or included via .csproj)
                string errorDesc = BTICard_ErrDescStr(result, hCore != IntPtr.Zero ? hCore : IntPtr.Zero);
                Console.WriteLine($"{testName}: FAILED - Error {result} ({errorDesc})");
            }
        }
    }
}
