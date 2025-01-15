using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class OrderDO
    {
        public OrderDO()
        {
            OrderItems = new HashSet<OrderItemDO>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public decimal Amount { get; set; }
        public int State { get; set; }

        public virtual AddressDO Address { get; set; } = null!;
        public virtual UserDO User { get; set; } = null!;
        public virtual ICollection<OrderItemDO> OrderItems { get; set; }
    }
}
