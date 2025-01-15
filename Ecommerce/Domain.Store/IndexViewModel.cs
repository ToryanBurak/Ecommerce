using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            CategoryList = new List<CategoryDO>();
        }
        public UserDO User { get; set; }

        public List<CategoryDO> CategoryList { get; set; }
    }
}
