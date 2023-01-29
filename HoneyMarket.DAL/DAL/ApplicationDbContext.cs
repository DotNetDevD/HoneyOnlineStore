using HoneyMarket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HoneyOnlineStore.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShopUser> ShopUsers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<UserOrderInquiryDetail> UserOrderInquiryDetails { get; set; }
        public DbSet<UserOrderInquiryHeader> UserOrderInquiryHeader { get; set; }
    }
}
