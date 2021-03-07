using P02_FootballBetting.Data;
using System;

namespace P02_FootballBetting
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new FootballBettingContext();
            context.Database.EnsureCreated();
        }
    }
}
