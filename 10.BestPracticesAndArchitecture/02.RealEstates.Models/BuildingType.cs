using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _02.RealEstates.Models
{
    public class BuildingType
    {
        public BuildingType()
        {
            Buildings = new HashSet<Building>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public ICollection<Building> Buildings { get; set; }
    }
}
