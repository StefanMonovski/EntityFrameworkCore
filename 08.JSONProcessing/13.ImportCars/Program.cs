using Newtonsoft.Json;
using System;
using System.IO;
using CarDealer.Data;
using CarDealer.Models;
using System.Collections.Generic;
using System.Linq;
using CarDealer.DataTransferObjects;
using AutoMapper;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            Mapper.Initialize(x => x.AddProfile(new CarDealerProfile()));

            string inputJson = File.ReadAllText("../../../Datasets/cars.json");
            Console.WriteLine(ImportCars(context, inputJson));
        }

        public static string ImportCars(CarDealerContext context, string inputJson)
        {
            var importCars = JsonConvert.DeserializeObject<List<ImportCarDto>>(inputJson);
            var cars = Mapper.Map<List<Car>>(importCars);

            for (int i = 0; i < cars.Count; i++)
            {
                foreach (var partId in importCars[i].PartsId.Distinct())
                {
                    cars[i].PartCars.Add(new PartCar
                    {
                        Car = cars[i],
                        PartId = partId
                    });
                }
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}.";
        }
    }
}
