//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Serialization.Formatters.Soap;
//using System.Text;
//using System.Xml;
//using System.Xml.Linq;
//using System.Xml.Serialization;
//using System.Xml.XPath;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//// Новая иерархия классов лабораторной работы №4

//interface ICloneable
//{
//    bool DoClone();
//}

//abstract class BaseClone
//{
//    public abstract bool DoClone();
//}

//[Serializable]
//[XmlInclude(typeof(Cake))]
//[XmlInclude(typeof(Candy))]
//[XmlInclude(typeof(Flowers))]
//[XmlInclude(typeof(Watch))]
//abstract class Product : BaseClone, ICloneable
//{
//    public string Name { get; set; }
//    public decimal Price { get; set; }

//    // Поле, запрещённое для сериализации
//    [NonSerialized]
//    private string _internalCode;

//    [XmlIgnore]
//    [JsonIgnore]
//    public string InternalCode
//    {
//        get { return _internalCode; }
//        set { _internalCode = value; }
//    }

//    // Пустой конструктор для десериализации
//    protected Product() { }

//    public Product(string name, decimal price)
//    {
//        Name = name;
//        Price = price;
//        InternalCode = Guid.NewGuid().ToString(); // Пример инициализации
//    }

//    public virtual string GetDescription()
//    {
//        return "Продукт без категории";
//    }

//    public override bool DoClone()
//    {
//        return false;
//    }

//    bool ICloneable.DoClone()
//    {
//        return true;
//    }

//    public override string ToString()
//    {
//        return $"{GetType().Name}: Название = {Name}, Цена = {Price} руб.";
//    }
//}

//[Serializable]
//abstract class Item : Product
//{
//    protected Item() { }

//    public Item(string name, decimal price) : base(name, price) { }

//    public override string GetDescription()
//    {
//        return "Это товар общего назначения";
//    }
//}

//[Serializable]
//class Confectionery : Item
//{
//    protected Confectionery() { }

//    public Confectionery(string name, decimal price) : base(name, price) { }

//    public override string GetDescription()
//    {
//        return "Это товар из категории кондитерские изделия";
//    }

//    public override string ToString()
//    {
//        return $"Кондитерское изделие: {base.ToString()}";
//    }
//}

//[Serializable]
//class Cake : Confectionery
//{
//    protected Cake() { }

//    public Cake(string name, decimal price) : base(name, price) { }

//    public override string GetDescription()
//    {
//        return "Это товар из категории торты";
//    }

//    public override string ToString()
//    {
//        return $"Торт: {base.ToString()}";
//    }
//}

//[Serializable]
//class Candy : Confectionery
//{
//    protected Candy() { }

//    public Candy(string name, decimal price) : base(name, price) { }

//    public override string GetDescription()
//    {
//        return "Это товар из категории конфеты";
//    }

//    public override string ToString()
//    {
//        return $"Конфета: {base.ToString()}";
//    }
//}

//[Serializable]
//class Flowers : Item
//{
//    protected Flowers() { }

//    public Flowers(string name, decimal price) : base(name, price) { }

//    public override string GetDescription()
//    {
//        return "Это товар из категории цветы";
//    }

//    public override string ToString()
//    {
//        return $"Цветы: {base.ToString()}";
//    }
//}

//[Serializable]
//sealed class Watch : Product
//{
//    public Watch() { }

//    public Watch(string name, decimal price) : base(name, price) { }

//    public override string GetDescription()
//    {
//        return "Это товар из категории часы";
//    }

//    public override string ToString()
//    {
//        return $"Часы: {base.ToString()}";
//    }
//}

//class Printer
//{
//    public void IAmPrinting(BaseClone obj)
//    {
//        Console.WriteLine(obj.ToString());
//    }
//}

//// Класс-обёртка для сериализации списка продуктов
//[Serializable]
//[XmlRoot("Products")]
//public class ProductList
//{
//    [XmlArray("ProductItems")]
//    [XmlArrayItem("Product")]
//    public List<Product> Products { get; set; }

//    public ProductList()
//    {
//        Products = new List<Product>();
//    }

//    public ProductList(List<Product> products)
//    {
//        Products = products;
//    }
//}

//// Общий интерфейс для сериализаторов
//public interface ISerializer
//{
//    string Serialize(object obj);
//    T Deserialize<T>(string data);
//}

//// Сериализатор для JSON
//public class JsonSerializerImpl : ISerializer
//{
//    public string Serialize(object obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);

//    public T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);
//}

//// Сериализатор для XML
//public class XmlSerializerImpl : ISerializer
//{
//    public string Serialize(object obj)
//    {
//        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
//        using (var stringWriter = new StringWriter())
//        {
//            xmlSerializer.Serialize(stringWriter, obj);
//            return stringWriter.ToString();
//        }
//    }

