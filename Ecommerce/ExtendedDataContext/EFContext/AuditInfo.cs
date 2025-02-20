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

        public AuditInfo(DbContext dataContext)
        {
            this.dataContext = dataContext;
        }
    }
}
