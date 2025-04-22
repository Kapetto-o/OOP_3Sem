using System;

class Program
{
    static void Main()
    {



        var t = ( 1, 2, 3, "Yaha");
        int? g = null;

        void CheckedFunction()
        {
            try
            {
                checked
                {
                    int maxValue = int.MaxValue;
                    Console.WriteLine($"Значение до переполнения (checked): {maxValue}");
                    maxValue++;
                    Console.WriteLine($"Значение после переполнения (checked): {maxValue}");
                }
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"Исключение в блоке checked: {ex.Message}");
            }
        }

        void UncheckedFunction()
        {
            unchecked
            {
                int maxValue = int.MaxValue;
                Console.WriteLine($"Значение до переполнения (unchecked): {maxValue}");
                maxValue++;
                Console.WriteLine($"Значение после переполнения (unchecked): {maxValue}");
            }
        }

        Console.WriteLine("Вызов функции CheckedFunction:");
        CheckedFunction();

        Console.WriteLine("\nВызов функции UncheckedFunction:");
        UncheckedFunction();
    }
}