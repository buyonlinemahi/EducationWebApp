﻿

namespace HCRGUniversityMgtApp.Domain.Models.OrganizationContact
{
    public class OrganizationContact : Base.BaseColumn
    {
        public int OrganizationContactID { get; set; }
        public string EncryptedOrganizationContactID { get; set; }
        public string OrganizationName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public string Zip { get; set; }
        public string OperationHour { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
