using AutoMapper;
using BL.Store;
using Domain.Store;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace UI.Ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IEmailService _emailService;

        // Dependency Injection ile EmailService ekleniyor
        public CheckoutController(IEmailService emailService)
        {
            _emailService = emailService;
        }

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

        
        // Kullanıcının e-posta adresine doğrulama bağlantısı gönderen metod
        [HttpPost]
        public async Task<IActionResult> SendConfirmationEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Geçerli bir e-posta adresi giriniz.");
                return View("Index");
            }

            // Doğrulama bağlantısı oluştur
            var token = Guid.NewGuid().ToString();
            var confirmationLink = Url.Action("ConfirmMail", "Checkout", new { token }, Request.Scheme);

            // E-postayı gönder
            await _emailService.SendEmailAsync(email, "E-posta Doğrulama", $"Lütfen doğrulama için <a href='{confirmationLink}'>buraya tıklayın</a>.");

            ViewBag.Message = "Doğrulama bağlantısı e-posta adresinize gönderildi.";
            return View("Index");
        }

        // E-posta doğrulama işlemini gerçekleştiren metod
        [HttpGet]
        public IActionResult ConfirmMail(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Geçersiz doğrulama isteği.");
            }

            // Token doğrulama işlemini burada yapın
            bool isTokenValid = ValidateToken(token);
            if (!isTokenValid)
            {
                return BadRequest("Doğrulama başarısız. Geçersiz token.");
            }

            ViewBag.Message = "E-posta başarıyla doğrulandı.";
            return View();
        }

        private bool ValidateToken(string token)
        {
            // Token doğrulama işlemini burada gerçekleştirin
            // Örn: Veritabanından kontrol edilebilir
            return true; // Örnek olarak her zaman doğru kabul ettik
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
