using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace GoodNight
{
    class Program
    {
        [DllImport("ntdll.dll")]
        public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);

        [DllImport("ntdll.dll")]
        public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);

        static unsafe void Main(string[] args)
        {
            Console.Title = "[ОПАСНО!] - NSINC BSoD Invoker";
            Console.WriteLine("Это пример использования функции вызова критической ошибки в ядре Windows NT 6.0+, \nвызывающей синий экран смерти (BSoD)");
            Console.WriteLine("ВНИМАНИЕ! Ваши несохраненные данные будут утеряны. Перед запуском сохраните всю свою работу. \nЗапускайте на свой страх и риск!");
            Console.Write("\nВы уверены что хотите запустить код? (Y/N): ");
            ConsoleKeyInfo cki = Console.ReadKey();

            if (cki.Key.ToString() is "y" or "Y")
            {
                Console.WriteLine(" ");
                Console.WriteLine("Запускаю код через 3 секунды...");
                Thread.Sleep(3000);
                Console.WriteLine("Bye-bye!");
                Boolean t1;
                uint t2;
                RtlAdjustPrivilege(19, true, false, out t1);
                NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out t2); //Здесь в первом аргументе может быть любое значение
            }
            else
            {
                if (cki.Key.ToString() is "n" or "N")
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("Хорошо, закрываюсь через 3 секунды!");
                    Thread.Sleep(3000);
                    Environment.Exit(1);
                }
            }
            Thread.Sleep(5000);
        }
    }
}