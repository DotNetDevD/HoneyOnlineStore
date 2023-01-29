using HoneyMarket.DAL.Repository.IRepository;
using HoneyMarket.Models;
using HoneyMarket.Utility;
using HoneyOnlineStore.DAL;
using Microsoft.AspNetCore.Hosting;

namespace HoneyMarket.DAL.Repository
{
    public class ShopUserRepository : Repository<ShopUser>, IShopUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ShopUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShopUser shopUser)
        {
            _db.ShopUsers.Update(shopUser);
        }
    }
}
