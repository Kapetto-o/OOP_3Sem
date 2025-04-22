using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace OOP_Lab14
{
    #region Task5
    public class TimerExample
    {
        private Timer timer;

        public TimerExample()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            timer = new Timer(2000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;

            Console.WriteLine("Timer started. Will raise event every 2 seconds.");
        }

        public void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                Console.WriteLine("Timer stopped and disposed.");
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"The Elapsed event was raised at {e.SignalTime}");
        }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Запущенные процессы\n");
            Info.ShowProcesses();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Текущий длмен\n");
            Info.ShowDomains();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Вывод простых чисел\n");
            Info.ShowSimpleNumbers();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Вывод чётных и не чётгых чисел\n");
            Info.ThoThreads();
            Console.WriteLine("\n---------------------------");
            TimerExample timerExample = new TimerExample();

            Console.WriteLine("Press [Enter] to exit the program...");
            Console.ReadLine();

            timerExample.StopTimer();
        }
    }
}