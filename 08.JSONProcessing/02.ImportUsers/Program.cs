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

            string inputJson = File.ReadAllText("../../../Datasets/users.json");
            Console.WriteLine(ImportUsers(context, inputJson));
        }

        public static string ImportUsers(ProductShopContext context, string inputJson)
        {
            var users = JsonConvert.DeserializeObject<IEnumerable<User>>(inputJson);

            context.Users.AddRange(users);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
