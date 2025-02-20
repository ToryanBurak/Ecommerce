using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class CityDO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Key { get; set; }
    }
}
