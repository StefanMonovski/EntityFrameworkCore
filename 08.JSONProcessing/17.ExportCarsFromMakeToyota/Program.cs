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

            File.WriteAllText("../../../Exports/toyota-cars.json", GetCarsFromMakeToyota(context));
        }

        public static string GetCarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new CarDto
                {
                    Id = x.Id,
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance
                })
                .Where(x => x.Make == "Toyota")
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.TravelledDistance)
                .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var carsJson = JsonConvert.SerializeObject(cars, jsonSerializerSettings);
            return carsJson;
        }
    }
}
