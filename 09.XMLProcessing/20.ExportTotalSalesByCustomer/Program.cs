using CarDealer.Data;
using CarDealer.DataTransferObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();

            File.WriteAllText("../../../Exports/customers-total-sales.xml", GetTotalSalesByCustomer(context));
        }

        public static string GetTotalSalesByCustomer(CarDealerContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var customers = context.Customers
                .Select(x => new CustomerDto
                {
                    FullName = x.Name,
                    BoughtCars = x.Sales.Count(),
                    SpentMoney = x.Sales.SelectMany(x => x.Car.PartCars.Select(x => x.Part.Price)).Sum()
                })
                .Where(x => x.BoughtCars > 0)
                .OrderByDescending(x => x.SpentMoney)
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<CustomerDto>), new XmlRootAttribute("customers"));
            serializer.Serialize(new StringWriter(sb), customers, namespaces);

            return sb.ToString().Trim();
        }
    }
}
