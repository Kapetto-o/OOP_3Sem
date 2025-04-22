using System;

class Program
{
    static void Main()
    {
        var intCollection = new CollectionType<int>();
        intCollection.Add(5);
        intCollection.Add(10);
        intCollection.Add(15);
        intCollection.ViewAll();
        intCollection.Remove(10);
        intCollection.ViewAll();

        var doubleCollection = new CollectionType<double>();
        doubleCollection.Add(1.11515);
        doubleCollection.Add(2.51612);
        doubleCollection.Add(3.31515);
        doubleCollection.ViewAll();
        doubleCollection.Remove(3.31515);
        doubleCollection.ViewAll();

        var cakeCollection = new CollectionType<Cake>();
        cakeCollection.Add(new Cake("Наполеон", 15.99m));
        cakeCollection.Add(new Cake("Медовик", 12.50m));
        cakeCollection.Add(new Cake("Шоколадный", 20.00m));
        cakeCollection.ViewAll();

        Cake foundCake = cakeCollection.FindByPredicate(c => c.Price > 15);
        Console.WriteLine($"Найденный торт: {foundCake}");

        string filePath = "cakes.json";
        cakeCollection.SaveToJson(filePath);

        var loadedCakeCollection = new CollectionType<Cake>();
        loadedCakeCollection.LoadFromJson(filePath);
        loadedCakeCollection.ViewAll();

        var watchCollection = new CollectionType<Watch>();
        watchCollection.Add(new Watch("Casio", 50.00m));
        watchCollection.Add(new Watch("Rolex", 10000.00m));
        watchCollection.ViewAll();

        string watchFilePath = "watches.json";
        watchCollection.SaveToJson(watchFilePath);

        var loadedWatchCollection = new CollectionType<Watch>();
        loadedWatchCollection.LoadFromJson(watchFilePath);
        loadedWatchCollection.ViewAll();
    }
}