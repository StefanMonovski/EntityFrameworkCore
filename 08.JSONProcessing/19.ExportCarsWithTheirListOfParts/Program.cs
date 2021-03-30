using Newtonsoft.Json;
using System;
using System.IO;
using CarDealer.Data;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;
using CarDealer.DataTransferObjects.CarsAndParts;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            File.WriteAllText("../../../Exports/cars-and-parts.json", GetCarsWithTheirListOfParts(context));
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(x => new
                {
                    car = new CarDto
                    {
                        Make = x.Make,
                        Model = x.Model,
                        TravelledDistance = x.TravelledDistance,
                    },
                    parts = x.PartCars
                    .Select(x => new PartDto
                    {
                        Name = x.Part.Name,
                        Price = x.Part.Price.ToString("f2")
                    })
                })
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
