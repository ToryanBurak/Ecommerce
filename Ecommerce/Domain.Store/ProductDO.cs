using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class ProductDO
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Amount { get; set; } = null!;
        public int CategoryId { get; set; }
        public int ImageUrlId { get; set; }
        public bool? IsActive { get; set; }


        public virtual ImageUrlDO ImageUrl { get; set; } = null!;

    }
}
