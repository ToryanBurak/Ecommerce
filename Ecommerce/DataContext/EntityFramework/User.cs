using System;
using System.Collections.Generic;

namespace DataContext.EntityFramework
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int VerifyState { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public string Phone { get; set; } = null!;
        public string Tc { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int? ImageUrlId { get; set; }
        public string VerifyConfirmCode { get; set; } = null!;
        public string SaltString { get; set; } = null!;
        public string? Guid { get; set; }
    }
}
