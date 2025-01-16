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

        public IActionResult Index()
        {
            return View();
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
        }
    }
}
