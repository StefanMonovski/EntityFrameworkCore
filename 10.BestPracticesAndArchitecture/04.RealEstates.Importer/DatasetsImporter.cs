using _01.RealEstates.Data;
using _03.RealEstates.Services;
using _03.RealEstates.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace _05.RealEstates.Importer
{
    public class DatasetsImporter
    {
        private readonly PropertyServices propertyServices;
        private readonly TagServices tagServices;
        private readonly Encoding encoding = Encoding.GetEncoding(1251);

        public DatasetsImporter(RealEstatesDbContext context)
        {
            propertyServices = new PropertyServices(context);
            tagServices = new TagServices(context);
        }

        public void ImportDatasets()
        {
            var tagsDto = JsonConvert.DeserializeObject<List<ImportTagDto>>(File.ReadAllText(@"Datasets\Tags.json", encoding));
            tagServices.ImportTagsDataset(tagsDto);
            var housesDto = JsonConvert.DeserializeObject<List<ImportPropertyDto>>(File.ReadAllText(@"Datasets\HouseAds.json", encoding));
            propertyServices.ImportPropertiesDataset(housesDto);
            var propertiesDto = JsonConvert.DeserializeObject<List<ImportPropertyDto>>(File.ReadAllText(@"Datasets\PropertyAds.json", encoding));
            propertyServices.ImportPropertiesDataset(propertiesDto);
        }
    }
}
