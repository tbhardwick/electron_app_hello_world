using System;

namespace UA2430_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UA2430 Card Test Application");
            Console.WriteLine("===========================");

            CardTest cardTest = new CardTest();

            try
            {
                // Initialize the card
                if (!cardTest.Initialize())
                {
                    Console.WriteLine("Failed to initialize the UA2430 card. Exiting...");
                    return;
                }

                // Display card information
                cardTest.DisplayCardInfo();

                // Run the tests
                Console.WriteLine("\nStarting Card Tests...");
                bool testsPassed = cardTest.RunTests();

                // Display final results
                Console.WriteLine("\nTest Results Summary");
                Console.WriteLine("===================");
                Console.WriteLine(testsPassed ? "All tests PASSED" : "Some tests FAILED");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
            finally
            {
                // Always cleanup
                cardTest.Cleanup();
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
} 