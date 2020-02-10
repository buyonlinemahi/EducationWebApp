using System;

namespace HCRGUniversityMgtApp.Domain.Models.UserModel
{
    public class UserSubscription : Base.BaseColumn
    {
        public int UserSubscriptionID { get; set; }
        public string EncryptedUserSubscriptionID { get; set; }
        public decimal IndividualAccessPricing { get; set; }
        public decimal IndividualAccessPricingStart { get; set; }
        public decimal IndividualAccessPricingEnd { get; set; }
        public string IndividualAccessTermsOfService { get; set; }
        public decimal AllAccessPassPricing { get; set; }
        public decimal AllAccessParametersCoursePriceStart { get; set; }
        public decimal AllAccessParametersCoursePriceEnd { get; set; }
        public bool? AllAccessIncludeProgram { get; set; }
        public string AllAccessTermsOfService { get; set; }
        public decimal EnterprisePricing { get; set; }
        public string EnterpriseTermsOfService { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Mode { get; set; }
        public bool? IsAllAccessIncludeProgram { get; set; }
        public string OrganizationName { get; set; }
    }
}
