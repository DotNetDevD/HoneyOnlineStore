using HoneyMarket.Utility;
using HoneyOnlineStore.DAL;
using HoneyMarket.Models;
using HoneyMarket.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HoneyMarket.DAL.Repository.IRepository;
using NToastNotify;

namespace HoneyOnlineStore.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _prodRepo;
        private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductRepository prodRepo, IWebHostEnvironment webHostEnvironment, IToastNotification toast)
        {
            _prodRepo = prodRepo;
            _webHostEnvironment = webHostEnvironment;
            _toast = toast;
        }

        public IActionResult Index() 
        {
            // eager loading instead of usting foreach
            IEnumerable<Product> listProducts = _prodRepo.GetAll(includeProperties: "Category,ApplicationType");

            //foreach (var product in listProducts)
            //{
            //    product.Category = _db.Categories.FirstOrDefault(i => i.Id == product.CategoryId);
            //    product.ApplicationType = _db.ApplicationTypes.FirstOrDefault(i => i.Id == product.ApplicationTypeId);
            //}

            return View(listProducts);
        }
        
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategorySelectedList = _prodRepo.GetAllDropDownList(WebConstant.CategoryName),
                ApplicationTypeList = _prodRepo.GetAllDropDownList(WebConstant.ApplicationTypeName),
            };

            if (id == null)
            {
                // this for create
                return View(productVM);
            }
            else
            {
                productVM.Product = _prodRepo.Find(id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                // this for update
                return View(productVM);
            }
        }

        //Delete and Update Product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productVM.Product.Id == 0)
                {
                    //Creating
                    string upload = webRootPath + WebConstant.ImagesPath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.Image = fileName + extension;
                    _toast.AddSuccessToastMessage("Item added to cart successful!");

                    _prodRepo.Add(productVM.Product);
                }
                else
                {
                    //updating
                    //AsNotracking for no updating this object in database
                    var objFromDb = _prodRepo.FirstOrDefault(u => u.Id == productVM.Product.Id, isTracking:false);

                    //if we edit the images
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WebConstant.ImagesPath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //referrance for old file for update
                        var oldFile = Path.Combine(upload, objFromDb.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.Image = fileName + extension;
                    }
                    // if we don't edit the imgaes
                    else
                    {
                        productVM.Product.Image = objFromDb.Image;
                    }
                    _prodRepo.Update(productVM.Product);
                }
                _prodRepo.Save();
                return RedirectToAction("Index");
            }
            productVM.CategorySelectedList = _prodRepo.GetAllDropDownList(WebConstant.CategoryName);
            productVM.ApplicationTypeList = _prodRepo.GetAllDropDownList(WebConstant.ApplicationTypeName);
            return View(productVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            // eager loading
            Product product = _prodRepo.FirstOrDefault(u => u.Id == id, includeProperties:"Category,ApplicationType");
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var product = _prodRepo.Find(id);
            if (product == null)
            {
                _toast.AddWarningToastMessage("Something go wrong");
                return NotFound();
            }
            else
            {
                // find root of image
                string webRootPath = _webHostEnvironment.WebRootPath;
                string upload = webRootPath + WebConstant.ImagesPath;
                var imgFilePath = Path.Combine(upload, product.Image!);

                if (System.IO.File.Exists(imgFilePath))
                {
                    System.IO.File.Delete(imgFilePath);
                }
                _prodRepo.Remove(product);
                _prodRepo.Save();
                _toast.AddSuccessToastMessage("Item deleted successful!");

                return RedirectToAction("Index");
            }
        }
    }
}
