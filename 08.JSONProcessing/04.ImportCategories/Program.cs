using Newtonsoft.Json;
using System;
using System.IO;
using ProductShop.Data;
using ProductShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();

            string inputJson = File.ReadAllText("../../../Datasets/categories.json");
            Console.WriteLine(ImportCategories(context, inputJson));
        }

        public static string ImportCategories(ProductShopContext context, string inputJson)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(inputJson)
                .Where(x => x.Name != null);

            context.Categories.AddRange(categories);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
