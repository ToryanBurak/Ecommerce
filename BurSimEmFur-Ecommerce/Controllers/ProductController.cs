using Microsoft.AspNetCore.Mvc;

namespace BurSimEmFur_Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
