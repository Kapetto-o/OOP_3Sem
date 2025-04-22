public abstract class Product
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
        return "Продукт без описания.";
    }
    public override string ToString()
    {
        return $"{Name}, Цена: {Price}";
    }
}