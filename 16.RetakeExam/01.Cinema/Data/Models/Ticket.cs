using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Range(0.01, (double)decimal.MaxValue)]
        public decimal Price { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ProjectionId { get; set; }

        public Projection Projection { get; set; }
    }
}
