
namespace HCRGUniversityMgtApp.Domain.Models.PlanModel
{
    public class CoursePlan
    {     
        public int CoursePlanID { get; set; }       
        public int PlanID { get; set; }      
        public int ClientID { get; set; } 
        public int CourseID { get; set; }
        public int[] MultipleCourseID { get; set; }
    }
}
