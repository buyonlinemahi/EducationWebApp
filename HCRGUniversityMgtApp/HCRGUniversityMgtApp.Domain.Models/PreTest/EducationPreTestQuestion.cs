
namespace HCRGUniversityMgtApp.Domain.Models.PreTestModel
{
   public class EducationPreTestQuestion
    {
        public int CoursePreTestID { get; set; }
        public int PreTestID { get; set; }
        public int EducationID { get; set; }
        public bool? IsActive { get; set; }
    }
}
