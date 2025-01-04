using System;
using System.Collections.Generic;

namespace Shop.Model.Entities
{
	public partial class Customer
	{
		public int CustomerId { get; set; }
		public string? Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Address { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Email { get; set; }
		public string? Gender { get; set; }
		public string? CustomerFeedback { get; set; }
		public string? Image { get; set; }
	}
}
