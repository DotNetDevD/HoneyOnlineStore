using Microsoft.AspNetCore.Identity;

namespace HoneyOnlineStore.Models
{
    public class ShopUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
