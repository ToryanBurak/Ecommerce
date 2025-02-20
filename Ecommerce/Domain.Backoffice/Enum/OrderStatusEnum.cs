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
        [Description("Kabul Bekleniyor")]
        ApproveWaiting = 0,
        [Description("Kabul Edildi")]
        Approved = 1,
        [Description("Hazırlanıyor")]
        Pending = 2,
        [Description("Yolda")]
        In_Transit = 3,
        [Description("Teslim Edildi")]
        Delivered = 4

    }
}
