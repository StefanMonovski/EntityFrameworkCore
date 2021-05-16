using _01.RealEstates.Data;
using _03.RealEstates.Services.Dto;
using _03.RealEstates.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _03.RealEstates.Services
{
    public class DistrictServices : IDistrictServices
    {
        private readonly RealEstatesDbContext context;

        public DistrictServices(RealEstatesDbContext context)
        {
            this.context = context;
        }

        public List<ExportPropertyDto> SelectPropertiesByDistrict(string district)
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
                .Where(x => x.District == district)
                .OrderBy(x => x.Price)
                .ToList();

            return properties;
        }

        public List<ExportDistrictDto> SelectDistrictsByAveragePrice()
        {
            var districts = context.Districts
                .Select(x => new ExportDistrictDto
                {
                    Name = x.Name,
                    AveragePrice = x.Properties.Average(x => x.Price)
                })
                .OrderByDescending(x => x.AveragePrice)
                .ToList();

            return districts;
        }
    }
}
