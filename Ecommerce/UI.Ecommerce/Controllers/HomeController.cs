using AutoMapper;
using BL.Store;
using Domain.Store;
using Domain.Store.UserLogin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using System.Diagnostics;
using UI.Ecommerce.Models;
using UI.Ecommerce.Extensions;

namespace UI.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(LoginRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserBL userBL = new UserBL(_mapper);
                RegisterResponse checkUniqeFieldResponse = userBL.CheckUniqueFieldsForRegister(model.Register);
                if (checkUniqeFieldResponse.IsSuccessfull)
                {
                    Guid guid = Guid.NewGuid();
                    RegisterResponse userRegisterResponse = userBL.Register(model.Register, guid);
                    model.Register.RegisterResponse = userRegisterResponse;
                    if (!userRegisterResponse.IsSuccessfull)
                    {
                        UserDO user = userBL.GetUserByGuid(guid);
                        string mail = _config.GetValue<string>("SendMailFrom:mail");
                        string Password = _config.GetValue<string>("SendMailFrom:password");
                        MimeMessage message = new MimeMessage();
                        MailboxAddress mailfrom = new MailboxAddress("BurSiMemFur E-Ticaret Doğrulama Kodu", mail);
                        MailboxAddress mailto = new MailboxAddress(user.FirstName + " " + user.LastName, user.Email);
                        message.From.Add(mailfrom);
                        message.To.Add(mailto);
                        message.Subject = "Doğrulama Kodu";
                        var Body = new BodyBuilder();
                        Body.TextBody = "Sayın " + user.FirstName + " " + user.LastName + ", Doğrulama Kodunuz:" + user.VerifyConfirmCode;
                        message.Body = Body.ToMessageBody();
                        SmtpClient smtp = new SmtpClient();
                        smtp.Connect("smtp.gmail.com", 587, false);
                        smtp.Authenticate(mail, Password);
                        smtp.Send(message);
                        smtp.Disconnect(true);
                        return View("Success", "Başarıyla Kayıt Olundu");
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserBL userBL = new UserBL(_mapper);
                model.Login.LoginResponse = userBL.LoginCheck(model.Login);
                UserDO user = userBL.GetUserByID(model.Login.LoginResponse.UserID);
                if (model.Login.LoginResponse.IsSuccess && user != null)
                {
                    IdentityHelper.Login(user, this.HttpContext,_mapper);
                    return RedirectToAction("Index", "Home");
                }
                else { return View(model); }
            }
            else
            {
                return View(model);
            }

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
