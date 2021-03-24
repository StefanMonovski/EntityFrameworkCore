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

            File.WriteAllText("../../../Exports/categories-by-products.json", GetCategoriesByProductsCount(context));
        }

        public static string GetCategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .Select(x => new CategoryDto
                {
                    Category = x.Name,
                    ProductsCount = x.CategoryProducts.Count(),
                    AveragePrice = x.CategoryProducts.Average(x => x.Product.Price).ToString("f2"),
                    TotalRevenue = x.CategoryProducts.Sum(x => x.Product.Price).ToString("f2"),
                })
                .OrderByDescending(x => x.ProductsCount)
                .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            var productsJson = JsonConvert.SerializeObject(categories, jsonSerializerSettings);
            return productsJson;
        }
    }
}
