using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Data.Models
{
    public class Customer
    {
        public Customer()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Range(12, 110)]
        public int Age { get; set; }

        [Range(0.01, (double)decimal.MaxValue)]
        public decimal Balance { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
