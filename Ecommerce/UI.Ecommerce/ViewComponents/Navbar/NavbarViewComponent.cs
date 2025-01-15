using AutoMapper;
using BL;
using BL.Store;
using DataContext.EntityFramework;
using Domain.Store;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UI2.ViewComponents.Navbar
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
            CategoryBL categoryBL = new CategoryBL(_mapper);
            List<CategoryDO> categoryList = categoryBL.GetAll();
            UserDO user = new UserDO();
            UserBL userBL = new UserBL(_mapper);
            CartBL cartBL = new CartBL(_mapper);
            CartDO cart = new CartDO();
            if (User.Identity.IsAuthenticated)
            {
                user = userBL.GetUserByGuid(new Guid(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
                cart = cartBL.GetByUserId(user.Id);
            }
            IndexViewModel vm = new IndexViewModel()
            {
                CategoryList = categoryList,
                User = user,
                Cart = cart
            };
            return View(vm);
        }
    }
}
