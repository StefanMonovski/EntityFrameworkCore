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
    public class PropertyServices : IPropertyServices
    {
        private readonly RealEstatesDbContext context;
        private readonly TagServices tagServices;

        public PropertyServices(RealEstatesDbContext context)
        {
            this.context = context;
            tagServices = new TagServices(context);
        }

        public void ImportPropertiesDataset(List<ImportPropertyDto> propertiesDto)
        {
            var properties = new List<Property>();

            foreach (var propertyDto in propertiesDto)
            {
                var property = new Property()
                {
                    Size = propertyDto.Size,
                    YardSize = propertyDto.YardSize,
                    Floor = propertyDto.Floor,
                    Price = propertyDto.Price,
                };

                if (propertyDto.YardSize <= 0)
                {
                    property.YardSize = null;
                }

                if (propertyDto.Floor <= 0)
                {
                    property.Floor = null;
                }

                if (propertyDto.Price <= 0)
                {
                    property.Price = null;
                }

                var existingPropertyType = properties.Select(x => x.PropertyType).Where(x => x.Type == propertyDto.Type).FirstOrDefault();
                if (existingPropertyType == null)
                {
                    property.PropertyType = new PropertyType { Type = propertyDto.Type };
                }
                else
                {
                    property.PropertyType = existingPropertyType;
                }

                var existingDistrict = properties.Select(x => x.District).Where(x => x.Name == propertyDto.District).FirstOrDefault();
                if (existingDistrict == null)
                {
                    property.District = new District { Name = propertyDto.District };
                }
                else
                {
                    property.District = existingDistrict;
                }

                var building = new Building()
                {
                    Floors = propertyDto.TotalFloors,
                    Year = propertyDto.Year
                };

                if (propertyDto.TotalFloors <= 0)
                {
                    building.Floors = null;
                }

                if (propertyDto.Year <= 0)
                {
                    building.Year = null;
                }

                var existingBuildingType = properties.Select(x => x.Building.BuildingType).Where(x => x.Type == propertyDto.BuildingType).FirstOrDefault();
                if (existingBuildingType == null)
                {
                    building.BuildingType = new BuildingType { Type = propertyDto.BuildingType };
                }
                else
                {
                    building.BuildingType = existingBuildingType;
                }

                property.Building = building;
                tagServices.InsertPropertyTags(property);

                properties.Add(property);
            }

            context.Properties.AddRange(properties);
            context.SaveChanges();
        }

        public void AddProperty(int size, int yardSize, int floor, int totalFloors, string district, int year, string type, string buildingType, decimal price)
        {
            var property = new Property()
            {
                Size = size,
                YardSize = yardSize,
                Floor = floor,
                Price = price,
            };

            if (yardSize <= 0)
            {
                property.YardSize = null;
            }

            if (floor <= 0)
            {
                property.Floor = null;
            }

            if (price <= 0)
            {
                property.Price = null;
            }

            var existingPropertyType = context.PropertyTypes.Where(x => x.Type == type).FirstOrDefault();
            if (existingPropertyType == null)
            {
                property.PropertyType = new PropertyType { Type = type };
            }
            else
            {
                property.PropertyType = existingPropertyType;
            }

            var existingDistrict = context.Districts.Where(x => x.Name == district).FirstOrDefault();
            if (existingDistrict == null)
            {
                property.District = new District { Name = district };
            }
            else
            {
                property.District = existingDistrict;
            }

            var building = new Building()
            {
                Floors = totalFloors,
                Year = year
            };

            if (totalFloors <= 0)
            {
                building.Floors = null;
            }

            if (year <= 0)
            {
                building.Year = null;
            }

            var existingBuildingType = context.BuildingTypes.Where(x => x.Type == buildingType).FirstOrDefault();
            if (existingBuildingType == null)
            {
                building.BuildingType = new BuildingType { Type = buildingType };
            }
            else
            {
                building.BuildingType = existingBuildingType;
            }

            property.Building = building;
            tagServices.InsertPropertyTags(property);

            context.Properties.Add(property);
            context.SaveChanges();
        }

        public List<ExportPropertyDto> SelectPropertiesBetweenPriceRange(decimal minimumPrice, decimal maximumPrice)
        {
            var properties = context.Properties
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
                .Where(x => x.Price >= minimumPrice && x.Price <= maximumPrice)
                .OrderBy(x => x.Price)
                .ToList();

            return properties;
        }

        public List<ExportPropertyDto> SelectPropertiesBetweenSizeRange(int minimumSize, int maximumSize)
        {
            var properties = context.Properties
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
                .Where(x => x.Size >= minimumSize && x.Size <= maximumSize)
                .OrderBy(x => x.Size)
                .ToList();

            return properties;
        }
    }
}
