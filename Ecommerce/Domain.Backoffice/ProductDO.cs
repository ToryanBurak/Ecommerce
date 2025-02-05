using DataContext.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backoffice
{
    public class ProductDO
    {
        public ProductDO()
        {
            OrderItems = new HashSet<OrderItemDO>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int ImageUrlId { get; set; }
        public bool IsActive { get; set; }

        public virtual ImageUrlDO ImageUrl { get; set; } = null!;
        public virtual ICollection<OrderItemDO> OrderItems { get; set; }
    }
}
