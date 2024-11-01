using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Model.Entities;
using Shop.Repository;

using System.Text.RegularExpressions;

namespace Shop.Web.Controllers
{
	public class AccountController : Controller
	{
		private UserRepository userRepo;
		public AccountController()
		{
			userRepo = new UserRepository();
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					userRepo.Insert(user);
					return Redirect("/Account/Login");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}
			return Redirect("/Account/Login");
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(User user)
		{
			// Kiểm tra thông tin đăng nhập
			if (user.Username == "admin" && user.Password == "admin123")
			{
				// Nếu là Admin, chuyển hướng đến trang Administrator/Home/Index
				return Redirect("/Administrator/Home/Index");
			}
			using (var dbContext = new ByDuckieContext())
			{
				// Kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu hay không
				var existingUser = dbContext.Users.FirstOrDefault(u => u.Username == user.Username);
                var existingPass = dbContext.Users.FirstOrDefault(u => u.Password == user.Password);
                if (existingUser != null && existingPass != null)
				{
					// Nếu là người dùng thông thường, chuyển hướng đến trang Home/Index
					HttpContext.Session.SetString("Username", user.Username);
                    return Redirect("/Home/Index");
				}
				else
				{
                    TempData["ErrorMessage"] = "Invalid username or password";
                    return RedirectToAction("Login");
				}
			}
			
		}
		[HttpPost]
		public IActionResult Logout()
		{
			// Xóa thông tin người dùng đã lưu trong phiên (session) khi đăng xuất
			HttpContext.Session.Remove("Username");

			// Chuyển hướng đến trang chủ hoặc trang đăng nhập (tuỳ theo yêu cầu của bạn)
			return RedirectToAction("Index", "Home");
		}
	}
}
