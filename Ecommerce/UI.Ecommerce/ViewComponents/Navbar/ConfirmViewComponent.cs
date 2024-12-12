using AutoMapper;
using BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace UI2.ViewComponents.Navbar
{
    public class ConfirmViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        public ConfirmViewComponent(IMapper mapper)
        {
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return null;
        }
    }
}
