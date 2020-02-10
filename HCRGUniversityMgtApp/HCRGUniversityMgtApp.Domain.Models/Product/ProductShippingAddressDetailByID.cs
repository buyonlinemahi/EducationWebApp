
namespace HCRGUniversityMgtApp.Domain.Models.Product
{
    public class ProductShippingAddressDetailByID
    {
       public string SAFirstName { get; set; }       
        public string SALastName { get; set; }        
        public string SAAddress { get; set; }      
        public string SAAddress2 { get; set; }        
        public string SACity { get; set; }
        public int SAStateID { get; set; }
        public string SAPostalCode { get; set; }
        public int ProductShoppingID { get; set; }
        public string StateName { get; set; }
        public int? ShippingPaymentID { get; set; }
    
    }
}
