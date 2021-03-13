using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models.Enums;
using System;
using System.Globalization;
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

            Console.WriteLine(GetBooksReleasedBefore(context, Console.ReadLine()));
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var maxReleaseDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = context.Books
                .Where(x => x.ReleaseDate < maxReleaseDate)
                .OrderByDescending(x => x.ReleaseDate)
                .Select(x => new { x.Title, x.EditionType, x.Price })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }
            return sb.ToString().Trim();
        }
    }
}
