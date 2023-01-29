using HoneyMarket.DAL.Repository;
using HoneyMarket.DAL.Repository.IRepository;

namespace HoneyMarket.Common.Extensions
{
    public static class AddRepositoryDependeciesExtensions
    {
        public static void AddRepositoryDependecies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IApplicationTypeRepository, ApplicationTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserOrderInquiryDetailRepository, UserOrderInquiryDetailRepository>();
            services.AddScoped<IUserOrderInquiryHeaderRepository, UserOrderInquiryHeaderRepository>();
            services.AddScoped<IShopUserRepository, ShopUserRepository>();
        }
    }
}
