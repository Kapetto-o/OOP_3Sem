using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class Reflector
{
    public static string GetAssemblyName(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Класс {className} не найден.");
        return type.Assembly.FullName;
    }

    public static bool HasPublicConstructors(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Класс {className} не найден.");
        return type.GetConstructors().Any(c => c.IsPublic);
    }

    public static IEnumerable<string> GetPublicMethods(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Класс {className} не найден.");
        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   .Select(m => m.Name);
    }

    public static IEnumerable<string> GetFieldsAndProperties(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Класс {className} не найден.");

        var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                         .Select(f => $"Field: {f.Name} ({f.FieldType.Name})");
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                             .Select(p => $"Property: {p.Name} ({p.PropertyType.Name})");
        return fields.Concat(properties);
    }

    public static IEnumerable<string> GetImplementedInterfaces(string className)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Класс {className} не найден.");
        return type.GetInterfaces().Select(i => i.Name);
    }

    public static IEnumerable<string> GetMethodsWithParameterType(string className, string parameterTypeName)
    {
        Type type = Type.GetType(className);
        Type parameterType = Type.GetType(parameterTypeName);
        if (type == null) throw new ArgumentException($"Класс {className} не найден.");
        if (parameterType == null) throw new ArgumentException($"Тип параметра {parameterTypeName} не найден.");

        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   .Where(m => m.GetParameters().Any(p => p.ParameterType == parameterType))
                   .Select(m => m.Name);
    }

    public static object InvokeMethod(string className, string methodName, string parametersFilePath)
    {
        Type type = Type.GetType(className);
        if (type == null) throw new ArgumentException($"Класс {className} не найден.");

        string[] paramLines = File.ReadAllLines(parametersFilePath);
        object[] parameters = paramLines.Select(line => GenerateValue(line)).ToArray();

        MethodInfo method = type.GetMethods().FirstOrDefault(m => m.Name == methodName && m.GetParameters().Length == parameters.Length);
        if (method == null) throw new ArgumentException($"Метод {methodName} с {parameters.Length} параметрами не найден в классе {className}.");

        object classInstance = CreateInstance(type);

        return method.Invoke(classInstance, parameters);
    }

    public static T Create<T>()
    {
        return (T)CreateInstance(typeof(T));
    }

    private static object CreateInstance(Type type)
    {
        ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
        if (ctor != null)
        {
            return ctor.Invoke(null);
        }

        ctor = type.GetConstructors().FirstOrDefault();
        if (ctor != null)
        {
            var parameters = ctor.GetParameters().Select(p => GenerateValue(p.ParameterType)).ToArray();
            return ctor.Invoke(parameters);
        }

        throw new ArgumentException($"Нет доступных публичных конструкторов для класса {type.FullName}.");
    }

    private static object GenerateValue(Type type)
    {
        if (type == typeof(string)) return "TestString";
        if (type == typeof(int)) return 123;
        if (type == typeof(decimal)) return 99.99m;
        if (type == typeof(double)) return 123.45;
        if (type == typeof(bool)) return true;
        if (type.IsEnum) return Enum.GetValues(type).GetValue(0);
        if (type == typeof(DateTime)) return DateTime.Now;
        return null;
    }

    private static object GenerateValue(string value)
    {
        return value;
    }

    public static void WriteInfoToFile(object info, string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(info, options);
        File.WriteAllText(filePath, json);
    }
}