using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Street
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int TownKey { get; set; }
    }
}
