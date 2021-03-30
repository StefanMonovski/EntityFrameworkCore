using Newtonsoft.Json;
using System;
using System.IO;
using CarDealer.Data;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using CarDealer.DataTransferObjects;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            File.WriteAllText("../../../Exports/customers-total-sales.json", GetTotalSalesByCustomer(context));
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Select(x => new CustomerPurchasesDto
                {
                    FullName = x.Name,
                    BoughtCars = x.Sales.Count(),
                    SpentMoney = x.Sales.SelectMany(x => x.Car.PartCars.Select(x => x.Part.Price)).Sum()
                })
                .Where(x => x.BoughtCars > 0)
                .OrderByDescending(x => x.SpentMoney)
                .ThenByDescending(x => x.BoughtCars)
                .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            var customersJson = JsonConvert.SerializeObject(customers, jsonSerializerSettings);
            return customersJson;
        }
    }
}
