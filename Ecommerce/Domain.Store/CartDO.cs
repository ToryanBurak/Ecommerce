using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class CartDO
    {
        public CartDO()
        {
            CartItems = new HashSet<CartItemDO>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual UserDO User { get; set; } = null!;
        public virtual ICollection<CartItemDO> CartItems { get; set; }
    }
}
