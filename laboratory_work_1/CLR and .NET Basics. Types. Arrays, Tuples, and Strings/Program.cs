using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("\tЗАДАНИЕ #1\n");
        ExecuteTask1A(); // ЗАДАНИЕ 1.A
        ExecuteTask1B(); // ЗАДАНИЕ 1.B
        ExecuteTask1C(); // ЗАДАНИЕ 1.C
        ExecuteTask1D(); // ЗАДАНИЕ 1.D
        ExecuteTask1E(); // ЗАДАНИЕ 1.E
    }

    static void ExecuteTask1A() // ЗАДАНИЕ 1.А
    {
        Console.WriteLine("A)\n");

        float f = 3.14f;
        double d = 3.14159265359;
        decimal dec = 7.9228162514264337593543950335m;

        byte b = 200;
        sbyte sb = -100;
        short sh = -32000;
        ushort us = 65000;
        uint ui = 4294967295;
        long l = -9223372036854775808;
        ulong ul = 18446744073709551615;

        Console.WriteLine("float: " + f);
        Console.WriteLine("double: " + d);
        Console.WriteLine("decimal: " + dec);
        Console.WriteLine("byte: " + b);
        Console.WriteLine("sbyte: " + sb);
        Console.WriteLine("short: " + sh);
        Console.WriteLine("ushort: " + us);
        Console.WriteLine("uint: " + ui);
        Console.WriteLine("long: " + l);
        Console.WriteLine("ulong: " + ul);

        Console.Write("Введите строку (string): ");
        string str = Console.ReadLine();                          // Строка
        Console.WriteLine($"Вы ввели строку: {str}");

        Console.Write("Введите целое число (int): ");
        int i = Convert.ToInt32(Console.ReadLine());             // Целое число
        Console.WriteLine($"Вы ввели число: {i}");

        Console.Write("Введите символ (char): ");
        char c = Convert.ToChar(Console.ReadLine());             // Символ
        Console.WriteLine($"Вы ввели символ: {c}");

        Console.Write("Введите true или false (boolean): ");
        bool boolean = Convert.ToBoolean(Console.ReadLine());    // Логический
        Console.WriteLine($"Вы ввели символ: {boolean}");
    }

    static void ExecuteTask1B() // ЗАДАНИЕ 1.B
    {
        Console.WriteLine("\nB)\n");

        double g = 123.456;
        int h = (int)g;
        long i = (long)g;
        float j = (float)g;
        uint k = (uint)i;
        byte l = (byte)h;

        Console.WriteLine("Явное приведение:");
        Console.WriteLine($"double -> int: {h}");
        Console.WriteLine($"double -> long: {i}");
        Console.WriteLine($"double -> float: {j}");
        Console.WriteLine($"long -> uint: {k}");
        Console.WriteLine($"int -> byte: {l}");

        int a = 100;
        long b = a;
        float c = a;
        double d = c;
        ushort e = 30000;
        uint f = e;

        Console.WriteLine("Неявное приведение:");
        Console.WriteLine($"int\t-> long: {b}");
        Console.WriteLine($"int\t-> float: {c}");
        Console.WriteLine($"float\t-> double: {d}");
        Console.WriteLine($"ushort\t-> uint: {f}");

        string strNumber = "123";
        int m = Convert.ToInt32(strNumber);
        double n = Convert.ToDouble(g);
        bool boolValue = Convert.ToBoolean(1);
        char charValue = Convert.ToChar(65);                // Число в символ 'A'
        string boolToString = Convert.ToString(boolValue);

        Console.WriteLine("\nПримеры использования Convert:");
        Console.WriteLine($"Convert.ToInt32: {m}");
        Console.WriteLine($"Convert.ToDouble: {n}");
        Console.WriteLine($"Convert.ToBoolean(1): {boolValue}");
        Console.WriteLine($"Convert.ToChar(65): {charValue}");
        Console.WriteLine($"Convert.ToString(bool): {boolToString}");
    }

    static void ExecuteTask1C() // ЗАДАНИЕ 1.C
    {
        Console.WriteLine("\nC)\n");

        int value = 123;
        object boxedValue = value;

        int unboxedValue = (int)boxedValue;

        Console.WriteLine("Упаковка и распаковка:");
        Console.WriteLine($"Исходное значение: {value}");
        Console.WriteLine($"Упакованное значение (object): {boxedValue}");
        Console.WriteLine($"Распакованное значение: {unboxedValue}");

        bool boolValue = true;
        object boxedBool = boolValue; 
        bool unboxedBool = (bool)boxedBool; 

        char charValue = 'A';
        object boxedChar = charValue; 
        char unboxedChar = (char)boxedChar;

        Console.WriteLine("\nДополнительные примеры:");
        Console.WriteLine($"Упакованное значение bool (object): {boxedBool}");
        Console.WriteLine($"Распакованное значение bool: {unboxedBool}");
        Console.WriteLine($"Упакованное значение char (object): {boxedChar}");
        Console.WriteLine($"Распакованное значение char: {unboxedChar}");
    }

    static void ExecuteTask1D() // ЗАДАНИЕ 1.D
    {
        Console.WriteLine("\nD)\n");

        var intValue = 10;
        var stringValue = "Hello";
        var doubleValue = 3.14;
        var boolValue = true;

        Console.WriteLine("Работа с неявно типизированными переменными:");
        Console.WriteLine($"intValue (int): {intValue}");
        Console.WriteLine($"stringValue (string): {stringValue}");
        Console.WriteLine($"doubleValue (double): {doubleValue}");
        Console.WriteLine($"boolValue (bool): {boolValue}");
    }
    static void ExecuteTask1E() // ЗАДАНИЕ 1.E
    {
        Console.WriteLine("\nF)\n");

        int? nullableInt = null;
        double? nullableDouble = 3.14;

        if (nullableInt.HasValue)
        {
            Console.WriteLine($"nullableInt имеет значение: {nullableInt.Value}");
        }
        else
        {
            Console.WriteLine("nullableInt равно null");
        }

        if (nullableInt.HasValue)
        {
            Console.WriteLine($"nullableDouble имеет значение: {nullableDouble.Value}");
        }
        else
        {
            Console.WriteLine("nullableDouble равно null");
        }
    }
    static void ExecuteTask1F() // ЗАДАНИЕ 1.F
    {
        Console.WriteLine("\nE)\n");
        
        var numberVar = 10;
        //numberVar = "Yasha";
    }
}