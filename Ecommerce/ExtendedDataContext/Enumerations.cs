using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedDataContext
{
    public class Enumerations
    {
        public enum CommitDBResult
        {
            Success = 0,
            Fail = 1,
        }

        public enum CommitActionType
        {
            Insert = 0,
            Delete = 1,
            Update = 2
        }
    }
}
