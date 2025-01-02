using AutoMapper;
using BL.Backoffice;
using Domain.Backoffice;
using Domain.Backoffice.UserLogin;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.BackOffice.Extensions;
using UI.BackOffice.Models;

namespace UI.BackOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            ImageUrlBL imageUrlBL = new ImageUrlBL(_mapper);
            ImageUrlDO imageUrl = imageUrlBL.GetFirst();
            return View(imageUrl);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserBL userBL = new UserBL(_mapper);
                model.LoginResponse = userBL.LoginCheck(model);
                UserDO user = userBL.GetUserByID(model.LoginResponse.UserID);
                if (model.LoginResponse.IsSuccess && user != null)
                {
                    IdentityHelper.Login(user, this.HttpContext);
                    return RedirectToAction("Index", "Home");
                }
                else { return View(model); }
            }
            else
            {
                return View(model);
            }

        }

    }
}
