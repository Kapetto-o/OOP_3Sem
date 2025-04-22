using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP_Lab15
{
    static partial class TPL
    {
        public static void Task1(int N)
        {
            Console.WriteLine("Поиск простых чисел");
            Stopwatch stw = new Stopwatch();

            Task task = new Task(() => FindSimpleNums(N, stw));

            Console.WriteLine($"Статус до запуска: {task.Status}");
            stw.Start();
            task.Start();
            Console.WriteLine($"Статус после запуска: {task.Status}");
        }

        public static void Task2(int N)
        {

            Console.WriteLine("Реалезация отмены задачи");
            CancellationTokenSource cts = new CancellationTokenSource();
            var token = cts.Token;

            Task task = new Task(() => FindSimpleNums(N, ref token), token);

            Console.WriteLine($"Статус до запуска: {task.Status}");

            task.Start();

            Console.WriteLine($"Статус после запуска: {task.Status}");

            cts.Cancel();

            Thread.Sleep(3000);

            Console.WriteLine($"Статус после отмены: {task.Status}");
        }

        private static List<int> FindSimpleNums(int N, Stopwatch stw = null)
        {
            List<int> simpleNums = new List<int>();

            for (int i = 2; i < N; i++)
            {
                bool isSimple = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isSimple = false;
                        break;
                    }
                }

                if (isSimple)
                {
                    simpleNums.Add(i);
                }
            }

            foreach (var num in simpleNums)
            {
                Console.Write($"{num}\n");
            }

            if (stw != null)
            {
                stw.Stop();
                Console.WriteLine($"Время выполнения: {stw.Elapsed}");
            }

            Console.WriteLine("Выполнено!");

            return simpleNums;
        }

        private static List<int> FindSimpleNums(int N, ref CancellationToken token)
        {
            List<int> simpleNums = new List<int>();

            for (int i = 2; i < N; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Задача отменена");
                    return null;
                }

                bool isSimple = true;

                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        isSimple = false;
                        break;
                    }
                }

                if (isSimple)
                {
                    simpleNums.Add(i);
                }
            }

            Console.WriteLine("Выполнено!");

            return simpleNums;
        }
    }
}