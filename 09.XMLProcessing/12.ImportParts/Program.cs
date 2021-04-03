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

            string inputXml = File.ReadAllText("../../../Datasets/parts.xml");
            Console.WriteLine(ImportParts(context, inputXml));
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportPartDto>), new XmlRootAttribute("Parts"));
            var partsDto = (List<ImportPartDto>)serializer.Deserialize(new StringReader(inputXml));

            var parts = mapper.Map<List<Part>>(partsDto);
            context.Parts.AddRange(parts.Where(x => context.Suppliers.Select(x => x.Id).Contains(x.SupplierId)));
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
