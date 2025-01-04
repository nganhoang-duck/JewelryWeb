using Microsoft.AspNetCore.Mvc;
using Shop.Repository;

namespace Shop.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {
        private TeamRepository teamRepo;
        public HomeController()
        {
            teamRepo= new TeamRepository();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Team()
        {
            var teams = teamRepo.GetAll().ToList();
            foreach (var item in teams)
            {
                var sayings = item.Sayings;
                int length = sayings?.Length ?? 0;
                if (length > 30)
                {
                    item.Sayings = sayings.Substring(0, 30) + "...";
                }
                else
                {
                    item.Sayings = sayings;
                }
            }
            return View(teams);
        }

    }
}
