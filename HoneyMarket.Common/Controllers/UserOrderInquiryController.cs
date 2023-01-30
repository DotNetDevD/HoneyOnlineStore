using HoneyMarket.DAL.Repository.IRepository;
using HoneyMarket.Models;
using HoneyMarket.Models.ViewModels;
using HoneyMarket.Utility;
using HoneyMarket.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace HoneyMarket.Common.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class UserOrderInquiryController : Controller
    {
        private readonly IUserOrderInquiryHeaderRepository _userOrderInquiryHeaderRepo;
        private readonly IUserOrderInquiryDetailRepository _userOrderInquiryDetailRepo;
        private readonly IToastNotification _toast;

        [BindProperty] // в случае создания метода POST данные будут доступы в нем
        public UserOrderInquiryVM UserOrderInquiryVM { get; set; }

        public UserOrderInquiryController(IUserOrderInquiryDetailRepository userOrderInquiryDetailRepo,
            IUserOrderInquiryHeaderRepository userOrderInquiryHeaderRepo,
            IToastNotification toast)
        {
            _userOrderInquiryDetailRepo = userOrderInquiryDetailRepo;
            _userOrderInquiryHeaderRepo = userOrderInquiryHeaderRepo;
            _toast = toast;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            UserOrderInquiryVM = new()
            {
                UserOrderInquiryHeader = _userOrderInquiryHeaderRepo.FirstOrDefault(u => u.Id == id),
                UserOrderInquiryDetail = _userOrderInquiryDetailRepo.GetAll(u => u.UserOrderInquiryId == id, 
                includeProperties: "Product")
            };
            return View(UserOrderInquiryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            UserOrderInquiryVM.UserOrderInquiryDetail = _userOrderInquiryDetailRepo.GetAll(u => u.UserOrderInquiryId == UserOrderInquiryVM.UserOrderInquiryHeader.Id);

            foreach (var detail in UserOrderInquiryVM.UserOrderInquiryDetail)
            {
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    ProductId = detail.ProductId
                };
                shoppingCartList.Add(shoppingCart);
            }
            HttpContext.Session.Clear();
            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCartList);
            HttpContext.Session.Set(WebConstant.SessionInquiryId, UserOrderInquiryVM.UserOrderInquiryHeader.Id);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult Delete()
        {
            UserOrderInquiryHeader userOrderInquiryHeader = _userOrderInquiryHeaderRepo.
                FirstOrDefault(u => u.Id == UserOrderInquiryVM.UserOrderInquiryHeader.Id);
            IEnumerable<UserOrderInquiryDetail> userOrderInquiryDetails = _userOrderInquiryDetailRepo.
                GetAll(u => u.UserOrderInquiryId == UserOrderInquiryVM.UserOrderInquiryHeader.Id);

            _userOrderInquiryDetailRepo.RemoveRange(userOrderInquiryDetails);
            _userOrderInquiryHeaderRepo.Remove(userOrderInquiryHeader);
            _userOrderInquiryHeaderRepo.Save();

            _toast.AddSuccessToastMessage("Order deleted seccsesful!");

            return RedirectToAction(nameof(Index));
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetInquiryList()
        {
            return Json(new { data = _userOrderInquiryHeaderRepo.GetAll() });
        }
        #endregion
    }
}
