using HoneyMarket.Models;

namespace HoneyMarket.DAL.Repository.IRepository
{
    public interface IShopUserRepository : IRepository<ShopUser>
    {
        void Update(ShopUser shopUser);
    }
}
