using _01.RealEstates.Data;
using _02.RealEstates.Models;
using _03.RealEstates.Services.Dto;
using _03.RealEstates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _03.RealEstates.Services
{
    public class TagServices : ITagServices
    {
        private readonly RealEstatesDbContext context;
        private List<Tag> tags;

        public TagServices(RealEstatesDbContext context)
        {
            this.context = context;
            tags = new List<Tag>();
        }

        public void ImportTagsDataset(List<ImportTagDto> tagsDto)
        {
            foreach (var tagDto in tagsDto)
            {
                var tag = new Tag()
                {
                    Name = tagDto.Name
                };

                var existingTagName = tags.Select(x => x.Name).Where(x => x == tagDto.Name).FirstOrDefault();
                if (existingTagName != null)
                {
                    continue;
                }

                tags.Add(tag);
            }

            context.Tags.AddRange(tags);
            context.SaveChanges();
        }

        public void InsertPropertyTags(Property property)
        {
            if (tags.Count == 0)
            {
                tags = context.Tags.ToList();
            }

            List<Tag> propertyTags = new List<Tag>();

            if (property.Floor <= 3)
            {
                propertyTags.Add(tags.First(x => x.Name == "Low floor"));
            }
            else if (property.Floor >= 10)
            {
                propertyTags.Add(tags.First(x => x.Name == "High floor"));
            }

            if (property.Price <= 150000)
            {
                propertyTags.Add(tags.First(x => x.Name == "Cheap"));
            }
            else if (property.Price >= 400000)
            {
                propertyTags.Add(tags.First(x => x.Name == "Expensive"));
            }

            if (property.Size <= 70)
            {
                propertyTags.Add(tags.First(x => x.Name == "Small size"));
            }
            else if (property.Size >= 120)
            {
                propertyTags.Add(tags.First(x => x.Name == "Big size"));
            }

            if (property.Building.Year <= 1950)
            {
                propertyTags.Add(tags.First(x => x.Name == "Old"));
            }
            else if (property.Building.Year >= 2000)
            {
                propertyTags.Add(tags.First(x => x.Name == "New"));
            }

            foreach (var propertyTag in propertyTags)
            {
                property.PropertiesTags.Add(new PropertyTag
                {
                    Property = property,
                    Tag = propertyTag
                });
            }
        }

        public List<ExportPropertyDto> SelectLowFloorProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "Low floor"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderBy(x => x.Floor)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectHighFloorProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "High floor"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderByDescending(x => x.Floor)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectCheapProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "Cheap"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderBy(x => x.Price)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectExpensiveProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "Expensive"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderByDescending(x => x.Price)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectSmallSizeProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "Small size"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderBy(x => x.Size)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectBigSizeProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "Big size"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderByDescending(x => x.Size)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectOldProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "Old"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderBy(x => x.Year)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectNewProperties()
        {
            var properties = context.Properties
                .Where(x => x.PropertiesTags.Any(x => x.Tag.Name == "New"))
                .Select(x => new ExportPropertyDto
                {
                    Size = x.Size,
                    YardSize = x.YardSize,
                    Floor = x.Floor,
                    District = x.District.Name,
                    Year = x.Building.Year,
                    Type = x.PropertyType.Type,
                    Price = x.Price
                })
                .OrderByDescending(x => x.Year)
                .ToList();

            return properties;
        }
    }
}
