	namespace FastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Enums;

    public class Order
	{
		public int Id { get; set; }

		[Required]
		public string Customer { get; set; }

		[Required]
		public DateTime DateTime { get; set; }

		[Required]
		[Column(TypeName = "VARCHAR(30)")]
		public OrderType Type { get; set; }

		[NotMapped]
		public decimal TotalPrice { get; set; }

		public int EmployeeId { get; set; }

		[Required]
		public Employee Employee { get; set; }

		public ICollection<OrderItem> OrderItems { get; set; }
	}
}