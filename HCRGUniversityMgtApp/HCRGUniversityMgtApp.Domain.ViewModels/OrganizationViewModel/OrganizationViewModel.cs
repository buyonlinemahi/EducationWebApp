using HCRGUniversityMgtApp.Domain.Models.Client;
using HCRGUniversityMgtApp.Domain.Models.OrganizationContact;
using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.ViewModels.OrganizationViewModel
{
    public class OrganizationViewModel
    {
       public IEnumerable<Organization> OrganizationResults { get; set; }
       public IEnumerable<OrganizationContact> OrganizationContactResults { get; set; }
       public bool IsHCRGAdmin { get; set; }




    }
}

