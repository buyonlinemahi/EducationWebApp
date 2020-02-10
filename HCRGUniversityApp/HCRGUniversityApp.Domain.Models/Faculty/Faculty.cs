using System.Web;
namespace HCRGUniversityApp.Domain.Models.FacultyModel
{
    public class Faculty
    {
        public int FacultyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string ProfessionalTitle { get; set; }
        public string AddressStreet { get; set; }
        public string AddressFloor { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AreaOfExpertise { get; set; }
        public string Topics { get; set; }
        public string Resume { get; set; }
        public HttpPostedFileBase ResumeFile { get; set; }
        public int OrganizationID { get; set; }
    }
}
