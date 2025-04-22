using System;
using System.Diagnostics;

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
        Debug.Assert(weight > 0, "Вес не может быть отрицательным или нулевым.");
        Debug.Assert(height > 0 && width > 0 && depth >0, "Размеры не могут быть отрицательными или нулевыми.");
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
    public override object DoClone()
    {
        return MemberwiseClone();
    }
    object ICloneable.Clone()
    {
        return DoClone();
    }
}