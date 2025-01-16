using Microsoft.AspNetCore.Mvc;

namespace UI.Ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConfirmMail()
        {

            return View();
        }
    }
}
