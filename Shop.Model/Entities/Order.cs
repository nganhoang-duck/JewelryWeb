using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Model.Entities
{
    public partial class Order
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerMobile { get; set; }
        public string? CustomerMessage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public bool? Status { get; set; }
    }
}
