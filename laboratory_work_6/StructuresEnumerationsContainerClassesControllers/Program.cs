using System;
using System.Diagnostics;

interface ICloneable
{
    object Clone();
}
public abstract class BaseClone
{
    public abstract object DoClone();
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
        try
        {
            ProcessGift();
        }
        catch (ProductNotFoundException ex)
        {
            Console.WriteLine($"Перехвачено исключение в Main: {ex.Message}");
            Console.WriteLine("Попробуйте проверить состав подарка.");
            //throw;
        }
        catch (InvalidProductDetailsException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (IndexOutOfRangeGiftException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (GiftException ex)
        {
            Console.WriteLine($"Ошибка подарка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Конец программы.");
        }
    }

    static void ProcessGift()
    {
        try
        {
            var gift = new Gift();
            var giftController = new GiftController(gift);

            gift.Add(new Cake("Наполеон", 10, new ProductDetails(1.2, 10, 20, 20)));
            gift.Add(new Candy("Шипучка", 1, new ProductDetails(0.05, 1, 1, 1)));
            gift.Add(new Flowers("Тюльпаны", 12, new ProductDetails(0.3, 30, 5, 5)));
            gift.Add(new Watch("Красная цена", 5000, new ProductDetails(0.2, 1, 3, 3)));

            //gift.Add(new Cake("Фейковый торт", 1000000, new ProductDetails(0, 30, 50, 50)));

            Console.WriteLine("Компоненты подарка:");
            gift.DisplayItems();

            Console.WriteLine($"\nОбщая стоимость подарка: {giftController.CalculateTotalPrice()} руб.");

            var lightestItem = giftController.FindLightestItem();
            Console.WriteLine($"\nКомпонент с наименьшей массой: {lightestItem}");

            giftController.SortItemsByDimensions();
            Console.WriteLine("\nКомпоненты после сортировки по габаритам:");
            gift.DisplayItems();

            var fakeProduct = new Cake("Фейкторт", 100, new ProductDetails(10, 10, 20, 20));
            gift.Remove(fakeProduct);
        }
        catch (ProductNotFoundException ex)
        {
            Console.WriteLine($"Перехвачено исключение в ProcessGift: {ex.Message}");
            throw; 
        }
        catch (GiftException ex)
        {
            Console.WriteLine($"Перехвачено GiftException: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общая ошибка в ProcessGift: {ex.Message}");
        }
    }
}