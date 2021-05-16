using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _02.RealEstates.Models
{
    public class Building
    {
        public Building()
        {
            Properties = new HashSet<Property>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }

        public int? Floors { get; set; }

        public int? Year { get; set; }

        public int BuildingTypeId { get; set; }

        public BuildingType BuildingType { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
