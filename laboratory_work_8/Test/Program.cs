using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_Events_Lambda_Expressions
{
    internal class Class4
    {
        public delegate string MyDeleg(int a, int b);

        public class EvClass
        {
            event MyDeleg On;

            string IntToString(int a, int b)
            {
                return $"Сумма: {a + b}";
            }

            static void Main(string[] args)
            {
                EvClass test = new EvClass();

                test.On = test.IntToString;

                string res = test.On.Invoke(100, 250);

                Console.WriteLine(res);
            }
        }
    }
}