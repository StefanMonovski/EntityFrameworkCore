using MusicHub.Data;
using System;

namespace MusicHub
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new MusicHubDbContext();
            context.Database.EnsureCreated();
        }
    }
}
