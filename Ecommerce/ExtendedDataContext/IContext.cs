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
        Context GetHistoryDataContext();
        void DestroyContext(bool? disposing = null);
        CommitDBResult CommitChanges(int userId);
        CommitDBResult CommitChangesWithoutHistory(int userId);
    }
}
