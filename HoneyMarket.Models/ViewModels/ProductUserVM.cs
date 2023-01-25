namespace HoneyMarket.Models.ViewModels
{
    public class ProductUserVM
    {
        public ProductUserVM()
        {
            ProductList = new List<Product>();
        }
        public ShopUser ShopUser { get; set; }
        public IList<Product> ProductList { get; set; }
    }
}
