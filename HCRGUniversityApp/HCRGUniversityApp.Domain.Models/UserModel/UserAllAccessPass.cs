using System;

namespace HCRGUniversityApp.Domain.Models.UserModel
{
    public class UserAllAccessPass
    {
        public int UserAllAccessPassID { get; set; }
        public int UserID { get; set; }
        public int? AAPCouponID { get; set; }
        public bool? IsAllAccessPricingAmountRecevied { get; set; }
        public DateTime? AllAccessStartDate { get; set; }
        public DateTime? AllAccessEndDate { get; set; }
        public int? ShippingPaymentID { get; set; }
        public string AllAccessValidEndDate { get; set; }
        
    }
}
