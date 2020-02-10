using System;

namespace HCRGUniversityApp.Domain.Models.Event
{
    public class EventDetail
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventDescription { get; set; }
        public int? NewsID { get; set; }
        public int? EducationID { get; set; }
        public string CourseName { get; set; }
        public string NewsTitle { get; set; }
        public string NewsType { get; set; }
        public DateTime? CourseStartDate { get; set; }
        public string CoursePresenterName { get; set; }
        public string CourseLocation { get; set; }
        public string CourseStartTime { get; set; }
        public string pageUrl { get; set; }
        public int OrganizationID { get; set; }
    }

}
