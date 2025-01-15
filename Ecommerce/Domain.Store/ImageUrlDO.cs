using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class ImageUrlDO
    {
        public ImageUrlDO()
        {
            Categories = new HashSet<CategoryDO>();
            Products = new HashSet<ProductDO>();
        }

        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public Guid? Guid { get; set; }

        public virtual ICollection<CategoryDO> Categories { get; set; }
        public virtual ICollection<ProductDO> Products { get; set; }
    }
}
