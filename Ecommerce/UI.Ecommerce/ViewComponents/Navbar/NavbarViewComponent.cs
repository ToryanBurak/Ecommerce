using AutoMapper;
using BL;
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
            return View();
        }
    }
}
