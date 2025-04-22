using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OOP_Lab15
{
    static partial class TPL
    {
        public static void Task5()
        {
            Console.WriteLine("Parallel выаод");
            int size = 3000;
            int[] arr = new int[size];
            Random rand = new Random();

            for (int i = 0; i < size; i++)
            {
                arr[i] = rand.Next(-100000, 100000);
            }

            int[] temp = new int[size];
            arr.CopyTo(temp, 0);
            Stopwatch sw = new Stopwatch();

            sw.Start();
            TPL.Sort(arr);
            sw.Stop();
            Console.WriteLine("Результат пятого задания обычными циклами: " + sw.ElapsedMilliseconds + " мс");

            sw.Reset();
            temp.CopyTo(arr, 0);
            sw.Start();
            var sortedArr = arr.AsParallel().OrderBy(x => x).ToArray();
            sw.Stop();
            Console.WriteLine("Время выполнения параллельного алгоритма(PLINQ): " + sw.ElapsedMilliseconds + " мс");
        }

        public static T[] Sort<T>(T[] arr) where T : IComparable
        {
            bool isSorted = false;

            while (!isSorted)
            {
                isSorted = true;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i].CompareTo(arr[i + 1]) > 0)
                    {
                        T temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        isSorted = false;
                    }
                }
            }

            return arr;
        }
    }
}