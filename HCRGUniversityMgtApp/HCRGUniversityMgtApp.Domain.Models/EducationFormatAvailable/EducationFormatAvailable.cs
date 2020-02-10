
namespace HCRGUniversityMgtApp.Domain.Models.EducationFormatAvailableModel
{
  public class EducationFormatAvailable
    {
    
        public int EducationAvailableID { get; set; }     
        public int EducationID { get; set; }      
        public int EducationFormatID { get; set; }
        public bool IsActive { get; set; }
    }

}
