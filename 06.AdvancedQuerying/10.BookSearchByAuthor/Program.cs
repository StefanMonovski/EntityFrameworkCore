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

            Console.WriteLine(GetBooksByAuthor(context, Console.ReadLine()));
        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var books = context.Books
                .Where(x => x.Author.LastName.Substring(0, input.Length).ToLower() == input.ToLower())
                .OrderBy(x => x.BookId)
                .Select(x => new { x.Title, AuthorName = x.Author.FirstName + " " + x.Author.LastName })
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorName})");
            }
            return sb.ToString().Trim();
        }
    }
}
