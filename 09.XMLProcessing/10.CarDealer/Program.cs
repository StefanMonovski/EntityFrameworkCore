using CarDealer.Data;

namespace CarDealer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();
            context.Database.EnsureCreated();
        }
    }
}
