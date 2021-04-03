using AutoMapper;
using CarDealer.Data;
using CarDealer.DataTransferObjects;
using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

            string inputXml = File.ReadAllText("../../../Datasets/suppliers.xml");
            Console.WriteLine(ImportSuppliers(context, inputXml));
        }

        public static string ImportSuppliers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportSupplierDto>), new XmlRootAttribute("Suppliers"));
            var suppliersDto = (List<ImportSupplierDto>)serializer.Deserialize(new StringReader(inputXml));

            var suppliers = mapper.Map<List<Supplier>>(suppliersDto);
            context.Suppliers.AddRange(suppliers);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
