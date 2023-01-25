using Microsoft.AspNetCore.Identity;

namespace HoneyMarket.Models
{
    public class ShopUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
