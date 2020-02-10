using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.ShippingPayment
{
    public class ShippingPayment
    {
        public int ShippingPaymentID { get; set; }
        public int UserID { get; set; }
        public int BillingAddressID { get; set; }
        public int ShippingAddressID { get; set; }
        public int ShippingMethodID { get; set; }
        public int? CreatedBy { get; set; }
        public System.DateTime? CreatedOn { get; set; }
        public bool? IsPaymentRecevied { get; set; }
        public string TransactionNumber { get; set; }
        public System.DateTime? TransactionDatetime { get; set; }
        public string PaymentStatus { get; set; }
    }
}
