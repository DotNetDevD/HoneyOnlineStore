using HoneyMarket.DAL.Repository.IRepository;
using HoneyMarket.Models;
using HoneyOnlineStore.DAL;

namespace HoneyMarket.DAL.Repository
{
    public class UserOrderInquiryHeaderRepository : Repository<UserOrderInquiryHeader>, IUserOrderInquiryHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public UserOrderInquiryHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserOrderInquiryHeader userOrderInquiryHeader)
        {
            _db.UserOrderInquiryHeader.Update(userOrderInquiryHeader);
        }
    }
}
