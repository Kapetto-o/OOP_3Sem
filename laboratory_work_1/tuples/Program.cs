using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("\tЗАДАНИЕ #1\n");
        ExecuteTask3();
    }

    static void ExecuteTask3() 
    {
        Console.WriteLine("A)\n");  // ЗАДАНИЕ 4.А

        // Кортеж
        var tuple = (1, "Hello", 'A', "World", 12345678901234567890UL);

        Console.WriteLine("Кортеж целиком:");
        Console.WriteLine(tuple);

        Console.WriteLine("\nB)\n");  // ЗАДАНИЕ 4.B

        Console.WriteLine("\nВыборочные элементы кортежа:");
        Console.WriteLine($"1 элемент: {tuple.Item1}");
        Console.WriteLine($"3 элемент: {tuple.Item3}");
        Console.WriteLine($"4 элемент: {tuple.Item4}");

        Console.WriteLine("\nC)\n");  // ЗАДАНИЕ 4.C

        // Способ 1: Распаковка в отдельные переменные
        var (id, greeting, letter, message, largeNumber) = tuple;
        Console.WriteLine("\nРаспаковка кортежа в переменные:");
        Console.WriteLine($"id: {id}");
        Console.WriteLine($"greeting: {greeting}");
        Console.WriteLine($"letter: {letter}");
        Console.WriteLine($"message: {message}");
        Console.WriteLine($"largeNumber: {largeNumber}");

        // Способ 2: Распаковка с использованием переменной _
        var (onlyId, _, onlyLetter, _, _) = tuple;
        Console.WriteLine("\nРаспаковка с использованием переменной _ (пропуск элементов):");
        Console.WriteLine($"onlyId: {onlyId}");
        Console.WriteLine($"onlyLetter: {onlyLetter}");

        Console.WriteLine("\nD)\n");  // ЗАДАНИЕ 4.D

        var tuple1 = (1, "Hello", 'A', "World", 12345678901234567890UL);
        var tuple2 = (1, "Hello", 'A', "World", 12345678901234567890UL);
        var tuple3 = (2, "Hello", 'B', "Universe", 8765432109876543210UL);

        Console.WriteLine("\nСравнение двух кортежей:");
        Console.WriteLine($"tuple1 == tuple2: {tuple1 == tuple2}"); // True
        Console.WriteLine($"tuple1 == tuple3: {tuple1 == tuple3}"); // False
    }
}