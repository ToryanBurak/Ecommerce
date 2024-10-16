using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedDataContext.EFContext
{
    public interface IEFContextProvider
    {
        public interface IEFContextProvider : IContext<DbContext>
        {

        }
    }
}
