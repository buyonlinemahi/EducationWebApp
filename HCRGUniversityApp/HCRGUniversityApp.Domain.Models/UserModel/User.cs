using HCRGUniversityApp.Domain.Models.Common;
using System;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.Models.UserModel
{
    [Serializable()]
    public class User:Base.BaseModel
    {
        public int UID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string EmailID { get; set; }

        public bool RememberMe { get; set; }

        public string Company { get; set; }

        public string Phone { get; set; }

        public string ProfessionalTitle { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public int FailedAttemptCount { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? IsAllAccessPricing { get; set; }

        public int? UserAllAccessPassID { get; set; }

        public bool? IsCoursePreview { get; set; }

        public bool? IsVerified { get; set; }

        public DateTime? VerifiedOn { get; set; }

        public bool? IsCleared { get; set; }

        public DateTime? ClearedOn { get; set; }

        public int? ClearedBy { get; set; }

        public int UserMenuGroupID { get; set; }

        public IEnumerable<Menu> MenuIDs { get; set; }

        public string SpecialMenuIDs { get; set; }

        public string PageName { get; set; }

        public string UserSessionID { get; set; }

        public string Message { get; set; }
        public string PasswordOTP { get; set; }

        public string PasswordOTP2 { get; set; }

        /*public bool? IsAllAccessPricingAmountRecevied { get; set; }

        public DateTime? AllAccessStartDate { get; set; }

        public DateTime? AllAccessEndDate { get; set; }*/


    }
}
