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

            string inputXml = File.ReadAllText("../../../Datasets/customers.xml");
            Console.WriteLine(ImportCustomers(context, inputXml));
        }
        
        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCustomerDto>), new XmlRootAttribute("Customers"));
            var customersDto = (List<ImportCustomerDto>)serializer.Deserialize(new StringReader(inputXml));

            var customers = mapper.Map<List<Customer>>(customersDto);
            context.Customers.AddRange(customers);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
