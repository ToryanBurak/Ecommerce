using Domain.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class AddressDO
    {
        public AddressDO()
        {
            Orders = new HashSet<OrderDO>();
        }

        public int Id { get; set; }
        public string AddressDescription { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int UserId { get; set; }

        public virtual UserDO User { get; set; } = null!;
        public virtual ICollection<OrderDO> Orders { get; set; }
    }
}
