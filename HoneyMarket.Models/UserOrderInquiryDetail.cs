using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoneyMarket.Models
{
    public class UserOrderInquiryDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserOrderInquiryId { get; set; }
        [ForeignKey("UserOrderInquiryId")]
        public UserOrderInquiryHeader UserOrderInquiryHeader { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
