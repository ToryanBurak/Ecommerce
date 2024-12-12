using AutoMapper;
using BL.Store;
using Domain.Store;
using Domain.Store.UserLogin;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Mail;
using UI.Ecommerce.Models;

namespace UI.Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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
                //RegisterResponse checkUniqeFieldResponse = userBL.CheckUniqueFieldsForRegister(model);
                //if (checkUniqeFieldResponse.IsSuccessfull)
                //{
                //    Guid guid = Guid.NewGuid();
                //    int imageUrlID = UploadFileAndGetImageUrlID(model.ImageFile, model.UserName, guid);
                //    RegisterResponse userRegisterResponse = userBL.Register(model, imageUrlID, guid);
                //    model.RegisterResponse = userRegisterResponse;
                //    if (!userRegisterResponse.IsSuccessfull)
                //    {
                //        UserDO user = userBL.GetUserByGuid(guid);
                //        string mail = _config.GetValue<string>("SendMailFrom:mail");
                //        string Password = _config.GetValue<string>("SendMailFrom:password");
                //        MimeMessage message = new MimeMessage();
                //        MailboxAddress mailfrom = new MailboxAddress("TOR-INCar Doğrulama Kodu", mail);
                //        MailboxAddress mailto = new MailboxAddress(user.UserName, user.Email);
                //        message.From.Add(mailfrom);
                //        message.To.Add(mailto);
                //        message.Subject = "Doğrulama Kodu";
                //        var Body = new BodyBuilder();
                //        Body.TextBody = "Sayın " + user.UserName + ", Doğrulama Kodunuz:" + user.VerifyConfirmCode;
                //        message.Body = Body.ToMessageBody();
                //        SmtpClient smtp = new SmtpClient();
                //        smtp.Connect("smtp.gmail.com", 587, false);
                //        smtp.Authenticate(mail, Password);
                //        smtp.Send(message);
                //        smtp.Disconnect(true);
                //        return View("Success", "Başarıyla Kayıt Olundu");
                //    }
                //}
            }

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
