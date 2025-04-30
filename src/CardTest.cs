using System;
using System.Threading;

namespace UA2430_Test
{
    public class CardTest
    {
        private IntPtr cardHandle;

        public CardTest()
        {
            cardHandle = IntPtr.Zero;
        }

        public bool Initialize()
        {
            int result = BTICARD.BTICard_CardOpen(ref cardHandle, 0); // Open first available card
            if (result != BTICARD.ERR_NONE)
            {
                Console.WriteLine($"Failed to open card. Error: {BTICARD.BTICard_ErrDesc(result, cardHandle)}");
                return false;
            }
            return true;
        }

        public void DisplayCardInfo()
        {
            int serialNumber = 0;
            int result = BTICARD.BTICard_GetSerialNumber(cardHandle, ref serialNumber);
            if (result == BTICARD.ERR_NONE)
            {
                Console.WriteLine($"Card Serial Number: {serialNumber}");
            }

            int driverVersion = 0;
            result = BTICARD.BTICard_GetDriverVersion(ref driverVersion);
            if (result == BTICARD.ERR_NONE)
            {
                Console.WriteLine($"Driver Version: {driverVersion >> 16}.{driverVersion & 0xFFFF}");
            }
        }

        public bool RunTests()
        {
            bool allTestsPassed = true;
            
            // Test Level 0 - Test I/O interface
            Console.WriteLine("\nRunning Test Level 0 - I/O Interface Test");
            int result = BTICARD.BTICard_CardTest0(cardHandle);
            if (result != BTICARD.ERR_NONE)
            {
                Console.WriteLine($"Test Level 0 Failed. Error: {BTICARD.BTICard_ErrDesc(result, cardHandle)}");
                allTestsPassed = false;
            }
            else
            {
                Console.WriteLine("Test Level 0 Passed");
            }

            // Test Level 1 - Test RAM
            Console.WriteLine("\nRunning Test Level 1 - RAM Test");
            result = BTICARD.BTICard_CardTest1(cardHandle);
            if (result != BTICARD.ERR_NONE)
            {
                Console.WriteLine($"Test Level 1 Failed. Error: {BTICARD.BTICard_ErrDesc(result, cardHandle)}");
                allTestsPassed = false;
            }
            else
            {
                Console.WriteLine("Test Level 1 Passed");
            }

            // Test Level 2 - Test ARINC 429 Channels
            Console.WriteLine("\nRunning Test Level 2 - ARINC 429 Channel Test");
            result = BTICARD.BTICard_CardTest2(cardHandle);
            if (result != BTICARD.ERR_NONE)
            {
                Console.WriteLine($"Test Level 2 Failed. Error: {BTICARD.BTICard_ErrDesc(result, cardHandle)}");
                allTestsPassed = false;
            }
            else
            {
                Console.WriteLine("Test Level 2 Passed");
            }

            return allTestsPassed;
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
} 