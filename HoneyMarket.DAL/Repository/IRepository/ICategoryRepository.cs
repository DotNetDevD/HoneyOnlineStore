using HoneyMarket.Models;

namespace HoneyMarket.DAL.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category cat);
        void DeleteBindImagesWithProduct(Category cat);
    }
}