//    public T Deserialize<T>(string data)
//    {
//        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
//        using (var stringReader = new StringReader(data))
//        {
//            return (T)xmlSerializer.Deserialize(stringReader);
//        }
//    }
//}

//// Сериализатор для SOAP
//public class SoapSerializerImpl : ISerializer
//{
//    public string Serialize(object obj)
//    {
//        using (var memoryStream = new MemoryStream())
//        {
//            IFormatter formatter = new SoapFormatter();
//            formatter.Serialize(memoryStream, obj);
//            return Convert.ToBase64String(memoryStream.ToArray());
//        }
//    }

//    public T Deserialize<T>(string data)
//    {
//        var bytes = Convert.FromBase64String(data);
//        using (var memoryStream = new MemoryStream(bytes))
//        {
//            IFormatter formatter = new SoapFormatter();
//            return (T)formatter.Deserialize(memoryStream);
//        }
//    }
//}

//// Сериализатор для Binary (используя BinaryFormatter, хотя он устарел)
//public class BinarySerializerImpl : ISerializer
//{
//    public string Serialize(object obj)
//    {
//        using (var memoryStream = new MemoryStream())
//        {
//            IFormatter formatter = new BinaryFormatter();
//            formatter.Serialize(memoryStream, obj);
//            return Convert.ToBase64String(memoryStream.ToArray());
//        }
//    }

//    public T Deserialize<T>(string data)
//    {
//        var bytes = Convert.FromBase64String(data);
//        using (var memoryStream = new MemoryStream(bytes))
//        {
//            IFormatter formatter = new BinaryFormatter();
//            return (T)formatter.Deserialize(memoryStream);
//        }
//    }
//}

//public class Program
//{
//    public static void Main()
//    {
//        // Создание объектов продуктов
//        Product cake = new Cake("Наполеон", 10);
//        Product candy = new Candy("Шипучка", 1);
//        Product flowers = new Flowers("Тюльпаны", 12);
//        Product watch = new Watch("Красная цена", 5000);

//        // Создание коллекции продуктов
//        List<Product> products = new List<Product> { cake, candy, flowers, watch };
//        ProductList productList = new ProductList(products);

//        // Создание экземпляра сериализаторов
//        List<ISerializer> serializers = new List<ISerializer>
//        {
//            new BinarySerializerImpl(),
//            new SoapSerializerImpl(),
//            new JsonSerializerImpl(),
//            new XmlSerializerImpl()
//        };

//        // Сериализация и запись в файлы
//        foreach (var serializer in serializers)
//        {
//            string serializedData = serializer.Serialize(productList);
//            string extension = GetExtension(serializer);
//            string fileName = $"products.{extension}";
//            File.WriteAllText(fileName, serializedData);
//            Console.WriteLine($"{extension.ToUpper()} Serialized and saved to {fileName}");
//        }

//        Console.WriteLine("\n--- Десериализация из файлов ---\n");

//        // Десериализация из файлов
//        foreach (var serializer in serializers)
//        {
//            string extension = GetExtension(serializer);
//            string fileName = $"products.{extension}";
//            if (File.Exists(fileName))
//            {
//                string data = File.ReadAllText(fileName);
//                ProductList deserializedList = serializer.Deserialize<ProductList>(data);
//                Console.WriteLine($"Deserialized from {extension.ToUpper()}:");
//                foreach (var product in deserializedList.Products)
//                {
//                    Console.WriteLine(product.ToString());
//                }
//                Console.WriteLine();
//            }
//            else
//            {
//                Console.WriteLine($"Файл {fileName} не найден.");
//            }
//        }

//        // Демонстрация отсутствия запрещённого члена (InternalCode)
//        Console.WriteLine("\n--- Проверка отсутствия InternalCode после сериализации ---\n");
//        string jsonSerializerData = serializers.OfType<JsonSerializerImpl>().First().Serialize(productList);
//        var deserializedJsonList = serializers.OfType<JsonSerializerImpl>().First().Deserialize<ProductList>(jsonSerializerData);
//        foreach (var product in deserializedJsonList.Products)
//        {
//            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, InternalCode: {(product.InternalCode ?? "null")}");
//        }

//        // Запись XML в файл для дальнейших операций
//        ISerializer xmlSerializer = serializers.OfType<XmlSerializerImpl>().First();
//        string xmlData = xmlSerializer.Serialize(productList);
//        string xmlFilePath = "products.xml";
//        File.WriteAllText(xmlFilePath, xmlData);
//        Console.WriteLine($"\nXML Serialized and saved to {xmlFilePath}");

