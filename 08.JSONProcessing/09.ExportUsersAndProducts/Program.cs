using Newtonsoft.Json;
using System;
using System.IO;
using ProductShop.Data;
using ProductShop.Models;
using System.Collections.Generic;
using System.Linq;
using ProductShop.DataTransferObjects.UsersAndProducts;
using Newtonsoft.Json.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();

            File.WriteAllText("../../../Exports/users-and-products.json", GetUsersWithProducts(context));
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var users = new UsersDto
            {
                UsersCount = context.Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer != null)).Count(),
                Users = context.Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer != null))
                .OrderByDescending(x => x.ProductsSold.Count(x => x.Buyer != null))
                .Select(x => new UserDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new SoldProductsDto
                    {
                        Count = x.ProductsSold.Count(x => x.Buyer != null),
                        Products = x.ProductsSold
                        .Where(x => x.Buyer != null)
                        .Select(x => new ProductDto
                        {
                            Name = x.Name,
                            Price = x.Price
                        })
                        .ToList()
                    }
                })
                .ToList()
            };
            
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
            };
            var productsJson = JsonConvert.SerializeObject(users, jsonSerializerSettings);
            return productsJson;
        }
    }
}
