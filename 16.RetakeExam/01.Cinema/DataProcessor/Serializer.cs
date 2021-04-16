namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            var movies = context.Movies.ToList()
                .Where(x => x.Rating >= rating && x.Projections.Any(x => x.Tickets.Count > 0))
                .OrderByDescending(x => x.Rating)
                .Select(x => new
                {
                    MovieName = x.Title,
                    Rating = x.Rating.ToString("f2"),
                    TotalIncomes = context.Tickets.ToList()
                        .Where(y => y.Projection.Movie.Title == x.Title)
                        .Sum(y => y.Price).ToString("f2"),
                    Customers = context.Tickets.ToList()
                        .Where(y => y.Projection.Movie.Title == x.Title)
                        .Select(x => new
                        {
                            FirstName = x.Customer.FirstName,
                            LastName = x.Customer.LastName,
                            Balance = x.Customer.Balance.ToString("f2")
                        })
                        .OrderByDescending(x => x.Balance)
                        .ThenBy(x => x.FirstName + ' ' + x.LastName)
                        .ToList(),
                })
                .OrderByDescending(x => double.Parse(x.Rating))
                .ThenByDescending(x => decimal.Parse(x.TotalIncomes))
                .Take(10)
                .ToList();

            var moviesJson = JsonConvert.SerializeObject(movies, Formatting.Indented);
            return moviesJson;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var customers = context.Customers.ToList()
                .Where(x => x.Age >= age)
                .Select(x => new ExportCustomerDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    SpentMoney = x.Tickets.Sum(x => x.Price).ToString("f2"),
                    SpentTime = x.Tickets.Sum(x => x.Projection.Movie.Duration).ToString(@"hh\:mm\:ss")
                })
                .OrderByDescending(x => decimal.Parse(x.SpentMoney))
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<ExportCustomerDto>), new XmlRootAttribute("Customers"));
            serializer.Serialize(new StringWriter(sb), customers, namespaces);
            return sb.ToString().Trim();
        }
    }

    public static class LinqExtensions
    {
        public static TimeSpan Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, TimeSpan> func)
        {
            return new TimeSpan(source.Sum(item => func(item).Ticks));
        }
    }
}