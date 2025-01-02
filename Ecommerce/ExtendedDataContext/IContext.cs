using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExtendedDataContext.Enumerations;

namespace ExtendedDataContext
{
    public interface IContext<Context>
    {
        Context GetDataContext();
        void DestroyContext(bool? disposing = null);
        CommitDBResult CommitChanges();
    }
}
