using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NuGet.Protocol.Core.Types;
using Shop.IRepository;
using Shop.Model.Entities;
using Shop.Repository;
using Shop.Web.Models;
using System.Diagnostics;
using System.Linq;

namespace Shop.Web.Controllers
{
	public class MenuController : Controller
	{
		private InfoRepository infoRepo;

		public MenuController()
		{
			infoRepo= new InfoRepository();
		}
		public ActionResult Index()
		{
			var info = infoRepo.GetAll().FirstOrDefault();
			ViewBag.Info = info;
			return View();
		}
	}
}
