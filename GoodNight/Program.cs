//Importing the necessary libraries
using System;
using System.Runtime.InteropServices;

namespace GoodNight
{
    class Program
    {
        //Getting the necessary functions from the ntdll.dll library (RtlAdjustPrivilege witch enables the SeShutdownPrivilege and NtRaiseHardError witch raises a blue screen)
        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        //Main function (marked as unsafe, as it uses pointers and raises a blue screen)
        static unsafe void Main(string[] args)
        {
            //Enabling the SeShutdownPrivilege
            bool previous;
            RtlAdjustPrivilege(19, true, false, out previous);

            //Raising a blue screen
            uint response;
            NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out response);
        }
    }
}