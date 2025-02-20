using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backoffice
{
    public class ImageUploadViewModel
    {
        public IFormFile FormFile { get; set; }
    }
}
