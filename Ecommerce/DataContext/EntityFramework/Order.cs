using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public decimal Amount { get; set; }
        public int State { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
