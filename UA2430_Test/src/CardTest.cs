using System;
using Cargo_Door_Base.BTI;

namespace UA2430_Test
{
    public class CardTest
    {
        private nint cardHandle;

        public CardTest()
        {
            cardHandle = nint.Zero;
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

            // Test Level 1 - Test memory interface
            Console.WriteLine("\nRunning Test Level 1 - Memory Interface Test");
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

            // Test Level 2 - Test communication process
            Console.WriteLine("\nRunning Test Level 2 - Communication Process Test");
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

            // Test Level 3 - Test bus transceiver
            Console.WriteLine("\nRunning Test Level 3 - Bus Transceiver Test");
            result = BTICARD.BTICard_CardTest3(cardHandle);
            if (result != BTICARD.ERR_NONE)
            {
                Console.WriteLine($"Test Level 3 Failed. Error: {BTICARD.BTICard_ErrDesc(result, cardHandle)}");
                allTestsPassed = false;
            }
            else
            {
                Console.WriteLine("Test Level 3 Passed");
            }

            return allTestsPassed;
        }

        public void DisplayCardInfo()
        {
            Console.WriteLine("\nCard Information:");
            Console.WriteLine($"Driver Info: {BTICARD.BTICard_DriverInfoStr()}");
            Console.WriteLine($"Card Type: {BTICARD.BTICard_CardTypeStr(cardHandle)}");
            Console.WriteLine($"Product: {BTICARD.BTICard_CardProductStr(cardHandle)}");
            
            // Get channel information
            int rcvCount = 0, xmtCount = 0;
            BTICARD.BTICard_ChGetCount(ref rcvCount, ref xmtCount, cardHandle);
            Console.WriteLine($"Receive Channels: {rcvCount}");
            Console.WriteLine($"Transmit Channels: {xmtCount}");
        }

        public void Cleanup()
        {
            if (cardHandle != nint.Zero)
            {
                BTICARD.BTICard_CardClose(cardHandle);
                cardHandle = nint.Zero;
            }
        }
    }
} 