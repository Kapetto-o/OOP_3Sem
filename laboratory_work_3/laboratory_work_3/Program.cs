using System;

public class List
{
    internal class Node
    {
        public int Value;
        public Node Next;
        public Node(int value)
        {
            Value = value;
            Next = null;
        }
    }
    public class Production
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public Production(int id, string organizationName)
        {
            Id = id;
            OrganizationName = organizationName;
        }
    }
    public class Developer
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }
        public Developer(string fullName, int id, string department)
        {
            FullName = fullName;
            Id = id;
            Department = department;
        }
    }
    internal Node head;
    public Production ProdInfo { get; set; }
    public Developer DevInfo { get; set; }
    public List()
    {
        head = null;
        ProdInfo = new Production(1, "БГТУ");
        DevInfo = new Developer("Ленкевич Павел Андреевич", 09, "ФИТ");
    }
    public void Add(int value)
    {
        Node newNode = new Node(value);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }
    public void RemoveFirst()
    {
        if (head != null)
        {
            head = head.Next;
        }
    }
    public bool IsEmpty() => head == null;
    public static List operator +(List list1, List list2)
    {
        List result = new List();
        Node current = list1.head;
        while (current != null)
        {
            result.Add(current.Value);
            current = current.Next;
        }
        current = list2.head;
        while (current != null)
        {
            result.Add(current.Value);
            current = current.Next;
        }
        return result;
    }
    public static List operator --(List list)
    {
        list.RemoveFirst();
        return list;
    }
    public static bool operator ==(List list1, List list2)
    {
        Node current1 = list1.head;
        Node current2 = list2.head;
        while (current1 != null && current2 != null)
        {
            if (current1.Value != current2.Value)
                return false;
            current1 = current1.Next;
            current2 = current2.Next;
        }
        return current1 == null && current2 == null;
    }
    public static bool operator !=(List list1, List list2) => !(list1 == list2);
    public static bool operator true(List list) => list.IsEmpty();
    public static bool operator false(List list) => !list.IsEmpty();
    public override bool Equals(object obj)
    {
        if (obj is List other)
            return this == other;
        return false;
    }
    public override int GetHashCode() => base.GetHashCode();
}
public static class StatisticOperation
{
    public static int Sum(List list)
    {
        int sum = 0;
        var current = list.head;
        while (current != null)
        {
            sum += current.Value;
            current = current.Next;
        }
        return sum;
    }
    public static int DifferenceBetweenMaxMin(List list)
    {
        if (list.head == null) return 0;

        int max = list.head.Value;
        int min = list.head.Value;
        var current = list.head;
        while (current != null)
        {
            if (current.Value > max) max = current.Value;
            if (current.Value < min) min = current.Value;

            current = current.Next;
        }
        return max - min;
    }
    public static int CountElements(List list)
    {
        int count = 0;
        var current = list.head;
        while (current != null)
        {
            count++;
            current = current.Next;
        }
        return count;
    }
    public static int GetLastNumber(this string str)
    {
        string[] numbers = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = numbers.Length - 1; i >= 0; i--)
        {
            if (int.TryParse(numbers[i], out int result))
                return result;
        }
        return 0;
    }
    public static void RemoveElement(this List list, int value)
    {
        List.Node current = list.head;
        List.Node previous = null;
        while (current != null)
        {
            if (current.Value == value)
            {
                if (previous == null)
                {
                    list.head = current.Next;
                }
                else
                {
                    previous.Next = current.Next;
                }
                return;
            }
            previous = current;
            current = current.Next;
        }
    }
}
public class Program
{
    public static void Main()
    {
        List list1 = new List();
        list1.Add(1);
        list1.Add(2);
        list1.Add(3);
        Console.WriteLine("Содержимое list1: ");
        var current = list1.head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();

        List list2 = new List();
        list2.Add(4);
        list2.Add(5);
        Console.WriteLine("Содержимое list2: ");
        current = list2.head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();

        List list3 = list1 + list2;
        Console.WriteLine("Содержимое list3: ");
        current = list3.head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();

        Console.WriteLine("Удаление первого элемента list3: ");
        --list3;
        current = list3.head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();

        Console.WriteLine("list1 == list2: " + (list1 == list2));
        if (list1) { Console.WriteLine("Список пуст"); }
        else Console.WriteLine("Список не пуст");

        string testString = "Тестируемая строка в середине которой два числа: 23 и 14 - на это число нацелен вывод";
        Console.WriteLine("Последнее число: " + testString.GetLastNumber());

        list3.RemoveElement(4);
        Console.WriteLine("list3 после удаления заданного элемента (4): ");
        current = list3.head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();

        Console.WriteLine("Сумма list1: " + StatisticOperation.Sum(list1));

        Console.WriteLine("max-min в list1: " + StatisticOperation.DifferenceBetweenMaxMin(list1));

        Console.WriteLine("Элементов в list1: " + StatisticOperation.CountElements(list1));
    }
}