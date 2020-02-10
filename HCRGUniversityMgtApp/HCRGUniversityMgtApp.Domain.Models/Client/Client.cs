using System;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Client
{
    [Serializable]
    public class Client : Base.BaseDataColumn
    {
        public int ClientID { get; set; }
        public int ClientTypeID { get; set; }
        public string EncryptedClientID { get; set; }
        public int OrganizationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateID { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public bool? IsApproved { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
        public string TempPassword { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsEmailSent { get; set; }
        public string LogoPath { get; set; }
        public string ClientTypeName { get; set; }
        public string ClientSessionID { get; set; }
        public List<string> ClientMenuIDS { get; set; }       
         
    }
   
}
