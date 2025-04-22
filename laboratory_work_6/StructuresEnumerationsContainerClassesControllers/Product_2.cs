using System;

public partial class Product
{
    public override string ToString()
    {
        return $"{GetType().Name}: Название = {Name}, Цена = {Price} руб., Категория = {Category}, {Details}";
    }
}