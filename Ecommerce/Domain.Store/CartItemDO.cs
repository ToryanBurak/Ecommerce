using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class CartItemDO
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }

        public virtual CartDO Cart { get; set; } = null!;
        public virtual ProductDO Product { get; set; } = null!;
    }
}
