using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.ShippingPayment
{
    public class PaypalDetail
    {
        public string URL { get; set; }
        public string cmd { get; set; }
        public string business { get; set; }
        public string no_shipping { get; set; }
        public string return_url { get; set; }
        public string rm { get; set; }
        public string cancel_url { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public string request_id { get; set; }
        public string no_note { get; set; }
        public string invoice { get; set; }
        public string notify_url { get; set; }
        public string txn_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string ProductName { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountDisplay { get; set; }
        public int NumberOfCredits { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public string FollowUpMessage { get; set; }
        public string address_override { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string Status { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
