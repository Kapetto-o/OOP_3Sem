using System;

class Program
{
    static void Main()
    {
        int[] numbers = { 10, 20, 5, 30, 15 };
        string text = "Hello, World!";

        var result = ProcessArrayAndString(numbers, text);

        Console.WriteLine($"Максимальный элемент массива: {result.max}");
        Console.WriteLine($"Минимальный элемент массива: {result.min}");
        Console.WriteLine($"Сумма элементов массива: {result.sum}");
        Console.WriteLine($"Первая буква строки: {result.firstChar}");
    }
    static (int max, int min, int sum, char firstChar) ProcessArrayAndString(int[] arr, string str)
    {
        if (arr.Length == 0)
        {
            throw new ArgumentException("Массив не должен быть пустым.", nameof(arr));
        }
        int max = arr[0];
        int min = arr[0];
        int sum = 0;
        foreach (var num in arr)
        {
            if (num > max)
            {
                max = num;
            }
            if (num < min)
            {
                min = num;
            }
            sum += num;
        }

        // Определение первой буквы строки
        char firstChar = str.Length > 0 ? str[0] : ' '; 

        return (max, min, sum, firstChar);
    }
}