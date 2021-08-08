using System;
using System.Diagnostics;
using System.Timers;

namespace process_monitor
{
    class Program
    {
            static Timer checker;
            static int life_time;
            static int check_time;
            static string program_name;

            static void Main(string[] args)
            {
                Console.WriteLine("Start");
                /*program_name = Console.ReadLine();
                Console.WriteLine("Program name setted");
                life_time = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Life time setted");
                check_time = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Check time setted");
                SetTimer(check_time);*/
                program_name = args[0];
                life_time = Convert.ToInt32(args[1]);
                SetTimer(Convert.ToInt32(args[2]));
            Console.ReadKey();
            }

            static void SetTimer(int check_time)
            {
                Console.WriteLine("Setting timer");
                checker = new Timer(check_time * 60000);
                checker.Elapsed += Check;
                checker.AutoReset = true;
            Console.WriteLine("Starting check: processes of {0} every {1} minute(s), life time: {2}", program_name, check_time, life_time);
            checker.Start();
            }

        static void Check(Object source, ElapsedEventArgs e)
        {
            Process[] processes = Process.GetProcessesByName(program_name);
            foreach (Process pr in processes)
            {
                if (pr.StartTime.Subtract(DateTime.Now).Minutes > life_time)
                {
                    pr.Kill();
                    Array.Clear(processes, Array.IndexOf(processes, pr), 1);

                }
                Console.WriteLine("Process {0} was killed", program_name);
            }
            if (processes.Length == 0)
            {
                checker.Stop();
                Console.WriteLine("All processes were killed");
            }
        }
        
    }
}
