// Importing the necessary libraries
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace GoodNight
{
    class Program
    {
        // Getting the necessary functions from the ntdll.dll library (RtlAdjustPrivilege witch enables the SeShutdownPrivilege and NtRaiseHardError witch raises a blue screen)
        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        // Main function (marked as unsafe, as it uses pointers and raises a blue screen)
        static unsafe void Main(string[] args)
        {
            // Getting the arguments from the command line
            string arg1 = args.Length > 0 ? args[0] : null;
            string arg2 = args.Length > 1 ? args[1] : null;

            // Check if arg2 is 0 and replace it with 5, else if arg2 is 1 replace it with 6

            if (arg2 == "0")
            {
                arg2 = "5";
            }
            else if (arg2 == "1")
            {
                arg2 = "6";
            }
            // If arg2 isn't 0 or 1 then exit app with code 1
            else if (arg2 != "0" && arg2 != "1")
            {
                Environment.Exit(1);
            }

            if (arg1 == "-h" || arg1 == "--help")
            {
                // Display help message
                Console.WriteLine("Usage: GoodNight [error code] [response code]");
                Console.WriteLine("error code: A hexadecimal number representing the error code."
                    + "\nCheck the documentation for more information on Microsoft website"
                    + "\nhttps://learn.microsoft.com/en-us/windows-hardware/drivers/debugger/bug-check-code-reference2#bug-check-codes");
                Console.WriteLine("response code: 0 or 1 (0 = Stay after BSoD, 1 = Reboot after BSoD)");
                Environment.Exit(0);
            }

            if (arg1 == null || arg2 == null)
            {
                // Missing arguments, display error message
                Console.WriteLine("Missing arguments. Use -h or --help for usage information.");
                Environment.Exit(1);
            }

            if (Regex.IsMatch(arg1, @"\b0x[a-fA-F0-9]+\b"))
            {
                // arg1 contains a hexadecimal number, continue with the program
                Console.WriteLine("Your error code is: " + arg1
                    + "\nPassing the error code to the system\n");
            }
            else
            {
                // arg1 does not contain a hexadecimal number
                Console.WriteLine(
                    "Your error code must be a hexadecimal number\n" +
                    "Check the documentation for more information on Microsoft website\n" +
                    "https://learn.microsoft.com/en-us/windows-hardware/drivers/debugger/bug-check-code-reference2#bug-check-codes"
                    );
                Environment.Exit(1);
            }

            // Setting parameters for the NtRaiseHardError function
            uint arg2Value = Convert.ToUInt32(arg2);
            uint response;
            uint arg1Value = Convert.ToUInt32(arg1);

            // Raising the blue screen
            NtRaiseHardError(arg1Value, 0, 0, IntPtr.Zero, arg2Value, out response);
        }
    }
}