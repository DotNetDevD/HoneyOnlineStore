using HoneyOnlineStore.DAL;
using HoneyOnlineStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyOnlineStore.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _db = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> listCategories = _db.Categories;
            return View(listCategories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(cat);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var cat = _db.Categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category cat)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(cat);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var cat = _db.Categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var cat = _db.Categories.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                //delete all product images connected with category 
                var products = _db.Products.Where(i => i.CategoryId == cat.Id);
                if (products != null)
                {
                    foreach(var product in products)
                    {
                        // find root of image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string upload = webRootPath + WebConstant.ImagesPath;
                        var imgFilePath = Path.Combine(upload, product.Image!);

                        if (System.IO.File.Exists(imgFilePath))
                        {
                            System.IO.File.Delete(imgFilePath);
                        }
                    }
                }
                _db.Categories.Remove(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
