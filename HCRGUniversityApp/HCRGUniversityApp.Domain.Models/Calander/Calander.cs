using System;

namespace HCRGUniversityApp.Domain.Models.Calander
{
    public class Calander
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public string eventDesc { get; set; }
        public string CourseLocation { get; set; }
        public string courseStartTime { get; set; }
        public string CoursePresenterName { get; set; }
        public string coursePageUrl { get; set; }
        public string eventType { get; set; }
        public string EventCourseNews { get; set; }
        public int OrganizationID { get; set; }
    }
}
