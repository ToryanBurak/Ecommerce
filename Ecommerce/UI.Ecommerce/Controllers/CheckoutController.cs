using AutoMapper;
using BL.Store;
using Domain.Store;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UI.Ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public CheckoutController(ILogger<CheckoutController> logger, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            if (User.Identity.IsAuthenticated)
            {
                if (User.FindFirst("User.ConfirmState").Value == "false")
                {
                    return RedirectToAction("VerifyConfirmCode", "User");
                }
                UserBL userBL = new UserBL(_mapper);
                int userId = userBL.GetUserByGuid(new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)).Id;
                CartBL cartBL = new CartBL(_mapper);
                checkoutViewModel.Cart = cartBL.GetByUserId(userId);
            }
            return View(checkoutViewModel);
        }

        
        public IActionResult ConfirmMail()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel checkoutViewModel)
        {
            try
            {
                UserBL userBL = new UserBL(_mapper);
                UserDO user = userBL.GetUserByGuid(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                if (!checkoutViewModel.UseSavedAdress)
                {
                    AdressBL adressBL = new AdressBL(_mapper);
                    checkoutViewModel.Adress.Id = adressBL.SaveAdress(user.Id, checkoutViewModel.Adress);
                }
                CheckoutBL checkoutBL = new CheckoutBL(_mapper);
                CartBL cartBL = new CartBL(_mapper);
                checkoutViewModel.Cart = cartBL.GetByUserId(user.Id);
                bool success = checkoutBL.Checkout(checkoutViewModel, user);
                if (success)
                {
                    cartBL.RefreshCart(user.Id);
                }
            }
            catch (Exception)
            {
                return View("Error", "Üzgünüz.Sipariş Sırasında bir hata oluştur.Lütfen Daha sonra tekrar deneyiniz.");
            }
            
            return View("Success","Bizi Tercih Ettiğiniz için teşekkür ederiz.Siparişiniz Başarıyla oluşturuldu");
        }
        [HttpGet]
        public JsonResult GetDistrictListByCityKey(int cityKey)
        {
            var districtList = Helper.SelectHelper.GetDistrictListByCityKey(cityKey);
            var result = districtList.Select(d => new { value = d.Value, text = d.Text }).ToList();
            return Json(result);
        }
        [HttpGet]
        public JsonResult GetTownListByDistrictKey(int districtKey)
        {
            var townList = Helper.SelectHelper.GetTownListByDistrictKey(districtKey);
            var result = townList.Select(d => new { value = d.Value, text = d.Text }).ToList();
            return Json(result);
        }
    }
}
