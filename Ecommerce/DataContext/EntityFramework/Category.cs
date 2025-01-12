using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ImageUrlId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ImageUrl ImageUrl { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
