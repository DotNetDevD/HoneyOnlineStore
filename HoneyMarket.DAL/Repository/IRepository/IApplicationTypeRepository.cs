using HoneyMarket.Models;


namespace HoneyMarket.DAL.Repository.IRepository
{
    public interface IApplicationTypeRepository : IRepository<ApplicationType>
    {
        void Update(ApplicationType type);
        void DeleteBindImagesWithProduct(ApplicationType type);
    }
}
