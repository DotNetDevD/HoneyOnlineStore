using HoneyMarket.Models;

namespace HoneyMarket.DAL.Repository.IRepository
{
    public interface IUserOrderInquiryHeaderRepository : IRepository<UserOrderInquiryHeader>
    {
        void Update(UserOrderInquiryHeader userOrderInquiryHeader);
    }
}