//        // Использование XPath для выборки из XML
//        Console.WriteLine("\n--- XPath селекторы ---\n");
//        XDocument xdoc;
//        try
//        {
//            xdoc = XDocument.Load(xmlFilePath);

//            // Селектор 1: Все названия продуктов
//            var productNames = xdoc.XPathSelectElements("//Product/Name");
//            Console.WriteLine("Все названия продуктов:");
//            foreach (var name in productNames)
//            {
//                Console.WriteLine(name.Value);
//            }

//            // Селектор 2: Продукты с ценой выше 5
//            var expensiveProducts = xdoc.XPathSelectElements("//Product[Price > 5]/Name");
//            Console.WriteLine("\nПродукты с ценой больше 5:");
//            foreach (var name in expensiveProducts)
//            {
//                Console.WriteLine(name.Value);
//            }
//        }
//        catch (XmlException ex)
//        {
//            Console.WriteLine("Ошибка при загрузке XML: " + ex.Message);
//        }

//        // Использование LINQ to XML для создания нового XML-документа и выполнения запросов
//        Console.WriteLine("\n--- LINQ to XML ---\n");
//        var newXDocument = new XElement("Store",
//            new XElement("Products",
//                new XElement("Product",
//                    new XElement("Name", "Пирожное"),
//                    new XElement("Price", 15)
//                ),
//                new XElement("Product",
//                    new XElement("Name", "Мармелад"),
//                    new XElement("Price", 3)
//                )
//            )
//        );

//        newXDocument.Save("new_products.xml");
//        Console.WriteLine("Создан новый XML документ 'new_products.xml'.");

//        var queryNames = from product in newXDocument.Descendants("Product")
//                         select product.Element("Name").Value;

//        Console.WriteLine("\nВсе названия в новом документе:");
//        foreach (var name in queryNames)
//        {
//            Console.WriteLine(name);
//        }

//        var queryExpensive = from product in newXDocument.Descendants("Product")
//                             let price = (decimal)product.Element("Price")
//                             where price > 10
//                             select product.Element("Name").Value;

//        Console.WriteLine("\nПродукты с ценой больше 10 в новом документе:");
//        foreach (var name in queryExpensive)
//        {
//            Console.WriteLine(name);
//        }

//        // Использование LINQ to JSON для создания нового JSON-документа и выполнения запросов
//        Console.WriteLine("\n--- LINQ to JSON ---\n");
//        var newJsonObject = new
//        {
//            Store = new
//            {
//                Products = new[]
//                {
//                    new { Name = "Печенье", Price = 5 },
//                    new { Name = "Леденец", Price = 2 }
//                }
//            }
//        };

//        string newJsonData = JsonConvert.SerializeObject(newJsonObject, Formatting.Indented);
//        File.WriteAllText("new_products.json", newJsonData);
//        Console.WriteLine("Создан новый JSON документ 'new_products.json'.");

//        var parsedJson = JObject.Parse(newJsonData);
//        var jsonNames = parsedJson["Store"]["Products"].Select(p => p["Name"].ToString());

//        Console.WriteLine("\nВсе названия в новом JSON документе:");
//        foreach (var name in jsonNames)
//        {
//            Console.WriteLine(name);
//        }

//        var jsonExpensive = parsedJson["Store"]["Products"]
//            .Where(p => (decimal)p["Price"] > 3)
//            .Select(p => p["Name"].ToString());

//        Console.WriteLine("\nПродукты с ценой больше 3 в новом JSON документе:");
//        foreach (var name in jsonExpensive)
//        {
//            Console.WriteLine(name);
//        }

//        // Демонстрация использования Printer и клонирования
//        Console.WriteLine("\n--- Демонстрация Printer и клонирования ---\n");
//        Printer printer = new Printer();
//        foreach (var product in products)
//        {
//            printer.IAmPrinting(product);
//        }

//        foreach (var product in products)
//        {
//            if (product is ICloneable cloneable)
//            {
//                Console.WriteLine($"{product.Name} можно клонировать: {cloneable.DoClone()}");
//            }
//        }
//        foreach (var product in products)
//        {
//            if (product is BaseClone cloneable)
//            {
//                Console.WriteLine($"{product.Name} можно клонировать: {cloneable.DoClone()}");
//            }
//        }
//    }

//    // Вспомогательный метод для определения расширения файла по типу сериализатора
//    private static string GetExtension(ISerializer serializer)
//    {
//        return serializer switch
//        {
//            BinarySerializerImpl => "bin",
//            SoapSerializerImpl => "soap",
//            JsonSerializerImpl => "json",
//            XmlSerializerImpl => "xml",
//            _ => "txt"
//        };
//    }
//}