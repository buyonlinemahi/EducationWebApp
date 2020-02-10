using System;

namespace HCRGUniversityApp.Domain.Models.EducationShoppingTempModel
{
    public class EducationShoppingTemp
    {
        public decimal Amount { get; set; }
        public int? CoupanID { get; set; }
        public DateTime Date { get; set; }
        public int EducationID { get; set; }
        public int EducationShoppingTempID { get; set; }
        public int EducationTypeID { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
        public string CourseName { get; set; }
        public int? CredentialID { get; set; }
        public int? ShippingPaymentID { get; set; }
        public int? UserAllAccessPassID { get; set; }
        public decimal? TaxPercentage { get; set; }
    }
}
