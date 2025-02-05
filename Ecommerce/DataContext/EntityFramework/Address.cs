using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Address
    {
        public Address()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string AddressDescription { get; set; } = null!;
        public string Value { get; set; } = null!;
        public int UserId { get; set; }
        public int TownId { get; set; }

        public virtual Town Town { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
