using HCRGUniversityMgtApp.Domain.Models.DiscountCouponModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.DiscountCouponViewModel
{
    public class DiscountCouponViewModel
    {
        public IEnumerable<DiscountCoupon> DiscountCouponResults { get; set; }
    }
}
