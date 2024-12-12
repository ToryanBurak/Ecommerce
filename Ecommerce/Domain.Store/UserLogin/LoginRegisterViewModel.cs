using Domain.Store.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store.UserLogin
{
    public class LoginRegisterViewModel
    {
        public RegisterViewModel? Register { get; set; }
        public LoginViewModel? Login { get; set; }
    }

    public class RegisterViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string Phone { get; set; }

        public GenderEnum Gender { get; set; }

        public string TC { get; set; }

        public string Address { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string? Error { get; set; }

        public RegisterResponse? RegisterResponse { get; set; }
    }

    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public bool IsSuccessfull { get; set; }

        public string ErrorMessage { get; set; }
    }
}
