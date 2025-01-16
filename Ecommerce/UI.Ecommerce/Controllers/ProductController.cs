using Microsoft.AspNetCore.Mvc;

namespace UI.Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
