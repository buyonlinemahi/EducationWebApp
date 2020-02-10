

using System;
namespace HCRGUniversityApp.Domain.Models.OrganizationModel
{
    [Serializable]
   public  class Organization
    {

        public int OrganizationID { get; set; }
        public string EncryptedOrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public string WebsiteName { get; set; }
        public int? ThemeID { get; set; }
        public string LogoImage { get; set; }
        public string LogoPath { get; set; }
        public string RegisteredPaypalEmailID { get; set; }
        public string FaviconImage { get; set; }
        public string MenuIDs { get; set; }
        
    }
}
