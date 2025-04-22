using System;

public enum ProductCategory
{
    General,
    Confectionery,
    Cake,
    Candy,
    Flowers,
    Watch
}
public struct ProductDetails
{
    public double Weight { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Depth { get; set; }

    public ProductDetails(double weight, double height, double width, double depth)
    {
        Weight = weight;
        Height = height;
        Width = width;
        Depth = depth;
    }
    public override string ToString()
    {
        return $"Вес: {Weight} кг, Размеры (ВxШxГ): {Height}x{Width}x{Depth} см";
    }
}
public abstract partial class Product : BaseClone, ICloneable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ProductCategory Category { get; set; }
    public ProductDetails Details { get; set; }

    public Product(string name, decimal price, ProductCategory category, ProductDetails details)
    {
        Name = name;
        Price = price;
        Category = category;
        Details = details;
    }
    public abstract string GetDescription();
    public override bool DoClone()
    {
        return false;
    }

    bool ICloneable.DoClone()
    {
        return true;
    }
}