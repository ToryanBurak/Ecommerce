using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backoffice.Enum
{
    public enum OrderStatusEnum
    {
        [Description("Kabul Edildi")]
        Approved=0,
        [Description("Hazırlanıyor")]
        Pending = 1,
        [Description("Yolda")]
        In_Transit = 2,
        [Description("Teslim Edildi")]
        Delivered = 3,

    }
}
