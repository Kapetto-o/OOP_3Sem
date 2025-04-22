using System;
using System.Linq;

public static class CollectionTypeExtensions
{
    public static T GetLastElement<T>(this CollectionType<T> collection) where T : IComparable<T>
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        var itemsField = typeof(CollectionType<T>)
            .GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var items = itemsField?.GetValue(collection) as System.Collections.Generic.List<T>;
        if (items == null || items.Count == 0)
            throw new InvalidOperationException("Коллекция пуста.");
        return items.Last();
    }
    public static void RemoveElement<T>(this CollectionType<T> collection, T item) where T : IComparable<T>
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));
        collection.Remove(item);
    }
    public static int GetSum<T>(this CollectionType<T> collection) where T : IComparable<T>
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));
        var itemsField = typeof(CollectionType<T>)
            .GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var items = itemsField?.GetValue(collection) as System.Collections.Generic.List<T>;
        if (typeof(T) != typeof(int) && typeof(T) != typeof(double))
            throw new InvalidOperationException("Метод поддерживает только числовые типы.");
        return items?.Sum(item => Convert.ToInt32(item)) ?? 0;
    }
    public static double GetDifferenceBetweenMaxAndMin<T>(this CollectionType<T> collection) where T : IComparable<T>
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));
        var itemsField = typeof(CollectionType<T>)
            .GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var items = itemsField?.GetValue(collection) as System.Collections.Generic.List<T>;
        if (items == null || items.Count == 0)
            throw new InvalidOperationException("Коллекция пуста.");
        if (typeof(T) != typeof(int) && typeof(T) != typeof(double))
            throw new InvalidOperationException("Метод поддерживает только числовые типы.");
        return Convert.ToDouble(items.Max()) - Convert.ToDouble(items.Min());
    }
}