using HCRGUniversityApp.Domain.Models.DiscountCouponModel;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.EducationShoppingCartTempViewModel
{
    public class EducationShoppingCartTempViewModel
    {
        public IEnumerable<EducationShoppingCart> EducationShoppingCartTempResults { get; set; }
        public   IEnumerable<DiscountCoupon> DiscountCouponResults { get; set; }
    }
}
