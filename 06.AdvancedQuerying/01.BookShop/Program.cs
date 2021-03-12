using BookShop.Data;
using System;

namespace BookShop
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new BookShopContext();
            context.Database.EnsureCreated();
        }
    }
}
