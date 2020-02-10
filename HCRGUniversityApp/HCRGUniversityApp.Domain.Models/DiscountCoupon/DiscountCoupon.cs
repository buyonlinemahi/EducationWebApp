using System;

namespace HCRGUniversityApp.Domain.Models.DiscountCouponModel
{
    public class DiscountCoupon
    {
        public bool CoupanValid { get; set; }
        public string CouponCode { get; set; }
        public decimal CouponDiscount { get; set; }
        public int CouponEducationID { get; set; }
        public DateTime CouponExpiryDate { get; set; }
        public int CouponID { get; set; }
        public DateTime CouponIssueDate { get; set; }
        public int CouponProduactID { get; set; }
        public string CouponType { get; set; }
    }
}
