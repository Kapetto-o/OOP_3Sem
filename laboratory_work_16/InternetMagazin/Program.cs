using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopSystem
{
    public enum Role
    {
        Customer,
        Admin
    }

    public class Program
    {
        static void Main(string[] args)
        {
            ShopSystem shopSystem = new ShopSystem();
            shopSystem.Run();
        }
    }

    public class ShopSystem
    {
        private List<User> Users = new List<User>();
        private List<Product> Products = new List<Product>();
        private List<Order> Orders = new List<Order>();
        private List<string> Blacklist = new List<string>();
        private User LoggedInUser = null;
        private int orderCounter = 1;

        public ShopSystem()
        {
            Users.Add(new User
            {
                Name = "Администратор",
                Email = "admin",
                Balance = 0,
                Role = Role.Admin,
                Orders = new List<Order>()
            });

            Products.Add(new Product { ProductId = 1, Name = "Корпус", Price = 100, Stock = 10 });
            Products.Add(new Product { ProductId = 2, Name = "Блок питания", Price = 500, Stock = 20 });
            Products.Add(new Product { ProductId = 3, Name = "Материнская плата", Price = 500, Stock = 20 });
            Products.Add(new Product { ProductId = 4, Name = "Процессор", Price = 300, Stock = 30 });
            Products.Add(new Product { ProductId = 5, Name = "Видеокарта", Price = 800, Stock = 20 });
            Products.Add(new Product { ProductId = 6, Name = "Оперативная память", Price = 100, Stock = 20 });
            Products.Add(new Product { ProductId = 7, Name = "SSD", Price = 200, Stock = 20 });
            Products.Add(new Product { ProductId = 8, Name = "Жёсткий диск", Price = 100, Stock = 20 });
            Products.Add(new Product { ProductId = 9, Name = "Кулер", Price = 50, Stock = 20 });
            Products.Add(new Product { ProductId = 10, Name = "Вентилятор", Price = 50, Stock = 20 });
            Products.Add(new Product { ProductId = 11, Name = "Монитор", Price = 500, Stock = 10 });
            Products.Add(new Product { ProductId = 12, Name = "Мышь", Price = 100, Stock = 10 });
            Products.Add(new Product { ProductId = 13, Name = "Клавиатура", Price = 100, Stock = 10 });
            Products.Add(new Product { ProductId = 14, Name = "Коврик для мыши", Price = 50, Stock = 10 });
            Products.Add(new Product { ProductId = 15, Name = "Колонки", Price = 100, Stock = 10 });
            Products.Add(new Product { ProductId = 16, Name = "Наушники", Price = 100, Stock = 10 });

        }

        public void Run()
        {
            while (true)
            {
                if (LoggedInUser == null)
                {
                    Console.WriteLine("1. Войти (Sign In)");
                    Console.WriteLine("2. Зарегистрироваться (Sign Up)");
                    Console.WriteLine("3. Выход (Exit)");
                    Console.Write("Выберите опцию: ");
                    var input = Console.ReadLine();
                    Console.Clear();

                    if (input == "1")
                        SignIn();
                    else if (input == "2")
                        SignUp();
                    else if (input == "3")
                        Environment.Exit(0);
                    else
                        Console.WriteLine("Неверный ввод. Попробуйте снова.");
                }
                else
                {
                    if (LoggedInUser.Role == Role.Admin)
                        AdminMenu();
                    else
                    {
                        if (Blacklist.Contains(LoggedInUser.Email))
                        {
                            Console.WriteLine("Ваш аккаунт находится в черном списке. Доступ запрещен.");
                            LoggedInUser = null;
                            continue;
                        }
                        CustomerMenu();
                    }
                }
            }
        }

        private void SignIn()
        {
            Console.Write("Введите email: ");
            string email = Console.ReadLine();

            var user = Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                LoggedInUser = user;
                Console.WriteLine($"Добро пожаловать, {user.Name}!");
            }
            else
                Console.WriteLine("Пользователь не найден. Проверьте email или зарегистрируйтесь.");

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void SignUp()
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите email: ");
            string email = Console.ReadLine();

            if (Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Пользователь с таким email уже существует.");
            }
            else
            {
                User newUser = new User
                {
                    Name = name,
                    Email = email,
                    Balance = 10000,
                    Role = Role.Customer,
                    Orders = new List<Order>()
                };
                Users.Add(newUser);
                LoggedInUser = newUser;
                Console.WriteLine("Регистрация прошла успешно!");
            }
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void CustomerMenu()
        {
            Console.WriteLine("1. Просмотр каталога товаров");
            Console.WriteLine("2. Создать заказ");
            Console.WriteLine("3. Отменить заказ");
            Console.WriteLine("4. Просмотреть мои заказы");
            Console.WriteLine("5. Выход (Sign Out)");
            Console.Write("Выберите опцию: ");
            var input = Console.ReadLine();
            Console.Clear();

            if (input == "1")
                ViewProducts();
            else if (input == "2")
                CreateOrder();
            else if (input == "3")
                CancelOrder();
            else if (input == "4")
                ViewMyOrders();
            else if (input == "5")
            {
                LoggedInUser = null;
                Console.WriteLine("Вы вышли из системы.");
            }
            else
                Console.WriteLine("Неверный ввод.");
        }

        private void AdminMenu()
        {
            Console.WriteLine("Администратор - меню:");
            Console.WriteLine("1. Просмотр каталога товаров");
            Console.WriteLine("2. Добавить товар");
            Console.WriteLine("3. Изменить товар");
            Console.WriteLine("4. Удалить товар");
            Console.WriteLine("5. Просмотр всех заказов");
            Console.WriteLine("6. Добавить пользователя в черный список");
            Console.WriteLine("7. Просмотр черного списка");
            Console.WriteLine("8. Выход (Sign Out)");
            Console.Write("Выберите опцию: ");
            var input = Console.ReadLine();
            Console.Clear();

            if (input == "1")
                ViewProducts();
            else if (input == "2")
                AddProduct();
            else if (input == "3")
                ModifyProduct();
            else if (input == "4")
                DeleteProduct();
            else if (input == "5")
                ViewAllOrders();
            else if (input == "6")
                AddUserToBlacklist();
            else if (input == "7")
                ViewBlacklist();
            else if (input == "8")
            {
                LoggedInUser = null;
                Console.WriteLine("Вы вышли из системы.");
            }
            else
                Console.WriteLine("Неверный ввод.");
        }

        private void ViewProducts()
        {
            Console.WriteLine("Каталог товаров:");
            foreach (var product in Products)
            {
                Console.WriteLine($"ID: {product.ProductId}, Название: {product.Name}, Цена: ${product.Price}, В наличии: {product.Stock}");
            }
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void CreateOrder()
        {
            Console.WriteLine("Создание нового заказа:");
            List<OrderItem> orderItems = new List<OrderItem>();
            bool adding = true;
            while (adding)
            {
                ViewProducts();
                Console.Write("Введите ID товара для добавления в заказ: ");
                if (int.TryParse(Console.ReadLine(), out int productId))
                {
                    var product = Products.FirstOrDefault(p => p.ProductId == productId);
                    if (product != null)
                    {
                        Console.Write($"Введите количество для товара \"{product.Name}\": ");
                        if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                        {
                            if (product.Stock >= quantity)
                            {
                                OrderItem existingItem = orderItems.FirstOrDefault(i => i.Product.ProductId == product.ProductId);
                                if (existingItem != null)
                                {
                                    existingItem.Quantity += quantity;
                                    existingItem.Subtotal += product.Price * quantity;
                                }
                                else
                                {
                                    orderItems.Add(new OrderItem
                                    {
                                        Product = product,
                                        Quantity = quantity,
                                        Subtotal = product.Price * quantity
                                    });
                                }
                                Console.Write("Добавить еще товар? (Y/N): ");
                                string answer = Console.ReadLine();
                                if (!answer.Equals("Y", StringComparison.OrdinalIgnoreCase))
                                    adding = false;
                            }
                            else
                                Console.WriteLine("Недостаточно товара на складе.");
                        }
                        else
                            Console.WriteLine("Неверное количество.");
                    }
                    else
                        Console.WriteLine("Товар не найден.");
                }
                else
                    Console.WriteLine("Неверный ввод.");

                Console.Clear();
            }

            if (orderItems.Count == 0)
            {
                Console.WriteLine("Заказ не создан, так как не было добавлено товаров.");
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            decimal totalAmount = orderItems.Sum(item => item.Subtotal);
            Console.WriteLine($"Общая сумма заказа: ${totalAmount}");
            Console.WriteLine($"Ваш текущий баланс: ${LoggedInUser.Balance}");
            if (LoggedInUser.Balance < totalAmount)
            {
                Console.WriteLine("Недостаточно средств для оплаты заказа.");
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            Console.Write("Подтвердить заказ? (Y/N): ");
            string confirm = Console.ReadLine();
            if (confirm.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var item in orderItems)
                {
                    item.Product.Stock -= item.Quantity;
                }
                LoggedInUser.Balance -= totalAmount;

                Order newOrder = new Order
                {
                    OrderId = orderCounter++,
                    User = LoggedInUser,
                    Items = orderItems,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount
                };
                Orders.Add(newOrder);
                LoggedInUser.Orders.Add(newOrder);

                Console.WriteLine("Заказ успешно создан и оплачен.");
            }
            else
                Console.WriteLine("Заказ отменен.");

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void CancelOrder()
        {
            if (LoggedInUser.Orders.Count == 0)
            {
                Console.WriteLine("У вас нет заказов для отмены.");
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            Console.WriteLine("Ваши заказы:");
            foreach (var order in LoggedInUser.Orders)
            {
                Console.WriteLine($"Заказ ID: {order.OrderId}, Сумма: ${order.TotalAmount}, Дата: {order.OrderDate}");
            }
            Console.Write("Введите ID заказа, который хотите отменить: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                var order = LoggedInUser.Orders.FirstOrDefault(o => o.OrderId == orderId);
                if (order != null)
                {
                    foreach (var item in order.Items)
                    {
                        var product = Products.FirstOrDefault(p => p.ProductId == item.Product.ProductId);
                        if (product != null)
                            product.Stock += item.Quantity;
                    }
                    LoggedInUser.Balance += order.TotalAmount;
                    Orders.Remove(order);
                    LoggedInUser.Orders.Remove(order);
                    Console.WriteLine("Заказ отменен и средства возвращены.");
                }
                else
                    Console.WriteLine("Заказ не найден.");
            }
            else
                Console.WriteLine("Неверный ввод.");

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void ViewMyOrders()
        {
            if (LoggedInUser.Orders.Count == 0)
            {
                Console.WriteLine("У вас нет заказов.");
            }
            else
            {
                Console.WriteLine("Ваши заказы:");
                foreach (var order in LoggedInUser.Orders)
                {
                    Console.WriteLine($"Заказ ID: {order.OrderId}, Дата: {order.OrderDate}, Сумма: ${order.TotalAmount}");
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"\tТовар: {item.Product.Name}, Количество: {item.Quantity}, Сумма: ${item.Subtotal}");
                    }
                }
            }
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void AddProduct()
        {
            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();
            Console.Write("Введите цену товара: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Неверная цена.");
                return;
            }
            Console.Write("Введите количество товара на складе: ");
            if (!int.TryParse(Console.ReadLine(), out int stock))
            {
                Console.WriteLine("Неверное количество.");
                return;
            }
            int newProductId = Products.Count > 0 ? Products.Max(p => p.ProductId) + 1 : 1;
            Products.Add(new Product { ProductId = newProductId, Name = name, Price = price, Stock = stock });
            Console.WriteLine("Товар успешно добавлен.");
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void ModifyProduct()
        {
            ViewProducts();
            Console.Write("Введите ID товара, который хотите изменить: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                var product = Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    Console.Write("Введите новое название (оставьте пустым для сохранения текущего): ");
                    string name = Console.ReadLine();
                    Console.Write("Введите новую цену (оставьте пустым для сохранения текущей): ");
                    string priceInput = Console.ReadLine();
                    Console.Write("Введите новое количество (оставьте пустым для сохранения текущего): ");
                    string stockInput = Console.ReadLine();

                    if (!string.IsNullOrEmpty(name))
                        product.Name = name;
                    if (decimal.TryParse(priceInput, out decimal newPrice))
                        product.Price = newPrice;
                    if (int.TryParse(stockInput, out int newStock))
                        product.Stock = newStock;

                    Console.WriteLine("Товар успешно изменен.");
                }
                else
                    Console.WriteLine("Товар не найден.");
            }
            else
                Console.WriteLine("Неверный ввод.");

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void DeleteProduct()
        {
            ViewProducts();
            Console.Write("Введите ID товара, который хотите удалить: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                var product = Products.FirstOrDefault(p => p.ProductId == productId);
                if (product != null)
                {
                    Products.Remove(product);
                    Console.WriteLine("Товар успешно удален.");
                }
                else
                    Console.WriteLine("Товар не найден.");
            }
            else
                Console.WriteLine("Неверный ввод.");

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void ViewAllOrders()
        {
            if (Orders.Count == 0)
            {
                Console.WriteLine("Нет заказов.");
            }
            else
            {
                Console.WriteLine("Все заказы:");
                foreach (var order in Orders)
                {
                    Console.WriteLine($"Заказ ID: {order.OrderId}, Клиент: {order.User.Name} ({order.User.Email}), Дата: {order.OrderDate}, Сумма: ${order.TotalAmount}");
                    foreach (var item in order.Items)
                    {
                        Console.WriteLine($"\tТовар: {item.Product.Name}, Количество: {item.Quantity}, Сумма: ${item.Subtotal}");
                    }
                }
            }
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void AddUserToBlacklist()
        {
            Console.Write("Введите email пользователя для добавления в черный список: ");
            string email = Console.ReadLine();
            var user = Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                if (!Blacklist.Contains(user.Email))
                {
                    Blacklist.Add(user.Email);
                    Console.WriteLine("Пользователь добавлен в черный список.");
                }
                else
                    Console.WriteLine("Пользователь уже находится в черном списке.");
            }
            else
                Console.WriteLine("Пользователь не найден.");

            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }

        private void ViewBlacklist()
        {
            Console.WriteLine("Черный список пользователей:");
            if (Blacklist.Count == 0)
                Console.WriteLine("Черный список пуст.");
            else
            {
                foreach (var email in Blacklist)
                    Console.WriteLine(email);
            }
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public Role Role { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public User User { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
