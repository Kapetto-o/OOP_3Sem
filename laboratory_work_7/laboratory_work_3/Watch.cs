using System;

public class Watch : Product, IComparable<Watch>
{
    public Watch(string name, decimal price) : base(name, price) { }

    public override string ToString()
    {
        return "Часы: " + base.ToString();
    }
    public int CompareTo(Watch other)
    {
        if (other == null) return 1;
        return Price.CompareTo(other.Price);
    }
}