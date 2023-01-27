using HoneyMarket.Utility;
using HoneyMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HoneyMarket.DAL.Repository.IRepository;

namespace HoneyOnlineStore.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _carRepo;

        public CategoryController(ICategoryRepository catRepo)
        {
            _carRepo = catRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> listCategories = _carRepo.GetAll();
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
                _carRepo.Add(cat);
                _carRepo.Save();
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
            var cat = _carRepo.Find(id);
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
                _carRepo.Update(cat);
                _carRepo.Save();
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
            var cat = _carRepo.Find(id);
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
            var cat = _carRepo.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                //delete all product images connected with category 
                _carRepo.DeleteBindImagesWithProduct(cat);
            }
            _carRepo.Remove(cat);
            _carRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
