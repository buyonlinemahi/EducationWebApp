using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.ShippingPayment
{
    public class BillingAddress
    {
        public int BillingAddressID { get; set; }

        public int BAUserID { get; set; }

        public string BAFirstName { get; set; }
        public string BALastName { get; set; }
        public string BAAddress { get; set; }
        public string BAAddress2 { get; set; }
        public string BACity { get; set; }
        public int BAStateID { get; set; }
        public string BAPostalCode { get; set; }
        public string BAPhone { get; set; }
        public bool? IsSABillingAddressSame { get; set; }
        public int ShippingPaymentID { get; set; }
        public bool? IsSaveBillingAddress { get; set; }

    }
}
