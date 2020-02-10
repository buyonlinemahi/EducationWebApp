using HCRGUniversityApp.Domain.Models.ResourceModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.ResourceViewModel
{
    public class ResourceViewModel
    {
        public IEnumerable<Resource> ResourceResults { get; set; }
    }
}
