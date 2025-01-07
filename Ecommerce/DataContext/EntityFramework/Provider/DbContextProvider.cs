using DataContext.Helper;
using ExtendedDataContext;
using ExtendedDataContext.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExtendedDataContext.Enumerations;

namespace DataContext.EntityFramework.Provider
{
    public class DbContextProvider : IEFContextProvider, IDisposable
    {
        private EcomDbContext _dataContext;
        private bool disposed = false;

        public static string CreateUserID = "CreateUserID";
        public static string CreateTime = "CreateTime";
        public static string UpdateUserID = "UpdateUserID";
        public static string UpdateTime = "UpdateTime";

        public static DbContextProvider Instance
        {
            get
            {
                return new DbContextProvider();
            }
        }

        public DbContext GetDataContext()
        {
            if (_dataContext == null)
                _dataContext = new EcomDbContext();
            return _dataContext;
        }

        public DbContext GetFeedDataContext()
        {
            if (_dataContext == null)
                _dataContext = new EcomDbContext();

            return _dataContext;
        }


        public void DestroyContext(bool? disposing = null)
        {
            if (_dataContext != null)
            {
                if (!disposed)
                {
                    if (disposing != null && disposing == true)
                    {
                        _dataContext.Dispose();
                    }
                }
                disposed = true;
            }
        }

        public CommitDBResult CommitChanges()
        {
            HistoryHelper.CommitChanges(_dataContext);
            CommitDBResult commitDBResult = CommitDBResult.Success;
            return commitDBResult;
        }

        public void Dispose()
        {
            DestroyContext(true);
            GC.SuppressFinalize(this);
        }

        public void DiscardPendingChanges()
        {
            _dataContext.Dispose();
            _dataContext = new EcomDbContext();
        }

        public int SaveChanges(DbContext _dataContext)
        {
            return 1;
        }

        public void RejectChanges()
        {
            foreach (var entry in _dataContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}
