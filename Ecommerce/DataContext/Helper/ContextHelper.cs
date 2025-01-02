using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using static ExtendedDataContext.Enumerations;

namespace DataContext.Helper
{
    public class HistoryHelper
    {
        public static CommitDBResult CommitChanges(DbContext _dataContext)
        {
            CommitDBResult commitDBResult = CommitDBResult.Success;
            _dataContext.SaveChanges();

            return commitDBResult;
        }

    }
}
