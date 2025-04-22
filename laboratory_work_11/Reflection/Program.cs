using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        File.WriteAllLines("CarParameters.txt", new string[] { "Toyota", "Camry", "2015", "Белый", "15000", "A123BC" });

        List<string> classNames = new List<string>
        {
            "Car",
            "Product",
            "Confectionery",
            "Cake",
            "Candy",
            "Flowers",
            "Watch",
            "System.String",
            "System.Collections.Generic.List`1"
        };

        foreach (var className in classNames)
        {
            try
            {
                Console.WriteLine($"\nИсследование класса: {className}");

                // a. Имя сборки
                string assemblyName = Reflector.GetAssemblyName(className);
                Console.WriteLine($"Имя сборки: {assemblyName}");

                bool hasPublicCtors = Reflector.HasPublicConstructors(className);
                Console.WriteLine($"Есть публичные конструкторы: {hasPublicCtors}");

                IEnumerable<string> methods = Reflector.GetPublicMethods(className);
                Console.WriteLine("Публичные методы:");
                foreach (var method in methods)
                {
                    Console.WriteLine($" - {method}");
                }

                IEnumerable<string> fieldsProps = Reflector.GetFieldsAndProperties(className);
                Console.WriteLine("Поля и свойства:");
                foreach (var item in fieldsProps)
                {
                    Console.WriteLine($" - {item}");
                }

                IEnumerable<string> interfaces = Reflector.GetImplementedInterfaces(className);
                Console.WriteLine("Реализованные интерфейсы:");
                foreach (var iface in interfaces)
                {
                    Console.WriteLine($" - {iface}");
                }

                string parameterType = "System.String";
                IEnumerable<string> methodsWithParam = Reflector.GetMethodsWithParameterType(className, parameterType);
                Console.WriteLine($"Методы с параметром типа {parameterType}:");
                foreach (var m in methodsWithParam)
                {
                    Console.WriteLine($" - {m}");
                }

                var info = new
                {
                    ClassName = className,
                    AssemblyName = assemblyName,
                    HasPublicConstructors = hasPublicCtors,
                    PublicMethods = methods,
                    FieldsAndProperties = fieldsProps,
                    ImplementedInterfaces = interfaces,
                    MethodsWithStringParameter = methodsWithParam
                };

                string fileName = $"{className}_Info.json";
                Reflector.WriteInfoToFile(info, fileName);
                Console.WriteLine($"Информация записана в файл: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при исследовании класса {className}: {ex.Message}");
            }
        }

        try
        {
            string targetClass = "Car";
            string targetMethod = "ToString";
            string paramsFile = "CarParameters.txt";

            object result = Reflector.InvokeMethod(targetClass, targetMethod, paramsFile);
            Console.WriteLine($"\nРезультат вызова метода {targetMethod} класса {targetClass}:");
            Console.WriteLine(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при вызове метода: {ex.Message}");
        }

        try
        {
            Car createdCar = Reflector.Create<Car>();
            Console.WriteLine($"\nСоздан объект Car с помощью Reflector.Create<Car>():");
            Console.WriteLine(createdCar);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании объекта: {ex.Message}");
        }

        try
        {
            Watch createdWatch = Reflector.Create<Watch>();
            Console.WriteLine($"\nСоздан объект Watch с помощью Reflector.Create<Watch>():");
            Console.WriteLine(createdWatch);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании объекта: {ex.Message}");
        }

        Console.WriteLine("\nИсследование завершено.");
    }
}