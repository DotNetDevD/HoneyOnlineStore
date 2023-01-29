using HoneyMarket.DAL.Repository.IRepository;
using HoneyMarket.Models;
using HoneyOnlineStore.DAL;

namespace HoneyMarket.DAL.Repository
{
    public class UserOrderInquiryDetailRepository : Repository<UserOrderInquiryDetail>, IUserOrderInquiryDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public UserOrderInquiryDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(UserOrderInquiryDetail userOrderInquiryDetail)
        {
            _db.UserOrderInquiryDetails.Update(userOrderInquiryDetail);
        }
    }
}
