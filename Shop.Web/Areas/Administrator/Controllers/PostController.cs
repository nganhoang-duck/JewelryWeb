using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Shop.Model.Entities;
using Shop.Repository;
using System.Text.RegularExpressions;

namespace Shop.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PostController : Controller
    {
        private PostRepository postRepo;
        public PostController()
        {
            postRepo = new PostRepository();
        }
        public IActionResult Index()
        {
            var posts = postRepo.GetAll().ToList();
            foreach (var item in posts)
            {
                var content = item.Content;
                int length = content?.Length ?? 0;
                if (length > 30)
                {
                    item.Content = content.Substring(0, 30) + "...";
                }
                else
                {
                    item.Content = content;
                }
            }
            return View(posts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var content = Request.Form["Content"];
                    content = Regex.Replace(content, "<.*?>", string.Empty);
                    post.Content = content;
                    post.WriteDate = DateTime.Now;
                    postRepo.Insert(post);
                    return Redirect("/Administrator/Post/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(post);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = postRepo.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        [HttpPost]
        public IActionResult Edit(Post post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var content = Request.Form["Content"];
                    content = Regex.Replace(content, "<.*?>", string.Empty);
                    post.Content = content;
                    postRepo.Update(post);
                    return Redirect("/Administrator/Post/Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(post);
        }
        public IActionResult Delete(int id)
        {
            postRepo.Delete(id);
            return Redirect("/Administrator/Post/Index");
        }
    }
}
