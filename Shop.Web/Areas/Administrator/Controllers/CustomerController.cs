using Microsoft.AspNetCore.Mvc;
using Shop.Model.Entities;
using Shop.Repository;
using System.Text.RegularExpressions;

namespace Shop.Web.Areas.Administrator.Controllers
{
	[Area("Administrator")]
	public class CustomerController : Controller
	{
		private CustomerRepository customerRepo;
		public CustomerController()
		{
			customerRepo = new CustomerRepository();
		}
		public IActionResult Index()
		{
			var customers = customerRepo.GetAll().ToList();
			return View(customers);
		}
		[HttpGet]
		
		public IActionResult Delete(int id)
		{
			customerRepo.Delete(id);
			return Redirect("/Administrator/Customer/Index");
		}
	}
}
