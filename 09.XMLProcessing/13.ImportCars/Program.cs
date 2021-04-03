using AutoMapper;
using CarDealer.Data;
using CarDealer.DataTransferObjects;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        static readonly MapperConfiguration config = new MapperConfiguration(x => x.AddProfile(new CarDealerProfile()));
        static readonly IMapper mapper = config.CreateMapper();

        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            string inputXml = File.ReadAllText("../../../Datasets/cars.xml");
            Console.WriteLine(ImportCars(context, inputXml));
        }
        
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCarDto>), new XmlRootAttribute("Cars"));
            var carsDto = (List<ImportCarDto>)serializer.Deserialize(new StringReader(inputXml));

            var cars = mapper.Map<List<Car>>(carsDto);
            for (int i = 0; i < cars.Count; i++)
            {
                foreach (var partId in carsDto[i].PartsId.Select(x => x.Id).Distinct())
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

            return $"Successfully imported {cars.Count}";
        }
    }
}
