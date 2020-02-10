using System;

namespace HCRGUniversityMgtApp.Domain.Models.EventModel
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventDescription { get; set; }
        public int? NewsID { get; set; }
        public int? EducationID { get; set; }
        public bool flag { get; set; }
        public string CourseName { get; set; }
        public string NewsTitle { get; set; }
        public string OrganizationName { get; set; }
        public int OrganizationID { get; set; }
    }
}
