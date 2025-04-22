using System;

interface ICloneable
{
    bool DoClone();
}
public abstract class BaseClone
{
    public abstract bool DoClone();
}
abstract class Item : Product
{
    public Item(string name, decimal price, ProductCategory category, ProductDetails details)
        : base(name, price, category, details) { }

    public override string GetDescription()
    {
        return "Это товар общего назначения";
    }
}
class Confectionery : Item
{
    public Confectionery(string name, decimal price, ProductCategory category, ProductDetails details)
        : base(name, price, category, details) { }

    public override string GetDescription()
    {
        return "Это товар из категории кондитерские изделия";
    }
}
class Cake : Confectionery
{
    public Cake(string name, decimal price, ProductDetails details)
        : base(name, price, ProductCategory.Cake, details) { }

    public override string GetDescription()
    {
        return "Это товар из категории торты";
    }
}
class Candy : Confectionery
{
    public Candy(string name, decimal price, ProductDetails details)
        : base(name, price, ProductCategory.Candy, details) { }

    public override string GetDescription()
    {
        return "Это товар из категории конфеты";
    }
}
class Flowers : Item
{
    public Flowers(string name, decimal price, ProductDetails details)
        : base(name, price, ProductCategory.Flowers, details) { }

    public override string GetDescription()
    {
        return "Это товар из категории цветы";
    }
}
sealed class Watch : Product
{
    public Watch(string name, decimal price, ProductDetails details)
        : base(name, price, ProductCategory.Watch, details) { }

    public override string GetDescription()
    {
        return "Это товар из категории часы";
    }
}
class Printer
{
    public void IAmPrinting(BaseClone obj)
    {
        Console.WriteLine(obj.ToString());
    }
}
class Program
{
    static void Main()
    {
        var Gift = new Gift();
        var giftController = new GiftController(Gift);

        Gift.Add(new Cake("Наполеон", 10, new ProductDetails(1.2, 10, 20, 20)));
        Gift.Add(new Candy("Шипучка", 1, new ProductDetails(0.05, 1, 1, 1)));
        Gift.Add(new Flowers("Тюльпаны", 12, new ProductDetails(0.3, 30, 5, 5)));
        Gift.Add(new Watch("Красная цена", 5000, new ProductDetails(0.2, 1, 3, 3)));

        Console.WriteLine("Компоненты подарка:");
        Gift.DisplayItems();

        Console.WriteLine($"\nОбщая стоимость подарка: {giftController.CalculateTotalPrice()} руб.");

        var lightestItem = giftController.FindLightestItem();
        Console.WriteLine($"\nКомпонент с наименьшей массой: {lightestItem}");

        giftController.SortItemsByDimensions();
        Console.WriteLine("\nКомпоненты после сортировки по габаритам:");
        Gift.DisplayItems();
    }
}