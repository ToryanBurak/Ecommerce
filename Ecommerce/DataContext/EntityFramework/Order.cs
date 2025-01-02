using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public decimal Amount { get; set; }
        public int State { get; set; }
    }
}
