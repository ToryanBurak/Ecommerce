using Domain.Store.Enum;
using RentACar.Global.User;
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

        public string Phone { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string? Error { get; set; }

        public RegisterResponse? RegisterResponse { get; set; }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginResponse? LoginResponse { get; set; }
    }

    public class RegisterResponse
    {
        public bool IsSuccessfull { get; set; }

        public string ErrorMessage { get; set; }
    }
}
