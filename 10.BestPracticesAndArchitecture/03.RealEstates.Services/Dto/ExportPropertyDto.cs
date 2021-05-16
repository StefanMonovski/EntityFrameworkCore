using System;
using System.Collections.Generic;
using System.Text;

namespace _03.RealEstates.Services.Dto
{
    public class ExportPropertyDto
    {        
        public int Size { get; set; }

        public int? YardSize { get; set; }

        public int? Floor { get; set; }

        public string District { get; set; }

        public int? Year { get; set; }

        public string Type { get; set; }

        public decimal? Price { get; set; }

        public override string ToString()
        {
            return $"Price: {Price}, District: {District}, PropertyType: {Type}, Size: {Size}";
        }
    }
}
