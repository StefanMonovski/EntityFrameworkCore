namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var dateTimeConverter = new IsoDateTimeConverter { Culture = CultureInfo.InvariantCulture, DateTimeFormat = "yyyy-MM-dd" };
			var gamesDto = JsonConvert.DeserializeObject<IEnumerable<ImportGameDto>>(jsonString, dateTimeConverter);

			StringBuilder sb = new StringBuilder();
			var games = new List<Game>();
			var tags = new List<Tag>();
            foreach (var gameDto in gamesDto)
            {
				Developer developer = new Developer();
                if (games.Select(x => x.Developer.Name).Contains(gameDto.Developer))
                {
					developer = games.Select(x => x.Developer).Where(x => x.Name == gameDto.Developer).First();
                }
                else
                {
					developer.Name = gameDto.Developer;
                }

				Genre genre = new Genre();
				if (games.Select(x => x.Genre.Name).Contains(gameDto.Genre))
				{
					genre = games.Select(x => x.Genre).Where(x => x.Name == gameDto.Genre).First();
				}
				else
				{
					genre.Name = gameDto.Genre;
				}

                var currentTags = new List<Tag>();
                foreach (var tagName in gameDto.Tags)
                {
                    if (tags.Select(x => x.Name).Contains(tagName))
                    {
						currentTags.Add(tags.First(x => x.Name == tagName));
                    }
                    else
                    {
						var tag = new Tag { Name = tagName };
						currentTags.Add(tag);
                    }
                }

                var game = new Game
				{
					Name = gameDto.Name,
					Price = gameDto.Price,
					ReleaseDate = gameDto.ReleaseDate,
					Developer = developer,
					Genre = genre,
					GameTags = new List<GameTag>()
				};
                foreach (var tag in currentTags)
                {
					game.GameTags.Add(new GameTag
					{
						Game = game,
						Tag = tag
					});
                }
                
                if (!IsValid(game) || !IsValid(game.Developer) || !IsValid(game.Genre) || !IsValid(game.GameTags) || game.GameTags.Count < 1)
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

                games.Add(game);
                foreach (var gameTag in game.GameTags)
                {
					tags.Add(gameTag.Tag);
                }
				sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
			}
			
			context.Games.AddRange(games);
			context.Tags.AddRange(tags);
			context.SaveChanges();
			return sb.ToString().Trim();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var usersDto = JsonConvert.DeserializeObject<IEnumerable<ImportUserDto>>(jsonString);

			StringBuilder sb = new StringBuilder();
			var users = new List<User>();
            foreach (var userDto in usersDto)
            {
				var user = new User
				{
					FullName = userDto.FullName,
					Username = userDto.UserName,
					Email = userDto.Email,
					Age = userDto.Age,
					Cards = userDto.Cards.Select(x => new Card
					{
						Number = x.Number,
						Cvc = x.CVC,
						Type = Enum.Parse<CardType>(x.Type)
					})
					.ToList()
				};

                if (!IsValid(user) || !user.Cards.Any(IsValid))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				users.Add(user);
				sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }

			context.Users.AddRange(users);
			context.SaveChanges();
			return sb.ToString().Trim();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var serializer = new XmlSerializer(typeof(List<ImportPurchaseDto>), new XmlRootAttribute("Purchases"));
			var purchasesDto = (List<ImportPurchaseDto>)serializer.Deserialize(new StringReader(xmlString));

			StringBuilder sb = new StringBuilder();
			var purchases = new List<Purchase>();
            foreach (var purchaseDto in purchasesDto)
            {
				var game = context.Games.FirstOrDefault(x => x.Name == purchaseDto.Title);
				var card = context.Cards.FirstOrDefault(x => x.Number == purchaseDto.Card);

				var purchase = new Purchase()
				{
					Type = Enum.Parse<PurchaseType>(purchaseDto.Type),
					ProductKey = purchaseDto.Key,
					Card = card,
					Game = game,
					Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
				};

                if (!IsValid(purchase))
                {
					sb.AppendLine("Invalid Data");
					continue;
                }

				purchases.Add(purchase);
				sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }

			context.Purchases.AddRange(purchases);
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