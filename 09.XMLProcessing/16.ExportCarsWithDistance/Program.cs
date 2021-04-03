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

            File.WriteAllText("../../../Exports/cars.xml", GetCarsWithDistance(context));
        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var cars = context.Cars
                .Select(x => new CarDto
                {
                    Make = x.Make,
                    Model = x.Model,
                    TravelledDistance = x.TravelledDistance,
                })
                .Where(x => x.TravelledDistance >= 2000000)
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<CarDto>), new XmlRootAttribute("cars"));
            serializer.Serialize(new StringWriter(sb), cars, namespaces);

            return sb.ToString().Trim();
        }
    }
}
