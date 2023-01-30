using HoneyMarket.Utility;
using HoneyMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HoneyMarket.DAL.Repository.IRepository;
using NToastNotify;

namespace HoneyOnlineStore.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _catRepo;
        private readonly IToastNotification _toast;
        public CategoryController(ICategoryRepository catRepo, IToastNotification toast)
        {
            _catRepo = catRepo;
            _toast = toast;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> listCategories = _catRepo.GetAll();
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
                _catRepo.Add(cat);
                _catRepo.Save();
                _toast.AddSuccessToastMessage("Category created successfully");
                return RedirectToAction("Index");
            }
            else
            {
                _toast.AddErrorToastMessage("Error with created new category");
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
            var cat = _catRepo.Find(id);
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
                _catRepo.Update(cat);
                _catRepo.Save();
                _toast.AddSuccessToastMessage("Category edited successfully");

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
            var cat = _catRepo.Find(id);
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
            var cat = _catRepo.Find(id);
            if (cat == null)
            {
                return NotFound();
            }
            else
            {
                //delete all product images connected with category 
                _catRepo.DeleteBindImagesWithProduct(cat);
            }
            _catRepo.Remove(cat);
            _catRepo.Save();
            _toast.AddSuccessToastMessage("Category deleted successfully");
            return RedirectToAction("Index");
        }
    }
}
