using Microsoft.AspNetCore.Mvc;
using Shop.Model.Entities;
using Shop.Repository;

namespace Shop.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PostCategoryController : Controller
    {
        private PostCategoryRepository postCategoryRepo;
        public PostCategoryController()
        {
            postCategoryRepo = new PostCategoryRepository();
        }
        public IActionResult Index()
        {
            var postCategories = postCategoryRepo.GetAll().ToList();
            return View(postCategories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PostCategory postCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    postCategoryRepo.Insert(postCategory);
                    return Redirect("/Administrator/PostCategory/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(postCategory);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var postCategory = postCategoryRepo.GetById(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            return View(postCategory);
        }
        [HttpPost]
        public IActionResult Edit(PostCategory postCategory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    postCategoryRepo.Update(postCategory);
                    return Redirect("/Administrator/PostCategory/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(postCategory);
        }
        public IActionResult Delete(int id)
        {
            postCategoryRepo.Delete(id);
            return Redirect("/Administrator/PostCategory/Index");
        }
    }
}
