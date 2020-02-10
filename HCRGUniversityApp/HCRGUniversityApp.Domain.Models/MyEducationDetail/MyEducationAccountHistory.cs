using System;
namespace HCRGUniversityApp.Domain.Models.MyEducationDetailModel
{
    public class MyEducationAccountHistory
    {
        public int MEID { get; set; }
        public int UserID { get; set; }
        public int EducationID { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string PurchaseDate { get; set; }
        public string CourseName { get; set; }
        public decimal? Price { get; set; }
        public string AllAccess { get; set; }
        public int? UserAllAccessPassID { get; set; }
        public string TransactionNumber { get; set; }
        public string PaymentStatus { get; set; }
    }
}
