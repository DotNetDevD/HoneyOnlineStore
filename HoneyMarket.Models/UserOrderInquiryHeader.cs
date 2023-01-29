using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoneyMarket.Models
{
    public class UserOrderInquiryHeader
    {
        [Key]
        public int Id { get; set; }
        public string ShopUserId { get; set; }
        [ForeignKey("ShopUserId")]
        public ShopUser ShopUser { get; set; }
        public DateTime InquiryDate { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
