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

            string inputXml = File.ReadAllText("../../../Datasets/categories.xml");
            System.Console.WriteLine(ImportCategories(context, inputXml));
        }

        public static string ImportCategories(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCategoryDto>), new XmlRootAttribute("Categories"));
            var categoriesDto = (List<ImportCategoryDto>)serializer.Deserialize(new StringReader(inputXml));
            
            var categories = mapper.Map<List<Category>>(categoriesDto);
            context.Categories.AddRange(categories);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
