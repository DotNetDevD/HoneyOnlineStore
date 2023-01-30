using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoneyMarket.Models.ViewModels
{
    public class UserOrderInquiryVM
    {
        public UserOrderInquiryHeader UserOrderInquiryHeader { get; set; }
        public IEnumerable<UserOrderInquiryDetail> UserOrderInquiryDetail { get; set; }
    }
}
