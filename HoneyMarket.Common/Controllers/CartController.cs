using HoneyMarket.Utility;
using HoneyMarket.Utility.Extensions;
using HoneyMarket.Models;
using HoneyMarket.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using HoneyMarket.DAL.Repository.IRepository;
using NToastNotify;

namespace HoneyOnlineStore.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUserOrderInquiryDetailRepository _userOrderInquiryDetailRepo;
        private readonly IUserOrderInquiryHeaderRepository _userOrderInquiryHeaderRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductRepository _productRepository;
        private readonly IShopUserRepository _shopUserRepo;
        private readonly IToastNotification _toast;

        //binding post request
        [BindProperty]
        public ProductUserVM ProductUserVM { get; set; }
        public CartController(IWebHostEnvironment webHostEnvironment, IProductRepository productRepo,
            IUserOrderInquiryDetailRepository userOrderInquiryDetailRepo, IUserOrderInquiryHeaderRepository userOrderInquiryHeaderRepo,
            IShopUserRepository shopUserRepo, IToastNotification toast)
        {
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepo;
            _userOrderInquiryDetailRepo = userOrderInquiryDetailRepo;
            _userOrderInquiryHeaderRepo = userOrderInquiryHeaderRepo;
            _shopUserRepo = shopUserRepo;
            _toast = toast;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart);
            }
            //all product in cart
            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            //list of product in cart                   .Where(u => prodInCart.Contains(u.Id));
            IEnumerable<Product> prodList = _productRepository.GetAll(u => prodInCart.Contains(u.Id));
                
            return View(prodList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Summary()
        {
            //identification user by id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //var userId = User.FindFirstValue(ClaimTypes.Name);

            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart);
            }

            List<int> prodInCart = shoppingCartList.Select(i => i.ProductId).ToList();
            IEnumerable<Product> prodList = _productRepository.GetAll(u => prodInCart.Contains(u.Id));

            ProductUserVM = new ProductUserVM()
            {
                ShopUser = _shopUserRepo.FirstOrDefault(u => u.Id == claim.Value),
                ProductList = prodList.ToList()
            };

            return View(ProductUserVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVM ProductUserVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity; // ref for user in current session
            // claim contains Id user 
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            UserOrderInquiryHeader userOrderInquiryHeader = new()
            {
                ShopUserId = claim.Value,
                FullName = ProductUserVM.ShopUser.FullName,
                Email = ProductUserVM.ShopUser.Email,
                PhoneNumber = ProductUserVM.ShopUser.PhoneNumber,
                InquiryDate = DateTime.Now
            };

            _userOrderInquiryHeaderRepo.Add(userOrderInquiryHeader);
            _userOrderInquiryHeaderRepo.Save();

            foreach (var prod in ProductUserVM.ProductList)
            {
                UserOrderInquiryDetail userOrderInquiryDetail = new()
                {
                    UserOrderInquiryId = userOrderInquiryHeader.Id,
                    ProductId = prod.Id,
                };
                _userOrderInquiryDetailRepo.Add(userOrderInquiryDetail);
            }
            _userOrderInquiryDetailRepo.Save();

            Guid jsonOrderId = Guid.NewGuid();
            using (FileStream fs = new FileStream($@"Orders\{jsonOrderId}.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<ProductUserVM>(fs, ProductUserVM);
            }
            _toast.AddSuccessToastMessage("Order added seccussful!");

            return RedirectToAction(nameof(InquiryConfirmation));
        }

        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Remove(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart).Count() > 0)
            {
                //session exsits
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart);
            }

            shoppingCartList.Remove(shoppingCartList.FirstOrDefault(u => u.ProductId == id));
            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
    }
}
