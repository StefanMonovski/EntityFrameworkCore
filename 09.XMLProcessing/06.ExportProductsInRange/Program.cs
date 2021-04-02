using ProductShop.Data;
using ProductShop.DataTransferObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();

            File.WriteAllText("../../../Exports/products-in-range.xml", GetProductsInRange(context));
        }

        public static string GetProductsInRange(ProductShopContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var products = context.Products
                .Select(x => new ProductDto
                {
                    Name = x.Name,
                    Price = x.Price,
                    BuyerFullName = x.Buyer.FirstName + " " + x.Buyer.LastName,
                })
                .Where(x => x.Price >= 500 && x.Price <= 1000)
                .OrderBy(x => x.Price)
                .Take(10)
                .ToList();
                
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<ProductDto>), new XmlRootAttribute("Products"));
            serializer.Serialize(new StringWriter(sb), products, namespaces);

            return sb.ToString().Trim();
        }
    }
}
