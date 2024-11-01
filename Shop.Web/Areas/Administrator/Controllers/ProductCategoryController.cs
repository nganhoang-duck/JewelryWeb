using Microsoft.AspNetCore.Mvc;
using Shop.Model.Entities;
using Shop.Repository;
namespace Shop.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductCategoryController : Controller
    {
        private ProductCategoryRepository productCategoryRepo;
        public ProductCategoryController()
        {
            productCategoryRepo = new ProductCategoryRepository();
        }
        public IActionResult Index()
        {
            var productCategories = productCategoryRepo.GetAll().ToList();
            return View(productCategories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    productCategoryRepo.Insert(productCategory);
                    return Redirect("/Administrator/ProductCategory/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(productCategory);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productCategory = productCategoryRepo.GetById(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }
        [HttpPost]
        public IActionResult Edit(ProductCategory productCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productCategoryRepo.Update(productCategory);
                    return Redirect("/Administrator/ProductCategory/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(productCategory);
        }
        public IActionResult Delete(int id)
        {
            productCategoryRepo.Delete(id);
            return Redirect("/Administrator/ProductCategory/Index");
        }
    }
}
