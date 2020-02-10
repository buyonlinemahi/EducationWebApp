using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.UserModel;


namespace HCRGUniversityMgtApp.Domain.ViewModels.UserSubscriptionViewModel
{
    public class UserSubscriptionViewModel
    {
        public IEnumerable<UserSubscription> UserSubscriptionResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
