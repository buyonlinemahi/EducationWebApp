
namespace HCRGUniversityMgtApp.Domain.Models.ProfessionModel
{
   public class Profession : Base.BaseColumn
    {
          public int ProfessionID { get; set; }
          public string ProfessionTitle { get; set; }
          public bool IsActive { get; set; }
          public bool flag { get; set; }
          public string OrganizationName { get; set; } 
    }
}
