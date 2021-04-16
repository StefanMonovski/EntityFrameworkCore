namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";

        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";

        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var moviesDto = JsonConvert.DeserializeObject<List<ImportMovieDto>>(jsonString);

            StringBuilder sb = new StringBuilder();
            var movies = new List<Movie>();
            foreach (var movieDto in moviesDto)
            {
                var movie = new Movie()
                {
                    Title = movieDto.Title,
                    Genre = (Genre)Enum.Parse(typeof(Genre), movieDto.Genre),
                    Duration = TimeSpan.ParseExact(movieDto.Duration, "c", CultureInfo.InvariantCulture),
                    Rating = movieDto.Rating,
                    Director = movieDto.Director
                };

                if (!IsValid(movie))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (movies.Select(x => x.Title).ToList().Any(x => x == movie.Title))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                movies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre.ToString(), movie.Rating.ToString("f2")));
            }
            context.Movies.AddRange(movies);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportProjectionDto>), new XmlRootAttribute("Projections"));
            var projectionsDto = (List<ImportProjectionDto>)serializer.Deserialize(new StringReader(xmlString));

            var moviesId = context.Movies.Select(x => x.Id).ToList();

            StringBuilder sb = new StringBuilder();
            var projections = new List<Projection>();
            foreach (var projectionDto in projectionsDto)
            {
                var movie = context.Movies.Find(projectionDto.MovieId);
                if (movie == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isDateTimeValid = DateTime.TryParseExact(projectionDto.DateTime, "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime);
                if (!isDateTimeValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection()
                {
                    Movie = movie,
                    DateTime = dateTime,
                };

                projections.Add(projection);
                sb.AppendLine(string.Format(SuccessfulImportProjection, projection.Movie.Title, projection.DateTime.ToString("MM/dd/yyyy")));
            }
            context.Projections.AddRange(projections);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(List<ImportCustomerDto>), new XmlRootAttribute("Customers"));
            var customersDto = (List<ImportCustomerDto>)serializer.Deserialize(new StringReader(xmlString));

            StringBuilder sb = new StringBuilder();
            var customers = new List<Customer>();
            foreach (var customerDto in customersDto)
            {
                var customer = new Customer()
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance,
                    Tickets = new List<Ticket>()
                };

                if (!IsValid(customer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var ticketDto in customerDto.Tickets)
                {
                    var ticket = new Ticket()
                    {
                        ProjectionId = ticketDto.ProjectionId,
                        Price = ticketDto.Price
                    };

                    if (!IsValid(ticket))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    customer.Tickets.Add(ticket);
                }

                customers.Add(customer);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
            }
            context.Customers.AddRange(customers);
            context.SaveChanges();
            return sb.ToString().Trim();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}