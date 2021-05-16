using System;
using System.Collections.Generic;
using System.Text;

namespace _03.RealEstates.Services.Dto
{
    public class ExportDistrictDto
    {
        public string Name { get; set; }

        public decimal? AveragePrice { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}, Average price: {AveragePrice:f2}";
        }
    }
}
