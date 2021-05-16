using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _02.RealEstates.Models
{
    public class PropertyType
    {
        public PropertyType()
        {
            Properties = new HashSet<Property>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
