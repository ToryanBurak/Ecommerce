using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backoffice
{
    public class CategoryDO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ImageUrlId { get; set; }
        public string? Url { get; set; }
    }
}
