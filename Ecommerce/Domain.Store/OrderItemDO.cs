using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store    
{
    public class OrderItemDO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }

        public virtual OrderDO Order { get; set; } = null!;
        public virtual ProductDO Product { get; set; } = null!;
    }
}
