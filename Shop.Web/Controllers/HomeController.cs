using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Shop.Model.Entities;
using Shop.Repository;
using Shop.Web.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        public string username;
        public string password;
        private readonly ILogger<HomeController> _logger;
        private SlideRepository slideRepo;
        private ProductRepository productRepo;
        private CustomerRepository customerRepo;
        private PostRepository postRepo;
        private PostCategoryRepository postCategoryRepo;
        private PostTagRepository postTagRepo;
        private TeamRepository teamRepo;
        private ProductCategoryRepository productCategoryRepo;
        private TagRepository tagRepo;
        private ProductTagRepository productTagRepo;
        private CommentRepository commentRepo;
        private ContactRepository contactRepo;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            slideRepo = new SlideRepository();
            productRepo = new ProductRepository();
            customerRepo = new CustomerRepository();
            postRepo = new PostRepository();
            postCategoryRepo = new PostCategoryRepository();
            postTagRepo = new PostTagRepository();
            teamRepo = new TeamRepository();
            productCategoryRepo = new ProductCategoryRepository();
            tagRepo = new TagRepository();
            productTagRepo = new ProductTagRepository();
            commentRepo = new CommentRepository();
            contactRepo = new ContactRepository();
        }

        public IActionResult Index()
        {
            var slides = slideRepo.GetAll().OrderByDescending(p => p.DisplayOrder).ToList();
			var ourProducts = productRepo.GetAll().OrderByDescending(p=>p.Id).ToList();
            var customers = customerRepo.GetAll().OrderByDescending(p => p.CustomerId).ToList();
            var posts = postRepo.GetAll().OrderByDescending(p => p.Id).ToList();
			var tupleModel = new Tuple<List<Slide>,List<Product>, List<Customer>, List<Post>>(slides, ourProducts,customers,posts);
            if (HttpContext.Session.GetString("Username") != null)
            {
                // Người dùng đã đăng nhập, lấy tên người dùng từ phiên (session)
                username = HttpContext.Session.GetString("Username");
                ViewData["Username"] = username;
            }
            return View(tupleModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
		public IActionResult ProductDetails(int id)
		{
			var product = productRepo.GetById(id);
			//var relatedProduct = productRepo.GetAll().Where(p => p.CategoryId == product.CategoryId).ToList(); Method syntax
			var relatedProduct = (from p in productRepo.GetAll()
								  where p.CategoryId == product.CategoryId
								  select p).ToList(); //Query Syntax
			var tupleModel = new Tuple<Product, List<Product>>(product, relatedProduct);
			return View(tupleModel);
		}
		
   
        
        public IActionResult Store()
        {
			var productCategories = productCategoryRepo.GetAll().ToList();
            var products = productRepo.GetAll().ToList();
            var tupleModel = new Tuple<List<ProductCategory>, List<Product>>(productCategories, products);
            return View(tupleModel);
        }

	}
}