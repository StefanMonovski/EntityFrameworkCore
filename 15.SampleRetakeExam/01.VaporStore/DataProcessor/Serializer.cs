namespace VaporStore.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context.Genres.ToList()
                .Where(x => genreNames.Contains(x.Name))
                .Select(x => new
                {
                    Id = x.Id,
                    Genre = x.Name,
                    Games = x.Games
                        .Where(x => x.Purchases.Any())
                        .Select(x => new
                        {
                            Id = x.Id,
                            Title = x.Name,
                            Developer = x.Developer.Name,
                            Tags = string.Join(", ", x.GameTags.Select(x => x.Tag.Name).ToList()),
                            Players = x.Purchases.Count
                        })
                        .OrderByDescending(x => x.Players)
                        .ThenBy(x => x.Id)
                        .ToList(),
                    TotalPlayers = x.Games.Sum(x => x.Purchases.Count)
                })
                .OrderByDescending(x => x.TotalPlayers)
                .ThenBy(x => x.Id)
                .ToList();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            var genresJson = JsonConvert.SerializeObject(genres, jsonSerializerSettings);
            return genresJson;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            var users = context.Users.ToList()
                .Where(x => x.Cards.Any(x => x.Purchases.Any(x => x.Type.ToString() == storeType)))
                .Select(x => new ExportUserDto()
                {
                    Username = x.Username,
                    Purchases = context.Purchases.ToList()
                        .Where(y => y.Card.User.Username == x.Username && y.Type.ToString() == storeType)
                        .Select(x => new ExportPurchaseDto()
                        {
                            Card = x.Card.Number,
                            Cvc = x.Card.Cvc,
                            Date = x.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Game = new ExportGameDto()
                            {
                                Title = x.Game.Name,
                                Genre = x.Game.Genre.Name,
                                Price = x.Game.Price
                            }
                        })
                        .OrderBy(x => x.Date)
                        .ToList(),
                    TotalSpent = context.Purchases.ToList()
                        .Where(y => y.Card.User.Username == x.Username && y.Type.ToString() == storeType)
                        .Sum(x => x.Game.Price)
                })
                .OrderByDescending(x => x.TotalSpent)
                .ThenBy(x => x.Username)
                .ToList();

            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(List<ExportUserDto>), new XmlRootAttribute("Users"));
            serializer.Serialize(new StringWriter(sb), users, namespaces);
            return sb.ToString().Trim();
        }
    }
}