using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store.Enum
{
    public enum VerifyStateEnum
    {
        [Display(Name = "Doğrulanmamış")]
        Unverified = 0,

        [Display(Name = "Doğrulanmış")]
        Verified = 1
    }
}
