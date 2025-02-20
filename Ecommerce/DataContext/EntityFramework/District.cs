using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class District
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Key { get; set; }
        public int CityKey { get; set; }
    }
}
