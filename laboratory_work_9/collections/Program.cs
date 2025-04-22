using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Image : IEquatable<Image>
{
    public string Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Image(string name, int width, int height)
    {
        Name = name;
        Width = width;
        Height = height;
    }

    public override string ToString()
    {
        return $"{Name} ({Width}x{Height})";
    }

    public bool Equals(Image other)
    {
        if (other == null) return false;
        return Name == other.Name && Width == other.Width && Height == other.Height;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 31 + (Name?.GetHashCode() ?? 0);
        hash = hash * 31 + Width.GetHashCode();
        hash = hash * 31 + Height.GetHashCode();
        return hash;
    }
}

// Коллекция для работы с ISet<T> на базе LinkedList<T>
public class ImageSet : ISet<Image>
{
    private LinkedList<Image> images = new LinkedList<Image>();

    public bool Add(Image item)
    {
        if (images.Contains(item)) return false;
        images.AddLast(item);
        return true;
    }

    void ICollection<Image>.Add(Image item)
    {
        Add(item);
    }

    public void Clear() => images.Clear();
    public bool Contains(Image item) => images.Contains(item);

    public void CopyTo(Image[] array, int arrayIndex) => images.CopyTo(array, arrayIndex);

    public bool Remove(Image item)
    {
        var node = images.Find(item);
        if (node != null)
        {
            images.Remove(node);
            return true;
        }
        return false;
    }

    public int Count => images.Count;
    public bool IsReadOnly => false;

    public IEnumerator<Image> GetEnumerator() => images.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void ExceptWith(IEnumerable<Image> other)
    {
        foreach (var item in other)
            Remove(item);
    }

    public void IntersectWith(IEnumerable<Image> other)
    {
        var toKeep = new HashSet<Image>(other);
        var current = images.First;
        while (current != null)
        {
            var next = current.Next;
            if (!toKeep.Contains(current.Value))
                images.Remove(current);
            current = next;
        }
    }

    public bool IsProperSubsetOf(IEnumerable<Image> other) => throw new NotImplementedException();
    public bool IsProperSupersetOf(IEnumerable<Image> other) => throw new NotImplementedException();
    public bool IsSubsetOf(IEnumerable<Image> other) => throw new NotImplementedException();
    public bool IsSupersetOf(IEnumerable<Image> other) => throw new NotImplementedException();
    public bool Overlaps(IEnumerable<Image> other) => throw new NotImplementedException();
    public bool SetEquals(IEnumerable<Image> other) => throw new NotImplementedException();

    public void SymmetricExceptWith(IEnumerable<Image> other) => throw new NotImplementedException();
    public void UnionWith(IEnumerable<Image> other)
    {
        foreach (var item in other)
            Add(item);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("\nЗАДАНИЕ #1");

        var imageSet = new ImageSet();

        imageSet.Add(new Image("Зимний лес", 1920, 1080));
        imageSet.Add(new Image("Хоббитская деревушка", 1280, 720));
        imageSet.Add(new Image("Горный хребет", 2560, 1440));

        Console.WriteLine("Изображения:");
        foreach (var image in imageSet)
        {
            Console.WriteLine(image);
        }

        Console.WriteLine("\nУдаление изображения 'Зимний лес'");
        imageSet.Remove(new Image("Зимний лес", 1920, 1080));

        Console.WriteLine("\nИзображения:");
        foreach (var image in imageSet)
        {
            Console.WriteLine(image);
        }

        Console.WriteLine("\nПоиск 'Хоббитская деревушка':");
        if (imageSet.Contains(new Image("Хоббитская деревушка", 1280, 720)))
            Console.WriteLine("Изображение найдено");
        else
            Console.WriteLine("Такого изображения нет");
        Console.WriteLine("\nПоиск 'Долина Смауга':");
        if (imageSet.Contains(new Image("Долина Смауга", 1280, 720)))
            Console.WriteLine("Изображение найдено");
        else
            Console.WriteLine("Такого изображения нет");

        Console.WriteLine("\nЗАДАНИЕ #2");

        Console.WriteLine("=== Коллекция LinkedList ===");
        var linkedList = new LinkedList<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        Console.WriteLine("Коллекция:");
        foreach (var item in linkedList)
            Console.Write(item + " ");
        Console.WriteLine();

        Console.WriteLine("\nУдаляем 6 элементов: 3 с начала и 3 с конца");
        for (int i = 0; i < 3; i++)
        {
            linkedList.RemoveFirst();
            linkedList.RemoveLast();
        }

        Console.WriteLine("\nКоллекция:");
        foreach (var item in linkedList)
            Console.Write(item + " ");
        Console.WriteLine();

        Console.WriteLine("\nДобавляем 2 элемента: 1 в начало и 1 в конец");
        linkedList.AddLast(80);
        linkedList.AddFirst(30);

        Console.WriteLine("\nКоллекция:");
        foreach (var item in linkedList)
            Console.Write(item + " ");
        Console.WriteLine();

        var dictionary = new Dictionary<int, int>();
        int key = 1;
        foreach (var value in linkedList)
            dictionary[key++] = value;

        Console.WriteLine("\n=== Коллекция Dictionary ===");
        foreach (var pair in dictionary)
            Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");

        Console.WriteLine("\nПоиск значения 5:");
        if (dictionary.ContainsValue(5))
            Console.WriteLine("Значение найдено");
        else
            Console.WriteLine("Значение не найдено");
        Console.WriteLine("\nПоиск значения 20:");
        if (dictionary.ContainsValue(20))
            Console.WriteLine("Значение найдено");
        else
            Console.WriteLine("Значение не найдено");

        Console.WriteLine("\nЗАДАНИЕ #3");

        var observableCollection = new ObservableCollection<Image>();

        observableCollection.CollectionChanged += (sender, e) =>
        {
            Console.WriteLine($"\nДействие: {e.Action}");
            if (e.NewItems != null)
            {
                Console.WriteLine("Добавлен элемент:");
                foreach (Image item in e.NewItems)
                    Console.WriteLine(item);
            }
            if (e.OldItems != null)
            {
                Console.WriteLine("Удалены элементы:");
                foreach (Image item in e.OldItems)
                    Console.WriteLine(item);
            }
        };

        Console.WriteLine("\nДобавление изображения");
        observableCollection.Add(new Image("Крепость Гондора", 1920, 1080));
        observableCollection.Add(new Image("Жизнь Средиземья", 1280, 720));

        Console.WriteLine("\nУдаление изображения");
        observableCollection.RemoveAt(0);
    }
}