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

            File.WriteAllText("../../../Exports/users-sold-products.xml", GetSoldProducts(context));
        }

        public static string GetSoldProducts(ProductShopContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var users = context.Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer.FirstName != null || x.Buyer.LastName != null))
                .Select(x => new UserSoldProductsDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SoldProducts = x.ProductsSold
                    .Select(x => new SoldProductDto
                    {
                        Name = x.Name,
                        Price = x.Price,
                    })
                    .ToList()
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Take(5)
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<UserSoldProductsDto>), new XmlRootAttribute("Users"));
            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().Trim();
        }
    }
}
