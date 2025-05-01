using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq; // Required for args parsing
using Cargo_Door_Base.BTI;

namespace BTICardTestApp
{
    // Simple class to hold individual test results
    public class TestResult
    {
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int ErrorCode { get; set; }
    }

    // Simple class to hold device information
    public class DeviceInfo
    {
        public string CardType { get; set; } = string.Empty;
        public string CardProduct { get; set; } = string.Empty;
        public string DriverInfo { get; set; } = string.Empty;
    }

    // Class to encapsulate the overall outcome
    public class ExecutionOutcome
    {
        public DeviceInfo? DeviceInfo { get; set; }
        public List<TestResult> TestResults { get; set; } = new List<TestResult>();
        public string? ErrorMessage { get; set; }
        public bool Success { get; set; }
    }

    public class BTICardTest
    {
        private nint cardHandle;

        public BTICardTest()
        {
            cardHandle = IntPtr.Zero;
        }

        // Modified Initialize to return DeviceInfo or throw an exception
        public DeviceInfo Initialize()
        {
            // Open the first available BTI card (card number 0)
            nint handle = IntPtr.Zero; // Use nint for the handle
            int cardNum = 0; // Card number 0
            // Pass the handle as ref nint
            int result = BTICARD.BTICard_CardOpen(ref handle, cardNum);

            if (result != BTICARD.ERR_NONE || handle == IntPtr.Zero)
            {
                string errorDesc = BTICARD.BTICard_ErrDesc(result, handle); // Pass handle even if zero for potential context
                throw new Exception($"Failed to open BTI card (Error {result}: {errorDesc}). Ensure device is connected and drivers are installed.");
            }
            
            cardHandle = handle; // Assign the opened handle to the class member

            // Get card information
            string cardType = BTICARD.BTICard_CardTypeStr(cardHandle);
            string cardProduct = BTICARD.BTICard_CardProductStr(cardHandle);
            string driverInfo = BTI429.BTI429_DriverInfoStr();

            return new DeviceInfo
            {
                CardType = cardType,
                CardProduct = cardProduct,
                DriverInfo = driverInfo
            };
            // Removed Console.WriteLine statements
        }

        // Modified RunTests to return a List<TestResult> or throw an exception
        public List<TestResult> RunTests()
        {
            if (cardHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Card not initialized before running tests.");
            }

            var results = new List<TestResult>();
            int testResultCode;

            // Removed Console.WriteLine statement

            // Test Level 0 - I/O interface
            testResultCode = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_0, cardHandle);
            results.Add(CreateTestResult("I/O Interface Test", testResultCode));

            // Test Level 1 - Memory interface
            testResultCode = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_1, cardHandle);
            results.Add(CreateTestResult("Memory Interface Test", testResultCode));

            // Test Level 2 - Communication process
            testResultCode = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_2, cardHandle);
            results.Add(CreateTestResult("Communication Process Test", testResultCode));

            // Test Level 3 - Bus transceiver
            testResultCode = BTICARD.BTICard_CardTest(BTICARD.TEST_LEVEL_3, cardHandle);
            results.Add(CreateTestResult("Bus Transceiver Test", testResultCode));

            // Test protocol specifically for ARINC 429
            testResultCode = BTI429.BTI429_TestProtocol(cardHandle);
            results.Add(CreateTestResult("ARINC 429 Protocol Test", testResultCode));

            return results;
        }

        // Helper method to create TestResult object, replacing ReportTestResult's direct output
        private TestResult CreateTestResult(string testName, int resultCode)
        {
            var result = new TestResult
            {
                Name = testName,
                ErrorCode = resultCode
            };

            if (resultCode == BTICARD.ERR_NONE)
            {
                result.Status = "PASSED";
            }
            else
            {
                string errorDesc = BTICARD.BTICard_ErrDesc(resultCode, cardHandle);
                result.Status = $"FAILED - Error {resultCode} ({errorDesc})";
            }
            return result;
        }
        
        // ReportTestResult is no longer needed as results are returned

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
        // Main method now accepts args, handles actions, returns JSON
        static void Main(string[] args)
        {
            // Default action
            string action = "testCard"; 

            // Basic argument parsing: Look for --action <value>
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (args[i].Equals("--action", StringComparison.OrdinalIgnoreCase))
                {
                    action = args[i + 1];
                    break;
                }
            }

            var outcome = new ExecutionOutcome();
            BTICardTest tester = new BTICardTest();
            int exitCode = 0; // 0 for success, 1 for failure

            try
            {
                switch (action.ToLowerInvariant())
                {
                    case "testcard":
                        outcome.DeviceInfo = tester.Initialize();
                        outcome.TestResults = tester.RunTests();
                        // Check if any test failed
                        outcome.Success = outcome.TestResults.All(r => r.ErrorCode == BTICARD.ERR_NONE);
                        if (!outcome.Success)
                        {
                           // Optional: Add a general error message if needed, specific errors are in TestResults
                           // outcome.ErrorMessage = "One or more card tests failed."; 
                           exitCode = 1; // Indicate failure if any test fails
                        }
                        break;
                    
                    // Add cases for future actions like "testUsb" here
                    // case "testusb":
                    //    // Call USB testing methods
                    //    break;

                    default:
                        throw new ArgumentException($"Unknown action requested: {action}");
                }
            }
            catch (Exception ex)
            {
                outcome.Success = false;
                outcome.ErrorMessage = ex.Message; 
                // Optional: include stack trace in debug mode? ex.ToString()
                exitCode = 1; // Indicate failure
            }
            finally
            {
                tester.Cleanup();
            }

            // Serialize the outcome object to JSON
            var options = new JsonSerializerOptions { WriteIndented = false }; // Use WriteIndented = true for debug/readability if needed
            string jsonOutput = JsonSerializer.Serialize(outcome, options);

            // --- BEGIN ADDED CODE FOR DEBUGGING ---
            try 
            {
                // Write to the current working directory (usually the project root when run via execFile)
                string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "output.json");
                System.IO.File.WriteAllText(filePath, jsonOutput);
                // Optionally, log that the file was written (might interfere if console is source of issue)
                Console.Error.WriteLine($"Debug JSON written to: {filePath}"); 
            } 
            catch (Exception fileEx)
            {
                // Log error writing file, but continue to console output
                Console.Error.WriteLine($"Error writing debug JSON file: {fileEx.Message}");
            }
            // --- END ADDED CODE FOR DEBUGGING ---

            // Write the JSON to standard output (still needed for Electron)
            Console.WriteLine(jsonOutput);
            
            // Exit with the appropriate code
            Environment.Exit(exitCode); 
        }
    }
} 