using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class ImageUrl
    {
        public ImageUrl()
        {
            Categories = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Url { get; set; } = null!;

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
