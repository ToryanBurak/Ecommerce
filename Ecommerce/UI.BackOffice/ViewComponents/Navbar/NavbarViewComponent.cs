using AutoMapper;
using BL.Backoffice;
using DataContext.EntityFramework;
using Domain.Backoffice;
using Domain.Backoffice.UserLogin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UI.ViewComponents.Navbar
{
    [ViewComponent]
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        public NavbarViewComponent(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserDO user = new UserDO();
            LoginViewModel loginViewModel = new LoginViewModel();
            UserBL userBL = new UserBL(_mapper);
            if (User.Identity.IsAuthenticated)
            {
                user = userBL.GetUserByGuid(new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
                return View(user);
            }
            else
            {
                return View("Login");
            }
            
        }
    }
}
