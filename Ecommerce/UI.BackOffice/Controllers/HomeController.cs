using AutoMapper;
using BL.Backoffice;
using Domain.Backoffice;
using Domain.Backoffice.Enum;
using Domain.Backoffice.UserLogin;
using FluentFTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
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
            int a = 5;
             if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                LoginViewModel loginViewModel = new LoginViewModel();
                return View("Login",loginViewModel);
            }
           
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
        [Authorize]
        public IActionResult LogOut()
        {
            IdentityHelper.Logout(this.HttpContext);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult CustomerList()
        {
            UserBL userBL = new UserBL(_mapper);
            List<UserDO> userList = userBL.GetAll();
            return View(userList);
        }
        [Authorize]
        public IActionResult CategoryList()
        {
            CategoryBL categoryBL = new CategoryBL(_mapper);
            List<CategoryDO> categoryList = categoryBL.GetAll();
            foreach (CategoryDO category in categoryList)
            {
                ImageUrlBL imageUrlBL = new ImageUrlBL(_mapper);
                category.Url = imageUrlBL.GetImageUrlById(category.ImageUrlId);
            }
            return View(categoryList);
        }
        [Authorize]
        public IActionResult AddCategory()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddCategory(CategoryViewModel categoryViewModel)
        {
            CategoryBL categoryBL = new CategoryBL(_mapper);
            int imageUrlId = UploadImageAndImageUrlId(categoryViewModel.Image, FtpDirectoryEnum.Category);
            categoryViewModel.Category.ImageUrlId = imageUrlId;
            categoryBL.AddCategory(categoryViewModel.Category);
            return RedirectToAction("CategoryList");
        }
        [Authorize]
        public int UploadImageAndImageUrlId(IFormFile file,FtpDirectoryEnum ftpDirectoryEnum)
        {
            if (file != null && file.Length > 0)
            {
                try
                {
                    // Geçici bir dosya oluşturun
                    var tempFilePath = Path.Combine(Path.GetTempPath(), file.FileName);
                    using (var stream = new FileStream(tempFilePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    }

                    // FTP sunucusuna yükleyin
                    string imageUrl = UploadFileToFtp(tempFilePath, ftpDirectoryEnum);

                    // Geçici dosyayı silin
                    System.IO.File.Delete(tempFilePath);

                    ViewBag.Message = "Dosya başarıyla yüklendi!";
                    ImageUrlBL imageUrlBL = new ImageUrlBL(_mapper);
                    int imageUrlId = imageUrlBL.UploadImageAndGetId(imageUrl);
                    return imageUrlId;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Hata: {ex.Message}";
                }
            }
            else
            {
                ViewBag.Message = "Lütfen geçerli bir dosya seçin.";
            }
            return 0;
            
        }

        static string UploadFileToFtp(string localFilePath, FtpDirectoryEnum ftpDirectoryEnum)
        {
            string ftpUrl = $"ftp://185.169.180.42/{ftpDirectoryEnum}/{Path.GetFileName(localFilePath)}";

            // FTP request oluştur
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential("ftpuser", "26052001.Burak");
            request.EnableSsl = false; // TLS kullanılmıyorsa bunu false olarak bırakın
            request.UseBinary = true;
            request.UsePassive = false;

            try
            {
                // Dosyayı yükle
                byte[] fileContents;
                using (FileStream fileStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                {
                    fileContents = new byte[fileStream.Length];
                    fileStream.Read(fileContents, 0, fileContents.Length);
                }

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }
            }
            catch (Exception ex)
            {
            }
            return ftpUrl.Replace("ftp", "https");
        }

    }
}
