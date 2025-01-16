using Microsoft.AspNetCore.Mvc;

namespace UI.Ecommerce.Controllers
{
    public class VerifyCodeController : Controller
    {
        [HttpGet]
        public IActionResult VerifyCode()
        {
            return View(); // Bu, Views/VerifyCode/VerifyCode.cshtml arar
        }
    }
}
