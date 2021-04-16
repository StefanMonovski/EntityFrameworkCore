using Cinema.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Data.Models
{
    public class Movie
    {
        public Movie()
        {
            Projections = new HashSet<Projection>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }

        public Genre Genre { get; set; }

        public TimeSpan Duration { get; set; }

        [Range(1.00, 10.00)]
        public double Rating { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Director { get; set; }

        public ICollection<Projection> Projections { get; set; }
    }
}
