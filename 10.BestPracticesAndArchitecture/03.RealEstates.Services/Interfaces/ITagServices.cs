using _02.RealEstates.Models;
using _03.RealEstates.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace _03.RealEstates.Services.Interfaces
{
    public interface ITagServices
    {
        void ImportTagsDataset(List<ImportTagDto> tagsDto);

        void InsertPropertyTags(Property property);

        List<ExportPropertyDto> SelectLowFloorProperties();

        List<ExportPropertyDto> SelectHighFloorProperties();

        List<ExportPropertyDto> SelectCheapProperties();

        List<ExportPropertyDto> SelectExpensiveProperties();

        List<ExportPropertyDto> SelectSmallSizeProperties();

        List<ExportPropertyDto> SelectBigSizeProperties();

        List<ExportPropertyDto> SelectOldProperties();

        List<ExportPropertyDto> SelectNewProperties();
    }
}
