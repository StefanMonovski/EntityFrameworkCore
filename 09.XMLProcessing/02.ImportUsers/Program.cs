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
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();
            context.Database.EnsureCreated();

            Mapper.Initialize(x => x.AddProfile(new ProductShopProfile()));

            string inputXml = File.ReadAllText("../../../Datasets/users.xml");
            System.Console.WriteLine(ImportUsers(context, inputXml));
        }

        public static string ImportUsers(ProductShopContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(List<ImportUserDto>), new XmlRootAttribute("Users"));
            var usersDto = (List<ImportUserDto>)serializer.Deserialize(new StringReader(inputXml));

            var users = Mapper.Map<List<User>>(usersDto);
            context.Users.AddRange(users);
            int count = context.SaveChanges();

            return $"Successfully imported {count}";
        }
    }
}
