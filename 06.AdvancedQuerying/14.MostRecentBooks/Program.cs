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

            Console.WriteLine(GetMostRecentBooks(context));
        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories
                .Select(x => new
                {
                    x.Name,
                    MostRecentBooks = x.CategoryBooks
                    .Select(x => new
                    {
                        x.Book.Title,
                        x.Book.ReleaseDate
                    })
                    .OrderByDescending(x => x.ReleaseDate)
                    .Take(3)
                    .ToList()
                })
                .OrderBy(x => x.Name)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var category in categories)
            {
                sb.AppendLine($"--{category.Name}");
                foreach (var book in category.MostRecentBooks)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }
            return sb.ToString().Trim();
        }
    }
}
