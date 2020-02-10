using HCRGUniversityApp.Domain.Models.CertificationTitleOption;
using HCRGUniversityApp.Domain.Models.MyEducationDetailModel;
using HCRGUniversityApp.Domain.Models.Product;
using HCRGUniversityApp.Domain.Models.UserModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.MyEducationDetailViewModel
{
    public class MyEducationDetailViewModel
    {
        public IEnumerable<MyEducationDetail> MyEducationDetailResults { get; set; }
        public IEnumerable<MyEducationDetail> MyEducationDetailCompletedResults { get; set; }
        public int TotalItemCount { get; set; }
        public int Skip { get; set; }
        public int TotalInprocessItemCount { get; set; }
        public PagedProductPurchase ProductPurchaseDetails { get; set; }
        public UserAllAccessPass UserAllAccessPassResults { get; set; }
        
    }
}
