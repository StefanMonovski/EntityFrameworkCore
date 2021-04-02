using AutoMapper;
using ProductShop.Data;
using ProductShop.DataTransferObjects;
using ProductShop.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ProductShop
{
    public class StartUp
    {
        static readonly MapperConfiguration config = new MapperConfiguration(x => x.AddProfile(new ProductShopProfile()));
        static readonly IMapper mapper = config.CreateMapper();

        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();

            string inputXml = File.ReadAllText("../../../Datasets/categories-products.xml");
            System.Console.WriteLine(ImportCategoryProducts(context, inputXml));
        }

        public static string ImportCategoryProducts(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCategoryProductDto>), new XmlRootAttribute("CategoryProducts"));
            var categoriesProductsDto = (List<ImportCategoryProductDto>)serializer.Deserialize(new StringReader(inputXml));
            
            var categoriesProducts = mapper.Map<List<CategoryProduct>>(categoriesProductsDto);
            context.CategoryProducts.AddRange(categoriesProducts);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
