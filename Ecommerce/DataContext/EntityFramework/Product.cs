using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Amount { get; set; } = null!;
        public int CategoryId { get; set; }
        public int ImageUrlId { get; set; }
    }
}
