using Newtonsoft.Json;
using System;
using System.IO;
using CarDealer.Data;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            string inputJson = File.ReadAllText("../../../Datasets/parts.json");
            Console.WriteLine(ImportParts(context, inputJson));
        }

        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var parts = JsonConvert.DeserializeObject<IEnumerable<Part>>(inputJson)
                .Where(x => context.Suppliers.Select(x => x.Id).Contains(x.SupplierId));

            context.Parts.AddRange(parts);
            int count = context.SaveChanges();

            return $"Successfully imported {count}.";
        }
    }
}
