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
        [HttpGet]
        public IActionResult VerifyConfirmCode()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult VerifyConfirmCode(string code)
        {
            UserBL userBL = new UserBL(_mapper);
            UserDO user = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (user.VerifyConfirmCode == code)
            {
                userBL.UpdateUserVerifyState(user.Id, VerifyStateEnum.Verified);
                IdentityHelper.Login(user, this.HttpContext);
                return View("Success", "Doğrulama Başarılı");
            }
            else
            {
                return View("Error", "Doğrulama Başarısız.Kodu Tekrar Kontrol Edin");
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
        //[Authorize]
        //public IActionResult LogOut()
        //{
        //    IdentityHelper.Logout(this.HttpContext);
        //    return RedirectToAction("Index", "Home");
        //}

        //public IActionResult Wallet()
        //{
        //    UserBL userBL = new UserBL(_mapper);
        //    WalletBL walletBL = new WalletBL(_mapper);
        //    WalletHistoryBL walletHistoryBL = new WalletHistoryBL(_mapper);
        //    UserDO user = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //    WalletDO wallet = walletBL.GetByID(user.WalletId);
        //    wallet.WalletHistoryList = walletHistoryBL.GetAllByWalletID(wallet.Id);
        //    return View(wallet);
        //}
        //[HttpGet]
        //public IActionResult DepositMoney()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult DepositMoney(WalletDO wallet)
        //{
        //    UserBL userBL = new UserBL(_mapper);
        //    WalletBL walletBL = new WalletBL(_mapper);
        //    WalletHistoryBL walletHistoryBL = new WalletHistoryBL(_mapper);
        //    int walletId = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))).WalletId;
        //    walletBL.UpdateWallet(walletId, RentACar.Global.Enums.WalletProcessTypeEnum.Deposit, wallet.Amount);
        //    walletHistoryBL.AddWalletHistory(walletId, RentACar.Global.Enums.WalletProcessTypeEnum.Deposit, wallet.Amount);
        //    return View("Success", String.Format("Bakiyeniz Yüklendi.Bakiyeniz: {0} TL", Convert.ToInt32(walletBL.GetByID(walletId).Amount)));
        //}
        //[HttpGet]
        //public IActionResult WithdrawMoney()
        //{
        //    UserBL userBL = new UserBL(_mapper);
        //    WalletBL walletBL = new WalletBL(_mapper);
        //    int walletId = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))).WalletId;
        //    TempData["WalletAmount"] = walletBL.GetByID(walletId).Amount.ToString();
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult WithdrawMoney(WalletDO wallet)
        //{
        //    UserBL userBL = new UserBL(_mapper);
        //    WalletBL walletBL = new WalletBL(_mapper);
        //    WalletHistoryBL walletHistoryBL = new WalletHistoryBL(_mapper);
        //    int walletId = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier))).WalletId;
        //    decimal amount = Convert.ToDecimal(TempData["WalletAmount"].ToString());
        //    if (amount < wallet.Amount)
        //    {
        //        return View("Error", "Yetersiz Bakiye.Lütfen Bakiyenizi Kontrol Edin!!");
        //    }
        //    WalletDO walletDO = walletBL.GetByID(walletId);
        //    walletBL.UpdateWallet(walletId, RentACar.Global.Enums.WalletProcessTypeEnum.Withdraw, wallet.Amount);
        //    walletHistoryBL.AddWalletHistory(walletId, RentACar.Global.Enums.WalletProcessTypeEnum.Withdraw, wallet.Amount);
        //    return View("Success", String.Format("Bakiyenizden Para Çekildi.Bakiyeniz: {0} TL", Convert.ToInt32(walletBL.GetByID(walletId).Amount)));
        //}

        //[HttpGet]
        //public IActionResult RentCarPage(int advertId)
        //{
        //    RentCarViewModel rentCarViewModel = new RentCarViewModel();
        //    CarBL carBL = new CarBL(_mapper);
        //    ImageUrlBL imageUrlBL = new ImageUrlBL(_mapper);
        //    UserBL userBL = new UserBL(_mapper);
        //    WalletBL walletBL = new WalletBL(_mapper);
        //    RentBL rentBL = new RentBL(_mapper);
        //    rentCarViewModel.Advert = AdvertBL.GetById(advertId);
        //    rentCarViewModel.Advert.Car = carBL.GetById(rentCarViewModel.Advert.CarID);
        //    rentCarViewModel.Advert.Car.ImageUrl = imageUrlBL.GetImageUrlByID(rentCarViewModel.Advert.Car.ImageUrlId);
        //    rentCarViewModel.User = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //    rentCarViewModel.User.Wallet = walletBL.GetByID(rentCarViewModel.User.WalletId);
        //    rentCarViewModel.RentListForTheCar = rentBL.GetRentListByAdvertId(rentCarViewModel.Advert.ID);
        //    return View(rentCarViewModel);
        //}
        //[HttpPost]
        //public IActionResult Rent(RentCarViewModel request, int advertId)
        //{
        //    try
        //    {
        //        RentBL rentBL = new RentBL(_mapper);
        //        UserBL userBL = new UserBL(_mapper);
        //        UserDO user = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //        AvailabilityRequest availabilityRequest = request.AvailabilityRequest;
        //        var pickUpDate = DateTime.Parse(availabilityRequest.PickUpDate);
        //        var pickupTime = TimeOnly.Parse(availabilityRequest.PickupTime);
        //        var returnDate = DateTime.Parse(availabilityRequest.ReturnDate);
        //        var returnTime = TimeOnly.Parse(availabilityRequest.ReturnTime);
        //        var pickUpDateTime = pickUpDate.Date.Add(pickupTime.ToTimeSpan());
        //        var returnDateTime = returnDate.Date.Add(returnTime.ToTimeSpan());

        //        if (rentBL.CheckAvailableRent(pickUpDateTime, returnDateTime, advertId))
        //        {
        //            rentBL.RentACar(advertId, user.ID, request.AvailabilityRequest);
        //        }
        //        return View("Success", "Kiralama Tamamlandı.Kiralamayı Kiralamalarım Bölümünden Ulaşabilirsiniz.Kira Onay Bildiriminiz Mail Üzerinden Atılacaktır.");
        //    }
        //    catch (Exception)
        //    {
        //        return View("Error", "Bir Sorun Oluştu.Lütfen Daha Sonra Tekrar Deneyiniz.");
        //    }


        //}

        //public IActionResult SuggestionList()
        //{
        //    UserBL userBL = new UserBL(_mapper);
        //    SuggestionBL suggestionBL = new SuggestionBL(_mapper);
        //    UserDO userDO = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //    List<SuggestionDO> suggestionList = suggestionBL.GetAllByUserId(userDO.ID);
        //    return View(suggestionList);
        //}
        //public IActionResult AddSuggestion()
        //{
        //    UserBL userBL = new UserBL(_mapper);
        //    UserDO userDO = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //    SuggestionDO suggestion = new SuggestionDO()
        //    {
        //        UserId = userDO.ID
        //    };
        //    return View(suggestion);
        //}
        //[HttpPost]
        //public IActionResult AddSuggestion(SuggestionDO suggestion)
        //{
        //    try
        //    {
        //        if (!String.IsNullOrWhiteSpace(suggestion.Message))
        //        {
        //            UserBL userBL = new UserBL(_mapper);
        //            SuggestionBL suggestionBL = new SuggestionBL(_mapper);
        //            UserDO userDO = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //            suggestion.UserId = userDO.ID;
        //            suggestion.SendMessageTime = DateTime.UtcNow;
        //            suggestionBL.Send(suggestion);
        //            return View("Success", "İlginiz için teşekkür ederiz.En kısa zamanda size dönüş sağlayacağız.");
        //        }
        //        else
        //        {
        //            return View("Error", "Lütfen Mesajı Boş Bırakmayın.");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return View("Error", "Bir Hata oluştu.Lütfen Daha Sonra Tekrar Deneyiniz.");
        //    }
        //}
        //public IActionResult SuggestionDetail(int id)
        //{
        //    UserBL userBL = new UserBL(_mapper);
        //    ImageUrlBL imageUrlBL = new ImageUrlBL(_mapper);
        //    SuggestionBL suggestionBL = new SuggestionBL(_mapper);
        //    UserDO userDO = userBL.GetUserByGuid(new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        //    SuggestionDO suggestion = suggestionBL.GetById(id);
        //    suggestion.User = userBL.GetUserByID(suggestion.UserId);
        //    suggestion.User.ImageUrl = imageUrlBL.GetImageUrlByID(suggestion.User.ImageUrlID);
        //    return View(suggestion);
        //}
    } 
}
