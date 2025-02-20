using AutoMapper;
using BL.Store;
using Domain.Store;
using Domain.Store.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UI.Ecommerce.Extensions;

namespace UI.Ecommerce.Controllers
{
    public class UserController : Controller
    {

        private readonly IMapper _mapper;

        public UserController(IMapper mapper)
        {
            this._mapper = mapper;
        }
        [Authorize]
        public IActionResult ConfirmMail()
        {
            return View();
        }
        [Authorize]
        public IActionResult MyAccount()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public IActionResult VerifyConfirmCode()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult VerifyConfirmCode(string confirmcode)
        {
            UserBL userBL = new UserBL(_mapper);
            UserDO user = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user.VerifyConfirmCode == confirmcode)
            {
                userBL.UpdateUserVerifyState(user.Id, VerifyStateEnum.Verified);
                IdentityHelper.UpdateUserVerifyState(this.HttpContext);
                return View("Success", "Doğrulama Başarılı");
            }
            else
            {
                return View("VerifyConfirmCode", "Doğrulama Başarısız.Kodu Tekrar Kontrol Edin");
            }

        }
        //[HttpGet]
        //public IActionResult ForgetPasswordWithMail()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[Authorize]
        //public IActionResult ForgetPasswordWithMail(ForgetPasswordViewModel forgetPasswordModel)
        //{
        //    if (ModelState.IsValid && !String.IsNullOrWhiteSpace(forgetPasswordModel.Email))
        //    {
        //        UserBL userBL = new UserBL(_mapper);
        //        UserDO user = userBL.GetUserByMail(forgetPasswordModel);
        //        if (user != null)
        //        {
        //            return RedirectToAction("UpdatePasswordConfirmWithMail", user.Guid);
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Lütfen Email Giriniz");

        //    }
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult UpdatePasswordConfirmWithMail(Guid guid)
        //{
        //    Random rnd = new Random();
        //    UserBL userBL = new UserBL(_mapper);
        //    UserDO user = userBL.GetUserByGuid(guid);
        //    string confirmCode = rnd.Next(10000, 99999).ToString();
        //    TempData["UpdatePasswordConfirmCode"] = confirmCode;
        //    TempData["UserGuid"] = guid.ToString();
        //    string mail = _config.GetValue<string>("SendMailFrom:mail");
        //    string Password = _config.GetValue<string>("SendMailFrom:password");
        //    MimeMessage message = new MimeMessage();
        //    MailboxAddress mailfrom = new MailboxAddress("Bursimemfur", mail);
        //    MailboxAddress mailto = new MailboxAddress(user.UserName, user.Email);
        //    message.From.Add(mailfrom);
        //    message.To.Add(mailto);
        //    message.Subject = "Şifre Güncelleme Doğrulama Kodu";
        //    var Body = new BodyBuilder();
        //    Body.TextBody = confirmCode.ToString();
        //    message.Body = Body.ToMessageBody();
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Connect("smtp.gmail.com", 587, false);
        //    smtp.Authenticate(mail, Password);
        //    smtp.Send(message);
        //    smtp.Disconnect(true);
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult UpdatePasswordConfirmWithMail(string confirmCode)
        //{
        //    if (!String.IsNullOrWhiteSpace(confirmCode))
        //    {
        //        if (confirmCode == TempData["UpdatePasswordConfirmCode"])
        //        {
        //            return RedirectToAction("UpdatePassword");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Doğrulama kodunu yanlış girdiniz.Kontrol Ediniz.");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Lütfen doğrulama kodu giriniz.");
        //    }
        //    return View();
        //}
        //[HttpGet]
        //public IActionResult UpdatePassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult UpdatePassword(UpdatePasswordViewModel updatePasswordViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        UserBL userBL = new UserBL(_mapper);
        //        UserDO user = userBL.GetUserByGuid(new Guid(TempData["UserGuid"].ToString()));
        //        if (user != null)
        //        {
        //            if (user.Password == HashMD5(updatePasswordViewModel.OldPassword, user.SaltString))
        //            {
        //                if (updatePasswordViewModel.NewPassword == updatePasswordViewModel.VerifyPassword)
        //                {
        //                    bool action = userBL.UpdatePassword(updatePasswordViewModel.NewPassword, user.Guid);
        //                    if (action)
        //                    {
        //                        return RedirectToAction("Success");
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Girdiğiniz Bilgiler Uyuşmuyor");
        //    }

        //    return View();
        //}
        //private string HashMD5(string value, string saltString)
        //{
        //    MD5 md5 = MD5.Create();
        //    return Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes(value + saltString)));
        //}
        [Authorize]
        public IActionResult LogOut()
        {
            IdentityHelper.Logout(this.HttpContext);
            return RedirectToAction("Index", "Home");
        }
    } 
}
