using HCRGUniversityApp.Domain.Models.MyEducationDetailModel;
using HCRGUniversityApp.Domain.Models.UserModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.MyEducationDetailViewModel
{
    public class MyEducationAccountHistoryViewModel
    {
        public IEnumerable<MyEducationAccountHistory> myEducationAccountHistory { get; set; }
        public UserAllAccessPass UserAllAccessPassResults { get; set; }
        public int TotalItemCount { get; set; }
    }
}
