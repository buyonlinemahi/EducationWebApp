using System;

namespace HCRGUniversityApp.Domain.Models.EducationShoppingModel
{
    public class EducationShopping
    {
        public int EducationShoppingID { get; set; }
        public int UserID { get; set; }
        public int EducationID { get; set; }
        public int EducationTypeAID { get; set; }
        public int Quantity { get; set; }
        public int? CoupanID { get; set; }
        public decimal Grandtotal { get; set; }
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public int? ShippingPaymentID { get; set; }
        public int? UserAllAccessPassID { get; set; }
        public decimal? TaxPercentage { get; set; }
    }
}
