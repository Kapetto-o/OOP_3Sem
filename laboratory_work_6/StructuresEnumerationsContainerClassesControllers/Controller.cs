using System;
using System.Linq;

public class GiftController
{
    private Gift container;

    public GiftController(Gift container)
    {
        this.container = container;
    }
    public decimal CalculateTotalPrice()
    {
        decimal total = 0;
        foreach (var item in container.GetItems())
            total += item.Price;
        return total;
    }
    public Product FindLightestItem()
    {
        Product lightest = container.GetItems().FirstOrDefault();
        foreach (var item in container.GetItems())
        {
            if (item.Details.Weight < lightest.Details.Weight)
                lightest = item;
        }
        return lightest;
    }
    public void SortItemsByDimensions()
    {
        var sortedItems = container.GetItems()
            .OrderBy(item => item.Details.Height)
            .ThenBy(item => item.Details.Width)
            .ThenBy(item => item.Details.Depth)
            .ToList();
        container.GetItems().Clear();
        foreach (var item in sortedItems)
        {
            container.Add(item);
        }
    }
}