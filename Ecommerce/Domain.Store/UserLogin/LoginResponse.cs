using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Global.User
{
    public class LoginResponse
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public int UserID { get; set; }
    }
}
