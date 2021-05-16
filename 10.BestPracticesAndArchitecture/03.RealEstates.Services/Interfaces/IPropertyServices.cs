using _03.RealEstates.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace _03.RealEstates.Services.Interfaces
{
    public interface IPropertyServices
    {
        void ImportPropertiesDataset(List<ImportPropertyDto> propertiesDto);

        void AddProperty(int size, int yardSize, int floor, int totalFloors, string districtName, int year, string type, string buildingType, decimal price);

        List<ExportPropertyDto> SelectPropertiesBetweenPriceRange(decimal minimumPrice, decimal maximumPrice);

        List<ExportPropertyDto> SelectPropertiesBetweenSizeRange(int minimumSize, int maximumSize);
    }
}
