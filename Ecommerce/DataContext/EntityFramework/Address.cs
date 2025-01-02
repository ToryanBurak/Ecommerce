using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Address
    {
        public int Id { get; set; }
        public string AddressDescription { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public int UserId { get; set; }
    }
}
