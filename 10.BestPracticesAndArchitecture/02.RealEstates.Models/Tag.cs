using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace _02.RealEstates.Models
{
    public class Tag
    {
        public Tag()
        {
            PropertiesTags = new HashSet<PropertyTag>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<PropertyTag> PropertiesTags { get; set; }
    }
}
