using Microsoft.AspNetCore.Mvc;

namespace UI.Ecommerce.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
