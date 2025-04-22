using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP_Lab15
{
    static partial class TPL
    {
        public static async Task Task8()
        {

            List<Product> data = new List<Product>();

            await Task.Run(() => data = GetProducts());
            Console.WriteLine("выполнение с await");

            foreach (var product in data)
            {
                Console.WriteLine(product);
            }
        }

        public static List<Product> GetProducts()
        {
            List<Product> list = new List<Product>();

            using (StreamReader sr = new StreamReader("products.json"))
            {
                int cnt = 0;
                string json;
                while (!sr.EndOfStream && cnt++ < 200)
                {
                    json = sr.ReadLine();
                    if (json != null)
                    {
                        Product product = JsonConvert.DeserializeObject<Product>(json);
                        list.Add(product);
                    }

                    Thread.Sleep(300);
                }
            }

            return list;
        }
    }
}