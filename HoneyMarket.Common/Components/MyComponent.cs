using HoneyOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HoneyOnlineStore.Components
{
    public class MyComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}