using Microsoft.AspNetCore.Mvc;

namespace BurSimEmFur_Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            int a = 1;
            return View();
        }
    }
}
