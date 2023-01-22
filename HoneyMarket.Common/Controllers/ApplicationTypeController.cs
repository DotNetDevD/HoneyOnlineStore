using HoneyMarket.Utility;
using HoneyOnlineStore.DAL;
using HoneyOnlineStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyOnlineStore.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicationTypeController(ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _db = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> listApp = _db.ApplicationTypes;
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
                _db.ApplicationTypes.Add(app);
                _db.SaveChanges();
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
            var app = _db.ApplicationTypes.Find(id);
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
                _db.ApplicationTypes.Update(app);
                _db.SaveChanges();
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
            var app = _db.ApplicationTypes.Find(id);
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
            var app = _db.ApplicationTypes.Find(id);
            if (app == null)
            {
                return NotFound();
            }
            else
            {
                //delete all product images connected with applicationType 
                var products = _db.Products.Where(i => i.ApplicationTypeId == app.Id);
                if (products != null)
                {
                    foreach (var product in products)
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
                _db.ApplicationTypes.Remove(app);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
