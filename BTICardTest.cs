using System;
using Cargo_Door_Base.BTI;

namespace BTICardTestApp
{
    public class BTICardTest
    {
        private nint cardHandle;

        public BTICardTest()
        {
            cardHandle = IntPtr.Zero;
        }

        public bool Initialize()
        {
            try
            {
                // Open the first available BTI card (card number 0)
                cardHandle = BTICARD.BTICard_CardOpen(0, 0);
                
                if (cardHandle == IntPtr.Zero)
                {
                    Console.WriteLine("Failed to open BTI card");
                    return false;
                }

                // Get card information
                string cardType = BTICARD.BTICard_CardTypeStr(cardHandle);
                string cardProduct = BTICARD.BTICard_CardProductStr(cardHandle);
                string driverInfo = BTI429.BTI429_DriverInfoStr();

                Console.WriteLine($"Card Type: {cardType}");
                Console.WriteLine($"Card Product: {cardProduct}");
                Console.WriteLine($"Driver Info: {driverInfo}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing BTI card: {ex.Message}");
                return false;
            }
        }

        public bool RunTests()
        {
            try
            {
                if (cardHandle == IntPtr.Zero)
                {
                    Console.WriteLine("Card not initialized");
                    return false;
                }

                // Run card self-test
                Console.WriteLine("\nRunning card self-test...");
                
                // Test Level 0 - I/O interface
                int testResult = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_0, cardHandle);
                ReportTestResult("I/O Interface Test", testResult);

                // Test Level 1 - Memory interface
                testResult = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_1, cardHandle);
                ReportTestResult("Memory Interface Test", testResult);

                // Test Level 2 - Communication process
                testResult = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_2, cardHandle);
                ReportTestResult("Communication Process Test", testResult);

                // Test Level 3 - Bus transceiver
                testResult = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_3, cardHandle);
                ReportTestResult("Bus Transceiver Test", testResult);

                // Test protocol specifically for ARINC 429
                testResult = BTI429.BTI429_TestProtocol(cardHandle);
                ReportTestResult("ARINC 429 Protocol Test", testResult);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running tests: {ex.Message}");
                return false;
            }
        }

        private void ReportTestResult(string testName, int result)
        {
            if (result == BTICARD.ERR_NONE)
            {
                Console.WriteLine($"{testName}: PASSED");
            }
            else
            {
                string errorDesc = BTICARD.BTICard_ErrDesc(result, cardHandle);
                Console.WriteLine($"{testName}: FAILED - Error {result} ({errorDesc})");
            }
        }

        public void Cleanup()
        {
            if (cardHandle != IntPtr.Zero)
            {
                BTICARD.BTICard_CardClose(cardHandle);
                cardHandle = IntPtr.Zero;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BTI Card Test Application");
            Console.WriteLine("========================\n");

            BTICardTest tester = new BTICardTest();

            if (tester.Initialize())
            {
                tester.RunTests();
            }

            tester.Cleanup();

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
} 