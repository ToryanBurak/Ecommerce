using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Amount { get; set; } = null!;
        public int CategoryId { get; set; }
        public int ImageUrlId { get; set; }

        public virtual ImageUrl ImageUrl { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
