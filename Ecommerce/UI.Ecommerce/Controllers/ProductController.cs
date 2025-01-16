using AutoMapper;
using BL.Store;
using Microsoft.AspNetCore.Mvc;

namespace UI.Ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _config;
        public ProductController(ILogger<ProductController> logger, IMapper mapper, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _logger = logger;
            _mapper = mapper;
            _config = config;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult List()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            if (id == 0 || id == null)
            {
                string refererUrl = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(refererUrl))
                {
                    return Redirect(refererUrl);
                }
            }
            ProductBL productBL = new ProductBL(_mapper);
            return View(productBL.GetById(id));
        }
    }
}
