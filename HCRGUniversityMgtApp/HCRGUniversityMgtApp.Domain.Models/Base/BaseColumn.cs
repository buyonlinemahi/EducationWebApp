using System;

namespace HCRGUniversityMgtApp.Domain.Models.Base
{
    [Serializable]
    public class BaseColumn
    {
        public int ClientID { get; set; }
        public int OrganizationID { get; set; }
        public int ClientTypeID { get; set; }
    }
}
