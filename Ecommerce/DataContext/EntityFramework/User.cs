using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class User
    {
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
    }
}
