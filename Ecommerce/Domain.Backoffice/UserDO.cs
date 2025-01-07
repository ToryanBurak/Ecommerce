using DataContext.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backoffice
{
    public class UserDO
    {
        public UserDO()
        {
            Addresses = new HashSet<AddressDO>();
            Orders = new HashSet<OrderDO>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int VerifyState { get; set; }
        public string Phone { get; set; } = null!;
        public string VerifyConfirmCode { get; set; } = null!;
        public string SaltString { get; set; } = null!;
        public Guid Guid { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<AddressDO> Addresses { get; set; }
        public virtual ICollection<OrderDO> Orders { get; set; }
    }
}
