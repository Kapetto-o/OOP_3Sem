using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class CollectionType<T> : GenericInterface<T> where T : IComparable<T>
{
    private readonly List<T> _items;

    public CollectionType()
    {
        _items = new List<T>();
    }

    public void Add(T item)
    {
        try
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Элемент не может быть null.");
            _items.Add(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении элемента: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция добавления завершена.");
        }
    }
    public void Remove(T item)
    {
        try
        {
            if (!_items.Remove(item))
                throw new KeyNotFoundException("Элемент для удаления не найден.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении элемента: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция удаления завершена.");
        }
    }
    public void ViewAll()
    {
        try
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Коллекция пуста.");
            Console.WriteLine("Элементы коллекции:");
            foreach (var item in _items)
            {
                Console.WriteLine(item);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при просмотре элементов: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция просмотра завершена.");
        }
    }
    public T FindByPredicate(Predicate<T> predicate)
    {
        try
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate), "Предикат не может быть null.");

            var result = _items.Find(predicate);
            if (result == null)
                throw new InvalidOperationException("Элемент, соответствующий предикату, не найден.");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при поиске элемента: {ex.Message}");
            return default;
        }
        finally
        {
            Console.WriteLine("Операция поиска завершена.");
        }
    }

    public void SaveToJson(string filePath)
    {
        try
        {
            var json = JsonSerializer.Serialize(_items);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Коллекция успешно сохранена в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
        }
    }

    public void LoadFromJson(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден.");

            var json = File.ReadAllText(filePath);
            var items = JsonSerializer.Deserialize<List<T>>(json);

            if (items != null)
            {
                _items.Clear();
                _items.AddRange(items);
                Console.WriteLine("Коллекция успешно загружена из файла.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
        }
    }

    public static CollectionType<T> operator +(CollectionType<T> collection, T item)
    {
        collection.Add(item);
        return collection;
    }
    public static CollectionType<T> operator --(CollectionType<T> collection)
    {
        try
        {
            if (collection._items.Count == 0)
                throw new InvalidOperationException("Невозможно удалить элемент из пустой коллекции.");
            collection.Remove(collection._items.Last());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении последнего элемента: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция удаления последнего элемента завершена.");
        }
        return collection;
    }
    public override bool Equals(object? obj)
    {
        if (obj is CollectionType<T> other)
        {
            return this == other;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return _items.Aggregate(0, (hash, item) => hash ^ item.GetHashCode());
    }
    public static bool operator ==(CollectionType<T> c1, CollectionType<T> c2)
    {
        if (ReferenceEquals(c1, c2)) return true;
        if (c1 is null || c2 is null) return false;
        return c1._items.SequenceEqual(c2._items);
    }
    public static bool operator !=(CollectionType<T> c1, CollectionType<T> c2)
    {
        return !(c1 == c2);
    }
    public static bool operator true(CollectionType<T> collection)
    {
        return collection._items.Count == 0;
    }
    public static bool operator false(CollectionType<T> collection)
    {
        return collection._items.Count > 0;
    }
}