using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Newtonsoft.Json;

public interface ISerializer
{
    string Serialize(object obj);
    T Deserialize<T>(string data);
}

[Serializable]
public class Country
{
    public string Name { get; set; }
    public int Rank { get; set; }
    [NonSerialized]
    public string Password;

    public Country() { } 

    public Country(string name, int number, string password)
    {
        Name = name;
        Rank = number;
        Password = password;
    }
}

[Serializable]
public class CountryList
{
    public Country[] Countries { get; set; }

    public CountryList()
    {
        Countries = new Country[0];
    }

    public static CountryList FromList(List<Country> countryList)
    {
        return new CountryList { Countries = countryList.ToArray() };
    }

    public List<Country> ToList()
    {
        return new List<Country>(Countries);
    }
}

public class JsonSerializer : ISerializer
{
    public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

    public T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);
}

public class XmlSerializer : ISerializer
{
    public string Serialize(object obj)
    {
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(obj.GetType());
        var stringWriter = new StringWriter();
        xmlSerializer.Serialize(stringWriter, obj);
        return stringWriter.ToString();
    }

    public T Deserialize<T>(string data)
    {
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        var stringReader = new StringReader(data);
        return (T)xmlSerializer.Deserialize(stringReader);
    }
}

public class SoapSerializer : ISerializer
{
    public string Serialize(object obj)
    {
        using (var memoryStream = new MemoryStream())
        {
            IFormatter formatter = new SoapFormatter();
            formatter.Serialize(memoryStream, obj);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }

    public T Deserialize<T>(string data)
    {
        var bytes = Convert.FromBase64String(data);
        using (var memoryStream = new MemoryStream(bytes))
        {
            IFormatter formatter = new SoapFormatter();
            return (T)formatter.Deserialize(memoryStream);
        }
    }
}


public class Program
{
    public static void Main()
    {
        var countryList = new List<Country>
        {
            new Country("Russia", 10, "secret_password"),
            new Country("Belarus", 20, "another_password"),
            new Country("China", 5, "mypassword")
        };

        var wrappedCountryList = CountryList.FromList(countryList);

        ISerializer soapSerializer = new SoapSerializer();
        string soapData = soapSerializer.Serialize(wrappedCountryList);
        Console.WriteLine("SOAP Serialized: " + soapData);

        File.WriteAllText("countrys.soap", soapData);

        var deserializedCountryList = soapSerializer.Deserialize<CountryList>(soapData);
        Console.WriteLine("Deserialized Countries from SOAP:");

        foreach (var country in deserializedCountryList.ToList())
        {
            Console.WriteLine($"Name: {country.Name}, Rank: {country.Rank}, Password: {country.Password}");
        }

        ISerializer jsonSerializer = new JsonSerializer();
        string jsonData = jsonSerializer.Serialize(countryList);
        Console.WriteLine("JSON Serialized: " + jsonData);

        File.WriteAllText("countrys.json", jsonData);

        string jsonFromFile = File.ReadAllText("countrys.json");
        var deserializedPeopleJson = jsonSerializer.Deserialize<List<Country>>(jsonFromFile);

        Console.WriteLine("Deserialized from JSON:");
        foreach (var countrie in deserializedPeopleJson)
        {
            Console.WriteLine($"{countrie.Name}, {countrie.Rank}");
        }

        ISerializer xmlSerializer = new XmlSerializer();
        string xmlData = xmlSerializer.Serialize(countryList);
        Console.WriteLine("XML Serialized: " + xmlData);

        File.WriteAllText("countrys.xml", xmlData);

        string xmlFromFile = File.ReadAllText("countrys.xml");
        var deserializedPeopleXml = xmlSerializer.Deserialize<List<Country>>(xmlFromFile);

        Console.WriteLine("Deserialized from XML:");
        foreach (var countrie in deserializedPeopleXml)
        {
            Console.WriteLine($"{countrie.Name}, {countrie.Rank}");
        }

        string filePath = "countrys.xml";
        using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        using (var writer = new StreamWriter(fs, new System.Text.UnicodeEncoding()))
        {
            writer.Write("<?xml version=\"1.0\" encoding=\"UTF-16\"?>");
            writer.Write("<Countrys>");
            writer.Write("<Country>");
            writer.Write("<Name>Russia</Name>");
            writer.Write("<Rank>10</Rank>");
            writer.Write("<Password>secret_password</Password>");
            writer.Write("</Country>");
            writer.Write("</Countrys>");
        }


        XDocument xdoc;
        try
        {
            xdoc = XDocument.Load(filePath);

            var countryNames = xdoc.XPathSelectElements("//Country/Name");
            Console.WriteLine("Все названия стран:");
            foreach (var name in countryNames)
            {
                Console.WriteLine(name.Value);
            }


            var countriesWithHighRank = xdoc.XPathSelectElements("//Country[Rank > 5]/Name");
            Console.WriteLine("\nСтраны с рангом больше 5:");
            foreach (var name in countriesWithHighRank)
            {
                Console.WriteLine(name.Value);
            }
        }
        catch (XmlException ex)
        {
            Console.WriteLine("Ошибка при загрузке XML: " + ex.Message);
        }

        var states = new XElement("Countrys",
            new XElement("Country",
                new XElement("Name", "Spane"),
                new XElement("Rank", 10),
                new XElement("Password", "secret_password")),
            new XElement("Country",
                new XElement("Name", "Belarus"),
                new XElement("Rank", 20),
                new XElement("Password", "another_password")),
            new XElement("Country",
                new XElement("Name", "China"),
                new XElement("Age", 5),
                new XElement("Password", "mypassword")
            )
        );


        states.Save("countrys.xml");


        var names = from state in states.Elements("Country")
                    select state.Element("Name").Value;

        Console.WriteLine("\nAll names:");
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }


        var worserThan15 = from state in states.Elements("Country")
                           let rank = (int)state.Element("Rank")
                           where rank > 15
                           select state.Element("Name").Value;

        Console.WriteLine("Rank worser than 15:");
        foreach (var name in worserThan15)
        {
            Console.WriteLine(name);
        }


    }
}