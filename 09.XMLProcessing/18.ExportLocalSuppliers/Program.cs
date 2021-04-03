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

            File.WriteAllText("../../../Exports/local-suppliers.xml", GetLocalSuppliers(context));
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var suppliers = context.Suppliers
                .Where(x => x.IsImporter == false)
                .Select(x => new SupplierDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<SupplierDto>), new XmlRootAttribute("suppliers"));
            serializer.Serialize(new StringWriter(sb), suppliers, namespaces);

            return sb.ToString().Trim();
        }
    }
}
