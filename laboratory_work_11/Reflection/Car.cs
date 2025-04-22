using System;

public partial class Car
{
    private readonly int id;
    private string brand;
    private string model;
    private int year;
    private string color;
    private decimal price;
    private string registrationNumber;
    private const int CurrentYear = 2024;
    private static int carCount;
    static Car()
    {
        carCount = 0;
    }
    private Car(int id)
    {
        this.id = id;
        carCount++;
    }
    public Car()
    {
        id = GetHashCode();
        carCount++;
    }
    public Car(string brand, string model, int year, string color, decimal price, string registrationNumber)
    {
        this.id = GetHashCode();
        Brand = brand;
        Model = model;
        Year = year;
        Color = color;
        Price = price;
        RegistrationNumber = registrationNumber;
        carCount++;
    }
    public string Brand
    {
        get => brand;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Марка не должна быть пустой.");
            brand = value;
        }
    }
    public string Model
    {
        get => model;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Модель не должна быть пустой.");
            model = value;
        }
    }
    public int Year
    {
        get => year;
        set
        {
            if (value < 1886 || value > CurrentYear)
                throw new ArgumentException("Неверно указан год выпуска.");
            year = value;
        }
    }
    public string Color
    {
        get => color;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Цвет не должен быть пустым.");
            color = value;
        }
    }
    public decimal Price
    {
        get => price;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Цена должна быть больше 0.");
            price = value;
        }
    }
    public string RegistrationNumber
    {
        get => registrationNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Регистрационный номер не должен быть пустым.");
            registrationNumber = value;
        }
    }
    public int GetCarAge()
    {
        return CurrentYear - year;
    }
    public override string ToString()
    {
        return $"ID: {id}, Марка: {brand}, Модель: {model}, Год выпуска: {year}, Цвет: {color}, Цена: {price}, Регистрационный номер: {registrationNumber}";
    }
    public override bool Equals(object obj)
    {
        if (obj is Car car)
        {
            return id == car.id;
        }
        return false;
    }
    public override int GetHashCode()
    {
        return (brand?.GetHashCode() ?? 0) ^ (model?.GetHashCode() ?? 0) ^ year.GetHashCode() ^ (registrationNumber?.GetHashCode() ?? 0);
    }
    public static void ShowClassInfo()
    {
        Console.WriteLine($"Класс Car, создано объектов: {carCount}");
    }
}

public partial class Car
{
    public static void ShowCarsByBrand(Car[] cars, string brand)
    {
        foreach (var car in cars)
        {
            if (car.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(car);
            }
        }
    }
    public static void ShowCarsByModelAndAge(Car[] cars, string model, int n)
    {
        foreach (var car in cars)
        {
            if (car.Model.Equals(model, StringComparison.OrdinalIgnoreCase) && car.GetCarAge() > n)
            {
                Console.WriteLine(car);
            }
        }
    }
}