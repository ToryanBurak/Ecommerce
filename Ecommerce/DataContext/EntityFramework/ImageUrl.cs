using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class ImageUrl
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
    }
}
