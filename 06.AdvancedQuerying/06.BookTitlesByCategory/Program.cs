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

            Console.WriteLine(GetBooksByCategory(context, Console.ReadLine()));
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var categories = input.Split().Select(x => x.ToLower()).ToList();

            var books = context.Books
                .Select(x => new
                {
                    x.Title,
                    Category = x.BookCategories
                    .Select(x => x.Category.Name)
                    .FirstOrDefault()
                })
                .Where(x => categories.Any(y => y.Equals(x.Category.ToLower())))
                .OrderBy(x => x.Title)
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().Trim();
        }
    }
}