
using System;
namespace HCRGUniversityApp.Domain.Models.EducationShoppingCartModel
{
    public class EducationShoppingCart
    {
        public int TempID { get; set; }
        public int EduorProID { get; set; }
        public int UserID { get; set; }
        public int Quantity { get; set; }
        public int? CoupanID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string PName { get; set; }
        public string CartType { get; set; }
        public string PType { get; set; }
        public decimal Price { get; set; }
        public int? CredentialID { get; set; }
        public int EducationTypeID { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? ShippingPaymentID { get; set; }
        public int? UserAllAccessPassID { get; set; }
        public decimal? TaxPercentage { get; set; }
    }
}
