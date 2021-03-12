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

            Console.WriteLine(GetBooksByAgeRestriction(context, Console.ReadLine()));
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var titles = context.Books
                .Where(x => Enum.Parse<AgeRestriction>(command, true) == x.AgeRestriction)
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var title in titles)
            {
                sb.AppendLine(title);
            }
            return sb.ToString().Trim();
        }
    }
}
