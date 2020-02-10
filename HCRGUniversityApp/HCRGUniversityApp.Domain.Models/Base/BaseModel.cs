using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.Base
{
    [Serializable]
    public class BaseModel
    {
        public int OrganizationID { get; set; }
        public int ClientID { get; set; }
    }
}
