using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.EnterprisePackageRegisterModel
{
    public class EnterprisePackageRegister
    {
        public int EPRID { get; set; }
        public string EPRName { get; set; }
        public string EPRPhone { get; set; }
        public string EPREmail { get; set; }
        public string EPRCompany { get; set; }
    }
}
