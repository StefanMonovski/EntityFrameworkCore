using Newtonsoft.Json;
using System;
using System.IO;
using CarDealer.Data;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;
using CarDealer.DataTransferObjects;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            File.WriteAllText("../../../Exports/local-suppliers.json", GetLocalSuppliers(context));
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new SupplierDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var suppliersJson = JsonConvert.SerializeObject(suppliers, jsonSerializerSettings);
            return suppliersJson;
        }
    }
}
