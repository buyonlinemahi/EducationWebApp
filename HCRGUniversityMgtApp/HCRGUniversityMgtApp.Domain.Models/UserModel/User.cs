using System;

namespace HCRGUniversityMgtApp.Domain.Models.UserModel
{
    [Serializable()]
    public class User : Base.BaseColumn
    {
        public int UID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserFullName { get; set; }

        public string Password { get; set; }

        public string EmailID { get; set; }

        public string Company { get; set; }

        public string Phone { get; set; }

        public string ProfessionalTitle { get; set; }

        public bool IsActive { get; set; }

        public bool IsLocked { get; set; }

        public string Message { get; set; }

        public int FailedAttemptCount { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public string Role { get; set; }

        public bool? IsManagement { get; set; }

        public int? StateID { get; set; }

        public bool? IsCoursePreview { get; set; }

        public string IsPasswordDirty { get; set; }

        public bool? IsVerified { get; set; }

        public DateTime? VerifiedOn { get; set; }

        public bool? IsCleared { get; set; }

        public DateTime? ClearedOn { get; set; }

        public int? ClearedBy { get; set; }

        public int UserMenuGroupID { get; set; }

        public string SpecialMenuIDs { get; set; }
        public string ClientTypeName { get; set; }
    }
}
