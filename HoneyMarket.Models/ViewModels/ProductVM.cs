using Microsoft.AspNetCore.Mvc.Rendering;

namespace HoneyMarket.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem>? CategorySelectedList { get; set; }
        public IEnumerable<SelectListItem>? ApplicationTypeList { get; set; }
    }
}
