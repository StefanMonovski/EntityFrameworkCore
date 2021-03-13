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

            Console.WriteLine(RemoveBooks(context));
        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books
                .Where(x => x.Copies < 4200)
                .ToList();

            int removed = 0;
            foreach (var book in books)
            {
                context.Remove(book);
                removed++;
            }

            context.SaveChanges();
            return removed;
        }
    }
}
