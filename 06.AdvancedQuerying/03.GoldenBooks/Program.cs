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

            Console.WriteLine(GetGoldenBooks(context));
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var titles = context.Books
                .Where(x => x.Copies < 5000 && Enum.Parse<EditionType>("Gold") == x.EditionType)
                .OrderBy(x => x.BookId)
                .Select(x => x.Title)
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
