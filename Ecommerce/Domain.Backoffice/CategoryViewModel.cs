using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backoffice
{
    public class CategoryViewModel
    {
        public CategoryDO Category { get; set; }
        public IFormFile? Image { get; set; }
    }
}
