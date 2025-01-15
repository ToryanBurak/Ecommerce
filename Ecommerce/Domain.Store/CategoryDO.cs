using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class CategoryDO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ImageUrlId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ImageUrlDO ImageUrl { get; set; } = null!;
        public virtual ICollection<ProductDO> Products { get; set; }
    }
}
