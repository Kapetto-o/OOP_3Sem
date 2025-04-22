using System;
using System.Collections.Generic;

public class Gift
{
    private IList<Product> items = new List<Product>();

    public Product Get(int index)
    {
        if (index < 0 || index >= items.Count)
            throw new IndexOutOfRangeGiftException($"Индекс {index} вне диапазона допустимых значений.");
        return items[index];
    }
    public void Set(int index, Product item)
    {
        if (index < 0 || index >= items.Count)
            throw new IndexOutOfRangeGiftException($"Индекс {index} имеет некорректное значение.");
        items[index] = item;
    }
    public void Add(Product item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Нельзя добавить null в подарок.");
        items.Add(item);
    }
    public void Remove(Product item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "Нельзя удалить null из подарка.");
        if (!items.Remove(item))
            throw new ProductNotFoundException("Удаляемого товара нет в подарке.");
    }
    public IList<Product> GetItems()
    {
        return items;
    }
    public void DisplayItems()
    {
        foreach (var item in items)
            Console.WriteLine(item);
    }
}