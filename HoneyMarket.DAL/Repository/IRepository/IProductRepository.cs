using HoneyMarket.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HoneyMarket.DAL.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);

        IEnumerable<SelectListItem> GetAllDropDownList(string obj); 
    }
}
