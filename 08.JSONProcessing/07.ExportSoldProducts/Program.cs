using Newtonsoft.Json;
using System;
using System.IO;
using ProductShop.Data;
using ProductShop.Models;
using System.Collections.Generic;
using System.Linq;
using ProductShop.DataTransferObjects;
using Newtonsoft.Json.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();

            File.WriteAllText("../../../Exports/users-sold-products.json", GetSoldProducts(context));
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer.FirstName != null || x.Buyer.LastName != null))
                .Select(x => new UserSoldProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold.Select(x => new SoldProductDto
                    {
                        Name = x.Name,
                        Price = x.Price,
                        BuyerFirstName = x.Buyer.FirstName,
                        BuyerLastName = x.Buyer.LastName,
                    })
                    .Where(x => x.BuyerFirstName != null || x.BuyerLastName != null)
                    .ToList()
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            var productsJson = JsonConvert.SerializeObject(users, jsonSerializerSettings);
            return productsJson;
        }
    }
}
