using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCRGUniversityApp.Domain.Models.OrganizationModel;

namespace HCRGUniversityApp.Domain.ViewModels.OrganizationViewModel
{
    public class OrganizationViewModel
    {
        public IEnumerable<OrganizationContact> OrganizationContactResults { get; set; }
    }
}
