using Newtonsoft.Json;
using System;
using System.IO;
using CarDealer.Data;
using CarDealer.Models;
using System.Collections.Generic;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            string inputJson = File.ReadAllText("../../../Datasets/sales.json");
            Console.WriteLine(ImportSales(context, inputJson));
        }

        public static string ImportSales(CarDealerContext context, string inputJson)
        {
            var sales = JsonConvert.DeserializeObject<IEnumerable<Sale>>(inputJson);

            context.Sales.AddRange(sales);
            int count = context.SaveChanges();

            return $"Successfully imported {count}.";
        }
    }
}
