using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class Town
    {
        public Town()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Key { get; set; }
        public int DistrictKey { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
