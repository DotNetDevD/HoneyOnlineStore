using HoneyMarket.DAL.Repository.IRepository;
using HoneyMarket.Models;
using HoneyMarket.Utility;
using HoneyOnlineStore.DAL;
using Microsoft.AspNetCore.Hosting;

namespace HoneyMarket.DAL.Repository
{
    public class ApplicationTypeRepository : Repository<ApplicationType>, IApplicationTypeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicationTypeRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) : base(db)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Update(ApplicationType type)
        {
            var category = _db.Categories.FirstOrDefault(c => c.Id == type.Id);
            if (category != null)
            {
                category.Name = type.Name;
            }
        }

        //delete all product images connected with category 
        public void DeleteBindImagesWithProduct(ApplicationType type)
        {
            var products = _db.Products.Where(i => i.CategoryId == type.Id);
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
