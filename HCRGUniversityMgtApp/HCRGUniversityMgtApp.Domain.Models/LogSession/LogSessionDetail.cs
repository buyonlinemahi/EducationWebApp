using System;

namespace HCRGUniversityMgtApp.Domain.Models.LogSessionModel
{
    public class LogSessionDetail
    {
        public int LogSessionID { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string PageUrl { get; set; }
        public string Browser { get; set; }
        public int MEID { get; set; }
        public DateTime LogCreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string CourseName { get; set; }
    }
}
