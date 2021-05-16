using System;
using System.Collections.Generic;
using System.Text;

namespace _02.RealEstates.Models
{
    public class Property
    {
        public Property()
        {
            PropertiesTags = new HashSet<PropertyTag>();
        }
        
        public int Id { get; set; }

        public int Size { get; set; }

        public int? YardSize { get; set; }

        public int? Floor { get; set; }

        public decimal? Price { get; set; }

        public int PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }

        public int BuildingId { get; set; }

        public Building Building { get; set; }

        public ICollection<PropertyTag> PropertiesTags { get; set; }
    }
}
