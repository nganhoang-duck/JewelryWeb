using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Shop.Model.Entities;
using Shop.Repository;
using System.Text.RegularExpressions;

namespace Shop.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductController : Controller
    {
        private ProductRepository productRepo;
        public ProductController()
        {
            productRepo = new ProductRepository();
        }
        public IActionResult Index()
        {
            var products = productRepo.GetAll().ToList();
            foreach (var item in products)
            {
                var des = item.Description;
                int length = des?.Length ?? 0;
                if (length > 30)
                {
                    item.Description = des.Substring(0, 30) + "...";
                }
                else
                {
                    item.Description = des;
                }
            }
            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var description = Request.Form["Description"];
                    description = Regex.Replace(description, "<.*?>", string.Empty);
                    product.Description = description; 
                    productRepo.Insert(product);
                    return Redirect("/Administrator/Product/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var description = Request.Form["Description"];
                    description = Regex.Replace(description, "<.*?>", string.Empty);
                    product.Description = description;
                    productRepo.Update(product);
                    return Redirect("/Administrator/Product/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            productRepo.Delete(id);
            return Redirect("/Administrator/Product/Index");
        }
    }
}
