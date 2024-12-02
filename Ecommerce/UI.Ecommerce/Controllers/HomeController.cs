using AutoMapper;
using BL.Store;
using Domain.Store;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using UI.Ecommerce.Models;

namespace UI.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
            int a = 5;
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult UserLogin()
        {
            UserDO userDO = new UserDO()
            {
                FirstName = "Burak",
                LastName = "Toryan"
            };

            return View(userDO);
        }

        [HttpPost]
        public IActionResult UserLogin(int model)
        {
            //if (ModelState.IsValid)
            //{
            //    UserBL userBL = new UserBL(_mapper);
            //    model.LoginResponse = userBL.LoginCheck(model);
            //    UserDO user = userBL.GetUserByID(model.LoginResponse.UserID);
            //    if (model.LoginResponse.IsSuccess && user != null)
            //    {
            //        IdentityHelper.Login(user, this.HttpContext);
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else { return View(model); }
            //}
           /* else {*/ return View(model); /*}*/

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
