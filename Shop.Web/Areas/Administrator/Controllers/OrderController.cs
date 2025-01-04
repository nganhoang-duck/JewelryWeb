using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Shop.Model.Entities;
using Shop.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class OrderController : Controller
    {
        private OrderRepository orderRepo;
        public OrderController()
        {
            orderRepo = new OrderRepository();
        }
        public IActionResult Index()
        {
            var orders = orderRepo.GetAll().ToList();
            return View(orders);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderRepo.Update(order);
                    return Redirect("/Administrator/Order/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(order);
        }
        public IActionResult Delete(int id)
        {
            orderRepo.Delete(id);
            return Redirect("/Administrator/Order/Index");
        }
    }
}

