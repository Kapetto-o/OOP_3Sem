using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Lab15
{
    static partial class TPL
    {
        public static int Sum(params int[] nums) => nums.Sum();
        public static double GetSquareOfCyrcle(int radius) => Math.PI * radius * radius;

        public static double GetRadius(double square) => Math.Sqrt(square / Math.PI);

        public static void Task4_1()
        {
            Console.WriteLine("Использование ContinueWith");

            var task1 = Task.Run(() => Sum(1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
            var task2 = task1.ContinueWith(n => GetSquareOfCyrcle(n.Result));
            var task3 = task2.ContinueWith(square => GetRadius(square.Result));

            Console.WriteLine("Результат ContinueWith: " + task3.Result);
        }

        public static void Task4_2()
        {

            Console.WriteLine("GetAwaiter и GetResult");

            Task<int> task1 = new Task<int>(() => Sum(12, 232, 232));
            task1.Start();

            task1.GetAwaiter().GetResult();

            Task<double> task2 = new Task<double>(() => GetSquareOfCyrcle(task1.Result));
            task2.Start();

            task2.GetAwaiter().GetResult();

            Task<double> task3 = new Task<double>(() => GetRadius(task2.Result));
            task3.Start();

            task3.GetAwaiter().GetResult();

            Console.WriteLine("Результат GetAwaiter.GetResult: " + task3.Result);
        }
    }
}