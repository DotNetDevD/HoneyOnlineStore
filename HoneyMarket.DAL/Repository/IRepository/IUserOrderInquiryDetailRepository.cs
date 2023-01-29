using HoneyMarket.Models;

namespace HoneyMarket.DAL.Repository.IRepository
{
    public interface IUserOrderInquiryDetailRepository : IRepository<UserOrderInquiryDetail>
    {
        void Update(UserOrderInquiryDetail userOrderInquiryDetail);
    }
}
