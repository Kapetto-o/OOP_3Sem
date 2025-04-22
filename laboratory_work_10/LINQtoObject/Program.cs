using System;
using System.Collections.Generic;
using System.Linq;

public partial class Car : IComparable<Car>
{
    private readonly int _id;
    private string _brand;
    private string _model;
    private int _year;
    private string _color;
    private double _price;
    private string _registrationNumber;
    public const int MaxYear = 2025;
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

    public int CompareTo(Car other)
    {
        if (other == null) return 1;
        return this.Year.CompareTo(other.Year);
    }
}

public partial class Car
{
    public string GetColor()
    {
        return Color;
    }
}

public class Owner
{
    public string RegistrationNumber { get; set; }
    public string Name { get; set; }
}

class Program
{
    static void Main()
    {
        ExecuteAssignment1();
        List<Car> carList = ExecuteAssignment2();
        ExecuteAssignment3(carList);
        ExecuteAssignment4(carList);
        ExecuteAssignment5(carList);
    }

    static void ExecuteAssignment1()
    {
        Console.WriteLine("ЗАДАНИЕ #1");

        string[] months = {"June", "July", "May", "December", "January", "February", "March", "April", "August", "September", "October", "November"};

        int n = 4;

        var monthsWithLengthN = months.Where(month => month.Length == n);
        Console.WriteLine($"Месяцы с длиной {n}: {string.Join(", ", monthsWithLengthN)}");

        string[] summerMonths = { "June", "July", "August" };
        string[] winterMonths = { "December", "January", "February" };
        var summerAndWinter = months.Where(month => summerMonths.Contains(month) || winterMonths.Contains(month));
        Console.WriteLine($"Летние и зимние месяцы: {string.Join(", ", summerAndWinter)}");

        var monthsAlphabetical = months.OrderBy(month => month);
        Console.WriteLine($"Месяцы в алфавитном порядке: {string.Join(", ", monthsAlphabetical)}");

        var monthsWithUAndLength = months.Where(month => month.Contains('u') && month.Length >= 4);
        Console.WriteLine($"Месяцы с 'u' и длиной >=4: {string.Join(", ", monthsWithUAndLength)}");
    }

    static List<Car> ExecuteAssignment2()
    {
        Console.WriteLine("\nЗАДАНИЕ #2");

        List<Car> carList = new List<Car>
        {
            new Car("Toyota", "Camry", 2015, "Белый", 15000, "A123BC"),
            new Car("Honda", "Civic", 2012, "Черный", 10000, "B456CD"),
            new Car("Toyota", "Corolla", 2018, "Синий", 13000, "C789DE"),
            new Car("Honda", "Accord", 2010, "Красный", 9000, "D012EF"),
            new Car("BMW", "X5", 2020, "Серый", 40000, "E345FG"),
            new Car("Audi", "A4", 2016, "Белый", 20000, "F678GH"),
            new Car("Mercedes", "C-Class", 2014, "Черный", 25000, "G901HI"),
            new Car("Ford", "Focus", 2013, "Синий", 8000, "H234IJ"),
            new Car("Tesla", "Model 3", 2022, "Красный", 35000, "I567JK"),
            new Car("Volkswagen", "Golf", 2011, "Зеленый", 7000, "J890KL")
        };

        Console.WriteLine("Коллекция List<Car> заполнена 10 элементами.");
        Car.ShowClassInfo();

        string brandToFind = "Honda";
        var hondaCars = from car in carList
                        where car.Brand == brandToFind
                        select car;

        Console.WriteLine($"\nАвтомобили марки {brandToFind}:");
        foreach (var car in hondaCars)
        {
            car.DisplayInfo();
        }

        string modelToFind = "Civic";
        int n = 8;
        var specificCars = carList
                            .Where(car => car.Model == modelToFind && car.GetCarAge() > n)
                            .ToList();

        Console.WriteLine($"\nАвтомобили модели {modelToFind}, эксплуатирующиеся более {n} лет:");
        foreach (var car in specificCars)
        {
            car.DisplayInfo();
        }

        return carList;
    }

