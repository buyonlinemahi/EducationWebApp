using HCRGUniversityApp.Domain.Models.ShippingPayment;
using HCRGUniversityApp.Domain.Models.Common;
using System.Collections.Generic;

using HCRGUniversityApp.Domain.Models.DiscountCouponModel;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;

namespace HCRGUniversityApp.Domain.ViewModels.ShippingPaymentViewModel
{
    public class ShippingPaymentViewModel
    {
        public ShippingPayment ShippingPaymentResults { get; set; }
        public BillingAddress BillingAddressResults { get; set; }
        public ShippingAddress ShippingAddressResults { get; set; }
        public ShippingMethod ShippingMethodResults { get; set; }

        public IEnumerable<ShippingMethod> ShippingMethodRecordResults { get; set; }
        public IEnumerable<State> StateResults { get; set; }

        public IEnumerable<BillingAddress> BillingAddressRecordResults { get; set; }
        public int TotalItemCountBA { get; set; }
        
        public IEnumerable<ShippingAddress> ShippingAddressRecordResults { get; set; }
        public int TotalItemCountSA { get; set; }

        public IEnumerable<EducationShoppingCart> EducationShoppingCartTempResults { get; set; }
        public int TotalItemCountESC { get; set; }

        public int ShippingTabDisable { get; set; }

        public IEnumerable<DiscountCoupon> DiscountCouponResults { get; set; }

        public string SAStateName { get; set; }
    }
}
