using Microsoft.AspNetCore.Mvc;

namespace UI.Ecommerce.Controllers
{
    public class CheckoutController : Controller
    {
        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DetailsPost()
        {
            return RedirectToAction("Complete");
        }

        public IActionResult Complete()
        {
            return View();
        }
    }
}
