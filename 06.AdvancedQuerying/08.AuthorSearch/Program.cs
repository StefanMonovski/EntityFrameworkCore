using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models.Enums;
using System;
using System.Linq;
using System.Text;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new BookShopContext();
            DbInitializer.ResetDatabase(context);

            Console.WriteLine(GetAuthorNamesEndingIn(context, Console.ReadLine()));
        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var names = context.Authors
                .Where(x => x.FirstName.Substring(x.FirstName.Length - input.Length, input.Length) == input)
                .Select(x => x.FirstName + " " + x.LastName)
                .OrderBy(x => x)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var name in names)
            {
                sb.AppendLine(name);
            }
            return sb.ToString().Trim();
        }
    }
}
