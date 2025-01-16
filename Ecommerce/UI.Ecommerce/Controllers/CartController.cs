using Microsoft.AspNetCore.Mvc;

namespace UI.Ecommerce.Controllers
{
    public class CartController : Controller
    {
        // GET: /Cart/Index
        public IActionResult Index()
        {
         
            return View(); 
        }
    }
}