    static void ExecuteAssignment3(List<Car> carList)
    {
        Console.WriteLine("\nЗАДАНИЕ #3");

        string colorToCount = "Черный";
        double minPrice = 8000;
        double maxPrice = 20000;

        var countCars = carList.Count(car => car.Color == colorToCount && car.Price >= minPrice && car.Price <= maxPrice);
        Console.WriteLine($"\nКоличество автомобилей цвета {colorToCount} в диапазоне цены {minPrice}-{maxPrice}: {countCars}");

        var oldestCar = carList.OrderBy(car => car.Year).FirstOrDefault();
        Console.WriteLine("\nСамый старый автомобиль:");
        oldestCar?.DisplayInfo();

        var newestFiveCars = carList.OrderByDescending(car => car.Year).Take(5);
        Console.WriteLine("\nПервые пять самых новых автомобилей:");
        foreach (var car in newestFiveCars)
        {
            car.DisplayInfo();
        }

        var sortedByPrice = carList.OrderBy(car => car.Price).ToArray();
        Console.WriteLine("\nАвтомобили, упорядоченные по цене:");
        foreach (var car in sortedByPrice)
        {
            car.DisplayInfo();
        }
    }

    static void ExecuteAssignment4(List<Car> carList)
    {
        Console.WriteLine("\nЗАДАНИЕ #4");

        var customQuery = carList
            .Where(car => car.Price > 8000)
            .GroupBy(car => car.Brand)
            .Select(group => new
            {
                Brand = group.Key,
                CarCount = group.Count(),
                AveragePrice = group.Average(car => car.Price)
            })
            .OrderByDescending(g => g.CarCount)
            .ThenBy(g => g.AveragePrice)
            .Take(3);

        Console.WriteLine("\nСобственный LINQ-запрос:");
        foreach (var group in customQuery)
        {
            Console.WriteLine($"Марка: {group.Brand}, Количество: {group.CarCount}, Средняя цена: {group.AveragePrice}");
        }
    }

    static void ExecuteAssignment5(List<Car> carList)
    {
        Console.WriteLine("\nЗАДАНИЕ #5");

        List<Owner> ownerList = GetOwnerList();

        var carOwners = from car in carList
                        join owner in ownerList
                        on car.RegistrationNumber equals owner.RegistrationNumber
                        select new
                        {
                            car.Brand,
                            car.Model,
                            car.Year,
                            OwnerName = owner.Name
                        };

        Console.WriteLine("\nСписок автомобилей с их владельцами:");
        foreach (var item in carOwners)
        {
            Console.WriteLine($"Марка: {item.Brand}, Модель: {item.Model}, Год: {item.Year}, Владелец: {item.OwnerName}");
        }
    }

    static List<Owner> GetOwnerList()
    {
        return new List<Owner>
        {
            new Owner { RegistrationNumber = "A123BC", Name = "Иван Иванов" },
            new Owner { RegistrationNumber = "B456CD", Name = "Мария Петрова" },
            new Owner { RegistrationNumber = "C789DE", Name = "Сергей Смирнов" },
            new Owner { RegistrationNumber = "D012EF", Name = "Анна Кузнецова" },
            new Owner { RegistrationNumber = "E345FG", Name = "Петр Васильев" },
            new Owner { RegistrationNumber = "F678GH", Name = "Елена Новикова" },
            new Owner { RegistrationNumber = "G901HI", Name = "Дмитрий Морозов" },
            new Owner { RegistrationNumber = "H234IJ", Name = "Ольга Павлова" },
            new Owner { RegistrationNumber = "I567JK", Name = "Алексей Соколов" },
            new Owner { RegistrationNumber = "J890KL", Name = "Татьяна Лебедева" }
        };
    }
}