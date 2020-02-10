using System.Web;

namespace HCRGUniversityMgtApp.Domain.Models.Client
{
    public class Organization
    {
        public int OrganizationID { get; set; }
        public string EncryptedOrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string WebsiteName { get; set; }
        public int? ThemeID { get; set; }
        public string LogoImage { get; set; }       
        public bool? IsCertificate { get; set; }
        public bool? IsWebPortal { get; set; }
        public HttpPostedFileBase OrganizationImageFile { get; set; }
        public string OrganizationImageFileContent { get; set; }
        public string FaviconImage { get; set; }
        public HttpPostedFileBase FaviconImageFile { get; set; }
        public string Message { get; set; }
        public string RegisteredPaypalEmailID { get; set; }
        public int ClientTypeID { get; set; }
        public string MenuIDs { get; set; }
    }
}
