using System;

public partial class Car
{
    private readonly int _id;
    private string _brand;
    private string _model;
    private int _year;
    private string _color;
    private double _price;
    private string _registrationNumber;
    public const int MaxYear = 2024;
    private static int _carCount;
    public string Brand
    {
        get => _brand;
        set => _brand = value ?? throw new ArgumentException("Марка не может быть null");
    }
    public string Model
    {
        get => _model;
        set => _model = value ?? throw new ArgumentException("Модель не может быть null");
    }
    public int Year
    {
        get => _year;
        set
        {
            if (value > MaxYear || value < 1800)
                throw new ArgumentException("Некорректный год выпуска");
            _year = value;
        }
    }
    public string Color
    {
        get => _color;
        set => _color = value ?? throw new ArgumentException("Цвет не может быть null");
    }
    public double Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new ArgumentException("Цена не может быть отрицательной");
            _price = value;
        }
    }
    public string RegistrationNumber
    {
        get => _registrationNumber;
        set => _registrationNumber = value;
    }
    static Car()
    {
        Console.WriteLine("Статический конструктор класса Car вызван");
    }
    public Car() : this("Неизвестная марка", "Неизвестная модель", 2024, "Неизвестный цвет", 0, "Нет номера") { }
    public Car(string brand, string model, int year, string color, double price, string registrationNumber)
    {
        _id = GenerateID();
        Brand = brand;
        Model = model;
        Year = year;
        Color = color;
        Price = price;
        RegistrationNumber = registrationNumber;
        _carCount++;
    }
    private Car(int id)
    {
        _id = id;
    }
    public static void ShowClassInfo()
    {
        Console.WriteLine($"Всего создано автомобилей: {_carCount}");
    }
    public int GetCarAge()
    {
        return MaxYear - Year;
    }
    public void DisplayInfo()
    {
        Console.WriteLine(this.ToString());
    }
    public void UpdatePrice(ref double newPrice, out double oldPrice)
    {
        oldPrice = _price;
        _price = newPrice;
    }
    public override bool Equals(object obj)
    {
        if (obj is Car other)
        {
            return _id == other._id;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
    public override string ToString()
    {
        return $"ID: {_id}, Марка: {Brand}, Модель: {Model}, Год: {Year}, Цвет: {Color}, Цена: {Price}, Рег. номер: {RegistrationNumber}";
    }
    private static int GenerateID()
    {
        return new Random().Next(100000, 999999);
    }
}
public partial class Car
{
    public string GetColor()
    {
        return Color;
    }
}
class Program
{
    static void Main()
    {
        Car[] cars = {
            new Car("Toyota", "Camry", 2015, "Белый", 15000, "A123BC"),
            new Car("Honda", "Civic", 2012, "Черный", 10000, "B456CD"),
            new Car("Toyota", "Corolla", 2018, "Синий", 13000, "C789DE"),
            new Car("Honda", "Accord", 2010, "Красный", 9000, "D012EF")
        };
        string brandToFind = "Honda";
        Console.WriteLine($"Автомобили марки {brandToFind}:");
        foreach (var car in cars)
        {
            if (car.Brand == brandToFind)
                car.DisplayInfo();
        }
        string modelToFind = "Civic";
        int n = 8;
        Console.WriteLine($"\nАвтомобили модели {modelToFind}, эксплуатирующиеся более {n} лет:");
        foreach (var car in cars)
        {
            if (car.Model == modelToFind && car.GetCarAge() > n)
                car.DisplayInfo();
        }
        var anonymousCar = new { Brand = "BMW", Model = "X5", Year = 2020, Price = 40000 };
        Console.WriteLine($"\nАнонимный автомобиль: {anonymousCar}");
    }
}