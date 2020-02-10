using HCRGUniversityMgtApp.Domain.Models.DiscountCouponForCoursesModel;
using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.Models.DiscountCouponModel
{
    public class PagedDiscountCoupon
    {
        public IEnumerable<DiscountCoupon> DiscountCoupans { get; set; }
        public IEnumerable<DiscountCouponForCourses> DiscountCoupansForCourse { get; set; }
        public int PagedRecords { get; set; }
        public int TotalCount { get; set; }

    }
}
