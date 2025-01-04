using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Shop.Model.Entities;
using Shop.Repository;
using System.Text.RegularExpressions;

namespace Shop.Web.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	public class UserController : Controller
	{
		private UserRepository userRepo;
		public UserController()
		{
			userRepo = new UserRepository();
		}
		public IActionResult Index()
		{
			var users = userRepo.GetAll().ToList();
            
            return View(users);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(User user)
		{
			try
			{
				if (ModelState.IsValid)
				{

					userRepo.Insert(user);
					return Redirect("/Administrator/User/Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}
			return View(user);
		}
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var user = userRepo.GetById(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}
		[HttpPost]
		public IActionResult Edit(User user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					userRepo.Update(user);
					return Redirect("/Administrator/User/Index");
				}
			}
			catch (Exception e)
			{
				ModelState.AddModelError("", e.Message);
			}
			return View(user);
		}
		public IActionResult Delete(int id)
		{
			userRepo.Delete(id);
			return Redirect("/Administrator/User/Index");
		}
	}

}
