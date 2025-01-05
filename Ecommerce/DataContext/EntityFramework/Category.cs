using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ImageUrlId { get; set; }

        public virtual ImageUrl ImageUrl { get; set; } = null!;
    }
}
