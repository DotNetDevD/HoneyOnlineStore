using HoneyMarket.Utility;
using HoneyMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HoneyMarket.DAL.Repository.IRepository;

namespace HoneyOnlineStore.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _typeRepo;
        public ApplicationTypeController(IApplicationTypeRepository typeRepo)
        {
            _typeRepo = typeRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> listApp = _typeRepo.GetAll();
            return View(listApp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType app)
        {
            if (ModelState.IsValid)
            {
                _typeRepo.Add(app);
                _typeRepo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(app);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var app = _typeRepo.Find(id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType app)
        {
            if (ModelState.IsValid)
            {
                _typeRepo.Update(app);
                _typeRepo.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(app);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }
            var app = _typeRepo.Find(id);
            if (app == null)
            {
                return NotFound();
            }
            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var app = _typeRepo.Find(id);
            if (app == null)
            {
                return NotFound();
            }
            else
            {
                //delete all product images connected with applicationType 
                _typeRepo.DeleteBindImagesWithProduct(app);
            }
            _typeRepo.Remove(app);
            _typeRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
