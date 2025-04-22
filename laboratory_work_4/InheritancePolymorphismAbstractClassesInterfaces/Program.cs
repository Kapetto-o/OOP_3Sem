using System;

interface ICloneable
{
    bool DoClone();
}
abstract class BaseClone
{
    public abstract bool DoClone();
}
abstract class Product : BaseClone, ICloneable
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
    public virtual string GetDescription()
    {
        return "Продукт без категории";
    }
    public override bool DoClone()
    {
        return false;
    }

    bool ICloneable.DoClone()
    {
        return true;
    }

    public override string ToString()
    {
        return $"{GetType().Name}: Название = {Name}, Цена = {Price} руб.";
    }
}
abstract class Item : Product
{
    public override string GetDescription()
    {
        return "Это товар общего назначения";
    }
    public Item(string name, decimal price) : base(name, price) { }
    
}
class Confectionery : Item
{
    public Confectionery(string name, decimal price) : base(name, price) { }
    public override string GetDescription()
    {
        return "Это товар из категории кондитерские изделия";
    }
    public override string ToString()
    {
        return $"Кондитерское изделие: {base.ToString()}";
    }
}
class Cake : Confectionery
{
    public Cake(string name, decimal price) : base(name, price) { }
    public override string GetDescription()
    {
        return "Это товар из котигории торты";
    }
    public override string ToString()
    {
        return $"Торт: {base.ToString()}";
    }
}
class Candy : Confectionery
{
    public override string GetDescription()
    {
        return "Это товар из категории конфеты";
    }
    public Candy(string name, decimal price) : base(name, price) { }
    public override string ToString()
    {
        return $"Конфета: {base.ToString()}";
    }
}
class Flowers : Item
{
    public Flowers(string name, decimal price) : base(name, price) { }
    public override string GetDescription()
    {
        return "Это товар из категории цветы";
    }
    public override string ToString()
    {
        return $"Цветы: {base.ToString()}";
    }
}
sealed class Watch : Product
{
    public override string GetDescription()
    {
        return "Это товар из категории часы";
    }
    public Watch(string name, decimal price) : base(name, price) { }

    public override string ToString()
    {
        return $"Часы: {base.ToString()}";
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
        Product cake = new Cake("Нападеон", 10);
        Product candy = new Candy("Шипучка", 1);
        Product flowers = new Flowers("Тюльпаны", 12);
        Product watch = new Watch("Красная цена", 5000);

        Product[] products = { cake, candy, flowers, watch };

        Printer printer = new Printer();
        foreach (var product in products)
        {
            printer.IAmPrinting(product);
        }

        foreach (var product in products)
        {
            if (product is ICloneable cloneable)
            {
                Console.WriteLine($"{product.Name} можно клонировать: {cloneable.DoClone()}");
            }
        }
        foreach (var product in products)
        {
            if (product is BaseClone cloneable)
            {
                Console.WriteLine($"{product.Name} можно клонировать: {cloneable.DoClone()}");
            }
        }
    }
}