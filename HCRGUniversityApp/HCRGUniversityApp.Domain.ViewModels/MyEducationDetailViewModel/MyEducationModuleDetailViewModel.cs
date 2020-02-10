using HCRGUniversityApp.Domain.Models.MyEducationDetailModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.MyEducationDetailViewModel
{
   public class MyEducationModuleDetailViewModel
    {
       public IEnumerable<MyEducationModuleDetail> MyEducationModuleDetailResults { get; set; }
       public MyEducationModuleDetail MyEducationModuleDetail { get; set; }
     
       public int TotalModules { get; set; }
       public int CompletedModules { get; set; }
       public int Hours { get; set; }
       public int Minutes { get; set; }
       public int Seconds { get; set; }
       public int UserID { get; set; }
       public bool IsRevisit { get; set; }
       public string SessionTimeout { get; set; }
    }
}
