using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Store
{
    public class CheckoutViewModel
    {
        public CartDO Cart { get; set; }
        public AddressDO Adress { get; set; }
        public int? CityKey { get; set; }
        public int? DistrictKey { get; set; }
        public bool UseSavedAdress { get; set; }
    }
}
