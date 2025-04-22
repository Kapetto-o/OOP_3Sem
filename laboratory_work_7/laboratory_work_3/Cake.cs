using System;

public class Cake : Product, IComparable<Cake>
{
    public Cake(string name, decimal price) : base(name, price) { }

    public override string GetDescription()
    {
        return "Торт из кондитерских изделий.";
    }
    public override string ToString()
    {
        return "Торт: " + base.ToString();
    }

    public int CompareTo(Cake other)
    {
        if (other == null) return 1;
        return Price.CompareTo(other.Price);
    }
}