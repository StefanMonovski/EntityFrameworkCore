using Newtonsoft.Json;
using System;
using System.IO;
using ProductShop.Data;
using ProductShop.Models;
using System.Collections.Generic;

namespace ProductShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();

            string inputJson = File.ReadAllText("../../../Datasets/products.json");
            Console.WriteLine(ImportProducts(context, inputJson));
        }

        public static string ImportProducts(ProductShopContext context, string inputJson)
        {
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(inputJson);

            context.Products.AddRange(products);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
