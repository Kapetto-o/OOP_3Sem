using System;
using System.Collections.Generic;
using System.Linq;

namespace labwork8
{
    class Programmer
    {
        public event Action<List<string>> Delete;
        public event Action<List<string>> Mutate;

        public void TriggerDelete(List<string> list)
        {
            Delete?.Invoke(list);
        }

        public void TriggerMutate(List<string> list)
        {
            Mutate?.Invoke(list);
        }
    }

    public static class StringProcessor
    {
        public static string RemovePunctuation(string input) =>
        new string(input.Where(c => !char.IsPunctuation(c)).ToArray());

        public static string AddSymbol(string input, char symbol) => $"{symbol}{input}";

        public static string ToUpperCase(string input) => input.ToUpper();

        public static string RemoveExtraSpaces(string input) =>
            string.Join(" ", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

        public static string ReplaceSpaces(string input, char symbol) => input.Replace(' ', symbol);
    }

    public static class ListHandlers
    {
        public static void RemoveFirstElement(List<string> list)
        {
            if (list.Count > 0) list.RemoveAt(0);
        }

        public static void RemoveLastElement(List<string> list)
        {
            if (list.Count > 0) list.RemoveAt(list.Count - 1);
        }

        public static void ShuffleList(List<string> list)
        {
            Random rand = new Random();
            var shuffled = list.OrderBy(_ => rand.Next()).ToList();
            list.Clear();
            list.AddRange(shuffled);
        }
    }

    class Program
    {
        public static string ProcessString(string input, List<Func<string, string>> operations)
        {
            foreach (var operation in operations)
            {
                input = operation(input);
            }
            return input;
        }

        static void Main(string[] args)
        {
            Programmer programmer = new Programmer();

            List<string> list1 = new List<string> { "А - rus", "Б - rus", "В - rus" };
            List<string> list2 = new List<string> { "A - eng", "B - eng", "C - eng" };

            programmer.Delete += ListHandlers.RemoveFirstElement;
            programmer.Delete += ListHandlers.RemoveLastElement;
            programmer.Mutate += ListHandlers.ShuffleList;

            Console.WriteLine("List1: " + string.Join(", ", list1));
            programmer.TriggerDelete(list1);
            Console.WriteLine("Delete для List1: " + string.Join(", ", list1) + "\n");

            Console.WriteLine("List2: " + string.Join(", ", list2));
            programmer.TriggerMutate(list2);
            Console.WriteLine("Mutate для List2: " + string.Join(", ", list2) + "\n");

            Console.WriteLine("Исходная строка: " + "!Тестовая строка, необходимая, для, эксперементов #1!");
            string input = "!Тестовая строка, необходимая, для, эксперементов #1!";
            List<Func<string, string>> operations = new List<Func<string, string>>
        {
            StringProcessor.RemovePunctuation,
            str => StringProcessor.AddSymbol(str, '#'),
            StringProcessor.ToUpperCase,
            StringProcessor.RemoveExtraSpaces,
            str => StringProcessor.ReplaceSpaces(str, '_')
        };

            string result = ProcessString(input, operations);
            Console.WriteLine("Изменённая строка: " + result + "\n");
        }
    }
}