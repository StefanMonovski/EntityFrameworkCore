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

            File.WriteAllText("../../../Exports/sales-discounts.xml", GetSalesWithAppliedDiscount(context));
        }

        public static string GetSalesWithAppliedDiscount(CarDealerContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var sales = context.Sales
                .Take(10)
                .Select(x => new SaleDto
                {
                    Car = new CarDto
                    {
                        Make = x.Car.Make,
                        Model = x.Car.Model,
                        TravelledDistance = x.Car.TravelledDistance
                    },
                    CustomerName = x.Customer.Name,
                    Discount = x.Discount,
                    Price = x.Car.PartCars.Sum(x => x.Part.Price),
                    PriceWithDiscount = x.Car.PartCars.Sum(x => x.Part.Price) - x.Car.PartCars.Sum(x => x.Part.Price) * x.Discount / 100,
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<SaleDto>), new XmlRootAttribute("sales"));
            serializer.Serialize(new StringWriter(sb), sales, namespaces);

            return sb.ToString().Trim();
        }
    }
}
