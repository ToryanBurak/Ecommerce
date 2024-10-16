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
        //private EcomHistoryDataContext _historyDataContext;
        //private static readonly string connectionStringName = "EcomDataContext";
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

        public EcomDbContext GetEcomDataContext()
        {
            return (EcomDbContext)GetDataContext();
        }

        public EcomDbContext GetEcomFeedDataContext()
        {
            return (EcomDbContext)GetFeedDataContext();
        }

        public DbContext GetDataContext()
        {
            // TODO: Connection string icin encryption yapılacak...
            if (_dataContext == null)
                _dataContext = new EcomDbContext();
            return _dataContext;
        }

        public DbContext GetFeedDataContext()
        {
            // TODO: Connection string icin encryption yapılacak...
            if (_dataContext == null)
                _dataContext = new EcomDbContext();

            return _dataContext;
        }

        //public DbContext GetHistoryDataContext()
        //{
        //    // TODO: Connection string icin encryption yapılacak...
        //    if (_historyDataContext == null)
        //        _historyDataContext = new EcomHistoryDataContext(ConfigurationManager.ConnectionStrings[connectionStringName].ToString());

        //    return _historyDataContext;
        //}

        //look HistoryDataContext tabloları mySql de schema olmamasından kaynaklı EcomDataContext ine dönüştürüldü.
        public DbContext GetHistoryDataContext()
        {
            // TODO: Connection string icin encryption yapılacak...
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

        public CommitDBResult CommitChanges(int UserID)
        {
            //Look
            //return DBContextHelper.CommitChanges(this.GetDataContext(), this.GetHistoryDataContext(), UserID);
            HistoryHelper.CommitChanges(_dataContext, UserID);
            //SaveChanges(this.GetHistoryDataContext());
            //setUpdateColumns(UserID);
            //_dataContext.SaveChanges();
            CommitDBResult commitDBResult = CommitDBResult.Success;

            // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
            // sadece değişen alanları güncellemeyide sağlayabiliriz.
            return commitDBResult;
        }
        public void setUpdateColumns(int userId)
        {
            var changes = _dataContext.ChangeTracker.Entries();
            var updateList = changes.Where(s => s.State == EntityState.Modified);
            var insertList = changes.Where(s => s.State == EntityState.Added);
            var dt = DateTime.UtcNow;
            foreach (object item in updateList)
            {
                setIntProperty(item, UpdateUserID, userId, _dataContext);
                setDateTimeProperty(item, UpdateTime, DateTime.UtcNow, _dataContext, false);
            }
            foreach (object item in insertList)
            {
                setIntProperty(item, UpdateUserID, userId, _dataContext);
                setDateTimeProperty(item, UpdateTime, DateTime.UtcNow, _dataContext, true);
                setIntProperty(item, CreateUserID, userId, _dataContext);
                setDateTimeProperty(item, CreateTime, DateTime.UtcNow, _dataContext, true);
            }
        }
        private void setDateTimeProperty(object tableName, string propertyName, DateTime propertyValue, DbContext dbContext, bool nullCheck = false)
        {
            dbContext.ChangeTracker.DetectChanges();
            var haveProperty = (dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName).CurrentValues.Properties.Any(s => s.Name == propertyName));

            if (haveProperty)
            {
                if (nullCheck == true)
                {
                    dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName).CurrentValues[propertyName] = propertyValue;
                }
                else
                {
                    dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName).CurrentValues[propertyName] = propertyValue;
                }
            }
        }
        private void setIntProperty(object tableName, string propertyName, int propertyValue, DbContext dbContext)
        {
            var haveProperty = (dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName).CurrentValues.Properties.Any(s => s.Name == propertyName));
            if (haveProperty)
            {
                dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName).CurrentValues[propertyName] = propertyValue;
            }
        }
        public CommitDBResult CommitChangesWithoutHistory(int UserID)
        {

            try
            {
                setUpdateColumns(UserID);
                _dataContext.SaveChanges();
                CommitDBResult commitDBResult = CommitDBResult.Success;

                // Transaction işlemleri burada ele alınabilir veya Identity Map kurumsal tasarım kalıbı kullanılarak
                // sadece değişen alanları güncellemeyide sağlayabiliriz.
                return commitDBResult;

            }
            catch
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                CommitDBResult commitDBResult = CommitDBResult.Fail;
                return commitDBResult;
            }
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

        DbContext IContext<DbContext>.GetHistoryDataContext()
        {
            throw new NotImplementedException();
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
