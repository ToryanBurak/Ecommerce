using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedDataContext.EFContext
{
    public class AuditInfo
    {
        #region Fields

        public static string[] Fields
        {
            get
            {
                string[] fieldArr = new string[6]{
                    CreateUserID,
                    CreateTime,
                    UpdateUserID,
                    UpdateTime,
                    ActionUserID,
                    ActionTime
                };

                return fieldArr;
            }
        }

        public static string CreateUserID = "CreateUserID";
        public static string CreateTime = "CreateTime";
        public static string UpdateUserID = "UpdateUserID";
        public static string UpdateTime = "UpdateTime";
        public static string ActionTime = "ActionTime";
        public static string ActionUserID = "ActionUserID";
        public static string Action = "Action";
        public DbContext dataContext;
        public int userId;

        #endregion Fields

        public AuditInfo(DbContext dataContext, int userId)
        {
            this.dataContext = dataContext;
            this.userId = userId;
        }

        private void setDateTimeProperty(object tableName, string propertyName, DateTime propertyValue, DbContext dbContext, bool nullCheck = false)
        {
            dbContext.ChangeTracker.DetectChanges();
            EntityEntry? property = dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName);
            if (property != null)
            {
                bool haveProperty = property.CurrentValues.Properties.Any(s => s.Name == propertyName);
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

        }
        private void setIntProperty(object tableName, string propertyName, int propertyValue, DbContext dbContext)
        {
            EntityEntry? property = dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName);
            if (property != null)
            {
                bool haveProperty = property.CurrentValues.Properties.Any(s => s.Name == propertyName);
                if (haveProperty)
                {
                    dbContext.ChangeTracker.Entries().FirstOrDefault(x => x.Entity.GetType().Name == tableName).CurrentValues[propertyName] = propertyValue;
                }
            }


        }

        public void setUpdateColumns()
        {
            var changes = dataContext.ChangeTracker.Entries();

            foreach (object item in changes.Where(p => p.State == EntityState.Modified))
            {
                setIntProperty(item, UpdateUserID, userId, dataContext);
                setDateTimeProperty(item, UpdateTime, DateTime.UtcNow, dataContext, false);
            }
            foreach (object item in changes.Where(p => p.State == EntityState.Added))
            {
                setIntProperty(item, UpdateUserID, userId, dataContext);
                setDateTimeProperty(item, UpdateTime, DateTime.UtcNow, dataContext, true);
                setIntProperty(item, CreateUserID, userId, dataContext);
                setDateTimeProperty(item, CreateTime, DateTime.UtcNow, dataContext, true);
            }
        }
    }
}
