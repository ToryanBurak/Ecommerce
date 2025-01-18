using AutoMapper;
using BL.Store;
using Domain.Store;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UI.Ecommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public CartController(ILogger<CartController> logger, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            CartDO cart = new CartDO();
            if (User.Identity.IsAuthenticated)
            {
                UserBL userBL = new UserBL(_mapper);
                int userId = userBL.GetUserByGuid(new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)).Id;
                CartBL cartBL = new CartBL(_mapper);
                cart = cartBL.GetByUserId(userId);
            }
            return View(cart);
        }
        [HttpPost]
        public IActionResult AddCartItem(int productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                UserBL userBL = new UserBL(_mapper);
                int userId = userBL.GetUserByGuid(new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)).Id;
                CartBL cartBL = new CartBL(_mapper);
                cartBL.AddCartItem(productId, userId);
                return RedirectToAction("Details", "Product", new { id = productId });
            }
            else
            {
                return RedirectToAction("SignIn", "Home");
            }

        }
        [HttpPost]
        public IActionResult RemoveCartItem(int cartItemId)
        {
            if (User.Identity.IsAuthenticated)
            {
                UserBL userBL = new UserBL(_mapper);
                int userId = userBL.GetUserByGuid(new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)).Id;
                CartBL cartBL = new CartBL(_mapper);
                int productId = cartBL.GetCartItemById(cartItemId).ProductId;
                cartBL.RemoveCartItem(cartItemId);
                return RedirectToAction("Details", "Product", new { id = productId });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
