using System;
using System.Collections.Generic;

public class Gift
{
    private IList<Product> items = new List<Product>();

    public Product Get(int index)
    {
        return (index >= 0 && index < items.Count) ? items[index] : null;
    }
    public void Set(int index, Product item)
    {
        if (index >= 0 && index < items.Count)
            items[index] = item;
    }
    public void Add(Product item)
    {
        items.Add(item);
    }
    public void Remove(Product item)
    {
        items.Remove(item);
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