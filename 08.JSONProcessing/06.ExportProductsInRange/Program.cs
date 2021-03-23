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

            File.WriteAllText("../../../Exports/products-in-range.json", GetProductsInRange(context));
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Select(x => new ProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    Seller = (x.Seller.FirstName + " " + x.Seller.LastName)
                })
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            var productsJson = JsonConvert.SerializeObject(products, jsonSerializerSettings);
            return productsJson;
        }
    }
}
