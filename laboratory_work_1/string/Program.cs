using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.WriteLine("\tЗАДАНИЕ #2\n");

        ExecuteTask2A(); // ЗАДАНИЕ 2.А
        ExecuteTask2B(); // ЗАДАНИЕ 2.B
        ExecuteTask2C(); // ЗАДАНИЕ 2.C
        ExecuteTask2D(); // ЗАДАНИЕ 2.D
    }

    static void ExecuteTask2A() // ЗАДАНИЕ 2.А
    {
        Console.WriteLine("A)\n");

        string str1 = "Yasha";
        string str2 = "Yasha";
        string str3 = "yasha";
        string str4 = "YaYasha";

        Console.WriteLine("Сравнение строк с помощью оператора ==:");
        Console.WriteLine($"str1 == str2: {str1 == str2}"); // true
        Console.WriteLine($"str1 == str2: {str1 == str3}"); // false
        Console.WriteLine($"str1 == str2: {str1 == str4}"); // false
        Console.WriteLine($"str1 == str2: {str1 == str4}");
    }
    static void ExecuteTask2B() // ЗАДАНИЕ 2.B
    {
        Console.WriteLine("B)\n");

        string str1 = "Ya";
        string str2 = "Yasha";
        string str3 = "Hello Yayasha";

        string concatenated = str1 + " " + str2;
        Console.WriteLine("Сцепление строк: " + concatenated);

        string copiedString = string.Copy(str1);
        Console.WriteLine("Копия строки str1: " + copiedString);

        string substring = str3.Substring(11, 2);
        Console.WriteLine("Выделенная подстрока из str3: " + substring);

        string[] words = str3.Split(' ');
        Console.WriteLine("Разделение строки str3 на слова:");
        foreach (var word in words)
        {
            Console.WriteLine(word);
        }

        string insertedString = str1.Insert(5, ", how are you?");
        Console.WriteLine("Вставка подстроки: " + insertedString);

        string removedSubstring = str3.Remove(7, 3);
        Console.WriteLine("Удаление подстроки из str3: " + removedSubstring);

        int year = 2024;
        string interpolatedString = $"Привет, {str1} {str2}! Добро пожаловать в {year} год!";
        Console.WriteLine("Интерполированная строка: " + interpolatedString);
    }
    static void ExecuteTask2C() // ЗАДАНИЕ 2.C
    {
        Console.WriteLine("C\n");

        string emptyString = "";
        string nullString = null;

        // Использование метода string.IsNullOrEmpty
        Console.WriteLine("Использование string.IsNullOrEmpty:");
        Console.WriteLine($"emptyString пустая или null? {string.IsNullOrEmpty(emptyString)}"); // True
        Console.WriteLine($"nullString пустая или null? {string.IsNullOrEmpty(nullString)}"); // True

        Console.WriteLine("\nДополнительные проверки и операции:");

        Console.WriteLine($"emptyString пустая, null или состоит только из пробелов? {string.IsNullOrWhiteSpace(emptyString)}"); // True
        Console.WriteLine($"nullString пустая, null или состоит только из пробелов? {string.IsNullOrWhiteSpace(nullString)}"); // True

        // Попытка выполнения операций на null строке (ошибки)
        try
        {
            int length = nullString.Length;
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nОперации с пустой строкой:");
        Console.WriteLine($"Длина пустой строки: {emptyString.Length}");
        string concatenatedEmpty = emptyString + " appended text";
        Console.WriteLine($"Пустая строка после сцепления: '{concatenatedEmpty}'");

        Console.WriteLine("\nОперации с null строкой:");
        string concatenatedNull = (nullString ?? "") + " appended text";
        Console.WriteLine($"null строка после сцепления: '{concatenatedNull}'");
    }
    static void ExecuteTask2D() // ЗАДАНИЕ 2.D
    {
        Console.WriteLine("D\n");

        // Строка на основе StringBuilder
        StringBuilder sb = new StringBuilder("Hello World!");

        Console.WriteLine("Исходная строка: " + sb.ToString());

        sb.Remove(5, 6); 
        Console.WriteLine("После удаления: " + sb.ToString());

        sb.Insert(0, "Start - ");
        Console.WriteLine("После добавления в начало: " + sb.ToString());

        sb.Append(" - End");
        Console.WriteLine("После добавления в конец: " + sb.ToString());
    }
}