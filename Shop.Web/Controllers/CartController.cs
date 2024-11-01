using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Model.Entities;
using Shop.Repository;
using Shop.Web.Models;
using System.Web.Helpers;

namespace Shop.Web.Controllers
{
	public class CartController : Controller
	{
		private ProductRepository productRepo;
		private OrderRepository orderRepo;
		private OrderDetailRepository orderDetailRepo;
		public CartController()
		{
			productRepo = new ProductRepository();
			orderRepo = new OrderRepository();
			orderDetailRepo = new OrderDetailRepository();
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult AddToCart(int id)
		{
			List<CartItem> cart;
			if (HttpContext.Session.Get<List<CartItem>>("Cart") == null)
			{
				cart = new List<CartItem>();
				cart.Add(new CartItem { ProductOrder = productRepo.GetById(id), Quantity = 1 });
			}
			else //trong giỏ hàng đã có sản phẩm rồi
			{
				cart = (List<CartItem>)HttpContext.Session.Get<List<CartItem>>("Cart");
				CartItem cartItem = cart.SingleOrDefault(x => x.ProductOrder.Id == id);
				if (cartItem != null) //đã có sản phẩm này trong giỏ hàng rồi
				{
					cartItem.Quantity++;
				}
				else //chưa có sản phẩm này trong giỏ hàng.
				{
					cart.Add(new CartItem { ProductOrder = productRepo.GetById(id), Quantity = 1 });
				}
			}
			//Cập nhật giỏ hàng
			HttpContext.Session.Set<List<CartItem>>("Cart", cart);
			//Trả về số lượng hàng trong giỏ
			return Json(new { total = cart.Sum(x => x.Quantity) });
		}
		[HttpPost]
		public IActionResult AddToCartDetail(int id, int quantity)
		{
			List<CartItem> cart;

			if (HttpContext.Session.Get<List<CartItem>>("Cart") == null)
			{
				cart = new List<CartItem>();
				cart.Add(new CartItem { ProductOrder = productRepo.GetById(id), Quantity = quantity });
			}
			else
			{
				cart = (List<CartItem>)HttpContext.Session.Get<List<CartItem>>("Cart");
				CartItem cartItem = cart.SingleOrDefault(x => x.ProductOrder.Id == id);

				if (cartItem != null)
				{
					cartItem.Quantity += quantity;
				}
				else
				{
					cart.Add(new CartItem { ProductOrder = productRepo.GetById(id), Quantity = quantity });
				}
			}

			HttpContext.Session.Set<List<CartItem>>("Cart", cart);

			// Trả về số lượng hàng trong giỏ
			return Json(new { total = cart.Sum(x => x.Quantity) });
		}

		public IActionResult Cart()
		{
			return View();
		}
		[HttpPost]
		public IActionResult UpdateCart(int id, int quantity)
		{
			List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");
			if (cart != null)
			{
				CartItem cartItem = cart.SingleOrDefault(x => x.ProductOrder.Id == id);
				if (cartItem != null)
				{
					if (quantity >= 1)
					{
						cartItem.Quantity = quantity;
					} 
					HttpContext.Session.Set<List<CartItem>>("Cart", cart);
				}
			}
			return Json(new { total = cart?.Sum(x => x.Quantity) ?? 0 });
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");
			if (cart != null)
			{
				CartItem cartItem = cart.SingleOrDefault(x => x.ProductOrder.Id == id);
				if (cartItem != null)
				{
					cart.Remove(cartItem); // Remove the entire item from the cart
										   // Update the cart in the session
					HttpContext.Session.Set<List<CartItem>>("Cart", cart);
				}
			}
			// Return the updated total quantity of items in the cart as a JSON object
			return Json(new { total = cart?.Sum(x => x.Quantity) ?? 0 });
		}
		[HttpGet]
		public IActionResult Checkout()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Add the new order to the database using the repository
					int lastOrderId = orderRepo.GetAll().OrderByDescending(o => o.Id).FirstOrDefault()?.Id ?? 0;
					order.Id = lastOrderId + 1;
					order.CreatedDate= DateTime.Now;
					order.Status = true;
					order.CustomerMessage = Request.Form["CustomerMessage"];
                    order.PaymentStatus = "Chưa thanh toán";
					orderRepo.Insert(order);

					List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");
					if (cart != null)
					{
                        foreach (var item in cart)
                        {
                            OrderDetail orderDetail = new OrderDetail
                            {
                                OrderId = order.Id,
                                ProductId = item.ProductOrder.Id,
                                Quantity = item.Quantity
                            };
                            orderDetailRepo.Insert(orderDetail);
                        }
                    }
					return Redirect("/Cart/Cart");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}
			HttpContext.Session.Remove("Cart");
			return Redirect("/Cart/Cart");
		}

	}
}
