using CarDealer.Data;
using CarDealer.DataTransferObjects.CarsAndParts;
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

            File.WriteAllText("../../../Exports/cars-and-parts.xml", GetCarsWithTheirListOfParts(context));
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var cars = context.Cars
                .Select(x => new CarDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance,
                    Parts = x.PartCars
                    .Select(x => new PartDto
                    {
                        Name = x.Part.Name,
                        Price = x.Part.Price
                    })
                    .OrderByDescending(x => x.Price)
                    .ToList()
                })
                .OrderByDescending(x => x.TravelledDistance)
                .ThenBy(x => x.Model)
                .Take(5)
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<CarDto>), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            return sb.ToString().Trim();
        }
    }
}
