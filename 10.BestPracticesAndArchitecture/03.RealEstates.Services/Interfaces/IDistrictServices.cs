using _03.RealEstates.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace _03.RealEstates.Services.Interfaces
{
    public interface IDistrictServices
    {
        List<ExportPropertyDto> SelectPropertiesByDistrict(string district);

        List<ExportDistrictDto> SelectDistrictsByAveragePrice();
    }
}
