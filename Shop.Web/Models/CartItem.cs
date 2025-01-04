using Shop.Model.Entities;

namespace Shop.Web.Models
{
	public class CartItem
	{
		public Product ProductOrder { get; set; }
		public int Quantity { get; set; }
	}
}
