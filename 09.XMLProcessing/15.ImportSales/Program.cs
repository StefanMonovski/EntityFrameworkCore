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

            string inputXml = File.ReadAllText("../../../Datasets/sales.xml");
            Console.WriteLine(ImportSales(context, inputXml));
        }
        
        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportSaleDto>), new XmlRootAttribute("Sales"));
            var salesDto = (List<ImportSaleDto>)serializer.Deserialize(new StringReader(inputXml));

            var sales = mapper.Map<List<Sale>>(salesDto);
            context.Sales.AddRange(sales.Where(x => context.Cars.Select(x => x.Id).Contains(x.CarId)));
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
