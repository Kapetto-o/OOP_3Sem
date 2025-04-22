using System;

class Program
{
    static void Main()
    {
        ExecuteTask3A(); // ЗАДАНИЕ 3.А
        ExecuteTask3B(); // ЗАДАНИЕ 3.B
        ExecuteTask3C(); // ЗАДАНИЕ 3.C
        ExecuteTask3D(); // ЗАДАНИЕ 3.D
    }
    static void ExecuteTask3A() // ЗАДАНИЕ 3.А
    {
        Console.WriteLine("A)\n");

        int[,] matrix = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };

        Console.WriteLine("Двумерный массив:");
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{matrix[i, j],4}"); // Выравнивание
            }
            Console.WriteLine();
        }
    }
    static void ExecuteTask3B() // ЗАДАНИЕ 3.B
    {
        Console.WriteLine("B)\n");

        // Одномерный массив строк
        string[] stringArray = { "One", "Two", "Three", "Four" };

        Console.WriteLine("Содержимое массива:");
        foreach (var str in stringArray)
        {
            Console.WriteLine(str);
        }
        Console.WriteLine($"Длина массива: {stringArray.Length}");

        Console.Write("Введите индекс элемента для изменения (0-3): ");
        int index = int.Parse(Console.ReadLine());
        Console.Write("Введите новое значение: ");
        string newValue = Console.ReadLine();

        if (index >= 0 && index < stringArray.Length)
        {
            stringArray[index] = newValue;
            Console.WriteLine("Обновленный массив:");
            foreach (var str in stringArray)
            {
                Console.WriteLine(str);
            }
        }
        else
        {
            Console.WriteLine("Недопустимый индекс.");
        }
    }
    static void ExecuteTask3C() // ЗАДАНИЕ 3.C
    {
        double[][] jaggedArray = new double[3][];
        jaggedArray[0] = new double[2]; // 2 столбца в строке 1
        jaggedArray[1] = new double[3];
        jaggedArray[2] = new double[4];

        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.WriteLine($"Введите значения для строки {i + 1}:");
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write($"Введите значение для элемента [{i}, {j}]: ");
                jaggedArray[i][j] = double.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("\nСтупенчатый массив:");
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            foreach (var item in jaggedArray[i])
            {
                Console.Write($"{item,6:F2} "); // Форматированный вывод
            }
            Console.WriteLine();
        }
    }
    static void ExecuteTask3D() // ЗАДАНИЕ 3.D
    {
        var array = new[] { 1, 2, 3, 4, 5 };
        var message = "Hello, World!";

        Console.WriteLine("Неявно типизированный массив:");
        foreach (var item in array)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine($"\nНеявно типизированная строка: {message}");
    }
}
