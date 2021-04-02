using ProductShop.Data;
using ProductShop.DataTransferObjects.UsersAndProducts;
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

            File.WriteAllText("../../../Exports/categories-by-products.xml", GetUsersWithProducts(context));
        }

        public static string GetUsersWithProducts(ProductShopContext context)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var users = new UsersDto
            {
                Count = context.Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer != null)).Count(),
                Users = context.Users
                .Where(x => x.ProductsSold.Any(x => x.Buyer != null))
                .OrderByDescending(x => x.ProductsSold.Count(x => x.Buyer != null))
                .Select(x => new UserDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    SoldProducts = new SoldProductsDto
                    {
                        Count = x.ProductsSold.Count(x => x.Buyer != null),
                        SoldProducts = x.ProductsSold
                        .Where(x => x.Buyer != null)
                        .Select(x => new ProductDto
                        {
                            Name = x.Name,
                            Price = x.Price
                        })
                        .OrderByDescending(x => x.Price)
                        .ToList()
                    }
                })
                .Take(10)
                .ToList()
            };

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(UsersDto), new XmlRootAttribute("Users"));
            serializer.Serialize(new StringWriter(sb), users, namespaces);

            return sb.ToString().Trim();
        }
    }
}
