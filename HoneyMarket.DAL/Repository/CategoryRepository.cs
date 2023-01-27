using HoneyMarket.DAL.Repository.IRepository;
using HoneyMarket.Models;
using HoneyMarket.Utility;
using HoneyOnlineStore.DAL;
using Microsoft.AspNetCore.Hosting;

namespace HoneyMarket.DAL.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) : base(db)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Update(Category cat)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == cat.Id);
            if(category != null)
            {
                category.Name = cat.Name;
                category.OrderCategory = cat.OrderCategory;
            }
        }

        //delete all product images connected with category 
        public void DeleteBindImagesWithProduct(Category cat)
        {
            var products = _db.Products.Where(i => i.CategoryId == cat.Id);
            if (products != null)
            {
                foreach (var product in products)
                {
                    // find root of image
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    string upload = webRootPath + WebConstant.ImagesPath;
                    var imgFilePath = Path.Combine(upload, product.Image!);

                    if (File.Exists(imgFilePath))
                    {
                        File.Delete(imgFilePath);
                    }
                }
            }
        }
    }
}
