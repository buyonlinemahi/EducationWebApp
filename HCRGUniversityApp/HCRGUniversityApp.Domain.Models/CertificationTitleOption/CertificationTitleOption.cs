using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.CertificationTitleOption
{
    public class CertificationTitleOption
    {
        public int CertificationTitleOptionID { get; set; }
        public string CertificationTitle { get; set; }
        public string CertificationContent { get; set; }
        public int EducationId { get; set; }
    }
}
