using Domain.Backoffice.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backoffice.UserLogin
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginResponse? LoginResponse { get; set; }
    }
}
