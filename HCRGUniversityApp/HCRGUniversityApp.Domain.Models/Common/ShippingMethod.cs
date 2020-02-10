using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.Common
{
    public class ShippingMethod
    {
        public int ShippingMethodID { get; set; }
        public string ShippingMethodName { get; set; }
        public decimal ShippingMethodAmount { get; set; }
    }
}
